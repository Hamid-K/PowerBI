using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000D0 RID: 208
	[OriginalName("MinuteRecurrence")]
	public class MinuteRecurrence : INotifyPropertyChanged
	{
		// Token: 0x06000948 RID: 2376 RVA: 0x00012E62 File Offset: 0x00011062
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static MinuteRecurrence CreateMinuteRecurrence(int minutesInterval)
		{
			return new MinuteRecurrence
			{
				MinutesInterval = minutesInterval
			};
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00012E70 File Offset: 0x00011070
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x00012E78 File Offset: 0x00011078
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("MinutesInterval")]
		public int MinutesInterval
		{
			get
			{
				return this._MinutesInterval;
			}
			set
			{
				this._MinutesInterval = value;
				this.OnPropertyChanged("MinutesInterval");
			}
		}

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x0600094B RID: 2379 RVA: 0x00012E8C File Offset: 0x0001108C
		// (remove) Token: 0x0600094C RID: 2380 RVA: 0x00012EC4 File Offset: 0x000110C4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600094D RID: 2381 RVA: 0x00012EF9 File Offset: 0x000110F9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000462 RID: 1122
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _MinutesInterval;
	}
}
