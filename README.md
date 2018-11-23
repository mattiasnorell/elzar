# Feedbag
> If you like good food, good fun  and a whole lot of crazy crap on the walls  then come on down to uncle Matt's Family Feedbag.

## Before we start
This application heavily rely on webscraping which may or may not be legal where you are from. And even if webscraping may be legal, sharing copyrighted material is not so please don't use this application to create public sites that use content not created by you.

## API endpoints
| Endpoint | Method | Properties | Description |
| :------------- | :------------- |:------------- |:------------- |
| /recepies/ | GET |  | Get all recepies |
| /recepies/:id | GET | Recepie id | Get a single recepie |
| /recepies/ | POST | CreateRecipeDto | Tell the api to get a recepie from an external site |
| /recepies/:id | PUT | UpdateRecipeDto | Update an existing recepie |
| /recepies/:id | DELETE | Recepie id | Delete a recepie |
