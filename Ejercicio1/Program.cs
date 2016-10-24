using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1
{
    class Program
    {
        struct Empleado
        {
            public int horas;
            public int precio;
            public string nombre;
            public string dni;
            public float total;
        }
        static void Main(string[] args)
        {
            // VARIABLES
            int horas = 0;
            int precio = 0;
            float total = 0;
            string nombre = "";
            string dni = "";
            int n = 0;
            char x = ' ';
            Empleado[] empleados=null;
            Empleado[] empleadoCopy = null;
            Empleado empleadoAuxiliar;
            

            bool flagFin = true;
            bool flagEntrada = true;
            bool flagBorrar = true;
            bool flagDni = true;
            bool flagHoras = true;
            bool flagPrecio = true;
            bool flagRepetirEntrada = true;
            
            bool flagRepetirMostrar = true;
            string eleccion = "";

            do
            {
                Console.WriteLine("Bienvenido a OpaOpa S.L.      ¿Que desea hacer? \n");
                Console.WriteLine("1. Introducir un empleado\n2. Borrar un empleado\n3. Modificar empleado\n4. Mostrar empleados\n5. Ayuda\n6. Salir " );

                eleccion = Console.ReadLine();
                eleccion.ToLower();

                switch (eleccion)
                {   
                    // INTRODUCCION DE DATOS

                    case "1":
                    case "introducir":  

                        Console.Clear();
                        do
                        {
                            flagEntrada = true;
                            Console.WriteLine("introduzca un nombre:");
                            nombre = Console.ReadLine();
                            do
                            {
                                flagDni = true;
                                Console.WriteLine("Introduzca un DNI:");
                                dni = Console.ReadLine();
                                try
                                {
                                    if ((dni.Length == 9) & (Int32.TryParse(dni.Substring(0, 7), out n)) && (char.TryParse(dni.Substring(8), out x)))
                                    {
                                        if (char.IsLetter(x))
                                        {
                                            flagDni = false;
                                            do
                                            {
                                                flagHoras = true;
                                                Console.WriteLine("Introduzca las horas");
                                                if (Int32.TryParse(Console.ReadLine(), out horas))
                                                {
                                                    flagHoras = false;
                                                    do
                                                    {
                                                        flagPrecio = true;
                                                        Console.WriteLine("Introduzca el precio");
                                                        if (Int32.TryParse(Console.ReadLine(), out precio))
                                                        {
                                                            flagPrecio = false;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Ha introducido un numero incorrecto");
                                                        }
                                                    } while (flagPrecio);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Ha introducido un numero incorrecto");
                                                }
                                            } while (flagHoras);
                                        }else
                                        {
                                            Console.WriteLine("Letra del dni incorrectamenta. Por favor, vuelva a intentarlo");
                                        }
                                    }else
                                    {
                                        Console.WriteLine("Ha introducido un dni incorrecto. Por favor, vuelva a intentarlo");
                                    }
                                } catch (ArgumentException e)
                                {
                                    Console.WriteLine("Ha introducido un dni incorrecto. Por favor, vuelva a intentarlo");
                                }                                
                            } while (flagDni);                         

                            // CALCULO DEL TOTAL

                            if (horas<=37)
                            {
                                total = horas * precio;
                            } else
                            {
                                total = 37 * precio;
                                total += (horas - 37) * (precio * 1.25f);
                            }

                            if (total > 100 && total <=200)
                            {
                                
                                total += (total - 100) * 1.15f;

                            } else if (total > 200)
                            {
                                total += ((total - 100) * 1.15f)+ ((total - 200) * 1.22f);
                            }

                            // PROCESAMIENTO TOTAL

                            empleadoAuxiliar.dni = dni;
                            empleadoAuxiliar.nombre = nombre;
                            empleadoAuxiliar.horas = horas;
                            empleadoAuxiliar.precio = precio;
                            empleadoAuxiliar.total = total;

                            if (empleados==null)
                            {
                                empleados = new Empleado[1];
                                
                            } else
                            {
                                empleadoCopy = new Empleado[empleados.Length];
                                empleados.CopyTo(empleadoCopy, 0);
                                empleados = new Empleado[empleadoCopy.Length +1];
                                empleadoCopy.CopyTo(empleados,0);
                                
                            }

                            empleados[empleados.Length-1] = empleadoAuxiliar;

                            do
                            {
                                flagRepetirEntrada = true;
                                Console.WriteLine("Empleado ingresado correctamente. ¿Desea introducir otro empleado? \n1.Si\n2.No");
                                switch (Console.ReadLine().ToLower())
                                {
                                    case "1":
                                    case "si":

                                        flagRepetirEntrada = false;
                                        break;
                                    case "2":
                                    case "no":

                                        flagRepetirEntrada = false;
                                        flagEntrada = false;
                                        break;
                                    default:
                                        Console.WriteLine("Ha introducido un valor incorrecto. Intentelo de nuevo");
                                        break;
                                }

                            } while (flagRepetirEntrada);
                            

                        } while (flagEntrada);

                        break;
                    
                    // BORRAR DATOS

                    case "2":
                    case "borrar":

                        do
                        {
                            flagBorrar = true;
                            try
                            {
                                if (empleados != null)
                                {
                                    Console.WriteLine("Introduzca el DNI o NOMBRE de la persona que quiera eliminar");
                                    string eliminar = Console.ReadLine() ;
                                    eliminar.ToLower();
                                    int encontrado = -1; // VARIABLE PARA REFLEJAR LA POSICION DEL QUE QUERERMOS BORRAR

                                    for (int i = 0; i < empleados.Length; i++)
                                    {
                                        if (eliminar.Equals(empleados[i].nombre) || eliminar.Equals(empleados[i].dni))                              // SI ENCUENTRA EL EMPLEADO INTRODUCIDO
                                        {
                                            encontrado = i;
                                            Console.WriteLine("Se ha encontrado el empleado " + empleados[i].nombre + " con dni " + empleados[i].dni);
                                        }
                                    }
                                        if (encontrado != -1)
                                        {
                                            
                                            empleadoCopy = new Empleado[empleados.Length];
                                            empleados.CopyTo(empleadoCopy, 0);
                                            empleados = new Empleado[empleadoCopy.Length - 1];

                                            for (int j = 0; j <= encontrado; j++)
                                            {
                                                empleados[j]=empleadoCopy[j];
                                            }

                                            for (int i = encontrado;  i < empleados.Length; i++)
                                            {
                                            empleados[i] = empleadoCopy[i + 1];                                        
                                            }
                                            Console.WriteLine("El empleado ha sido borrado correctamente");
                                            flagBorrar = false;
                                        }else
                                    {
                                        Console.WriteLine("No se ha encontrado al empleado.  ¿Volver a intentarlo?\n1. Si\n 2.No");         // SI NO ENCUENTRA NINGUN EMPLEADO, VOLVER A PREGUNTAR

                                        switch (Console.ReadLine())
                                        {
                                            case "1":
                                            case "si":

                                                flagBorrar = true;
                                                break;
                                            case "2":
                                            case "no":
                                                flagBorrar = false;
                                                break;
                                            default:
                                                Console.WriteLine("Ha introducido un valor incorrecto. Intentelo de nuevo");
                                                break;
                                        }
                                    }

                                    
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: No hay ningun empleado.");
                                    flagBorrar = false;
                                }
                            } catch (Exception error) { }

                            Console.WriteLine("Se ha borrado un empleado.  ¿Volver a intentarlo?\n1. Si\n 2.No");                       // DESPUES DE BORRAR, PREGUNTAR SI REPETIR

                            switch (Console.ReadLine())
                            {
                                case "1":
                                case "si":
                                    flagBorrar = true;
                                    break;
                                case "2":
                                case "no":
                                    flagBorrar = false;
                                    break;
                                default:
                                    Console.WriteLine("Ha introducido un valor incorrecto. Intentelo de nuevo");
                                    break;
                            }



                        } while (flagBorrar);
                        break;

                    // MODIFICACION DE DATOS

                    case "3":
                    case "modificar":

                        break;

                    // MOSTRAR DATOS

                    case "4":
                    case "mostrar":
                        do
                        {


                            if (empleados != null)
                            {
                                for (int i = 0; i < empleados.Length; i++)
                                {
                                    Console.WriteLine(empleados[i].nombre.PadRight(15) +empleados[i].dni.PadRight(15) + empleados[i].horas.ToString().PadRight(15) + empleados[i].precio.ToString().PadRight(15) + empleados[i].total.ToString().PadRight(15));
                                }
                                Console.WriteLine("¿Desea mostrar la lista de empleados de nuevo?");
                                switch (Console.ReadLine().ToLower())
                                {
                                    case "1":
                                    case "si":

                                        flagRepetirMostrar = true;
                                        break;
                                    case "2":
                                    case "no":

                                        flagRepetirMostrar = false;
                                        Console.Clear();
                                        break;
                                    default:
                                        Console.WriteLine("Ha introducido un valor incorrecto. Intentelo de nuevo");
                                        break;
                                }
                            } else
                            {
                                Console.WriteLine("ERROR: No se ha añadido ningun empleado");
                            }
                        } while (flagRepetirMostrar);
                        break;

                    // MOSTRAR AYUDA

                    case "5":
                    case "ayuda":

                        break;

                    // SALIDA

                    case "6":
                    case "salir":
                        flagFin = false;
                        break;
                    default:
                        Console.WriteLine("Valor introducido incorrecto. Por favor, vuelva a intentarlo");
                        break;
                }

            } while (flagFin);

            Console.WriteLine("\nEl programa se ha finalizado correctamente.\n");
            

            


        }
    }
}
