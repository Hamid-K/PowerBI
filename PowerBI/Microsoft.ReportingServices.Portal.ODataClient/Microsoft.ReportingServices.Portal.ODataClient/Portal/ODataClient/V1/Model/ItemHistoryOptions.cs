using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000C3 RID: 195
	[OriginalName("ItemHistoryOptions")]
	public class ItemHistoryOptions : INotifyPropertyChanged
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x00011A30 File Offset: 0x0000FC30
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static ItemHistoryOptions CreateItemHistoryOptions(bool enableManualSnapshotCreation, bool keepExecutionSnapshots)
		{
			return new ItemHistoryOptions
			{
				EnableManualSnapshotCreation = enableManualSnapshotCreation,
				KeepExecutionSnapshots = keepExecutionSnapshots
			};
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00011A45 File Offset: 0x0000FC45
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x00011A4D File Offset: 0x0000FC4D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("EnableManualSnapshotCreation")]
		public bool EnableManualSnapshotCreation
		{
			get
			{
				return this._EnableManualSnapshotCreation;
			}
			set
			{
				this._EnableManualSnapshotCreation = value;
				this.OnPropertyChanged("EnableManualSnapshotCreation");
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00011A61 File Offset: 0x0000FC61
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x00011A69 File Offset: 0x0000FC69
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

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00011A7D File Offset: 0x0000FC7D
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x00011A85 File Offset: 0x0000FC85
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

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x060008A6 RID: 2214 RVA: 0x00011A9C File Offset: 0x0000FC9C
		// (remove) Token: 0x060008A7 RID: 2215 RVA: 0x00011AD4 File Offset: 0x0000FCD4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060008A8 RID: 2216 RVA: 0x00011B09 File Offset: 0x0000FD09
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400041E RID: 1054
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _EnableManualSnapshotCreation;

		// Token: 0x0400041F RID: 1055
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _KeepExecutionSnapshots;

		// Token: 0x04000420 RID: 1056
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleReference _Schedule;
	}
}
