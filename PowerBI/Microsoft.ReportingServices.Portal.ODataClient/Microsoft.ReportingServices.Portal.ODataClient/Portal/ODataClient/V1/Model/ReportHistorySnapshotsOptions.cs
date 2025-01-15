using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000C0 RID: 192
	[OriginalName("ReportHistorySnapshotsOptions")]
	public class ReportHistorySnapshotsOptions : INotifyPropertyChanged
	{
		// Token: 0x06000868 RID: 2152 RVA: 0x000111BD File Offset: 0x0000F3BD
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

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x000111E8 File Offset: 0x0000F3E8
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x000111F0 File Offset: 0x0000F3F0
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

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00011204 File Offset: 0x0000F404
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x0001120C File Offset: 0x0000F40C
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

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00011220 File Offset: 0x0000F420
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x00011228 File Offset: 0x0000F428
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

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0001123C File Offset: 0x0000F43C
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x00011244 File Offset: 0x0000F444
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

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00011258 File Offset: 0x0000F458
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x00011260 File Offset: 0x0000F460
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

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x00011274 File Offset: 0x0000F474
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x0001127C File Offset: 0x0000F47C
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

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06000875 RID: 2165 RVA: 0x00011290 File Offset: 0x0000F490
		// (remove) Token: 0x06000876 RID: 2166 RVA: 0x000112C8 File Offset: 0x0000F4C8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000877 RID: 2167 RVA: 0x000112FD File Offset: 0x0000F4FD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000404 RID: 1028
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _ManualCreationEnabled;

		// Token: 0x04000405 RID: 1029
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _KeepExecutionSnapshots;

		// Token: 0x04000406 RID: 1030
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _UseDefaultSystemLimit;

		// Token: 0x04000407 RID: 1031
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _ScopedLimit;

		// Token: 0x04000408 RID: 1032
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _SystemLimit;

		// Token: 0x04000409 RID: 1033
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleReference _Schedule;
	}
}
