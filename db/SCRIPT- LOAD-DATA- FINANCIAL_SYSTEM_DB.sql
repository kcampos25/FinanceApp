USE FINANCIAL_SYSTEM_DB;
GO

/** Card_Types  */

INSERT INTO Card_Types (card_type_code,description,created_by)
VALUES('CRED','Credit','Admin')

INSERT INTO Card_Types (card_type_code,description,created_by)
VALUES('DEBT','Debit','Admin')


/** Banks  */

INSERT INTO Banks(description,created_by)
VALUES('Banco Nacional','Amin')

INSERT INTO Banks(description,created_by)
VALUES('BAC','Amin')



/** currencies  */

INSERT INTO Currencies(description,created_by)
VALUES('DOL','Amin')

INSERT INTO Currencies(description,created_by)
VALUES('COL','Amin')


