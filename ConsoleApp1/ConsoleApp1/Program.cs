using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static int lap = 0;
        private static int maxLap = 100;
        private static double total = 0;
        private static string calc = "price + tax";

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            for (int i = 0; i < maxLap; i++)
            {
                // 
                stopwatch.Start();
                Program.CalcColumns();
                stopwatch.Stop();

                TimeSpan timeSpan1 = stopwatch.Elapsed;
                Console.WriteLine($"{timeSpan1}");
                stopwatch.Reset();

                //
                stopwatch.Start();
                TEST test = new TEST();
                stopwatch.Stop();

                TimeSpan timeSpan2 = stopwatch.Elapsed;
                Console.WriteLine($"{timeSpan2}");
                stopwatch.Reset();

                double multiple = timeSpan1.TotalMilliseconds / timeSpan2.TotalMilliseconds;
                Console.WriteLine($"{multiple}");
                total += multiple;
                
                Console.WriteLine();
                lap++;
            }

            Console.WriteLine($"{total / maxLap}");

            Console.ReadKey();
        }

        private static void CalcColumns()
        {

            DataTable table = new DataTable();

            // Create the first column.
            DataColumn priceColumn = new DataColumn();
            priceColumn.DataType = typeof(decimal);
            priceColumn.ColumnName = "price";
            priceColumn.DefaultValue = 50;

            // Create the second, calculated, column.
            DataColumn taxColumn = new DataColumn();
            taxColumn.DataType = typeof(decimal);
            taxColumn.ColumnName = "tax";
            taxColumn.Expression = "price * 0.0862";

            // Create third column.
            DataColumn totalColumn = new DataColumn();
            totalColumn.DataType = typeof(decimal);
            totalColumn.ColumnName = "total";
            totalColumn.Expression = calc;

            // Add columns to DataTable.
            table.Columns.Add(priceColumn);
            table.Columns.Add(taxColumn);
            table.Columns.Add(totalColumn);

            DataRow row = table.NewRow();
            table.Rows.Add(row);
            //table.Compute("total", null);
            Console.WriteLine($"{calc} = {row[totalColumn]}");
        }
    }

    public class TEST
    {
        public TEST(decimal price = 50)
        {
            Price = price;
            Tax = Price * 0.0862m;
            Total = Price + Tax;
            Console.WriteLine($"price + tax = {Total}");
        }

        decimal Price { get; set; }
        decimal Tax { get; set; }
        decimal Total { get; set; }
    }
}
