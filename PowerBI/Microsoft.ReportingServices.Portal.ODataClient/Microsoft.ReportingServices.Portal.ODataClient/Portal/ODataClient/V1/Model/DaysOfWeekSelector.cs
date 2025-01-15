using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000EA RID: 234
	[OriginalName("DaysOfWeekSelector")]
	public class DaysOfWeekSelector : INotifyPropertyChanged
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x00014B6F File Offset: 0x00012D6F
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

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00014BAA File Offset: 0x00012DAA
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x00014BB2 File Offset: 0x00012DB2
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

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00014BC6 File Offset: 0x00012DC6
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x00014BCE File Offset: 0x00012DCE
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

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00014BE2 File Offset: 0x00012DE2
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x00014BEA File Offset: 0x00012DEA
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

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00014BFE File Offset: 0x00012DFE
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x00014C06 File Offset: 0x00012E06
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

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00014C1A File Offset: 0x00012E1A
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x00014C22 File Offset: 0x00012E22
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

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00014C36 File Offset: 0x00012E36
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x00014C3E File Offset: 0x00012E3E
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

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00014C52 File Offset: 0x00012E52
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x00014C5A File Offset: 0x00012E5A
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

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06000A6F RID: 2671 RVA: 0x00014C70 File Offset: 0x00012E70
		// (remove) Token: 0x06000A70 RID: 2672 RVA: 0x00014CA8 File Offset: 0x00012EA8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000A71 RID: 2673 RVA: 0x00014CDD File Offset: 0x00012EDD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040004C4 RID: 1220
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Sunday;

		// Token: 0x040004C5 RID: 1221
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Monday;

		// Token: 0x040004C6 RID: 1222
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Tuesday;

		// Token: 0x040004C7 RID: 1223
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Wednesday;

		// Token: 0x040004C8 RID: 1224
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Thursday;

		// Token: 0x040004C9 RID: 1225
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Friday;

		// Token: 0x040004CA RID: 1226
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _Saturday;
	}
}
