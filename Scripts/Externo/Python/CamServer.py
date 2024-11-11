import cv2
import requests
from datetime import datetime
import time
import os

# Configurações
SERVER_URL = "https://visao.pythonanywhere.com/upload_image"
capture_interval = 30  # Tempo entre as capturas em segundos

def generate_heatmap(frame):
    # Converter para escala de cinza e aplicar blurring para ocultar detalhes
    gray_image = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    blurred_image = cv2.GaussianBlur(gray_image, (25, 25), 0)

    # Gerar o mapa de calor a partir da imagem desfocada
    heatmap_image = cv2.applyColorMap(blurred_image, cv2.COLORMAP_JET)
    return heatmap_image

def capture_and_send_image():
    # Inicializar a câmera
    cap = cv2.VideoCapture(0)
    cap.set(cv2.CAP_PROP_FRAME_WIDTH, 320)
    cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 240)

    while True:
        ret, frame = cap.read()
        if not ret:
            print("Erro ao capturar a imagem")
            time.sleep(1)
            continue

        # Gerar o mapa de calor
        heatmap_image = generate_heatmap(frame)

        # Salvar a imagem localmente
        timestamp = datetime.now().strftime("%d-%m-%Y-%H-%M-%S")
        filename = f"temp_image-{timestamp}.jpg"
        cv2.imwrite(filename, heatmap_image)

        # Enviar a imagem para o servidor
        with open(filename, "rb") as img_file:
            response = requests.post(SERVER_URL, files={"image": img_file})
            if response.status_code == 200:
                print(f"Imagem enviada com sucesso: {timestamp}")
            else:
                print("Erro ao enviar a imagem:", response.json())

        # Remover a imagem temporária
        os.remove(filename)

        # Aguardar o intervalo de captura
        time.sleep(capture_interval)

    # Fechar a captura de vídeo
    cap.release()

if _name_ == "_main_":
    capture_and_send_image()