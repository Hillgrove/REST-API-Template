from socket import AF_INET, SOCK_DGRAM, socket
import requests

SERVERPORT = 44556
SERVERADDRESS = ("", SERVERPORT)

API_ADDRESS = "https://exercise-b-set.azurewebsites.net/api/chairs"

serverSocket = socket(AF_INET, SOCK_DGRAM)
headersArray = {"Content-Type": "application/json"}

serverSocket.bind(SERVERADDRESS)
print("The server is ready to receive")
try:
    while True:
        message, clientAddress = serverSocket.recvfrom(2048)
        print(f"Received: {message.decode()} from {clientAddress}")
        response = requests.post(API_ADDRESS, data=message, headers=headersArray)
        print(response.text)

except KeyboardInterrupt:
    print("Server is shutting down")

finally:
    serverSocket.close()