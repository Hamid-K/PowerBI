using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000068 RID: 104
	[OriginalName("ReportHistorySnapshotsOptions")]
	public class ReportHistorySnapshotsOptions : INotifyPropertyChanged
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x00009DDD File Offset: 0x00007FDD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ReportHistorySnapshotsOptions CreateReportHistorySnapshotsOptions(bool manualCreationEnabled, bool keepExecutionSnapshots, bool useDefaultSystemLimit, int scopedLimit, int systemLimit)
		{
			return new ReportHistorySnapshotsOptions
			{
				ManualCreationEnabled = manualCreationEnabled,
				KeepExecutionSnapshots = keepExecutionSnapshots,
				UseDefaultSystemLimit = useDefaultSystemLimit,
				ScopedLimit = scopedLimit,
				SystemLimit = systemLimit
			};
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00009E08 File Offset: 0x00008008
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00009E10 File Offset: 0x00008010
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ManualCreationEnabled")]
		public bool ManualCreationEnabled
		{
			get
			{
				return this._ManualCreationEnabled;
			}
			set
			{
				this._ManualCreationEnabled = value;
				this.OnPropertyChanged("ManualCreationEnabled");
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00009E24 File Offset: 0x00008024
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00009E2C File Offset: 0x0000802C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("KeepExecutionSnapshots")]
		public bool KeepExecutionSnapshots
		{
			get
			{
				return this._KeepExecutionSnapshots;
			}
			set
			{
				this._KeepExecutionSnapshots = value;
				this.OnPropertyChanged("KeepExecutionSnapshots");
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00009E40 File Offset: 0x00008040
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00009E48 File Offset: 0x00008048
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("UseDefaultSystemLimit")]
		public bool UseDefaultSystemLimit
		{
			get
			{
				return this._UseDefaultSystemLimit;
			}
			set
			{
				this._UseDefaultSystemLimit = value;
				this.OnPropertyChanged("UseDefaultSystemLimit");
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00009E5C File Offset: 0x0000805C
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x00009E64 File Offset: 0x00008064
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ScopedLimit")]
		public int ScopedLimit
		{
			get
			{
				return this._ScopedLimit;
			}
			set
			{
				this._ScopedLimit = value;
				this.OnPropertyChanged("ScopedLimit");
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00009E78 File Offset: 0x00008078
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00009E80 File Offset: 0x00008080
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("SystemLimit")]
		public int SystemLimit
		{
			get
			{
				return this._SystemLimit;
			}
			set
			{
				this._SystemLimit = value;
				this.OnPropertyChanged("SystemLimit");
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00009E94 File Offset: 0x00008094
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x00009E9C File Offset: 0x0000809C
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

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060004BD RID: 1213 RVA: 0x00009EB0 File Offset: 0x000080B0
		// (remove) Token: 0x060004BE RID: 1214 RVA: 0x00009EE8 File Offset: 0x000080E8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060004BF RID: 1215 RVA: 0x00009F1D File Offset: 0x0000811D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400022D RID: 557
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ManualCreationEnabled;

		// Token: 0x0400022E RID: 558
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _KeepExecutionSnapshots;

		// Token: 0x0400022F RID: 559
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _UseDefaultSystemLimit;

		// Token: 0x04000230 RID: 560
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _ScopedLimit;

		// Token: 0x04000231 RID: 561
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _SystemLimit;

		// Token: 0x04000232 RID: 562
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleReference _Schedule;
	}
}
