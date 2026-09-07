CREATE PROCEDURE sp_UcitajSvePutneNaloge
AS
BEGIN
    SELECT Id, Zaposleni, Destinacija, Dnevnica, BrojDana, UkupniTrosak FROM PutniNalozi;
END
GO

CREATE PROCEDURE sp_DodajPutniNalog
    @Zaposleni NVARCHAR(100),
    @Destinacija NVARCHAR(100),
    @Dnevnica DECIMAL(18,2),
    @BrojDana INT
AS
BEGIN
    INSERT INTO PutniNalozi (Zaposleni, Destinacija, Dnevnica, BrojDana, UkupniTrosak)
    VALUES (@Zaposleni, @Destinacija, @Dnevnica, @BrojDana, @Dnevnica * @BrojDana);
END
GO