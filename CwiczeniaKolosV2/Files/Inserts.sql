/* Do pierwszej końcówki */
INSERT INTO ClientCategory
VALUES ('Rich', 5)

SELECT *
FROM ClientCategory

INSERT INTO Client
VALUES ('Jan', 'Kowalski', '2003-12-12', 00292516932, 'jan@kowalski.pl', 1)

INSERT INTO Client
VALUES ('Anna', 'Kowalska', '1959-12-12', 59292516932, 'anna@kowalska.pl', 1)

SELECT *
FROM Client

INSERT INTO BoatStandard
VALUES ('A-Class', 10)

SELECT *
FROM BoatStandard

INSERT INTO Reservation
VALUES (1, '2024-02-05', '2024-02-10', 1, 20, 1, 0, 2500, null)

SELECT *
FROM Reservation

/* Do drugiej końcówki */
INSERT INTO Sailboat
VALUES ('Sally', 20, 'Luxurious yacht', 1, 5000);

SELECT *
FROM Sailboat

SELECT *
FROM Sailboat_Reservation

