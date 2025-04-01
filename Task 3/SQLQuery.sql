-- Create database
CREATE DATABASE ClientDates;
GO

USE ClientDates;
GO

-- Create table
CREATE TABLE Dates (
    Id BIGINT,
    Dt DATE
);
GO

-- Populate table
INSERT INTO Dates (Id, Dt) VALUES
(1, '2021-01-01'),
(1, '2021-01-10'),
(1, '2021-01-30'),
(2, '2021-01-15'),
(2, '2021-01-30');
GO

-- Solution 1
SELECT d1.Id, d1.Dt AS Sd, MIN(d2.Dt) AS Ed
FROM Dates d1
JOIN Dates d2 
ON d1.Id = d2.Id AND d1.Dt < d2.Dt
GROUP BY d1.Id, d1.Dt
HAVING MIN(d2.Dt) IS NOT NULL
ORDER BY d1.Id, Sd;

-- Solution 2
WITH DateIntervals AS 
(
	SELECT Id, Dt AS Sd, LEAD(Dt) OVER (PARTITION BY Id ORDER BY Dt) AS Ed
	FROM Dates
)
SELECT Id, Sd, Ed
FROM DateIntervals
WHERE Ed IS NOT NULL