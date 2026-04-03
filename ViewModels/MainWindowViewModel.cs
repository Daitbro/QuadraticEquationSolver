using System;
using ReactiveUI;

namespace QuadraticEquationSolver.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _valueA = "";
    private string _valueB = "";
    private string _valueC = "";
    private string _result = "Введите коэффициенты a, b, c и нажмите 'Решить'";

    public string ValueA
    {
        get => _valueA;
        set => this.RaiseAndSetIfChanged(ref _valueA, value);
    }

    public string ValueB
    {
        get => _valueB;
        set => this.RaiseAndSetIfChanged(ref _valueB, value);
    }

    public string ValueC
    {
        get => _valueC;
        set => this.RaiseAndSetIfChanged(ref _valueC, value);
    }

    public string Result
    {
        get => _result;
        set => this.RaiseAndSetIfChanged(ref _result, value);
    }

    public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> SolveCommand { get; }

    public MainWindowViewModel()
    {
        SolveCommand = ReactiveCommand.Create(Solve);
    }

    private void Solve()
    {
        try
        {
            double a = double.Parse(ValueA.Replace('.', ','));
            double b = double.Parse(ValueB.Replace('.', ','));
            double c = double.Parse(ValueC.Replace('.', ','));

            if (a == 0)
            {
                if (b == 0)
                    Result = c == 0 ? "Бесконечно много решений" : "Нет решений";
                else
                    Result = $"x = {(-c / b):F4}";
                return;
            }

            double d = b * b - 4 * a * c;

            if (d > 0)
            {
                double x1 = (-b + Math.Sqrt(d)) / (2 * a);
                double x2 = (-b - Math.Sqrt(d)) / (2 * a);
                Result = $"Два корня:\nx₁ = {x1:F4}\nx₂ = {x2:F4}";
            }
            else if (Math.Abs(d) < 0.0001)
            {
                double x = -b / (2 * a);
                Result = $"Один корень:\nx = {x:F4}";
            }
            else
            {
                double real = -b / (2 * a);
                double imag = Math.Sqrt(-d) / (2 * a);
                Result = $"Комплексные корни:\nx₁ = {real:F4} + {imag:F4}i\nx₂ = {real:F4} - {imag:F4}i";
            }
        }
        catch
        {
            Result = "Ошибка! Введите корректные числа (используйте точку или запятую)";
        }
    }
}