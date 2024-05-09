using ClassLibraryInterfaces;
using System.Text.Json;

namespace ClassLibraryClasses
{
    // Ім'я файлу: MyCalculator.cs
    // Ремарка: MyCalculator - включає в себе деякі математичні операції
    // Автор: Андрій Сахно

    public class MyCalculator : ICalculator
    {
        public double Calculate(double x, double y)
        {
            return x + y;
        }

        public int Factorial(int n)
        {
            if (n == 0)
                return 1;
            else
                return n * Factorial(n - 1);
        }

        public int Fibonacci(int n)
        {
            if (n <= 1)
                return n;
            else
                return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        public void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right);
                QuickSort(array, left, pivotIndex - 1);
                QuickSort(array, pivotIndex + 1, right);
            }
        }

        private int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;
            for (int j = left; j < right; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    Swap(array, i, j);
                }
            }
            Swap(array, i + 1, right);
            return i + 1;
        }

        private void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

    }
}

// Кінець файлу