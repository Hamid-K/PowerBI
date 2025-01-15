using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000078 RID: 120
	[OriginalName("KpiData")]
	public class KpiData : INotifyPropertyChanged
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0000AC1C File Offset: 0x00008E1C
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0000AC24 File Offset: 0x00008E24
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

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0000AC38 File Offset: 0x00008E38
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0000AC40 File Offset: 0x00008E40
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

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0000AC54 File Offset: 0x00008E54
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0000AC5C File Offset: 0x00008E5C
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

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0000AC70 File Offset: 0x00008E70
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0000AC78 File Offset: 0x00008E78
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

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600054C RID: 1356 RVA: 0x0000AC8C File Offset: 0x00008E8C
		// (remove) Token: 0x0600054D RID: 1357 RVA: 0x0000ACC4 File Offset: 0x00008EC4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600054E RID: 1358 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000266 RID: 614
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiDataItem _Value;

		// Token: 0x04000267 RID: 615
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiDataItem _Goal;

		// Token: 0x04000268 RID: 616
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiDataItem _Status;

		// Token: 0x04000269 RID: 617
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private KpiDataItem _TrendSet;
	}
}
