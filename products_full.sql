-- ── CAPTEURS ÉLECTRONIQUES (CategoryId = 1) ────────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(1,
 'Capteur d''empreinte digitale',
 'Capteur biométrique permettant l''identification et la vérification d''identité par empreinte digitale. Compatible avec les microcontrôleurs Arduino et Raspberry Pi via liaison UART. Idéal pour les projets de contrôle d''accès, de sécurité embarquée et d''authentification.',
 7000.00, '/images/products/Empreinte.jpeg', 1, 50, true, '2026-03-09 15:33:54');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(2,
 'Capteur de température et humidité DHT22',
 'Capteur numérique haute précision pour la mesure de la température (−40 à +80 °C) et de l''humidité relative (0–100 % HR). Signal numérique calibré, interface à un seul fil, compatible Arduino, ESP32 et Raspberry Pi. Idéal pour les stations météo, serres et systèmes domotiques.',
 3000.00, '/images/products/DHT22.jpeg', 1, 60, true, '2026-03-09 15:20:35');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(3,
 'Capteur de température et humidité DHT11',
 'Capteur économique pour la mesure de la température (0–50 °C) et de l''humidité relative (20–90 % HR). Idéal pour les projets d''initiation et les applications domotiques simples ne nécessitant pas une haute précision. Compatible Arduino et ESP32.',
 2000.00, '/images/products/DHT11.jpeg', 1, 80, true, '2026-03-09 15:23:06');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(4,
 'Capteur de couleur TCS3200',
 'Capteur de couleur TCS3200 convertissant la lumière réfléchie en un signal de fréquence proportionnel à l''intensité lumineuse RGB. Idéal pour la détection et le tri de couleurs dans les projets de robotique, de tri automatisé et de contrôle qualité. Compatible Arduino et microcontrôleurs TTL.',
 3500.00, '/images/products/Couleur TCS3200.jpeg', 1, 10, true, '2026-03-09 18:22:14');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(5,
 'Capteur de poussière DSM501A',
 'Capteur de particules fines DSM501A permettant de mesurer la concentration de poussières dans l''air (PM2.5 et PM10). Basé sur le principe de diffusion de la lumière, il fournit un signal PWM exploitable par un microcontrôleur pour surveiller la qualité de l''air ambiant.',
 5000.00, '/images/products/DSM501A.jpeg', 1, 10, true, '2026-03-09 12:03:25');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(6,
 'Capteur de couleur TCS3472',
 'Capteur de couleur numérique TCS3472 avec interface I2C, offrant une meilleure précision et une meilleure résolution que le TCS3200. Il mesure les composantes rouge, vert, bleu et la luminosité globale (RGBC). Idéal pour la détection de couleurs dans des environnements variés, compatible Arduino et Raspberry Pi.',
 3500.00, '/images/products/Couleur TCS3472.jpeg', 1, 40, true, '2026-03-09 18:22:55');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(7,
 'Anémomètre',
 'Capteur de vitesse du vent basé sur une technologie à coupelles rotatives, fournissant un signal analogique ou à impulsions proportionnel à la vitesse du vent. Idéal pour les stations météorologiques, les systèmes de surveillance environnementale et les projets IoT outdoor.',
 5000.00, '/images/products/Anemometre.jpeg', 1, 0, true, '2026-03-09 17:25:11');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(8,
 'Module webcam Raspberry',
 'Module caméra haute résolution compatible Raspberry Pi, utilisant l''interface CSI pour un transfert rapide des données vidéo. Supporte la capture photo et la vidéo HD. Idéal pour les projets de vision par ordinateur, de surveillance et de reconnaissance d''images.',
 8000.00, '/images/products/Camera Raspberry.jpeg', 1, 20, true, '2026-03-09 17:42:08');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(9,
 'Capteur Reed',
 'Capteur à lame souple (reed switch) fonctionnant comme un interrupteur magnétique normalement ouvert. Il se ferme au contact d''un champ magnétique, permettant la détection d''ouverture de portes, de fenêtres ou la mesure de vitesse de rotation. Compatible avec toutes les cartes Arduino et microcontrôleurs.',
 2000.00, '/images/products/Capteur de commutateur.jpeg', 1, 40, true, '2026-03-09 17:45:26');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(10,
 'Capteur de courant',
 'Module de mesure de courant continu ou alternatif basé sur un capteur à effet Hall, permettant une mesure non invasive sans interruption du circuit. Fournit une tension analogique proportionnelle au courant mesuré, compatible avec les entrées ADC des cartes Arduino et ESP32.',
 4000.00, '/images/products/Courant.jpeg', 1, 30, true, '2026-03-09 17:48:18');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(11,
 'Capteur de tension',
 'Module de mesure de tension continue utilisant un pont diviseur de tension, permettant de mesurer des tensions supérieures à la plage d''entrée ADC du microcontrôleur. Idéal pour la surveillance de batteries, de sources d''alimentation et de systèmes solaires.',
 4000.00, '/images/products/tension.jpeg', 1, 30, true, '2026-03-09 17:50:47');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(12,
 'Capteur de tension V2',
 'Version améliorée du module de mesure de tension, offrant une meilleure précision et une plage de mesure étendue. Intègre un amplificateur opérationnel pour un signal de sortie plus stable. Compatible Arduino et ESP32.',
 2000.00, '/images/products/tension2.jpeg', 1, 40, true, '2026-03-09 17:53:00');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(13,
 'Capteur de débit d''eau',
 'Capteur à effet Hall mesurant le débit volumique d''eau (ou liquides non corrosifs) circulant dans une conduite. Fournit des impulsions proportionnelles au débit pour un comptage précis. Idéal pour les systèmes d''irrigation automatique, les fontaines et les projets de domotique hydraulique.',
 10000.00, '/images/products/Debit d''Eau.jpeg', 1, 50, true, '2026-03-09 17:54:07');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(14,
 'Capteur infrarouge E18-D80NK',
 'Capteur infrarouge de proximité réglable pour la détection d''obstacles à une distance de 3 à 80 cm. Fournit un signal numérique TTL. Résistant aux interférences lumineuses ambiantes, il est idéal pour la robotique mobile, les convoyeurs automatisés et les portes automatiques.',
 2500.00, '/images/products/INFRAROUGE.jpeg', 1, 50, true, '2026-03-09 17:56:48');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(15,
 'Capteur de pluie',
 'Module capteur de pluie LM393 conçu pour détecter la présence d''eau ou de gouttes de pluie sur sa surface conductrice. Fournit un signal numérique (seuil réglable via potentiomètre) et un signal analogique. Idéal pour les systèmes d''irrigation automatique, les alertes météo et les fermetures automatiques de fenêtres.',
 5000.00, '/images/products/PLUIE.jpeg', 1, 60, true, '2026-03-09 17:58:48');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(16,
 'Capteur de vibration',
 'Module de détection de chocs et de vibrations basé sur un capteur piézoélectrique ou à bille conductrice. Fournit un signal numérique lors d''un impact ou d''une secousse. Idéal pour les alarmes anti-chocs, les détecteurs d''intrusion et les projets de surveillance mécanique.',
 2500.00, '/images/products/vibrations.jpeg', 1, 50, true, '2026-03-09 18:00:27');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(17,
 'DHT11 ESP-01',
 'Combinaison d''un capteur de température/humidité DHT11 et du module WiFi ESP-01 (ESP8266), permettant l''envoi direct des mesures environnementales vers un serveur ou un cloud via WiFi. Solution compacte et économique pour les nœuds de surveillance IoT sans fil.',
 12000.00, '/images/products/DHT11-ESP.jpeg', 1, 50, true, '2026-03-09 18:06:43');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(18,
 'Capteur de pouls',
 'Module de mesure de fréquence cardiaque basé sur la photopléthysmographie (PPG). Détecte les variations de lumière transmise à travers les tissus pour calculer le rythme cardiaque en temps réel. Compatible Arduino et ESP32, idéal pour les projets de santé connectée et les wearables DIY.',
 4500.00, '/images/products/Frequence Cardiaque.jpeg', 1, 100, true, '2026-03-09 18:30:40');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(19,
 'Capteur MQ-135 – Qualité de l''air',
 'Capteur de gaz MQ-135 capable de détecter l''ammoniac, le benzène, le CO2, les oxydes d''azote et d''autres gaz nocifs. Fournit une sortie analogique proportionnelle à la concentration de gaz ainsi qu''une sortie numérique seuil. Idéal pour les stations de surveillance de la qualité de l''air intérieur.',
 5000.00, '/images/products/GAZ.jpeg', 1, 100, true, '2026-03-09 18:33:26');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(20,
 'Capteur ultrasonique HC-SR04',
 'Capteur ultrasonique HC-SR04 mesurant la distance avec précision grâce à la technologie des ultrasons (plage 2–400 cm, résolution 3 mm). Il émet une onde ultrasonique et mesure le temps de retour de l''écho pour déterminer la distance entre le capteur et un obstacle. Très utilisé en robotique et en domotique.',
 3500.00, '/images/products/HC-SR04.jpeg', 1, 100, true, '2026-03-09 18:37:19');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(21,
 'Capteur de température KY-028',
 'Module capteur de température KY-028 basé sur une thermistance NTC, fournissant un signal numérique (seuil réglable) et un signal analogique. Idéal pour détecter des variations de température dans des projets Arduino et systèmes d''alarme thermique.',
 2500.00, '/images/products/KY-028.jpeg', 1, 0, true, '2026-03-10 16:45:22');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(22,
 'Module RTC DS3231',
 'Module d''horloge temps réel DS3231 à haute précision (±2 ppm) avec mémoire EEPROM intégrée et interface I2C. Maintient l''heure, la date et le jour de la semaine même hors tension grâce à une pile de sauvegarde. Idéal pour les data loggers, les alarmes programmables et les systèmes embarqués.',
 4000.00, '/images/products/Module RTC DS3231.jpeg', 1, 60, true, '2026-03-10 16:40:09');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(23,
 'Module ECG AD8232',
 'Module de surveillance électrocardiographique (ECG) basé sur le circuit intégré AD8232. Mesure l''activité électrique du cœur et fournit un signal analogique amplifié et filtré, exploitable directement par l''ADC d''un Arduino ou ESP32. Livré avec électrodes et câbles de connexion.',
 13000.00, '/images/products/moniteur de frequence cardiaque.jpeg', 1, 30, true, '2026-03-10 16:39:17');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(24,
 'Capteur de niveau d''eau',
 'Module de détection du niveau d''eau fonctionnant par mesure de résistance entre les pistes conductrices de la sonde immergée. Fournit une tension analogique proportionnelle au niveau d''eau. Idéal pour les alertes d''inondation, le contrôle de réservoirs et les systèmes d''arrosage automatique.',
 2000.00, '/images/products/Niveau Eau.jpeg', 1, 100, true, '2026-03-10 16:34:22');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(25,
 'Mini PIR AM312',
 'Détecteur de mouvement infrarouge passif (PIR) miniature AM312 à faible consommation, idéal pour les espaces restreints. Angle de détection 100°, portée jusqu''à 3 m, alimentation 2,7–12V. Parfait pour les alarmes, l''automatisation d''éclairage et les systèmes de détection de présence embarqués.',
 4000.00, '/images/products/PIR AM312.jpeg', 1, 50, true, '2026-03-10 16:34:03');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(26,
 'PIR HC-SR501',
 'Capteur de mouvement infrarouge passif HC-SR501 avec seuil et temps de temporisation réglables via potentiomètres. Angle de détection 110°, portée jusqu''à 7 m. Idéal pour les systèmes d''alarme, l''allumage automatique d''éclairage et la détection de présence dans les projets domotiques.',
 4500.00, '/images/products/PIR.jpeg', 1, 60, true, '2026-03-10 16:33:16');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(27,
 'Capteur TDS – Qualité de l''eau',
 'Module capteur TDS (Total Dissolved Solids) permettant de mesurer la concentration de solides dissous dans l''eau (en ppm), indicateur direct de sa qualité et de sa conductivité. Compatible Arduino et ESP32 via sortie analogique. Idéal pour les projets d''aquariophilie, de purification d''eau et d''agriculture hydroponique.',
 5500.00, '/images/products/Qualite de l''eau.jpeg', 1, 20, true, '2026-03-10 16:32:36');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(28,
 'Capteur de distance ToF VL53L1X',
 'Capteur de distance ToF (Time of Flight) VL53L1X mesurant avec précision des distances de 4 cm à 4 m grâce à un faisceau laser infrarouge invisible. Interface I2C, temps de mesure configurable, FoV ajustable. Idéal pour la détection de présence, le comptage de personnes et la robotique.',
 5000.00, '/images/products/TOF400C.png', 1, 100, true, '2026-03-09 18:26:51');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(29,
 'Capteur de force FSR402',
 'Capteur de pression FSR402 (Force Sensitive Resistor) dont la résistance diminue proportionnellement à la force appliquée sur sa surface. Plage de détection 0,2–20 N, réponse rapide, format compact et flexible. Idéal pour la détection de contact, les interfaces tactiles et les projets de mesure de pression.',
 3000.00, '/images/products/FSR402.png', 1, 100, true, '2026-03-09 16:56:44');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(30,
 'Capteur de température DS18B20 (module)',
 'Module capteur de température numérique DS18B20 utilisant le protocole 1-Wire pour une communication fiable sur un seul fil de données. Précision ±0,5 °C sur −10 à +85 °C, résolution configurable 9–12 bits. Idéal pour la mesure de température dans les projets Arduino et ESP32.',
 1000.00, '/images/products/DS18B20.png', 1, 50, true, '2026-03-09 18:27:22');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(31,
 'Module capteur de turbidité',
 'Module capteur de turbidité permettant de mesurer la clarté de l''eau en détectant la quantité de particules en suspension grâce à la diffusion lumineuse. Fournit un signal analogique et numérique. Idéal pour le contrôle de qualité de l''eau potable, les aquariums et les systèmes de traitement des eaux.',
 6000.00, '/images/products/capteur de turbidité.jpeg', 1, 100, true, '2026-03-09 12:03:25');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(32,
 'Module MPU6050 – Accéléromètre + Gyroscope',
 'Module MPU6050 combinant un accéléromètre 3 axes et un gyroscope 3 axes dans un seul circuit intégré avec interface I2C. Permet de mesurer l''orientation, l''inclinaison, la rotation et les mouvements d''un objet avec une grande précision. Idéal pour les drones, la robotique et les systèmes de stabilisation.',
 4000.00, '/images/products/MPU6050.jpeg', 1, 100, true, '2026-03-09 18:25:29');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(34,
 'Capteur de courant ZMCT103C – 5A AC',
 'Transformateur de courant monophasé ZMCT103C pour la mesure précise du courant alternatif jusqu''à 5A AC. Sortie analogique proportionnelle au courant, isolation galvanique intégrée. Idéal pour la surveillance de consommation électrique, les compteurs d''énergie et les protections de surcharge.',
 3000.00, '/images/products/ZMCT103C.jpg', 1, 10, true, '2026-03-09 12:03:25');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(35,
 'Capteur ultrasonique étanche AJ-SR04M',
 'Module ultrasonique étanche AJ-SR04M conçu pour la mesure précise de distance (25–450 cm) en environnement extérieur ou humide. La sonde imperméable peut être immergée dans l''eau. Idéal pour la mesure de niveau de cuves, les capteurs de proximité outdoor et les robots tout terrain.',
 5000.00, '/images/products/AJ-SR04M.jpeg', 1, 50, true, '2026-03-10 16:22:19');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(36,
 'Capteur de pression atmosphérique BMP280 / BME280',
 'Module capteur environnemental haute précision BMP280/BME280 mesurant la pression atmosphérique (300–1100 hPa), la température et (pour le BME280) l''humidité relative. Interface I2C/SPI, faible consommation. Idéal pour les stations météo, les altimètres et les systèmes de navigation.',
 4000.00, '/images/products/BMP280.png', 1, 50, true, '2026-03-09 13:27:23');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(37,
 'Module capteur température & humidité AHT10',
 'Module capteur numérique AHT10 à haute précision pour la mesure de la température (−40 à +85 °C, ±0,3 °C) et de l''humidité relative (0–100 % HR, ±2 %) via interface I2C. Compact, faible consommation, idéal pour les stations météo, la domotique et les systèmes IoT.',
 3500.00, '/images/products/AHT10.png', 1, 40, true, '2026-03-09 17:32:52');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(39,
 'Module RFID RC522',
 'Lecteur/écrivain RFID RC522 fonctionnant à 13,56 MHz, compatible avec les cartes et tags Mifare. Interface SPI, portée de lecture jusqu''à 5 cm. Livré avec une carte et un badge RFID. Idéal pour les systèmes de contrôle d''accès, de présence et d''identification sans contact.',
 4000.00, '/images/products/RFID.jpeg', 1, 60, true, '2026-03-09 17:58:48');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(40,
 'Capteur de poids 20 kg (cellule de charge + HX711)',
 'Ensemble composé d''une cellule de charge 20 kg et du module convertisseur HX711 24 bits, permettant de mesurer précisément le poids dans vos projets électroniques. Interface simple compatible Arduino et ESP32 via la bibliothèque HX711. Idéal pour les balances électroniques DIY et systèmes de pesée.',
 10000.00, '/images/products/Capteur de poid.jpeg', 1, 25, true, '2026-03-09 15:46:54');

