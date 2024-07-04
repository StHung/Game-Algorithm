using ConsoleApp1;
using System.Text.Json;

string ConvertToStringJson(List<Stack<BallColor>> stacks)
{
    return JsonSerializer.Serialize(stacks);
}

bool IsStackSolved(Stack<BallColor> stack, int stackHeight) => stack.Count == stackHeight && stack.Distinct<BallColor>().Count() == 1;

bool IsLevelPassed(List<Stack<BallColor>> stacks, int stackHeight, int colorCount) => stacks.Count(stack => IsStackSolved(stack, stackHeight)) == colorCount;

bool IsValidMove(Stack<BallColor> source, Stack<BallColor> destiation, int stackHeight)
{
    if (source.Count == 0 || destiation.Count == stackHeight)
    {
        return false;
    }

    if( destiation.Count == 0)
    {
        return true;
    }

    return source.Peek() == destiation.Peek();
}

void MoveBall(Stack<BallColor> source, Stack<BallColor> destiation )
{
    destiation.Push(source.Pop());
}

void PrintStacks(List<Stack<BallColor>> stacks, int stackHeight, int colorCount)
{
    Console.WriteLine("----------------------------------------");
    for (int i = 0; i < stacks.Count; i++)
    {
        var stackInx = i + 1;
        var stackContent = string.Join(" - ", stacks[i].Reverse());
        Console.ForegroundColor = IsStackSolved(stacks[i], stackHeight) || stacks[i].Count == 0 ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine($"Stack {stackInx} : {stackContent}");
    }
    Console.WriteLine("----------------------------------------");
    Console.WriteLine();
}

bool SolvePuzzle(List<Stack<BallColor>> stacks, int stackHeight, int colorCount, HashSet<string> visited)
{
    visited.Add(ConvertToStringJson(stacks));

    for (int i = 0; i < stacks.Count; i++)
    {
        var sourceStack = stacks[i];
        for (int j = 0; j < stacks.Count; j++)
        {
            if (i == j)
            {
                continue;
            }
            var destinationStack = stacks[j];
            if (IsValidMove(sourceStack, destinationStack, stackHeight))
            {
                MoveBall(sourceStack, destinationStack);
                PrintStacks(stacks, stackHeight, colorCount);

                if (IsLevelPassed(stacks, stackHeight, colorCount))
                {
                    return true;
                }

                if( !visited.Contains(ConvertToStringJson(stacks)))
                {
                    if (SolvePuzzle(stacks, stackHeight, colorCount, visited))
                    {
                        return true;
                    }
                }
            }
        }
    }

    return false;
}

int stackHeight = 4;
int colorCount = 3;

List<Stack<BallColor>> ballStacks = new()
{
        new( new[]{BallColor.Yellow, BallColor.Blue, BallColor.Red, BallColor.Blue } ),
        new( new[]{BallColor.Blue, BallColor.Yellow, BallColor.Red, BallColor.Red } ),
        new( new[]{BallColor.Red, BallColor.Blue, BallColor.Yellow, BallColor.Yellow } ),
        new(),
        new(),
};

PrintStacks(ballStacks, stackHeight, colorCount);
SolvePuzzle(ballStacks, stackHeight, colorCount, new());