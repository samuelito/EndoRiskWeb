-- MySQL dump 10.13  Distrib 5.6.23, for Win64 (x86_64)
--
-- Host: localhost    Database: endorisk
-- ------------------------------------------------------
-- Server version	5.6.23-log

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
  `password` varchar(25) NOT NULL,
  `firstname` varchar(50) DEFAULT NULL,
  `lastname` varchar(50) DEFAULT NULL,
  `subadmin` int(10) unsigned NOT NULL,
  PRIMARY KEY (`idAdmin`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `administrator`
--

LOCK TABLES `administrator` WRITE;
/*!40000 ALTER TABLE `administrator` DISABLE KEYS */;
INSERT INTO `administrator` VALUES (1,'samuel.feliciano@upr.edu','samuel','Samuel','Feliciano',0),(4,'luz.gonzalez@upr.edu','luz','Luz','Gonzalez',1),(5,'eddie.perez@upr.edu','eddie','Eddie','Perez',1),(6,'saylisse.davila@upr.edu','saylisse','Saylisse','Davila',1),(7,'samuel@upr.edu','samuel','Sammy','Feliciano',1);
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
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comment`
--

LOCK TABLES `comment` WRITE;
/*!40000 ALTER TABLE `comment` DISABLE KEYS */;
INSERT INTO `comment` VALUES (1,'Test','Testing Feedback','samuel.feliciano@upr.edu','2015-03-31 06:08:00'),(2,'Test 2','Testing Comment 2','testing@gmail.com','2015-03-31 06:52:00'),(3,'Test 3','Testing DateTime function','samuel.feliciano@upr.edu','2015-04-02 02:49:22'),(5,'Test 5','Test','samuel.feliciano@upr.edu','2015-04-10 11:17:57');
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `diseases`
--

LOCK TABLES `diseases` WRITE;
/*!40000 ALTER TABLE `diseases` DISABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=75 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `endoanswers`
--

LOCK TABLES `endoanswers` WRITE;
/*!40000 ALTER TABLE `endoanswers` DISABLE KEYS */;
INSERT INTO `endoanswers` VALUES (1,13,0,'23'),(2,36,1,'Caucasico'),(3,37,1,'Hispano'),(4,37,2,'23'),(5,37,3,'11'),(6,37,4,'27'),(7,37,5,'12'),(8,37,6,'Si'),(9,37,7,'3'),(10,37,8,'2'),(11,37,9,'Si'),(12,37,10,'No'),(13,37,11,'1'),(14,37,12,'1'),(15,37,13,'No'),(16,37,14,'0'),(17,37,15,'Si'),(18,37,16,'No'),(19,37,17,'13'),(20,37,18,'No'),(21,37,19,' '),(22,37,20,'No'),(23,37,21,'No'),(24,37,22,'No'),(25,37,23,'n/a'),(26,37,24,'n/a'),(27,38,1,'Asiatico'),(28,38,2,'34'),(29,38,3,'13'),(30,38,4,'11'),(31,38,5,'7'),(32,38,6,'Si'),(33,38,7,'3'),(34,38,8,'4'),(35,38,9,'Si'),(36,38,10,'Si'),(37,38,11,'1'),(38,38,12,'1'),(39,38,13,'Si'),(40,38,14,'1'),(41,38,15,'No'),(42,38,16,'No'),(43,38,17,'13'),(44,38,18,'Si'),(45,38,19,'Madre'),(46,38,20,'No'),(47,38,21,'No'),(48,38,22,'n/a'),(49,38,23,'n/a'),(50,38,24,'n/a'),(51,39,1,'Multirracial'),(52,39,2,' 55'),(53,39,3,'13'),(54,39,4,' 13'),(55,39,5,' 13'),(56,39,6,'Si'),(57,39,7,' 4'),(58,39,8,' 10'),(59,39,9,''),(60,39,10,''),(61,39,11,' '),(62,39,12,' '),(63,39,13,''),(64,39,14,' '),(65,39,15,''),(66,39,16,''),(67,39,17,' '),(68,39,18,''),(69,39,19,' '),(70,39,20,''),(71,39,21,''),(72,39,22,' '),(73,39,23,' '),(74,39,24,' ');
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
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `endochoices`
--

LOCK TABLES `endochoices` WRITE;
/*!40000 ALTER TABLE `endochoices` DISABLE KEYS */;
INSERT INTO `endochoices` VALUES (1,'A','Afroamericano'),(2,'A','Asiatico'),(3,'A','Caucasico'),(4,'A','Hispano'),(5,'A','Multirracial'),(6,'A','Amerindio o Nativo de Alaska'),(7,'A','Prefiero no contestar'),(8,'B','Si'),(9,'B','No'),(10,'C','Si'),(11,'C','No'),(12,'C','No Aplica');
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
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `endoquestion`
--

LOCK TABLES `endoquestion` WRITE;
/*!40000 ALTER TABLE `endoquestion` DISABLE KEYS */;
INSERT INTO `endoquestion` VALUES (1,'Origen étnico','EBA','A'),(2,'¿Cuál es su edad? (años)','AGE',''),(3,'¿A qué edad tuvo su primer período?','AAM',''),(4,'¿Cuál es el tiempo promedio entre períodos? (días)','DBP',''),(5,'¿Cuál es la duración promedio de su período? (días)','PL',''),(6,'¿Actualmente fuma o ha fumado?','SM','B'),(7,'Si contestó que fuma o ha fumado, indique el tiempo que lleva fumando (años fumando)','YS',''),(8,'¿Cuántos cigarrillos fuma al día?','CI',''),(9,'¿Ha tenido problemas para quedar embarazada?','PGP','C'),(10,'Si contestó que ha tenido problemas para quedar embarazada, indique si ha estado o se encuentra recibiendo tratamiento m édico para infertilidad','IT','C'),(11,'¿Cuántas veces ha quedado embarazada?','TP',''),(12,'¿Cuántos partos ha tenido?','PA',''),(13,'¿Ha tenido abortos espontáneos?','SA','B'),(14,'Indique la cantidad de abortos espontáneos','NA',''),(15,'¿Ha utilizado pastillas anticonceptivas en el pasado?','OCP','B'),(16,'¿Está utilizando pastillas anticonceptivas actualmente?','CC','C'),(17,'¿A qué edad comenzaron los síntomas?','AWSS',''),(18,'¿Posee historial familiar de mujeres con endometriosis?','SI','B'),(19,'Si contestó que tiene familiar con endometriosis, indique la persona o personas ','SIA',''),(20,'¿A recibido algún diagnóstico de endometriosis?','ED','B'),(21,'De haber recibido un diagnóstico de endometriosis, ¿fue diagnosticada a través de una cirugía? ','SU','C'),(22,'De haber recibido un diagnóstico de endometriosis, indique el nivel de severidad ','SE',''),(23,'¿Que tipo de tratamiento le recomendó su médico?','ETR',''),(24,'Si contentó otro en la pregunta anterior, por favor indique el tratamiento','OETR','');
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
  PRIMARY KEY (`idQuiz`,`idPatient`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patients`
--

LOCK TABLES `patients` WRITE;
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
INSERT INTO `patients` VALUES (1,5,56.78,'Yes','2015-04-07 18:26:04'),(2,5,56.78,'Yes','2015-04-07 18:30:52'),(3,5,56.78,'Yes','2015-04-07 19:50:38'),(4,2,56.78,'Yes','2015-04-07 20:02:37'),(5,3,56.78,'Yes','2015-04-07 20:05:07'),(6,2,56.78,'Yes','2015-04-07 20:19:29'),(7,1,56.78,'Yes','2015-04-07 20:21:19'),(8,0,56.78,'Yes','2015-04-07 20:48:32'),(9,0,56.78,'Yes','2015-04-07 20:53:26'),(10,2,56.78,'Yes','2015-04-07 21:01:42'),(11,4,56.78,'Yes','2015-04-07 21:05:21'),(12,3,56.78,'Yes','2015-04-07 21:11:56'),(13,23,56.78,'Yes','2015-04-07 21:15:04'),(14,1,56.78,'Yes','2015-04-07 21:32:19'),(15,3,56.78,'Yes','2015-04-07 21:34:48'),(16,12,56.78,'Yes','2015-04-07 21:37:51'),(17,4,56.78,'Yes','2015-04-07 21:42:19'),(18,4,56.78,'Yes','2015-04-07 21:51:43'),(19,2,56.78,'Yes','2015-04-07 21:59:59'),(20,3,56.78,'Yes','2015-04-07 22:02:58'),(21,3,56.78,'Yes','2015-04-07 22:04:32'),(22,3,56.78,'Yes','2015-04-07 22:04:54'),(23,3,56.78,'Yes','2015-04-07 22:07:59'),(24,3,56.78,'Yes','2015-04-08 01:28:06'),(25,1,56.78,'Yes','2015-04-08 01:34:38'),(26,2,56.78,'Yes','2015-04-08 01:41:58'),(27,2,56.78,'Yes','2015-04-08 01:43:04'),(28,2,56.78,'Yes','2015-04-08 01:46:06'),(29,2,56.78,'Yes','2015-04-08 01:52:16'),(30,2,56.78,'Yes','2015-04-08 01:55:42'),(31,1,56.78,'Yes','2015-04-08 01:58:14'),(32,1,56.78,'Yes','2015-04-08 02:03:09'),(33,23,56.78,'Yes','2015-04-08 02:11:24'),(34,23,56.78,'Yes','2015-04-08 02:13:18'),(35,23,56.78,'Yes','2015-04-08 02:23:46'),(36,4,56.78,'Yes','2015-04-08 02:31:11'),(37,23,56.78,'Yes','2015-04-08 02:42:32'),(38,8,56.78,'Yes','2015-04-10 00:27:15'),(39,20,56.78,'Yes','2015-04-10 11:11:47');
/*!40000 ALTER TABLE `patients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patientsymptom`
--

DROP TABLE IF EXISTS `patientsymptom`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `patientsymptom` (
  `idPatient` int(11) NOT NULL,
  `symptom` varchar(50) NOT NULL,
  `hasSymptom` int(11) DEFAULT NULL,
  PRIMARY KEY (`idPatient`,`symptom`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patientsymptom`
--

LOCK TABLES `patientsymptom` WRITE;
/*!40000 ALTER TABLE `patientsymptom` DISABLE KEYS */;
/*!40000 ALTER TABLE `patientsymptom` ENABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `romechoices`
--

LOCK TABLES `romechoices` WRITE;
/*!40000 ALTER TABLE `romechoices` DISABLE KEYS */;
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
  `disease` varchar(50) DEFAULT NULL,
  `diagnosis` varchar(255) DEFAULT NULL,
  `rome` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idDiagnosis`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `romediagnosis`
--

LOCK TABLES `romediagnosis` WRITE;
/*!40000 ALTER TABLE `romediagnosis` DISABLE KEYS */;
/*!40000 ALTER TABLE `romediagnosis` ENABLE KEYS */;
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `romequestion`
--

LOCK TABLES `romequestion` WRITE;
/*!40000 ALTER TABLE `romequestion` DISABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `severity`
--

LOCK TABLES `severity` WRITE;
/*!40000 ALTER TABLE `severity` DISABLE KEYS */;
INSERT INTO `severity` VALUES (1,7,'75'),(2,8,'75'),(3,9,'75'),(4,10,'75'),(5,11,'75'),(6,12,'75'),(7,13,'75'),(8,38,'76'),(9,39,'76');
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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-04-11 12:15:38