-- ── CÂBLES ET CONNECTIQUE (CategoryId = 2) ─────────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(41,
 'Rouleau 100m câble électrique 3×2,5 mm²',
 'Câble électrique rigide en cuivre 3×2,5 mm² (phase, neutre, terre) livré en rouleau de 100 m. Conforme aux normes NFC 32-321, idéal pour les circuits d''éclairage et prises de courant résidentiels et industriels supportant jusqu''à 16A.',
 80000.00, '/images/products/cable-2-5.jpg', 2, 100, true, '2026-03-09 18:25:56');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(42,
 'Rouleau 100m câble électrique 3×1,5 mm² souple',
 'Câble électrique souple en cuivre 3×1,5 mm² (phase, neutre, terre) livré en rouleau de 100 m. Conforme aux normes NFC 32-321, idéal pour les circuits d''éclairage résidentiels et les branchements d''appareils supportant jusqu''à 10A.',
 55000.00, '/images/products/cable-1-5.jpg', 2, 100, true, '2026-03-09 18:25:29');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(43,
 'Rouleau 100m câble électrique 3×6 mm²',
 'Câble électrique rigide en cuivre 3×6 mm² (phase, neutre, terre) livré en rouleau de 100 m. Haute capacité de transport de courant (jusqu''à 32A), idéal pour les circuits de forte puissance tels que les chauffe-eau, climatiseurs et équipements industriels.',
 200000.00, '/images/products/cable-6.jpeg', 2, 100, true, '2026-03-09 18:26:51');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(44,
 'Support de batterie 4×AA',
 'Support de piles 4×AA en série (6V) avec fils de connexion et interrupteur intégré. Boîtier robuste en plastique avec couvercle de protection. Idéal pour alimenter les projets Arduino, ESP32 et montages électroniques nécessitant une alimentation portable.',
 1500.00, '/images/products/Support Batteries 4x AA.jpeg', 2, 50, true, '2026-03-09 13:27:23');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(45,
 'Bornier à vis KF301',
 'Bornier à vis KF301 (pas 5,08 mm) permettant le raccordement rapide et sécurisé de fils sur un circuit imprimé. Disponible en 2, 3 ou 5 broches. Idéal pour les connexions d''alimentation, les capteurs et les actionneurs dans les montages électroniques et industriels.',
 250.00, '/images/products/Bornier.png', 2, 80, true, '2026-03-09 15:23:06');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(46,
 'Supports de circuits intégrés DIP',
 'Assortiment de supports DIP (Dual In-line Package) permettant d''insérer les circuits intégrés sans les souder directement sur le PCB. Protègent les composants contre la chaleur lors du soudage et facilitent leur remplacement. Disponibles en plusieurs tailles (8, 14, 16, 18, 20, 28, 40 broches).',
 250.00, '/images/products/Support-Circuit-Intégré.jpeg', 2, 100, true, '2026-03-09 18:25:56');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(47,
 'Fil d''étain à souder',
 'Fil de soudure étain-plomb (Sn63/Pb37) de haute qualité avec flux intégré, idéal pour les travaux d''électronique précis. Diamètre 0,8 mm, bobine 50g. Assure des joints propres, brillants et mécaniquement solides sur tous types de composants traversants et CMS.',
 3000.00, '/images/products/etain.jpeg', 2, 30, true, '2026-03-10 16:22:09');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(48,
 'Pâte à souder SN42Bi58',
 'Pâte à souder alliage étain-bismuth SN42Bi58 (42% Sn, 58% Bi) à point de fusion bas (~138°C), idéale pour la soudure de composants sensibles à la chaleur. Conditionnement en seringue de 10g pour une application précise. Convient aux réparations de cartes et aux assemblages CMS délicats.',
 4500.00, '/images/products/Pate a souder.jpeg', 2, 40, true, '2026-03-09 15:19:45');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(49,
 'Connecteur d''alimentation DC 5,5×2,1 mm',
 'Prise d''alimentation DC-005 SMD standard 5,5×2,1 mm pour montage en surface sur circuit imprimé (PCB). Compatible avec les adaptateurs secteur et câbles d''alimentation DC standard. Idéale pour les projets nécessitant une connexion d''alimentation externe robuste et démontable.',
 500.00, '/images/products/Connecteur alimentation DC.jpeg', 2, 60, true, '2026-03-09 15:20:35');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(50,
 'Boîte de 530 tubes thermorétractables assortis',
 'Assortiment de 530 tubes thermorétractables en polyoléfine (ratio 2:1) en diverses tailles (Ø 1 à 10 mm) et couleurs. Idéals pour l''isolation électrique, la protection des soudures, l''identification des câbles et la finition professionnelle des câblages.',
 5000.00, '/images/products/thermorétractables.jpg', 2, 50, true, '2026-03-10 16:23:44');

