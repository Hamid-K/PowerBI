using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000D3 RID: 211
	[OriginalName("MonthlyRecurrence")]
	public class MonthlyRecurrence : INotifyPropertyChanged
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x000130BD File Offset: 0x000112BD
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x000130C5 File Offset: 0x000112C5
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

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x000130D9 File Offset: 0x000112D9
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x000130E1 File Offset: 0x000112E1
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

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06000965 RID: 2405 RVA: 0x000130F8 File Offset: 0x000112F8
		// (remove) Token: 0x06000966 RID: 2406 RVA: 0x00013130 File Offset: 0x00011330
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000967 RID: 2407 RVA: 0x00013165 File Offset: 0x00011365
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400046A RID: 1130
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Days;

		// Token: 0x0400046B RID: 1131
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private MonthsOfYearSelector _MonthsOfYear;
	}
}
