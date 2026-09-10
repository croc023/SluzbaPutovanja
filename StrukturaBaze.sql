CREATE DATABASE SluzbenaPutovanjaDB;
GO

USE SluzbenaPutovanjaDB;
GO

CREATE TABLE Zaposleni (
    ZaposleniId INT PRIMARY KEY IDENTITY(1,1),
    Ime NVARCHAR(50) NOT NULL,
    Prezime NVARCHAR(50) NOT NULL,
    Pozicija NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);

CREATE TABLE ZahteviZaPutovanje (
    BrojZahteva INT PRIMARY KEY IDENTITY(1,1),
    Oznaka NVARCHAR(20) NOT NULL,
    ZaposleniId INT NOT NULL,
    Destinacija NVARCHAR(100) NOT NULL,
    DatumPodnosenja DATETIME NOT NULL,
    Status NVARCHAR(30) NOT NULL,
    UkupniTrosak DECIMAL(18,2) NOT NULL DEFAULT 0,
    CONSTRAINT FK_Zahtevi_Zaposleni FOREIGN KEY (ZaposleniId) REFERENCES Zaposleni(ZaposleniId)
);

CREATE TABLE StavkaZahteva (
    StavkaId INT PRIMARY KEY IDENTITY(1,1),
    BrojZahteva INT NOT NULL,
    Opis NVARCHAR(200) NOT NULL,
    Iznos DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Stavka_Zahtev FOREIGN KEY (BrojZahteva) REFERENCES ZahteviZaPutovanje(BrojZahteva) ON DELETE CASCADE
);

INSERT INTO Zaposleni (Ime, Prezime, Pozicija, Email) VALUES 
('Marko', 'Marković', 'Developer', 'marko@test.com'),
('Nikola', 'Nikolić', 'Menadžer', 'nikola@test.com');