-- ── ÉQUIPEMENTS RÉSEAU (CategoryId = 3) ────────────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(51,
 'Rouleau 100m câble Ethernet RJ45 CAT6',
 'Câble réseau Ethernet CAT6 blindé (FTP) livré en rouleau de 100 m, supportant des débits jusqu''à 10 Gbps pour des longueurs inférieures à 55 m. Idéal pour les installations réseau résidentielles, bureautiques et industrielles nécessitant une haute performance et une immunité aux interférences.',
 45000.00, '/images/products/ethernet-cable.jpeg', 3, 100, true, '2026-03-09 18:27:46');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(52,
 'Switch réseau 8 ports',
 'Switch Ethernet non-manageable 8 ports 10/100 Mbps avec auto-négociation et plug-and-play. Idéal pour étendre le réseau local d''une maison, d''un bureau ou d''une salle serveur en ajoutant 8 ports RJ45 supplémentaires sans configuration.',
 25000.00, '/images/products/network-switch.jpg', 3, 0, true, '2026-03-09 16:56:44');

-- ── ÉNERGIES RENOUVELABLES (CategoryId = 4) ────────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(53,
 'Panneau solaire 380W',
 'Panneau solaire monocristallin haute performance 380W avec rendement supérieur à 20 %. Cadre en aluminium anodisé résistant à la corrosion, verre trempé anti-reflet, connecteurs MC4. Idéal pour les installations résidentielles, commerciales et les systèmes de pompage solaire.',
 45000.00, '/images/products/solar-panel.jpg', 4, 50, true, '2026-03-09 18:27:22');

