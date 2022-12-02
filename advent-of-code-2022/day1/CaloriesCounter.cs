namespace advent_of_code_2022.day1
{
	public class CaloriesCounter
	{

		public int FindElfWithGreatestCalloriesReserve()
		{
			int accumulator = 0, result = 0;

            foreach (var line in File.ReadAllLines("./input.txt"))
			{
				if (line.Length == 0)
				{
					result = result < accumulator ? accumulator : result;
					accumulator = 0;
					continue;
				}

				accumulator += int.Parse(line);
			}

			return result;
		}


        public int FindTopThreeElvesByCalloriesReserve()
        {
            var accumulator = 0;
            var caloriesPerElf = new List<int>();

            foreach (var line in File.ReadAllLines("./input.txt"))
            {
                if (line.Length == 0)
                {
                    caloriesPerElf.Add(accumulator);
                    accumulator = 0;
                    continue;
                }

                accumulator += int.Parse(line);
            }

            var top3 = caloriesPerElf.OrderByDescending(c => c).ToArray();

            return top3.Take(3).Sum();
        }

    }
}

