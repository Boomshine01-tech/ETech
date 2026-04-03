using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Data;

public static class DbInitializer
{
    public static async Task Initialize(AppDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (context.Services.Any())
            return;

        var services = new Service[]
        {
            new Service
            {
                Name = "Installation et maintenance électrique",
                Description = "Solutions sur mesure pour installations résidentielles, industrielles et commerciales",
                IconClass = "fa-bolt",
                DetailedDescription = "Notre équipe de techniciens qualifiés assure des installations électriques complètes et une maintenance préventive et corrective pour garantir la sécurité et la performance de vos systèmes électriques.",
                IsActive = true
            },
            new Service
            {
                Name = "Installation et maintenance industrielle",
                Description = "Conception et maintenance d'équipements industriels",
                IconClass = "fa-industry",
                DetailedDescription = "Nous intervenons dans la conception, l'installation et la maintenance des équipements industriels pour garantir un fonctionnement optimal et une longévité accrue.",
                IsActive = true
            },
            new Service
            {
                Name = "Installation et maintenance de réseaux",
                Description = "Solutions fiables pour réseaux informatiques et télécoms",
                IconClass = "fa-network-wired",
                DetailedDescription = "Mise en place de solutions fiables et performantes pour vos réseaux informatiques, télécoms et infrastructures de communication.",
                IsActive = true
            },
            new Service
            {
                Name = "Génie civil",
                Description = "Projets de construction et rénovation",
                IconClass = "fa-building",
                DetailedDescription = "Expertise reconnue dans le bâtiment et les travaux publics, avec respect des normes de qualité et de sécurité.",
                IsActive = true
            },
            new Service
            {
                Name = "Énergies renouvelables",
                Description = "Solutions écologiques et panneaux solaires",
                IconClass = "fa-solar-panel",
                DetailedDescription = "Installation de panneaux solaires, systèmes photovoltaïques et solutions pour réduire l'empreinte carbone et optimiser la consommation énergétique.",
                IsActive = true
            },
            new Service
            {
                Name = "Plomberie",
                Description = "Services de plomberie professionnels",
                IconClass = "fa-wrench",
                DetailedDescription = "De l'installation à la réparation, nous assurons des services de plomberie de haute qualité pour particuliers et entreprises.",
                IsActive = true
            },
            new Service
            {
                Name = "Menuiserie aluminium et métallique",
                Description = "Structures en aluminium et métal",
                IconClass = "fa-door-open",
                DetailedDescription = "Conception et réalisation de portes, fenêtres, cloisons alliant esthétique, durabilité et fonctionnalité.",
                IsActive = true
            },
            new Service
            {
                Name = "Commerce international",
                Description = "Import-export de produits et équipements",
                IconClass = "fa-globe",
                DetailedDescription = "Facilitation des échanges commerciaux grâce à notre réseau de partenaires internationaux.",
                IsActive = true
            }
        };

        context.Services.AddRange(services);
        await context.SaveChangesAsync();
        Console.WriteLine($"✅ {services.Length} services ajoutés");

        var categories = new Category[]
        {
            new Category { Name = "Capteurs électroniques", Description = "Capteurs et détecteurs pour applications électroniques" },
            new Category { Name = "Câbles et connectique", Description = "Câbles électriques et réseau de qualité professionnelle" },
            new Category { Name = "Équipements réseau", Description = "Matériel pour réseaux informatiques et télécommunications" },
            new Category { Name = "Énergies renouvelables", Description = "Panneaux solaires et équipements pour énergie verte" },
            new Category { Name = "Cartes de développement", Description = "Arduino, ESP32, Raspberry Pi pour projets électroniques" },
            new Category { Name = "Modules de communication", Description = "WiFi, GSM, LoRa..." },
            new Category { Name = "Kits", Description = "Kits électronique" },
            new Category { Name = "Outils de programmation", Description = "Programmateurs et outils" },
            new Category { Name = "Composants électroniques", Description = "Transistor, diode, ciruit intégrés..." }
        };

        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();
        Console.WriteLine($"✅ {categories.Length} catégories ajoutées");

        var products = new Product[]
        {
            new Product { Id=1, Name="Capteur d'empreinte digitale", Description="Capteur biométrique permettant l'identification par empreinte digitale", Price=7000m, ImageUrl="/images/products/Empreinte.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:33:54Z") },
            new Product { Id=2, Name="Capteur infrarouge E18-D80NK", Description="Capteur infrarouge de proximité pour détection d'obstacles", Price=2500m, ImageUrl="/images/products/INFRAROUGE.jpeg", CategoryId=1, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:19:45Z") },
            new Product { Id=3, Name="Capteur de température et humidité DHT22", Description="Capteur numérique haute précision", Price=3000m, ImageUrl="/images/products/DHT22.jpeg", CategoryId=1, Stock=60, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:20:35Z") },
            new Product { Id=4, Name="Capteur de température et humidité DHT11", Description="Capteur économique", Price=2000m, ImageUrl="/images/products/DHT11.jpeg", CategoryId=1, Stock=80, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:23:06Z") },
            new Product { Id=5, Name="Rouleau 100m Câble électrique 3x2.5 mm²", Description="Câble cuivre 2.5 mm²", Price=80000m, ImageUrl="/images/products/cable-2-5.jpg", CategoryId=2, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:25:56Z") },
            new Product { Id=6, Name="Rouleau 100m Câble électrique 3x1.5 mm² souple", Description="Câble cuivre 1.5 mm²", Price=55000m, ImageUrl="/images/products/cable-1-5.jpg", CategoryId=2, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:25:29Z") },
            new Product { Id=7, Name="Rouleau 100m Câble électrique 3x6 mm²", Description="Câble forte puissance", Price=200000m, ImageUrl="/images/products/cable-6.jpeg", CategoryId=2, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:26:51Z") },
            new Product { Id=8, Name="Rouleau 100m Câble Ethernet RJ45 CAT6", Description="Câble réseau Ethernet", Price=45000m, ImageUrl="/images/products/ethernet-cable.jpeg", CategoryId=3, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:27:46Z") },
            new Product { Id=9, Name="Switch réseau 8 ports", Description="Switch Ethernet", Price=25000m, ImageUrl="/images/products/network-switch.jpg", CategoryId=3, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T16:56:44Z") },
            new Product { Id=10, Name="Panneau solaire 380W", Description="Panneau solaire haute performance", Price=45000m, ImageUrl="/images/products/solar-panel.jpg", CategoryId=4, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:27:22Z") },
            new Product { Id=11, Name="Arduino Uno R3", Description="Carte Arduino", Price=8000m, ImageUrl="/images/products/arduino-uno.jpeg", CategoryId=5, Stock=35, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:29:30Z") },
            new Product { Id=12, Name="ESP32", Description="Carte ESP32 Wi-Fi Bluetooth", Price=6000m, ImageUrl="/images/products/esp32.jpg", CategoryId=5, Stock=45, IsAvailable=true, CreatedAt=DateTime.Parse("2026-02-23T13:45:28Z") },
            new Product { Id=13, Name="Raspberry Pi 5", Description="Mini ordinateur", Price=105000m, ImageUrl="/images/products/Raspberry pie 5.jpeg", CategoryId=5, Stock=25, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:46:54Z") },
            new Product { Id=14, Name="Capteur de Couleur TCS3200", Description="Capteur couleur", Price=3500m, ImageUrl="/images/products/Couleur TCS3200.jpeg", CategoryId=1, Stock=10, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:22:14Z") },
            new Product { Id=15, Name="Capteur DSM501A", Description="Capteur de poussière", Price=5000m, ImageUrl="/images/products/DSM501A.jpeg", CategoryId=1, Stock=10, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T12:03:25Z") },
            new Product { Id=16, Name="LILYGO TTGO LoRa32", Description="ESP32 avec LoRa", Price=15000m, ImageUrl="/images/products/LILYGO TTGO LoRa32.jpeg", CategoryId=5, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T16:56:54Z") },
            new Product { Id=17, Name="Pick it 3", Description="Programmateur PIC", Price=15000m, ImageUrl="/images/products/Pick it 3.jpeg", CategoryId=8, Stock=10, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:21:46Z") },
            new Product { Id=18, Name="KIT Maison intelligente", Description="Kit maison connectée", Price=40000m, ImageUrl="/images/products/Kit Maison intelligente.jpeg", CategoryId=7, Stock=20, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:21:56Z") },
            new Product { Id=19, Name="Kit ESP32 Basic", Description="Kit IoT ESP32", Price=12000m, ImageUrl="/images/products/KIT ESP 32 Basic.jpeg", CategoryId=7, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:22:09Z") },
            new Product { Id=20, Name="Starter Kit UNO R3", Description="Kit Arduino débutant", Price=25000m, ImageUrl="/images/products/KIT UNO R3.jpeg", CategoryId=7, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:23:44Z") },

            new Product { Id=21, Name="Arduino Uno R3 Starter Kit V2", Description="Kit Arduino avancé", Price=35000m, ImageUrl="/images/products/Super Kit UNO R3.jpeg", CategoryId=7, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:22:19Z") },
            new Product { Id=22, Name="Kit ESP32 CAM", Description="ESP32 avec caméra", Price=40000m, ImageUrl="/images/products/Super Kit ESP 32 CAM .jpeg", CategoryId=7, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:21:31Z") },
            new Product { Id=23, Name="Double Breadboard", Description="Plaque de prototypage", Price=3000m, ImageUrl="/images/products/Breadboard.jpeg", CategoryId=2, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-17T18:54:17Z") },
            new Product { Id=24, Name="Support Batterie 4xAA", Description="Support piles", Price=1500m, ImageUrl="/images/products/Support Batteries 4x AA.jpeg", CategoryId=2, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T13:27:23Z") },
            new Product { Id=25, Name="Capteur de couleur TCS3472", Description="Capteur RGB I2C", Price=3500m, ImageUrl="/images/products/Couleur TCS3472.jpeg", CategoryId=1, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:22:55Z") },

            new Product { Id=26, Name="Module UART Zigbee", Description="Communication Zigbee", Price=8000m, ImageUrl="/images/products/Module UART Zigbee.jpeg", CategoryId=6, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:22:49Z") },
            new Product { Id=27, Name="Module ESP8266", Description="Module Wi-Fi", Price=10000m, ImageUrl="/images/products/Module ESP8266.jpeg", CategoryId=6, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:23:11Z") },
            new Product { Id=28, Name="Module LORA", Description="Communication longue portée", Price=6000m, ImageUrl="/images/products/Module LORA.jpeg", CategoryId=6, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:51:35Z") },

            new Product { Id=29, Name="Kit minimal Arduino UNO R3", Description="Kit Arduino", Price=22000m, ImageUrl="/images/products/Kit 22k.jpeg", CategoryId=7, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:23:57Z") },
            new Product { Id=30, Name="Alimentation Breadboard", Description="Module alimentation", Price=2000m, ImageUrl="/images/products/alimentation breadboard.jpeg", CategoryId=2, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:01:54Z") },

            new Product { Id=31, Name="Anemometre", Description="Capteur vent", Price=5000m, ImageUrl="/images/products/Anemometre.jpeg", CategoryId=1, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:25:11Z") },
            new Product { Id=32, Name="Arduino Mega 2560", Description="Carte Arduino Mega", Price=13000m, ImageUrl="/images/products/Arduino Mega.jpeg", CategoryId=5, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:31:08Z") },
            new Product { Id=33, Name="Arduino Nano", Description="Carte compacte", Price=4500m, ImageUrl="/images/products/Arduino Nano.jpeg", CategoryId=5, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:32:52Z") },

            new Product { Id=34, Name="Module Webcam Raspberry", Description="Caméra Raspberry", Price=8000m, ImageUrl="/images/products/Camera Raspberry.jpeg", CategoryId=1, Stock=20, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:42:08Z") },
            new Product { Id=35, Name="Capteur Reed", Description="Capteur magnétique", Price=2000m, ImageUrl="/images/products/Capteur de commutateur.jpeg", CategoryId=1, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:45:26Z") },
            new Product { Id=36, Name="Capteur de Courant", Description="Mesure courant", Price=4000m, ImageUrl="/images/products/Courant.jpeg", CategoryId=1, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:48:18Z") },
            new Product { Id=37, Name="Capteur de Tension", Description="Mesure tension", Price=4000m, ImageUrl="/images/products/tension.jpeg", CategoryId=1, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:50:47Z") },
            new Product { Id=38, Name="Capteur de tension V2", Description="Version améliorée", Price=2000m, ImageUrl="/images/products/tension2.jpeg", CategoryId=1, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:53:00Z") },

            new Product { Id=39, Name="Capteur Débit d'Eau", Description="Mesure débit", Price=10000m, ImageUrl="/images/products/Debit d'Eau.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:54:07Z") },
            new Product { Id=40, Name="Capteur IR E18-D80NK", Description="Capteur obstacle", Price=2000m, ImageUrl="/images/products/IR.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:56:48Z") },

            new Product { Id=41, Name="Capteur de pluie", Description="Détection pluie", Price=5000m, ImageUrl="/images/products/PLUIE.jpeg", CategoryId=1, Stock=60, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:58:48Z") },
            new Product { Id=42, Name="Capteur vibration", Description="Détection chocs", Price=2500m, ImageUrl="/images/products/vibrations.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:00:27Z") },
            new Product { Id=43, Name="Clavier Matriciel 4x4", Description="Clavier 16 touches", Price=2500m, ImageUrl="/images/products/Clavier Matriciel.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:03:44Z") },

            new Product { Id=44, Name="DHT11 ESP-01", Description="Capteur + WiFi", Price=12000m, ImageUrl="/images/products/DHT11-ESP.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:06:43Z") },
            new Product { Id=45, Name="Convertisseur DC-DC", Description="Boost tension", Price=5000m, ImageUrl="/images/products/elevateur DC-DC.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:08:58Z") },

            new Product { Id=46, Name="Module Empreinte Digitale", Description="Reconnaissance biométrique", Price=15000m, ImageUrl="/images/products/Empreinte.jpeg", CategoryId=1, Stock=60, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:11:59Z") },
            new Product { Id=47, Name="ESP-12S A9G", Description="Module GSM GPS", Price=8000m, ImageUrl="/images/products/ESP 12S, GSM GPRS+GPS.jpeg", CategoryId=1, Stock=60, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:15:28Z") },
            new Product { Id=48, Name="ESP32 CAM", Description="Module caméra", Price=15000m, ImageUrl="/images/products/ESP32 CAM.jpeg", CategoryId=5, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:18:02Z") },
            new Product { Id=49, Name="ESP8266 D1 Wemos", Description="Carte WiFi", Price=8000m, ImageUrl="/images/products/ESP8266 D1 Wemos.jpeg", CategoryId=5, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:19:31Z") },

            new Product { Id=50, Name="Extension ESP32", Description="Carte extension", Price=2500m, ImageUrl="/images/products/Extension ESP 32.jpeg", CategoryId=2, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:28:16Z") },

            new Product { Id=51, Name="Capteur de pouls", Description="Fréquence cardiaque", Price=4500m, ImageUrl="/images/products/Frequence Cardiaque.jpeg", CategoryId=1, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:30:40Z") },
            new Product { Id=52, Name="Capteur MQ-135", Description="Qualité air", Price=5000m, ImageUrl="/images/products/GAZ.jpeg", CategoryId=1, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:33:26Z") },
            new Product { Id=53, Name="Module Bluetooth HC-05", Description="Bluetooth", Price=5000m, ImageUrl="/images/products/HC-05.jpeg", CategoryId=5, Stock=70, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:35:31Z") },
            new Product { Id=54, Name="Capteur Ultrason HCSR04", Description="Distance", Price=3500m, ImageUrl="/images/products/HC-SR04.jpeg", CategoryId=1, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:37:19Z") },

            new Product { Id=55, Name="Jumpers male-femelle", Description="Câbles connexion", Price=21m, ImageUrl="/images/products/Jumpers male-femelle.jpeg", CategoryId=2, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:50:04Z") },

            new Product { Id=56, Name="Kit Basic UNO R3", Description="Kit Arduino", Price=22000m, ImageUrl="/images/products/Kit 22k.jpeg", CategoryId=7, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:47:51Z") },
            new Product { Id=57, Name="Kit LED 500 pcs", Description="Lot LEDs", Price=6000m, ImageUrl="/images/products/Kit de Led .jpeg", CategoryId=7, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:47:10Z") },
            new Product { Id=58, Name="Kit Robot 2 roues", Description="Robot éducatif", Price=25000m, ImageUrl="/images/products/Kit robot 2 roue.jpeg", CategoryId=7, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:46:03Z") },

            new Product { Id=59, Name="Capteur KY-028", Description="Température", Price=2500m, ImageUrl="/images/products/KY-028.jpeg", CategoryId=1, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:45:22Z") },

            new Product { Id=60, Name="Module Lora V1", Description="Module LoRa", Price=3500m, ImageUrl="/images/products/Lora 3.5k.jpeg", CategoryId=6, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:51:46Z") },
            new Product { Id=61, Name="Module Lora V2", Description="LoRa amélioré", Price=5000m, ImageUrl="/images/products/Lora 5k.jpeg", CategoryId=6, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:51:52Z") },

            new Product { Id=62, Name="Mini feux", Description="Simulation feu", Price=2500m, ImageUrl="/images/products/Mini Feux de Signalisation.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:52:28Z") },

            new Product { Id=63, Name="ESP8266 GPIO", Description="Module GPIO", Price=10000m, ImageUrl="/images/products/Module ESP8255 GPIO.jpeg", CategoryId=5, Stock=70, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:43:40Z") },

            new Product { Id=64, Name="Module GPS", Description="Position", Price=5000m, ImageUrl="/images/products/Module GPS .jpeg", CategoryId=6, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:41:12Z") },

            new Product { Id=65, Name="Module Relais 1", Description="Relais 1 canal", Price=2500m, ImageUrl="/images/products/Module Relais.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:43:31Z") },

            new Product { Id=66, Name="RTC DS3231", Description="Horloge temps réel", Price=4000m, ImageUrl="/images/products/Module RTC DS3231.jpeg", CategoryId=1, Stock=60, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:40:09Z") },

            new Product { Id=67, Name="ECG AD8232", Description="Capteur cardiaque", Price=13000m, ImageUrl="/images/products/moniteur de frequence cardiaque.jpeg", CategoryId=1, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:39:17Z") },

            new Product { Id=68, Name="Capteur niveau eau", Description="Niveau eau", Price=2000m, ImageUrl="/images/products/Niveau Eau.jpeg", CategoryId=1, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:34:22Z") },

            new Product { Id=69, Name="Mini PIR AM312", Description="Détecteur mouvement", Price=4000m, ImageUrl="/images/products/PIR AM312.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:34:03Z") },

            new Product { Id=70, Name="PIR HC-SR501", Description="Capteur mouvement", Price=4500m, ImageUrl="/images/products/PIR.jpeg", CategoryId=1, Stock=60, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:33:16Z") },

            new Product { Id=71, Name="Capteur TDS", Description="Qualité eau", Price=5500m, ImageUrl="/images/products/Qualite de l'eau.jpeg", CategoryId=1, Stock=20, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:32:36Z") },

            new Product { Id=72, Name="Raspberry Pi Pico", Description="Microcontrôleur RP2040", Price=8000m, ImageUrl="/images/products/Raspberry Pi Pico.jpeg", CategoryId=5, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:32:03Z") },

            new Product { Id=73, Name="Relais 2 canaux", Description="Module relais", Price=4000m, ImageUrl="/images/products/Relais 2 canaux.jpeg", CategoryId=1, Stock=60, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:31:04Z") },

            new Product { Id=74, Name="Relais 4 canaux", Description="Module relais", Price=6000m, ImageUrl="/images/products/Relais 4 canaux.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:43:20Z") },

            new Product { Id=75, Name="Relais 8 canaux", Description="Module relais", Price=8000m, ImageUrl="/images/products/Relais 8 canaux.jpeg", CategoryId=1, Stock=60, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:30:45Z") },

            new Product { Id=76, Name="Relais Bluetooth", Description="Relais Bluetooth", Price=15000m, ImageUrl="/images/products/Relais bluetooth 2 canaux.jpeg", CategoryId=1, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:30:36Z") },

            new Product { Id=77, Name="Boite de resistance", Description="600 résistances", Price=8000m, ImageUrl="/images/products/resistance.jpeg", CategoryId=1, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:28:59Z") },

            new Product { Id=78, Name="RFID RC522", Description="Module RFID NFC", Price=5000m, ImageUrl="/images/products/RFID .jpeg", CategoryId=6, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:28:33Z") },

            new Product { Id=79, Name="Servo Moteur", Description="Moteur position", Price=3500m, ImageUrl="/images/products/Servo.jpeg", CategoryId=1, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:25:04Z") },

            new Product { Id=80, Name="TTL USB-C", Description="Convertisseur USB TTL", Price=5m, ImageUrl="/images/products/TTL Type C.jpeg", CategoryId=6, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T11:08:09Z") },

            new Product { Id=81, Name="Voltmetre", Description="Mesure tension", Price=3000m, ImageUrl="/images/products/Voltmetre.jpeg", CategoryId=1, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T11:06:45Z") },

            new Product { Id=82, Name="FT232RL", Description="USB TTL", Price=4000m, ImageUrl="/images/products/USB to TTL.jpeg", CategoryId=1, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T10:59:59Z") },

            new Product { Id=83, Name="USB Host Shield", Description="Extension Arduino", Price=8000m, ImageUrl="/images/products/USB Host Shield.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T10:56:37Z") }
            
        };


        context.Products.AddRange(products);
        await context.SaveChangesAsync();
        Console.WriteLine($"✅ {products.Length} produits ajoutés");


        Console.WriteLine("");
        Console.WriteLine("╔═══════════════════════════════════════════════╗");
        Console.WriteLine("║   ✅ Initialisation Terminée avec Succès !   ║");
        Console.WriteLine("╚═══════════════════════════════════════════════╝");
    }
}
