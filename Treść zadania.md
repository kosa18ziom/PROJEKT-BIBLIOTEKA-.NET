# Projekt-Biblioteka-.NET
Zadanie 1 za 25 pkt.
Napisać aplikację internetową wspomagającą pracę biblioteki. W aplikacji powinny zostać utworzone dwie role: administrator, pracownik oraz czytelnik. Aplikacja powinna udostępniać następujące funkcje:
a) Rejestrację oraz logowanie użytkownika w serwisie. Aplikacja powinna wykorzystywać mechanizm uwierzytelniania formularzy. Użytkownik powinien mieć możliwość zarejestrowania się (podania danych potrzebnych do założenia konta). Konto jest aktywowane przez administratora, który przypisuje je do odpowiedniej roli: pracownika lub czytelnika. Administrator ma również możliwość usuwania kont użytkowników z systemu.
b) Zarządzania zasobami książkowymi biblioteki. Dodawanie nowych pozycji, kasowanie pozycji uszkodzonych. Funkcje te powinien obsługiwać użytkownik przypisany do roli – pracownik.
c) Wypożyczenie/zwrot książki może zrealizować użytkownik zalogowany. Wypożyczenie oraz zwrot książki powinny być zatwierdzone przez użytkownika o roli - pracownik, co automatycznie powinno zmienić pole informujące o liczbie dostępnych egzemplarzy danej książki. Opcja powinna umożliwiać określenie okresu, na który książka jest wypożyczona, po tym okresie naliczana jest umowna kara za każdy dzień zwłoki
d) Rezerwację książki. Rezerwacji może dokonać użytkownik zalogowany. W momencie, gdy liczba dostępnych egzemplarzy zwiększy się użytkownik może książkę wypożyczyć, status zmienia się z rezerwacji na wypożyczenie. Rezerwacja może być anulowana przez użytkownika, który jej dokonał.
e) Wyszukiwania książki w zasobach. Wyszukiwanie powinno być realizowane po nazwisku autora lub tytule. Wyszukiwanie powinno być dostępne dla wszystkich użytkowników (również niezalogowanych).
f) Wyświetlanie informacji o historii wypożyczeni oraz aktualnym stanie konta użytkownika. Informacje może wyświetlać właściciel konta, pracownik oraz administrator.
g) Modyfikację danych osobowych konta użytkownika. Zmiany przeprowadzane mogą być przez właściciela konta. Zmiany są zatwierdzane przez administratora.
Dane powinny być przechowywane w bazie danych. Do uwierzytelniania należy wykorzystać mechanizm formularzy. Projekt powinien być zbudowany w .NET 4.5 lub wyższym. Nie należy używać wzorca pustego projektu – powinien bazować na MasterPage.
Punktacja za poszczególne elementy:
1. Za zrealizowanie punktu a) można otrzymać 3 punkty.
2. Za zrealizowanie punktu b) można otrzymać 3 punkty.
3. Za zrealizowanie punktu c) można otrzymać 3 punkty.
4. Za zrealizowanie punktu d) można otrzymać 4 punkty.
5. Za zrealizowanie punktu e) można otrzymać 4 punkty.
6. Za zrealizowanie punktu f) można otrzymać 3 punkty.
7. Za zrealizowanie punktu g) można otrzymać 2 punkty.
8. Za wygląd interfejsu (użycie CSS, motywów) oraz czytelność kodu (w tym użycie komentarzy) 3 punkty.
Wszystkie dane powinny być przechowywane w jednej bazie danych dołączonej lokalnie do projektu.
