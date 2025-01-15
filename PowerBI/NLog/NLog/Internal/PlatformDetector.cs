using System;
using NLog.Common;
using NLog.Internal.FileAppenders;

namespace NLog.Internal
{
	// Token: 0x02000131 RID: 305
	internal static class PlatformDetector
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x000263A6 File Offset: 0x000245A6
		public static RuntimeOS CurrentOS
		{
			get
			{
				return PlatformDetector.currentOS;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x000263AD File Offset: 0x000245AD
		public static bool IsWin32
		{
			get
			{
				return PlatformDetector.currentOS == RuntimeOS.Windows || PlatformDetector.currentOS == RuntimeOS.WindowsNT;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x000263C1 File Offset: 0x000245C1
		public static bool IsUnix
		{
			get
			{
				return PlatformDetector.currentOS == RuntimeOS.Linux || PlatformDetector.currentOS == RuntimeOS.MacOSX;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x000263D8 File Offset: 0x000245D8
		public static bool IsMono
		{
			get
			{
				bool? isMono = PlatformDetector._isMono;
				if (isMono == null)
				{
					bool? flag = (PlatformDetector._isMono = new bool?(Type.GetType("Mono.Runtime") != null));
					return flag.Value;
				}
				return isMono.GetValueOrDefault();
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0002641F File Offset: 0x0002461F
		public static bool SupportsSharableMutex
		{
			get
			{
				return (!PlatformDetector.IsMono || Environment.Version.Major >= 4) && PlatformDetector.RunTimeSupportsSharableMutex;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0002643C File Offset: 0x0002463C
		private static bool RunTimeSupportsSharableMutex
		{
			get
			{
				if (PlatformDetector.runTimeSupportsSharableMutex != null)
				{
					return PlatformDetector.runTimeSupportsSharableMutex.Value;
				}
				try
				{
					BaseMutexFileAppender.ForceCreateSharableMutex("NLogMutexTester").Close();
					PlatformDetector.runTimeSupportsSharableMutex = new bool?(true);
				}
				catch (Exception ex)
				{
					InternalLogger.Debug(ex, "Failed to create sharable mutex processes");
					PlatformDetector.runTimeSupportsSharableMutex = new bool?(false);
				}
				return PlatformDetector.runTimeSupportsSharableMutex.Value;
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x000264B0 File Offset: 0x000246B0
		private static RuntimeOS GetCurrentRuntimeOS()
		{
			PlatformID platform = Environment.OSVersion.Platform;
			if (platform == PlatformID.Unix || platform == (PlatformID)128)
			{
				return RuntimeOS.Linux;
			}
			if (platform == PlatformID.Win32Windows)
			{
				return RuntimeOS.Windows;
			}
			if (platform == PlatformID.Win32NT)
			{
				return RuntimeOS.WindowsNT;
			}
			return RuntimeOS.Unknown;
		}

		// Token: 0x0400040E RID: 1038
		private static RuntimeOS currentOS = PlatformDetector.GetCurrentRuntimeOS();

		// Token: 0x0400040F RID: 1039
		private static bool? _isMono;

		// Token: 0x04000410 RID: 1040
		private static bool? runTimeSupportsSharableMutex;
	}
}
