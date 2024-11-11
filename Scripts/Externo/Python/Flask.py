from flask import Flask, jsonify, request, send_from_directory, send_file

from datetime import datetime
import os
import time
import random
import cv2
import numpy as np

os.environ["TZ"] = "America/Recife"
time.tzset()

# Configuração do Flask
app = Flask(_name_)

# Diretório para armazenar as imagens
image_directory = "/home/visao/mysite/imagens"  # Caminho completo no PythonAnywhere
output_image_path = "/home/visao/mysite/imagens/temp_output.png"  # Altere para .png

#os.makedirs(image_directory, exist_ok=True)

# Endpoint para receber e armazenar a imagem
@app.route('/upload_image', methods=['POST'])
def upload_image():
    if 'image' not in request.files:
        return jsonify({"error": "Nenhuma imagem enviada"}), 400

    # Receber o arquivo da imagem
    file = request.files['image']
    timestamp = datetime.now().strftime("%d-%m-%Y-%H-%M-%S")
    filename = f"imagem-{timestamp}.jpg"
    file_path = os.path.join(image_directory, filename)

    # Salvar a imagem no servidor
    file.save(file_path)
    print(f"Imagem salva: {file_path}")
    return jsonify({"status": "Imagem salva com sucesso"}), 200

# Endpoint para fornecer a última imagem salva - comentada apos o recnplay
# @app.route('/latest_image', methods=['GET'])
def latest_image():
    images = sorted([f for f in os.listdir(image_directory) if f.endswith(".jpg")], reverse=True)
    if images:
        return send_from_directory(image_directory, images[0])
    else:
        return jsonify({"error": "Nenhuma imagem disponível"}), 404

# Endpoint para fornecer a última imagem salva
@app.route('/latest_image', methods=['GET'])
def random_image_grid():
    # Obter a lista de todas as imagens no diretório
    images = [f for f in os.listdir(image_directory) if f.endswith(".jpg")]

    # Verificar se há pelo menos 25 imagens para compor a grade
    if len(images) < 25:
        return jsonify({"error": "Menos de 25 imagens disponíveis"}), 404

    # Selecionar 25 imagens aleatórias
    selected_images = random.sample(images, 25)

    # Carregar e redimensionar cada imagem para o mesmo tamanho (ex: 100x100 pixels)
    image_tiles = []
    tile_size = (100, 100)
    for image_name in selected_images:
        image_path = os.path.join(image_directory, image_name)
        image = cv2.imread(image_path)
        if image is None:
            continue
        resized_image = cv2.resize(image, tile_size)
        image_tiles.append(resized_image)

    # Verificar se todas as 25 imagens foram carregadas corretamente
    if len(image_tiles) < 25:
        return jsonify({"error": "Erro ao carregar uma ou mais imagens"}), 500

    # Compor a imagem 5x5 em uma grade
    rows = []
    for i in range(0, 25, 5):
        row = np.hstack(image_tiles[i:i+5])  # Combina 5 imagens horizontalmente
        rows.append(row)

    grid_image = np.vstack(rows)  # Combina as 5 linhas para formar a grade 5x5

    # Salvar a imagem composta temporariamente com um nome de arquivo completo
    cv2.imwrite(output_image_path, grid_image)

    # Retornar a imagem composta
    return send_file(output_image_path, mimetype='image/jpeg')

@app.route('/random_image', methods=['GET'])
def random_image():
    # Obter a lista de todas as imagens no diretório
    images = [f for f in os.listdir(image_directory) if f.endswith(".jpg")]

    # Verificar se existem imagens disponíveis
    if images:
        # Selecionar uma imagem aleatoriamente
        random_image = random.choice(images)
        return send_from_directory(image_directory, random_image)
    else:
        return jsonify({"error": "Nenhuma imagem disponível"}), 404


# Endpoint para fornecer uma imagem específica pelo nome
@app.route('/get_image/<string:image_name>', methods=['GET'])
def get_image(image_name):
    image_path = os.path.join(image_directory, image_name)
    if os.path.exists(image_path):
        return send_from_directory(image_directory, image_name)
    else:
        return jsonify({"error": "Imagem não encontrada"}), 404

if _name_ == '_main_':
    app.run()