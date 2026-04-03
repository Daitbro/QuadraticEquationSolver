using Avalonia.Controls;
using System;

namespace QuadraticEquationSolver.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        var aBox = this.FindControl<TextBox>("A");
        var bBox = this.FindControl<TextBox>("B");
        var cBox = this.FindControl<TextBox>("C");
        var solveBtn = this.FindControl<Button>("SolveBtn");
        var resultBox = this.FindControl<TextBox>("Result");
        
        if (solveBtn != null)
            solveBtn.Click += (s, e) => Solve(aBox, bBox, cBox, resultBox);
    }
    
    private void Solve(TextBox? aBox, TextBox? bBox, TextBox? cBox, TextBox? resultBox)
    {
        if (resultBox == null) return;
        
        try
        {
            double a = double.Parse(aBox?.Text?.Replace('.', ',') ?? "0");
            double b = double.Parse(bBox?.Text?.Replace('.', ',') ?? "0");
            double c = double.Parse(cBox?.Text?.Replace('.', ',') ?? "0");
            
            if (a == 0)
            {
                if (b == 0)
                    resultBox.Text = c == 0 ? "Бесконечно много решений" : "Нет решений";
                else
                    resultBox.Text = $"x = {(-c / b):F4}";
                return;
            }
            
            double d = b * b - 4 * a * c;
            
            if (d > 0)
            {
                double x1 = (-b + Math.Sqrt(d)) / (2 * a);
                double x2 = (-b - Math.Sqrt(d)) / (2 * a);
                resultBox.Text = $"x₁ = {x1:F4}\nx₂ = {x2:F4}";
            }
            else if (Math.Abs(d) < 0.0001)
            {
                resultBox.Text = $"x = {(-b / (2 * a)):F4}";
            }
            else
            {
                double r = -b / (2 * a);
                double i = Math.Sqrt(-d) / (2 * a);
                resultBox.Text = $"x₁ = {r:F4} + {i:F4}i\nx₂ = {r:F4} - {i:F4}i";
            }
        }
        catch
        {
            resultBox.Text = "Ошибка! Введите числа";
        }
    }
}