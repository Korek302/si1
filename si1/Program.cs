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

        int _factoryNumber;
        int _locNumber;
        int _popSize;

        public Program()
        {
            _factoryNumber = 12;
            _locNumber = 12;
            _popSize = 100;

            _flowTable = new int[12, 12] {
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

            _distTable = new int[12, 12] {
              { 0,  3,  4,  6,  8,  5,  6,  6,  5,  1,  4,  6},
              { 3, 0,  6,  3,  7,  9,  9,  2,  2,  7,  4,  7},
              { 4,  6,  0,  2,  6,  4,  4,  4,  2,  6,  3,  6},
              {6,  3,  2,  0,  5,  5,  3,  3,  9,  4,  3,  6},
              {8,  7,  6,  5,  0,  4,  3,  4,  5,  7,  6,  7},
              {5,  9,  4,  5,  4,  0,  8,  5,  5,  5,  7,  5},
              {6,  9,  4,  3,  3,  8,  0,  6,  8,  4,  6,  7},
              {6,  2,  4,  3,  4,  5,  6,  0,  1,  5,  5,  3},
              {5,  2,  2,  9,  5,  5,  8,  1,  0,  4,  5,  2},
              {1,  7,  6,  4,  7,  5,  4,  5,  4,  0,  7,  7},
              {4,  4,  3,  3,  6,  7,  6,  5,  5,  7,  0,  9},
              {6,  7,  6,  6,  7,  5,  7,  3,  2,  7,  9,  0 },
            };

            //_flowTable = new int[5, 5] {
            //    { 0, 3, 4, 6, 8 },
            //    { 3, 0, 6, 3, 7 },
            //    { 4, 6, 0, 2, 6 },
            //    { 6, 3, 2, 0, 5 },
            //    { 8, 7, 6, 5, 0 }
            //};
        }

        public Program(int popSize)
        {
            _factoryNumber = 12;
            _locNumber = 12;
            _popSize = popSize;

            _flowTable = new int[12, 12] {
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

            _distTable = new int[12, 12] {
              { 0,  3,  4,  6,  8,  5,  6,  6,  5,  1,  4,  6},
              { 3, 0,  6,  3,  7,  9,  9,  2,  2,  7,  4,  7},
              { 4,  6,  0,  2,  6,  4,  4,  4,  2,  6,  3,  6},
              {6,  3,  2,  0,  5,  5,  3,  3,  9,  4,  3,  6},
              {8,  7,  6,  5,  0,  4,  3,  4,  5,  7,  6,  7},
              {5,  9,  4,  5,  4,  0,  8,  5,  5,  5,  7,  5},
              {6,  9,  4,  3,  3,  8,  0,  6,  8,  4,  6,  7},
              {6,  2,  4,  3,  4,  5,  6,  0,  1,  5,  5,  3},
              {5,  2,  2,  9,  5,  5,  8,  1,  0,  4,  5,  2},
              {1,  7,  6,  4,  7,  5,  4,  5,  4,  0,  7,  7},
              {4,  4,  3,  3,  6,  7,  6,  5,  5,  7,  0,  9},
              {6,  7,  6,  6,  7,  5,  7,  3,  2,  7,  9,  0 },
            };

            //_flowTable = new int[5, 5] {
            //    { 0, 3, 4, 6, 8 },
            //    { 3, 0, 6, 3, 7 },
            //    { 4, 6, 0, 2, 6 },
            //    { 6, 3, 2, 0, 5 },
            //    { 8, 7, 6, 5, 0 }
            //};
        }

        public Program(int factoryNumber, int locNumber, int popSize, int[,] distTable, int[,] flowTable)
        {
            _factoryNumber = factoryNumber;
            _locNumber = locNumber;
            _popSize = popSize;

            _distTable = distTable;
            _flowTable = flowTable;
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
            int[,] _out = new int[_popSize, _factoryNumber];
            Random _rnd = new Random();

            for (int i = 0; i < _out.GetLength(0); i++)
            {
                int[] _temp = RandomUniqueNums(_factoryNumber, _rnd);
                for (int j = 0; j < _out.GetLength(1); j++)
                {
                    _out[i, j] = _temp[j];
                }
            }
            return _out;
        }

        public int[] RandomUniqueNums(int howMany, Random rnd)
        {
            int[] _out = Enumerable.Repeat(-1, howMany).ToArray();

            for (int i = 0; i < howMany; i++)
            {
                int _temp = rnd.Next(_locNumber);
                while (_out.Contains(_temp))
                {
                    _temp = rnd.Next(_locNumber);
                }
                _out[i] = _temp;
            }
            return _out;
        }

        public int[] BestSpeciment(int[,] pop)
        {
            int[] _out = new int[pop.GetLength(1)];
            int _bestCost = int.MaxValue;

            for (int i = 0; i < pop.GetLength(0); i++)
            {
                int _currCost = CostFunc(GetRow(pop, i));
                if (_currCost < _bestCost)
                {
                    _out = GetRow(pop, i);
                    _bestCost = _currCost;
                }
            }
            return _out;
        }

        public Tuple<int[,], int[], int[]> Evaluate(int[,] pop, Boolean bestProtection)
        {
            int[] _costs = new int[pop.GetLength(0)];
            int[] _best = new int[pop.GetLength(1)];
            int _bestCost = int.MaxValue;

            for (int i = 0; i < pop.GetLength(0); i++)
            {
                _costs[i] = CostFunc(GetRow(pop, i));
                if (_costs[i] < _bestCost)
                {
                    _best = GetRow(pop, i);
                    _bestCost = _costs[i];
                }
            }
            if (bestProtection)
            {
                return new Tuple<int[,], int[], int[]>(pop, _costs, _best);
            }
            else
            {
                return new Tuple<int[,], int[], int[]>(pop, _costs, Enumerable.Repeat(-1, pop.GetLength(1)).ToArray());
            }

        }

        public Tuple<int[,], int[]> TournamentSelection(Tuple<int[,], int[], int[]> pop, int tournamentSize, Random rnd)
        {
            int _popSizeTemp = pop.Item1.GetLength(0);

            Tuple<int[,], int[]> _out = new Tuple<int[,], int[]>
                (new int[_popSizeTemp, pop.Item1.GetLength(1)], new int[_popSizeTemp]);

            for (int i = 0; i < _popSizeTemp; i++)
            {
                Tuple<int[], int> _winner = new Tuple<int[], int>(new int[pop.Item1.GetLength(1)], int.MaxValue);
                for (int j = 0; j < tournamentSize; j++)
                {
                    int _playerIndex = rnd.Next(_popSizeTemp);
                    if (pop.Item2[_playerIndex] < _winner.Item2)
                    {
                        _winner = new Tuple<int[], int>(GetRow(pop.Item1, _playerIndex), pop.Item2[_playerIndex]);
                    }
                }
                for (int j = 0; j < pop.Item1.GetLength(1); j++)
                {
                    _out.Item1[i, j] = _winner.Item1[j];
                }
                _out.Item2[i] = _winner.Item2;
            }

            if (pop.Item3[0] != pop.Item3[1])
            {
                for (int j = 0; j < pop.Item1.GetLength(1); j++)
                {
                    _out.Item1[_popSizeTemp - 1, j] = pop.Item3[j];
                }
                _out.Item2[_popSizeTemp - 1] = CostFunc(pop.Item3);
            }
            return _out;
        }

        public Tuple<int[,], int[]> RouletteSelection(Tuple<int[,], int[], int[]> pop, Random rnd)
        {
            int _popSizeTemp = pop.Item1.GetLength(0);

            Tuple<int[,], int[]> _out = new Tuple<int[,], int[]>
                (new int[_popSizeTemp, pop.Item1.GetLength(1)], new int[_popSizeTemp]);

            int _totalSumOfCosts = pop.Item2.Sum();

            for (int i = 0; i < _popSizeTemp; i++)
            {
                int _winnerNumber = rnd.Next(_totalSumOfCosts);
                int _sumOfCosts = 0;
                int j = 0;

                while (_sumOfCosts <= _winnerNumber)
                {
                    _sumOfCosts += pop.Item2[j];
                    j++;
                }
                Tuple<int[], int> _winner = new Tuple<int[], int>(GetRow(pop.Item1, j - 1), pop.Item2[j - 1]);

                for (int k = 0; k < pop.Item1.GetLength(1); k++)
                {
                    _out.Item1[i, k] = _winner.Item1[k];
                }
                _out.Item2[i] = _winner.Item2;
            }

            if (pop.Item3[0] != pop.Item3[1])
            {
                for (int j = 0; j < pop.Item1.GetLength(1); j++)
                {
                    _out.Item1[_popSizeTemp - 1, j] = pop.Item3[j];
                }
                _out.Item2[_popSizeTemp - 1] = CostFunc(pop.Item3);
            }
            return _out;
        }

        public int[] SingleCrossover(int[] parent1, int[] parent2, int breakpoint, Random rnd)
        {
            int[] _out = new int[parent1.Length];
            if (breakpoint < parent1.Length && parent1.Length == parent2.Length && breakpoint > 0)
            {
                int _which = rnd.Next(2);
                if (_which == 0)
                {
                    Array.Copy(parent1, 0, _out, 0, breakpoint);
                    Array.Copy(parent2, breakpoint, _out, breakpoint, _out.Length - breakpoint);
                }
                else
                {
                    Array.Copy(parent2, 0, _out, 0, breakpoint);
                    Array.Copy(parent1, breakpoint, _out, breakpoint, _out.Length - breakpoint);
                }
            }
            else
            {
                Console.WriteLine("Error: SingleCrossover");
            }

            return _out;
        }

        public int[,] Crossover(int[,] pop, float px, Random rnd)
        {
            int _popSizeTemp = pop.GetLength(0);
            int[] _best = GetRow(pop, _popSizeTemp - 1);
            int _threshold = (int)(100 * px);

            int[,] _out = new int[_popSizeTemp, pop.GetLength(1)];

            for (int i = 0; i < _popSizeTemp; i++)
            {
                int[] _chosen = GetRow(pop, i);

                int _roll = rnd.Next(100) + 1;
                if (_roll > _threshold)
                {
                    //nie ma krzyz
                    for (int k = 0; k < pop.GetLength(1); k++)
                    {
                        _out[i, k] = _chosen[k];
                    }
                }
                else
                {
                    //jest krzyz
                    int _chosenIndex2 = rnd.Next(_popSizeTemp);
                    int[] _chosen2 = GetRow(pop, _chosenIndex2);
                    int _breakpoint = rnd.Next(1, (_chosen.Length / 2)); //DUNNNNNNNNNNNNNNNNNNOOOOOOOOOOOO
                    int[] _child = SingleCrossover(_chosen, _chosen2, _breakpoint, rnd);
                    Repair(_child);

                    for (int j = 0; j < pop.GetLength(1); j++)
                    {
                        _out[i, j] = _child[j];
                    }
                }
            }

            if (_best[0] != _best[1])
            {
                for (int j = 0; j < pop.GetLength(1); j++)
                {
                    _out[_popSizeTemp - 1, j] = _best[j];
                }
            }
            return _out;
        }

        public int[] SingleMutation(int[] spec, Random rnd)
        {
            int[] _out = spec;
            int _genToSwap = rnd.Next(_out.Length / 2);
            if (_genToSwap < _out.Length / 2)
            {
                int _temp = _out[_genToSwap];
                _out[_genToSwap] = _out[_out.Length - _genToSwap - 1];
                _out[_out.Length - _genToSwap - 1] = _temp;
            }
            else
            {
                Console.WriteLine("Error: SingleMutation");
            }

            return _out;
        }

        public int[,] Mutation(int[,] pop, float pm, Random rnd)
        {
            int _popSizeTemp = pop.GetLength(0);
            int[] _best = GetRow(pop, _popSizeTemp - 1);
            int _threshold = (int)(100 * pm);

            int[,] _out = new int[_popSizeTemp, pop.GetLength(1)];

            for (int i = 0; i < _popSizeTemp; i++)
            {
                int[] _chosen = GetRow(pop, i);

                int _roll = rnd.Next(100) + 1;
                if (_roll > _threshold)
                {
                    //nie ma mut
                    for (int k = 0; k < pop.GetLength(1); k++)
                    {
                        _out[i, k] = _chosen[k];
                    }
                }
                else
                {
                    //jest mut
                    int[] _mutant = SingleMutation(_chosen, rnd);

                    for (int j = 0; j < pop.GetLength(1); j++)
                    {
                        _out[i, j] = _mutant[j];
                    }
                }
            }

            if (_best[0] != _best[1])
            {
                for (int j = 0; j < pop.GetLength(1); j++)
                {
                    _out[_popSizeTemp - 1, j] = _best[j];
                }
            }
            return _out;
        }

        public void Repair(int[] spec)
        {
            for (int i = 0; i < spec.Length; i++)
            {
                for (int j = i + 1; j < spec.Length; j++)
                {
                    if (spec[i] == spec[j])
                    {
                        if (spec[j] + 1 < _locNumber)
                        {
                            spec[j]++;
                        }
                        else
                        {
                            spec[j] = 0;
                        }
                        i = 0;
                        j = 0;
                    }
                }
            }
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

        public void Show(int[,] arr)
        {
            for(int i = 0; i < arr.GetLength(0); i++)
            {
                for(int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i,j] + ", ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("#######");
        }

        static void Main(string[] args)
        {
            int _genNumber = 100;
            int _popSize = 100;
            int _tournamentSize = 3;
            float px = 0.7f;
            float pm = 0.01f;
            Boolean _bestProtection = true;

            Program p = new Program(_popSize);
            Random _rnd = new Random();

            int[] _best = new int[p._factoryNumber];
            int[,] _currPop = p.InitialPop();
            Tuple<int[,], int[], int[]> _currPopWithCost = p.Evaluate(_currPop, _bestProtection);

            for (int i = 0; i < _genNumber; i++)
            {
                _currPop = p.RouletteSelection(_currPopWithCost, _rnd).Item1;
                //_currPop = p.TournamentSelection(_currPopWithCost, _tournamentSize, _rnd).Item1;
                //p.Show(_currPop);
                _currPop = p.Crossover(_currPop, px, _rnd);
                //p.Show(_currPop);
                _currPop = p.Mutation(_currPop, pm, _rnd);
                //p.Show(_currPop);
                _currPopWithCost = p.Evaluate(_currPop, _bestProtection);

                Console.WriteLine(_currPopWithCost.Item2[99]);

                if (_bestProtection)
                {
                    _best = _currPopWithCost.Item3;
                }
                else
                {
                    _best = p.BestSpeciment(_currPopWithCost.Item1);
                }
            }

            for (int i = 0; i < _best.Length; i++)
            {
                if (i == 0)
                {
                    Console.Write("[");
                }
                if (i != _best.Length - 1)
                {
                    Console.Write(_best[i] + ", ");
                }
                if (i == _best.Length - 1)
                {
                    Console.WriteLine(_best[i] + "]");
                }
            }

            Console.WriteLine(p.CostFunc(new int[] { 2, 9, 10, 1, 11, 4, 5, 6, 7, 0, 3, 8 }));
        }
    }
}
