using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000101 RID: 257
	[OriginalName("KpiValues")]
	public class KpiValues : INotifyPropertyChanged
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00016192 File Offset: 0x00014392
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x0001619A File Offset: 0x0001439A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Value")]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				this._Value = value;
				this.OnPropertyChanged("Value");
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x000161AE File Offset: 0x000143AE
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x000161B6 File Offset: 0x000143B6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Goal")]
		public double? Goal
		{
			get
			{
				return this._Goal;
			}
			set
			{
				this._Goal = value;
				this.OnPropertyChanged("Goal");
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x000161CA File Offset: 0x000143CA
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x000161D2 File Offset: 0x000143D2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Status")]
		public double? Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
				this.OnPropertyChanged("Status");
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x000161E6 File Offset: 0x000143E6
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x000161EE File Offset: 0x000143EE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("TrendSet")]
		public ObservableCollection<double> TrendSet
		{
			get
			{
				return this._TrendSet;
			}
			set
			{
				this._TrendSet = value;
				this.OnPropertyChanged("TrendSet");
			}
		}

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06000B3A RID: 2874 RVA: 0x00016204 File Offset: 0x00014404
		// (remove) Token: 0x06000B3B RID: 2875 RVA: 0x0001623C File Offset: 0x0001443C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B3C RID: 2876 RVA: 0x00016271 File Offset: 0x00014471
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400051F RID: 1311
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;

		// Token: 0x04000520 RID: 1312
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private double? _Goal;

		// Token: 0x04000521 RID: 1313
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private double? _Status;

		// Token: 0x04000522 RID: 1314
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<double> _TrendSet = new ObservableCollection<double>();
	}
}
