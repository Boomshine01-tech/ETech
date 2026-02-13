// Server/ETechEnergie.Server/Data/DbInitializer.cs

using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Data;

public static class DbInitializer
{
    public static async Task Initialize(AppDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (context.Services.Any())
            return;

        // ===================================
        // 1. SERVICES
        // ===================================
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

        // ===================================
        // 2. CATÉGORIES
        // ===================================
        // ✅ CORRECTION : Ajout de la 5ème catégorie pour les cartes de développement
        var categories = new Category[]
        {
            new Category { Name = "Capteurs électroniques", Description = "Capteurs et détecteurs pour applications électroniques" },
            new Category { Name = "Câbles et connectique", Description = "Câbles électriques et réseau de qualité professionnelle" },
            new Category { Name = "Équipements réseau", Description = "Matériel pour réseaux informatiques et télécommunications" },
            new Category { Name = "Énergies renouvelables", Description = "Panneaux solaires et équipements pour énergie verte" },
            new Category { Name = "Cartes de développement", Description = "Arduino, ESP32, Raspberry Pi pour projets électroniques" } // ✅ AJOUTÉ
        };

        context.Categories.AddRange(categories);
        await context.SaveChangesAsync();
        Console.WriteLine($"✅ {categories.Length} catégories ajoutées");

        // ===================================
        // 3. PRODUITS
        // ===================================
        var products = new Product[]
        {
            // Catégorie 1 : Capteurs électroniques
            new Product
            {
                Name = "Capteur d'empreinte digitale",
                Description = "Capteur biométrique permettant l'identification par empreinte digitale avec haute précision",
                Price = 15000,
                ImageUrl = "/images/products/fingerprint-sensor.jpg",
                CategoryId = 1,
                Stock = 30,
                IsAvailable = true
            },
            new Product
            {
                Name = "Capteur infrarouge E18-D80NK",
                Description = "Capteur infrarouge de proximité réglable pour détection d'obstacles sans contact",
                Price = 2500,
                ImageUrl = "/images/products/e18-d80nk.jpg",
                CategoryId = 1,
                Stock = 40,
                IsAvailable = true
            },
            new Product
            {
                Name = "Capteur de température et humidité DHT22",
                Description = "Capteur numérique haute précision pour la mesure de la température et de l'humidité",
                Price = 3000,
                ImageUrl = "/images/products/dht22.jpg",
                CategoryId = 1,
                Stock = 60,
                IsAvailable = true
            },
            new Product
            {
                Name = "Capteur de température et humidité DHT11",
                Description = "Capteur économique pour la mesure basique de la température et de l'humidité",
                Price = 2000,
                ImageUrl = "/images/products/dht11.jpg",
                CategoryId = 1,
                Stock = 80,
                IsAvailable = true
            },

            // Catégorie 2 : Câbles et connectique
            new Product
            {
                Name = "Câble électrique 2.5 mm²",
                Description = "Câble électrique en cuivre 2.5 mm² pour installations domestiques",
                Price = 21500,
                ImageUrl = "/images/products/cable-2-5.jpg",
                CategoryId = 2,
                Stock = 200,
                IsAvailable = true
            },
            new Product
            {
                Name = "Câble électrique 1.5 mm²",
                Description = "Câble électrique en cuivre 1.5 mm² pour éclairage et petits équipements",
                Price = 17500,
                ImageUrl = "/images/products/cable-1-5.jpg",
                CategoryId = 2,
                Stock = 250,
                IsAvailable = true
            },
            new Product
            {
                Name = "Câble électrique 6 mm²",
                Description = "Câble électrique haute section 6 mm² pour fortes puissances",
                Price = 31500,
                ImageUrl = "/images/products/cable-6.jpeg",
                CategoryId = 2,
                Stock = 150,
                IsAvailable = true
            },

            // Catégorie 3 : Équipements réseau
            new Product
            {
                Name = "Câble Ethernet RJ45",
                Description = "Câble réseau Ethernet pour connexion internet et réseaux locaux",
                Price = 45000,
                ImageUrl = "/images/products/ethernet-cable.jpeg",
                CategoryId = 3,
                Stock = 100,
                IsAvailable = true
            },
            new Product
            {
                Name = "Switch réseau 8 ports",
                Description = "Switch Ethernet 8 ports pour interconnexion de plusieurs équipements réseau",
                Price = 25000,
                ImageUrl = "/images/products/network-switch.jpg",
                CategoryId = 3,
                Stock = 20,
                IsAvailable = true
            },

            // Catégorie 4 : Énergies renouvelables
            new Product
            {
                Name = "Panneau solaire 300W",
                Description = "Panneau solaire monocristallin haute performance pour production d'énergie renouvelable",
                Price = 150000,
                ImageUrl = "/images/products/solar-panel.jpg",
                CategoryId = 4,
                Stock = 50,
                IsAvailable = true
            },

            // Catégorie 5 : Cartes de développement (✅ CORRIGÉ)
            new Product
            {
                Name = "Arduino Uno R3",
                Description = "Carte de développement Arduino Uno idéale pour projets électroniques et éducatifs",
                Price = 8000,
                ImageUrl = "/images/products/arduino-uno.jpg",
                CategoryId = 5, // ✅ Maintenant valide car CategoryId 5 existe
                Stock = 35,
                IsAvailable = true
            },
            new Product
            {
                Name = "ESP32",
                Description = "Carte de développement ESP32 avec Wi-Fi et Bluetooth intégrés pour projets IoT",
                Price = 6000,
                ImageUrl = "/images/products/esp32.jpg",
                CategoryId = 5,
                Stock = 45,
                IsAvailable = true
            },
            new Product
            {
                Name = "Raspberry Pi 4",
                Description = "Mini-ordinateur Raspberry Pi pour projets informatiques, domotiques et éducatifs",
                Price = 75000,
                ImageUrl = "/images/products/raspberry-pi.jpg",
                CategoryId = 5,
                Stock = 25,
                IsAvailable = true
            }
        };

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
        Console.WriteLine($"✅ {products.Length} produits ajoutés");

        // ===================================
        // 4. MEMBRES DE L'ÉQUIPE
        // ===================================
        var teamMembers = new TeamMember[]
        {
            new TeamMember
            {
                Name = "Demba Diol",
                Position = "Directeur Général",
                Bio = "Expert en gestion d'entreprise avec plus de 10 ans d'expérience dans le secteur de l'électricité.",
                ImageUrl = "/images/team/ceo.png",
                DisplayOrder = 1,
                
            },
            new TeamMember
            {
                Name = "Papa Gueye",
                Position = "Responsable Technique",
                Bio = "Ingénieur électricien, spécialisé en énergies renouvelables et installations domestiques.",
                ImageUrl = "/images/team/technical-lead.png",
                DisplayOrder = 2,
                
            },
            new TeamMember
            {
                Name = "Demba Diol",
                Position = "Chef de Projet",
                Bio = "Gestion de projets techniques complexes avec une expertise en coordination d'équipes.",
                ImageUrl = "/images/team/project-manager.png",
                DisplayOrder = 3,
                
            },
            new TeamMember
            {
                Name = "Adama Ndao",
                Position = "Responsable Support Client",
                Bio = "Spécialiste en relation client et support technique, garantissant votre satisfaction.",
                ImageUrl = "/images/team/support-lead.png",
                DisplayOrder = 4,
                
            }
        };

        context.TeamMembers.AddRange(teamMembers);
        await context.SaveChangesAsync();
        Console.WriteLine($"✅ {teamMembers.Length} membres d'équipe ajoutés");

        Console.WriteLine("");
        Console.WriteLine("╔═══════════════════════════════════════════════╗");
        Console.WriteLine("║   ✅ Initialisation Terminée avec Succès !   ║");
        Console.WriteLine("╚═══════════════════════════════════════════════╝");
    }
}