namespace DataGenerator
{
    public static class RecipesGenerator
    {
        public static List<RecipeVM> GenerateRecipes(int n, int visitCount)
        {
            Random random = new();
            List<RecipeVM> recipes = new();

            for (int i = 0; i < n; i++)
            {
                RecipeVM recipe = new()
                {
                    IssueDate = GeneratorHelper.GenerateVisitDate(),
                    VisitId = random.Next(1, visitCount),
                };

                recipes.Add(recipe);
            }

            return recipes;
        }

    }
}
