
namespace advent_of_code_2022.day2
{
	public class RockPaperScisorsStrategyPointsCalculator
	{
		public int CalculateBasedOnSecondColumnBeingMyPlay() {

			var lines = File.ReadAllLines("./input.txt");

			var points = 0;

			foreach (var line in lines)
			{
                points += line switch
                {
                    "C X" or "A Y" or "B Z" => 6,
                    "A X" or "B Y" or "C Z" => 3,
                    _ => 0,
                };

				points += 1 + line.Last() - 'X';
            }
			
			return points;
        }


        public int CalculateBasedOnSecondColumnBeingExpectedResult()
        {

            var lines = File.ReadAllLines("./input.txt");

            var points = 0;

            foreach (var line in lines)
            {
                points += line switch
                {
                    "A Z" or "B Z" or "C Z" => 6,
                    "A Y" or "B Y" or "C Y" => 3,
                    _ => 0,
                };

                points += line switch
                {
                    "A X" => 3,
                    "B X" => 1,
                    "C X" => 2,
                    "A Y" => 1,
                    "B Y" => 2,
                    "C Y" => 3,
                    "A Z" => 2,
                    "B Z" => 3,
                    "C Z" => 1,
                    _ => 0,
                };
            }

            return points;
        }
    }
}
