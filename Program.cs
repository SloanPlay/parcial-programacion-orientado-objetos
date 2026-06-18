class Empleado
{
    public string Nombre { get; set; } = "";
    public decimal ValorHora { get; set; }
    public decimal[] HorasDiarias { get; set; } = new decimal[6];

    public void CapturarHoras()
    {
        string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };

        for (int i = 0; i < 6; i++)
        {
            decimal horas;
            bool valido = false;

            while (!valido)
            {
                Console.Write($"Horas trabajadas el {dias[i]}: ");
                string? entrada = Console.ReadLine();

                if (decimal.TryParse(entrada, out horas))
                {
                    if (horas < 0)
                        Console.WriteLine("  Error: No se permiten horas negativas.");
                    else if (horas > 12)
                        Console.WriteLine("  Error: Máximo 12 horas por día.");
                    else
                    {
                        HorasDiarias[i] = horas;
                        valido = true;
                    }
                }
                else
                {
                    Console.WriteLine("  Error: Ingrese un valor numérico válido.");
                }
            }
        }
    }

    public DesgloseSalario CalcularSalario()
    {
        decimal totalHoras = 0;
        for (int i = 0; i < 6; i++)
            totalHoras += HorasDiarias[i];

        decimal horasNormales = Math.Min(totalHoras, 40);
        decimal horasDobles = Math.Max(0, Math.Min(totalHoras - 40, 8));
        decimal horasTriples = Math.Max(0, totalHoras - 48);

        decimal salarioNormal = horasNormales * ValorHora;
        decimal salarioDoble = horasDobles * ValorHora * 2;
        decimal salarioTriple = horasTriples * ValorHora * 3;
        decimal salarioBruto = salarioNormal + salarioDoble + salarioTriple;

        decimal isr = 0;
        if (salarioBruto > 120000)
            isr = salarioBruto * 0.10m;

        decimal salarioNeto = salarioBruto - isr;

        return new DesgloseSalario
        {
            TotalHoras = totalHoras,
            HorasNormales = horasNormales,
            HorasDobles = horasDobles,
            HorasTriples = horasTriples,
            SalarioNormal = salarioNormal,
            SalarioDoble = salarioDoble,
            SalarioTriple = salarioTriple,
            SalarioBruto = salarioBruto,
            ISR = isr,
            SalarioNeto = salarioNeto
        };
    }
}

class DesgloseSalario
{
    public decimal TotalHoras { get; set; }
    public decimal HorasNormales { get; set; }
    public decimal HorasDobles { get; set; }
    public decimal HorasTriples { get; set; }
    public decimal SalarioNormal { get; set; }
    public decimal SalarioDoble { get; set; }
    public decimal SalarioTriple { get; set; }
    public decimal SalarioBruto { get; set; }
    public decimal ISR { get; set; }
    public decimal SalarioNeto { get; set; }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("===========================================");
        Console.WriteLine("  LIQUIDADOR DE NÓMINA INDUSTRIAL");
        Console.WriteLine("  con Horas Extra Complejas");
        Console.WriteLine("===========================================\n");

        Empleado emp = new Empleado();

        Console.Write("Nombre del empleado: ");
        emp.Nombre = Console.ReadLine() ?? "";

        decimal valor;
        bool valorValido = false;
        while (!valorValido)
        {
            Console.Write("Valor por hora (RD$): ");
            string? entrada = Console.ReadLine();
            if (decimal.TryParse(entrada, out valor) && valor > 0)
            {
                emp.ValorHora = valor;
                valorValido = true;
            }
            else
            {
                Console.WriteLine("  Error: Ingrese un valor monetario válido y mayor a 0.");
            }
        }

        Console.WriteLine("\n--- Registro de horas trabajadas (semana de 6 días) ---");
        emp.CapturarHoras();

        Console.WriteLine("\n--- Cálculo de salario ---");
        DesgloseSalario d = emp.CalcularSalario();

        Console.WriteLine("\n===========================================");
        Console.WriteLine("          DESGLOSE DE NÓMINA");
        Console.WriteLine("===========================================");
        Console.WriteLine($"Empleado:            {emp.Nombre}");
        Console.WriteLine($"Valor por hora:      RD${emp.ValorHora:N2}");
        Console.WriteLine("-------------------------------------------");

        string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
        for (int i = 0; i < 6; i++)
            Console.WriteLine($"  {dias[i],-11} {emp.HorasDiarias[i],5:N1} horas");

        Console.WriteLine("-------------------------------------------");
        Console.WriteLine($"Total horas:         {d.TotalHoras,5:N1}");
        Console.WriteLine();
        Console.WriteLine($"Horas normales:      {d.HorasNormales,5:N1}  x  RD${emp.ValorHora:N2}   = RD${d.SalarioNormal,10:N2}");
        Console.WriteLine($"Horas dobles:        {d.HorasDobles,5:N1}  x  RD${emp.ValorHora * 2:N2}   = RD${d.SalarioDoble,10:N2}");
        Console.WriteLine($"Horas triples:       {d.HorasTriples,5:N1}  x  RD${emp.ValorHora * 3:N2}   = RD${d.SalarioTriple,10:N2}");
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine($"Salario bruto:                        RD${d.SalarioBruto,10:N2}");

        if (d.ISR > 0)
        {
            Console.WriteLine($"ISR (10%):                           -RD${d.ISR,10:N2}");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"SALARIO NETO:                         RD${d.SalarioNeto,10:N2}");
        }
        else
        {
            Console.WriteLine($"ISR (10%):                             RD$    ---.--");
            Console.WriteLine($"SALARIO NETO:                         RD${d.SalarioNeto,10:N2}");
            Console.WriteLine("  (No aplica ISR: salario bruto <= RD$120,000.00)");
        }

        Console.WriteLine("===========================================\n");
        Console.WriteLine("Presione cualquier tecla para salir...");
        Console.ReadKey();
    }
}
