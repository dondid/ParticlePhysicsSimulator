# 🌟 Particle Physics Simulator

Un simulator interactiv de particule fizice dezvoltat în C# cu Windows Forms, care permite utilizatorilor să creeze și să manipuleze particule cu diferite proprietăți fizice în timp real.

## ✨ Caracteristici

### 🎮 Funcționalități Interactive
- **Creare particule prin click/drag** - Click sau drag pe canvas pentru a adăuga particule
- **Tipuri diferite de particule**:
  - **Normal** - Particule standard (albastru)
  - **Heavy** - Particule grele cu masă mare (roșu)
  - **Light** - Particule ușoare și rapide (verde)
  - **Bouncy** - Particule cu elasticitate mare (portocaliu)

### ⚙️ Controale Fizice
- **Gravitație ajustabilă** - Trackbar pentru controlul forței gravitaționale (0.0 - 2.0)
- **Damping (frecare)** - Control pentru reducerea vitezei în timp (0.90 - 1.00)
- **Activare/dezactivare gravitație** - Checkbox pentru a activa/dezactiva gravitația
- **Activare/dezactivare coliziuni** - Checkbox pentru coliziunile între particule

### 🎯 Simulare Fizică Realistă
- **Coliziuni elastice** între particule cu conservarea momentului
- **Coliziuni cu pereții** cu proprietăți de elasticitate
- **Vectori de viteză vizuali** - Linii galbene care arată direcția și viteza particulelor
- **Simulare în timp real** la ~60 FPS

### 🎨 Interfață Modernă
- **Design dark theme** pentru o experiență vizuală plăcută
- **Controale intuitive** cu butoane colorate și trackbare
- **Feedback vizual** în timp real
- **Contor de particule** pentru monitorizarea simulării

## 🛠️ Cerințe de Sistem

- **Sistem de operare**: Windows 10/11
- **.NET**: .NET 6.0 sau superior
- **IDE recomandat**: JetBrains Rider, Visual Studio 2022, sau Visual Studio Code

## 📦 Instalare și Rulare

### Opțiunea 1: JetBrains Rider (Recomandat)

1. **Deschide JetBrains Rider**
2. **File → Open** și selectează folderul proiectului
3. **Sau File → New → Project from Existing Sources**
4. Rider va detecta automat fișierul `.csproj`
5. **Build → Build Solution** (Ctrl+F9)
6. **Run → Start Debugging** (F5) sau **Run → Start Without Debugging** (Ctrl+F5)

### Opțiunea 2: Command Line

```bash
# Clonează sau descarcă proiectul
# Navighează în directorul proiectului
cd ParticlePhysicsSimulator

# Restore packages (dacă este necesar)
dotnet restore

# Build proiectul
dotnet build

# Rulează aplicația
dotnet run
```

### Opțiunea 3: Visual Studio

1. **File → Open → Project/Solution**
2. Selectează fișierul `ParticlePhysicsSimulator.csproj`
3. **Build → Build Solution** (Ctrl+Shift+B)
4. **Debug → Start Debugging** (F5)

## 🎮 Cum să Folosești Simulatorul

### Comenzi de Bază
1. **Start/Stop** - Pornește/oprește simularea fizică
2. **Clear** - Șterge toate particulele din canvas
3. **Add Particle** - Adaugă o particulă random pe canvas

### Creare Particule
- **Click simplu** pe canvas pentru o particulă
- **Click și drag** pentru multiple particule consecutive
- **Selectează tipul** din dropdown înainte de creare

### Ajustări Fizice
- **Gravity Slider**: Controlează forța gravitațională
- **Damping Slider**: Controlează "fricțiunea aerului"
- **Enable Gravity**: Activează/dezactivează gravitația globală
- **Enable Collisions**: Activează/dezactivează coliziunile între particule

### Tipuri de Particule
- 🔵 **Normal**: Echilibrate, bune pentru teste generale
- 🔴 **Heavy**: Grele, cad rapid, greu de mișcat
- 🟢 **Light**: Ușoare, plutesc, se mișcă rapid
- 🟠 **Bouncy**: Foarte elastice, "sărind" mult

## 🧪 Experimente Recomandate

### Experiment 1: Gravitație și Masă
1. Adaugă particule **Heavy** și **Light**
2. Activează gravitația la maximum
3. Observă cum particulele grele cad mai repede

### Experiment 2: Coliziuni Elastice
1. Creează mai multe particule **Bouncy**
2. Dezactivează gravitația
3. Adaugă particule **Heavy** pentru impact
4. Observă transferul de energie

### Experiment 3: Simulare Gaz
1. Adaugă multe particule **Light**
2. Setează gravitația la minim
3. Damping-ul la 0.99
4. Observă comportamentul asemănător unui gaz

## 🎨 Personalizare

### Modificarea Culorilor
Poți modifica culorile particulelor în clasa `Particle`, metoda `SetParticleProperties()`:

```csharp
case ParticleType.Normal:
    Color = Color.FromArgb(100, 150, 255); // Schimbă aici
    break;
```

### Adăugarea de Noi Tipuri
1. Adaugă în enum-ul `ParticleType`
2. Extinde `SetParticleProperties()` cu noul caz
3. Adaugă în ComboBox din `InitializeComponent()`

### Modificarea Fizicii
Ajustează constantele în metoda `UpdatePhysics()`:
- Forța gravitației
- Coeficientul de damping
- Proprietățile de coliziune

## 🐛 Troubleshooting

### Probleme Comune

**Aplicația nu pornește:**
- Verifică că ai .NET 6.0 instalat
- Verifică că Windows Forms este suportat

**Performance scăzut:**
- Reduce numărul de particule
- Dezactivează coliziunile pentru mai multe particule

**Coliziunile nu funcționează corect:**
- Verifică că checkbox-ul "Enable Collisions" este activat
- Restart simularea după modificări

## 📁 Structura Proiectului

```
ParticlePhysicsSimulator/
├── MainForm.cs              # Interfața principală și logica UI
├── Program.cs               # Entry point al aplicației
├── ParticlePhysicsSimulator.csproj  # Configurația proiectului
└── README.md               # Acest fișier
```

## 🔮 Funcționalități Viitoare

- [ ] Salvare/încărcare configurații
- [ ] Mai multe tipuri de particule (magnetice, radioactive)
- [ ] Câmpuri de forță (atracție/respingere)
- [ ] Export animații ca GIF
- [ ] Statistici fizice în timp real
- [ ] Mode preset pentru diferite experimente

## 🤝 Contribuții

Proiectul este deschis pentru contribuții! Poți:
1. Adăuga noi tipuri de particule
2. Îmbunătăți algoritmii fizici
3. Crea noi experimente preset
4. Optimiza performanța

## 📄 Licență

Acest proiect este dezvoltat în scop educațional și poate fi folosit și modificat liber.

## 📞 Contact

Pentru întrebări sau sugestii, nu ezita să creezi un issue sau să contribui la proiect!

---

**Distrează-te explorând fizica particulelor! 🚀**