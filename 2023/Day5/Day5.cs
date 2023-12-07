using System.Text;

namespace AOC.Y2023;

class Day5(string inputName = "input.txt") : BaseDay(2023, 5, inputName)
{
    public override double Solve1()
    {
        double[] seeds = [];
        bool[] isMapped = [];
        bool mapping = false;

        foreach (var input in inputs)
        {
            if (input == "")
            {
                mapping = false;
            }
            else if (mapping)
            {
                var information = input.Split();
                double destinationStart = double.Parse(information[0]);
                double sourceStart = double.Parse(information[1]);
                double rangeLength = double.Parse(information[2]);

                for (int i = 0; i < seeds.Length; i++)
                {
                    if (seeds[i] >= sourceStart && seeds[i] < sourceStart + rangeLength && !isMapped[i])
                    {
                        // map it!!
                        seeds[i] = destinationStart + (seeds[i] - sourceStart);
                        isMapped[i] = true;
                    }
                }
            }
            else if (input.StartsWith("seeds: "))
            {
                StringBuilder sb = new();
                List<double> _seeds = [];
                for (int i = input.IndexOf(':') + 2; i < input.Length; i++)
                {
                    sb.Append(input[i]);

                    if (input[i] == ' ' || i == input.Length - 1)
                    {
                        _seeds.Add(double.Parse(sb.ToString()));
                        sb.Clear();
                    }
                }

                seeds = [.. _seeds];
                isMapped = new bool[seeds.Length];
            }
            else if (input.EndsWith("map:"))
            {
                mapping = true;
                Array.Fill(isMapped, false);
            }
        }

        double min = seeds[0];
        foreach (double seed in seeds)
        {
            if (min > seed)
            {
                min = seed;
            }
        }

        return min;
    }

    public override double Solve2()
    {
        List<double[]> seeds = [];
        List<double[]> mappingRange = [];
        bool mapping = false;


        foreach (var input in inputs)
        {
            if (input == "")
            {
                if (mapping)
                {
                    List<double[]> mappedSeed = [];

                    seeds.Sort((double[] x, double[] y) => x[0] - y[0] > 0 ? 1 : -1);
                    mappingRange.Sort((double[] x, double[] y) => x[1] - y[1] > 0 ? 1 : -1);

                    var seedsStack = new Stack<double[]>(seeds);
                    var mappingRangeStack = new Stack<double[]>(mappingRange);
                    double[] currentMappingRange = mappingRangeStack.Pop();
                    double destinationStart = currentMappingRange[0];
                    double sourceStart = currentMappingRange[1];
                    double rangeLength = currentMappingRange[2];
                    double sourceEdge = sourceStart + rangeLength;


                    while (seedsStack.Count > 0)
                    {
                        var currentSeed = seedsStack.Pop();
                        var seedStart = currentSeed[0];
                        var seedRange = currentSeed[1];
                        var seedEdge = seedStart + seedRange;

                        if (seedStart < sourceStart)
                        {
                            if (seedEdge < sourceStart)
                            {
                                mappedSeed.Add([seedStart, seedRange]);
                            }
                            else
                            {
                                mappedSeed.Add([seedStart, sourceStart - seedStart]);
                                seedsStack.Push([sourceStart, seedEdge - sourceStart]);
                            }
                        }
                        else if (seedStart < sourceEdge)
                        {
                            var newDestinationStart = seedStart - sourceStart + destinationStart;
                            if (seedEdge < sourceEdge)
                            {
                                mappedSeed.Add([newDestinationStart, seedRange]);
                            }
                            else
                            {
                                mappedSeed.Add([newDestinationStart, sourceEdge - seedStart]);
                                seedsStack.Push([sourceEdge, seedEdge - sourceEdge]);

                            }
                        }
                        else
                        {
                            if (mappingRangeStack.Count > 0)
                            {
                                currentMappingRange = mappingRangeStack.Pop();
                                destinationStart = currentMappingRange[0];
                                sourceStart = currentMappingRange[1];
                                rangeLength = currentMappingRange[2];
                                sourceEdge = sourceStart + rangeLength;
                                seedsStack.Push([seedStart, seedRange]);
                            }
                            else
                            {
                                mappedSeed.Add([seedStart, seedRange]);
                            }
                        }
                    }

                    seeds = mappedSeed;
                    mappingRange = [];
                }

                mapping = false;
            }
            else if (mapping)
            {
                var information = input.Split();
                double destinationStart = double.Parse(information[0]);
                double sourceStart = double.Parse(information[1]);
                double rangeLength = double.Parse(information[2]);
                mappingRange.Add([destinationStart, sourceStart, rangeLength]);
            }
            else if (input.StartsWith("seeds:"))
            {
                var nums = input[(input.IndexOf(":") + 2)..].Split(" ");
                for (int i = 0; i < nums.Length; i += 2)
                {
                    seeds.Add([double.Parse(nums[i]), double.Parse(nums[i + 1])]);
                }
            }
            else if (input.EndsWith("map:"))
            {
                mapping = true;
            }
        }

        double min = seeds[0][0];
        foreach (double[] seed in seeds)
        {
            if (min > seed[0])
            {
                min = seed[0];
            }
        }

        return min;
    }

}
