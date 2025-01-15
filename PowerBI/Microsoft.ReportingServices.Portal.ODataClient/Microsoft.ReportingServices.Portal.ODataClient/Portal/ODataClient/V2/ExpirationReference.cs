using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000069 RID: 105
	[OriginalName("ExpirationReference")]
	public class ExpirationReference : INotifyPropertyChanged
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x00009F39 File Offset: 0x00008139
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ExpirationReference CreateExpirationReference(int minutes)
		{
			return new ExpirationReference
			{
				Minutes = minutes
			};
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00009F47 File Offset: 0x00008147
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x00009F4F File Offset: 0x0000814F
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

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00009F63 File Offset: 0x00008163
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00009F6B File Offset: 0x0000816B
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

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060004C6 RID: 1222 RVA: 0x00009F80 File Offset: 0x00008180
		// (remove) Token: 0x060004C7 RID: 1223 RVA: 0x00009FB8 File Offset: 0x000081B8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060004C8 RID: 1224 RVA: 0x00009FED File Offset: 0x000081ED
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000234 RID: 564
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Minutes;

		// Token: 0x04000235 RID: 565
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleReference _Schedule;
	}
}
