class Empleado
{
    public string Nombre { get; set; } = "";
    public string Cargo { get; set; } = "";
    public decimal ValorHora { get; set; }
    public decimal[] HorasDiarias { get; set; } = new decimal[6];

    public void CapturarHoras()
    {
        string[] dias = { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado" };

        for (int i = 0; i < 6; i++)
        {
            decimal horas;
            bool valido = false;

            while (!valido)
            {
                Console.Write($"  Horas trabajadas el {dias[i],-9}: ");
                string? entrada = Console.ReadLine();

                if (decimal.TryParse(entrada, out horas))
                {
                    if (horas < 0)
                        Console.WriteLine("    Error: No se permiten horas negativas.");
                    else if (horas > 12)
                        Console.WriteLine("    Error: Maximo 12 horas por dia.");
                    else
                    {
                        HorasDiarias[i] = horas;
                        valido = true;
                    }
                }
                else
                {
                    Console.WriteLine("    Error: Ingrese un valor numerico valido.");
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
    static readonly int W = 67;
    static readonly string D = new string('=', W);
    static readonly string S = new string('-', W);

    static string Centro(string texto)
    {
        int espacios = (W - texto.Length) / 2;
        if (espacios < 0) espacios = 0;
        return new string(' ', espacios) + texto;
    }

    static string Relleno(string izquierda, string derecha)
    {
        int espacios = W - izquierda.Length - derecha.Length - 2;
        if (espacios < 0) espacios = 1;
        return " " + izquierda + new string(' ', espacios) + derecha + " ";
    }

    static string RellenoDer(string izquierda, string derecha)
    {
        int espacios = W - izquierda.Length - derecha.Length - 3;
        if (espacios < 0) espacios = 1;
        return "  " + izquierda + new string(' ', espacios) + derecha + " ";
    }

    static string FechaSemana()
    {
        DateTime hoy = DateTime.Now;
        int diff = (7 + (hoy.DayOfWeek - DayOfWeek.Monday)) % 7;
        DateTime lunes = hoy.AddDays(-diff);
        DateTime sabado = lunes.AddDays(5);
        return $"{lunes:dd/MM/yyyy} al {sabado:dd/MM/yyyy}";
    }

    static void Borde(string contenido)
    {
        Console.WriteLine($"| {contenido}{new string(' ', Math.Max(0, W - contenido.Length - 3))}|");
    }

    static void BordeCentro(string contenido)
    {
        int espacios = (W - contenido.Length - 2) / 2;
        if (espacios < 0) espacios = 0;
        int resto = W - 2 - espacios - contenido.Length;
        if (resto < 0) resto = 0;
        Console.WriteLine($"|{new string(' ', espacios)}{contenido}{new string(' ', resto)}|");
    }

    static void Main()
    {
        Console.Clear();

        Console.WriteLine($"+{D}+");
        BordeCentro("TEXTILES CAMPUSANO, S.R.L.");
        BordeCentro("RNC: 1-30-98765-4");
        BordeCentro("Zona Franca Industrial de Santiago");
        BordeCentro("Tel: (809) 555-0190  |  nomina@textilescaribe.com.do");
        Console.WriteLine($"+{D}+");
        BordeCentro("COMPROBANTE DE PAGO SEMANAL");
        BordeCentro("Liquidador de Nomina con Horas Extra");
        Console.WriteLine($"+{D}+");

        Empleado emp = new Empleado();

        Console.Write("  Nombre del empleado: ");
        emp.Nombre = Console.ReadLine() ?? "";

        Console.Write("  Cargo / Posicion:    ");
        emp.Cargo = Console.ReadLine() ?? "";

        decimal valor;
        bool valorValido = false;
        while (!valorValido)
        {
            Console.Write("  Valor por hora (RD$): ");
            string? entrada = Console.ReadLine();
            if (decimal.TryParse(entrada, out valor) && valor > 0)
            {
                emp.ValorHora = valor;
                valorValido = true;
            }
            else
            {
                Console.WriteLine("    Error: Ingrese un valor monetario valido y mayor a 0.");
            }
        }

        Console.WriteLine();
        Console.WriteLine($"  Registro de horas trabajadas");
        Console.WriteLine($"  Semana: {FechaSemana()}");
        Console.WriteLine($"  {new string('-', 50)}");
        emp.CapturarHoras();

        Console.WriteLine();
        Console.WriteLine("  Calculando nomina...");
        DesgloseSalario d = emp.CalcularSalario();

        Console.Clear();

        string semana = FechaSemana();
        string fechaEmision = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");

        Console.WriteLine($"+-{D}-+");
        BordeCentro("TEXTILES CAMPUSANO, S.R.L.");
        BordeCentro("RNC: 1-30-98765-4  |  Zona Franca Industrial, Santiago");
        BordeCentro("COMPROBANTE DE PAGO DE NOMINA SEMANAL");
        Console.WriteLine($"+-{D}-+");

        Console.WriteLine($"| {new string(' ', W)} |");
        Console.WriteLine($"| {Relleno("DATOS DEL EMPLEADO", "")}");
        Console.WriteLine($"| {new string(' ', W)} |");
        Console.WriteLine($"| {Relleno("Nombre:", emp.Nombre)}");
        Console.WriteLine($"| {Relleno("Cargo:", emp.Cargo)}");
        Console.WriteLine($"| {Relleno("Valor por Hora:", $"RD${emp.ValorHora:N2}")}");
        Console.WriteLine($"| {Relleno("Semana:", semana)}");
        Console.WriteLine($"| {new string(' ', W)} |");

        Console.WriteLine($"+-{D}-+");
        Console.WriteLine($"| {Relleno("REGISTRO DE ASISTENCIA", "")}");
        Console.WriteLine($"+-{D}-+");
        Console.WriteLine($"| {new string(' ', W)} |");

        string[] dias = { "Lun", "Mar", "Mie", "Jue", "Vie", "Sab" };
        string encabezadoDias = "";
        string valoresDias = "";
        for (int i = 0; i < 6; i++)
        {
            encabezadoDias += $"  {dias[i]}  ";
            valoresDias += $" {emp.HorasDiarias[i],4:N1} ";
        }
        BordeCentro(encabezadoDias.TrimEnd());
        BordeCentro(valoresDias.TrimEnd());

        Console.WriteLine($"| {new string(' ', W)} |");
        Console.WriteLine($"| {RellenoDer("Total horas semanales:", $"{d.TotalHoras:N1} hrs")}");

        Console.WriteLine($"| {new string(' ', W)} |");
        Console.WriteLine($"+-{D}-+");
        Console.WriteLine($"| {Relleno("DESGLOSE DE SALARIO", "")}");
        Console.WriteLine($"+-{D}-+");
        Console.WriteLine($"| {new string(' ', W)} |");
        Console.WriteLine($"|   {"Concepto",-23} {"Horas",8} {"Tarifa",14} {"Monto",18} |");
        Console.WriteLine($"|   {new string('-', 23)} {new string('-', 8)} {new string('-', 14)} {new string('-', 18)} |");

        string filaNormal = $"|   {"Horas Normales",-23} {d.HorasNormales,8:N1} {($"RD${emp.ValorHora:N2}"),14} {($"RD${d.SalarioNormal:N2}"),18} |";
        string filaDoble = $"|   {"Horas Extra Dobles",-23} {d.HorasDobles,8:N1} {($"RD${emp.ValorHora * 2:N2}"),14} {($"RD${d.SalarioDoble:N2}"),18} |";
        string filaTriple = $"|   {"Horas Extra Triples",-23} {d.HorasTriples,8:N1} {($"RD${emp.ValorHora * 3:N2}"),14} {($"RD${d.SalarioTriple:N2}"),18} |";

        Console.WriteLine(filaNormal);
        Console.WriteLine(filaDoble);
        Console.WriteLine(filaTriple);

        Console.WriteLine($"|   {new string('-', 23)} {new string('-', 8)} {new string('-', 14)} {new string('-', 18)} |");
        Console.WriteLine($"|   {"SALARIO BRUTO",-23} {"",8} {"",14} {($"RD${d.SalarioBruto:N2}"),18} |");

        if (d.ISR > 0)
        {
            Console.WriteLine($"|   {"ISR (10%)",-23} {"",8} {"",14} {($"-RD${d.ISR:N2}"),18} |");
            Console.WriteLine($"|   {new string('-', 23)} {new string('-', 8)} {new string('-', 14)} {new string('-', 18)} |");
            Console.WriteLine($"|   {"SALARIO NETO",-23} {"",8} {"",14} {($"RD${d.SalarioNeto:N2}"),18} |");
        }
        else
        {
            Console.WriteLine($"|   {"ISR (10%)",-23} {"",8} {"",14} {"RD$0.00  (Exento)",18} |");
            Console.WriteLine($"|   {new string('-', 23)} {new string('-', 8)} {new string('-', 14)} {new string('-', 18)} |");
            Console.WriteLine($"|   {"SALARIO NETO",-23} {"",8} {"",14} {($"RD${d.SalarioNeto:N2}"),18} |");
        }

        Console.WriteLine($"| {new string(' ', W)} |");
        if (d.ISR == 0)
            Console.WriteLine($"|   {"( * ) Salario bruto <= RD$120,000.00: Exento de ISR"}   {new string(' ', Math.Max(0, W - 55))}|");

        Console.WriteLine($"| {new string(' ', W)} |");
        Console.WriteLine($"+-{D}-+");
        Console.WriteLine($"| {Relleno("Emitido:", fechaEmision)}");
        Console.WriteLine($"+-{D}-+");
        Console.WriteLine($"| {new string(' ', W)} |");
        Console.WriteLine($"|   {"_________________________",-30} {"_________________________"} |");
        Console.WriteLine($"|   {"Firma del Empleado",-30} {"Recibi Conforme - RRHH"} |");
        Console.WriteLine($"| {new string(' ', W)} |");
        Console.WriteLine($"+{D}+");

        Console.WriteLine("\n  Presione cualquier tecla para salir...");
        Console.ReadKey();
    }
}
