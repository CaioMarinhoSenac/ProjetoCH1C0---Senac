#include <NewPing.h>
#include <Keyboard.h>
#include <Servo.h>
#include <Adafruit_NeoPixel.h>
#ifdef __AVR__
#include <avr/power.h> // Required for 16 MHz Adafruit Trinket
#endif

const int pinoSensor = 12; //PINO DIGITAL UTILIZADO PELO SENSOR
#define LED_PIN     11 //pino do NeoPixel LED
#define NUMPIXELS  16 // Popular NeoPixel ring size

#define SONAR_NUM 4      // Number of sensors
#define MAX_DISTANCE 200 // Maximum distance (in cm) to ping

Adafruit_NeoPixel pixels(NUMPIXELS, LED_PIN, NEO_GRB + NEO_KHZ800);
Servo meuServo; // Cria um objeto Servo para controlar o servo motor
int pos; // Declara uma variável para controlar a posição do servo motor

NewPing sonar[SONAR_NUM] = {
  NewPing(3, 2, MAX_DISTANCE),
  NewPing(5, 4, MAX_DISTANCE),
  NewPing(7, 6, MAX_DISTANCE),
  NewPing(9, 8, MAX_DISTANCE),
};

int distances[SONAR_NUM]; // Array to store sensor readings

// Declaração da função processReadings antes do uso
void processReadings();

void setup() {
  pinMode(pinoSensor, INPUT_PULLUP); //DEFINE O PINO COMO ENTRADA
  meuServo.attach(13); // Associa o servo motor ao pino digital 13 do Arduino
  meuServo.write(0); // Define a posição inicial do servo motor para 0 graus

  #if defined(__AVR_ATtiny85__) && (F_CPU == 16000000)
  clock_prescale_set(clock_div_1);
  #endif

  pixels.begin(); // Inicializa o NeoPixel
  delay(13000);
  Serial.begin(9600);
  Keyboard.begin();
}

void loop() {
  // Lê todos os sensores e armazena os resultados no array distances
  for (uint8_t i = 0; i < SONAR_NUM; i++) {
    delay(50); // Aguarda 50ms entre as leituras
    distances[i] = sonar[i].ping_cm();
    Serial.print("Sensor ");
    Serial.print(i);
    Serial.print(" = ");
    Serial.print(distances[i]);
    Serial.print(" cm ");
  }
  Serial.println();

  processReadings(); // função de tomada de decisão baseada nos sensores

  if (digitalRead(pinoSensor) == LOW) { // Se a leitura do pino for LOW
    Serial.println("Sensor 0");
    Serial.println("feliz");
    Keyboard.write('p');  // emoção feliz
    Keyboard.write('d');  // direção
    meuServo.write(-60);

    pixels.clear(); // Limpa os LEDs

    // Define todos os LEDs para a cor vermelha
    for(int i = 0; i < NUMPIXELS; i++) {
      pixels.setPixelColor(i, pixels.Color(150, 0, 0));
    }
    pixels.show(); // Atualiza os LEDs
  }
}

// Função processReadings
void processReadings() {
  // Loop através das leituras dos sensores e toma decisões
  for (uint8_t i = 0; i < SONAR_NUM; i++) {
    if (distances[i] < 50 && distances[i] > 0) { // Checa se a leitura está abaixo de 50 cm
      switch (i) {
        case 0:
          Serial.println("Sensor 0");
          Serial.println("feliz");
          Keyboard.write('m');  // emoção thug life
          Keyboard.write('s');  // direção
          meuServo.write(15);

          pixels.clear();
          for(int j = 0; j < NUMPIXELS; j++) {
            pixels.setPixelColor(j, pixels.Color(255, 255, 255));
          }
          pixels.show();
          break;

        case 1:
          Serial.println("Sensor 1");
          Serial.println("dormindo");
          Keyboard.write('h');  // emoção apaixonado
          Keyboard.write('d');  // direção
          meuServo.write(40);

          pixels.clear();
          for(int j = 0; j < NUMPIXELS; j++) {
            pixels.setPixelColor(j, pixels.Color(255, 0, 0));
          }
          pixels.show();
          break;

        case 2:
          Serial.println("Sensor 2");
          Serial.println("medo");
          Keyboard.write('r');  // medo
          Keyboard.write('d');  // direção
          meuServo.write(80);

          pixels.clear();
          for(int j = 0; j < NUMPIXELS; j++) {
            pixels.setPixelColor(j, pixels.Color(150, 0, 150));
          }
          pixels.show();
          break;

        case 3:
          Serial.println("Sensor 3");
          Serial.println("Raiva");
          Keyboard.write('f');  // feliz
          Keyboard.write('a');  // direção
          meuServo.write(-60);

          pixels.clear();
          for(int j = 0; j < NUMPIXELS; j++) {
            pixels.setPixelColor(j, pixels.Color(0, 255, 255));
          }
          pixels.show();
          break;

        default:
          Serial.println("Unknown sensor");
          pixels.clear();
          for(int j = 0; j < NUMPIXELS; j++) {
            pixels.setPixelColor(j, pixels.Color(150, 150, 150));
          }
          pixels.show();
          break;
      }
    }
  }
}