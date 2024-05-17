using System;
using System.Collections.Generic;
using System.Text;

namespace TiltGame
{
    class Movements
    {
        public static Tuple<int, int> target;
        public static bool finalStateReached = false;
        public static int finalState = -1;
        public static char[,] emptyMatrix;
        public struct state
        {
            public List<Tuple<int, int>> indicesOfSliders;
            public char predecessor;
        }
        
        static public Dictionary<int, Tuple<int, char>> Parentof;

        static public int ConvertStateToInt(state theState, int sizeOfBoard)
        {
            int stateInt = 0;
            

            foreach (var slider in theState.indicesOfSliders)
            {
                int index = slider.Item1 * sizeOfBoard + slider.Item2;
                stateInt += index * index + 1039;

                
            }

            return stateInt;
        }

        
        static bool IsStateVisited(state theState, int parent, int sizeOfBoard)
        {
            
            int Hashedstate = ConvertStateToInt(theState, sizeOfBoard);

            
            if (Parentof.ContainsKey(Hashedstate))
            {
                
                return true;
            }
            else
            {
                
                Parentof.Add(Hashedstate, Tuple.Create(parent, theState.predecessor));
                return false;
            }
        }
        public static state moveDown(state currentState, int sizeOfBoard, int numOfSliders)
        {
            int r, c;
            state nextState = new state();
            nextState.indicesOfSliders = new List<Tuple<int, int>>();
            nextState.predecessor = 'd';
            for (int i = 0; i < numOfSliders; i++)
            {
                r = currentState.indicesOfSliders[i].Item1;
                c = currentState.indicesOfSliders[i].Item2;
                emptyMatrix[r, c] = 'o';
            }
            int lastR = sizeOfBoard - 1;
            for (int j = 0; j < sizeOfBoard; j++)
            {
                lastR = sizeOfBoard - 1;
                for (int i = sizeOfBoard - 1; i >= 0; i--)
                {
                    if (emptyMatrix[i, j] == '#')
                    {
                        lastR = i - 1;
                    }
                    else if (emptyMatrix[i, j] == 'o')
                    {
                        emptyMatrix[lastR, j] = 'o';
                        nextState.indicesOfSliders.Add(new Tuple<int, int>(lastR, j));
                        if (lastR != i)
                            emptyMatrix[i, j] = '.';
                        lastR--;
                    }
                }
            }
            //nextState.indicesOfSliders.Sort();
            if (emptyMatrix[target.Item1, target.Item2] == 'o')
            {
                finalStateReached = true;

            }
            if (finalStateReached && finalState == -1)
            {
                finalState = ConvertStateToInt(nextState, sizeOfBoard);

            }
            for (int i = 0; i < numOfSliders; i++)
            {
                r = nextState.indicesOfSliders[i].Item1;
                c = nextState.indicesOfSliders[i].Item2;
                emptyMatrix[r, c] = '.';
            }
            return nextState;
        }
        public static state moveRight(state currentState, int sizeOfBoard, int numOfSliders)
        {
            int r, c;

            state nextState = new state();
            nextState.indicesOfSliders = new List<Tuple<int, int>>();
            nextState.predecessor = 'r';
            int lastc = sizeOfBoard - 1;
            for (int i = 0; i < numOfSliders; i++)
            {
                r = currentState.indicesOfSliders[i].Item1;
                c = currentState.indicesOfSliders[i].Item2;
                emptyMatrix[r, c] = 'o';
            }
            for (int i = 0; i < sizeOfBoard; i++)
            {
                lastc = sizeOfBoard - 1;
                for (int j = sizeOfBoard - 1; j >= 0; j--)
                {
                    if (emptyMatrix[i, j] == '#')
                    {
                        lastc = j - 1;
                    }
                    else if (emptyMatrix[i, j] == 'o')
                    {
                        emptyMatrix[i, lastc] = 'o';
                        nextState.indicesOfSliders.Add(new Tuple<int, int>(i, lastc));
                        if (lastc != j)
                            emptyMatrix[i, j] = '.';
                        lastc--;
                    }
                }
            }
            //nextState.indicesOfSliders.Sort();
            if (emptyMatrix[target.Item1, target.Item2] == 'o')
            {
                finalStateReached = true;

            }
            if (finalStateReached && finalState == -1)
            {
                finalState = ConvertStateToInt(nextState, sizeOfBoard);

            }
            for (int i = 0; i < numOfSliders; i++)
            {
                r = nextState.indicesOfSliders[i].Item1;
                c = nextState.indicesOfSliders[i].Item2;
                emptyMatrix[r, c] = '.';
            }
            return nextState;
        }

