from socket import AF_INET, SOCK_DGRAM, socket
import json

UDP_IP = "0.0.0.0"
UDP_PORT = 12345

# Opret en socket til at modtage UDP-pakker
sock = socket(AF_INET, SOCK_DGRAM)
sock.bind((UDP_IP, UDP_PORT))

received_data = []

print(f"UDP server listening on port {UDP_PORT}")

while True:
    data, addr = sock.recvfrom(1024)
    try:
        message = json.loads(data.decode('utf-8'))
        name = message.get('Name')
        url = message.get('Url')
        if name and url:
            print(f"Received Name: {name}, URL: {url}")
            received_data.append((name, url))
        else:
            print("Invalid message format")
    except json.JSONDecodeError:
        print("Received invalid JSON")

    response = json.dumps(received_data).encode('utf-8')
    sock.sendto(response, addr)