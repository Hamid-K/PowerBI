using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200004F RID: 79
	[OriginalName("MinuteRecurrence")]
	public class MinuteRecurrence : INotifyPropertyChanged
	{
		// Token: 0x06000385 RID: 901 RVA: 0x00008442 File Offset: 0x00006642
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static MinuteRecurrence CreateMinuteRecurrence(int minutesInterval)
		{
			return new MinuteRecurrence
			{
				MinutesInterval = minutesInterval
			};
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00008450 File Offset: 0x00006650
		// (set) Token: 0x06000387 RID: 903 RVA: 0x00008458 File Offset: 0x00006658
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

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000388 RID: 904 RVA: 0x0000846C File Offset: 0x0000666C
		// (remove) Token: 0x06000389 RID: 905 RVA: 0x000084A4 File Offset: 0x000066A4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600038A RID: 906 RVA: 0x000084D9 File Offset: 0x000066D9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040001BC RID: 444
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _MinutesInterval;
	}
}
