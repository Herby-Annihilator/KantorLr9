using KantorLr9.Infrastructure.Commands;
using KantorLr9.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Markup;

namespace KantorLr9.ViewModels
{
	[MarkupExtensionReturnType(typeof(MainWindowViewModel))]
	public class MainWindowViewModel : ViewModel
	{
		#region Properties
		private string _title = "Title";
		public string Title { get => _title; set => Set(ref _title, value); }

		private string _status;
		public string Status { get => _status; set => Set(ref _status, value); }

		private string _left;
		public string Left { get => _left; set => Set(ref _left, value); }

		private string _right;
		public string Right { get => _right; set => Set(ref _right, value); }

		private string _step;
		public string Step { get => _step; set => Set(ref _step, value); }

		private string _segmentsCount;
		public string SegmentsCount { get => _segmentsCount; set => Set(ref _segmentsCount, value); }

		private string _precision;
		public string Precision { get => _precision; set => Set(ref _precision, value); }

		#endregion

		#region Commands

		#endregion
	}
}