-- ── CARTES DE DÉVELOPPEMENT (CategoryId = 5) ───────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(54,
 'Arduino Uno R3',
 'Carte de développement Arduino Uno R3 basée sur le microcontrôleur ATmega328P. Dispose de 14 broches E/S numériques (dont 6 PWM), 6 entrées analogiques, interface USB-B et connecteur d''alimentation. Référence mondiale pour l''apprentissage de l''électronique et du prototypage.',
 8000.00, '/images/products/arduino-uno.jpeg', 5, 35, true, '2026-03-09 15:29:30');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(55,
 'ESP32',
 'Carte de développement ESP32 intégrant un processeur dual-core Xtensa LX6 240 MHz avec WiFi 802.11 b/g/n et Bluetooth 4.2/BLE. Dispose de 30 broches GPIO, ADC, DAC, I2C, SPI et UART. Solution idéale pour les projets IoT, la domotique et les objets connectés à faible consommation d''énergie.',
 6000.00, '/images/products/esp32.jpg', 5, 45, true, '2026-02-23 13:45:28');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(56,
 'Raspberry Pi 5',
 'Mini-ordinateur Raspberry Pi 5 équipé d''un processeur ARM Cortex-A76 quad-core 2,4 GHz, jusqu''à 8 Go de RAM LPDDR4X, double micro-HDMI 4K, USB 3.0, Gigabit Ethernet et PCIe 2.0. Idéal pour les projets d''IA embarquée, de vision par ordinateur, de serveurs et d''émulation.',
 105000.00, '/images/products/Raspberry pie 5.jpeg', 5, 25, true, '2026-03-09 15:46:54');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(57,
 'LILYGO TTGO – WiFi + Bluetooth + OLED 0,96"',
 'Carte de développement IoT compacte LILYGO TTGO intégrant un ESP32, une communication longue portée LoRa 868 MHz, un écran OLED 0,96" pour l''affichage local des données, WiFi et Bluetooth. Idéale pour les réseaux de capteurs LoRaWAN, les nœuds IoT autonomes et la télémétrie longue distance.',
 30000.00, '/images/products/LILYGO TTGO LoRa32.jpeg', 5, 30, true, '2026-03-09 16:56:54');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(58,
 'Arduino Mega 2560',
 'Carte Arduino Mega 2560 basée sur le microcontrôleur ATmega2560. Dispose de 54 broches E/S numériques (dont 15 PWM), 16 entrées analogiques, 4 ports UART et 256 Ko de mémoire Flash. Idéale pour les projets complexes nécessitant de nombreuses E/S : imprimantes 3D, CNC, systèmes de contrôle avancés.',
 13000.00, '/images/products/Arduino Mega.jpeg', 5, 50, true, '2026-03-09 17:31:08');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(59,
 'Arduino Nano',
 'Carte Arduino Nano compacte (18×45 mm) basée sur le microcontrôleur ATmega328P, compatible breadboard. Dispose de 22 broches E/S (14 numériques dont 6 PWM + 8 analogiques), interface Mini-USB. Idéale pour les projets embarqués nécessitant un faible encombrement.',
 4500.00, '/images/products/Arduino Nano.jpeg', 5, 40, true, '2026-03-09 17:32:52');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(60,
 'ESP32-CAM',
 'Module ESP32-CAM intégrant un microcontrôleur ESP32-S avec WiFi et Bluetooth, une caméra OV2640 2MP et un emplacement pour carte micro-SD. Résolution configurable jusqu''au UXGA (1600×1200). Idéal pour les projets de surveillance vidéo, de vision par ordinateur et de streaming en temps réel.',
 15000.00, '/images/products/ESP32 CAM.jpeg', 5, 50, true, '2026-03-09 18:18:02');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(61,
 'ESP8266 D1 Wemos Mini',
 'Carte de développement WiFi compacte basée sur le module ESP8266, au format Wemos D1 Mini. Compatible avec l''IDE Arduino, dispose de 11 broches GPIO numériques, 1 entrée analogique et une interface USB-C pour la programmation. Idéale pour les projets IoT à faible coût.',
 8000.00, '/images/products/ESP8266 D1 Wemos.jpeg', 5, 50, true, '2026-03-09 18:19:31');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(62,
 'Module ESP8266 GPIO',
 'Module d''extension GPIO pour ESP8266 (ESP-07/ESP-12) facilitant l''accès à toutes les broches avec connecteurs pin headers. Intègre un régulateur 3,3V, des boutons reset/flash et des LEDs indicatrices. Simplifie le prototypage de projets WiFi IoT sur breadboard.',
 10000.00, '/images/products/Module ESP8255 GPIO.jpeg', 5, 70, true, '2026-03-10 16:43:40');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(63,
 'Raspberry Pi Pico',
 'Microcontrôleur Raspberry Pi Pico basé sur la puce RP2040 dual-core ARM Cortex-M0+ 133 MHz avec 264 Ko de RAM SRAM et 2 Mo de Flash. Dispose de 26 broches GPIO multifonctions, ADC 12 bits, PIO programmable et interface USB 1.1. Idéal pour l''apprentissage du MicroPython et des applications embarquées temps réel.',
 8000.00, '/images/products/Raspberry Pi Pico.jpeg', 5, 40, true, '2026-03-10 16:32:03');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(64,
 'Arduino Freenove V5 – UNO R4 WiFi + ESP32-S3 + Matrice LED',
 'Carte de développement avancée Freenove Control Board V5 basée sur Arduino UNO R4 WiFi avec processeur ARM Cortex-M4, module ESP32-S3 pour la connectivité sans fil et une matrice LED 12×8 intégrée. Idéale pour les projets IoT avancés et l''apprentissage de la programmation embarquée.',
 20000.00, '/images/products/Freenove V5.png', 5, 35, true, '2026-03-09 15:29:30');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(65,
 'Carte de développement ESP32-S3-WROOM',
 'Microcontrôleur ESP32-S3-WROOM-N16R8 avec processeur dual-core Xtensa LX7 240 MHz, 16 Mo de Flash, 8 Mo de PSRAM, WiFi et Bluetooth LE 5.0. Dispose de 45 broches GPIO programmables, interface USB natif, accélérateur pour le traitement IA/ML. Idéal pour les applications IoT embarquées exigeantes.',
 15000.00, '/images/products/ESP32-S3.png', 5, 80, true, '2026-03-09 15:23:06');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(67,
 'Arduino Nano V3 (ATmega328PB)',
 'Carte Arduino Nano V3 compacte basée sur le microcontrôleur ATmega328PB amélioré, offrant des performances supérieures à l''ATmega328P standard avec deux interfaces I2C et deux SPI. Compatible breadboard, interface USB-C, idéale pour les projets embarqués et le prototypage compact.',
 5000.00, '/images/products/NANO.jpg', 5, 100, true, '2026-03-09 17:25:11');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(68,
 'Carte STM32 NUCLEO-F401RE',
 'Carte de développement STM32 NUCLEO-F401RE basée sur le microcontrôleur ARM Cortex-M4 STM32F401RET6 (84 MHz, 512 Ko Flash, 96 Ko RAM). Compatible avec les shields Arduino et les connecteurs Morpho. Intègre un débogueur/programmeur ST-Link V2 pour le développement professionnel d''applications embarquées.',
 17000.00, '/images/products/STM32.png', 5, 50, true, '2026-03-09 17:56:48');

