USE master; 
DROP DATABASE IF EXISTS Hostels; 
CREATE DATABASE Hostels;
GO 

USE Hostels;
GO



CREATE TABLE Nomer(
	id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Square] float NOT NULL,
	[id_Type] int NOT NULL,
	Status varchar(10) NOT NULL,
	End_Time Datetime
);
GO

CREATE TABLE Type(
	id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] varchar(40) NOT NULL,
);
GO

CREATE TABLE Orders(
id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
[Date] Datetime NOT NULL
)
GO

CREATE TABLE ���������(
 id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
 ������� varchar(30) NOT NULL,
 ��� varchar(30) NOT NULL,
 �������� varchar(30) NOT NULL
)
GO

CREATE TABLE ������������(
	id int Identity(1,1) PRIMARY KEY NOT Null,
	������� varchar(30) NOT NULL,
	��� varchar(30) NOT NULL,
	�������� varchar(30) NOT NULL,
	Phone varchar(20) NOT NULL,
	[Password] varchar(30) NOT NULL
)
GO






ALTER TABLE Orders
ADD id_Nomer int NOT NULL
GO

ALTER TABLE Orders
ADD id_������������ int NOT NULL
GO

ALTER TABLE Orders
WITH CHECK ADD CONSTRAINT FK_Orders_id_Nomer FOREIGN KEY(id_nomer)
REFERENCES Nomer(id)
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE Orders
WITH CHECK ADD CONSTRAINT FK_Orders_id_������������ FOREIGN KEY(id_������������)
REFERENCES ������������(id)
ON UPDATE CASCADE
ON DELETE CASCADE
GO


ALTER TABLE Nomer
  ADD FOREIGN KEY (id_Type) REFERENCES Type(id)
GO



INSERT INTO Type(Name) VALUES 
('�������������'),
('����'),
('������������'),
('�����������');
GO


INSERT INTO Nomer(Status, Square, id_Type, End_Time) VALUES 
('Free',140.9,  1, null),
('Free',80.2,  2, null),
('Booked',80.2,  2, '31-05-20 02:30'),
('Free',40.2,  4, null),
('Busy',40.2,  4, '08-06-20 14:00'),
('Free',40.2,  4, null),
('Free',40.2,  4, null),
('Busy',40.2,  4, '07-06-20 12:00'),
('Busy',40.2,  4, '05-06-20 15:00'),
('Free',40.2,  4, null),
('Booked',40.2,  4 , '31-05-20 02:30'),
('Free',70.4,  3, null),
('Busy',40.2,  4, '03-06-20 14:00'),
('Booked',40.2, 4, '31-05-20 02:30'),
('Free',40.2,  4, null),
('Free',40.2,  4, null),
('Busy',140.9,  1, '04-06-20 11:00'),
('Free',70.4,  3, null),
('Busy',40.2,  4, '02-06-20 4:00');
GO

INSERT INTO ���������(�������, ���, ��������) VALUES 
('��������','������','����������'),
('���������','������','�����������'),
('C������','�������','���������'),
('�������','�������','�������'),
('���������','������','���������'),
('��������','�����','������')
GO
INSERT INTO ������������(�������, ���, ��������,Phone, Password) VALUES 
('��������','�����','���������','+375259436789' ,'534ahuAkbar'),
('���������','������','�������������','+375259906789' ,'534a4324kbar'),
('�����','��������','���������','+375259436723' ,'5654fwe'),
('�����','�����','�����������','+375253436789' ,'345ger'),
('��������','�������','�������','+375256436789' ,'23463htkbar'),
('����������','����������','�����������','+375259489789','5461fh5'),
('���������','������','�����������','+375259435489','53562rf'),
('��������','������','��������������','+375259786789','hgfj43'),
('������','������','���������','+375332336789','ghrw53'),
('��������','������','�����������','+375442336789','423gey5'),
('����������','�������','�����������','+375335636789' ,'746g3fs'),
('�������','�������','�����������','+375446434389' ,'55f3r'),
('���������','�����','������','+375255336789','234kbar'),
('�������','����','�����������', '+375445836789',  '535tghuAkbar'),
('����������','����','�����������','+375336736789', '453kbar'),
('�������','������','�����������','+375254566789', 'Aeyr'),
('��������','������','�����������','+375259486489','yeAkbar'),
('�����','������','�����������', '+375259436619', 'wtbar,48.5'),
('����','������','�����������','+375449436149', 'Aerrtar'),
('�����','������','�����������','+375449716789', 'AgsuAkbar');
GO


