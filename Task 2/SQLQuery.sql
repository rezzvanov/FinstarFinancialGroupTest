-- Create database
CREATE DATABASE ClientManagement;
GO

USE ClientManagement;
GO

-- Create tables
CREATE TABLE Clients (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    ClientName NVARCHAR(200) NOT NULL
);

CREATE TABLE ClientContacts (
    Id BIGINT PRIMARY KEY IDENTITY(1,1),
    ClientId BIGINT NOT NULL,
    ContactType NVARCHAR(255) NOT NULL,
    ContactValue NVARCHAR(255) NOT NULL,
    FOREIGN KEY (ClientId) REFERENCES Clients(Id)
);

-- Populate tables
INSERT INTO Clients (ClientName) VALUES 
('Microsoft'),
('Apple'),
('Amazon'),
('Google');

INSERT INTO ClientContacts (ClientId, ContactType, ContactValue) VALUES
(1, 'Email', 'testsdf@gmail.com'),
(1, 'Phone', '+5334534'),
(1, 'Telegram', '+58604'),
(2, 'Email', 'fdgdf@gmail.com'),
(2, 'Phone', '+2344435'),
(3, 'Email', 'jmyjvmkc@gmail.com'),
(3, 'Phone', '+943342'),
(4, 'Email', 'noini@gmail.com'),
(4, 'Phone', '+10847');

-- Query 1
SELECT c.ClientName, COUNT(cc.Id) AS ContactCount
FROM Clients c
LEFT JOIN ClientContacts cc 
ON c.Id = cc.ClientId
GROUP BY c.ClientName;

-- Query 2
SELECT c.ClientName, COUNT(cc.Id) AS ContactCount
FROM Clients c
JOIN ClientContacts cc ON c.Id = cc.ClientId
GROUP BY c.ClientName
HAVING COUNT(cc.Id) > 2;