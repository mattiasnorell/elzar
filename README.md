# Elzar
> Hey, look at this crowd. You guys gotta try the pasta. It's got a real nice profit margin. Bam!

## Before we start
This application heavily rely on webscraping which may or may not be legal where you are from. And even if webscraping may be legal, sharing copyrighted material is not so please don't use this application to create public sites that use content not created by you.

## API endpoints
| Endpoint | Method | Properties | Description |
| :------------- | :------------- |:------------- |:------------- |
| /recipes/ | GET |  | Get all recipes |
| /recipes/:id | GET | Recipe id | Get a single recipe |
| /recipes/ | POST | CreateRecipeDto | Tell the api to get a recipe from an external site |
| /recipes/:id | PUT | UpdateRecipeDto | Update an existing recipe |
| /recipes/:id | DELETE | Recipe id | Delete a recipe |
