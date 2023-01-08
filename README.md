# Simulation de dinosaures!
## Découverte des entités
Toutes les formes de vie disposent de points d'énergie. Lorsque l'énergie est épuisée, des points de vie sont convertis en énergie.
Chaque tour, l'énergie de chaque forme de vie est décrémentée. Une fois tout sa vie épuisée, l'entité meurt. La carte est en wraparound.
### Animaux `Animals`
- Se déplacent
- Ont une zone de vision et une zone d'action: les animaux se dirigent vers les entités intéressantes présentes dans leur zone de vision, et peuvent interagir avec une fois celles-ci dans leur zone d'action
- Se transforment en viande à la fin de leur vie: prédation ou mort naturelle
- Peuvent se reproduire avec un semblable de sexe opposé: un petit apparaitra après une période de gestation[^1]

[^1]: Les dinosaures sont ovipares mais on fera comme si de rien n'était ;-)

#### Carnivores `Carnivorous` ![](https://placehold.co/15x15/red/red)
- Attaquent et pourchassent tous les herbivores
- Mangent de la viande
- Forts points d'attaque

Exemple: `Tyrannosaurus`

#### Herbivores `Herbivorous`  ![](https://placehold.co/15x15/darkblue/darkblue)
- Mangent des plantes
- Faibles points d'attaque

Exemple: `Stegosaurus`

### Plantes `Plants`  ![](https://placehold.co/15x15/lightgreen/lightgreen)
- Mangent les déchets organiques situés dans leur zone de racines
- Se transforment en déchet organique à leur mort
- Réplication dans leur zone de semis
	- Faible chance à chaque tour
	- Grande chance à l'absorption d'un déchet organique 

Exemple: `Fern`

### Viande `Meat` ![](https://placehold.co/15x15/darkred/darkred)
- Se transforme en déchet organique après un certain temps

### Déchets organiques `OrganicWaste` ![](https://placehold.co/15x15/green/green)
- Disparait après un certain temps
- Petite chance de génére une plante en disparaissant

## Gestion des interactions
Chaque entité calcule la distance à toute entité existante par le théorème de Pythagore: `r² = (x-a)² + (y-b)²`. 
Si une cible se trouve dans le rayon d'action, son type est analysé et un comportement adapté est choisi. Si une cible est dans le rayon de visibilité, l'animal avance de quelques pas dans sa direction.

## Principes SOLID

### Depency Inversion Principle
Le depency inversion principal veut que les sous classes ne dépendent que de classe abstraite. Ce qui aide à maintenir un autre principe SOLID qui est l’ « Open/close principle ». Dans notre projet, toutes les classes concrêtes héritent de classe abstraite et des classes abstraites héritent eux-mêmes d’autres classes abstraites. Ceci est possible car, dans notre cas, nous ne devons jamais instancier les classes dont hérite les classes concrêtes, nous avions pu donc toutes les passer en mode abstraites.

### Open/close Principle
Le open/close principle veut que le code soit ouvert aux extensions et fermé à la modification. Dans notre projet, l’utilisation des classes abstraites nous permette en partie de respecter ce principe. De plus, la création d’une nouvelle classe d’une espèce carnivore, par exemple, ne nous obligera pas à modifier quelconque classe supérieur. Ce qui valide l’ajout d’extension et la fermeture à la modification.

## Diagrammes UML

