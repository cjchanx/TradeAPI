﻿CREATE TABLE Accounts
(
	`Id` TINYINT NOT NULL AUTO_INCREMENT, 
	`Active` BIT NOT NULL DEFAULT 1, 
	`Broker` VARCHAR(25) NOT NULL,
    `DateCreated` DATE NOT NULL,
	`Name` VARCHAR(25) NOT NULL,
	`Description` VARCHAR(100) NOT NULL, 
    CONSTRAINT `AK_Accounts_Id` UNIQUE (`Id`),
	PRIMARY KEY(`Id`)
);