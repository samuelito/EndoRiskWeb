-- MySQL dump 10.13  Distrib 5.6.23, for Win64 (x86_64)
--
-- Host: localhost    Database: endorisk
-- ------------------------------------------------------
-- Server version	5.6.24-log

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
-- Table structure for table `administrator`
--

DROP TABLE IF EXISTS `administrator`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `administrator` (
  `idAdmin` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `email` varchar(100) NOT NULL,
  `password` varchar(200) NOT NULL,
  `firstname` varchar(50) DEFAULT NULL,
  `lastname` varchar(50) DEFAULT NULL,
  `subadmin` int(10) unsigned NOT NULL,
  `passwordSalt` varchar(200) NOT NULL,
  PRIMARY KEY (`idAdmin`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `administrator`
--

LOCK TABLES `administrator` WRITE;
/*!40000 ALTER TABLE `administrator` DISABLE KEYS */;
INSERT INTO `administrator` VALUES (1,'samuel.feliciano@upr.edu','samuel','Samuel','Feliciano',0,''),(4,'luz.gonzalez@upr.edu','luz','Luz','Gonzalez',1,''),(5,'eddie.perez@upr.edu','B5dFxJJDK5Pt3GdmFNgil6G/3MHDVmzz847t0Cq654XnrYECbuFnaXnPWQCtViGJEQWvZMesMtPN68u1S5yyrw==','Eddie','Perez',0,'100000.9j+J9NChhq5+hCPRwsR99NuLAEX07DfkUvgPacTJT4O10Q=='),(6,'saylisse.davila@upr.edu','saylisse','Saylisse','Davila',1,''),(7,'samuel@upr.edu','samuel','Sammy','Feliciano',1,''),(9,'jaimeiris.nieves@upr.edu','sk8RKVtUi2+t4kav/ns8hofT2RRPkpvv8e7hjmDNYrAlTEefQwa6tn03foKTeB3gIrAXXv0r/d5Dae45I6ztjQ==','Jaimeiris ','Nieves',0,'100000.ZVq5MHaqnjyPnmmzv/IHxC1hveoJwc5wRA1rHsOW0JOvPA=='),(10,'epm059@gmail.com','Se9Vp5tnoOiAVM479E+ChNFbnHgGXEN0EunnRqDeBFPkWchdt0NMgKxfQWdn5BbuVp7MhzmHzgaTNCXs4oj9bw==','Ed','Perez',0,'100000.hFjdyGOw4T87wKYSYePiD4c86IYbTwzjo1yrgw1AZRWSiQ=='),(11,'epm058@hotmail.com','o5xj+/Q/ZQAd9WjuzECUrOSPoCl+i7ArLsihAiXIUUDC0tCVF7Qxx3LrhmHkmCeQiXj20RTDdp5tQQg+Uo1uYA==','Ee','so',1,'100000.UftXzxYS1IKOYzVR8OABBK89AjsEeUykA/icKFYFFToQeg=='),(12,'epm058@hotmail.com','Rh3H580h2lR1IV6vDwFSbMW9rhbd7SgwZZv0JrLzltIZdNeQCDqjWBUZDZ0U2Eka0npnz7WqHkZQus2ZD0pLfQ==','de','fr',1,'100000.WdPNapIKHMdgk5mwBsQYmvbZf4SeMSMi87qXLGOhtqZGqA=='),(13,'epm058@hotmail.com','Jzue+ILhPvsL1iHb5smUfHuCJsYaSRl8v1XMtgluNFoeYz9qmTUjdfQGUWStyfRw7s9Vb3tios6zqn/DgIBY1Q==','de','fr',1,'100000.7Ee0hw31vVcLj2nMaPkUbHchAy3XeWK2MqtoGJ1MSfJOQA=='),(14,'epm058@hotmail.com','EjujVDL168plaFGXodbSqI2+cwn5oe0MhaRjJNRIUW4Hv1kuJDGyrAeL2alS2gv+muoXg9j/W6tltvnSOQ5qSg==','Ed','so',1,'100000.azZRnYbIg0+QuzPOBGCRGWGuMa765c/iHzGdpQxJoftBwQ=='),(15,'epm058@hotmail.com','uQUMhBa8Qi/fE/J+V+kpIguOGNl2vPtbX0/ML//0Kpy1Unv5Q5qOzfw0NC7eqVpQuX3baOsLfe7yCZmbX0YYpA==','Ed','so',1,'100000.7qtCJgLfpbekQ3IlL16FXaf6oPC6SkalZUIt+rSntmlFig=='),(16,'epm058@hotmail.com','43kB3jnN63jJbqWo3ZNUySRv/jQvuJAZaLxtwC/qr114G3hzAnIzDje3YgF75Ebj9FyK4Q8POzm1T8gsWMFEbA==','Ed','so',1,'100000.qA1ngBCDlqH4kUCQ83/2tpNPQh+0sqHnWX1AZRZT8Zf9Kg=='),(17,'epm058@hotmail.com','qwiW5AyAWZ2lzRYsbqiRtHrJlhhQ7gVuBuop27VxhAEYbddntGonrJtJJNAl5OplO3RkKWDPBVmAYYnFryDW2w==','Ed','so',1,'100000.yKWWDYRGYIrDMifn2NBNPMohmpCFcJoG5jq6sgGO12MMcA=='),(18,'eddie.perez1@upr.edu','nnKDyaEc8gYW9rRi1NXwRBmlzCoE/FvV29xUutwDG0fs1GwEUf3tYkgZ/mOza5qInUgYzGl1pvrF5YJWAWCKVw==','Coso','Perez',0,'100000.cgrrUyzPOeUcX4O/8ISo2uL7ZBamR3J3ZI/LNclVDnSddA=='),(19,'eddie.perez1@upr.edu','rSsSDnQey9OvgBE9/TmVNHGsd+ZLsB1jntaMUt8ojItEA/9ltJ7owzBlgIRWCbvKuIgOu6/+v334O4Ncu4T+8A==','Coso','Perez',0,'100000.wQKtV5LgS0O/Vn/bnhPG6puklJ5pbm/GnPQVUT6g8f+OYQ=='),(20,'eddie.perez1@upr.edu','RTgZdkBEf8nnoL1GAsv2vAJoqBm9hfEoTZR+GM0kq1OhZmoY04WK7jkQmGDslx34zA8H8b3puuRPBxMecj+oSg==','Coso','Perez',0,'100000.5/kT4jjYPTtGtoKZWRRhzr1lb8VUFzmbLP1geqOP4dkn9Q=='),(21,'eddie.perez1@upr.edu','/zdxeE2IoK6Od/A+U+ih7lwtNSp5b5RZT8M+t+xApegItqf9hzqdQk+BbyfOeKqR+ia4Uo7rCkSc//SQTBLZUQ==','Coso','Perez',0,'100000.Kz4CfAijh1Up8zqMIgfq8B7X5cguTakM830d2HN640YNKg=='),(22,'eddie.perez1@upr.edu','omxAKZmUYw91cHasrKZMqaOwMS71GEeXwmwPBQJeS7eB5qt4NImtP0TEG9BE/urAaCRRzcHrbytSONP+8yYzKw==','Coso','Perez',0,'100000.t5P9z41BFW6izZKtsN/uOU5VeOdUeign4qdYDvH1X3F4SA=='),(23,'eddie.perez1@upr.edu','wD9aR1Q9dKdGO7MVXjYIGiVTE+JzDDEw+Z4AckIwfeFOEl4X7CvXjHKrs5D2hFc3PjWdXOGaTk2HQd9IxavAUg==','Ee','rrerer',1,'100000.xGZxWboQH6pyu7OssjvNN0L4tuF86jC892g/P50ccttO3Q=='),(24,'eddie.perez1@upr.edu','pTXdHeI0elBn2s4t65TCilcvabXv1tw5oedkb11VOxJLuw5XvSAFYwXS3oSRVNyIoNpjkxfpg2fJcMbJEdFB+g==','Ee','rrerer',1,'100000.ZlQn+zHgEJQST7dqGsFxsHfKGNQbUzfV1x1RurddXX1TKQ=='),(25,'eddie.perez1@upr.edu','5ZximYE6vhBRBLOUpcIsTfKk45rqMCEMjqg9IZ9x64daJXVoBQTcfvDQgQU3dj4IpExfAlvMXgEwUxLf5BWwZQ==','Ee','rrerer',1,'100000.MhkmP6GPegQ8zLrJPW0CoTe7ZXrJEHGKNt8sd8g761OIvg=='),(26,'eddie.perez1@upr.edu','qSr7z/7dsmvSSwprlKMhUzypqSin2y0jZyNRmNx7tok9RrkDv87ZV105QYnaLSOllD8he+omCksUxcyMmXNDkw==','Ee','rrerer',1,'100000.TZSuCkr0XfpbdxPJU7LpwLU0kCWUabAd4mMXKoEhiqkFPQ=='),(27,'eddie.perez1@upr.edu','cFHIgAjtZx5fL92FXRKra5Jk6WxXwhLPMNyek+/33fwtc4EEw/0rGMTkdTjgN4zcYIjwb8nhjah+1UbJZQGr3A==','Ee','rrerer',1,'100000.YApx+oXsfz/OzdRINTkTRKtOQRVAbnmacZfSNefFbUiLSw=='),(28,'eddie.perez1@upr.edu','U4dKfCQ2LaDeAUnFkzGueIsMnsTgzLbvtnDNTJOOgWZ/jCR/+BjPjqp2NRcLY3HIsIqVuAemj2ARcVeg3Y6o3Q==','Ee','rrerer',1,'100000.SN4WzlIeR+9UYWkfU1EjaWCSLOpjtafadlB+NyHDNLkv1A=='),(29,'eddie.perez1@upr.edu','4VIJXZB9zzjkfFeQrp9j+diNSfYgKm/wxq0NyGK8xoyqrEzPcY1wg39y5LwZCFXJGshHi1xyusmxTg7VVeBGQg==','Ee','rrerer',1,'100000.CU2UU9LeIAgr7TRtl/Zc1bUak376TmrMHzTtnYOPS90Tyg=='),(30,'samuel.feliciano@upr.edu','9lsATfy18clxhiNCdjjE50p+W8aEw7/l9Y+6j4RFKNX0cu1gCMdAEVysprx3GJjBIZU5nXkkTXopz+mo0OZfoQ==','Samuel','Feliciano',1,'100000.hHG/xo1dwM0PvpKA84142DqdAlEN/ezbgsCh3a+wSGl+Xw==');
/*!40000 ALTER TABLE `administrator` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `comment`
--

DROP TABLE IF EXISTS `comment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `comment` (
  `idComment` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(45) DEFAULT NULL,
  `content` varchar(255) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `time` datetime DEFAULT NULL,
  PRIMARY KEY (`idComment`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comment`
--

LOCK TABLES `comment` WRITE;
/*!40000 ALTER TABLE `comment` DISABLE KEYS */;
INSERT INTO `comment` VALUES (1,'Test','Testing Feedback','samuel.feliciano@upr.edu','2015-03-31 06:08:00'),(2,'Test 2','Testing Comment 2','testing@gmail.com','2015-03-31 06:52:00'),(3,'Test 3','Testing DateTime function','samuel.feliciano@upr.edu','2015-04-02 02:49:22'),(5,'Test 5','Test','samuel.feliciano@upr.edu','2015-04-10 11:17:57'),(6,NULL,NULL,NULL,'2015-04-28 23:22:01'),(7,'Test','Esto es una prueba','epm058@hotmail.com','2015-04-29 00:45:14'),(8,'h','jhghjhg','e@gmail.com','2015-04-29 22:36:18');
/*!40000 ALTER TABLE `comment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `diseases`
--

DROP TABLE IF EXISTS `diseases`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `diseases` (
  `idDiseases` int(11) NOT NULL AUTO_INCREMENT,
  `disease` varchar(50) DEFAULT NULL,
  `idRomeQuestion` int(11) DEFAULT NULL,
  `criteria` varchar(3) DEFAULT NULL,
  `comparedValue` varchar(3) DEFAULT NULL,
  PRIMARY KEY (`idDiseases`)
) ENGINE=InnoDB AUTO_INCREMENT=115 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `diseases`
--

LOCK TABLES `diseases` WRITE;
/*!40000 ALTER TABLE `diseases` DISABLE KEYS */;
INSERT INTO `diseases` VALUES (1,'Irritable Bowel Syndrome',1,'>','2'),(2,'Irritable Bowel Syndrome',3,'=','0'),(3,'Irritable Bowel Syndrome',5,'=','1'),(4,'Irritable Bowel Syndrome',6,'>','0'),(5,'Irritable Bowel Syndrome',7,'>','0'),(6,'Irritable Bowel Syndrome',8,'>','0'),(7,'Irritable Bowel Syndrome',9,'>','0'),(8,'Irritable Bowel Syndrome',10,'>','0'),(9,'Irritable Bowel Syndrome - Constipation',15,'>','0'),(10,'Irritable Bowel Syndrome - Constipation',11,'=','0'),(11,'Irritable Bowel Syndrome - Diarrhea',15,'=','0'),(12,'Irritable Bowel Syndrome - Diarrhea',11,'>','0'),(13,'Irritable Bowel Syndrome - Mixed',15,'>','0'),(14,'Irritable Bowel Syndrome - Mixed',11,'>','0'),(15,'Irritable Bowel Syndrome - Unspecified',15,'=','0'),(16,'Irritable Bowel Syndrome - Unspecified',11,'=','0'),(17,'Functional Diarrhea',1,'=','0'),(18,'Functional Diarrhea',12,'=','1'),(19,'Functional Diarrhea',13,'=','1'),(20,'Functional Constipation',14,'>','1'),(21,'Functional Constipation',15,'>','1'),(22,'Functional Constipation',16,'>','1'),(23,'Functional Constipation',17,'>','0'),(24,'Functional Constipation',18,'>','0'),(25,'Functional Constipation',19,'>','0'),(26,'Functional Constipation',21,'=','1'),(27,'Functional Constipation',11,'=','0'),(28,'Functional Bloating',18,'<','5'),(29,'Functional Bloating',19,'=','0'),(30,'Functional Bloating',21,'<','5'),(31,'Functional Bloating',11,'=','0'),(32,'Functional Bloating',12,'<','5'),(33,'Functional Bloating',13,'=','0'),(34,'Functional Bloating',22,'>','2'),(35,'Functional Bloating',23,'=','1'),(36,'Functional Defecation Disorders',16,'>','1'),(37,'Functional Defecation Disorders',17,'>','1'),(38,'Functional Defecation Disorders',18,'>','1'),(39,'Functional Defecation Disorders',19,'>','1'),(40,'Functional Defecation Disorders',20,'>','1'),(41,'Functional Defecation Disorders',21,'=','1'),(42,'Epigastric Pain Syndrome',28,'<','3'),(43,'Epigastric Pain Syndrome',29,'<','3'),(44,'Epigastric Pain Syndrome',30,'>','3'),(45,'Epigastric Pain Syndrome',31,'=','1'),(46,'Epigastric Pain Syndrome',32,'>','1'),(47,'Epigastric Pain Syndrome',33,'>','2'),(48,'Epigastric Pain Syndrome',35,'=','0'),(49,'Chronic Proctalgia',38,'>','2'),(50,'Chronic Proctalgia',39,'=','2'),(51,'Chronic Proctalgia',41,'=','1'),(52,'Proctalgia Fugax',38,'>','1'),(53,'Proctalgia Fugax',39,'=','1'),(54,'Proctalgia Fugax',40,'=','1'),(55,'Functional Abdominal Pain Syndrome',1,'=','6'),(56,'Functional Abdominal Pain Syndrome',2,'=','1'),(57,'Functional Abdominal Pain Syndrome',3,'=','0'),(58,'Functional Abdominal Pain Syndrome',4,'>','0'),(59,'Functional Abdominal Pain Syndrome',5,'=','1'),(60,'Functional Abdominal Pain Syndrome',34,'<','2'),(61,'Functional Abdominal Pain Syndrome',35,'<','2'),(62,'Functional Abdominal Pain Syndrome',36,'<','2'),(63,'Functional Abdominal Pain Syndrome',37,'<','2'),(89,'Functional Gallbladder Sphincter Oddi Disorders',42,'>','0'),(90,'Functional Gallbladder Sphincter Oddi Disorders',43,'>','1'),(91,'Functional Gallbladder Sphincter Oddi Disorders',44,'>','1'),(92,'Functional Gallbladder Sphincter Oddi Disorders',45,'>','1'),(93,'Functional Gallbladder Sphincter Oddi Disorders',46,'=','0'),(94,'Functional Gallbladder Sphincter Oddi Disorders',47,'>','1'),(95,'Functional Gallbladder Sphincter Oddi Disorders',48,'=','0'),(96,'Functional Gallbladder Sphincter Oddi Disorders',49,'=','0'),(97,'Functional Biliary SO Disorder',50,'=','1'),(98,'Functional Biliary SO Disorder',51,'>','1'),(99,'Aerophagia',63,'>','4'),(100,'Aerophagia',64,'=','1'),(101,'Chronic Idiophathic Nausea',52,'>','4'),(102,'Chronic Idiophathic Nausea',53,'=','1'),(103,'Chronic Idiophathic Nausea',54,'<','4'),(104,'Functional Vomiting',54,'>','3'),(105,'Functional Vomiting',55,'=','1'),(106,'Functional Vomiting',56,'=','0'),(107,'Cyclic Vomiting Syndrome',54,'>','0'),(108,'Cyclic Vomiting Syndrome',55,'=','1'),(109,'Cyclic Vomiting Syndrome',57,'>','1'),(110,'Cyclic Vomiting Syndrome',58,'=','1'),(111,'Rumination Syndrome Adults',59,'>','3'),(112,'Rumination Syndrome Adults',60,'=','1'),(113,'Rumination Syndrome Adults',61,'>','1'),(114,'Rumination Syndrome Adults',62,'=','0');
/*!40000 ALTER TABLE `diseases` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `endoanswers`
--

DROP TABLE IF EXISTS `endoanswers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `endoanswers` (
  `idAnswer` int(11) NOT NULL AUTO_INCREMENT,
  `idQuiz` int(11) NOT NULL,
  `idQuestion` int(11) NOT NULL,
  `answer` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idAnswer`),
  KEY `idQuiz_idx` (`idQuiz`)
) ENGINE=InnoDB AUTO_INCREMENT=500 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `endoanswers`
--

LOCK TABLES `endoanswers` WRITE;
/*!40000 ALTER TABLE `endoanswers` DISABLE KEYS */;
INSERT INTO `endoanswers` VALUES (1,13,0,'23'),(2,36,1,'Caucasico'),(3,37,1,'Hispano'),(4,37,2,'23'),(5,37,3,'11'),(6,37,4,'27'),(7,37,5,'12'),(8,37,6,'Si'),(9,37,7,'3'),(10,37,8,'2'),(11,37,9,'Si'),(12,37,10,'No'),(13,37,11,'1'),(14,37,12,'1'),(15,37,13,'No'),(16,37,14,'0'),(17,37,15,'Si'),(18,37,16,'No'),(19,37,17,'13'),(20,37,18,'No'),(21,37,19,' '),(22,37,20,'No'),(23,37,21,'No'),(24,37,22,'No'),(25,37,23,'n/a'),(26,37,24,'n/a'),(27,38,1,'Asiatico'),(28,38,2,'34'),(29,38,3,'13'),(30,38,4,'11'),(31,38,5,'7'),(32,38,6,'Si'),(33,38,7,'3'),(34,38,8,'4'),(35,38,9,'Si'),(36,38,10,'Si'),(37,38,11,'1'),(38,38,12,'1'),(39,38,13,'Si'),(40,38,14,'1'),(41,38,15,'No'),(42,38,16,'No'),(43,38,17,'13'),(44,38,18,'Si'),(45,38,19,'Madre'),(46,38,20,'No'),(47,38,21,'No'),(48,38,22,'n/a'),(49,38,23,'n/a'),(50,38,24,'n/a'),(51,39,1,'Multirracial'),(52,39,2,' 55'),(53,39,3,'13'),(54,39,4,' 13'),(55,39,5,' 13'),(56,39,6,'Si'),(57,39,7,' 4'),(58,39,8,' 10'),(59,39,9,''),(60,39,10,''),(61,39,11,' '),(62,39,12,' '),(63,39,13,''),(64,39,14,' '),(65,39,15,''),(66,39,16,''),(67,39,17,' '),(68,39,18,''),(69,39,19,' '),(70,39,20,''),(71,39,21,''),(72,39,22,' '),(73,39,23,' '),(74,39,24,' '),(75,40,1,''),(76,40,2,' '),(77,40,3,' '),(78,40,4,' '),(79,40,5,' '),(80,40,6,''),(81,40,7,' '),(82,40,8,' '),(83,40,9,''),(84,40,10,''),(85,40,11,' '),(86,40,12,' '),(87,40,13,''),(88,40,14,' '),(89,40,15,''),(90,40,16,''),(91,40,17,' '),(92,40,18,''),(93,40,19,' '),(94,40,20,''),(95,40,21,''),(96,40,22,' '),(97,40,23,' '),(98,40,24,' '),(99,41,1,'Asiatico'),(100,41,2,' 1'),(101,41,3,' 13'),(102,41,4,' 35'),(103,41,5,' 6'),(104,41,6,'Si'),(105,41,7,' 5'),(106,41,8,' 2'),(107,41,9,'Si'),(108,41,10,'Si'),(109,41,11,' 2'),(110,41,12,' 2'),(111,41,13,'Si'),(112,41,14,' 1'),(113,41,15,'Si'),(114,41,16,'Si'),(115,41,17,' 40'),(116,41,18,'Si'),(117,41,19,' madre'),(118,41,20,'Si'),(119,41,21,'Si'),(120,41,22,' grave'),(121,41,23,' tumba'),(122,41,24,' suicidio'),(123,42,1,'Caucasico'),(124,42,2,' 1'),(125,42,3,' 13'),(126,42,4,' 35'),(127,42,5,' 6'),(128,42,6,'Si'),(129,42,7,' 5'),(130,42,8,' 2'),(131,42,9,'Si'),(132,42,10,'Si'),(133,42,11,' 2'),(134,42,12,' 2'),(135,42,13,'Si'),(136,42,14,' 1'),(137,42,15,'Si'),(138,42,16,'Si'),(139,42,17,' 40'),(140,42,18,'Si'),(141,42,19,' madre'),(142,42,20,'Si'),(143,42,21,'Si'),(144,42,22,' grave'),(145,42,23,' tumba'),(146,42,24,' suicidio'),(147,43,1,'Hispano'),(148,43,2,'45'),(149,43,3,'13'),(150,43,4,'35'),(151,43,5,'6'),(152,43,6,'Si'),(153,43,7,'5'),(154,43,8,'3'),(155,43,9,'Si'),(156,43,10,'Si'),(157,43,11,'2'),(158,43,12,'3'),(159,43,13,'Si'),(160,43,14,'1'),(161,43,15,'Si'),(162,43,16,'Si'),(163,43,17,'26'),(164,43,18,'Si'),(165,43,19,'abuela'),(166,43,20,'Si'),(167,43,21,'Si'),(168,43,22,'grave'),(169,43,23,'otro'),(170,43,24,'cirugia'),(171,43,0,'DEP,PE,PP,VOM'),(172,44,1,'Hispano'),(173,44,2,'45'),(174,44,3,'13'),(175,44,4,'35'),(176,44,5,'6'),(177,44,6,'Si'),(178,44,7,'5'),(179,44,8,'3'),(180,44,9,'Si'),(181,44,10,'Si'),(182,44,11,'2'),(183,44,12,'3'),(184,44,13,'Si'),(185,44,14,'1'),(186,44,15,'Si'),(187,44,16,'Si'),(188,44,17,'26'),(189,44,18,'Si'),(190,44,19,'abuela'),(191,44,20,'Si'),(192,44,21,'Si'),(193,44,22,'grave'),(194,44,23,'otro'),(195,44,24,'cirugia'),(196,44,0,'DEP,PU,PP,UVB'),(197,45,1,'Hispano'),(198,45,2,'45'),(199,45,3,'13'),(200,45,4,'35'),(201,45,5,'6'),(202,45,6,'Si'),(203,45,7,'5'),(204,45,8,'3'),(205,45,9,'Si'),(206,45,10,'Si'),(207,45,11,'4'),(208,45,12,'3'),(209,45,13,'Si'),(210,45,14,'1'),(211,45,15,'Si'),(212,45,16,'Si'),(213,45,17,'26'),(214,45,18,'Si'),(215,45,19,'abuela'),(216,45,20,'Si'),(217,45,21,'Si'),(218,45,22,'grave'),(219,45,23,'otro'),(220,45,24,'cirugia'),(221,45,0,'DEP,DPA,BP,HVB,VOM'),(222,46,1,'Hispano'),(223,46,2,'45'),(224,46,3,'13'),(225,46,4,'35'),(226,46,5,'6'),(227,46,6,'Si'),(228,46,7,'5'),(229,46,8,'3'),(230,46,9,'Si'),(231,46,10,'Si'),(232,46,11,'2'),(233,46,12,'3'),(234,46,13,'Si'),(235,46,14,'1'),(236,46,15,'Si'),(237,46,16,'Si'),(238,46,17,'26'),(239,46,18,'Si'),(240,46,19,'abuela'),(241,46,20,'Si'),(242,46,21,'Si'),(243,46,22,'grave'),(244,46,23,'otro'),(245,46,24,'cirugia'),(246,46,0,'DEP,DME,DPA,PU,HVB'),(247,47,1,'Hispano'),(248,47,2,'45'),(249,47,3,'13'),(250,47,4,'35'),(251,47,5,'6'),(252,47,6,'Si'),(253,47,7,'5'),(254,47,8,'3'),(255,47,9,'Si'),(256,47,10,'Si'),(257,47,11,'0'),(258,47,12,'0'),(259,47,13,'Si'),(260,47,14,'1'),(261,47,15,'Si'),(262,47,16,'Si'),(263,47,17,'26'),(264,47,18,'Si'),(265,47,19,'abuela'),(266,47,20,'Si'),(267,47,21,'Si'),(268,47,22,'Alto'),(269,47,23,'Otro'),(270,47,24,'cirugia'),(271,48,1,'Hispano'),(272,48,2,'45'),(273,48,3,'13'),(274,48,4,'35'),(275,48,5,'6'),(276,48,6,'Si'),(277,48,7,'5'),(278,48,8,'3'),(279,48,9,'Si'),(280,48,10,'Si'),(281,48,11,'0'),(282,48,12,'0'),(283,48,13,'Si'),(284,48,14,'0'),(285,48,15,'Si'),(286,48,16,'Si'),(287,48,17,'26'),(288,48,18,'Si'),(289,48,19,'abuela'),(290,48,20,'Si'),(291,48,21,'Si'),(292,48,22,'Bajo'),(293,48,23,'Otro'),(294,48,24,'cirugia'),(295,48,0,'DEP,DPA'),(296,49,1,'Hispano'),(297,49,2,'45'),(298,49,3,'13'),(299,49,4,'35'),(300,49,5,'6'),(301,49,6,'Si'),(302,49,7,'5'),(303,49,8,'3'),(304,49,9,'Si'),(305,49,10,'Si'),(306,49,11,'2'),(307,49,12,'3'),(308,49,13,'Si'),(309,49,14,'1'),(310,49,15,'Si'),(311,49,16,'Si'),(312,49,17,'26'),(313,49,18,'Si'),(314,49,19,'abuela'),(315,49,20,'Si'),(316,49,21,'Si'),(317,49,22,'Alto'),(318,49,23,'Otro'),(319,49,24,'cirugia'),(320,49,0,'DEP,HEA,VS,FE,VOM'),(321,50,1,'Asiatico'),(322,50,2,'45'),(323,50,3,'13'),(324,50,4,'35'),(325,50,5,'6'),(326,50,6,'Si'),(327,50,7,'5'),(328,50,8,'3'),(329,50,9,'Si'),(330,50,10,'Si'),(331,50,11,'2'),(332,50,12,'3'),(333,50,13,'Si'),(334,50,14,'1'),(335,50,15,'Si'),(336,50,16,'Si'),(337,50,17,'26'),(338,50,18,'Si'),(339,50,19,'abuela'),(340,50,20,'Si'),(341,50,21,'Si'),(342,50,22,'Alto'),(343,50,23,'Pastillas anticonceptivas'),(344,50,24,'cirugia'),(345,51,0,''),(346,51,1,'Hispano'),(347,51,2,'45'),(348,51,3,'13'),(349,51,4,'21'),(350,51,5,'6'),(351,51,6,'Si'),(352,51,7,'5'),(353,51,8,'3'),(354,51,9,'Si'),(355,51,10,'Si'),(356,51,11,'5'),(357,51,12,'3'),(358,51,13,'Si'),(359,51,14,'1'),(360,51,15,'Si'),(361,51,16,'Si'),(362,51,17,'26'),(363,51,18,'Si'),(364,51,19,'madre'),(365,51,20,'Si'),(366,51,21,'Si'),(367,51,22,'Alto'),(368,51,23,'Otro'),(369,51,24,'pastillas'),(370,52,0,''),(371,52,1,'Hispano'),(372,52,2,'45'),(373,52,3,'13'),(374,52,4,'35'),(375,52,5,'6'),(376,52,6,'Si'),(377,52,7,'5'),(378,52,8,'3'),(379,52,9,'Si'),(380,52,10,'Si'),(381,52,11,'2'),(382,52,12,'3'),(383,52,13,'Si'),(384,52,14,'1'),(385,52,15,'Si'),(386,52,16,'Si'),(387,52,17,'26'),(388,52,18,'Si'),(389,52,19,'abuela'),(390,52,20,'Si'),(391,52,21,'Si'),(392,52,22,'Alto'),(393,52,23,'Pastillas anticonceptivas'),(394,52,24,'cirugia'),(395,52,0,'DEP,DIA,DME,DPA'),(396,53,0,''),(397,53,1,'Hispano'),(398,53,2,'17'),(399,53,3,'13'),(400,53,4,'35'),(401,53,5,'2'),(402,53,6,'Si'),(403,53,7,'5'),(404,53,8,'2'),(405,53,9,'Si'),(406,53,10,'Si'),(407,53,11,'4'),(408,53,12,'3'),(409,53,13,'Si'),(410,53,14,'1'),(411,53,15,'Si'),(412,53,16,'Si'),(413,53,17,'26'),(414,53,18,'Si'),(415,53,19,'Madre'),(416,53,20,'Si'),(417,53,21,'Si'),(418,53,22,'Moderado'),(419,53,23,'Otro'),(420,53,24,'cirugia'),(421,53,0,'DEP,DIA,DME,DPA,HVB'),(422,41,0,''),(423,41,1,'Hispano'),(424,41,2,'45'),(425,41,3,'13'),(426,41,4,'35'),(427,41,5,'6'),(428,41,6,'Sí'),(429,41,7,'0'),(430,41,8,'0'),(431,41,9,'No'),(432,41,10,'No Aplica'),(433,41,11,'4'),(434,41,12,'4'),(435,41,13,'No'),(436,41,14,'0'),(437,41,15,'No'),(438,41,16,'No'),(439,41,17,'26'),(440,41,18,'No'),(441,41,19,'Madre'),(442,41,20,'No'),(443,41,21,'No'),(444,41,22,'Mínimo'),(445,41,23,'Otro'),(446,41,24,' nada'),(447,41,0,'DEP,DIA'),(448,42,0,''),(449,42,1,'Asiático'),(450,42,2,'45'),(451,42,3,'13'),(452,42,4,'35'),(453,42,5,'6'),(454,42,6,'No'),(455,42,7,'0'),(456,42,8,'0'),(457,42,9,'No'),(458,42,10,'No'),(459,42,11,'4'),(460,42,12,'4'),(461,42,13,'No'),(462,42,14,'0'),(463,42,15,'No'),(464,42,16,'No'),(465,42,17,'26'),(466,42,18,'No'),(467,42,19,'Madre'),(468,42,20,'No'),(469,42,21,'No Aplica'),(470,42,22,'Mínimo'),(471,42,23,'Otro'),(472,42,24,'na'),(473,43,0,''),(474,43,1,'Asiático'),(475,43,2,'45'),(476,43,3,'13'),(477,43,4,'35'),(478,43,5,'6'),(479,43,6,'Sí'),(480,43,7,'5'),(481,43,8,'3'),(482,43,9,'Sí'),(483,43,10,'Sí'),(484,43,11,'2'),(485,43,12,'3'),(486,43,13,'Sí'),(487,43,14,'1'),(488,43,15,'Sí'),(489,43,16,'Sí'),(490,43,17,'26'),(491,43,18,'Sí'),(492,43,19,'Hermana'),(493,43,20,'Sí'),(494,43,21,'Sí'),(495,43,22,'Mínimo'),(496,43,23,'Depo-Provera'),(497,43,24,'suicidio'),(498,43,0,'DEP,DPA,HEA'),(499,43,0,'UF,BC,OPC');
/*!40000 ALTER TABLE `endoanswers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `endochoices`
--

DROP TABLE IF EXISTS `endochoices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `endochoices` (
  `idChoice` int(11) NOT NULL AUTO_INCREMENT,
  `choiceSet` varchar(5) DEFAULT NULL,
  `choiceOption` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idChoice`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `endochoices`
--

LOCK TABLES `endochoices` WRITE;
/*!40000 ALTER TABLE `endochoices` DISABLE KEYS */;
INSERT INTO `endochoices` VALUES (1,'A','Afroamericano'),(2,'A','Asiático'),(3,'A','Caucásico'),(4,'A','Hispano'),(5,'A','Multirracial'),(6,'A','Amerindio o Nativo de Alaska'),(7,'A','Prefiero no contestar'),(8,'B','Sí'),(9,'B','No'),(10,'C','Sí'),(11,'C','No'),(12,'C','No Aplica'),(13,'D','Mínimo'),(14,'D','Leve'),(15,'D','Moderado'),(16,'D','Severo'),(17,'E','Madre'),(18,'E','Hija'),(19,'E','Hermana'),(20,'E','Abuela'),(21,'E','Tía'),(22,'E','Sobrina'),(24,'E','Prima'),(25,'F','Danazol/Danocine'),(26,'F','Depo-Provera'),(27,'F','Lupron/Zoladex'),(28,'F','Pastillas anticonceptivas'),(29,'F','Reemplazo hormonal (Norentindrone, Climara)'),(30,'F','Otro');
/*!40000 ALTER TABLE `endochoices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `endoquestion`
--

DROP TABLE IF EXISTS `endoquestion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `endoquestion` (
  `idQuestion` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `endoQuestion` varchar(255) DEFAULT NULL,
  `abbr` varchar(5) DEFAULT NULL,
  `choiceSet` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`idQuestion`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `endoquestion`
--

LOCK TABLES `endoquestion` WRITE;
/*!40000 ALTER TABLE `endoquestion` DISABLE KEYS */;
INSERT INTO `endoquestion` VALUES (1,'Etnia','EBA','A'),(2,'¿Cuál es su edad?','AGE',''),(3,'¿A qué edad tuvo su primer período?','AAM',''),(4,'¿Cuál es el tiempo promedio entre períodos? (días)','DBP',''),(5,'¿Cuál es la duración promedio de su período? (días)','PL',''),(6,'¿Actualmente fuma o ha fumado?','SM','B'),(7,'Si contestó que si, indique la cantidad de años fumando o que ha fumado. De lo contrario, escriba cero (0).','YS',''),(8,'¿Cuántos cigarrillos fuma al día? (Si no fuma, escriba cero (0)).','CI',''),(9,'¿Ha tenido problemas para quedar embarazada?','PGP','C'),(10,'Si contestó que ha tenido problemas para quedar embarazada, indique si ha estado o se encuentra recibiendo tratamiento médico para infertilidad.','IT','C'),(11,'¿Cuántas veces ha quedado embarazada? (Si no ha quedado embarazada, escriba cero (0)).','TP',''),(12,'¿Cuántos partos ha tenido? (Si no ha tenido partos, escriba cero (0)).','PA',''),(13,'¿Ha tenido abortos espontáneos?','SA','B'),(14,'Indique la cantidad de abortos espontáneos (Si no ha tenido abortos, escriba cero (0)).','NA',''),(15,'¿Ha utilizado pastillas anticonceptivas en el pasado?','OCP','B'),(16,'¿Está utilizando pastillas anticonceptivas actualmente?','CC','C'),(17,'¿A qué edad comenzaron los síntomas?','AWSS',''),(18,'¿Posee historial familiar de mujeres con endometriosis?','SI','B'),(19,'Si contestó que tiene familiar con endometriosis, indique el parentesco. ','SIA','E'),(20,'¿Ha recibido algún diagnóstico de endometriosis?','ED','B'),(21,'De haber recibido un diagnóstico de endometriosis, ¿fue diagnosticada a través de una cirugía? (Nota: Esta pregunta no se utilizará para el cálculo de su resultado.) ','SU','C'),(22,'De haber recibido un diagnóstico de endometriosis, indique el nivel de severidad. (Nota: Esta pregunta no se utilizará para el cálculo de su resultado.)','SE','D'),(23,'De haber recibido un diagnóstico de endometriosis, ¿qué tipo de tratamiento le recomendó su médico? (Nota: Esta pregunta no se utilizará para el cálculo de su resultado.)','ETR','F'),(24,'Si contestó otro en la pregunta anterior, por favor indique el tratamiento.(Nota: Esta pregunta no se utilizará para el cálculo de su resultado.)','OETR','');
/*!40000 ALTER TABLE `endoquestion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patients`
--

DROP TABLE IF EXISTS `patients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `patients` (
  `idQuiz` int(11) NOT NULL AUTO_INCREMENT,
  `idPatient` int(11) NOT NULL,
  `risk` float DEFAULT NULL,
  `verified` varchar(45) DEFAULT NULL,
  `time` datetime DEFAULT NULL,
  `severity` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idQuiz`,`idPatient`)
) ENGINE=InnoDB AUTO_INCREMENT=44 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patients`
--

LOCK TABLES `patients` WRITE;
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
INSERT INTO `patients` VALUES (1,5,56.78,'Yes','2015-04-07 18:26:04',NULL),(2,5,56.78,'Yes','2015-04-07 18:30:52',NULL),(3,5,56.78,'Yes','2015-04-07 19:50:38',NULL),(4,2,56.78,'Yes','2015-04-07 20:02:37',NULL),(5,3,56.78,'Yes','2015-04-07 20:05:07',NULL),(6,2,56.78,'Yes','2015-04-07 20:19:29',NULL),(7,1,56.78,'Yes','2015-04-07 20:21:19',NULL),(8,0,56.78,'Yes','2015-04-07 20:48:32',NULL),(9,0,56.78,'Yes','2015-04-07 20:53:26',NULL),(10,2,56.78,'Yes','2015-04-07 21:01:42',NULL),(11,4,56.78,'Yes','2015-04-07 21:05:21',NULL),(12,3,56.78,'Yes','2015-04-07 21:11:56',NULL),(13,23,56.78,'Yes','2015-04-07 21:15:04',NULL),(14,1,56.78,'Yes','2015-04-07 21:32:19',NULL),(15,3,56.78,'Yes','2015-04-07 21:34:48',NULL),(16,12,56.78,'Yes','2015-04-07 21:37:51',NULL),(17,4,56.78,'Yes','2015-04-07 21:42:19',NULL),(18,4,56.78,'Yes','2015-04-07 21:51:43',NULL),(19,2,56.78,'Yes','2015-04-07 21:59:59',NULL),(20,3,56.78,'Yes','2015-04-07 22:02:58',NULL),(21,3,56.78,'Yes','2015-04-07 22:04:32',NULL),(22,3,56.78,'Yes','2015-04-07 22:04:54',NULL),(23,3,56.78,'Yes','2015-04-07 22:07:59',NULL),(24,3,56.78,'Yes','2015-04-08 01:28:06',NULL),(25,1,56.78,'Yes','2015-04-08 01:34:38',NULL),(26,2,56.78,'Yes','2015-04-08 01:41:58',NULL),(27,2,56.78,'Yes','2015-04-08 01:43:04',NULL),(28,2,56.78,'Yes','2015-04-08 01:46:06',NULL),(29,2,56.78,'Yes','2015-04-08 01:52:16',NULL),(30,2,56.78,'Yes','2015-04-08 01:55:42',NULL),(31,1,56.78,'Yes','2015-04-08 01:58:14',NULL),(32,1,56.78,'Yes','2015-04-08 02:03:09',NULL),(33,23,56.78,'Yes','2015-04-08 02:11:24',NULL),(34,23,56.78,'Yes','2015-04-08 02:13:18',NULL),(35,23,56.78,'Yes','2015-04-08 02:23:46',NULL),(36,4,56.78,'Yes','2015-04-08 02:31:11',NULL),(37,23,56.78,'Yes','2015-04-08 02:42:32',NULL),(38,8,56.78,'Yes','2015-04-10 00:27:15',NULL),(39,20,56.78,'Yes','2015-04-10 11:11:47',NULL),(40,40,70.9,'NO','2015-04-26 20:37:55','56.89'),(41,41,0.00742881,'No','2015-05-05 18:14:07',NULL),(42,42,0.111909,'No','2015-05-05 18:23:08',NULL),(43,43,0.938981,'No','2015-05-06 01:02:41',NULL);
/*!40000 ALTER TABLE `patients` ENABLE KEYS */;
UNLOCK TABLES;


--
-- Table structure for table `patientsprecond`
--

DROP TABLE IF EXISTS `patientsprecond`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `patientsprecond` (
  `idCondition` int(11) NOT NULL AUTO_INCREMENT,
  `idQuiz` int(11) DEFAULT NULL,
  `preCondition` varchar(255) DEFAULT NULL,
  `preAbbr` varchar(10) DEFAULT NULL,
  `haspreCond` int(11) DEFAULT NULL,
  PRIMARY KEY (`idCondition`)
) ENGINE=InnoDB AUTO_INCREMENT=171 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patientsprecond`
--

LOCK TABLES `patientsprecond` WRITE;
/*!40000 ALTER TABLE `patientsprecond` DISABLE KEYS */;
INSERT INTO `patientsprecond` VALUES (1,47,'Fibromas uterinos','UF',1),(2,47,'Quiste en los ovarios','OC',1),(3,47,'PAP irregular clase I','PAP_I',0),(4,47,'PAP irregular clase II','PAP_II',0),(5,47,'PAP irregular clase III','PAP_III',0),(6,47,'PAP irregular clase IV','PAP_IV',0),(7,47,'Quiste en seno(s)','BC',0),(8,47,'Cáncer','CAN',0),(9,47,'Migraña','MIG',1),(10,47,'Otra condición pre-exsistente','OPC',1),(11,48,'Fibromas uterinos','UF',1),(12,48,'Quiste en los ovarios','OC',1),(13,48,'PAP irregular clase I','PAP_I',0),(14,48,'PAP irregular clase II','PAP_II',0),(15,48,'PAP irregular clase III','PAP_III',0),(16,48,'PAP irregular clase IV','PAP_IV',0),(17,48,'Quiste en seno(s)','BC',0),(18,48,'Cáncer','CAN',0),(19,48,'Migraña','MIG',1),(20,48,'Otra condición pre-exsistente','OPC',1),(21,49,'Fibromas uterinos','UF',1),(22,49,'Quiste en los ovarios','OC',1),(23,49,'PAP irregular clase I','PAP_I',0),(24,49,'PAP irregular clase II','PAP_II',0),(25,49,'PAP irregular clase III','PAP_III',0),(26,49,'PAP irregular clase IV','PAP_IV',0),(27,49,'Quiste en seno(s)','BC',0),(28,49,'Cáncer','CAN',0),(29,49,'Migraña','MIG',1),(30,49,'Otra condición pre-exsistente','OPC',1),(31,50,'Fibromas uterinos','UF',1),(32,50,'Quiste en los ovarios','OC',1),(33,50,'PAP irregular clase I','PAP_I',0),(34,50,'PAP irregular clase II','PAP_II',0),(35,50,'PAP irregular clase III','PAP_III',0),(36,50,'PAP irregular clase IV','PAP_IV',0),(37,50,'Quiste en seno(s)','BC',0),(38,50,'Cáncer','CAN',0),(39,50,'Migraña','MIG',1),(40,50,'Otra condición pre-exsistente','OPC',1),(41,51,'Fibromas uterinos','UF',1),(42,51,'Quiste en los ovarios','OC',1),(43,51,'PAP irregular clase I','PAP_I',0),(44,51,'PAP irregular clase II','PAP_II',0),(45,51,'PAP irregular clase III','PAP_III',0),(46,51,'PAP irregular clase IV','PAP_IV',0),(47,51,'Quiste en seno(s)','BC',0),(48,51,'Cáncer','CAN',0),(49,51,'Migraña','MIG',1),(50,51,'Otra condición pre-exsistente','OPC',1),(51,52,'Fibromas uterinos','UF',1),(52,52,'Quiste en los ovarios','OC',1),(53,52,'PAP irregular clase I','PAP_I',0),(54,52,'PAP irregular clase II','PAP_II',0),(55,52,'PAP irregular clase III','PAP_III',0),(56,52,'PAP irregular clase IV','PAP_IV',0),(57,52,'Quiste en seno(s)','BC',0),(58,52,'Cáncer','CAN',0),(59,52,'Migraña','MIG',1),(60,52,'Otra condición pre-exsistente','OPC',1),(61,53,'Fibromas uterinos','UF',1),(62,53,'Quiste en los ovarios','OC',1),(63,53,'PAP irregular clase I','PAP_I',0),(64,53,'PAP irregular clase II','PAP_II',0),(65,53,'PAP irregular clase III','PAP_III',0),(66,53,'PAP irregular clase IV','PAP_IV',0),(67,53,'Quiste en seno(s)','BC',0),(68,53,'Cáncer','CAN',0),(69,53,'Migraña','MIG',1),(70,53,'Otra condición pre-exsistente','OPC',1),(71,54,'Fibromas uterinos','UF',1),(72,54,'Quiste en los ovarios','OC',1),(73,54,'PAP irregular clase I','PAP_I',0),(74,54,'PAP irregular clase II','PAP_II',0),(75,54,'PAP irregular clase III','PAP_III',0),(76,54,'PAP irregular clase IV','PAP_IV',0),(77,54,'Quiste en seno(s)','BC',0),(78,54,'Cáncer','CAN',0),(79,54,'Migraña','MIG',1),(80,54,'Otra condición pre-exsistente','OPC',1),(81,55,'Fibromas uterinos','UF',1),(82,55,'Quiste en los ovarios','OC',1),(83,55,'PAP irregular clase I','PAP_I',0),(84,55,'PAP irregular clase II','PAP_II',0),(85,55,'PAP irregular clase III','PAP_III',0),(86,55,'PAP irregular clase IV','PAP_IV',0),(87,55,'Quiste en seno(s)','BC',0),(88,55,'Cáncer','CAN',0),(89,55,'Migraña','MIG',1),(90,55,'Otra condición pre-exsistente','OPC',1),(91,56,'Fibromas uterinos','UF',1),(92,56,'Quiste en los ovarios','OC',1),(93,56,'PAP irregular clase I','PAP_I',0),(94,56,'PAP irregular clase II','PAP_II',0),(95,56,'PAP irregular clase III','PAP_III',0),(96,56,'PAP irregular clase IV','PAP_IV',0),(97,56,'Quiste en seno(s)','BC',0),(98,56,'Cáncer','CAN',0),(99,56,'Migraña','MIG',1),(100,56,'Otra condición pre-exsistente','OPC',1),(101,57,'Fibromas uterinos','UF',1),(102,57,'Quiste en los ovarios','OC',1),(103,57,'PAP irregular clase I','PAP_I',0),(104,57,'PAP irregular clase II','PAP_II',0),(105,57,'PAP irregular clase III','PAP_III',0),(106,57,'PAP irregular clase IV','PAP_IV',0),(107,57,'Quiste en seno(s)','BC',0),(108,57,'Cáncer','CAN',0),(109,57,'Migraña','MIG',1),(110,57,'Otra condición pre-exsistente','OPC',1),(111,58,'Fibromas uterinos','UF',1),(112,58,'Quiste en los ovarios','OC',1),(113,58,'PAP irregular clase I','PAP_I',0),(114,58,'PAP irregular clase II','PAP_II',0),(115,58,'PAP irregular clase III','PAP_III',0),(116,58,'PAP irregular clase IV','PAP_IV',0),(117,58,'Quiste en seno(s)','BC',0),(118,58,'Cáncer','CAN',0),(119,58,'Migraña','MIG',1),(120,58,'Otra condición pre-exsistente','OPC',1),(121,59,'Fibromas uterinos','UF',1),(122,59,'Quiste en los ovarios','OC',1),(123,59,'PAP irregular clase I','PAP_I',0),(124,59,'PAP irregular clase II','PAP_II',0),(125,59,'PAP irregular clase III','PAP_III',0),(126,59,'PAP irregular clase IV','PAP_IV',0),(127,59,'Quiste en seno(s)','BC',0),(128,59,'Cáncer','CAN',0),(129,59,'Migraña','MIG',1),(130,59,'Otra condición pre-exsistente','OPC',1),(131,60,'Fibromas uterinos','UF',1),(132,60,'Quiste en los ovarios','OC',1),(133,60,'PAP irregular clase I','PAP_I',0),(134,60,'PAP irregular clase II','PAP_II',0),(135,60,'PAP irregular clase III','PAP_III',0),(136,60,'PAP irregular clase IV','PAP_IV',0),(137,60,'Quiste en seno(s)','BC',0),(138,60,'Cáncer','CAN',0),(139,60,'Migraña','MIG',1),(140,60,'Otra condición pre-exsistente','OPC',1),(141,61,'Fibromas uterinos','UF',1),(142,61,'Quiste en los ovarios','OC',1),(143,61,'PAP irregular clase I','PAP_I',0),(144,61,'PAP irregular clase II','PAP_II',0),(145,61,'PAP irregular clase III','PAP_III',0),(146,61,'PAP irregular clase IV','PAP_IV',0),(147,61,'Quiste en seno(s)','BC',0),(148,61,'Cáncer','CAN',0),(149,61,'Migraña','MIG',1),(150,61,'Otra condición pre-exsistente','OPC',1),(151,62,'Fibromas uterinos','UF',1),(152,62,'Quiste en los ovarios','OC',1),(153,62,'PAP irregular clase I','PAP_I',0),(154,62,'PAP irregular clase II','PAP_II',0),(155,62,'PAP irregular clase III','PAP_III',0),(156,62,'PAP irregular clase IV','PAP_IV',0),(157,62,'Quiste en seno(s)','BC',0),(158,62,'Cáncer','CAN',0),(159,62,'Migraña','MIG',1),(160,62,'Otra condición pre-exsistente','OPC',1),(161,63,'Fibromas uterinos','UF',1),(162,63,'Quiste en los ovarios','OC',1),(163,63,'PAP irregular clase I','PAP_I',0),(164,63,'PAP irregular clase II','PAP_II',0),(165,63,'PAP irregular clase III','PAP_III',0),(166,63,'PAP irregular clase IV','PAP_IV',0),(167,63,'Quiste en seno(s)','BC',0),(168,63,'Cáncer','CAN',0),(169,63,'Migraña','MIG',1),(170,63,'Otra condición pre-exsistente','OPC',1);
/*!40000 ALTER TABLE `patientsprecond` ENABLE KEYS */;
UNLOCK TABLES;


--
-- Table structure for table `patientsymptom`
--

DROP TABLE IF EXISTS `patientsymptom`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `patientsymptom` (
  `idSymp` int(11) NOT NULL AUTO_INCREMENT,
  `idQuiz` int(11) DEFAULT NULL,
  `symptom` varchar(50) DEFAULT NULL,
  `hasSymptom` int(11) DEFAULT NULL,
  PRIMARY KEY (`idSymp`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patientsymptom`
--

LOCK TABLES `patientsymptom` WRITE;
/*!40000 ALTER TABLE `patientsymptom` DISABLE KEYS */;
INSERT INTO `patientsymptom` VALUES (1,13,'CON',1),(2,13,'CPP',0),(3,13,'DIA',1),(4,13,'PP',1);
/*!40000 ALTER TABLE `patientsymptom` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pre_existing_conditions`
--

DROP TABLE IF EXISTS `pre_existing_conditions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pre_existing_conditions` (
  `idPreCond` int(11) NOT NULL AUTO_INCREMENT,
  `condition` varchar(45) NOT NULL,
  `abbr` varchar(45) NOT NULL,
  PRIMARY KEY (`idPreCond`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pre_existing_conditions`
--

LOCK TABLES `pre_existing_conditions` WRITE;
/*!40000 ALTER TABLE `pre_existing_conditions` DISABLE KEYS */;
INSERT INTO `pre_existing_conditions` VALUES (1,'Fibromas uterinos','UF'),(2,'Quiste en ovarios','OC'),(3,'PAP irregular clase I','PAP_I'),(4,'PAP irregular clase II','PAP_II'),(5,'PAP irregular clase III','PAP_III'),(6,'PAP irregular clase IV','PAP_IV'),(7,'Quiste en seno(s)','BC'),(8,'Cáncer','CAN'),(9,'Migraña','MIG'),(10,'Otra','OPC');
/*!40000 ALTER TABLE `pre_existing_conditions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `romeanswers`
--

DROP TABLE IF EXISTS `romeanswers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `romeanswers` (
  `idRomeQuiz` int(11) NOT NULL,
  `idRomeQuestion` int(11) NOT NULL,
  `answer` int(11) DEFAULT NULL,
  PRIMARY KEY (`idRomeQuiz`,`idRomeQuestion`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `romeanswers`
--

LOCK TABLES `romeanswers` WRITE;
/*!40000 ALTER TABLE `romeanswers` DISABLE KEYS */;
/*!40000 ALTER TABLE `romeanswers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `romechoices`
--

DROP TABLE IF EXISTS `romechoices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `romechoices` (
  `idRomeChoices` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `romeChoice` varchar(45) DEFAULT NULL,
  `romeOption` varchar(255) DEFAULT NULL,
  `value` int(11) DEFAULT NULL,
  PRIMARY KEY (`idRomeChoices`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `romechoices`
--

LOCK TABLES `romechoices` WRITE;
/*!40000 ALTER TABLE `romechoices` DISABLE KEYS */;
INSERT INTO `romechoices` VALUES (1,'A','0 - Nunca',0),(2,'A','1 - Menos de un día al mes',1),(3,'A','2 - Un día al mes',2),(4,'A','3 - Dos o tres días al mes',3),(5,'A','4 - Un día a la semana',4),(6,'A','5 - Más de un día a la semana',5),(7,'A','6 - Todos los días',6),(8,'B','0 - No',0),(9,'B','1 - Sí',1),(10,'B','2 - No aplica porque ya pasé la menopausia',0),(11,'C','0 - No',0),(12,'C','1 - Sí',1),(13,'D','0 - Nunca o rara vez',0),(14,'D','1 - Algunas veces',1),(15,'D','2 - A menudo',2),(16,'D','3 - Casi todo el tiempo',3),(17,'D','4 - Siempre',4),(18,'E','1 - Muy leve',1),(19,'E','2 - Leve',2),(20,'E','3 - Moderado',3),(21,'E','4 - Severo',4),(22,'E','5 - Muy severo',5),(23,'F','1 - Desde segundos hasta 20 minutos y luego desaparece completamente',1),(24,'F','2 - Más de 20 minutos y varios días o más de duración',2),(25,'G','0 - No se afecta por comer',0),(26,'G','1 - El dolor es peor luego de comer',1),(27,'G','2 - Menos dolor luego de comer',2);
/*!40000 ALTER TABLE `romechoices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `romedependencies`
--

DROP TABLE IF EXISTS `romedependencies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `romedependencies` (
  `disease` varchar(50) NOT NULL,
  `preDisease` varchar(50) NOT NULL,
  PRIMARY KEY (`disease`,`preDisease`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `romedependencies`
--

LOCK TABLES `romedependencies` WRITE;
/*!40000 ALTER TABLE `romedependencies` DISABLE KEYS */;
INSERT INTO `romedependencies` VALUES ('Functional Abdominal Pain Syndrome','Chronic Proctalgia'),('Functional Abdominal Pain Syndrome','Epigastric Pain Syndrome'),('Functional Abdominal Pain Syndrome','Proctalgia Fugax'),('Functional Bloating','Functional Constipation'),('Functional Bloating','Irritable Bowel Syndrome'),('Functional Constipation','Irritable Bowel Syndrome'),('Functional Defecation Disorders','Functional Constipation'),('Irritable Bowel Syndrome - Constipation','Irritable Bowel Syndrome'),('Irritable Bowel Syndrome - Diarrhea','Irritable Bowel Syndrome'),('Irritable Bowel Syndrome - Mixed','Irritable Bowel Syndrome'),('Irritable Bowel Syndrome - Unspecified','Irritable Bowel Syndrome');
/*!40000 ALTER TABLE `romedependencies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `romediagnosis`
--

DROP TABLE IF EXISTS `romediagnosis`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `romediagnosis` (
  `idDiagnosis` int(11) NOT NULL AUTO_INCREMENT,
  `disease` varchar(255) DEFAULT NULL,
  `diagnosis` text,
  `rome` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idDiagnosis`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `romediagnosis`
--

LOCK TABLES `romediagnosis` WRITE;
/*!40000 ALTER TABLE `romediagnosis` DISABLE KEYS */;
INSERT INTO `romediagnosis` VALUES (21,'Irritable Bowel Syndrome','Síndrome de Intestino Irritable (IBS)','III'),(22,'Irritable Bowel Syndrome - Constipation','Síndrome de Intestino Irritable con Estreñimiento (IBS-C)','III'),(23,'Irritable Bowel Syndrome - Diarrhea','Síndrome de Intestino Irritable con Diarrea (IBS-D)','III'),(24,'Irritable Bowel Syndrome - Mixed','Síndrome de Intestino Irritable Mixto (IBS-M)','III'),(25,'Irritable Bowel Syndrome - Unspecified','Síndrome de Intestino Irritable no específico (IBS-U)','III'),(26,'Functional Diarrhea','Diarrea Funcional','III'),(27,'Functional Constipation','Estreñimiento Funcional','III'),(28,'Functional Bloating','Hinchazón Funcional','III'),(29,'Functional Defecation Disorders','Problemas de Defecación Funcionales','III'),(30,'Epigastric Pain Syndrome','Síndrome de Dolor en el Epigastrio, de no cumplir con el criterio de dolor biliar, que no se puede determinar por este cuestionario ofrecido.','III'),(31,'Chronic Proctalgia','Proctalgia Crónica, de no cumplir con alguna de las siguientes causas de dolor rectal: isquemia, enfermedad inflamatoria del intestino, criptitis, absceso intramuscular y fisuras, hemorroides, prostatitis, y coccigodinia.','III'),(32,'Proctalgia Fugax','Proctalgia Fugaz','III'),(33,'Functional Abdominal Pain Syndrome','Síndrome de Dolor Abdominal Funcional, solo si el dolor que limita sus actividades no es fingido.','III'),(34,'Functional Gallbladder Sphincter Oddi Disorders','Trastornos de la Vesícula Biliar Funcional y del Esfínter de Oddi, solo si se excluyen otras enfermedades estructurales, que no se pueden determinar a través de este cuestionario, que expliquen los síntomas presentados.','III'),(35,'Functional Biliary SO Disorder','Trastorno Biliar SO Funcional, si se observa amilasa/lipasa normal.','III'),(36,'Functioanl Pancreatic SO Disorder','Trastorno Pancreático SO Funcional, si se observa amilasa/lipasa elevada.','III'),(37,'Aerophagia','Aerofagia, solamente si se puede observar objetivamente o medir que se está tragando aire.','III'),(38,'Chronic Idiopathic Nausea','Náusea Idiopática Crónica, si hay ausencia de anormalidades en endoscopía digestiva alta o una enfermedad metabólica que expliquen las náuseas.','III'),(39,'Functional Vomiting','Vómitos Funcionales, si la paciente no cumple con el criterio de: algún desorden alimenticio, del Trastorno de Rumiación, o alguna enfermedad psiquiátrica mayor según el DSM-IV.','III'),(40,'Cyclic Vomiting Syndrome','Síndrome del Vómito Cíclico','III'),(41,'Rumination Syndrome Adults','Síndrome de Rumiación en Adultos','III');
/*!40000 ALTER TABLE `romediagnosis` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `romeparameters`
--

DROP TABLE IF EXISTS `romeparameters`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `romeparameters` (
  `idOrder` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `step` int(11) DEFAULT NULL,
  `disease` varchar(50) DEFAULT NULL,
  `que` varchar(5) DEFAULT NULL,
  `cual` int(11) DEFAULT NULL,
  `boolValue` int(11) DEFAULT NULL,
  PRIMARY KEY (`idOrder`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `romeparameters`
--

LOCK TABLES `romeparameters` WRITE;
/*!40000 ALTER TABLE `romeparameters` DISABLE KEYS */;
INSERT INTO `romeparameters` VALUES (1,1,'Irritable Bowel Syndrome','Q',2,1),(2,1,'Irritable Bowel Syndrome','Q',5,0);
/*!40000 ALTER TABLE `romeparameters` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `romequestion`
--

DROP TABLE IF EXISTS `romequestion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `romequestion` (
  `idRomeQuestion` int(11) NOT NULL AUTO_INCREMENT,
  `romeQuestion` varchar(255) DEFAULT NULL,
  `romeChoice` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idRomeQuestion`)
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `romequestion`
--

LOCK TABLES `romequestion` WRITE;
/*!40000 ALTER TABLE `romequestion` DISABLE KEYS */;
INSERT INTO `romequestion` VALUES (1,'1. En los últimos 3 meses, ¿cuán seguido ha sentido dolor o incomodidad en cualquier área de su abdomen?','A'),(2,'2. ¿Solo tiene dolor en el abdomen (no incomodidad o una mezcla de dolor e incomodidad)?','D'),(3,'3. Para las mujeres: ¿Esta incomodidad o dolor abdominal ocurre solamente durante su sangrado menstrual y no en cualquier otro momento?','B'),(4,'4. Cuando tiene el dolor abdominal, ¿cuán seguido restringe o limita sus actividades diarias (trabajo, tareas del hogar, eventos sociales, etc.)?','D'),(5,'5. ¿Ha tenido este dolor o incomodidad abdominal por 6 meses o más? ','C'),(6,'6. ¿Cuán seguido el malestar o dolor abdominal mejoró o se detuvo luego de haber defecado?','D'),(7,'7. Cuando comenzó el malestar o dolor abdominal, ¿ha tenido que defecar más frecuente?','D'),(8,'8. Cuando comenzó el malestar o dolor abdominal, ¿ha tenido que defecar menos frecuente?','D'),(9,'9. Cuando comenzó el malestar o dolor abdominal, ¿sus heces fecales eran blandas?','D'),(10,'10. Cuando comenzó el malestar o dolor abdominal, ¿cuán seguido tenia heces fecales duras?','D'),(11,'11. En los últimos 3 meses, ¿cuán seguido ha tenido heces fecales sueltas, blandas o líquidas?','D'),(12,'12. En los últimos 3 meses, ¿por lo menos tres cuartos (¾) de sus heces fecales eran sueltas, blandas o líquidas?','C'),(13,'13. ¿Comenzó a tener heces fecales sueltas, blandas o líquidas hace más de 6 meses atrás?','C'),(14,'14. En los últimos 3 meses, ¿cuán seguido ha defecado menos de tres veces (0-2) en la semana?','D'),(15,'15. En los últimos 3 meses, ¿cuán seguido ha tenido heces duras o incómodas?','D'),(16,'16. En los últimos 3 meses, ¿cuán seguido siente tensión al defecar?','D'),(17,'17. En los últimos 3 meses, ¿cuán seguido siente que no defecó completamente?','D'),(18,'18. En los últimos 3 meses, ¿cuán seguido tiene la sensación de que las heces no pueden salir al defecar?','D'),(19,'19. En los últimos 3 meses, ¿cuán seguido ha tenido que presionar en o alrededor de su trasero, o remover heces para poder completar la defecación?','D'),(20,'20. En los últimos 3 meses, ¿cuán seguido ha tenido dificultad para dejar salir las heces fecales para poder completar la defecación?','D'),(21,'21. ¿Alguno de los síntomas presentados en las preguntas de estreñimiento comenzaron hace más de 6 meses?','C'),(22,'22. En los últimos 3 meses, ¿cuán seguido ha tenido hinchazón o distensión?','A'),(23,'23. ¿Sus síntomas de hinchazón o distensión comenzaron hace más de 6 meses atrás?','C'),(24,'24. En los últimos 3 meses, ¿cuán seguido se siente incómodamente lleno luego de una merienda de tamaño regular?','A'),(25,'25. ¿Ha tenido la incomodidad de sentirse lleno con las meriendas hace 6 meses o más?','C'),(26,'26. En los últimos 3 meses, ¿cuán seguido no ha podido terminar una merienda de tamaño regular?','A'),(27,'27. ¿Ha tenido la incapacidad de terminar una merienda de tamaño regular hace 6 meses o más?','C'),(28,'28. En los últimos 3 meses, ¿cuán seguido ha sentido dolor o incomodidad en el área central del pecho (no relacionado a problemas del corazón?','A'),(29,'29. En los últimos 3 meses, ¿cuán seguido ha tenido acidez (un dolor o malestar ardiente en el área de su pecho)?','A'),(30,'30. En los últimos 3 meses, ¿cuán seguido ha sentido dolor o ardor en el área central de su abdomen, por encima de su ombligo pero no en el pecho?','A'),(31,'31. ¿Ha tenido esta sensación de dolor o ardor por 6 meses o más?','C'),(32,'32. ¿Este dolor o ardor abdominal ocurrió y luego desapareció completamente durante el mismo día?','D'),(33,'33. Usualmente, ¿cuán severo fue el dolor o ardor en el área central de su abdomen, por encima de su ombligo?','E'),(34,'34. ¿Este dolor o ardor abdominal fue afectado por el consumo de comida?','G'),(35,'35. ¿Este dolor o ardor abdominal usualmente mejora o se detiene luego de la defecación o luego de flatulencia?','D'),(36,'36. Cuando este dolor o ardor abdominal comenzó, ¿usualmente tuvo un cambio en el número de veces que defecaba (ya sea más veces o menos veces)?','D'),(37,'37. Cuando este dolor o ardor abdominal comenzó, ¿usualmente tuvo heces fecales más blandas o más duras?','D'),(38,'38. En los últimos 3 meses, ¿cuán seguido ha tenido malestar, dolor o presión en el ano o en el recto mientras no estaba defecando?','A'),(39,'39. ¿Cuánto duró el malestar, dolor o presión en el ano o en el recto?','F'),(40,'40. ¿Tuvo dolor en el ano o el recto y desapareció completamente durante el mismo día?','C'),(41,'41. ¿El malestar, dolor o presión en el canal anal o en el recto comenzó hace más de 6 meses?','C'),(42,'42. En los últimos 3 meses, ¿cuán seguido tuvo dolor constante en el área central o derecha en la parte superior de su abdomen?','A'),(43,'43. ¿Este dolor constante en el área central o derecha de la parte superior de su abdomen duró 30 minutos o más?','D'),(44,'44.  ¿Este dolor constante en el área central o derecha de la parte superior de su abdomen pasó a ser severo y constante?','D'),(45,'45. ¿Este dolor constante en el área central o derecha de la parte superior de su abdomen se fue por completo durante episodios?','D'),(46,'46. ¿Cuán seguido este malestar o dolor en el área central o derecha de la parte superior de su abdomen mejoró o se detuvo mientas defecaba?','D'),(47,'47. ¿Este dolor constante en el área central o derecha de la parte superior de su abdomen le provocó detener sus actividades diarias, o causó que visitara un doctor urgentemente o fuera a una sala de emergencias?','D'),(48,'48. ¿Este dolor o ardor en el área central o derecha de la parte superior de su abdomen fue aliviado tomando antiácidos?','D'),(49,'49. ¿Cuán seguido este dolor o malestar en el área central o derecha de la parte superior de su abdomen se alivió al moverse o cambiar de posición?','D'),(50,'50. ¿Le han removido la vesícula biliar?','C'),(51,'51. ¿Cuán seguido ha tenido este dolor en el área central o derecha de la parte superior de su abdomen desde que le removieron la vesícula biliar?','D'),(52,'52. En los últimos 3 meses, ¿cuán seguido tiene náuseas molestosas?','A'),(53,'53. ¿Estas nauseas comenzaron hace más de 6 meses atrás?','C'),(54,'54. En los últimos 3 meses, ¿cuán seguido ha vomitado?','A'),(55,'55. ¿Ha tenido estos vómitos por 6 meses o más?','C'),(56,'56. ¿Se ha provocado usted misma el vómito?','D'),(57,'57. ¿Ha tenido vómitos en el pasado año que hayan ocurrido en episodios separados de unos cuantos días y luego se detienen?','D'),(58,'58. ¿Ha tenido por lo menos tres episodios de vómito durante el pasado año?','C'),(59,'59. En los últimos 3 años, ¿cuán seguido el cuerpo le ha devuelto la comida a su boca?','A'),(60,'60. ¿Ha tenido el problema de la comida devuelta a su boca por 6 meses o más?','C'),(61,'61. Usualmente, cuando la comida fue devuelta a su boca, ¿la dejó en su boca por un tiempo antes de tragársela o la escupió rápidamente?','D'),(62,'62. ¿Ha tenido nausea antes de que la comida llegue a su boca?','D'),(63,'63. En los últimos 3 meses, ¿cuán seguido tiene eructos molestosos?','A'),(64,'64. ¿Los eructos molestosos comenzaron hace más de 6 meses?','C');
/*!40000 ALTER TABLE `romequestion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `romequestionnaires`
--

DROP TABLE IF EXISTS `romequestionnaires`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `romequestionnaires` (
  `idRomeQuiz` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `idPatient` int(11) NOT NULL,
  `idQuiz` int(11) NOT NULL,
  PRIMARY KEY (`idRomeQuiz`,`idPatient`,`idQuiz`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `romequestionnaires`
--

LOCK TABLES `romequestionnaires` WRITE;
/*!40000 ALTER TABLE `romequestionnaires` DISABLE KEYS */;
/*!40000 ALTER TABLE `romequestionnaires` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `romesteps`
--

DROP TABLE IF EXISTS `romesteps`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `romesteps` (
  `idStep` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `steps` int(11) DEFAULT NULL,
  `disease` varchar(50) DEFAULT NULL,
  `method` varchar(50) DEFAULT NULL,
  `quantityN` int(11) DEFAULT NULL,
  PRIMARY KEY (`idStep`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `romesteps`
--

LOCK TABLES `romesteps` WRITE;
/*!40000 ALTER TABLE `romesteps` DISABLE KEYS */;
INSERT INTO `romesteps` VALUES (1,1,'Irritable Bowel Syndrome','NMORE',2),(2,1,'Irritable Bowel Syndrome','NMORE',2);
/*!40000 ALTER TABLE `romesteps` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `severity`
--

DROP TABLE IF EXISTS `severity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `severity` (
  `idSeverity` int(11) NOT NULL AUTO_INCREMENT,
  `idQuiz` int(11) DEFAULT NULL,
  `severity` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idSeverity`),
  KEY `idQuiz_idx` (`idQuiz`),
  CONSTRAINT `idQuiz` FOREIGN KEY (`idQuiz`) REFERENCES `patients` (`idQuiz`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `severity`
--

LOCK TABLES `severity` WRITE;
/*!40000 ALTER TABLE `severity` DISABLE KEYS */;
INSERT INTO `severity` VALUES (1,7,'75'),(2,8,'75'),(3,9,'75'),(4,10,'75'),(5,11,'75'),(6,12,'75'),(7,13,'75'),(8,38,'76'),(9,39,'76'),(10,40,'76'),(11,41,'76'),(12,42,'76');
/*!40000 ALTER TABLE `severity` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `symptoms`
--

DROP TABLE IF EXISTS `symptoms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `symptoms` (
  `idSymptom` int(11) NOT NULL AUTO_INCREMENT,
  `symptom` varchar(255) NOT NULL,
  `abbr` varchar(5) NOT NULL,
  PRIMARY KEY (`idSymptom`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `symptoms`
--

LOCK TABLES `symptoms` WRITE;
/*!40000 ALTER TABLE `symptoms` DISABLE KEYS */;
INSERT INTO `symptoms` VALUES (1,'Depresion','DEP'),(2,'Diarreas (u otro malestar gastrointestinal)','DIA'),(3,'Disminorrea (menstruacion dolorosa)','DME'),(4,'Dispareunia (dolor o irritacion durante el acto sexual','DPA'),(5,'Dolor al evacuar','PE'),(6,'Dolor al orinar','PU'),(7,'Dolor de cabeza','HEA'),(8,'Dolor de espalda','BP'),(9,'Dolor de piernas','LP'),(10,'Dolor musculo esqueletal','MP'),(11,'Dolor o sensibilidad vaginal','VS'),(12,'Dolor pelvico (durante el periodo)','PP'),(13,'Dolor pelvico cronico','CPP'),(14,'Dolor que la incapacita a llevar a cabo sus actividades regulares','DP'),(15,'Fatiga o baja energia','FE'),(16,'Hinchazon abdominal','AB'),(17,'Insomnio','INS'),(18,'Nauseas','NAU'),(19,'Quistes en los ovarios','OC'),(20,'Sangrado vaginal irregular','UVB'),(21,'Sangrado vaginal profuso','HVB'),(22,'Vomitos','VOM'),(23,'Otros sintomas','OS'),(24,'Estreñimiento','CON'),(25,'Mareos','MAR');
/*!40000 ALTER TABLE `symptoms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `symptomstodisease`
--

DROP TABLE IF EXISTS `symptomstodisease`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `symptomstodisease` (
  `idRelation` int(11) NOT NULL AUTO_INCREMENT,
  `symptom` varchar(5) DEFAULT NULL,
  `disease` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idRelation`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `symptomstodisease`
--

LOCK TABLES `symptomstodisease` WRITE;
/*!40000 ALTER TABLE `symptomstodisease` DISABLE KEYS */;
INSERT INTO `symptomstodisease` VALUES (1,'CON','Irritable Bowel Syndrome');
/*!40000 ALTER TABLE `symptomstodisease` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-05-06  1:12:45
