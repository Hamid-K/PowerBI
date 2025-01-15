using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000D1 RID: 209
	[OriginalName("DailyRecurrence")]
	public class DailyRecurrence : INotifyPropertyChanged
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x00012F15 File Offset: 0x00011115
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DailyRecurrence CreateDailyRecurrence(int daysInterval)
		{
			return new DailyRecurrence
			{
				DaysInterval = daysInterval
			};
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00012F23 File Offset: 0x00011123
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x00012F2B File Offset: 0x0001112B
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

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06000952 RID: 2386 RVA: 0x00012F40 File Offset: 0x00011140
		// (remove) Token: 0x06000953 RID: 2387 RVA: 0x00012F78 File Offset: 0x00011178
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000954 RID: 2388 RVA: 0x00012FAD File Offset: 0x000111AD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000464 RID: 1124
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _DaysInterval;
	}
}
