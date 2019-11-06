-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`Povlastice`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Povlastice` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Povlastice` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `naziv` VARCHAR(45) NULL,
  `procenat` DECIMAL NULL,
  `detalji` VARCHAR(450) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Putnik`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Putnik` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Putnik` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `ime` VARCHAR(45) NOT NULL,
  `prezime` VARCHAR(45) NOT NULL,
  `broj_pasosa` VARCHAR(45) NOT NULL,
  `pol` TINYINT NULL,
  `datum_rodjenja` DATE NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Kompanija`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Kompanija` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Kompanija` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `naziv` VARCHAR(45) NOT NULL,
  `oznaka` VARCHAR(45) NOT NULL,
  `sjediste` VARCHAR(45) NOT NULL,
  `logo` VARCHAR(1000) NULL,
  `ocjena` INT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Sluzbenik`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Sluzbenik` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Sluzbenik` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `Kompanija_id` INT NOT NULL,
  `ime` VARCHAR(45) NOT NULL,
  `prezime` VARCHAR(45) NOT NULL,
  `radno_mjesto` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_sluzbenik_Avio-kompanija1_idx` (`Kompanija_id` ASC),
  CONSTRAINT `fk_sluzbenik_Avio-kompanija1`
    FOREIGN KEY (`Kompanija_id`)
    REFERENCES `mydb`.`Kompanija` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Aerodrom`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Aerodrom` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Aerodrom` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `naziv` VARCHAR(45) NOT NULL,
  `grad` VARCHAR(45) NOT NULL,
  `drzava` VARCHAR(45) NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Destinacija`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Destinacija` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Destinacija` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `Aerodrom_id` INT NOT NULL,
  `grad` VARCHAR(45) NOT NULL,
  `drzava` VARCHAR(45) NULL,
  `img` VARCHAR(3000) NULL,
  `opis` VARCHAR(300) NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Destinacija_Aerodrom1_idx` (`Aerodrom_id` ASC),
  CONSTRAINT `fk_Destinacija_Aerodrom1`
    FOREIGN KEY (`Aerodrom_id`)
    REFERENCES `mydb`.`Aerodrom` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Terminal`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Terminal` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Terminal` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `Aerodrom_id` INT NOT NULL,
  `naziv` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Terminal_Aerodrom1_idx` (`Aerodrom_id` ASC),
  CONSTRAINT `fk_Terminal_Aerodrom1`
    FOREIGN KEY (`Aerodrom_id`)
    REFERENCES `mydb`.`Aerodrom` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Let`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Let` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Let` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `Destinacija_id` INT NOT NULL,
  `Terminal_id` INT NOT NULL,
  `Kompanija_id` INT NOT NULL,
  `datum_polaska` DATE NULL,
  `broj_mjesta` INT NULL,
  `broj_leta` INT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Let_Destinacija1_idx` (`Destinacija_id` ASC),
  INDEX `fk_Let_Terminal1_idx` (`Terminal_id` ASC),
  INDEX `fk_Let_Avio-kompanija1_idx` (`Kompanija_id` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Karta`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Karta` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Karta` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `Putnik_id` INT NOT NULL,
  `Let_id` INT NOT NULL,
  `Sluzbenik_id` INT NOT NULL,
  `broj_sjedista` VARCHAR(45) NOT NULL,
  `datum_prodaje` DATE NULL,
  `cijena` DECIMAL NULL,
  `popust` DECIMAL NULL,
  `storn` INT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Karta_Putnik1_idx` (`Putnik_id` ASC),
  INDEX `fk_Karta_sluzbenik1_idx` (`Sluzbenik_id` ASC),
  INDEX `fk_Karta_Let1_idx` (`Let_id` ASC),
  CONSTRAINT `fk_Karta_Putnik1`
    FOREIGN KEY (`Putnik_id`)
    REFERENCES `mydb`.`Putnik` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Karta_sluzbenik1`
    FOREIGN KEY (`Sluzbenik_id`)
    REFERENCES `mydb`.`Sluzbenik` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Karta_Let1`
    FOREIGN KEY (`Let_id`)
    REFERENCES `mydb`.`Let` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Rezervacija`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Rezervacija` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Rezervacija` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `Let_id` INT NOT NULL,
  `Karta_id` INT NOT NULL,
  `datum_rezervacije` DATE NOT NULL,
  `vazenje_rezervacije` DATE NOT NULL,
  `storn` INT NOT NULL DEFAULT 0,
  `realizovana` INT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Rezervacija_Let1_idx` (`Let_id` ASC),
  INDEX `fk_Rezervacija_Karta1_idx` (`Karta_id` ASC),
  CONSTRAINT `fk_Rezervacija_Let1`
    FOREIGN KEY (`Let_id`)
    REFERENCES `mydb`.`Let` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Rezervacija_Karta1`
    FOREIGN KEY (`Karta_id`)
    REFERENCES `mydb`.`Karta` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Putnik_povlastice`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Putnik_povlastice` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Putnik_povlastice` (
  `Putnik_id` INT NOT NULL,
  `Povlastice_id` INT NOT NULL,
  PRIMARY KEY (`Putnik_id`, `Povlastice_id`),
  INDEX `fk_Putnik_has_povlastice_povlastice1_idx` (`Povlastice_id` ASC),
  INDEX `fk_Putnik_has_povlastice_Putnik1_idx` (`Putnik_id` ASC),
  CONSTRAINT `fk_Putnik_has_povlastice_Putnik1`
    FOREIGN KEY (`Putnik_id`)
    REFERENCES `mydb`.`Putnik` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Putnik_has_povlastice_povlastice1`
    FOREIGN KEY (`Povlastice_id`)
    REFERENCES `mydb`.`Povlastice` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
