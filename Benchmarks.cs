﻿using AoC2024.Solutions;
using BenchmarkDotNet.Attributes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC2024
{
    public class Benchmarks
    {
        private readonly Day1 _day1;
        private readonly Day2 _day2;
        private readonly Day3 _day3;
        private readonly Day4 _day4;
        private readonly Day5 _day5;

        public Benchmarks()
        {
            _day1 = new Day1();
            _day2 = new Day2();
            _day3 = new Day3();
            _day4 = new Day4();
            _day5 = new Day5();
        }

        [Benchmark]
        public string Day1PartOne() => _day1.PartOne();

        [Benchmark]
        public string Day1PartTwo() => _day1.PartTwo();

        [Benchmark]
        public string Day2PartOne() => _day2.PartOne();

        [Benchmark]
        public string Day2PartTwo() => _day2.PartTwo();

        [Benchmark]
        public string Day3PartOne() => _day3.PartOne();

        [Benchmark]
        public string Day3PartTwo() => _day3.PartTwo();

        [Benchmark]
        public string Day4PartOne() => _day4.PartOne();

        [Benchmark]
        public string Day4PartTwo() => _day4.PartTwo();

        [Benchmark]
        public string Day5PartOne() => _day5.PartOne();

        [Benchmark]
        public string Day5PartTwo() => _day5.PartTwo();
    }
}