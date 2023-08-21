-- phpMyAdmin SQL Dump
-- version 4.0.10.14
-- http://www.phpmyadmin.net
--
-- Host: localhost:3306
-- Generation Time: Sep 25, 2016 at 10:48 PM
-- Server version: 5.6.33
-- PHP Version: 5.6.20

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `sean_aspSamples`
--

-- --------------------------------------------------------

--
-- Table structure for table `tblCategory`
--

CREATE TABLE IF NOT EXISTS `tblCategory` (
  `categoryID` int(10) NOT NULL AUTO_INCREMENT,
  `categoryName` varchar(100) NOT NULL,
  PRIMARY KEY (`categoryID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `tblCategory`
--

INSERT INTO `tblCategory` (`categoryID`, `categoryName`) VALUES
(2, 'Windows Programming'),
(3, 'Networking'),
(5, 'Web Programming'),
(6, 'Hardware'),
(8, 'Animation');

-- --------------------------------------------------------

--
-- Table structure for table `tblGrades`
--

CREATE TABLE IF NOT EXISTS `tblGrades` (
  `gradeID` int(10) NOT NULL AUTO_INCREMENT,
  `categoryID` int(10) NOT NULL,
  `courseName` varchar(100) NOT NULL,
  `courseDescription` text NOT NULL,
  `grade` varchar(5) NOT NULL,
  `comments` text NOT NULL,
  PRIMARY KEY (`gradeID`),
  KEY `categoryID` (`categoryID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Dumping data for table `tblGrades`
--

INSERT INTO `tblGrades` (`gradeID`, `categoryID`, `courseName`, `courseDescription`, `grade`, `comments`) VALUES
(1, 2, 'Introduction to Programming : Python', 'Learn all about Python 3', '87', 'What do you mean I need to indent?'),
(2, 8, 'Introduction to Classical Animation', 'Make stuff move through digital and classic animating techniques', '78', 'Right brain stuff'),
(3, 3, 'Internetworking I', 'Cisco stuff', '67', ''),
(4, 3, 'Internetworking II', 'More Cisco stuff...', '78', ''),
(5, 2, 'Object Oriented Programming', 'Learn how to model real world objects inside your application design', '89', ''),
(6, 5, 'Web Programming with ASP.NET Core', 'Develop cool web applications using Microsoft''s server sided technology', '97', 'MVC in full force!');

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tblGrades`
--
ALTER TABLE `tblGrades`
  ADD CONSTRAINT `tblGrades_ibfk_1` FOREIGN KEY (`categoryID`) REFERENCES `tblCategory` (`categoryID`) ON DELETE CASCADE ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
