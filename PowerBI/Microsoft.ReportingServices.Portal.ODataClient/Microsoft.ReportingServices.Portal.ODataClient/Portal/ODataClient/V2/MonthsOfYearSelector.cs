using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000067 RID: 103
	[OriginalName("MonthsOfYearSelector")]
	public class MonthsOfYearSelector : INotifyPropertyChanged
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x00009B94 File Offset: 0x00007D94
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

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00009C02 File Offset: 0x00007E02
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00009C0A File Offset: 0x00007E0A
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

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00009C1E File Offset: 0x00007E1E
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x00009C26 File Offset: 0x00007E26
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

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00009C3A File Offset: 0x00007E3A
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00009C42 File Offset: 0x00007E42
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

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00009C56 File Offset: 0x00007E56
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x00009C5E File Offset: 0x00007E5E
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

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00009C72 File Offset: 0x00007E72
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x00009C7A File Offset: 0x00007E7A
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

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00009C8E File Offset: 0x00007E8E
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00009C96 File Offset: 0x00007E96
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

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00009CAA File Offset: 0x00007EAA
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00009CB2 File Offset: 0x00007EB2
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

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00009CC6 File Offset: 0x00007EC6
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00009CCE File Offset: 0x00007ECE
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

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00009CE2 File Offset: 0x00007EE2
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00009CEA File Offset: 0x00007EEA
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

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00009CFE File Offset: 0x00007EFE
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x00009D06 File Offset: 0x00007F06
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

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00009D1A File Offset: 0x00007F1A
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00009D22 File Offset: 0x00007F22
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

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00009D36 File Offset: 0x00007F36
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x00009D3E File Offset: 0x00007F3E
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

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060004AC RID: 1196 RVA: 0x00009D54 File Offset: 0x00007F54
		// (remove) Token: 0x060004AD RID: 1197 RVA: 0x00009D8C File Offset: 0x00007F8C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060004AE RID: 1198 RVA: 0x00009DC1 File Offset: 0x00007FC1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000220 RID: 544
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _January;

		// Token: 0x04000221 RID: 545
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _February;

		// Token: 0x04000222 RID: 546
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _March;

		// Token: 0x04000223 RID: 547
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _April;

		// Token: 0x04000224 RID: 548
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _May;

		// Token: 0x04000225 RID: 549
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _June;

		// Token: 0x04000226 RID: 550
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _July;

		// Token: 0x04000227 RID: 551
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _August;

		// Token: 0x04000228 RID: 552
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _September;

		// Token: 0x04000229 RID: 553
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _October;

		// Token: 0x0400022A RID: 554
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _November;

		// Token: 0x0400022B RID: 555
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _December;
	}
}
