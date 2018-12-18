-- MySQL dump 10.13  Distrib 5.7.24, for Linux (x86_64)
--
-- Host: localhost    Database: Project
-- ------------------------------------------------------
-- Server version	5.7.24-0ubuntu0.18.04.1-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `CardFusions`
--

DROP TABLE IF EXISTS `CardFusions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `CardFusions` (
  `base` varchar(30) NOT NULL,
  `spice` varchar(30) NOT NULL,
  `valueReq` int(11) NOT NULL,
  `product` varchar(30) NOT NULL,
  PRIMARY KEY (`base`,`spice`,`valueReq`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CardFusions`
--

LOCK TABLES `CardFusions` WRITE;
/*!40000 ALTER TABLE `CardFusions` DISABLE KEYS */;
INSERT INTO `CardFusions` VALUES ('Almonds','Water',0,'Activated Almonds'),('Any','Feed',5,'Chicken'),('Any','Onion',5,'Soup'),('Any','Onion',7,'Onion Rings'),('Any','Tomato Sauce',7,'Soup'),('Chicken','Horseradish',0,'Chickenhorse'),('Corn','Cheese',0,'Taco'),('Corn','Hawt Sauce',0,'Popcorn'),('Corn','Onion',0,'Taco'),('Corn','Salt and Pepper',0,'Popcorn'),('Corn','Water',0,'Soup'),('Dough','Maple Syrup',0,'Pancake'),('Dough','Sugar',0,'Scrumptious Cake'),('Dough','Tomato Sauce',0,'Pizza'),('Egg','Feed',0,'Chicken'),('Egg','Hawt Sauce',0,'Hot Wings'),('Egg','Horseradish',0,'Omelette'),('Egg','Maple Syrup',0,'French Toast'),('Egg','Onion',0,'Omelette'),('Egg','Salt and Pepper',0,'Omelette'),('Egg','Sugar',0,'Sugar Glider'),('Egg','Water',0,'Crab'),('Flour','Cheese',0,'Taco'),('Flour','Maple Syrup',0,'Waffle'),('Flour','Sugar',0,'Scrumptious Cake'),('Flour','Tomato Sauce',0,'Pasta'),('Fries','Feed',0,'Tenders and Fries'),('Hot Wings','Hawt Sauce',0,'Bird.obj'),('Hotdog','Water',0,'Corndog of Disappointment'),('Mystery Meat','Onion',0,'Hotdog'),('Mystery Meat','Salt and Pepper',0,'Ribs'),('Mystery Meat','Tomato Sauce',0,'Hotdog'),('Pancakes','Maple Syrup',0,'Foreclosed Pancake House'),('Pasta','Sauce',0,'Holy Moly Stromboli!'),('Pizza','Sauce',0,'Holy Moly Stromboli!'),('Potato','Garlic',0,'Loaded Baked Potato'),('Potato','Hawt Sauce',0,'Loaded Baked Potato'),('Potato','Onion',0,'Loaded Baked Potato'),('Potato','Salt and Pepper',0,'Fries'),('Potato','Water',0,'Soup'),('Scrumptious Cake','Cheese',0,'Cheesecake'),('Taco','Cheese',0,'Triple Threat Taco'),('Taco','Onion',0,'Triple Threat Taco'),('Taco','Tomato Sauce',0,'Triple Threat Taco'),('Tofu','Water',0,'Almonds'),('Waffle','Maple Syrup',0,'Kidz Bop Cask');
/*!40000 ALTER TABLE `CardFusions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Cards`
--

DROP TABLE IF EXISTS `Cards`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Cards` (
  `ID` int(11) NOT NULL,
  `Name` varchar(30) DEFAULT NULL,
  `Type` varchar(12) DEFAULT NULL,
  `Attack` int(11) DEFAULT NULL,
  `Defense` int(11) DEFAULT NULL,
  `Value` int(11) DEFAULT NULL,
  `isFusable` tinyint(1) DEFAULT NULL,
  `HP` int(11) DEFAULT NULL,
  `Description` varchar(50) DEFAULT NULL,
  `ImageFilepath` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Cards`
--

LOCK TABLES `Cards` WRITE;
/*!40000 ALTER TABLE `Cards` DISABLE KEYS */;
INSERT INTO `Cards` VALUES (1,'Dough','Base',1,2,2,1,1,'Ol\' reliable for your baked goods','Sprites/CardImgs/dough'),(2,'Egg','Base',2,0,2,1,1,'The foundation of life and lunch','Sprites/CardImgs/egg'),(3,'Potato','Base',1,1,3,1,1,'Carbs for days','Sprites/CardImgs/potato'),(4,'Corn','Base',1,1,3,1,1,'Good luck not getting this stuck in your teeth','Sprites/CardImgs/corn'),(5,'Tofu','Base',0,3,1,1,1,'What even is tofu?','Sprites/CardImgs/tofu'),(6,'Mystery Meat','Base',2,2,2,1,1,'We made it and have no idea what it is','Sprites/CardImgs/mysterymeat'),(7,'Flour','Base',2,2,2,1,1,'Grainy','Sprites/CardImgs/flour'),(8,'Water','Spice',0,0,1,1,1,'Pure and refreshing to the core','Sprites/CardImgs/water'),(9,'Feed','Spice',0,0,2,1,0,'Non-GMO nutrients for your stock','Sprites/CardImgs/feed'),(10,'Maple Syrup','Spice',0,0,4,1,0,'Canada\'s greatest export','Sprites/CardImgs/maplesyrup'),(11,'Tomato Sauce','Spice',0,0,4,1,0,'Big Shaq\'s favorite','Sprites/CardImgs/tomatosauce'),(12,'Horseradish','Spice',0,0,3,1,0,'Whoever likes this is a freak','Sprites/CardImgs/horseradish'),(13,'Salt and Pepper','Spice',0,0,2,1,0,'Adding flavor to your life','Sprites/CardImgs/saltandpepper'),(14,'Onion','Spice',0,0,4,1,0,'Who\'s chopping onions?','Sprites/CardImgs/onion'),(15,'Garlic','Spice',0,0,3,1,0,'There\'s never enough garlic','Sprites/CardImgs/garlic'),(16,'Hawt Sauce','Spice',0,0,5,1,0,'The spiciest around','Sprites/CardImgs/hawtsauce'),(17,'Sugar','Spice',0,0,4,1,0,'Sweet and delicious','Sprites/CardImgs/sugar'),(18,'Cheese','Spice',0,0,3,1,0,'Legendary savory flavor-enhancer','Sprites/CardImgs/cheese'),(19,'Gruel','Monster',2,0,2,0,1,'...I guess this is food?','Sprites/CardImgs/gruel'),(20,'Plain Oatmeal','Monster',4,1,4,0,1,'The most boring food of em all','Sprites/CardImgs/plainoatmeal'),(21,'GDS Slop','Monster',6,2,6,0,1,'Just starve','Sprites/CardImgs/gdsslop'),(22,'Grub','Monster',7,2,7,0,1,'Garbage','Sprites/CardImgs/grub'),(23,'Big Grub','Monster',8,3,8,0,1,'Bigger garbage','Sprites/CardImgs/biggrub'),(24,'Feast','Monster',9,3,9,0,1,'That\'s a lot of food...','Sprites/CardImgs/feast'),(25,'Cauliflower Beast','Monster',10,5,10,0,1,'A conglameration of the most bland veggie there is','Sprites/CardImgs/cauliflowerbeast'),(26,'Omelette','Monster',5,3,6,1,2,'Customizable eggs perfect for brunch!','Sprites/CardImgs/omelette'),(27,'Soup','Monster',6,2,7,1,2,'*Slurp* Aaah, nice and warm.','Sprites/CardImgs/soup'),(28,'Taco','Monster',5,3,8,1,2,'Is it Taco Tuesday yet?','Sprites/CardImgs/taco'),(29,'Waffles','Monster',5,3,6,1,2,'\"\"and in the morning, I\'m making waffles\"\"','Sprites/CardImgs/waffles'),(30,'Chicken','Monster',7,4,8,1,2,'The greatest meat....fight me','Sprites/CardImgs/chicken'),(31,'Pasta','Monster',6,2,7,1,2,'The foundation of Italian food','Sprites/CardImgs/pasta'),(32,'Pizza','Monster',6,1,8,1,2,'Best comfort food around ','Sprites/CardImgs/pizza'),(33,'Crab','Monster',7,4,8,1,2,'Crabs...Plankton...Crabs...Plankton...Spongebob','Sprites/CardImgs/crab'),(34,'Onion Rings','Monster',8,3,8,1,2,'Heart-clogging!','Sprites/CardImgs/onionrings'),(35,'Fries','Monster',6,2,8,1,2,'Lean, salty and crispy','Sprites/CardImgs/fries'),(36,'Scrumptious Cake','Monster',5,4,7,1,2,'Is it my birthday yet?','Sprites/CardImgs/scrumptiouscake'),(37,'French Toast','Monster',7,3,7,1,2,'Yeah, we like breakfast foods, move along','Sprites/CardImgs/frenchtoast'),(38,'Loaded Baked Potato','Monster',7,2,8,1,2,'What toppings do you want in me?','Sprites/CardImgs/loadedbakedpotato'),(39,'Hot Wings','Monster',8,1,7,1,2,'America\'s favorite appetizer','Sprites/CardImgs/hotwings'),(40,'Popcorn','Monster',5,3,7,1,2,'Don\'t forget the butter','Sprites/CardImgs/popcorn'),(41,'Almonds','Monster',4,2,5,1,2,'The perfect snack','Sprites/CardImgs/almonds'),(42,'Ribs','Monster',7,1,8,1,2,'Messy but delicious ','Sprites/CardImgs/ribs'),(43,'Sugar Glider','Monster',8,2,9,1,2,'The tiny god of sweetness','Sprites/CardImgs/sugarglider'),(44,'Chickenhorse','Super Food',13,5,17,0,2,'It\'s a chicken and a horse. What\'s not to love?','Sprites/CardImgs/chickenhorse'),(45,'Bird.obj','Super Food',11,7,17,0,2,'It\'s a bird','Sprites/CardImgs/birdobj'),(46,'Activated Almonds','Super Food',10,7,15,0,2,'You just activated my almonds','Sprites/CardImgs/activatedalmonds'),(47,'Triple Threat Taco','Super Food',9,3,15,0,2,'Taco Tuesday!!!','Sprites/CardImgs/triplethreattaco'),(48,'Corndog of Disappointment','Super Food',1,0,10,0,2,'Oh dear','Sprites/CardImgs/corndogofdisappointment'),(49,'Kidz Bop Cask','Super Food',10,7,15,0,2,'How\'d I get stuck in here?','Sprites/CardImgs/KidzBopCask'),(50,'Holy Moly Stromboli!','Super Food',12,4,11,0,2,'A heavenly sandwich with all the meats','Sprites/CardImgs/holymolystromboli'),(51,'Tenders and Fries ','Super Food',13,4,12,0,2,'You can\'t go wrong with a classic','Sprites/CardImgs/tendersandfries'),(52,'Foreclosed Pancake House','Super Food',12,5,15,0,2,'We had something special going','Sprites/CardImgs/foreclosedpancakehouse'),(53,'Cheesecake','Super Food',12,5,14,0,2,'Who thought this was a good idea?','Sprites/CardImgs/cheesecake'),(54,'Pancake','Monster',4,4,6,1,2,'Are these cakes too THICC for you...?','Sprites/CardImgs/pancake'),(55,'Hotdog','Monster',7,3,6,1,2,'The old American frankfurter','Sprites/CardImgs/hotdog');
/*!40000 ALTER TABLE `Cards` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Shop`
--

DROP TABLE IF EXISTS `Shop`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Shop` (
  `ID` int(11) NOT NULL,
  `price` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Shop`
--

LOCK TABLES `Shop` WRITE;
/*!40000 ALTER TABLE `Shop` DISABLE KEYS */;
INSERT INTO `Shop` VALUES (1,50),(2,50),(3,50),(4,50),(5,50),(6,50),(7,50),(8,50),(9,50),(10,50),(11,50),(12,50),(13,50),(14,50),(15,50),(16,50),(17,50),(18,50);
/*!40000 ALTER TABLE `Shop` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `UserDeck`
--

DROP TABLE IF EXISTS `UserDeck`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `UserDeck` (
  `username` varchar(30) NOT NULL,
  `deckID` int(11) NOT NULL,
  `lru` int(11) DEFAULT NULL,
  `card0` int(11) DEFAULT NULL,
  `card1` int(11) DEFAULT NULL,
  `card2` int(11) DEFAULT NULL,
  `card3` int(11) DEFAULT NULL,
  `card4` int(11) DEFAULT NULL,
  `card5` int(11) DEFAULT NULL,
  `card6` int(11) DEFAULT NULL,
  `card7` int(11) DEFAULT NULL,
  `card8` int(11) DEFAULT NULL,
  `card9` int(11) DEFAULT NULL,
  `card10` int(11) DEFAULT NULL,
  `card11` int(11) DEFAULT NULL,
  `card12` int(11) DEFAULT NULL,
  `card13` int(11) DEFAULT NULL,
  `card14` int(11) DEFAULT NULL,
  `card15` int(11) DEFAULT NULL,
  `card16` int(11) DEFAULT NULL,
  `card17` int(11) DEFAULT NULL,
  `card18` int(11) DEFAULT NULL,
  `card19` int(11) DEFAULT NULL,
  `card20` int(11) DEFAULT NULL,
  `card21` int(11) DEFAULT NULL,
  `card22` int(11) DEFAULT NULL,
  `card23` int(11) DEFAULT NULL,
  `card24` int(11) DEFAULT NULL,
  `card25` int(11) DEFAULT NULL,
  `card26` int(11) DEFAULT NULL,
  `card27` int(11) DEFAULT NULL,
  `card28` int(11) DEFAULT NULL,
  `card29` int(11) DEFAULT NULL,
  PRIMARY KEY (`username`,`deckID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UserDeck`
--

LOCK TABLES `UserDeck` WRITE;
/*!40000 ALTER TABLE `UserDeck` DISABLE KEYS */;
INSERT INTO `UserDeck` VALUES ('b',0,2,1,2,6,2,7,11,12,16,18,17,9,6,4,8,12,1,6,11,2,18,7,1,8,11,10,13,11,16,10,11);
/*!40000 ALTER TABLE `UserDeck` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `UserHistory`
--

DROP TABLE IF EXISTS `UserHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `UserHistory` (
  `timestamp` datetime NOT NULL,
  `username` varchar(30) NOT NULL,
  `actionType` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`timestamp`,`username`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UserHistory`
--

LOCK TABLES `UserHistory` WRITE;
/*!40000 ALTER TABLE `UserHistory` DISABLE KEYS */;
INSERT INTO `UserHistory` VALUES ('2018-10-31 19:06:09','b','login'),('2018-10-31 19:07:32','b','login'),('2018-10-31 19:08:00','b','logout'),('2018-11-01 18:35:10','beeear','register'),('2018-11-01 18:35:27','beeear','login'),('2018-11-01 18:38:17','beeear','logout'),('2018-11-01 18:38:30','beeear','login'),('2018-11-01 18:51:35','b','login'),('2018-11-01 18:51:51','beeear','logout'),('2018-11-01 18:52:08','beeear','login'),('2018-11-01 18:52:25','b','logout'),('2018-11-01 18:52:39','b','login'),('2018-11-01 18:52:52','b','logout'),('2018-11-01 18:53:08','b','login'),('2018-11-01 18:54:22','b','logout'),('2018-11-01 18:54:44','b','login'),('2018-11-01 18:58:41','beeear','login'),('2018-11-01 19:00:28','beeear','logout'),('2018-11-01 19:00:42','b','login'),('2018-11-01 19:00:57','b','logout'),('2018-11-01 19:01:13','b','login'),('2018-11-01 19:02:15','b','login'),('2018-11-01 19:02:25','b','logout'),('2018-11-01 19:02:40','b','login'),('2018-11-01 19:02:51','b','logout'),('2018-11-01 19:02:57','b','login'),('2018-11-01 19:03:07','b','login'),('2018-11-01 19:03:55','b','login'),('2018-11-01 19:05:55','b','login'),('2018-11-01 19:06:13','beeear','login'),('2018-11-01 19:08:16','b','logout'),('2018-11-02 10:47:45','b','login'),('2018-11-02 10:50:14','b','logout'),('2018-11-02 10:51:11','testy','register'),('2018-11-02 10:57:20','b','login'),('2018-11-02 10:59:02','b','logout'),('2018-11-02 11:00:37','b','login'),('2018-11-02 11:01:18','b','logout'),('2018-11-02 11:03:02','b','login'),('2018-11-08 17:59:44','b','login'),('2018-11-08 18:01:18','b','login'),('2018-11-08 18:03:11','b','login'),('2018-11-08 18:09:49','b','login'),('2018-11-08 18:30:12','b','login'),('2018-11-08 18:31:06','b','logout'),('2018-11-08 18:31:23','b','login'),('2018-11-08 18:35:25','b','logout'),('2018-11-08 18:35:38','b','login'),('2018-11-08 18:41:12','','logout'),('2018-11-08 18:41:26','b','login'),('2018-11-08 18:42:10','b','logout'),('2018-11-08 18:42:24','b','login'),('2018-11-08 18:42:40','b','login'),('2018-11-08 18:46:04','b','logout'),('2018-11-08 18:46:17','b','login'),('2018-11-08 18:46:57','b','logout'),('2018-11-08 18:47:14','b','login'),('2018-11-08 18:54:29','b','logout'),('2018-11-08 18:54:39','b','login'),('2018-11-08 18:54:40','b','login'),('2018-11-08 18:56:40','b','login'),('2018-11-08 18:57:35','b','logout'),('2018-11-08 18:57:46','b','login'),('2018-11-08 18:58:22','b','logout'),('2018-11-08 18:58:35','b','login'),('2018-11-08 19:00:14','','logout'),('2018-11-08 19:00:24','b','login'),('2018-11-08 19:04:01','b','login'),('2018-11-08 19:04:02','b','logout'),('2018-11-08 19:04:25','b','login'),('2018-11-08 19:25:57','b','login'),('2018-11-08 19:26:55','b','login'),('2018-11-08 19:28:25','b','login'),('2018-11-08 19:32:25','b','logout'),('2018-11-08 19:33:33','b','login'),('2018-12-05 20:37:54','b','login'),('2018-12-07 16:55:02','b','login'),('2018-12-13 20:18:12','b','login'),('2018-12-13 20:45:05','b','login'),('2018-12-14 16:37:04','b','login');
/*!40000 ALTER TABLE `UserHistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `UserInventory`
--

DROP TABLE IF EXISTS `UserInventory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `UserInventory` (
  `username` varchar(30) NOT NULL,
  `numCard1` int(11) DEFAULT '0',
  `numCard2` int(11) DEFAULT '0',
  `numCard3` int(11) DEFAULT '0',
  `numCard4` int(11) DEFAULT '0',
  `numCard5` int(11) DEFAULT '0',
  `numCard6` int(11) DEFAULT '0',
  `numCard7` int(11) DEFAULT '0',
  `numCard8` int(11) DEFAULT '0',
  `numCard9` int(11) DEFAULT '0',
  `numCard10` int(11) DEFAULT '0',
  `numCard11` int(11) DEFAULT '0',
  `numCard12` int(11) DEFAULT '0',
  `numCard13` int(11) DEFAULT '0',
  `numCard14` int(11) DEFAULT '0',
  `numCard15` int(11) DEFAULT '0',
  `numCard16` int(11) DEFAULT '0',
  `numCard17` int(11) DEFAULT '0',
  `numCard18` int(11) DEFAULT '0',
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UserInventory`
--

LOCK TABLES `UserInventory` WRITE;
/*!40000 ALTER TABLE `UserInventory` DISABLE KEYS */;
INSERT INTO `UserInventory` VALUES ('b',3,4,0,1,31,4,2,2,1,2,5,2,1,0,0,2,1,2),('beeear',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0),('testy',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);
/*!40000 ALTER TABLE `UserInventory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `UserTransactions`
--

DROP TABLE IF EXISTS `UserTransactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `UserTransactions` (
  `timestamp` datetime NOT NULL,
  `username` varchar(30) NOT NULL,
  `itemBoughtID` int(11) DEFAULT NULL,
  `moneySpent` int(11) DEFAULT NULL,
  PRIMARY KEY (`timestamp`,`username`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `UserTransactions`
--

LOCK TABLES `UserTransactions` WRITE;
/*!40000 ALTER TABLE `UserTransactions` DISABLE KEYS */;
INSERT INTO `UserTransactions` VALUES ('2018-11-02 10:48:33','b',9,50),('2018-11-02 10:48:35','b',6,50),('2018-11-02 10:48:36','b',4,50),('2018-11-02 10:57:57','b',8,50),('2018-11-02 10:57:59','b',12,50),('2018-11-02 11:00:57','b',1,50),('2018-11-02 11:00:58','b',6,50),('2018-11-02 11:00:59','b',11,50),('2018-11-02 11:04:09','b',2,50),('2018-11-02 11:04:14','b',18,50),('2018-11-02 11:04:22','b',7,50),('2018-11-08 18:18:48','b',1,50),('2018-11-08 19:04:44','b',8,50),('2018-11-08 19:04:47','b',11,50),('2018-11-08 19:05:15','b',10,50),('2018-11-08 19:06:49','b',13,50),('2018-11-08 19:06:57','b',11,50),('2018-11-08 19:27:43','b',16,50),('2018-11-08 19:34:09','b',10,50),('2018-11-08 19:34:11','b',11,50),('2018-11-08 19:34:13','b',1,50),('2018-11-08 19:34:14','b',2,50),('2018-12-14 17:07:32','am2272-buyer@njit.edu',0,-500);
/*!40000 ALTER TABLE `UserTransactions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Users` (
  `username` varchar(30) NOT NULL,
  `password` varchar(30) DEFAULT NULL,
  `email` varchar(30) DEFAULT NULL,
  `totalMoney` int(11) DEFAULT NULL,
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES ('b','444','am2272-buyer@njit.edu',8000),('beeear','bear.','bear@gmail.com',200),('testy','test','test@test.com',200);
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `VersionControl`
--

DROP TABLE IF EXISTS `VersionControl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `VersionControl` (
  `version` int(11) NOT NULL,
  `type` varchar(30) NOT NULL,
  `path` varchar(60) NOT NULL,
  `status` varchar(30) NOT NULL,
  PRIMARY KEY (`version`,`type`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `VersionControl`
--

LOCK TABLES `VersionControl` WRITE;
/*!40000 ALTER TABLE `VersionControl` DISABLE KEYS */;
INSERT INTO `VersionControl` VALUES (1,'client','backups/backup_client_v1.tgz','good'),(1,'frontend','backups/backup_frontend_v1.tgz','good'),(1,'server','backups/backup_server_v1.tgz','good'),(2,'frontend','backups/backup_frontend_v2.tgz','good'),(4,'frontend','backups/backup_frontend_v4.tgz','testing'),(6,'client','backups/backup_client_v6.tgz','bad'),(7,'client','backups/backup_client_v7.tgz','testing');
/*!40000 ALTER TABLE `VersionControl` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-14 18:19:58
