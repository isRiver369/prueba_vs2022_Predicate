using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace MiNuevoProyecto
{
    internal class Program
    {
        // --- AUX ---
        List<Item> items = new List<Item>();
        int contadorId = 0;

        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("MENÚ CRUD");
            Console.WriteLine("1. Crear Item");
            Console.WriteLine("2. Buscar Item");
            Console.WriteLine("3. Actualizar Item");
            Console.WriteLine("4. Eliminar Item");
            Console.WriteLine("5. Salir");
            Console.Write("Ingrese una opción (1 - 5): ");
        }

        // --- CRUD ---
        public void CrearItem()
        {
            Console.Write("Ingrese la cantidad de items que desea crear: ");
            string texto = Console.ReadLine();
            bool validar = int.TryParse(texto, out int cantidad);

            Random rnd = new Random();
            Faker nombreFaker = new Faker("es");


            if (validar)
            {
                for (int i = 0; i < cantidad; i++)
                {
                    Item nuevoItem = new Item();

                    nuevoItem.Id = contadorId;
                    nuevoItem.Nombre = nombreFaker.Commerce.Product();
                    nuevoItem.Precio = rnd.Next(5, 11);

                    contadorId++;
                    items.Add(nuevoItem);
                }

                Console.WriteLine("Items creados!");
            }
        }

        public void BuscarItem()
        {
            foreach (var i in items)
            {
                Console.WriteLine();
                Console.WriteLine($"ID: {i.Id}");
                Console.WriteLine($"Nombre: {i.Nombre}");
                Console.WriteLine($"Precios: ${i.Precio}");
            }
        }

        public void ActualizarItem()
        {
            Console.Write("Ingrese ID del producto que desea Actualizar: ");
            string entrada = Console.ReadLine();

            if (int.TryParse(entrada, out int buscarId))
            {
                // Definimos un Predicate para encontrar el item
                Predicate<Item> predicadoBuscar = item => item.Id == buscarId;

                // Buscamos el item usando el predicado
                Item itemAActualizar = items.Find(predicadoBuscar);

                if (itemAActualizar != null) // Si se encontró el item
                {
                    Console.Write("Ingrese nuevo nombre: ");
                    string nuevoNombre = Console.ReadLine();
                    itemAActualizar.Nombre = nuevoNombre;

                    Console.Write("Ingrese nuevo precio: ");
                    string nuevoPrecio = Console.ReadLine();
                    bool auxValidar = int.TryParse(nuevoPrecio, out int nPrecio);

                    if (auxValidar)
                    {
                        itemAActualizar.Precio = nPrecio;
                        Console.WriteLine("Actualizació terminada.");
                    }
                    else
                    {
                        Console.WriteLine("Precio no válido.");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontró un item con ese ID.");
                }
            }
            else
            {
                Console.WriteLine("ID no válido.");
            }
        }

        public void EliminarItem()
        {
            Console.Write("Ingrese ID del producto que desea eliminar: ");
            string texto = Console.ReadLine();

            if (int.TryParse(texto, out int buscarId))
            {
                // Definimos un Predicate para encontrar el item a eliminar
                Predicate<Item> predicadoEliminar = item => item.Id == buscarId;

                // Buscamos el item para confirmar que existe
                Item itemAEliminar = items.Find(predicadoEliminar);

                if (itemAEliminar != null)
                {
                    items.Remove(itemAEliminar);
                    Console.WriteLine("Elimincación terminada!");
                }
                else
                {
                    Console.WriteLine("No se encontró un item con ese ID.");
                }
            }
            else
            {
                Console.WriteLine("ID no válido");
            }

        }

        // --- MAIN ---
        static void Main(string[] args)
        {
            Program Crud = new Program();
            ConsoleKeyInfo key;
            int opcion;


            do
            {
                Crud.Menu();
                string texto = Console.ReadLine();
                bool validar = int.TryParse(texto, out opcion);

                switch (opcion)
                {
                    case 1:
                        Crud.CrearItem();
                        break;
                    case 2:
                        Crud.BuscarItem();
                        break;
                    case 3:
                        Crud.ActualizarItem();
                        break;
                    case 4:
                        Crud.EliminarItem();
                        break;
                    case 5:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                if (opcion != 5)
                {
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    key = Console.ReadKey();
                }
            }
            while (opcion != 5);
        }
    }
}
