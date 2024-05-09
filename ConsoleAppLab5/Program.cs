using ClassLibraryInterfaces;
using ConsoleAppLab5.AssemblyLoader;

class Program
{
    static void Main(string[] args)
    {
        //LoadAndTryMyAssembly();
        //LoadAndTryCREncryptor();
        LoadAndTryManyThreads();
    }

    static void LoadAndTryMyAssembly()
    {
        try
        {
            var libraryLoader = new AssemblyLoader();
            libraryLoader.LoadAssemblyFromProjectInSameSolution("ClassLibraryClasses");

            object? calculatorInstance = libraryLoader.GetClassInstanceFromAssembly("MyCalculator");

            if (calculatorInstance != null && calculatorInstance is ICalculator)
            {
                ICalculator calculator = (ICalculator)calculatorInstance;
                var result = calculator.Calculate(1, 2);

                Console.WriteLine(result);
            }

            libraryLoader.UnloadAssembly();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void LoadAndTryCREncryptor()
    {
        try
        {
            var libraryLoader = new AssemblyLoader();

            string solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName!;
            string filePath = Path.Combine(solutionRoot, "CREncryptor.dll");
            libraryLoader.LoadAssemblyByAbsolutePath("CREncryptor", filePath);

            object? encryptorInstance = libraryLoader.GetClassInstanceFromAssembly("CharacterReplacementEncryptor");

            if (encryptorInstance != null)
            {
                ICharacterReplacementEncryptor encryptor = (ICharacterReplacementEncryptor)encryptorInstance;
                int key = 5;
                string encStr = encryptor.Encrypt(key, "Success!");
                string decStr = encryptor.Decrypt(key, encStr);
                Console.WriteLine(decStr);
            }

            libraryLoader.UnloadAssembly();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void LoadAndTryManyThreads()
    {
        try
        {
            var libraryLoader = new AssemblyLoader();
            libraryLoader.LoadAssemblyFromProjectInSameSolution("ClassLibraryClasses");

            object? calculatorInstance = libraryLoader.GetClassInstanceFromAssembly("MyCalculator");

            if (calculatorInstance != null && calculatorInstance is ICalculator)
            {
                ICalculator calculator = (ICalculator)calculatorInstance;

                Console.ReadLine();
                StartManyThreads(calculator);
            }

            libraryLoader.UnloadAssembly();
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void StartManyThreads(ICalculator calculator)
    {
        int numThreads = 1000; // Кількість потоків для створення
        Thread[] threads = new Thread[numThreads];

        // Генерація випадкового масиву для сортування
        int[] array = GenerateRandomArray(1000);

        // Створення та запуск потоків
        for (int i = 0; i < numThreads; i++)
        {
            threads[i] = new Thread(() =>
            {
                calculator.QuickSort(array, 0, array.Length - 1);
            });
            threads[i].Start();
        }

        // Очікування завершення всіх потоків
        foreach (Thread t in threads)
        {
            t.Join();
        }

        Console.WriteLine("All threads have finished sorting.");
    }

    static int[] GenerateRandomArray(int size)
    {
        Random rand = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = rand.Next(1000); // Для прикладу, максимальне значення - 1000
        }
        return array;
    }
}
