CREATE TABLE `Recipes` (
                    `Id`	INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                    `Title`	TEXT(100),
                    `Image`	TEXT(255),
                    `Description`	TEXT(500),
                    `SourceUrl` TEXT(255),
                    `CreatedAtUtc`	INTEGER,
                    `UpdatedAtUtc`	INTEGER
                    )

CREATE TABLE `HowToSteps` (
                    `Id`	INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                    `RecepieId` INTEGER,
                    `Step`	TEXT,
                    )

CREATE TABLE `Ingredients` (
                    `Id`	INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                    `RecepieId` INTEGER,
                    `Amount`	TEXT(100),
                    `Unit`	TEXT(100),
                    `Name`	TEXT(100)
                    )