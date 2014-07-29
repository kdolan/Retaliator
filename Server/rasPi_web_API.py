from bottle import route, run, get, post, request
import serial
import copy
import time
from multiprocessing import Process

API_KEY = "123"
GUN_ADRR = None;
CAR_ADRR = None;

gunSer = None;
carSer = None;

"""
Sends a command to the gun arduino to fire and waits for a responce.
Returns: String indicating result of command
"""
def gun_fire():
    gunSer.write("F")
    responce = gunSer.readline() #Will timeout after 1 sec
    if(responce == None):
        returnString = "ERROR - No responce from Gun Arduino after Fire command"
    elif(responce == "0\r\n"):
        returnString = "SUCCESS - Gun Fired"
    else:
        returnString = "WARNING - Arduino returned unexpected responce: '" + responce + "'"

    print(returnString)
    return returnString;

def gun_aim(rotation, tilt):
    pass

def car_Drive(LF, RF, LR, RR): #TODO: Need to include leading zeros here.
    LF = "%03d" % LF;
    RF = "%03d" % RF;
    LR = "%03d" % LR;
    RR = "%03d" % RR;

    writeString = "D"+str(LF)+str(RF)+str(LR)+str(RR)
    print(writeString)
    carSer.write(writeString)
    responce = carSer.readline() #Will timeout after 1 sec
    if(responce == None):
        returnString = "ERROR - No responce from Car Arduino after Drive command"
    elif(responce == "0\r\n"):
        returnString = "SUCCESS - Driving"
    else:
        returnString = "WARNING - Arduino returned unexpected responce: '" + responce + "'"
    print(returnString)
    return returnString

@get('/gun/aim')
def aimGun():
    rotation = request.GET.get('rotation')
    tilt = request.GET.get('tilt')
    user_apiKey = request.GET.get('apiKey')

    if(API_KEY != user_apiKey):
        return "ERROR - INVALID API KEY"

    #Check rotation and tilt are ints
    try:
        rotation = int(rotation)
        tilt = int(tilt)
    except:
        return "ERROR - value error. Rotation and tilt must be of type int"

    #Range check rotation and tilt
    if((rotation >= 0 and rotation <= 360) == False):
        return "ERROR - rotation must be between 0 and 360"
    if((tilt >= 0 and tilt <= 180) == False):
        return "ERROR - tilt must be between 0 and 180"

    #transmit values to arduino

    return "SUCCESS"

@get('/gun/fire')
def fireGun():
    user_apiKey = request.GET.get('apiKey')

    if(API_KEY != user_apiKey):
        return "ERROR - INVALID API KEY"

    #transmit fire to arduino
    return gun_fire()

@get('/car/motor')
def carDrive():
    user_apiKey = request.GET.get('apiKey')
    LF = request.GET.get('LF')
    RF = request.GET.get('RF')
    LR = request.GET.get('LR')
    RR = request.GET.get('RR')

    if(API_KEY != user_apiKey):
        return "ERROR - INVALID API KEY"

    try:
        LF = int(LF)
        RF = int(RF)
        LR = int(LR)
        RR = int(RR)
    except:
        return "ERROR - value error. Paramaters must be of type int"

    if((LF>=1 and LF<=200 and RF>=1 and RF<=200 and LR>=1 and LR<=200 and RR>=1 and RR<=200)==False):
        return "ERROR - value error. Parameters must be between -100 and 100 (inclusive)"

    return car_Drive(LF,RF,LR,RR)
#Sync Arduinos
#while(GUN_ADRR == None): #and CAR_ADDR == None):
while(CAR_ADRR == None):
    bool_deviceFound = False
    deviceFound = ""

    try:
        if(GUN_ADRR != "ACM0" and CAR_ADRR != "ACM0"):
            print("Checking for Arduino on ACM0")
            ser = serial.Serial('/dev/ttyACM0',9600, timeout=1)
            bool_deviceFound = True
            deviceFound = "ACM0"
            print("Device found on ACM0")
    except:
        try:
            if(GUN_ADRR != "ACM1" and CAR_ADRR != "ACM1"):
                print("Checking for Arduino on ACM1")
                ser = serial.Serial('/dev/ttyACM1',9600, timeout=1)
                bool_deviceFound = True
                deviceFound = "ACM1"
                print("Device found on ACM1")
        except:
            pass
    if(bool_deviceFound):
        responce = ser.readline()
        if(responce == "GUN\r\n"):
            GUN_ADRR = deviceFound
            ser.write('1')
            ser.write('1')
            ser.write('1')
            print("GUN Arduino found on " + GUN_ADRR)
            ser.close()
        elif(responce == "CAR\r\n"):
            CAR_ADRR = deviceFound
            ser.write('1')
            ser.write('1')
            ser.write('1')
            ser.write('STOP')
            print("CAR Arduino found on " + CAR_ADRR)
            ser.close()


#After the loop has been completed
ser.close()
time.sleep(1)



#Open serial conections to the gun and the car
carSer = serial.Serial('/dev/tty'+CAR_ADRR,9600, timeout=1)
#gunSer = serial.Serial('/dev/tty'+GUN_ADRR,9600, timeout=1)

#carSer.flushInput()
#carSer.flushOutput()
#carSer.flush()

#Read any data in the buffer. (Things like CARCAR, AR, and other data from the handshake
try:
    while(True):
        x = carSer.read()
        if(x==""):
            break;
        #print(x)
except:
    pass

#gunSer.flushInput()
#gunSer.flushOutput()
#gunSer.flush()

#run(host='192.168.1.10', port=8080)
t = Process(target=run, kwargs=dict(host='192.168.1.10', port=8080))
t.daemon = True;
t.start()
t.join()
