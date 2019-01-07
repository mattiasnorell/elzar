using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        private bool IsAmountValid(string input){
            var isNumericWithSpecialChars = Regex.IsMatch(input, @"^[0-9-–\.\,]+$");
            var hasReplaceableCharacters = amountReplaceCharMap.ContainsKey(input);
            double parseResult;
            var isParseble = double.TryParse(input, out parseResult);

            return isNumericWithSpecialChars || hasReplaceableCharacters || isParseble;
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
            }else if(split.Length > 1 && IsAmountValid(split[0])){
                var hasUnit = validUnits.Contains(split[1]);

                if(hasUnit){
                    ingredient.Amount = ParseAmount(split[0]);
                    ingredient.Unit = split[1];
                    ingredient.Name = string.Join(" ", split.Skip(2))?.Trim();
                }else{
                    ingredient.Amount = ParseAmount(split[0]);
                    ingredient.Name = string.Join(" ", split.Skip(1))?.Trim();
                }
            }else{
                ingredient.Name = input;
            }

            return ingredient;
        }
    }
}