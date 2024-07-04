using ConsoleApp1;

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
    Console.ForegroundColor = IsLevelPassed(stacks, stackHeight, colorCount) ? ConsoleColor.Green : ConsoleColor.Red;
    Console.WriteLine("----------------------------------------");
    for (int i = 0; i < stacks.Count; i++)
    {
        var stackInx = i + 1;
        var stackContent = string.Join(" - ", stacks[i].Reverse());
        Console.WriteLine($"Stack {stackInx} : {stackContent}");
    }
    Console.WriteLine("----------------------------------------");
    Console.WriteLine();
}

bool SolvePuzzle(List<Stack<BallColor>> stacks, int stackHeight, int colorCount)
{
    for (int i = 0; i < stacks.Count; i++)
    {
        var sourceStack = stacks[i];
        if (IsStackSolved(sourceStack, stackHeight))
        {
            continue;
        }
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
                else
                {
                    SolvePuzzle(stacks, stackHeight, colorCount);
                }
            }
        }
    }

    return false;
}

int stackHeight = 4;
int colorCount = 1;

List<Stack<BallColor>> ballStacks = new()
{
        new Stack<BallColor>( new[]{BallColor.Blue, BallColor.Blue} ),
        new Stack<BallColor>( new[]{BallColor.Blue, BallColor.Blue } ),
};

PrintStacks(ballStacks, stackHeight, colorCount);
SolvePuzzle(ballStacks, stackHeight, colorCount);