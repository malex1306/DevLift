# Quiz-App Projektplan (ASP.NET Core + Razor Pages)

## 📌 **1. Projekt-Setup**
- [x] Neues ASP.NET Core Web App (Razor Pages)-Projekt in Rider erstellen
- [x] `.NET 6+` als Ziel-Framework auswählen
- [ ] Projektstruktur bereinigen (unnötige Beispieldateien löschen)

## 📂 **2. Datenmodell & Logik**
- [x] Ordner `Models` erstellen
- [ ] Klasse `QuizQuestion.cs` anlegen mit:
    - Frage-Text (`string`)
    - Richtige Antwort (`string`)
    - Liste falscher Antworten (`List<string>`)
    - (Optional) Kategorie/Schwierigkeit
- [ ] Datenquelle planen:
    - [ ] Option 1: JSON-Datei (`quizdata.json` im `Data`-Ordner)
    - [ ] Option 2: SQLite-Datenbank (für Skalierbarkeit)

## 🖥️ **3. Razor Pages**
### **Startseite (`Index.cshtml`)**
- [x] Begrüßungstext + Quiz-Start-Button
- [x] Link zur Quiz-Seite (`/Quiz`)

### **Quiz-Seite (`Quiz.cshtml`)**
- [ ] Frage anzeigen (aus `QuizQuestion`)
- [ ] 5 Antwort-Buttons (1 richtig + 4 falsch, zufällig gemischt)
- [ ] "Nächste Frage"-Button (zählt Fortschritt)
- [ ] Logik: Ausgewählte Antwort prüfen (richtig/falsch)

### **Ergebnis-Seite (`Result.cshtml`)**
- [ ] Auswertung anzeigen (z. B. "5/10 richtig")
- [ ] Button "Neues Quiz starten" (zurück zu `/Index`)

## 🎨 **4. Frontend (UI)**
- [ ] Bootstrap einbinden (bereits in ASP.NET Core enthalten)
- [ ] Quiz-Karten stylen (Fragen/Antworten als Cards)
- [ ] Responsive Design testen (Mobile/Desktop)

## 🛠️ **5. Backend-Logik**
- [ ] Service erstellen (`IQuizService.cs` + `QuizService.cs`):
    - Lädt Fragen aus JSON/Datenbank
    - Mischt Antworten
    - Prüft Ergebnisse
- [ ] Dependency Injection in `Program.cs` registrieren

## 💾 **6. Datenverwaltung (optional)**
- [ ] JSON-Einlesen:
    - [ ] `Data/quizdata.json` mit Beispiel-Fragen erstellen
    - [ ] JSON-Parsing-Logik in `QuizService`
- [ ] Oder SQLite:
    - [ ] DbContext erstellen
    - [ ] Migrationen anlegen

## 🔄 **7. Navigation & Zustand**
- [ ] Session-State aktivieren (für Quiz-Fortschritt)
- [ ] Counter für richtige Antworten (zwischen Seiten speichern)

## 🧪 **8. Testing**
- [ ] Manuell testen:
    - [ ] Navigation zwischen Seiten
    - [ ] Antworten prüfen (richtige/falsche Zuweisung)
    - [ ] Mobile-Ansicht prüfen
- [ ] (Optional) Unit Tests für `QuizService`

## 🚀 **9. Deployment (optional)**
- [ ] Azure: Web App erstellen
- [ ] Oder Docker-Container bauen

---

### **Priorisierung**
1. Kernfunktion: Quiz mit Fragen/Antworten (Schritte 1–5)
2. Erweiterungen: Datenbank, Highscores, Multi-Quiz  