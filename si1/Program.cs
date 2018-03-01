using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace si1
{
    class Program
    {
        int[,] _distTable;
        int[,] _flowTable;

        int _tourSize = 5;
        int _popSize = 100;
        int _genNumber = 100;

        public Program()
        {
            _distTable = new int[12, 12] {
                { 0, 1, 2, 2, 3, 4, 4, 5, 3, 5, 6, 7 },
                { 1, 0, 1, 1, 2, 3, 3, 4, 2, 4, 5, 6 },
                { 2, 1, 0, 2, 1, 2, 2, 3, 1, 3, 4, 5 },
                { 2, 1, 2, 0, 1, 2, 2, 3, 3, 3, 4, 5 },
                { 3, 2, 1, 1, 0, 1, 1, 2, 2, 2, 3, 4 },
                { 4, 3, 2, 2, 1, 0, 2, 3, 3, 1, 2, 3 },
                { 4, 3, 2, 2, 1, 2, 0, 1, 3, 1, 2, 3 },
                { 5, 4, 3, 3, 2, 3, 1, 0, 4, 2, 1, 2 },
                { 3, 2, 1, 3, 2, 3, 3, 4, 0, 4, 5, 6 },
                { 5, 4, 3, 3, 2, 1, 1, 2, 4, 0, 1, 2 },
                { 6, 5, 4, 4, 3, 2, 2, 1, 5, 1, 0, 1 },
                { 7, 6, 5, 5, 4, 3, 3, 2, 6, 2, 1, 0 }
            };

            _flowTable = new int[5, 5] {
                { 0, 3, 4, 6, 8 },
                { 3, 0, 6, 3, 7 },
                { 4, 6, 0, 2, 6 },
                { 6, 3, 2, 0, 5 },
                { 8, 7, 6, 5, 0 }
            };
        }

        public int CostFunc(int[] specimen)
        {
            int _out = 0;
            for (int i = 0; i < specimen.Length - 1; i++)
            {
                for (int j = i + 1; j < specimen.Length; j++)
                {
                    _out += Flow(i, j) * Dist(specimen[i], specimen[j]);
                }
            }
            return _out;
        }

        public int Dist(int loc1, int loc2)
        {
            return _distTable[loc1, loc2];
        }

        public int Flow(int fac1, int fac2)
        {
            return _flowTable[fac1, fac2];
        }

        public int[,] InitialPop()
        {
            int[,] _out = new int[_popSize, _tourSize];
            Random _rnd = new Random();

            for (int i = 0; i < _out.GetLength(0); i++)
            {
                for (int j = 0; j < _out.GetLength(1); j++)
                {
                    _out[i, j] = _rnd.Next(12);
                }
            }
            return _out;
        }

        public int[] BestSpeciment(int[,] pop)
        {
            int[] _out = new int[pop.GetLength(1)];
            int _bestCost = int.MaxValue;

            for (int i = 0; i < pop.GetLength(0); i++)
            {
                if (CostFunc(GetRow(pop, i)) < CostFunc(_out))
                {
                    _out = GetRow(pop, i);
                    _bestCost = CostFunc(_out);
                }
            }
            return _out;
        }

        public Tuple<int[,], int[]> Evaluate(int[,] pop)
        {
            int[] _costs = new int[pop.GetLength(0)];

            for (int i = 0; i < pop.GetLength(0); i++)
            {
                _costs[i] = CostFunc(GetRow(pop, i));
            }
            return new Tuple<int[,], int[]>(pop, _costs);
        }

        public Tuple<int[,], int[]> TournamentSelection(Tuple<int[,], int[]> pop, int tournamentSize)
        {
            Random _rnd = new Random();
            int _popSizeTemp = pop.Item1.GetLength(0);

            Tuple<int[,], int[]> _out = new Tuple<int[,], int[]>
                (new int[_popSizeTemp, pop.Item1.GetLength(1)], new int[_popSizeTemp]);

            Tuple<int[], int> _winner = new Tuple<int[], int>(new int[pop.Item1.GetLength(1)], int.MaxValue);

            for (int i = 0; i < _popSizeTemp; i++)
            {
                for(int j = 0; j < tournamentSize; j++)
                {
                    int _playerIndex = _rnd.Next(_popSizeTemp);
                    if(pop.Item2[_playerIndex] < _winner.Item2)
                    {
                        _winner = new Tuple<int[], int>(GetRow(pop.Item1, _playerIndex), pop.Item2[_playerIndex]);
                    }
                }
                for(int j = 0; j < pop.Item1.GetLength(1); j++)
                {
                    _out.Item1[i, j] = _winner.Item1[j];
                }
                _out.Item2[i] = _winner.Item2;
            }
            return _out;
        }

        public Tuple<int[,], int[]> RouletteSelection(Tuple<int[,], int[]> pop)
        {
            Random _rnd = new Random();
            int _popSizeTemp = pop.Item1.GetLength(0);

            Tuple<int[,], int[]> _out = new Tuple<int[,], int[]>
                (new int[_popSizeTemp, pop.Item1.GetLength(1)], new int[_popSizeTemp]);

            Tuple<int[], int> _winner = new Tuple<int[], int>(new int[pop.Item1.GetLength(1)], int.MaxValue);

            int _totalSumOfCosts = pop.Item2.Sum();

            for(int i = 0; i < _popSizeTemp; i++)
            {
                int _winnerNumber = _rnd.Next(_totalSumOfCosts);
                int _sumOfCosts = 0;
                int j = 0;

                while(_sumOfCosts < _winnerNumber)
                {
                    _sumOfCosts += pop.Item2[j];
                    j++;
                }
                _winner = new Tuple<int[], int>(GetRow(pop.Item1, j - 1), pop.Item2[j-1]);

                for (int k = 0; k < pop.Item1.GetLength(1); k++)
                {
                    _out.Item1[i, k] = _winner.Item1[k];
                }
                _out.Item2[i] = _winner.Item2;
            }
            return _out;
        }

        public Tuple<int[,], int[]> Crossover(Tuple<int[,], int[]> pop, float px)
        {

        }

        public Tuple<int[,], int[]> Mutation(Tuple<int[,], int[]> pop, float pm)
        {

        }

        public void Repair(int[] speciment)
        {

        }

        public int[] GetRow(int[,] array, int index)
        {
            int[] _out = new int[array.GetLength(1)];

            for (int i = 0; i < array.GetLength(1); i++)
            {
                _out[i] = array[index, i];
            }
            return _out;
        }

        public int[] GetColumn(int[,] array, int index)
        {
            int[] _out = new int[array.GetLength(0)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                _out[i] = array[i, index];
            }
            return _out;
        }

        static void Main(string[] args)
        {
            Program p = new Program();

            Console.WriteLine(p.CostFunc(new int[]{ 3, 5, 11, 4, 8 }));

            int[,] list = p.InitialPop();

            for(int i = 0; i < list.GetLength(0); i++)
            {
                for(int j = 0; j < list.GetLength(1); j ++)
                {
                    Console.Write(list[i,j] + ", ");
                }
                Console.WriteLine();
            }

            int[,] _pop = p.InitialPop();
        }
    }
}
