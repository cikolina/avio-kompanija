-- MySQL Workbench Synchronization
-- Generated: 2019-10-24 14:33
-- Model: New Model
-- Version: 1.0
-- Project: Name of the project
-- Author: Aco

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

ALTER TABLE `mydb`.`povlastice` 
CHANGE COLUMN `procenat` `procenat` DECIMAL NULL DEFAULT NULL ;

ALTER TABLE `mydb`.`sluzbenik` 
DROP COLUMN `kompanija_id`,
ADD COLUMN `kompanija_id` INT(11) NOT NULL AFTER `radno_mjesto`,
DROP PRIMARY KEY,
ADD PRIMARY KEY (`id`, `kompanija_id`),
DROP INDEX `fk_sluzbenik_Avio-kompanija1_idx` ,
ADD INDEX `fk_sluzbenik_Avio-kompanija1_idx` (`kompanija_id` ASC);
;

ALTER TABLE `mydb`.`Let` 
ENGINE = InnoDB ,
CHANGE COLUMN `Avio-kompanija_id` `kompanija_id` INT(11) NOT NULL ;

ALTER TABLE `mydb`.`Karta` 
CHANGE COLUMN `cijena` `cijena` DECIMAL NULL DEFAULT NULL ,
CHANGE COLUMN `popust` `popust` DECIMAL NULL DEFAULT NULL ;

ALTER TABLE `mydb`.`sluzbenik` 
ADD CONSTRAINT `fk_sluzbenik_Avio-kompanija1`
  FOREIGN KEY (`kompanija_id`)
  REFERENCES `mydb`.`kompanija` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
