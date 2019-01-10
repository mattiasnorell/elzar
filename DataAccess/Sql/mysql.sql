CREATE TABLE `CookingProcedures` (
	`Id`	INTEGER PRIMARY KEY AUTO_INCREMENT UNIQUE,
	`RecipeId`	INTEGER,
	`Step`	TEXT
);

CREATE TABLE `Ingredients` (
	`Id`	INTEGER PRIMARY KEY AUTO_INCREMENT UNIQUE,
	`RecipeId`	INTEGER,
	`Amount`	TEXT ( 100 ),
	`Unit`	TEXT ( 100 ),
	`Name`	TEXT ( 100 )
);

CREATE TABLE `Recipes` (
	`Id`	INTEGER PRIMARY KEY AUTO_INCREMENT UNIQUE,
	`Title`	TEXT ( 100 ),
	`Image`	TEXT ( 255 ),
	`Description`	TEXT ( 500 ),
	`SourceUrl`	TEXT ( 255 ),
	`Tags`	TEXT,
	`CreatedAtUtc`	DATETIME,
	`UpdatedAtUtc`	DATETIME
);

