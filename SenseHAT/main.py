from sense_hat import SenseHat
from time import sleep

TEMP_THRESHOLD = 34
HUMIDITY_LOW = 20
HUMIDITY_MEDIUM = 50
HUMIDITY_HIGH = 85

COLOR_NO_RISK = (255, 255, 255)     # Hvid
COLOR_LOW_RISK = (0, 255, 0)        # Grøn
COLOR_MEDIUM_RISK = (255, 255, 0)   # Gul
COLOR_HIGH_RISK = (255, 0, 0)       # Rød

sense = SenseHat()

def get_heatstroke_risk(temp, humidity):
    if temp < TEMP_THRESHOLD:
        return COLOR_NO_RISK
    elif humidity < HUMIDITY_LOW:
        return COLOR_NO_RISK
    elif humidity < HUMIDITY_MEDIUM:
        return COLOR_LOW_RISK
    elif humidity < HUMIDITY_HIGH:
        return COLOR_MEDIUM_RISK
    else:
        return COLOR_HIGH_RISK

while True:
    temp = sense.get_temperature()
    humidity = sense.get_humidity()

    color = get_heatstroke_risk(temp, humidity)

    sense.clear(color)
    sleep(1)