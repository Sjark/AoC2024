﻿using AoC2024.Solutions;
using BenchmarkDotNet.Attributes;

namespace AoC2024
{
    public class Benchmarks
    {
        private readonly Day1 _day1;
        private readonly Day2 _day2;
        private readonly Day3 _day3;
        private readonly Day4 _day4;
        private readonly Day5 _day5;
        private readonly Day6 _day6;
        private readonly Day7 _day7;
        private readonly Day8 _day8;
        private readonly Day9 _day9;
        private readonly Day10 _day10;
        private readonly Day11 _day11;

        public Benchmarks()
        {
            _day1 = new Day1();
            _day2 = new Day2();
            _day3 = new Day3();
            _day4 = new Day4();
            _day5 = new Day5();
            _day6 = new Day6();
            _day7 = new Day7();
            _day8 = new Day8();
            _day9 = new Day9();
            _day10 = new Day10();
            _day11 = new Day11();
        }

        //[Benchmark]
        //public string Day1PartOne() => _day1.PartOne();

        //[Benchmark]
        //public string Day1PartTwo() => _day1.PartTwo();

        //[Benchmark]
        //public string Day2PartOne() => _day2.PartOne();

        //[Benchmark]
        //public string Day2PartTwo() => _day2.PartTwo();

        //[Benchmark]
        //public string Day3PartOne() => _day3.PartOne();

        //[Benchmark]
        //public string Day3PartTwo() => _day3.PartTwo();

        //[Benchmark]
        //public string Day4PartOne() => _day4.PartOne();

        //[Benchmark]
        //public string Day4PartTwo() => _day4.PartTwo();

        //[Benchmark]
        //public string Day5PartOne() => _day5.PartOne();

        //[Benchmark]
        //public string Day5PartTwo() => _day5.PartTwo();

        //[Benchmark]
        //public string Day6PartOne() => _day6.PartOne();

        //[Benchmark]
        //public string Day6PartTwo() => _day6.PartTwo();

        //[Benchmark]
        //public string Day7PartOne() => _day7.PartOne();

        //[Benchmark]
        //public string Day7PartTwo() => _day7.PartTwo();

        //[Benchmark]
        //public string Day8PartOne() => _day8.PartOne();

        //[Benchmark]
        //public string Day8PartTwo() => _day8.PartTwo();

        //[Benchmark]
        //public string Day9PartOne() => _day9.PartOne();

        //[Benchmark]
        //public string Day9PartTwo() => _day9.PartTwo();

        //[Benchmark]
        //public string Day10PartOne() => _day10.PartOne();

        //[Benchmark]
        //public string Day10PartTwo() => _day10.PartTwo();

        [Benchmark]
        public string Day11PartOne() => _day11.PartOne();

        [Benchmark]
        public string Day11PartTwo() => _day11.PartTwo();
    }
}
