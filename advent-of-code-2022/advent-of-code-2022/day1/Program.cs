﻿global using System;
global using System.Linq;

using advent_of_code_2022.day1;
using advent_of_code_2022.day2;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Advent of Code 2022");

//var result1 =  new CaloriesCounter().FindElfWithGreatestCalloriesReserve();
//Console.WriteLine($"Day 1-1: {result1}");

//var result2 = new CaloriesCounter().FindTopThreeElvesByCalloriesReserve();
//Console.WriteLine($"Day 1-2: {result2}");

var result1 = new PointsPrediction().ResolvePuzzle();
Console.WriteLine($"Day 2-1: {result1}");

var result2 = new PointsPrediction().ResolvePuzzle2();
Console.WriteLine($"Day 2-2: {result2}");

Console.ReadKey();