using KantorLr9.Infrastructure.Commands;
using KantorLr9.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Markup;
using KantorLr9.Model.Data;
using KantorLr9.Model.Methods;
using KantorLr9.Model.Methods.Base;

namespace KantorLr9.ViewModels
{
	[MarkupExtensionReturnType(typeof(MainWindowViewModel))]
	public class MainWindowViewModel : ViewModel
	{
		private TrapeziumMethod _trapeziumMethod;
		private SimpsonMethod _simpsonMethod;
		private MediumRectangleMethod _rectangleMethod;
		public MainWindowViewModel()
		{
			_trapeziumMethod = new TrapeziumMethod();
			_simpsonMethod = new SimpsonMethod();
			_rectangleMethod = new MediumRectangleMethod();
			ClearFixedTableCommand = new LambdaCommand(OnClearFixedTableCommandExecuted, CanClearFixedTableCommandExecute);
			ClearAutoTableCommand = new LambdaCommand(OnClearAutoTableCommandExecuted, CanClearAutoTableCommandExecute);
			CalculateFixedTableCommand = new LambdaCommand(OnCalculateFixedTableCommandExecuted, CanCalculateFixedTableCommandExecute);
			CalculateAutoStepTableCommand = new LambdaCommand(OnCalculateAutoStepTableCommandExecuted, CanCalculateAutoStepTableCommandExecute);
		}
		#region Properties
		private string _title = "Title";
		public string Title { get => _title; set => Set(ref _title, value); }

		private string _status = "Численное интегрирование";
		public string Status { get => _status; set => Set(ref _status, value); }

		private string _left;
		public string Left { get => _left; set => Set(ref _left, value); }

		private string _right;
		public string Right { get => _right; set => Set(ref _right, value); }

		private string _countOfSteps;
		public string CountOfSteps { get => _countOfSteps; set => Set(ref _countOfSteps, value); }

		private string _segmentsCount;
		public string SegmentsCount { get => _segmentsCount; set => Set(ref _segmentsCount, value); }

		private string _precision;
		public string Precision { get => _precision; set => Set(ref _precision, value); }

		public ObservableCollection<FixedStepTableRow> FixedStepTable { get; set; } = new ObservableCollection<FixedStepTableRow>();
		public ObservableCollection<AutoStepTableRow> AutoStepTable { get; set; } = new ObservableCollection<AutoStepTableRow>();
		#endregion

		#region Commands
		public ICommand ClearFixedTableCommand { get; }
		private void OnClearFixedTableCommandExecuted(object p)
		{
			try
			{
				FixedStepTable.Clear();
				Status = "Таблица очищена";
			}
			catch(Exception e)
			{
				Status = $"Операция не выполнена. Причина {e.Message}";
			}
		}
		private bool CanClearFixedTableCommandExecute(object p) => FixedStepTable.Count > 0;

		public ICommand ClearAutoTableCommand { get; }
		private void OnClearAutoTableCommandExecuted(object p)
		{
			try
			{
				AutoStepTable.Clear();
				Status = "Таблица очищена";
			}
			catch (Exception e)
			{
				Status = $"Операция не выполнена. Причина {e.Message}";
			}
		}
		private bool CanClearAutoTableCommandExecute(object p) => AutoStepTable.Count > 0;

		public ICommand CalculateFixedTableCommand { get; }
		private void OnCalculateFixedTableCommandExecuted(object p)
		{
			try
			{
				FixedStepTable.Clear();
				double left = Convert.ToDouble(Left);
				double right = Convert.ToDouble(Right);
				int count = Convert.ToInt32(CountOfSteps);
				double step = Math.Abs(right - left) / count;
				AddRowToFixedTable(_rectangleMethod, left, right, step);
				AddRowToFixedTable(_trapeziumMethod, left, right, step);
				AddRowToFixedTable(_simpsonMethod, left, right, step);
				Status = "Рассчет с фиксированным шагом произведен";
			}
			catch (Exception e)
			{
				Status = $"Операция не выполнена. Причина {e.Message}";
			}
		}
		private bool CanCalculateFixedTableCommandExecute(object p) => IsIntegrationLimitsIntroduced() && !string.IsNullOrWhiteSpace(CountOfSteps);

		public ICommand CalculateAutoStepTableCommand { get; }
		private void OnCalculateAutoStepTableCommandExecuted(object p)
		{
			try
			{
				AutoStepTable.Clear();
				double left = Convert.ToDouble(Left);
				double right = Convert.ToDouble(Right);
				int count = Convert.ToInt32(SegmentsCount);
				double precision = Convert.ToDouble(Precision);
				AddRowToAutoStepTable(_rectangleMethod, left, right, precision, count);
				AddRowToAutoStepTable(_trapeziumMethod, left, right, precision, count);
				AddRowToAutoStepTable(_simpsonMethod, left, right, precision, count);
				Status = "Рассчет с автоматическим шагом произведен";
			}
			catch (Exception e)
			{
				Status = $"Операция не выполнена. Причина {e.Message}";
			}
		}
		private bool CanCalculateAutoStepTableCommandExecute(object p) => IsIntegrationLimitsIntroduced() && !string.IsNullOrWhiteSpace(SegmentsCount) && !string.IsNullOrWhiteSpace(Precision);
		#endregion

		private bool IsIntegrationLimitsIntroduced()
		{
			return !(string.IsNullOrWhiteSpace(Left) || string.IsNullOrWhiteSpace(Right));
		}

		private void AddRowToFixedTable(NumericalIntegrationMethod method, double left, double right, double step)
		{
			var answer = method.GetSolutionWithFixedStep(Function, left, right, step);
			double realValue = IntegralOfFunction(left, right);
			FixedStepTableRow fixedStepTableRow = new FixedStepTableRow(method.ToString(), answer, realValue);
			FixedStepTable.Add(fixedStepTableRow);
		}

		private void AddRowToAutoStepTable(NumericalIntegrationMethod method, double left, double right, double precision, int segmetsCount)
		{
			var answer = method.GetSolutionWithAutoStep(Function, left, right, precision, segmetsCount);
			AutoStepTableRow row = new AutoStepTableRow(answer.MethodName, precision, answer.Value, answer.CountOfSteps);
			AutoStepTable.Add(row);
		}

		private double Function(double arg)
		{
			return Math.Sin(arg);
		}
		private double IntegralOfFunction(double left, double right)
		{
			return Math.Cos(left) - Math.Cos(right);
		}
	}
}
