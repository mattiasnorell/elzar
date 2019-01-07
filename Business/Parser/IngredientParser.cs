using System.Collections.Generic;
using System.Linq;
using Elzar.DataAccess.Entites;

namespace Elzar.Business.Parser{
    public class IngredientParser : IIngredientParser
    {
        private readonly string[] validUnits = {
            "mg",
            "g",
            "gram",
            "hg",
            "hekto",
            "kg",
            "kilo",
            "ml",
            "cl",
            "dl",
            "l",
            "krm",
            "tsk",
            "msk",
            "st"
        };

        private readonly Dictionary<string,string> amountReplaceCharMap = new Dictionary<string,string>{
           {"½", "0.5"},
           {"&#189;", "0.5"}
        };

        private string ParseAmount(string input){
            if(amountReplaceCharMap.ContainsKey(input)){
                return amountReplaceCharMap[input];
            }

            return input;
        }

        public bool IsIngredientList(string input)
        {
            var totalOccurances = 0;
            var unitSplit = input.Split(' ').Distinct();
            foreach(var unit in validUnits){

                totalOccurances += unitSplit.Count(x => x == unit);
            }

            return totalOccurances >= 1;
        }

        public IngredientParserResult Parse(string input)
        {
            if(string.IsNullOrEmpty(input)){
                return null;
            }
            
            var ingredient = new IngredientParserResult();
            var split = input.Split(' ').Where(e => !string.IsNullOrWhiteSpace(e)).ToArray();
            
            if(split.Length > 1 && split[0] == "0"){
                ingredient.Name = split[1];
            }else if(split.Length > 1){
                var hasUnit = validUnits.Contains(split[1]);

                if(hasUnit){
                    ingredient.Amount = ParseAmount(split[0]);
                    ingredient.Unit = split[1];
                    ingredient.Name = string.Join(" ", split.Skip(2));
                }else{
                    ingredient.Amount = ParseAmount(split[0]);
                    ingredient.Name = string.Join(" ", split.Skip(1));
                }
            }else{
                ingredient.Name = input;
            }

            return ingredient;
        }
    }
}