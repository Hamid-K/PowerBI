using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000053 RID: 83
	internal static class ExecutionOptions
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00005BEF File Offset: 0x00003DEF
		public static int Invalid
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000053DC File Offset: 0x000035DC
		public static int Live
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00010309 File Offset: 0x0000E509
		public static int Snapshot
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0001030C File Offset: 0x0000E50C
		public static int LiveOrSnapshotMask
		{
			get
			{
				return ExecutionOptions.Live | ExecutionOptions.Snapshot;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00010319 File Offset: 0x0000E519
		public static int KeepExecutionSnapshots
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0001031C File Offset: 0x0000E51C
		public static int DisableManualHistory
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001031F File Offset: 0x0000E51F
		public static int HistoryOnSchedule
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00010324 File Offset: 0x0000E524
		internal static int ExecutionSettingEnumToInt(ExecutionSettingEnum source)
		{
			int num = ExecutionOptions.Invalid;
			if (source == ExecutionSettingEnum.Live)
			{
				num |= ExecutionOptions.Live;
			}
			else
			{
				if (source != ExecutionSettingEnum.Snapshot)
				{
					throw new InternalCatalogException("Incorrect ExecutionSettingEnum in ExecutionOptions.Construct.");
				}
				num |= ExecutionOptions.Snapshot;
			}
			return num;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001035E File Offset: 0x0000E55E
		internal static ExecutionSettingEnum ToExecutionSettingEnum(int option)
		{
			if (ExecutionOptions.IsLiveExecution(option))
			{
				return ExecutionSettingEnum.Live;
			}
			if (ExecutionOptions.IsSnapshotExecution(option))
			{
				return ExecutionSettingEnum.Snapshot;
			}
			throw new InternalCatalogException("Unexpected execution option value");
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001037E File Offset: 0x0000E57E
		internal static bool IsLiveExecution(int options)
		{
			return (options & ExecutionOptions.Live) != 0;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001038A File Offset: 0x0000E58A
		internal static bool IsSnapshotExecution(int options)
		{
			return (options & ExecutionOptions.Snapshot) != 0;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00010396 File Offset: 0x0000E596
		internal static bool ExecutionSnapshotsKept(int options)
		{
			return (options & ExecutionOptions.KeepExecutionSnapshots) != 0;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000103A2 File Offset: 0x0000E5A2
		internal static bool IsManualHistoryEnabled(int options)
		{
			return (options & ExecutionOptions.DisableManualHistory) == 0;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000103AE File Offset: 0x0000E5AE
		internal static bool IsHistoryOnSchedule(int options)
		{
			return (options & ExecutionOptions.HistoryOnSchedule) != 0;
		}
	}
}
