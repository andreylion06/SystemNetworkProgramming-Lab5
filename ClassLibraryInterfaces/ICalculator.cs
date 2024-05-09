namespace ClassLibraryInterfaces
{
    public interface ICalculator
    {
        double Calculate(double x, double y);
        int Factorial(int n);
        int Fibonacci(int n);
        void QuickSort(int[] array, int left, int right);
    }
}

// Кінець файлу