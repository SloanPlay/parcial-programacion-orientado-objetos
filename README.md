# Liquidador de Nómina Industrial con Horas Extra Complejas

Sistema en C# para procesar la nómina semanal (6 días laborables) de una empresa textil. Calcula el salario considerando horas normales, horas extra dobles, horas extra triples y deducción de ISR cuando aplica.

## Reglas de negocio

| Concepto               | Regla                              |
|------------------------|------------------------------------|
| Horas normales         | Primeras 40 horas a tarifa normal  |
| Horas extra dobles     | Hora 41 a 48 (tarifa x 2)          |
| Horas extra triples    | Horas > 48 (tarifa x 3)            |
| ISR                    | 10% si el salario bruto > $1,200   |
| Validación por día     | 0 a 12 horas, sin valores negativos|

## Tabla de pruebas

| Caso | Empleado     | Valor Hora | Lun | Mar | Mié | Jue | Vie | Sáb | Total Horas | H. Normales | H. Dobles | H. Triples | Salario Bruto | ISR (10%) | Salario Neto |
|------|-------------|------------|-----|-----|-----|-----|-----|-----|-------------|-------------|-----------|------------|---------------|-----------|--------------|
| 1    | Ana López   | $10.00     | 7   | 7   | 7   | 7   | 6   | 6   | 40          | 40          | 0         | 0          | $400.00       | $0.00     | $400.00      |
| 2    | Carlos Ruiz | $15.00     | 8   | 8   | 8   | 8   | 8   | 8   | 48          | 40          | 8         | 0          | $840.00       | $0.00     | $840.00      |
| 3    | María Díaz  | $25.00     | 10  | 10  | 10  | 10  | 10  | 10  | 60          | 40          | 8         | 12         | $2,300.00     | $230.00   | $2,070.00    |

### Desglose por caso

**Caso 1 — Semana normal (sin horas extra):**
- 40 horas normales x $10.00 = $400.00
- Salario bruto $400.00 ≤ $1,200 → No aplica ISR

**Caso 2 — Horas extra dobles (sin ISR):**
- 40 horas normales x $15.00 = $600.00
- 8 horas dobles x $30.00 = $240.00
- Salario bruto $840.00 ≤ $1,200 → No aplica ISR

**Caso 3 — Horas extra triples con ISR:**
- 40 horas normales x $25.00 = $1,000.00
- 8 horas dobles x $50.00 = $400.00
- 12 horas triples x $75.00 = $900.00
- Salario bruto $2,300.00 > $1,200 → ISR 10% = $230.00

## Estructura del proyecto

```
.
├── Program.cs                          # Código fuente (clase Empleado + Main)
├── parcial-programacion-orientado-objetos.csproj
└── README.md
```

## Ejecución

```bash
dotnet run
```
