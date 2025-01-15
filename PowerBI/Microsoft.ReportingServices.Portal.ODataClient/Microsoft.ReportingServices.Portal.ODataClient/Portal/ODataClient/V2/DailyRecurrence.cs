using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000050 RID: 80
	[OriginalName("DailyRecurrence")]
	public class DailyRecurrence : INotifyPropertyChanged
	{
		// Token: 0x0600038C RID: 908 RVA: 0x000084F5 File Offset: 0x000066F5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DailyRecurrence CreateDailyRecurrence(int daysInterval)
		{
			return new DailyRecurrence
			{
				DaysInterval = daysInterval
			};
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00008503 File Offset: 0x00006703
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000850B File Offset: 0x0000670B
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DaysInterval")]
		public int DaysInterval
		{
			get
			{
				return this._DaysInterval;
			}
			set
			{
				this._DaysInterval = value;
				this.OnPropertyChanged("DaysInterval");
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600038F RID: 911 RVA: 0x00008520 File Offset: 0x00006720
		// (remove) Token: 0x06000390 RID: 912 RVA: 0x00008558 File Offset: 0x00006758
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000391 RID: 913 RVA: 0x0000858D File Offset: 0x0000678D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040001BE RID: 446
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _DaysInterval;
	}
}
