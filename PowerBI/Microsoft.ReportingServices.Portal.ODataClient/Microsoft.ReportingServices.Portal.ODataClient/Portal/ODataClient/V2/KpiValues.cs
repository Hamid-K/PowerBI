using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000077 RID: 119
	[OriginalName("KpiValues")]
	public class KpiValues : INotifyPropertyChanged
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0000AB0D File Offset: 0x00008D0D
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x0000AB15 File Offset: 0x00008D15
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

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0000AB29 File Offset: 0x00008D29
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0000AB31 File Offset: 0x00008D31
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

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0000AB45 File Offset: 0x00008D45
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0000AB4D File Offset: 0x00008D4D
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

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0000AB61 File Offset: 0x00008D61
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0000AB69 File Offset: 0x00008D69
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

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000540 RID: 1344 RVA: 0x0000AB80 File Offset: 0x00008D80
		// (remove) Token: 0x06000541 RID: 1345 RVA: 0x0000ABB8 File Offset: 0x00008DB8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000542 RID: 1346 RVA: 0x0000ABED File Offset: 0x00008DED
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000261 RID: 609
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;

		// Token: 0x04000262 RID: 610
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private double? _Goal;

		// Token: 0x04000263 RID: 611
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private double? _Status;

		// Token: 0x04000264 RID: 612
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<double> _TrendSet = new ObservableCollection<double>();
	}
}