-- ── MODULES DE COMMUNICATION (CategoryId = 6) ──────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(69,
 'ESP-12S A9G – Module GSM/GPRS + GPS',
 'Module compact combinant la connectivité GSM/GPRS (appels, SMS, données) et la localisation GPS sur une seule carte. Basé sur la puce A9G, il permet de créer des objets connectés avec géolocalisation en temps réel. Compatible avec les cartes Arduino et ESP32 via liaison série UART.',
 8000.00, '/images/products/ESP 12S, GSM GPRS+GPS.jpeg', 6, 60, true, '2026-03-09 18:15:28');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(70,
 'Module Bluetooth HC-05',
 'Module Bluetooth HC-05 série permettant d''ajouter une connectivité Bluetooth 2.0/2.1 à tout microcontrôleur via UART. Configurable en mode maître ou esclave, portée jusqu''à 10 m. Idéal pour le contrôle à distance de robots, le transfert de données sans fil et les interfaces avec smartphones.',
 5000.00, '/images/products/HC-05.jpeg', 6, 70, true, '2026-03-09 18:35:31');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(71,
 'Module UART Zigbee',
 'Module de communication Zigbee avec interface UART permettant d''intégrer facilement un réseau maillé Zigbee (IEEE 802.15.4) à tout microcontrôleur. Portée jusqu''à 100 m en champ libre, faible consommation. Idéal pour les réseaux de capteurs IoT, la domotique et l''industrie 4.0.',
 8000.00, '/images/products/Module UART Zigbee.jpeg', 6, 30, true, '2026-03-10 16:22:49');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(72,
 'Module ESP8266',
 'Module WiFi ESP8266 (ESP-01) permettant d''ajouter une connectivité Internet à tout microcontrôleur via interface AT over UART. Supporte les protocoles TCP/IP, HTTP et MQTT. Solution économique pour connecter des projets Arduino ou PIC au réseau WiFi domestique ou professionnel.',
 10000.00, '/images/products/Module ESP8266.jpeg', 6, 50, true, '2026-03-10 16:23:11');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(73,
 'Module LoRa SX1278',
 'Module de communication LoRa longue portée basé sur le circuit SX1278, opérant sur la bande 433/868 MHz. Portée jusqu''à 10 km en espace libre, débit configurable, faible consommation. Idéal pour les réseaux de capteurs IoT ruraux, la télémétrie et les applications LoRaWAN.',
 6000.00, '/images/products/Module LORA.jpeg', 6, 50, true, '2026-03-10 16:51:35');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(74,
 'Module LoRa V1',
 'Module LoRa V1 compact à faible coût pour la communication longue portée sur bande ISM. Fournit une interface SPI simple pour l''intégration avec les microcontrôleurs Arduino et ESP32. Idéal pour les premiers projets LoRa et les déploiements de nœuds IoT à portée étendue.',
 3500.00, '/images/products/Lora 3.5k.jpeg', 6, 50, true, '2026-03-10 16:51:46');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(75,
 'Module LoRa V2',
 'Module LoRa V2 version améliorée offrant une sensibilité accrue, une portée supérieure et une meilleure tolérance aux interférences par rapport au V1. Interface SPI, antenne externe pour optimiser la portée. Idéal pour les déploiements LoRaWAN professionnels et les réseaux de capteurs étendus.',
 5000.00, '/images/products/Lora 5k.jpeg', 6, 50, true, '2026-03-10 16:51:52');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(76,
 'Module GPS',
 'Module GPS compact avec antenne céramique intégrée, fournissant des données de position NMEA (latitude, longitude, altitude, vitesse) via interface UART. Précision de 2,5 m CEP, temps de premier fix (TTFF) rapide. Compatible Arduino et Raspberry Pi pour les projets de géolocalisation et de suivi de véhicules.',
 5000.00, '/images/products/Module GPS .jpeg', 6, 50, true, '2026-03-10 16:41:12');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(77,
 'Module convertisseur RS232 vers TTL (MAX3232)',
 'Module convertisseur bidirectionnel RS232 vers TTL basé sur le circuit MAX3232. Permet de connecter des équipements industriels RS232 (PC, PLC, appareils de mesure) à des microcontrôleurs TTL (Arduino, ESP32). Alimentation 3,3V–5V, débit jusqu''à 115200 bps.',
 2000.00, '/images/products/RS232 vers TTL.jpeg', 6, 45, true, '2026-02-23 13:45:28');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(78,
 'FT232RL – Convertisseur USB/TTL',
 'Module adaptateur USB vers TTL basé sur la puce FT232RL, permettant de programmer des microcontrôleurs et de communiquer en liaison série depuis un ordinateur. Supporte les niveaux logiques 3,3V et 5V. Compatible Windows, Linux et macOS, reconnu sans pilote supplémentaire sur la plupart des systèmes.',
 4000.00, '/images/products/USB to TTL.jpeg', 6, 30, true, '2026-03-10 10:59:59');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(79,
 'USB Host Shield',
 'Shield USB Host pour Arduino permettant à la carte de jouer le rôle d''hôte USB et de communiquer avec des périphériques USB (claviers, souris, manettes de jeu, dongles Bluetooth, etc.). Basé sur le contrôleur MAX3421E, compatible Arduino UNO et Mega.',
 8000.00, '/images/products/USB Host Shield.jpeg', 6, 50, true, '2026-03-10 10:56:37');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(80,
 'Carte d''extension ESP32',
 'Carte d''extension pour module ESP32 facilitant la connexion des broches via borniers à vis ou connecteurs pin headers. Intègre un régulateur de tension et des indicateurs LED. Simplifie le câblage des projets IoT en éliminant la nécessité de soudure directe sur le module.',
 2500.00, '/images/products/Extension ESP 32.jpeg', 15, 0, true, '2026-03-09 18:28:16');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(81,
 'Module d''isolation optocoupleur PC817',
 'Carte d''isolation optocoupleur PC817 permettant une séparation galvanique entre circuits de commande et charges. Disponible en 2, 4 ou 8 canaux. Protège les microcontrôleurs des surtensions et parasites. Idéal pour piloter des relais, des thyristors et des actionneurs industriels depuis un Arduino ou Raspberry Pi.',
 2000.00, '/images/products/PC817.jpeg', 6, 100, true, '2026-03-17 18:54:17');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(82,
 'Carte d''extension Nano IO Shield',
 'Carte d''extension pour Arduino Nano V3.0 permettant de connecter facilement toutes les entrées/sorties via des borniers à vis codés par couleur. Intègre des connecteurs pour servos, I2C, SPI et UART. Élimine le câblage sur breadboard pour des projets plus propres et robustes.',
 3500.00, '/images/products/Nano IO Shield.jpeg', 15, 40, true, '2026-03-09 18:22:55');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(83,
 'Kit module RF sans fil 315/433 MHz',
 'Kit émetteur/récepteur RF 315 MHz ou 433 MHz pour la transmission de données sans fil jusqu''à 100 m en espace libre. Inclut 1 émetteur et 1 récepteur, compatible Arduino, Raspberry Pi et systèmes embarqués. Idéal pour les télécommandes DIY, les alarmes sans fil et les systèmes de contrôle à distance.',
 6000.00, '/images/products/Kit module RF.png', 6, 0, true, '2026-02-23 13:45:28');

-- ── KITS (CategoryId = 7) ───────────────────────────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(84,
 'Kit maison intelligente',
 'Kit complet de domotique éducative comprenant les modules nécessaires pour construire une maquette de maison connectée : capteurs de température, d''humidité, de luminosité, relais, écran et communication WiFi. Idéal pour apprendre les bases de la domotique et de l''IoT avec Arduino ou ESP32.',
 40000.00, '/images/products/Kit Maison intelligente.jpeg', 7, 20, true, '2026-03-10 16:21:56');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(85,
 'Kit ESP32 Basic (95 pièces)',
 'Kit de démarrage RMWIN 95 pièces pour ESP32 (ESP-32S) conçu pour les débutants et passionnés d''électronique souhaitant explorer le développement WiFi et IoT avec Arduino. Inclut l''ESP32, une breadboard, des résistances, LEDs, capteurs, modules et des câbles de connexion.',
 12000.00, '/images/products/KIT ESP 32 Basic.jpeg', 7, 30, true, '2026-03-10 16:22:09');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(86,
 'Kit d''initiation développeur Arduino UNO R3 (version améliorée)',
 'Kit d''apprentissage complet pour débutants souhaitant découvrir la programmation et l''électronique embarquée. Compatible Arduino UNO R3, contient les composants essentiels pour réaliser de nombreux projets pratiques : LEDs, capteurs (température, ultrasons, PIR), moteurs, afficheurs LCD, boutons et bien plus.',
 25000.00, '/images/products/KIT UNO R3.jpeg', 7, 50, true, '2026-03-10 16:23:44');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(87,
 'Arduino Uno R3 Starter Kit V2',
 'Kit Arduino avancé V2 incluant la carte Arduino UNO R3 et un large ensemble de composants électroniques pour réaliser des projets plus complexes : afficheur 7 segments, matrice LED, servomoteur, module Bluetooth, capteurs variés et guide de projets. Idéal pour progresser au-delà des bases.',
 35000.00, '/images/products/Super Kit UNO R3.jpeg', 7, 50, true, '2026-03-10 16:22:19');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(88,
 'Kit ESP32 CAM complet',
 'Kit complet pour projets de surveillance vidéo basé sur l''ESP32-CAM. Inclut le module ESP32-CAM OV2640, la carte de programmation USB, un support de caméra, des câbles et une documentation de démarrage. Idéal pour réaliser une caméra de surveillance WiFi, un scanner de visages ou un système de streaming vidéo.',
 40000.00, '/images/products/Super Kit ESP 32 CAM .jpeg', 7, 30, true, '2026-03-10 16:21:31');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(89,
 'Kit de démarrage Arduino UNO R3 – Basic',
 'Kit éducatif complet combinant une carte Arduino UNO R3, un moteur pas à pas pour l''apprentissage du contrôle de mouvement et un module RFID pour la gestion d''identification par carte ou badge. Inclut câbles, résistances et guide de projets pas à pas. Idéal pour les formations techniques.',
 22000.00, '/images/products/Kit 22k.jpeg', 7, 50, true, '2026-03-10 16:47:51');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(90,
 'Kit LED 500 pcs (assorties)',
 'Assortiment de 500 LEDs traversantes en 5 couleurs (rouge, vert, bleu, jaune, blanc), Ø 3 mm et 5 mm, stockées dans une boîte compartimentée. Luminosité standard, tension de travail 1,8–3,6V selon couleur. Indispensable pour tout kit d''électronique et projets Arduino.',
 6000.00, '/images/products/Kit de Led .jpeg', 7, 100, true, '2026-03-10 16:47:10');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(91,
 'Kit robot 2 roues',
 'Kit de construction de robot mobile 2 roues avec châssis acrylique, 2 moteurs DC TT, encodeurs, roue folle et visserie. Idéal pour les projets de robotique éducative, de suivi de ligne et d''évitement d''obstacles avec Arduino ou ESP32. Livré en kit à assembler avec notice.',
 25000.00, '/images/products/Kit robot 2 roue.jpeg', 7, 30, true, '2026-03-10 16:46:03');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(92,
 'Coffret complet d''initiation électronique',
 'Kit d''initiation complet présenté dans un coffret de rangement compartimenté, idéal pour débutants, étudiants et passionnés d''électronique. Contient une carte compatible UNO R3, breadboard, résistances, condensateurs, LEDs, capteurs, modules de communication et guide de projets complet. Tout le nécessaire pour commencer.',
 60000.00, '/images/products/Initiation Électronique.png', 7, 30, true, '2026-03-10 16:21:31');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(93,
 'Kit de 45 capteurs pour Arduino',
 'Kit de capteurs 45-en-1 pour Arduino comprenant les modules les plus utilisés : capteur de température, d''humidité, ultrasonique, infrarouge, de flamme, de son, de vibration, d''inclinaison, joystick, relais, LED RGB et bien d''autres. Présentés avec leurs broches identifiées, idéal pour apprendre et expérimenter.',
 15000.00, '/images/products/kit capteur.png', 7, 40, true, '2026-03-09 17:53:00');