        public static state moveUp(state currentState, int sizeOfBoard, int numOfSliders)
        {
            int r, c;
            state nextState = new state();
            nextState.indicesOfSliders = new List<Tuple<int, int>>();
            nextState.predecessor = 'u';
            for (int i = 0; i < numOfSliders; i++)
            {
                r = currentState.indicesOfSliders[i].Item1;
                c = currentState.indicesOfSliders[i].Item2;
                emptyMatrix[r, c] = 'o';
            }
            int lastr = 0;
            for (int j = 0; j < sizeOfBoard; j++)
            {
                lastr = 0;
                for (int i = 0; i < sizeOfBoard; i++)
                {
                    if (emptyMatrix[i, j] == '#')
                    {
                        lastr = i + 1;
                    }
                    else if (emptyMatrix[i, j] == 'o')
                    {
                        emptyMatrix[lastr, j] = 'o';
                        nextState.indicesOfSliders.Add(new Tuple<int, int>(lastr, j));
                        if (lastr != i)
                            emptyMatrix[i, j] = '.';
                        lastr++;
                    }

                }
            }
            //nextState.indicesOfSliders.Sort();
            if (emptyMatrix[target.Item1, target.Item2] == 'o')
            {
                finalStateReached = true;

            }
            if (finalStateReached && finalState == -1)
            {
                finalState = ConvertStateToInt(nextState, sizeOfBoard);

            }
            for (int i = 0; i < numOfSliders; i++)
            {
                r = nextState.indicesOfSliders[i].Item1;
                c = nextState.indicesOfSliders[i].Item2;
                emptyMatrix[r, c] = '.';
            }
            return nextState;
        }
        public static state moveLeft(state currentState, int sizeOfBoard, int numOfSliders)
        {
            int r, c;
            state nextState = new state();
            nextState.indicesOfSliders = new List<Tuple<int, int>>();
            nextState.predecessor = 'l';
            for (int i = 0; i < numOfSliders; i++)
            {
                r = currentState.indicesOfSliders[i].Item1;
                c = currentState.indicesOfSliders[i].Item2;
                emptyMatrix[r, c] = 'o';
            }
            int lastc = 0;
            for (int i = 0; i < sizeOfBoard; i++)
            {
                lastc = 0;
                for (int j = 0; j < sizeOfBoard; j++)
                {
                    if (emptyMatrix[i, j] == '#')
                    {
                        lastc = j + 1;
                    }
                    else if (emptyMatrix[i, j] == 'o')
                    {
                        emptyMatrix[i, lastc] = 'o';
                        nextState.indicesOfSliders.Add(new Tuple<int, int>(i, lastc));
                        if (lastc != j)
                            emptyMatrix[i, j] = '.';
                        lastc++;
                    }
                }
            }
            //nextState.indicesOfSliders.Sort();
            if (emptyMatrix[target.Item1, target.Item2] == 'o')
            {
                finalStateReached = true;

            }
            for (int i = 0; i < numOfSliders; i++)
            {
                r = nextState.indicesOfSliders[i].Item1;
                c = nextState.indicesOfSliders[i].Item2;
                emptyMatrix[r, c] = '.';
            }
            if (finalStateReached && finalState == -1)
            {
                finalState = ConvertStateToInt(nextState, sizeOfBoard);

            }
            return nextState;
        }
        static void move(state currentState, int sizeOfBoard, int numOfSliders, ref Queue<state> queueOfStates)
        {


            state returnedState;
            int parent = ConvertStateToInt(currentState, sizeOfBoard);
            if (currentState.predecessor == 'i')
            {

                Parentof.Add(parent, Tuple.Create(-1, 'i'));
            }
            if (currentState.predecessor == 'l' || currentState.predecessor == 'r' || currentState.predecessor == 'i')
            {

                returnedState = moveUp(currentState, sizeOfBoard, numOfSliders);

                ///check here
                if (!IsStateVisited(returnedState, parent, sizeOfBoard))
                    queueOfStates.Enqueue(returnedState);

                returnedState = moveDown(currentState, sizeOfBoard, numOfSliders);
                ///check here
                if (!IsStateVisited(returnedState, parent, sizeOfBoard))
                    queueOfStates.Enqueue(returnedState);
            }
            if (currentState.predecessor == 'u' || currentState.predecessor == 'd' || currentState.predecessor == 'i')
            {
                returnedState = moveLeft(currentState, sizeOfBoard, numOfSliders);
                ///check here
                if (!IsStateVisited(returnedState, parent, sizeOfBoard))
                    queueOfStates.Enqueue(returnedState);

                returnedState = moveRight(currentState, sizeOfBoard, numOfSliders);
                ///check here
                if (!IsStateVisited(returnedState, parent, sizeOfBoard))
                    queueOfStates.Enqueue(returnedState);
            }


        }
        public static Stack<char> sequence;
        public static int minNumOfMoves;
        public static void BFS(state currentState, int sizeOfBoard, int numOfSliders)
        {

         
            minNumOfMoves = 0;
            Queue<state> queueOfStates = new Queue<state>();
            finalStateReached = false;
            finalState = -1;
            Parentof = new Dictionary<int, Tuple<int, char>>();
            currentState.predecessor = 'i';
            move(currentState, sizeOfBoard, numOfSliders, ref queueOfStates);
            while (queueOfStates.Count > 0)
            {
                if (finalStateReached)
                {
                    Console.WriteLine("Solvable");
                    sequence = new Stack<char>();

                    Tuple<int, char> t = new Tuple<int, char>(-1, 'i');

                    for (Tuple<int, char> i = Parentof[finalState]; !i.Equals(t); i = Parentof[i.Item1])
                    {
                        minNumOfMoves++;
                        sequence.Push(i.Item2);
                    }
                    Console.WriteLine("Min number of moves: " + minNumOfMoves);
                    Console.Write("Sequence of moves: ");
                    return;
                }
                currentState = queueOfStates.Dequeue();
                
                move(currentState, sizeOfBoard, numOfSliders, ref queueOfStates);

            }
            Console.WriteLine("unsolvable");
        }

    }
}
