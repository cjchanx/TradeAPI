-- phpMyAdmin SQL Dump
-- version 4.9.5deb2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Dec 23, 2021 at 03:11 AM
-- Server version: 8.0.27-0ubuntu0.20.04.1
-- PHP Version: 7.4.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `TradeDB`
--

-- --------------------------------------------------------

--
-- Table structure for table `Accounts`
--

CREATE TABLE `Accounts` (
  `Id` int NOT NULL,
  `Active` bit(1) NOT NULL DEFAULT b'1',
  `Broker` varchar(25) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `Name` varchar(25) NOT NULL,
  `Description` varchar(100) NOT NULL,
  `Password` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `Account_Summary`
--

CREATE TABLE `Account_Summary` (
  `AccountRef` int NOT NULL,
  `AvailableFunds` double NOT NULL DEFAULT '0',
  `GrossPositionValue` double NOT NULL DEFAULT '0',
  `NetLiquidation` double NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `Brokers`
--

CREATE TABLE `Brokers` (
  `Name` varchar(25) NOT NULL,
  `Website` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `Commissions`
--

CREATE TABLE `Commissions` (
  `BrokerName` varchar(25) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `OrderType` int NOT NULL,
  `Rate` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Orders`
--

CREATE TABLE `Orders` (
  `Id` int NOT NULL,
  `AccountRef` int NOT NULL,
  `Action` int NOT NULL,
  `TargetPrice` double NOT NULL,
  `DateCreated` datetime NOT NULL,
  `Quantity` int NOT NULL DEFAULT '0',
  `Status` int NOT NULL DEFAULT '1',
  `Symbol` varchar(20) NOT NULL,
  `BrokerName` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `OwnedSecurity`
--

CREATE TABLE `OwnedSecurity` (
  `AccountRef` int NOT NULL,
  `Symbol` varchar(5) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Quantity` int NOT NULL,
  `AveragePrice` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `Security`
--

CREATE TABLE `Security` (
  `Symbol` varchar(5) NOT NULL,
  `Description` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Price` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Table structure for table `Transactions`
--

CREATE TABLE `Transactions` (
  `Id` int NOT NULL,
  `AccountRef` int NOT NULL,
  `Action` int NOT NULL,
  `AveragePrice` double NOT NULL,
  `Commission` double NOT NULL,
  `DateCreated` datetime NOT NULL,
  `Price` double NOT NULL,
  `Quantity` int NOT NULL,
  `RealizedPNL` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Accounts`
--
ALTER TABLE `Accounts`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `AK_Accounts_Id` (`Id`),
  ADD UNIQUE KEY `Name` (`Name`),
  ADD KEY `Broker` (`Broker`);

--
-- Indexes for table `Account_Summary`
--
ALTER TABLE `Account_Summary`
  ADD PRIMARY KEY (`AccountRef`),
  ADD KEY `FK_Account_Summary_To_Accounts` (`AccountRef`);

--
-- Indexes for table `Brokers`
--
ALTER TABLE `Brokers`
  ADD PRIMARY KEY (`Name`);

--
-- Indexes for table `Commissions`
--
ALTER TABLE `Commissions`
  ADD PRIMARY KEY (`BrokerName`,`OrderType`);

--
-- Indexes for table `Orders`
--
ALTER TABLE `Orders`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `OrderId` (`Id`),
  ADD KEY `Security` (`Symbol`),
  ADD KEY `Accounts` (`AccountRef`),
  ADD KEY `Broker_Reference` (`BrokerName`);

--
-- Indexes for table `OwnedSecurity`
--
ALTER TABLE `OwnedSecurity`
  ADD PRIMARY KEY (`AccountRef`,`Symbol`),
  ADD KEY `AccountRef` (`AccountRef`,`Symbol`),
  ADD KEY `Symbol` (`Symbol`);

--
-- Indexes for table `Security`
--
ALTER TABLE `Security`
  ADD PRIMARY KEY (`Symbol`),
  ADD UNIQUE KEY `AK_Security_Types_Type` (`Symbol`);

--
-- Indexes for table `Transactions`
--
ALTER TABLE `Transactions`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `FK_Transaction_To_Accounts` (`AccountRef`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `Accounts`
--
ALTER TABLE `Accounts`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `Orders`
--
ALTER TABLE `Orders`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `Transactions`
--
ALTER TABLE `Transactions`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `Accounts`
--
ALTER TABLE `Accounts`
  ADD CONSTRAINT `Accounts_ibfk_1` FOREIGN KEY (`Broker`) REFERENCES `Brokers` (`Name`);

--
-- Constraints for table `Account_Summary`
--
ALTER TABLE `Account_Summary`
  ADD CONSTRAINT `FK_Account_Summary_To_Accounts` FOREIGN KEY (`AccountRef`) REFERENCES `Accounts` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT;

--
-- Constraints for table `Commissions`
--
ALTER TABLE `Commissions`
  ADD CONSTRAINT `FK_BROKER_NAME` FOREIGN KEY (`BrokerName`) REFERENCES `Brokers` (`Name`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Constraints for table `Orders`
--
ALTER TABLE `Orders`
  ADD CONSTRAINT `Account_Reference` FOREIGN KEY (`AccountRef`) REFERENCES `Accounts` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  ADD CONSTRAINT `Broker_Reference` FOREIGN KEY (`BrokerName`) REFERENCES `Brokers` (`Name`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Security_Reference` FOREIGN KEY (`Symbol`) REFERENCES `Security` (`Symbol`);

--
-- Constraints for table `OwnedSecurity`
--
ALTER TABLE `OwnedSecurity`
  ADD CONSTRAINT `OwnedSecurity_ibfk_1` FOREIGN KEY (`AccountRef`) REFERENCES `Accounts` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  ADD CONSTRAINT `SECURITY_FK` FOREIGN KEY (`Symbol`) REFERENCES `Security` (`Symbol`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Constraints for table `Transactions`
--
ALTER TABLE `Transactions`
  ADD CONSTRAINT `FK_Transaction_To_Accounts` FOREIGN KEY (`AccountRef`) REFERENCES `Accounts` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
