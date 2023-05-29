SELECT P.FarmerID, U.FirstName AS "FarmerName", U.Surname AS "FarmerSurname", P.ProductName, P.ProductPrice, P.DateAdded, PT.ProductType, S.StockAmount
FROM (([dbo].[Product] P
INNER JOIN [dbo].[Product_Type] PT ON P.ProductTypeID = PT.ProductTypeID)
INNER JOIN [dbo].[Stock] S ON S.ProductID = P.ProductID
INNER JOIN [dbo].[Farmer] F ON F.FarmerID = P.FarmerID
INNER JOIN [dbo].[User] U ON U.UserID = F.UserID);

SELECT P.FarmerID, U.FirstName AS "FarmerName", U.Surname AS "FarmerSurname", P.ProductName, P.ProductPrice, P.DateAdded, PT.ProductType, S.StockAmount
FROM (([dbo].[Product] P
INNER JOIN [dbo].[Product_Type] PT ON P.ProductTypeID = PT.ProductTypeID)
INNER JOIN [dbo].[Stock] S ON S.ProductID = P.ProductID
INNER JOIN [dbo].[Farmer] F ON F.FarmerID = P.FarmerID
INNER JOIN [dbo].[User] U ON U.UserID = F.UserID)
WHERE
P.FarmerID = 'FRM1';

SELECT P.FarmerID, U.FirstName AS "FarmerName", U.Surname AS "FarmerSurname", P.ProductName, P.ProductPrice, P.DateAdded, PT.ProductType, S.StockAmount
FROM (([dbo].[Product] P
INNER JOIN [dbo].[Product_Type] PT ON P.ProductTypeID = PT.ProductTypeID)
INNER JOIN [dbo].[Stock] S ON S.ProductID = P.ProductID
INNER JOIN [dbo].[Farmer] F ON F.FarmerID = P.FarmerID
INNER JOIN [dbo].[User] U ON U.UserID = F.UserID)
WHERE
P.DateAdded BETWEEN '2023-05-26' AND '2023-06-01';