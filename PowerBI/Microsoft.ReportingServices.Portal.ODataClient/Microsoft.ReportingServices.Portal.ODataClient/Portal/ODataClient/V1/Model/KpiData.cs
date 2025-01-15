using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000102 RID: 258
	[OriginalName("KpiData")]
	public class KpiData : INotifyPropertyChanged
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x000162A0 File Offset: 0x000144A0
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x000162A8 File Offset: 0x000144A8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Value")]
		public KpiDataItem Value
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

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000162BC File Offset: 0x000144BC
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x000162C4 File Offset: 0x000144C4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Goal")]
		public KpiDataItem Goal
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

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x000162D8 File Offset: 0x000144D8
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x000162E0 File Offset: 0x000144E0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Status")]
		public KpiDataItem Status
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

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x000162F4 File Offset: 0x000144F4
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x000162FC File Offset: 0x000144FC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("TrendSet")]
		public KpiDataItem TrendSet
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

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x06000B46 RID: 2886 RVA: 0x00016310 File Offset: 0x00014510
		// (remove) Token: 0x06000B47 RID: 2887 RVA: 0x00016348 File Offset: 0x00014548
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B48 RID: 2888 RVA: 0x0001637D File Offset: 0x0001457D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000524 RID: 1316
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiDataItem _Value;

		// Token: 0x04000525 RID: 1317
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiDataItem _Goal;

		// Token: 0x04000526 RID: 1318
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiDataItem _Status;

		// Token: 0x04000527 RID: 1319
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiDataItem _TrendSet;
	}
}
