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
-- Table `mydb`.`povlastice`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`povlastice` ;

CREATE TABLE IF NOT EXISTS `mydb`.`povlastice` (
  `id` INT NOT NULL,
  `naziv` VARCHAR(45) NULL,
  `procenat` DECIMAL NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Putnik`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Putnik` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Putnik` (
  `id` INT NOT NULL,
  `ime` VARCHAR(45) NULL,
  `prezime` VARCHAR(45) NOT NULL,
  `broj_pasosa` VARCHAR(45) NOT NULL,
  `pol` TINYINT NULL,
  `datum_rodjenja` DATETIME NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Avio-kompanija`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Avio-kompanija` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Avio-kompanija` (
  `id` INT NOT NULL,
  `naziv` VARCHAR(45) NOT NULL,
  `oznaka` VARCHAR(45) NOT NULL,
  `sjediste` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`sluzbenik`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`sluzbenik` ;

CREATE TABLE IF NOT EXISTS `mydb`.`sluzbenik` (
  `id` INT NOT NULL,
  `ime` VARCHAR(45) NOT NULL,
  `prezime` VARCHAR(45) NOT NULL,
  `radno_mjesto` VARCHAR(45) NULL,
  `Avio-kompanija_id` INT NOT NULL,
  PRIMARY KEY (`id`, `Avio-kompanija_id`),
  INDEX `fk_sluzbenik_Avio-kompanija1_idx` (`Avio-kompanija_id` ASC),
  CONSTRAINT `fk_sluzbenik_Avio-kompanija1`
    FOREIGN KEY (`Avio-kompanija_id`)
    REFERENCES `mydb`.`Avio-kompanija` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`putnik_povlastice`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`putnik_povlastice` ;

CREATE TABLE IF NOT EXISTS `mydb`.`putnik_povlastice` (
  `Putnik_id` INT NOT NULL,
  `povlastice_id` INT NOT NULL,
  PRIMARY KEY (`Putnik_id`, `povlastice_id`),
  INDEX `fk_putnik_povlastice_povlastice1_idx` (`povlastice_id` ASC),
  CONSTRAINT `fk_putnik_povlastice_Putnik`
    FOREIGN KEY (`Putnik_id`)
    REFERENCES `mydb`.`Putnik` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_putnik_povlastice_povlastice1`
    FOREIGN KEY (`povlastice_id`)
    REFERENCES `mydb`.`povlastice` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Aerodrom`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Aerodrom` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Aerodrom` (
  `id` INT NOT NULL,
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
  `id` INT NOT NULL,
  `grad` VARCHAR(45) NOT NULL,
  `drzava` VARCHAR(45) NULL,
  `Aerodrom_id` INT NOT NULL,
  PRIMARY KEY (`id`, `Aerodrom_id`),
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
  `id` INT NOT NULL,
  `naziv` VARCHAR(45) NULL,
  `Aerodrom_id` INT NOT NULL,
  PRIMARY KEY (`id`, `Aerodrom_id`),
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
  `id` INT NOT NULL,
  `datum_polaska` DATETIME NULL,
  `broj_mjesta` INT NULL,
  `broj_leta` INT NULL,
  `Destinacija_id` INT NOT NULL,
  `Terminal_id` INT NOT NULL,
  `Avio-kompanija_id` INT NOT NULL,
  PRIMARY KEY (`id`, `Destinacija_id`, `Terminal_id`, `Avio-kompanija_id`),
  INDEX `fk_Let_Destinacija1_idx` (`Destinacija_id` ASC),
  INDEX `fk_Let_Terminal1_idx` (`Terminal_id` ASC),
  INDEX `fk_Let_Avio-kompanija1_idx` (`Avio-kompanija_id` ASC))
ENGINE = EXAMPLE;


-- -----------------------------------------------------
-- Table `mydb`.`Karta`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`Karta` ;

CREATE TABLE IF NOT EXISTS `mydb`.`Karta` (
  `id` INT NOT NULL,
  `broj_sjedista` VARCHAR(45) NOT NULL,
  `datum_prodaje` DATETIME NULL,
  `cijena` DECIMAL NULL,
  `popust` DECIMAL NULL,
  `storn` INT NULL,
  `Putnik_id` INT NOT NULL,
  `sluzbenik_id` INT NOT NULL,
  `Let_id` INT NOT NULL,
  PRIMARY KEY (`id`, `Putnik_id`, `sluzbenik_id`, `Let_id`),
  INDEX `fk_Karta_Putnik1_idx` (`Putnik_id` ASC),
  INDEX `fk_Karta_sluzbenik1_idx` (`sluzbenik_id` ASC),
  INDEX `fk_Karta_Let1_idx` (`Let_id` ASC),
  CONSTRAINT `fk_Karta_Putnik1`
    FOREIGN KEY (`Putnik_id`)
    REFERENCES `mydb`.`Putnik` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Karta_sluzbenik1`
    FOREIGN KEY (`sluzbenik_id`)
    REFERENCES `mydb`.`sluzbenik` (`id`)
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
  `id` INT NOT NULL,
  `datum_rezervacije` DATETIME NOT NULL,
  `vazenje_rezervacije` DATETIME NOT NULL,
  `storn` INT NOT NULL DEFAULT 0,
  `realizovana` INT NULL,
  `Let_id` INT NOT NULL,
  `Karta_id` INT NOT NULL,
  PRIMARY KEY (`id`, `Karta_id`),
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


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
