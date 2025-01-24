import json
import random
from socket import AF_INET, SO_BROADCAST, SOCK_DGRAM, SOL_SOCKET, socket
from time import sleep

SERVER_NAME = "255.255.255.255"
SERVER_PORT = 44556

MODELS = ["Ikea", "Herman Miller", "Steelcase", "Haworth", "Knoll"]
MIN_MAXWEIGHT = 50
MAX_MAXWEIGHT = 200
HAS_PILLOW = [True, False]

SECONDS = 2

clientSocket = socket(AF_INET, SOCK_DGRAM)
clientSocket.setsockopt(SOL_SOCKET, SO_BROADCAST, 1)

while True:
    model = random.choice(MODELS)
    has_pillow = random.choice(HAS_PILLOW)
    max_weight = random.randint(MIN_MAXWEIGHT, MAX_MAXWEIGHT)

    chair = {
        "Model": model,
        "MaxWeight": max_weight,
        "HasPillow": has_pillow
    }
    message = json.dumps(chair)
    clientSocket.sendto(message.encode(), (SERVER_NAME, SERVER_PORT))

    print(f"broadcasting: {message}")

    sleep(SECONDS)