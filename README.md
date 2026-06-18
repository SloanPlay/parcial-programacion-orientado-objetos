# Liquidador de Nomina Industrial con Horas Extra Complejas

Sistema en C# para procesar la nomina semanal (6 dias laborables) de una empresa textil en Republica Dominicana. Calcula el salario en pesos dominicanos (RD$) considerando horas normales, horas extra dobles, horas extra triples y deduccion de ISR cuando aplica.

El comprobante de pago se emite bajo el membrete de **Textiles Caribe, S.R.L.** con formato profesional incluyendo datos de la empresa, del empleado, registro de asistencia y desglose completo del salario.

## Reglas de negocio

| Concepto               | Regla                                     |
|------------------------|-------------------------------------------|
| Horas normales         | Primeras 40 horas a tarifa normal         |
| Horas extra dobles     | Hora 41 a 48 (tarifa x 2)                 |
| Horas extra triples    | Horas > 48 (tarifa x 3)                   |
| ISR                    | 10% si el salario bruto > RD$120,000.00   |
| Validacion por dia     | 0 a 12 horas, sin valores negativos       |

## Tabla de pruebas

| Caso | Empleado        | Cargo                    | Valor Hora  | Lun | Mar | Mie | Jue | Vie | Sab | Total Horas | H. Normales | H. Dobles | H. Triples | Salario Bruto   | ISR (10%)      | Salario Neto    |
|------|----------------|--------------------------|-------------|-----|-----|-----|-----|-----|-----|-------------|-------------|-----------|------------|-----------------|----------------|-----------------|
| 1    | Ana Lopez      | Operaria de Confeccion   | RD$150.00   | 8   | 7   | 8   | 7   | 5   | 5   | 40          | 40          | 0         | 0          | RD$6,000.00     | RD$0.00        | RD$6,000.00     |
| 2    | Carlos Ruiz    | Operario de Corte        | RD$200.00   | 9   | 9   | 8   | 8   | 8   | 6   | 48          | 40          | 8         | 0          | RD$11,200.00    | RD$0.00        | RD$11,200.00    |
| 3    | Maria Diaz     | Jefa de Taller           | RD$2,500.00 | 9   | 9   | 9   | 9   | 9   | 9   | 54          | 40          | 8         | 6          | RD$185,000.00   | RD$18,500.00   | RD$166,500.00   |

### Desglose por caso

**Caso 1 -- Operaria textil, semana normal (sin horas extra):**
- 40 horas normales x RD$150.00 = RD$6,000.00
- Salario bruto RD$6,000.00 <= RD$120,000.00 -> No aplica ISR

**Caso 2 -- Operario de corte, horas extra dobles (sin ISR):**
- 40 horas normales x RD$200.00 = RD$8,000.00
- 8 horas dobles x RD$400.00 = RD$3,200.00
- Salario bruto RD$11,200.00 <= RD$120,000.00 -> No aplica ISR

**Caso 3 -- Jefa de taller, horas extra triples con ISR:**
- 40 horas normales x RD$2,500.00 = RD$100,000.00
- 8 horas dobles x RD$5,000.00 = RD$40,000.00
- 6 horas triples x RD$7,500.00 = RD$45,000.00
- Salario bruto RD$185,000.00 > RD$120,000.00 -> ISR 10% = RD$18,500.00

## Estructura del proyecto

```
.
+-- Program.cs                          # Codigo fuente (clase Empleado + Main)
+-- parcial-programacion-orientado-objetos.csproj
+-- README.md
```

## Ejecucion

```bash
dotnet run
```
