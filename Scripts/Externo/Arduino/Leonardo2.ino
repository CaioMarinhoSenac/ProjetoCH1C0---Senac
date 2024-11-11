#include <NewPing.h>
#include <Keyboard.h>
#include <Adafruit_NeoPixel.h>
#ifdef __AVR__
#include <avr/power.h> // Necessário para Adafruit Trinket 16 MHz
#endif

// Configurações
const int pinoSensor = 8; // PINO DIGITAL UTILIZADO PELO SENSOR
#define SONAR_NUM 3       // Número de sensores
#define MAX_DISTANCE 200  // Distância máxima (em cm) para o sensor ultrassônico
#define LED_PIN 11        // Pino do NeoPixel LED
#define NUMPIXELS 16      // Número de LEDs no NeoPixel

Adafruit_NeoPixel pixels(NUMPIXELS, LED_PIN, NEO_GRB + NEO_KHZ800);
NewPing sonar[SONAR_NUM] = {
  NewPing(3, 2, MAX_DISTANCE),
  NewPing(5, 4, MAX_DISTANCE),
  NewPing(7, 6, MAX_DISTANCE)
};

int distances[SONAR_NUM]; // Array para armazenar leituras dos sensores

void setup() {
  pinMode(pinoSensor, INPUT_PULLUP); // Define o pino como entrada
  #if defined(__AVR_ATtiny85__) && (F_CPU == 16000000)
  clock_prescale_set(clock_div_1);
  #endif

  pixels.begin();       // Inicializa o NeoPixel
  delay(13000);         // Atraso inicial
  Serial.begin(9600);
  Keyboard.begin();
}

void loop() {
  // Leitura de todos os sensores e armazenamento dos resultados
  for (uint8_t i = 0; i < SONAR_NUM; i++) {
    delay(50); // Aguarda 50ms entre leituras
    distances[i] = sonar[i].ping_cm();
    Serial.print("Sensor ");
    Serial.print(i);
    Serial.print(" = ");
    Serial.print(distances[i]);
    Serial.print(" cm ");
  }
  Serial.println();

  pixels.clear();  // Limpa o preset dos LEDs

  processReadings(); // Chama a função para processar as leituras dos sensores

  if (digitalRead(pinoSensor) == LOW) {
    Serial.println("Sensor 0");
    Serial.println("feliz");
    Keyboard.write('p');  // Emoção feliz
    Keyboard.write('d');  // Direção
    delay(10);
  }
}

void processReadings() {
  // Processa as leituras dos sensores e toma decisões
  for (uint8_t i = 0; i < SONAR_NUM; i++) {
    if (distances[i] < 50 && distances[i] > 0) { // Verifica se a leitura está abaixo de 50 cm
      switch (i) {
        case 0:
          Serial.println("Sensor 0");
          Serial.println("feliz");
          Keyboard.write('u');  // Emoção thug life
          Keyboard.write('z');  // Direção
          setAllPixelsColor(150, 0, 0); // Define cor vermelha para todos os LEDs
          delay(10);
          break;

        case 1:
          Serial.println("Sensor 1");
          Serial.println("dormindo");
          Keyboard.write('p');  // Emoção dormindo
          Keyboard.write('d');  // Direção
          setAllPixelsColor(150, 0, 0); // Define cor vermelha para todos os LEDs
          delay(10);
          break;

        case 2:
          Serial.println("Sensor 2");
          Serial.println("medo");
          Keyboard.write('b');  // Emoção medo
          Keyboard.write('c');  // Direção
          setAllPixelsColor(150, 0, 0); // Define cor vermelha para todos os LEDs
          delay(10);
          break;

        default:
          Serial.println("Unknown sensor");
          break;
      }
    }
  }
}

// Função para definir a cor de todos os LEDs
void setAllPixelsColor(uint8_t red, uint8_t green, uint8_t blue) {
  pixels.clear(); // Apaga todos os LEDs
  for (int i = 0; i < NUMPIXELS; i++) {
    pixels.setPixelColor(i, pixels.Color(red, green, blue));
  }
  pixels.show(); // Atualiza o NeoPixel
}