-- ── OUTILS DE PROGRAMMATION (CategoryId = 8) ───────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(94,
 'PICkit 3',
 'Programmateur et débogueur en circuit PICkit 3 de Microchip Technology, compatible avec une large gamme de microcontrôleurs PIC (8, 16 et 32 bits). Interface USB, support du débogage pas à pas, de la programmation ICSP et de la lecture/écriture de mémoire. Outil indispensable pour le développement sur architecture PIC.',
 15000.00, '/images/products/Pick it 3.jpeg', 8, 10, true, '2026-03-10 16:21:46');

-- ── COMPOSANTS ÉLECTRONIQUES (CategoryId = 9) ──────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(95,
 'Transistor MOSFET IRLZ44N – Canal N',
 'Transistor MOSFET canal N IRLZ44N à faible résistance RDS(on) (22 mΩ typ.), compatible avec les niveaux logiques 3,3V et 5V. Tension drain-source max 55V, courant 47A en continu. Idéal pour piloter des charges de puissance (moteurs, LEDs, solénoïdes) depuis un microcontrôleur sans circuit de commande supplémentaire.',
 500.00, '/images/products/IRLZ44N.png', 9, 100, true, '2026-03-09 18:25:29');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(96,
 'Boîte de résistances (600 pcs)',
 'Assortiment de 600 résistances à couche carbone de précision, couvrant les valeurs les plus courantes de 10 Ω à 1 MΩ. Présentées dans une boîte compartimentée avec étiquettes, indispensable pour tout atelier d''électronique, laboratoire ou projet de prototypage.',
 8000.00, '/images/products/resistance.jpeg', 9, 0, true, '2026-03-10 16:28:59');

-- ── ACTIONNEURS (CategoryId = 10) ──────────────────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(97,
 'Module Peltier TEC1-12706',
 'Module thermoélectrique Peltier TEC1-12706 basé sur l''effet Peltier : refroidit d''un côté et chauffe de l''autre en courant continu (12V, 6A max). Utilisé pour les mini-réfrigérateurs, les refroidisseurs de composants électroniques et les régulateurs thermiques embarqués.',
 2500.00, '/images/products/peltier.png', 10, 10, true, '2026-03-10 16:21:46');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(98,
 'Micro moteur DC 716 (7×16 mm)',
 'Micro moteur DC 716 (7×16 mm) à grande vitesse (15 000–55 000 tr/min selon alimentation), conçu pour les mini drones, hélicoptères DIY et petits robots. Alimentation 3,7V–8,4V, poids inférieur à 5g. Les hélices compatibles sont vendues séparément.',
 2000.00, '/images/products/Mini moteur DC.jpeg', 10, 100, true, '2026-03-09 18:25:56');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(99,
 'Mini pompe à eau DC 12V',
 'Mini pompe centrifuge auto-amorçante DC 12V (moteur 365/385) capable de transférer de l''eau ou des liquides légers non corrosifs avec un débit de 1,2 L/min et une pression de 0,5 bar. Compacte et silencieuse, idéale pour les fontaines, l''arrosage automatique et les systèmes de refroidissement liquide.',
 5000.00, '/images/products/Mini pompe.jpeg', 10, 100, true, '2026-03-09 18:27:46');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(100,
 'Micro servomoteur SG90 9g',
 'Micro servomoteur SG90 9g compact et léger (17×32×12 mm), rotation 180°, couple 1,8 kg·cm à 4,8V. Livré avec 3 types de bras et visserie. Idéal pour les projets de robotique (bras, pinces, rotations), les modèles réduits RC et toutes les applications nécessitant un positionnement angulaire précis.',
 2000.00, '/images/products/servo.png', 10, 100, true, '2026-03-09 16:56:44');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(101,
 'Micro moteur à engrenages N20',
 'Micro moteur DC N20 avec réducteur à engrenages métalliques, offrant un couple élevé dans un format ultra-compact (12×10×26 mm). Disponible en plusieurs rapports de réduction (30 à 1000:1) pour adapter la vitesse et le couple. Idéal pour les robots miniatures, les actionneurs et les mécanismes de précision.',
 4000.00, '/images/products/engrenage.png', 10, 100, true, '2026-03-09 16:56:44');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(102,
 'Moteur pas à pas NEMA 17 (SM-42HB34F08AB)',
 'Moteur pas à pas biphasé NEMA 17 (42×42 mm) SM-42HB34F08AB avec un angle de pas de 1,8° (200 pas/tour), couple de maintien de 4 kg·cm et courant de phase 0,8A. Idéal pour les imprimantes 3D, les fraiseuses CNC et les systèmes de positionnement de précision.',
 6000.00, '/images/products/NEMA 17.jpeg', 10, 50, true, '2026-03-10 16:51:35');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(103,
 'Module chauffage par induction ZVS 1000W',
 'Module chauffage par induction ZVS 1000W fonctionnant en basse tension DC 12–48V, courant max 20A. Chauffe rapidement les métaux ferreux par induction électromagnétique. Idéal pour les projets de forge miniature, de soudure par induction, de dégazage et d''expériences de physique.',
 20000.00, '/images/products/zvs.jpeg', 10, 100, true, '2026-03-09 17:01:54');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(104,
 'Moteur pas à pas 28BYJ-48 5V + driver ULN2003',
 'Ensemble complet composé du moteur pas à pas unipolaire 28BYJ-48 5V (angle de pas 5,625°/64 réductions, couple 34,3 mN·m) et de son module driver ULN2003 avec LEDs indicatrices. Idéal pour les projets Arduino d''initiation au contrôle de mouvement : bras robotiques, systèmes d''orientation, actionneurs lents.',
 4000.00, '/images/products/Moteur pas à pas.png', 10, 40, true, '2026-03-09 17:45:26');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(105,
 'Verrou électromagnétique CJSD DC 12V',
 'Solénoïde verrou électromagnétique CJSD DC 12V pour la sécurisation de portes d''armoires, tiroirs ou coffres. Force de maintien de 2 kg, actionnement rapide (< 10 ms). Compatible avec les modules relais et les systèmes de contrôle d''accès Arduino. Livré avec fixations.',
 4000.00, '/images/products/verrou.png', 10, 30, true, '2026-03-09 17:48:18');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(106,
 'Moteur DC TT avec roue',
 'Moteur DC TT à courant continu avec réducteur et roue caoutchouc assortie (Ø 65 mm), idéal pour les robots mobiles à entraînement différentiel. Alimentation 3–6V, vitesse 90–200 tr/min selon tension. Compatible avec les drivers L298N, L293D et les modules pont en H pour Arduino.',
 3000.00, '/images/products/Moteur-DC-&-Roue.jpeg', 10, 30, true, '2026-03-09 17:50:47');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(107,
 'Moteur TT réducteur DC – fort couple',
 'Moteur DC TT avec réducteur à fort couple, idéal pour les robots éducatifs, voitures suiveuses de ligne et projets Arduino. Corps compact, axe en métal, alimentation 3–9V DC. Vitesse et couple adaptés aux châssis 2WD et 4WD standards. Compatible avec les drivers L298N et L293D.',
 1000.00, '/images/products/Moteur TT.jpeg', 10, 50, true, '2026-03-09 18:00:27');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(108,
 'Mini feux de signalisation',
 'Module de simulation de feux de signalisation tricolores (rouge, orange, vert) avec LEDs intégrées. Idéal pour les projets éducatifs d''apprentissage de la programmation séquentielle, les maquettes de robotique et les simulations de trafic sur Arduino.',
 2500.00, '/images/products/Mini Feux de Signalisation.jpeg', 10, 50, true, '2026-03-10 16:52:28');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(109,
 'Module relais 1 canal',
 'Module relais électromécanique 1 canal permettant de contrôler une charge électrique indépendante (AC ou DC jusqu''à 250V/10A) depuis un microcontrôleur. La LED indicatrice et l''optocoupleur d''isolation assurent sécurité et retour visuel. Compatible Arduino, ESP32 et Raspberry Pi.',
 2500.00, '/images/products/Module Relais.jpeg', 10, 50, true, '2026-03-10 16:43:31');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(110,
 'Module relais 2 canaux',
 'Module relais électromécanique 2 canaux permettant de contrôler deux charges électriques indépendantes (AC ou DC jusqu''à 250V/10A) depuis un microcontrôleur. Chaque canal dispose d''une LED indicatrice et d''un optocoupleur d''isolation pour plus de sécurité. Compatible Arduino, ESP32 et Raspberry Pi.',
 4000.00, '/images/products/Relais 2 canaux.jpeg', 10, 60, true, '2026-03-10 16:31:04');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(111,
 'Module relais 4 canaux',
 'Module relais électromécanique 4 canaux permettant de contrôler quatre charges électriques indépendantes (AC ou DC jusqu''à 250V/10A) depuis un microcontrôleur. Chaque canal dispose d''une LED indicatrice et d''un optocoupleur d''isolation. Compatible Arduino, ESP32 et Raspberry Pi.',
 6000.00, '/images/products/Relais 4 canaux.jpeg', 10, 50, true, '2026-03-10 16:43:20');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(112,
 'Module relais 8 canaux',
 'Module relais électromécanique 8 canaux permettant de contrôler huit charges électriques indépendantes (AC ou DC jusqu''à 250V/10A) depuis un microcontrôleur. Idéal pour l''automatisation industrielle, les systèmes domotiques et les projets nécessitant de nombreuses sorties de puissance.',
 8000.00, '/images/products/Relais 8 canaux.jpeg', 10, 60, true, '2026-03-10 16:30:45');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(113,
 'Module relais Bluetooth 2 canaux',
 'Module relais 2 canaux pilotable à distance via Bluetooth, permettant de contrôler deux charges électriques indépendantes depuis un smartphone ou un microcontrôleur. Idéal pour la domotique sans fil, les prises télécommandées et les automatisations IoT.',
 15000.00, '/images/products/Relais bluetooth 2 canaux.jpeg', 10, 30, true, '2026-03-10 16:30:36');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(114,
 'Carte pilote moteur L293D',
 'Carte shield L293D Motor Driver pour Arduino permettant de contrôler simultanément 4 moteurs DC, 2 moteurs pas à pas ou 2 servomoteurs. Intègre 4 ponts en H avec protection contre les surtensions. Compatible Arduino UNO et Mega, idéale pour la robotique mobile.',
 4500.00, '/images/products/L293D.jpeg', 10, 100, true, '2026-03-10 16:25:04');