---
Diagramme de séquences
---
```mermaid
sequenceDiagram
	
	participant Simulation
	participant Tyranosaurus
	participant Stegosaurus
	participant Fern
	participant Meat
	participant OrganicWaste
	loop every 0.25sec
		Simulation->>Tyranosaurus: Update()
		alt HealthPoints <= 0
			Tyranosaurus->>Simulation: RemoveObjet(this)
			Tyranosaurus->>Simulation: AddObject(new Meat(Colors.Brown, this.X, this.Y, Simu))
			Simulation->>Meat: <<create>>
		end
		alt DischargeFreq == 0
			Tyranosaurus->>Simulation: AddObject(new OrganicWaste(Colors.Green, this.X, this.Y, Simu))
			Simulation->>OrganicWaste: <<create>>
		end
		alt GestationTime == 0
			Tyranosaurus->>Simulation: AddObject(new Tyrannosaurus(Colors.Red, mom.X, mom.Y, Simu, 100, 100, random.Next(60, 95), Convert.ToBoolean(random.Next(2)), -1, 100, 20)))
			Simulation->>Tyranosaurus: <<create>>
		end
		Tyranosaurus->>Simulation: NearbyObject(this, this.FieldOfView, this.ContactRange)
		alt obj in listContact is Meat
			Tyranosaurus->>Simulation: RemoveObjet(obj)
		else obj in listcontact is Stegosaurus
			Tyranosaurus->>Stegosaurus: Attack(this, obj)
		end
		
		
		Simulation->>Stegosaurus: Update()
		alt HealthPoints <= 0
			Stegosaurus->>Simulation: RemoveObjet(this)
			Stegosaurus->>Simulation: AddObject(new Meat(Colors.Brown, this.X, this.Y, Simu))
			Simulation->>Meat: <<create>>
		end
		alt DischargeFreq == 0
			Stegosaurus->>Simulation: AddObject(new OrganicWaste(Colors.Green, this.X, this.Y, Simu))
			Simulation->>OrganicWaste: <<create>>
		end
		alt GestationTime == 0
			Stegosaurus->>Simulation: AddObject(new Stegosaurus(Colors.DarkBlue, mom.X, mom.Y, Simu, 100, 100, random.Next(0, 30), Convert.ToBoolean(random.Next(2)), -1, 50, 20))
			Simulation->>Stegosaurus: <<create>>
		end
		Stegosaurus->>Simulation: NearbyObject(this, this.FieldOfView, this.ContactRange)
		alt obj in listContact is Plant
			Stegosaurus->>Simulation: RemoveObjet(obj)
		end
		
		
		Simulation->>Fern: Update()
		alt HealthPoints <= 0
			Fern->>Simulation: RemoveObjet(this)
			Fern->>Simulation: AddObjet(new OrganicWaste(Colors.Green, this.X, this.Y, Simu))
			Simulation->>OrganicWaste: <<create>>
		end
		alt random.Next(0, 75) == 5
			Fern->>Simulation: AddObjet(new Fern(Colors.LightGreen, this.X + random.Next(-this.SowingRadius, this.SowingRadius), this.Y + random.Next(-this.SowingRadius, this.SowingRadius), Simu, 50, 20, 50, 15));
		end
		Fern->>Simulation: NearbyObject(this, this.FieldOfView, this.ContactRange)
		alt obj in listContact is OrganicWaste
			Fern->>Simulation: RemoveObjet((OrganicWaste)obj)
		end
		
		
		Simulation->>Meat: Update()
		alt lifeExpectancy <= 0
			Meat->>Simulation: RemoveObjet(this)
			Meat->>Simulation: AddObjet(new OrganicWaste(Colors.Green, this.X, this.Y, Simu));
			Simulation->>OrganicWaste: <<create>>
		end
		
		
		Simulation->>OrganicWaste: Update()
		alt durability == 0
			OrganicWaste->>Simulation: RemoveObjet(this);
			alt random.Next(0, 50) == 1
				OrganicWaste->>Simulation: AddObjet(new Fern(Colors.LightGreen, this.X, this.Y, Simu, 10, 20, 30, 15));
				Simulation->>Fern: <<create>>
			end
		end
	end
	
	
```
---
Diagramme de classes
---

```mermaid
classDiagram

    Meat --|> SimulationObject 
    DrawableObject <|-- SimulationObject
    OrganicWaste --|> SimulationObject
    Carnivorous --|> Animal
    Organism <|-- Animal
    SimulationObject <|-- Organism
    Organism <|-- Plant
    Plant <|-- Fern
    Tyrannosaurus --|> Carnivorous
    Herbivorous --|> Animal
    Stegosaurus --|> Herbivorous
    Simulation --|> IDrawable
    Simulation --* SimulationObject

    class Simulation{
        -List objects : List
        -List nextState : List
        -id : int
        -distance : double
        +AddObjet()
        +RemoveObjet()
        +NearbyObject(DrawableObject subject, double fieldOfView, double contactRange) List<DrawableObject subject>
	+Update()
	+Draw(ICanvas canvas, RectF dirtyRect)
    }
    class SimulationObject{
    	<<Abstract>>
    	-simulation : Simulation
	+Simu() Simulation
        +Update()*
    }
    class DrawableObject{
    	<<Abstract>>
        -color : Color
        -x : double
        -y : double
	+Color() Color
	+X() double
	+Y() double
    }
    class Organism{
    	<<Abstract>>
        -healtPoints : int
        -energyPoints : int
	+HealtPoints() int
	+EnergyPoints() int
	+Update()
    }
    class Meat{
        -lifeExpectancy : int
	+Update()
    }
    class OrganicWaste{
        -durability : int
	+Update()
    }
    class Animal{
    	<<Abstract>>
        -strenght : int
        -female : bool
        -gestationTime : int
        -fieldOfView : int
        -contactRange : int
        -cooldown : int
        -dischargeFreq : int
	-baby : Type
	+Strenght() int
	+Female() bool
	+GestationTime() int
	+FieldOfView() int
	+ContactRange() int
	+Cooldown() int
	+DischargeFreq() int
	+Baby() Type
        -Dies()
        +Breed(Animal subject, DrawableObject target)
        +Birth(Animal mom)
        -Discharge()
        -Move()
        -LoseHealth()
        +Update()
    }
    class Carnivorous{
    	<<Abstract>>
    	-Eat(DrawableObject obj)
        -Attack(Carnivorous subject, DrawableObject target)
        +Update()
    }
    class Tyrannosaurus{
        +override Update()
	+Birth(Animal mom)
    }
    class Herbivorous{
    	<<Abstract>>
        -int visibleObjectsLength
        -int listContactLength
        -DrawableObject obj
        -Eat(Plant obj)
        +Update()
    }
    class Stegosaurus{
        +Update()
	+Birth(Animal mom)
    }
    class Plant{
    	<<Abstract>>
        -rootRadius : int
        -sowingRadius : int
	-seed : Type
	+RootRadius() int
        +SowingRadius() int
	+Seed() Type
	-Eat(DrawableObject obj)
	+Expand(DrawableObject obj)
	+Update()
    }
    class Fern{
        +Update()
	+Expand(DrawableObject obj)
    }
```
