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
            new Category { Name = "Composants électroniques", Description = "Transistor, diode, ciruit intégrés..." },
            new Category { Name = "Actionneurs", Description = "Moteur, outil de puissance..." },
            new Category { Name = "Module d'allimentation", Description = "Module d'allimentation" }
        
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
            new Product { Id=12, Name="ESP32", Description="La carte de développement ESP32 intègre le WiFi et le Bluetooth pour des projets connectés performants et économes en énergie.", Price=6000m, ImageUrl="/images/products/esp32.jpg", CategoryId=5, Stock=45, IsAvailable=true, CreatedAt=DateTime.Parse("2026-02-23T13:45:28Z") },
            new Product { Id=13, Name="Raspberry Pi 5", Description="Mini ordinateur", Price=105000m, ImageUrl="/images/products/Raspberry pie 5.jpeg", CategoryId=5, Stock=25, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:46:54Z") },
            new Product { Id=14, Name="Capteur de Couleur TCS3200", Description="Capteur couleur", Price=3500m, ImageUrl="/images/products/Couleur TCS3200.jpeg", CategoryId=1, Stock=10, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:22:14Z") },
            new Product { Id=15, Name="Capteur DSM501A", Description="Capteur de poussière", Price=5000m, ImageUrl="/images/products/DSM501A.jpeg", CategoryId=1, Stock=10, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T12:03:25Z") },
            new Product { Id=16, Name="LILYGO TTGO – WiFi + Bluetooth + OLED 0.96", Description="La carte LILYGO 868MHz avec écran OLED 0.96” est une plateforme de développement IoT compacte combinant WiFi, Bluetooth et communication longue portée (868 MHz – type LoRa selon version). Elle intègre un écran OLED pour afficher directement les données ", Price=30000m, ImageUrl="/images/products/LILYGO TTGO LoRa32.jpeg", CategoryId=5, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T16:56:54Z") },
            new Product { Id=17, Name="Pick it 3", Description="Programmateur PIC", Price=15000m, ImageUrl="/images/products/Pick it 3.jpeg", CategoryId=8, Stock=10, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:21:46Z") },
            new Product { Id=18, Name="KIT Maison intelligente", Description="Kit maison connectée", Price=40000m, ImageUrl="/images/products/Kit Maison intelligente.jpeg", CategoryId=7, Stock=20, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:21:56Z") },
            new Product { Id=19, Name="Kit ESP32 Basic", Description="Le kit de démarrage RMWIN 95 pièces pour ESP32 (ESP-32S) est conçu pour les débutants et les passionnés d’électronique souhaitant explorer les possibilités du développement WiFi et IoT avec Arduino.", Price=12000m, ImageUrl="/images/products/KIT ESP 32 Basic.jpeg", CategoryId=7, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:22:09Z") },
            new Product { Id=20, Name="Kit d’initiation développeur Version améliorée", Description="Kit d’apprentissage complet conçu pour les débutants souhaitant découvrir la programmation et l’électronique embarquée. Compatible avec Arduino UNO R3, ce pack contient les composants essentiels pour réaliser de nombreux projets pratiques : LED, capteurs, moteurs, afficheurs, boutons et plus encore.", Price=25000m, ImageUrl="/images/products/KIT UNO R3.jpeg", CategoryId=7, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:23:44Z") },

            new Product { Id=21, Name="Arduino Uno R3 Starter Kit V2", Description="Kit Arduino avancé", Price=35000m, ImageUrl="/images/products/Super Kit UNO R3.jpeg", CategoryId=7, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:22:19Z") },
            new Product { Id=22, Name="Kit ESP32 CAM", Description="ESP32 avec caméra", Price=40000m, ImageUrl="/images/products/Super Kit ESP 32 CAM .jpeg", CategoryId=7, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:21:31Z") },
            new Product { Id=23, Name="Double Breadboard", Description="Plaque de prototypage", Price=3000m, ImageUrl="/images/products/Breadboard.jpeg", CategoryId=2, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-17T18:54:17Z") },
            new Product { Id=24, Name="Support Batterie 4xAA", Description="Support piles", Price=1500m, ImageUrl="/images/products/Support Batteries 4x AA.jpeg", CategoryId=2, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T13:27:23Z") },
            new Product { Id=25, Name="Capteur de couleur TCS3472", Description="Capteur RGB I2C", Price=3500m, ImageUrl="/images/products/Couleur TCS3472.jpeg", CategoryId=1, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:22:55Z") },

            new Product { Id=26, Name="Module UART Zigbee", Description="Communication Zigbee", Price=8000m, ImageUrl="/images/products/Module UART Zigbee.jpeg", CategoryId=6, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:22:49Z") },
            new Product { Id=27, Name="Module ESP8266", Description="Module Wi-Fi", Price=10000m, ImageUrl="/images/products/Module ESP8266.jpeg", CategoryId=6, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:23:11Z") },
            new Product { Id=28, Name="Module LORA", Description="Communication longue portée", Price=6000m, ImageUrl="/images/products/Module LORA.jpeg", CategoryId=6, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:51:35Z") },

            new Product { Id=29, Name="Kit de démarrage Arduino UNO R3", Description="Kit éducatif combinant un moteur pas à pas pour l’apprentissage du contrôle de mouvement et un module RFID pour la gestion d’identification par carte ou badge. Idéal pour les projets pédagogiques et les cartes de développement type Arduino.", Price=22000m, ImageUrl="/images/products/Kit 22k.jpeg", CategoryId=7, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:23:57Z") },
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

            new Product { Id=41, Name="Capteur de pluie", Description="Module capteur de pluie LM393 conçu pour détecter la présence d’eau ou de gouttes de pluie.", Price=5000m, ImageUrl="/images/products/PLUIE.jpeg", CategoryId=1, Stock=60, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:58:48Z") },
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
            new Product { Id=54, Name="Capteur Ultrason HCSR04", Description="Capteur ultrasonique HC-SR04 conçu pour mesurer la distance avec précision grâce à la technologie des ultrasons. Il émet une onde ultrasonique et mesure le temps de retour de l’écho pour déterminer la distance entre le capteur et un obstacle.", Price=3500m, ImageUrl="/images/products/HC-SR04.jpeg", CategoryId=1, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:37:19Z") },

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

            new Product { Id=73, Name="Relais 2 canaux", Description="Le module relais 2 canaux permet de contrôler deux charges électriques indépendantes à partir d’un microcontrôleur.", Price=4000m, ImageUrl="/images/products/Relais 2 canaux.jpeg", CategoryId=1, Stock=60, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:31:04Z") },

            new Product { Id=74, Name="Relais 4 canaux", Description="Le module relais 4 canaux permet de contrôler quatre charges électriques indépendantes à partir d’un microcontrôleur.", Price=6000m, ImageUrl="/images/products/Relais 4 canaux.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:43:20Z") },

            new Product { Id=75, Name="Relais 8 canaux", Description="Le module relais 8 canaux permet de contrôler huit charges électriques indépendantes à partir d’un microcontrôleur.", Price=8000m, ImageUrl="/images/products/Relais 8 canaux.jpeg", CategoryId=1, Stock=60, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:30:45Z") },

            new Product { Id=76, Name="Relais Bluetooth", Description="Le module relais Bluetooth 2 canaux permet de contrôler deux charges électriques indépendantes à partir d’un microcontrôleur via bluetooth.", Price=15000m, ImageUrl="/images/products/Relais bluetooth 2 canaux.jpeg", CategoryId=1, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:30:36Z") },

            new Product { Id=77, Name="Boite de resistance", Description="600 résistances", Price=8000m, ImageUrl="/images/products/resistance.jpeg", CategoryId=1, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:28:59Z") },

            new Product { Id=78, Name="RFID RC522", Description="Module RFID NFC", Price=5000m, ImageUrl="/images/products/RFID .jpeg", CategoryId=6, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:28:33Z") },

            new Product { Id=79, Name="Carte pilote moteur L293D", Description="La carte L293D Motor Driver Shield permet de contrôler simultanément 4 moteurs DC, 2 moteurs pas à pas ou 2 servomoteurs avec un microcontrôleur Arduino.", Price=4500m, ImageUrl="/images/products/L293D.jpeg", CategoryId=1, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:25:04Z") },

            new Product { Id=80, Name="TTL USB-C", Description="Convertisseur USB TTL", Price=5m, ImageUrl="/images/products/TTL Type C.jpeg", CategoryId=6, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T11:08:09Z") },

            new Product { Id=81, Name="Voltmetre", Description="Mesure tension", Price=3000m, ImageUrl="/images/products/Voltmetre.jpeg", CategoryId=1, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T11:06:45Z") },

            new Product { Id=82, Name="FT232RL", Description="USB TTL", Price=4000m, ImageUrl="/images/products/USB to TTL.jpeg", CategoryId=1, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T10:59:59Z") },

            new Product { Id=83, Name="USB Host Shield", Description="Extension Arduino", Price=8000m, ImageUrl="/images/products/USB Host Shield.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T10:56:37Z") }
           
            new Product { Id=84, Name="Bornier à vis KF301", Description="Le bornier à vis KF301 est un connecteur électrique à vis permettant de raccorder facilement des fils sur un circuit imprimé.", Price=250m, ImageUrl="/images/products/Bornier.jpeg", CategoryId=2, Stock=80, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:23:06Z") },
            new Product { Id=85, Name="Supports de circuits intégrés", Description="Les supports DIP (Dual In-line Package) sont des connecteurs permettant d’insérer facilement des circuits intégrés sans les souder directement sur le PCB. Ils protègent les composants contre la chaleur lors du soudage et facilitent leur remplacement.", Price=250m, ImageUrl="/images/products/Support-Circuit-Intégré.jpg", CategoryId=2, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:25:56Z") },
            new Product { Id=86, Name="Transistor MOSFET IRLZ44N – Canal N", Description="Le IRLZ44N est un transistor MOSFET canal N à faible résistance, idéal pour piloter des charges de puissance avec des microcontrôleurs", Price=500m, ImageUrl="/images/products/IRLZ44N.jpg", CategoryId=9, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:25:29Z") },
            new Product { Id=87, Name="Capteur de distance ToF VL53L1X", Description="Le VL53L1X (TOF400C) est un capteur de distance basé sur la technologie Time of Flight (ToF), permettant de mesurer avec précision la distance en utilisant un faisceau laser infrarouge", Price=5000m, ImageUrl="/images/products/TOF400C.jpeg", CategoryId=1, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:26:51Z") },
            new Product { Id=88, Name="Module CA-888 STR – Alimentation AC-DC compacte", Description="Le module CA-888 STR est une carte d’alimentation AC vers DC compacte permettant de convertir directement le courant secteur (AC) en une tension continue stable pour alimenter vos circuits électroniques.", Price=2000m, ImageUrl="/images/products/CA-888 STR.jpeg", CategoryId=2, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:27:46Z") },
            new Product { Id=89, Name="Capteur de force FSR402", Description="Le FSR402 (Force Sensitive Resistor) est un capteur de pression flexible qui détecte la force appliquée sur sa surface. Sa résistance diminue lorsque la pression augmente, ce qui permet de mesurer des appuis ou contacts de manière simple.", Price=3000m, ImageUrl="/images/products/FSR402.jpg", CategoryId=1, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T16:56:44Z") },
            new Product { Id=90, Name="Capteur de température DS18B20", Description="Le DS18B20 est un capteur de température numérique très précis utilisant le protocole single wire.", Price=1000m, ImageUrl="/images/products/DS18B20.jpg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:27:22Z") },
            new Product { Id=91, Name="Arduino Freenove V5 – UNO R4 WiFi + ESP32-S3 + Matrice LED", Description="La Freenove Control Board V5 est une carte de développement avancée basée sur Arduino UNO R4 WiFi, intégrant un processeur ARM Cortex-M4 et un module ESP32-S3 pour la connectivité sans fil.", Price=20000m, ImageUrl="/images/products/Freenove V5.jpeg", CategoryId=5, Stock=35, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:29:30Z") },
            new Product { Id=92, Name="Kit module RF sans fil 315 – 433 MHz", Description="Le kit RF 315 MHz / 433 MHz comprend un émetteur et un récepteur sans fil, permettant de transmettre des données à distance entre microcontrôleurs. Simple et économique, il est idéal pour les projets Arduino, Raspberry Pi et systèmes embarqués.", Price=6000m, ImageUrl="/images/products/Kit module RF.jpg", CategoryId=6, Stock=0, IsAvailable=true, CreatedAt=DateTime.Parse("2026-02-23T13:45:28Z") },
            new Product { Id=93, Name="Bouton poussoir", Description="Le bouton poussoir tactile 12×12×7.3 mm est un interrupteur momentané compact, idéal pour les projets électroniques et Arduino.", Price=200m, ImageUrl="/images/products/bouton V1.jpeg", CategoryId=1, Stock=105, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:46:54Z") },
            new Product { Id=94, Name="Module capteur de turbidité", Description="Le module capteur de turbidité permet de mesurer la clarté de l’eau en détectant la quantité de particules en suspension.", Price=6000m, ImageUrl="/images/products/capteur de turbidité.jpeg", CategoryId=1, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:22:14Z") },
            new Product { Id=95, Name="Bouton poussoir V2", Description="Le bouton poussoir PBS-110 est un interrupteur miniature rond momentané (NO – normalement ouvert).", Price=200m, ImageUrl="/images/products/bouton.jpeg", CategoryId=1, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T12:03:25Z") },
            new Product { Id=96, Name="Écran TFT LCD 3.5", Description="L’écran TFT LCD 3.5 pouces ILI9486 est un module d’affichage couleur haute résolution spécialement conçu pour une utilisation directe avec Arduino UNO et Mega2560.", Price=11000m, ImageUrl="/images/products/ecran tft.jpeg", CategoryId=6, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T16:56:54Z") },
            new Product { Id=97, Name="Module Peltier TEC1-12706", Description="Le module TEC1-12706 est un dispositif thermoélectrique basé sur l’effet Peltier, capable de refroidir d’un côté et chauffer de l’autre lorsqu’il est alimenté en courant continu.", Price=2500m, ImageUrl="/images/products/peltier.jpeg", CategoryId=1, Stock=10, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:21:46Z") },
            new Product { Id=98, Name="Écran TFT LCD 2.4", Description="L’écran TFT LCD 2.4 pouces ILI9486 est un module d’affichage couleur haute résolution spécialement conçu pour une utilisation directe avec Arduino UNO et Mega2560.", Price=5000m, ImageUrl="/images/products/ecran tft.jpeg", CategoryId=6, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T16:56:54Z") },
            new Product { Id=99, Name="Fil d’étain à souder", Description="Fil d’étain de soudure, idéal pour les travaux électroniques précis et propres", Price=3000m, ImageUrl="/images/products/etain.jpeg", CategoryId=2, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:22:09Z") },
            new Product { Id=100, Name="Alimentation de laboratoire programmable 30V 5A", Description="La FNIRSI DPS-150 est une alimentation DC programmable de haute précision, conçue pour les travaux électroniques, le prototypage et la réparation.", Price=55000m, ImageUrl="/images/products/Alimentation programmable.jpeg", CategoryId=2, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:23:44Z") },
            new Product { Id=101, Name="Oscilloscope + Testeur LCR & Transistors", Description="Le FNIRSI DSO-TC4 est un outil multifonction 3-en-1 combinant oscilloscope numérique, testeur de composants (LCR/transistors) et générateur de signal.", Price=36000m, ImageUrl="/images/products/Oscilloscope + Testeur.jpeg", CategoryId=1, Stock=10, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:33:54Z") },
            new Product { Id=102, Name="Pâte à souder SN42Bi58", Description="La pâte à souder SN42Bi58 est un alliage étain-bismuth (42% Sn, 58% Bi) conçu pour les travaux de soudure électronique, particulièrement pour les circuits sensibles à la chaleur.", Price=4500m, ImageUrl="/images/products/Pate a souder.jpeg", CategoryId=2, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:19:45Z") },
            new Product { Id=103, Name="Connecteur d’alimentation DC", Description="Le connecteur DC-005 SMD est une prise d’alimentation standard 5.5 × 2.1 mm, conçue pour montage en surface sur circuit imprimé (PCB).", Price=500m, ImageUrl="/images/products/Connecteur alimentation DC.jpeg", CategoryId=2, Stock=60, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:20:35Z") },
            new Product { Id=104, Name="Carte de développement ESP32-S3-WROOM", Description="La carte ESP32-S3-WROOM-N16R8 est un microcontrôleur puissant basé sur la puce ESP32-S3 avec connectivité WiFi et Bluetooth intégrée. Elle dispose de 16 MB de mémoire Flash et 8 MB de PSRAM.", Price=15000m, ImageUrl="/images/products/ESP32-S3.jpeg", CategoryId=5, Stock=80, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:23:06Z") },
            new Product { Id=105, Name="Micro moteur DC", Description="Le micro moteur DC 716 (7×16 mm) est un moteur compact à grande vitesse, spécialement conçu pour les mini drones, hélicoptères DIY et petits projets robotiques", Price=2000m, ImageUrl="/images/products/Mini moteur DC.jpg", CategoryId=10, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:25:56Z") },
            new Product { Id=106, Name="Module MPU6050", Description="Le MPU6050 est un capteur de mouvement combinant un accéléromètre 3 axes et un gyroscope 3 axes dans un seul module. Il permet de mesurer l’orientation, l’inclinaison, la rotation et les mouvements d’un objet avec une grande précision.", Price=4000m, ImageUrl="/images/products/MPU6050.jpg", CategoryId=1, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:25:29Z") },
            new Product { Id=107, Name="Clavier matriciel à membrane 4x4", Description="Le clavier matriciel à membrane est un dispositif d’entrée compact permettant d’envoyer des commandes à un microcontrôleur.", Price=3000m, ImageUrl="/images/products/Clavier.jpeg", CategoryId=2, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:26:51Z") },
            new Product { Id=108, Name="Pompe miniature DC 12V", Description="La pompe miniature DC 12V basée sur moteur 365/385 est une pompe compacte et auto-amorçante capable de transférer de l’eau ou d’autres liquides légers avec une bonne pression.", Price=5000m, ImageUrl="/images/products/Mini pompe.jpeg", CategoryId=10, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:27:46Z") },
            new Product { Id=109, Name="Micro Servo 9g", Description="Le SG90 9g est un micro servomoteur compact et léger, largement utilisé dans les projets électroniques, la robotique et les modèles RC.", Price=2000m, ImageUrl="/images/products/servo.jpg", CategoryId=10, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T16:56:44Z") },
            new Product { Id=110, Name="Mini moteur à engrenages", Description="Le moteur N20 à engrenages métalliques est un micro moteur à courant continu compact, conçu pour offrir un couple élevé dans un format réduit.", Price=4000m, ImageUrl="/images/products/engrenage.jpg", CategoryId=10, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T16:56:44Z") },
            new Product { Id=111, Name="Interrupteur tactile capacitif", Description="Module interrupteur tactile capacitif DC 5V–24V 3A permettant de contrôler l’allumage, l’extinction et la gradation d’une lumière par simple toucher.", Price=2000m, ImageUrl="/images/products/Interrupteur-capacitif.jpeg", CategoryId=1, Stock=35, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:29:30Z") },
            new Product { Id=112, Name="Module convertisseur RS232 vers TTL", Description="Le module MAX3232 permet de convertir les signaux RS232 en niveau logique TTL (et inversement).", Price=2000m, ImageUrl="/images/products/RS232 vers TTL.jpg", CategoryId=6, Stock=45, IsAvailable=true, CreatedAt=DateTime.Parse("2026-02-23T13:45:28Z") },
            new Product { Id=113, Name="Capteur de poids 20KG", Description="Ensemble composé d’une cellule de charge 20KG et du module convertisseur HX711, conçu pour mesurer précisément le poids dans vos projets électroniques.", Price=10000m, ImageUrl="/images/products/Capteur de poid.jpeg", CategoryId=1, Stock=25, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T15:46:54Z") },
            new Product { Id=114, Name="Module d’alimentation DC 7V–24V vers 5V 5A – 6 canaux", Description="Module d’alimentation servo DC 7V–24V vers 5V 5A – 6 canaux, conçu pour alimenter plusieurs servomoteurs simultanément de manière stable et sécurisée.", Price=2500m, ImageUrl="/images/products/alimentation Servo.jpeg", CategoryId=11, Stock=10, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:22:14Z") },
            new Product { Id=115, Name="Capteur de courant ZMCT103C", Description="Capteur de courant ZMCT103C 5A AC, transformateur de courant monophasé conçu pour mesurer le courant alternatif avec précision.", Price=3000m, ImageUrl="/images/products/ZMCT103C.jpeg", CategoryId=1, Stock=10, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T12:03:25Z") },
            new Product { Id=116, Name="Module convertisseur DC-DC abaisseur 24V / 12V vers 5V", Description="Module convertisseur DC-DC abaisseur 24V / 12V vers 5V 5A XY-3606 (LM2596S) conçu pour fournir une alimentation 5V stable et haute puissance à partir d’une source 12V ou 24V.", Price=30000m, ImageUrl="/images/products/LM2596S.jpeg", CategoryId=11, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T16:56:54Z") },
            new Product { Id=117, Name="Interrupteur à bascule", Description="Interrupteur à bascule YZ KCD1-101 ON/OFF 2PIN, conçu pour le contrôle simple marche/arrêt des équipements électriques.", Price=500m, ImageUrl="/images/products/Interrupteur.jpeg", CategoryId=2, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:21:46Z") },
            new Product { Id=118, Name="Écran LCD ESP32 Xtouch 2.4", Description="L’écran ESP32 Xtouch 2.4 pouces (référence ESP32-2432S028R) est un module intelligent intégrant un microcontrôleur ESP32 et un écran TFT couleur RGB 240×320.", Price=15000m, ImageUrl="/images/products/Écran LCD ESP32.jpeg", CategoryId=5, Stock=20, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:21:56Z") },
            new Product { Id=119, Name="Testeur multifonction LCD GM328A", Description="Testeur multifonction LCD GM328A conçu pour identifier et mesurer automatiquement les composants électroniques tels que transistors, diodes, MOSFET, résistances, condensateurs et bobines.", Price=12000m, ImageUrl="/images/products/GM328A.png", CategoryId=1, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:22:09Z") },
            new Product { Id=120, Name="Boîte de 530 pièces de tubes thermorétractables", Description="Boîte de 530 pièces de tubes thermorétractables assortis, idéale pour l’isolation, la protection et l’organisation des câbles électriques.", Price=5000m, ImageUrl="/images/products/thermorétractables.jpeg", CategoryId=2, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:23:44Z") },

            new Product { Id=121, Name="Module ultrasonore étanche AJ-SR04M", Description="Le module AJ-SR04M est un capteur ultrasonore étanche conçu pour la mesure précise de distance en environnement extérieur ou humide.", Price=5000m, ImageUrl="/images/products/AJ-SR04M.jpeg", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:22:19Z") },
            new Product { Id=122, Name="Coffret Complet d’Initiation Électronique", Description="Kit d’initiation complet présenté dans un coffret de rangement compartimenté, idéal pour débutants, étudiants et passionnés d’électronique. Il contient les composants essentiels pour apprendre la programmation et le prototypage avec une carte compatible UNO R3.", Price=60000m, ImageUrl="/images/products/Initiation Électronique.png", CategoryId=7, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:21:31Z") },
            new Product { Id=123, Name="Module d’isolation optocoupleur PC817", Description="Le module PC817 est une carte d’isolation optocoupleur permettant de séparer électriquement les circuits de commande et les charges. Disponible en 2, 4 ou 8 canaux.", Price=2000m, ImageUrl="/images/products/PC817.jpeg", CategoryId=2, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-17T18:54:17Z") },
            new Product { Id=124, Name="Capteur pression atmosphérique BMP280 / BME280", Description="Le module BMP280 / BME280 est un capteur environnemental haute précision permettant de mesurer la pression atmosphérique", Price=4000m, ImageUrl="/images/products/BMP280.png", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T13:27:23Z") },
            new Product { Id=125, Name="Carte d’extension Nano IO Shield", Description="Carte d’extension pour Arduino Nano V3.0 permettant de connecter facilement les entrées/sorties via des borniers à vis.", Price=3500m, ImageUrl="/images/products/Nano IO Shield.jpeg", CategoryId=2, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:22:55Z") },

            new Product { Id=126, Name="Module ESP32-CAM", Description="Module ESP32-CAM intégrant un microcontrôleur WiFi + Bluetooth et une caméra OV2640 haute résolution.", Price=8000m, ImageUrl="/images/products/ESP32-CAM.jpeg", CategoryId=5, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:22:49Z") },
            new Product { Id=127, Name="Module convertisseur DC-DC abaisseur (Buck) 6V–20V vers 5V 3A USB", Description="Module convertisseur DC-DC abaisseur (Buck) 6V–20V vers 5V 3A USB, conçu pour transformer une tension d’entrée 12V ou 24V en une sortie 5V stable haute puissance.", Price=2000m, ImageUrl="/images/products/DC-DC.jpeg", CategoryId=11, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:23:11Z") },
            new Product { Id=128, Name="Moteur pas à pas NEMA 17", Description="Le moteur pas à pas SM-42HB34F08AB est un moteur biphasé de type NEMA 17 conçu pour les applications de précision telles que l’impression 3D, la CNC et les systèmes automatisés.", Price=6000m, ImageUrl="/images/products/NEMA 17.jpeg", CategoryId=10, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:51:35Z") },

            new Product { Id=129, Name="Module écran LCD 1602A 16×2", Description="Le module LCD 1602A 16×2 est un écran alphanumérique rétroéclairé conçu pour afficher 16 caractères sur 2 lignes.", Price=5000m, ImageUrl="/images/products/LCD.jpeg", CategoryId=1, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-10T16:23:57Z") },
            new Product { Id=130, Name="Module ZVS 1000W – Chauffage par induction basse tension 20A", Description="Ce chauffage par induction ZVS 1000W fonctionne avec une alimentation CC basse tension de 12 à 48V.", Price=20000m, ImageUrl="/images/products/zvs.jpeg", CategoryId=10, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:01:54Z") },

            new Product { Id=131, Name="Carte de développement Arduino Nano V3", Description="La carte Arduino Nano V3 basée sur le microcontrôleur ATmega328PB est une version compacte et performante idéale pour les projets embarqués et le prototypage sur breadboard.", Price=5000m, ImageUrl="/images/products/NANO.jpeg", CategoryId=5, Stock=100, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:25:11Z") },
            new Product { Id=132, Name="Module de commutation automatique batterie YX850 – 5V à 48V", Description="Le module YX850 est un système de commutation automatique permettant de basculer instantanément vers une batterie de secours en cas de coupure d’alimentation principale.", Price=3000m, ImageUrl="/images/products/commutation.png", CategoryId=1, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:31:08Z") },
            new Product { Id=133, Name="Module capteur température & humidité AHT10", Description="Le module AHT10 est un capteur numérique haute précision permettant la mesure de la température et de l’humidité via interface I2C", Price=3500m, ImageUrl="/images/products/AHT10.png", CategoryId=1, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:32:52Z") },
            new Product { Id=134, Name="Capteur de température DS18B20", Description="Le capteur DS18B20 est une sonde de température numérique étanche, logée dans un boîtier en acier inoxydable robuste.", Price=2000m, ImageUrl="/images/products/DS18B20.jpeg", CategoryId=1, Stock=20, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:42:08Z") },
            new Product { Id=135, Name="Moteur pas à pas 5V 28BYJ-48 + Module driver ULN2003 pour Arduino", Description="Ensemble composé du moteur pas à pas 28BYJ-48 5V et de son module driver ULN2003, idéal pour projets Arduino et systèmes embarqués.", Price=4000m, ImageUrl="/images/products/Moteur pas à pas.png", CategoryId=10, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:45:26Z") },
            new Product { Id=136, Name="SERRURE CJSD – DC 12V", Description="Le verrou électromagnétique CJSD est un solénoïde compact conçu pour sécuriser les portes d’armoires, tiroirs ou coffres.", Price=4000m, ImageUrl="/images/products/verrou.png", CategoryId=10, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:48:18Z") },
            new Product { Id=137, Name="Moteur DC TT avec roue", Description="Le moteur DC TT avec roue est une solution simple et efficace pour la conception de robots mobiles et voitures intelligentes.", Price=3000m, ImageUrl="/images/products/Moteur-DC-&-Roue.jpeg", CategoryId=10, Stock=30, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:50:47Z") },
            new Product { Id=138, Name="Kit de capteurs Arduino 45 en 1", Description="Le kit de capteurs 45 en 1 pour Arduino est une solution complète et évoluée pour l’apprentissage de l’électronique et le développement de projets DIY. ", Price=15000m, ImageUrl="/images/products/kit capteur.png", CategoryId=7, Stock=40, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:53:00Z") },

            new Product { Id=139, Name="Module DC-DC élévateur MT3608", Description="Le module MT3608 est un convertisseur DC-DC élévateur (Step-Up) permettant d’augmenter une tension continue basse vers une tension plus élevée et réglable.", Price=3000m, ImageUrl="/images/products/MT3608.png", CategoryId=11, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:54:07Z") },
            new Product { Id=140, Name="Carte de développement STM32 NUCLEO-F401RE", Description="La carte de développement STM32 NUCLEO-F401RE est basée sur le microcontrôleur ARM Cortex-M4 STM32F401RET6. Elle offre une plateforme puissante et flexible pour le développement d’applications embarquées professionnelles.", Price=17000m, ImageUrl="/images/products/STM32.png", CategoryId=5, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:56:48Z") },

            new Product { Id=141, Name="Module RFID RC522", Description="Le module RFID RC522 est un lecteur/écrivain de cartes sans contact fonctionnant à 13,56 MHz.", Price=4000m, ImageUrl="/images/products/RFID.jpeg", CategoryId=1, Stock=60, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T17:58:48Z") },
            new Product { Id=142, Name="Moteur TT Réducteur DC – Fort Couple", Description="Le moteur TT à courant continu avec réducteur est un grand classique des projets robotiques et éducatifs. Compact mais costaud, il offre un fort couple, une vitesse stable et une excellente fiabilité, ce qui le rend parfait pour les voitures robotiques, robots suiveurs de ligne, projets Arduino et formations techniques.", Price=1000m, ImageUrl="/images/products/Moteur TT.jpeg", CategoryId=10, Stock=50, IsAvailable=true, CreatedAt=DateTime.Parse("2026-03-09T18:00:27Z") }
            
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