-- ── MODULES D'ALIMENTATION (CategoryId = 11) ───────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(115,
 'Alimentation de laboratoire programmable 30V 5A (FNIRSI DPS-150)',
 'Alimentation DC programmable haute précision FNIRSI DPS-150, plage 0–30V / 0–5A, résolution 10 mV / 10 mA. Affichage numérique TFT couleur, mémoire de 10 profils, protection OVP/OCP/OTP. Idéale pour le laboratoire électronique, le prototypage et la réparation de cartes.',
 55000.00, '/images/products/Alimentation programmable.jpeg', 11, 50, true, '2026-03-10 16:23:44');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(116,
 'Module d''alimentation servo 7V–24V vers 5V 5A – 6 canaux',
 'Module d''alimentation servo DC 7V–24V vers 5V 5A à 6 canaux, conçu pour alimenter plusieurs servomoteurs simultanément de manière stable et sécurisée. Idéal pour les bras robotiques multi-axes, les hexapodes et les projets nécessitant de nombreux servos.',
 2500.00, '/images/products/alimentation Servo.jpg', 11, 10, true, '2026-03-09 18:22:14');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(117,
 'Convertisseur DC-DC abaisseur LM2596S – 24V/12V vers 5V 5A',
 'Module convertisseur DC-DC abaisseur (Buck) XY-3606 basé sur le LM2596S, fournissant une sortie 5V stable jusqu''à 5A depuis une entrée 12V ou 24V. Tension de sortie réglable 1,25–35V, protection intégrée. Idéal pour alimenter des Raspberry Pi, ESP32 et appareils 5V depuis un réseau industriel.',
 30000.00, '/images/products/LM2596S.jpg', 11, 30, true, '2026-03-09 16:56:54');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(118,
 'Convertisseur DC-DC Buck USB – 6V–20V vers 5V 3A',
 'Module convertisseur DC-DC abaisseur (Buck) avec sortie USB, convertissant 6V–20V en 5V/3A stable. Protection contre les surtensions et court-circuits. Idéal pour alimenter des smartphones, Raspberry Pi et périphériques USB depuis une batterie lithium, un panneau solaire ou une alimentation 12V.',
 2000.00, '/images/products/DC-DC.jpeg', 11, 50, true, '2026-03-10 16:23:11');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(119,
 'Module DC-DC élévateur MT3608 (Step-Up)',
 'Convertisseur DC-DC élévateur (Step-Up) MT3608 permettant d''augmenter une tension d''entrée (2–24V) vers une tension de sortie réglable jusqu''à 28V, courant max 2A. Compact et économique, idéal pour les alimentations de capteurs, LEDs haute puissance et projets nécessitant une tension supérieure à la source.',
 3000.00, '/images/products/MT3608.png', 11, 50, true, '2026-03-09 17:54:07');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(120,
 'Alimentation breadboard 3,3V/5V',
 'Module d''alimentation pour breadboard fournissant simultanément 3,3V et 5V régulés depuis une entrée USB ou DC 6,5–12V. Switch de sélection de tension par rail, LED indicatrice, compatible directement avec les breadboards standard 830 points. Indispensable pour les montages mixtes 3,3V/5V.',
 2000.00, '/images/products/alimentation breadboard.jpeg', 11, 0, true, '2026-03-09 17:01:54');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(121,
 'Convertisseur DC-DC élévateur',
 'Module convertisseur DC-DC boost (élévateur de tension) permettant d''augmenter une tension d''entrée faible vers une tension de sortie plus élevée et réglable. Idéal pour alimenter des circuits depuis une batterie lithium ou un accumulateur basse tension.',
 5000.00, '/images/products/elevateur DC-DC.jpeg', 11, 50, true, '2026-03-09 18:08:58');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(122,
 'Module d''alimentation AC-DC CA-888 STR',
 'Carte d''alimentation compacte CA-888 STR convertissant directement le courant secteur AC (220V) en tension continue DC stable. Idéale pour alimenter des circuits électroniques depuis le réseau sans transformateur externe. Intègre une protection contre les surtensions et les court-circuits.',
 2000.00, '/images/products/CA-888 STR.png', 11, 100, true, '2026-03-09 18:27:46');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(123,
 'Module de commutation automatique batterie YX850',
 'Module de commutation automatique YX850 (5V–48V) basculant instantanément vers une batterie de secours en cas de coupure d''alimentation principale, sans interruption de service. Idéal pour les systèmes embarqués critiques, les routeurs et les équipements nécessitant une alimentation ininterrompue.',
 3000.00, '/images/products/commutation.png', 11, 50, true, '2026-03-09 17:31:08');

