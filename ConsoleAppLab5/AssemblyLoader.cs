using ClassLibraryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLab5
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    namespace AssemblyLoader
    {
        // Ім'я файлу: AssemblyLoader.cs
        // Ремарка: Клас AssemblyLoader призначений для завантаження та взаємодії з збірками (assembly) у .NET проекті.
        // Він надає методи для завантаження збірок з власного проекту у тій самій рішенні (solution), або за абсолютним шляхом.
        // Клас також дозволяє створювати екземпляри класів з завантажених збірок та розгружати їх з поточного домену застосунку.
        // Автор: Андрій Сахно

        public class AssemblyLoader
        {
            public string? AssemblyName { get; private set; }
            private Assembly? _classesAssembly;

            public bool IsAssemblyLoaded()
            {
                return _classesAssembly != null;
            }

            public bool LoadAssemblyFromProjectInSameSolution(string assemblyName)
            {
                if (IsAssemblyLoaded())
                {
                    throw new Exception("Assembly is already loaded.");
                }

                try
                {
                    AssemblyName = assemblyName;
                    string? solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;
                    if (solutionRoot != null)
                    {
                        string filePath = Path.Combine(solutionRoot, $@"{AssemblyName}\bin\Debug\net8.0\{AssemblyName}.dll");
                        if (File.Exists(filePath))
                        {
                            _classesAssembly = Assembly.LoadFrom(filePath);
                            return true;
                        }
                        else
                        {
                            throw new FileNotFoundException($"Assembly {AssemblyName} not found at '{filePath}'.");
                        }
                    }
                    else
                    {
                        throw new DirectoryNotFoundException("Failed to find solution root directory path.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error loading assembly: {ex.Message}");
                }
            }

            public bool LoadAssemblyByAbsolutePath(string assemblyName, string absolutePath)
            {
                if (IsAssemblyLoaded())
                {
                    throw new Exception("Assembly is already loaded.");
                }

                try
                {
                    AssemblyName = assemblyName;
                    if (File.Exists(absolutePath))
                    {
                        _classesAssembly = Assembly.LoadFrom(absolutePath);
                        return true;
                    }
                    else
                    {
                        throw new FileNotFoundException($"Assembly not found at '{absolutePath}'.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error loading assembly from absolute path: {ex.Message}");
                }
            }

            public object? GetClassInstanceFromAssembly(string className)
            {
                if (!IsAssemblyLoaded())
                {
                    throw new Exception("Assembly is not loaded.");
                }

                try
                {
                    return _classesAssembly?.CreateInstance($"{AssemblyName}.{className}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error creating instance of {className}: {ex.Message}");
                }
            }

            public void UnloadAssembly()
            {
                try
                {
                    AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => a.FullName!.Contains(AssemblyName))
                        .ToList()
                        .ForEach(a => AppDomain.CurrentDomain.Load(a.GetName()));
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error unloading assembly: {ex.Message}");
                }
            }
        }
    }
}

// Кінець файлу