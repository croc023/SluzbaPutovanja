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

CREATE TABLE Korisnici (
    KorisnikID INT PRIMARY KEY IDENTITY(1,1),
    KorisnickoIme NVARCHAR(50) NOT NULL,
    Lozinka NVARCHAR(100) NOT NULL,
    Uloga NVARCHAR(30) NOT NULL, -- Npr. 'Direktor', 'Admin', 'Zaposleni'
    ZaposleniId INT NULL,        -- Opciono vezivanje za tabelu Zaposleni
    CONSTRAINT FK_Korisnici_Zaposleni FOREIGN KEY (ZaposleniId) REFERENCES Zaposleni(ZaposleniId)
);

INSERT INTO Zaposleni (Ime, Prezime, Pozicija, Email) VALUES 
('Marko', 'Marković', 'Developer', 'marko@test.com'),
('Nikola', 'Nikolić', 'Menadžer', 'nikola@test.com');

INSERT INTO Korisnici (KorisnickoIme, Lozinka, Uloga, ZaposleniId) VALUES 
('admin', 'admin123', 'Administrator', NULL),
('direktor_marko', 'lozinka123', 'Direktor', 1),
('menadzer_nikola', 'lozinka123', 'Direktor', 2);