-- ── AFFICHEURS (CategoryId = 12) ───────────────────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(124,
 'Écran LCD ESP32 Xtouch 2,4"',
 'Module intelligent ESP32-2432S028R intégrant un microcontrôleur ESP32 dual-core, un écran TFT couleur tactile 2,4" RGB 240×320 pixels, un emplacement micro-SD et un connecteur d''extension. Programmable via Arduino IDE ou MicroPython. Idéal pour les interfaces HMI IoT, les tableaux de bord et les panneaux de contrôle connectés.',
 15000.00, '/images/products/Écran LCD ESP32.jpeg', 12, 20, true, '2026-03-10 16:21:56');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(125,
 'Module écran LCD 1602A 16×2',
 'Écran LCD alphanumérique 16 caractères × 2 lignes avec rétroéclairage bleu/blanc et contraste réglable. Interface HD44780 parallèle (4 ou 8 bits) ou I2C via module adaptateur PCF8574. Idéal pour afficher des données de capteurs, des menus de navigation et des messages dans les projets Arduino et ESP32.',
 5000.00, '/images/products/LCD.jpg', 12, 40, true, '2026-03-10 16:23:57');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(126,
 'Écran TFT LCD 3,5" ILI9486',
 'Module d''affichage TFT LCD couleur 3,5" ILI9486, résolution 480×320 pixels, interface SPI. Conçu pour une utilisation directe avec Arduino UNO et Mega2560, il se connecte via shield sans câblage. Supporte l''affichage de texte, d''images et d''interfaces graphiques. Dalle tactile résistive optionnelle.',
 11000.00, '/images/products/ecran tft.jpeg', 12, 100, true, '2026-03-09 16:56:54');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(127,
 'Écran TFT LCD 2,4" ILI9341',
 'Module d''affichage TFT LCD couleur 2,4" ILI9341, résolution 320×240 pixels, interface SPI. Compatible Arduino UNO et Mega2560, il permet l''affichage de texte, d''images BMP et d''interfaces graphiques colorées. Version avec dalle tactile résistive XPT2046 disponible pour les applications interactives.',
 5000.00, '/images/products/ecran tft.jpeg', 12, 100, true, '2026-03-09 16:56:54');

-- ── INSTRUMENTS DE MESURE (CategoryId = 13) ────────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(128,
 'Voltmètre numérique',
 'Module voltmètre numérique à affichage LED 3 chiffres permettant la mesure directe de tensions continues de 4,5 à 30V. Compact et facile à intégrer dans un boîtier, idéal pour les panneaux de contrôle, les alimentations DIY et la surveillance de batteries.',
 3000.00, '/images/products/Voltmetre.jpeg', 13, 30, true, '2026-03-10 11:06:45');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(129,
 'Oscilloscope + Testeur LCR & Transistors DSO-TC4',
 'Outil multifonction 3-en-1 FNIRSI DSO-TC4 combinant un oscilloscope numérique portable, un testeur automatique de composants (résistances, condensateurs, inductances, transistors, MOSFETs, diodes) et un générateur de signal. Interface graphique couleur, alimentation par batterie rechargeable.',
 36000.00, '/images/products/Oscilloscope + Testeur.jpeg', 13, 10, true, '2026-03-09 15:33:54');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(130,
 'Testeur multifonction LCD GM328A',
 'Testeur multifonction LCD GM328A identifiant et mesurant automatiquement les composants électroniques : transistors NPN/PNP, MOSFETs, diodes, résistances, condensateurs et inductances. Affichage graphique LCD, alimentation par pile 9V. Indispensable pour le diagnostic et le tri de composants.',
 12000.00, '/images/products/GM328A.png', 13, 30, true, '2026-03-10 16:22:09');

-- ── INTERFACE UTILISATEUR (CategoryId = 14) ────────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(131,
 'Interrupteur tactile capacitif',
 'Module interrupteur tactile capacitif DC 5V–24V 3A permettant de contrôler l''allumage, l''extinction et la gradation d''une lumière par simple toucher. Sans pièces mobiles, durée de vie prolongée. Idéal pour les lampes connectées, les panneaux de commande et les projets domotiques.',
 2000.00, '/images/products/Interrupteur-capacitif.jpg', 14, 35, true, '2026-03-09 15:29:30');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(132,
 'Clavier matriciel 4×4',
 'Clavier matriciel 4 lignes × 4 colonnes (16 touches) avec interface à 8 broches, boîtier plastique compact. Tensions de travail 3,3V–5V, compatible Arduino, ESP32 et Raspberry Pi. Utilisé pour la saisie de codes, de commandes numériques et de mots de passe dans les systèmes embarqués.',
 2500.00, '/images/products/Clavier Matriciel.jpeg', 14, 50, true, '2026-03-09 18:03:44');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(133,
 'Bouton poussoir tactile 12×12×7,3 mm',
 'Bouton poussoir tactile traversant 12×12×7,3 mm, interrupteur momentané normalement ouvert (NO). Compact et robuste, idéal pour les circuits de test sur breadboard, les panneaux de commande et toutes les applications nécessitant une entrée numérique simple.',
 200.00, '/images/products/bouton V1.jpeg', 14, 105, true, '2026-03-09 15:46:54');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(134,
 'Bouton poussoir PBS-110',
 'Bouton poussoir miniature rond PBS-110, normalement ouvert (NO), montage traversant. Tête ronde métal, actionnement doux et retour précis. Utilisé pour les interfaces de commande, les réinitialisations de circuits et les projets embarqués nécessitant un interrupteur momentané fiable.',
 200.00, '/images/products/bouton.jpeg', 14, 100, true, '2026-03-09 12:03:25');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(135,
 'Clavier matriciel à membrane 4×4',
 'Clavier matriciel à membrane 16 touches (4 lignes × 4 colonnes) avec interface à 8 fils, compact et léger. La membrane souple permet une intégration dans des boîtiers fins. Idéal pour la saisie de codes PIN, la navigation dans des menus et les interfaces de commande embarquées sur Arduino et ESP32.',
 3000.00, '/images/products/Clavier.jpeg', 14, 100, true, '2026-03-09 18:26:51');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(136,
 'Interrupteur à bascule ON/OFF',
 'Interrupteur à bascule KCD1-101 ON/OFF 2 broches, 6A/250V AC. Corps en plastique robuste avec levier basculant et port de montage en panneau. Idéal pour le contrôle marche/arrêt des projets électroniques, alimentations DIY et équipements embarqués.',
 500.00, '/images/products/Interrupteur.jpeg', 14, 100, true, '2026-03-10 16:21:46');

-- ── PROTOTYPAGE (CategoryId = 15) ──────────────────────────
INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(137,
 'Double Breadboard',
 'Double plaque de prototypage sans soudure offrant de nombreux points de connexion pour la réalisation de montages électroniques temporaires. Compatible avec tous les composants traversants et modules standard. Indispensable pour le prototypage rapide de circuits.',
 5000.00, '/images/products/Breadboard.jpeg', 15, 0, true, '2026-03-17 18:54:17');

INSERT INTO "Products"
("Id","Name","Description","Price","ImageUrl","CategoryId","Stock","IsAvailable","CreatedAt") VALUES
(138,
 'Breadboard',
 'Plaque de prototypage sans soudure standard permettant la réalisation rapide de circuits électroniques temporaires. 830 points de connexion, compatible avec les composants traversants et modules Arduino, ESP32 et Raspberry Pi.',
 3000.00, '/images/products/BreadboardV1.jpg', 15, 0, true, '2026-03-17 18:54:17');

-- ============================================================
-- FIN DU SCRIPT — 15 catégories, 138 produits insérés
-- ============================================================
INSERT INTO "Users"
("Id","Username","Email","PasswordHash","Role","CreatedAt","IsActive") VALUES
(1,'admin','etechenergieplus@gmail.com','$2a$12$MGXyJJT7VPffX0tQhDMMBe5qX5bxKPBGoJeUasTjeTDMSCxE57Nvy','Admin','2026-03-17 18:54:17', true);