USE dmaj0916_197331
--Users Table
go
CREATE TABLE Users(
userID int IDENTITY(1,1) PRIMARY KEY,
username varchar(50) NOT NULL,
salt uniqueIdentifier NOT NULL,
passwordHash binary(64) NOT NULL,
email varchar(100) NOT NULL,
nickname varchar(50) NOT NULL,
);