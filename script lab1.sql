-- Disable foreign key checks
USE persona_db
GO

-- -----------------------------------------------------
-- Table persona_db.persona
-- -----------------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[persona]') AND type in (N'U'))
BEGIN
    CREATE TABLE persona (
        cc INT NOT NULL PRIMARY KEY,
        nombre VARCHAR(45) NOT NULL,
        apellido VARCHAR(45) NOT NULL,
        genero CHAR(1) NOT NULL CHECK (genero IN ('M', 'F')),
        edad INT NULL
    )
END
GO

-- -----------------------------------------------------
-- Table persona_db.profesion
-- -----------------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[profesion]') AND type in (N'U'))
BEGIN
    CREATE TABLE profesion (
        id INT NOT NULL PRIMARY KEY,
        nom VARCHAR(90) NOT NULL,
        des TEXT NULL
    )
END
GO

-- -----------------------------------------------------
-- Table persona_db.estudios
-- -----------------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[estudios]') AND type in (N'U'))
BEGIN
    CREATE TABLE estudios (
        id_prof INT NOT NULL,
        cc_per INT NOT NULL,
        fecha DATE NULL,
        univer VARCHAR(50) NULL,
        PRIMARY KEY (id_prof, cc_per),
        CONSTRAINT estudio_persona_fk FOREIGN KEY (cc_per) REFERENCES persona (cc),
        CONSTRAINT estudio_profesion_fk FOREIGN KEY (id_prof) REFERENCES profesion (id)
    )
END
GO

-- -----------------------------------------------------
-- Table persona_db.telefono
-- -----------------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[telefono]') AND type in (N'U'))
BEGIN
    CREATE TABLE telefono (
        num VARCHAR(15) NOT NULL PRIMARY KEY,
        oper VARCHAR(45) NOT NULL,
        duenio INT NOT NULL,
        CONSTRAINT telefono_persona_fk FOREIGN KEY (duenio) REFERENCES persona (cc)
    )
END
GO

-- Enable foreign key checks
EXEC sp_MSforeachtable 'ALTER TABLE ? CHECK CONSTRAINT ALL'

--------------------------------------------------------

-- Insertar datos de prueba en la tabla persona
INSERT INTO persona (cc, nombre, apellido, genero, edad) VALUES
(1, 'Juan', 'Perez', 'M', 30),
(2, 'Maria', 'Lopez', 'F', 25),
(3, 'Pedro', 'Gonzalez', 'M', 35);

-- Insertar datos de prueba en la tabla profesion
INSERT INTO profesion (id, nom, des) VALUES
(1, 'Ingeniero', 'Ingeniero de Sistemas'),
(2, 'Médico', 'Especialista en Pediatría'),
(3, 'Abogado', 'Derecho Civil');

-- Insertar datos de prueba en la tabla estudios
INSERT INTO estudios (id_prof, cc_per, fecha, univer) VALUES
(1, 1, '2010-01-01', 'Universidad Nacional'),
(1, 2, '2012-01-01', 'Universidad Autónoma'),
(2, 3, '2008-01-01', 'Universidad de la Ciudad');

-- Insertar datos de prueba en la tabla telefono
INSERT INTO telefono (num, oper, duenio) VALUES
('123456789', 'Movistar', 1),
('987654321', 'Claro', 2),
('555555555', 'Tigo', 3);
