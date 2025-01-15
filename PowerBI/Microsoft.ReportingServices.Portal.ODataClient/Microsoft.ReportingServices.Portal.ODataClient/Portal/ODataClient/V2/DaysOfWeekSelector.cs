using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000066 RID: 102
	[OriginalName("DaysOfWeekSelector")]
	public class DaysOfWeekSelector : INotifyPropertyChanged
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x00009A06 File Offset: 0x00007C06
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static DaysOfWeekSelector CreateDaysOfWeekSelector(bool sunday, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday)
		{
			return new DaysOfWeekSelector
			{
				Sunday = sunday,
				Monday = monday,
				Tuesday = tuesday,
				Wednesday = wednesday,
				Thursday = thursday,
				Friday = friday,
				Saturday = saturday
			};
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00009A41 File Offset: 0x00007C41
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x00009A49 File Offset: 0x00007C49
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Sunday")]
		public bool Sunday
		{
			get
			{
				return this._Sunday;
			}
			set
			{
				this._Sunday = value;
				this.OnPropertyChanged("Sunday");
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00009A5D File Offset: 0x00007C5D
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00009A65 File Offset: 0x00007C65
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Monday")]
		public bool Monday
		{
			get
			{
				return this._Monday;
			}
			set
			{
				this._Monday = value;
				this.OnPropertyChanged("Monday");
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00009A79 File Offset: 0x00007C79
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x00009A81 File Offset: 0x00007C81
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Tuesday")]
		public bool Tuesday
		{
			get
			{
				return this._Tuesday;
			}
			set
			{
				this._Tuesday = value;
				this.OnPropertyChanged("Tuesday");
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00009A95 File Offset: 0x00007C95
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x00009A9D File Offset: 0x00007C9D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Wednesday")]
		public bool Wednesday
		{
			get
			{
				return this._Wednesday;
			}
			set
			{
				this._Wednesday = value;
				this.OnPropertyChanged("Wednesday");
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00009AB1 File Offset: 0x00007CB1
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x00009AB9 File Offset: 0x00007CB9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Thursday")]
		public bool Thursday
		{
			get
			{
				return this._Thursday;
			}
			set
			{
				this._Thursday = value;
				this.OnPropertyChanged("Thursday");
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00009ACD File Offset: 0x00007CCD
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x00009AD5 File Offset: 0x00007CD5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Friday")]
		public bool Friday
		{
			get
			{
				return this._Friday;
			}
			set
			{
				this._Friday = value;
				this.OnPropertyChanged("Friday");
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00009AE9 File Offset: 0x00007CE9
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00009AF1 File Offset: 0x00007CF1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Saturday")]
		public bool Saturday
		{
			get
			{
				return this._Saturday;
			}
			set
			{
				this._Saturday = value;
				this.OnPropertyChanged("Saturday");
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x0600048F RID: 1167 RVA: 0x00009B08 File Offset: 0x00007D08
		// (remove) Token: 0x06000490 RID: 1168 RVA: 0x00009B40 File Offset: 0x00007D40
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000491 RID: 1169 RVA: 0x00009B75 File Offset: 0x00007D75
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000218 RID: 536
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Sunday;

		// Token: 0x04000219 RID: 537
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Monday;

		// Token: 0x0400021A RID: 538
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Tuesday;

		// Token: 0x0400021B RID: 539
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Wednesday;

		// Token: 0x0400021C RID: 540
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Thursday;

		// Token: 0x0400021D RID: 541
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Friday;

		// Token: 0x0400021E RID: 542
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Saturday;
	}
}
