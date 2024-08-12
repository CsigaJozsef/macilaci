# Eseményvezérelt alkalmazásokra készített programok
## Közös követelmények:
 - A beadandók dokumentációból, valamint programból állnak, utóbbi csak a megfelelő dokumentáció bemutatásával értékelhető. Csak funkcionálisan teljes, a feladatnak megfelelő, önállóan megvalósított, személyesen bemutatott program fogadható el.
 - A megvalósításnak felhasználóbarátnak, és könnyen kezelhetőnek kell lennie. A szerkezetében törekednie kell az objektumorientált szemlélet megtartására. 
 - A programot háromrétegű (modell/nézet/perzisztencia) architektúrában kell felépíteni, amelyben elkülönül a megjelenítés, az üzleti logika, valamint az adatelérés (amennyiben része a feladatnak). Az egyes rétegeknek megfelelő funkcionalitást kell biztosítania, és megfelelő hierarchiában kell kommunikálnia (pl. a modell csak eseményekkel kommunikálhat a nézettel, a nézet nem végezheti az adatkezelést).
 - A modell működését egységtesztek segítségével kell ellenőrizni. Nem kell teljes körű tesztet végezni, azonban a lényeges funkciókat, és azok hatásait ellenőrizni kell. Az adatbetöltés/mentés teszteléséhez a perzisztencia működését szimulálni kell.
 - A program játékfelületét dinamikusan kell létrehozni futási időben. A megjelenítéshez lehet vezérlőket használni, vagy elemi grafikát. Egyes feladatoknál különböző méretű játéktábla létrehozását kell megvalósítani, ekkor ügyelni kell arra, hogy az ablakméret mindig alkalmazkodjon a játéktábla méretéhez.
 - A dokumentációnak jól áttekinthetőnek, megfelelően formázottnak kell lennie, tartalmaznia kell a fejlesztő adatait, a feladatleírást, a feladat elemzését, felhasználói eseteit (UML felhasználói esetek diagrammal), a program szerkezetének leírását (UML osztálydiagrammal), valamint a tesztesetek leírását. A dokumentáció ne tartalmazzon kódrészleteket, illetve képernyőképeket. A megjelenő diagramokat megfelelő szerkesztőeszköz segítségével kell előállítani. A dokumentációt elektronikusan, PDF formátumban kell leadni.

## Feladat:
### Maci Laci
Készítsünk programot, amellyel a következő játékot játszhatjuk:
 - Adott egy 𝑛 × 𝑛 elemből álló játékpálya, amelyben Maci Lacival kell piknikkosarakra vadásznunk. 
 - A játékpályán az egyszerű mezők mellett elhelyezkednek akadályok (pl. fa), valamint piknikkosarak. A játék célja, hogy a piknikkosarakat minél gyorsabban begyűjtsük.
 - Az erdőben vadőrök is járőröznek, akik adott időközönként lépnek egy mezőt (vízszintesen, vagy függőlegesen). A járőrözés során egy megadott irányba haladnak egészen addig, amíg akadályba (vagy az erdő szélébe) nem ütköznek, ekkor megfordulnak, és visszafelé haladnak (tehát folyamatosan egy vonalban járőröznek). A vadőr járőrözés közben a vele szomszédos mezőket látja (átlósan is, azaz egy 3 × 3-as négyzetet).
 - A játékos kezdetben a bal felső sarokban helyezkedik el, és vízszintesen, illetve függőlegesen mozoghat (egyesével) a pályán, a piknikkosárra való rálépéssel pedig felveheti azt. Ha Maci Lacit meglátja valamelyik vadőr, akkor a játékos veszít.
 - A pályák méretét, illetve felépítését (piknikkosarak, akadályok, vadőrök kezdőpozíciója) tároljuk fájlban. A program legalább 3 különböző méretű pályát tartalmazzon.
 - A program biztosítson lehetőséget új játék kezdésére a pálya kiválasztásával, valamint játék szüneteltetésére (ekkor nem telik az idő, és nem léphet a játékos).
 - Ismerje fel, ha vége a játéknak, és jelezze, győzött, vagy veszített a játékos.
 - A program játék közben folyamatosan jelezze ki a játékidőt, valamint a megszerzett piknikkosarak számát.