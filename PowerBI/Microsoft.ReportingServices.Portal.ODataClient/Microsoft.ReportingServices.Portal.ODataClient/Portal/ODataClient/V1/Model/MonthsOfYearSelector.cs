using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000EB RID: 235
	[OriginalName("MonthsOfYearSelector")]
	public class MonthsOfYearSelector : INotifyPropertyChanged
	{
		// Token: 0x06000A73 RID: 2675 RVA: 0x00014CFC File Offset: 0x00012EFC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static MonthsOfYearSelector CreateMonthsOfYearSelector(bool january, bool february, bool march, bool april, bool may, bool june, bool july, bool august, bool september, bool october, bool november, bool december)
		{
			return new MonthsOfYearSelector
			{
				January = january,
				February = february,
				March = march,
				April = april,
				May = may,
				June = june,
				July = july,
				August = august,
				September = september,
				October = october,
				November = november,
				December = december
			};
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00014D6A File Offset: 0x00012F6A
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x00014D72 File Offset: 0x00012F72
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("January")]
		public bool January
		{
			get
			{
				return this._January;
			}
			set
			{
				this._January = value;
				this.OnPropertyChanged("January");
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00014D86 File Offset: 0x00012F86
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x00014D8E File Offset: 0x00012F8E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("February")]
		public bool February
		{
			get
			{
				return this._February;
			}
			set
			{
				this._February = value;
				this.OnPropertyChanged("February");
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00014DA2 File Offset: 0x00012FA2
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x00014DAA File Offset: 0x00012FAA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("March")]
		public bool March
		{
			get
			{
				return this._March;
			}
			set
			{
				this._March = value;
				this.OnPropertyChanged("March");
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00014DBE File Offset: 0x00012FBE
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x00014DC6 File Offset: 0x00012FC6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("April")]
		public bool April
		{
			get
			{
				return this._April;
			}
			set
			{
				this._April = value;
				this.OnPropertyChanged("April");
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00014DDA File Offset: 0x00012FDA
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x00014DE2 File Offset: 0x00012FE2
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("May")]
		public bool May
		{
			get
			{
				return this._May;
			}
			set
			{
				this._May = value;
				this.OnPropertyChanged("May");
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00014DF6 File Offset: 0x00012FF6
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x00014DFE File Offset: 0x00012FFE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("June")]
		public bool June
		{
			get
			{
				return this._June;
			}
			set
			{
				this._June = value;
				this.OnPropertyChanged("June");
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00014E12 File Offset: 0x00013012
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x00014E1A File Offset: 0x0001301A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("July")]
		public bool July
		{
			get
			{
				return this._July;
			}
			set
			{
				this._July = value;
				this.OnPropertyChanged("July");
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00014E2E File Offset: 0x0001302E
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x00014E36 File Offset: 0x00013036
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("August")]
		public bool August
		{
			get
			{
				return this._August;
			}
			set
			{
				this._August = value;
				this.OnPropertyChanged("August");
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00014E4A File Offset: 0x0001304A
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x00014E52 File Offset: 0x00013052
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("September")]
		public bool September
		{
			get
			{
				return this._September;
			}
			set
			{
				this._September = value;
				this.OnPropertyChanged("September");
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00014E66 File Offset: 0x00013066
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x00014E6E File Offset: 0x0001306E
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("October")]
		public bool October
		{
			get
			{
				return this._October;
			}
			set
			{
				this._October = value;
				this.OnPropertyChanged("October");
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00014E82 File Offset: 0x00013082
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x00014E8A File Offset: 0x0001308A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("November")]
		public bool November
		{
			get
			{
				return this._November;
			}
			set
			{
				this._November = value;
				this.OnPropertyChanged("November");
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00014E9E File Offset: 0x0001309E
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x00014EA6 File Offset: 0x000130A6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("December")]
		public bool December
		{
			get
			{
				return this._December;
			}
			set
			{
				this._December = value;
				this.OnPropertyChanged("December");
			}
		}

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06000A8C RID: 2700 RVA: 0x00014EBC File Offset: 0x000130BC
		// (remove) Token: 0x06000A8D RID: 2701 RVA: 0x00014EF4 File Offset: 0x000130F4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000A8E RID: 2702 RVA: 0x00014F29 File Offset: 0x00013129
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040004CC RID: 1228
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _January;

		// Token: 0x040004CD RID: 1229
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _February;

		// Token: 0x040004CE RID: 1230
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _March;

		// Token: 0x040004CF RID: 1231
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _April;

		// Token: 0x040004D0 RID: 1232
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _May;

		// Token: 0x040004D1 RID: 1233
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _June;

		// Token: 0x040004D2 RID: 1234
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _July;

		// Token: 0x040004D3 RID: 1235
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _August;

		// Token: 0x040004D4 RID: 1236
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _September;

		// Token: 0x040004D5 RID: 1237
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _October;

		// Token: 0x040004D6 RID: 1238
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _November;

		// Token: 0x040004D7 RID: 1239
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _December;
	}
}
