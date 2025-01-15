using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000052 RID: 82
	[OriginalName("MonthlyRecurrence")]
	public class MonthlyRecurrence : INotifyPropertyChanged
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000869D File Offset: 0x0000689D
		// (set) Token: 0x0600039F RID: 927 RVA: 0x000086A5 File Offset: 0x000068A5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Days")]
		public string Days
		{
			get
			{
				return this._Days;
			}
			set
			{
				this._Days = value;
				this.OnPropertyChanged("Days");
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000086B9 File Offset: 0x000068B9
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x000086C1 File Offset: 0x000068C1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("MonthsOfYear")]
		public MonthsOfYearSelector MonthsOfYear
		{
			get
			{
				return this._MonthsOfYear;
			}
			set
			{
				this._MonthsOfYear = value;
				this.OnPropertyChanged("MonthsOfYear");
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060003A2 RID: 930 RVA: 0x000086D8 File Offset: 0x000068D8
		// (remove) Token: 0x060003A3 RID: 931 RVA: 0x00008710 File Offset: 0x00006910
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060003A4 RID: 932 RVA: 0x00008745 File Offset: 0x00006945
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040001C4 RID: 452
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Days;

		// Token: 0x040001C5 RID: 453
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MonthsOfYearSelector _MonthsOfYear;
	}
}
