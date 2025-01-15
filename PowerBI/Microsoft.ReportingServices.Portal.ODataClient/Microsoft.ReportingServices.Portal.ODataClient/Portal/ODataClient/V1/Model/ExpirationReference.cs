using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000116 RID: 278
	[OriginalName("ExpirationReference")]
	public class ExpirationReference : INotifyPropertyChanged
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x0001757D File Offset: 0x0001577D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ExpirationReference CreateExpirationReference(int minutes)
		{
			return new ExpirationReference
			{
				Minutes = minutes
			};
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0001758B File Offset: 0x0001578B
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x00017593 File Offset: 0x00015793
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Minutes")]
		public int Minutes
		{
			get
			{
				return this._Minutes;
			}
			set
			{
				this._Minutes = value;
				this.OnPropertyChanged("Minutes");
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x000175A7 File Offset: 0x000157A7
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x000175AF File Offset: 0x000157AF
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Schedule")]
		public ScheduleReference Schedule
		{
			get
			{
				return this._Schedule;
			}
			set
			{
				this._Schedule = value;
				this.OnPropertyChanged("Schedule");
			}
		}

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06000C12 RID: 3090 RVA: 0x000175C4 File Offset: 0x000157C4
		// (remove) Token: 0x06000C13 RID: 3091 RVA: 0x000175FC File Offset: 0x000157FC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000C14 RID: 3092 RVA: 0x00017631 File Offset: 0x00015831
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000576 RID: 1398
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Minutes;

		// Token: 0x04000577 RID: 1399
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleReference _Schedule;
	}
}
