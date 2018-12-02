--Oksana Blagutin
--2018-11-18

PRINT ''
PRINT 'Create databes TraineeDb'
PRINT ''

USE Master
SET NOCOUNT ON
GO

IF EXISTS (SELECT * FROM Sysdatabases WHERE name = 'TraineeDb')
	BEGIN
		ALTER DATABASE TraineeDb 
		SET SINGLE_USER
		WITH ROLLBACK IMMEDIATE;
 
		--DROP DB
		DROP DATABASE [TraineeDb]
	END
GO


--------------------------------------------------------------------------
--Create databes TraineeDb
----------------------------------------------------------------------------
CREATE DATABASE [TraineeDb];
GO

PRINT 'Database TraineeDb is created'

USE [TraineeDb];
GO


PRINT ''
PRINT '****** CREATE TABLE Subject ******'
PRINT ''

------------------------------------------------------------------------------
----Create Table WellType
------------------------------------------------------------------------------
CREATE TABLE [Subject] (    
	[SubjectCode] nvarchar(5) NOT NULL CONSTRAINT Subject_PK PRIMARY KEY,
	[Name] nvarchar(20) NOT NULL           
);
GO


PRINT ''
PRINT '****** CREATE TABLE Test ******'
PRINT ''
----------------------------------------------------------------------------
--Create Table Test
----------------------------------------------------------------------------
CREATE TABLE [Test] (
    [TestId] int NOT NULL IDENTITY(1,1) CONSTRAINT Test_PK PRIMARY KEY,
	[Name] nvarchar(20) NOT NULL,  
    [Descrition] nvarchar(150) NOT NULL	
);
GO

PRINT ''
PRINT '****** CREATE TABLE TestSubject ******'
PRINT ''
----------------------------------------------------------------------------
--Create Table TestSubject
----------------------------------------------------------------------------
CREATE TABLE [TestSubject] (
    [TestSubjectId] int NOT NULL IDENTITY(1,1) CONSTRAINT TestSubject_PK PRIMARY KEY,
	[TestId] int NOT NULL,  
    [SubjectCode] nvarchar(5) NOT NULL,
	CONSTRAINT TestSubject_Test_FK FOREIGN KEY(TestId) REFERENCES [Test](TestId) ON DELETE CASCADE,
	CONSTRAINT TestSubject_Subject_FK FOREIGN KEY(SubjectCode) REFERENCES [Subject](SubjectCode) ON DELETE CASCADE
);
GO

PRINT ''
PRINT '****** CREATE TABLE Trainee ******'
PRINT ''
------------------------------------------------------------------------------
----Create Table Well
------------------------------------------------------------------------------
CREATE TABLE [Trainee] (	 
    [TraineeId] int NOT NULL IDENTITY(1,1) CONSTRAINT Trainee_PK PRIMARY KEY,	
    [TraineeName] nvarchar(20) NOT NULL	
);
GO


PRINT ''
PRINT '****** CREATE TABLE TraineeTest ******'
PRINT ''
----------------------------------------------------------------------------
--Create Table Test
----------------------------------------------------------------------------
CREATE TABLE [TraineeTest] (
	[TraineeTestId] int NOT NULL IDENTITY(1,1) CONSTRAINT TraineeTest_PK PRIMARY KEY,
    [TestId] int NOT NULL,
	[TraineeId] int NOT NULL,
	[TestStatus] varchar(10) NULL CHECK(TestStatus IN('Pass', 'Failed')),
	CONSTRAINT TraineeTest_Test_FK FOREIGN KEY(TestId) REFERENCES [Test](TestId) ON DELETE CASCADE,
	CONSTRAINT TraineeTest_Trainee_FK FOREIGN KEY(TraineeId) REFERENCES [Trainee](TraineeId) ON DELETE CASCADE
);
GO

--drop table TraineeTest

INSERT INTO [Subject] VALUES('MATH', 'Math')
INSERT INTO [Subject] VALUES('HIS', 'History')
INSERT INTO [Subject] VALUES('GEO', 'Geography')

INSERT INTO Test VALUES('TestTerm1', 'Test Term 1')
INSERT INTO Test VALUES('TestTerm2', 'Test Term 2')
INSERT INTO Test VALUES('TestFinal', 'Final Test')

INSERT INTO TestSubject VALUES(1, 'GEO')
INSERT INTO TestSubject VALUES(1, 'HIS')
INSERT INTO TestSubject VALUES(2, 'MATH')
INSERT INTO TestSubject VALUES(3, 'MATH')
INSERT INTO TestSubject VALUES(3, 'GEO')
INSERT INTO TestSubject VALUES(3, 'HIS')


INSERT INTO Trainee VALUES('Tom B')
INSERT INTO Trainee VALUES('Balu M')
INSERT INTO Trainee VALUES('Rita F')

INSERT INTO TraineeTest VALUES(1, 1, 'Pass')
INSERT INTO TraineeTest VALUES(2, 1, 'Failed')
INSERT INTO TraineeTest VALUES(1, 2, 'Pass')
INSERT INTO TraineeTest VALUES(2, 2, 'Pass')
INSERT INTO TraineeTest VALUES(3, 2, 'Pass')
INSERT INTO TraineeTest VALUES(1, 3, 'Pass')
INSERT INTO TraineeTest VALUES(2, 3, 'Pass')
INSERT INTO TraineeTest VALUES(3, 3, 'Failed')


--  TraineeName,  Test.Name, [Subject].Name from TraineeTest
select 
 '{ traineeName: ''' + TraineeName + ''', testName: ''' + Test.Name + ''', subjectName:	''' + [Subject].Name + ''' },'
 from TraineeTest
inner join Test on Test.TestId = TraineeTest.TestId
inner join Trainee on TraineeTest.TraineeId = Trainee.TraineeId
inner join TestSubject on TestSubject.TestId = Test.TestId
inner join [Subject] on TestSubject.SubjectCode = [Subject].SubjectCode
