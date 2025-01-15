using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200002C RID: 44
	internal static class LocalAppContextSwitches
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0000E274 File Offset: 0x0000C474
		public static bool SuppressInsecureTLSWarning
		{
			get
			{
				bool? flag = LocalAppContextSwitches.s_SuppressInsecureTLSWarning;
				if (flag == null)
				{
					bool flag2 = AppContext.TryGetSwitch("Switch.Microsoft.Data.SqlClient.SuppressInsecureTLSWarning", out flag2) && flag2;
					LocalAppContextSwitches.s_SuppressInsecureTLSWarning = new bool?(flag2);
				}
				return LocalAppContextSwitches.s_SuppressInsecureTLSWarning.Value;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		public static bool MakeReadAsyncBlocking
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContext.TryGetSwitch("Switch.Microsoft.Data.SqlClient.MakeReadAsyncBlocking", out LocalAppContextSwitches.s_makeReadAsyncBlocking) && LocalAppContextSwitches.s_makeReadAsyncBlocking;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0000E2D4 File Offset: 0x0000C4D4
		public static bool LegacyRowVersionNullBehavior
		{
			get
			{
				bool? flag = LocalAppContextSwitches.s_LegacyRowVersionNullBehavior;
				if (flag == null)
				{
					bool flag2 = AppContext.TryGetSwitch("Switch.Microsoft.Data.SqlClient.LegacyRowVersionNullBehavior", out flag2) && flag2;
					LocalAppContextSwitches.s_LegacyRowVersionNullBehavior = new bool?(flag2);
				}
				return LocalAppContextSwitches.s_LegacyRowVersionNullBehavior.Value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0000E318 File Offset: 0x0000C518
		public static bool UseMinimumLoginTimeout
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContext.TryGetSwitch("Switch.Microsoft.Data.SqlClient.UseOneSecFloorInTimeoutCalculationDuringLogin", out LocalAppContextSwitches._useMinimumLoginTimeout) && LocalAppContextSwitches._useMinimumLoginTimeout;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0000E332 File Offset: 0x0000C532
		public static bool DisableTNIRByDefault
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContext.TryGetSwitch("Switch.Microsoft.Data.SqlClient.DisableTNIRByDefaultInConnectionString", out LocalAppContextSwitches._disableTNIRByDefault) && LocalAppContextSwitches._disableTNIRByDefault;
			}
		}

		// Token: 0x0400009C RID: 156
		private const string TypeName = "LocalAppContextSwitches";

		// Token: 0x0400009D RID: 157
		internal const string MakeReadAsyncBlockingString = "Switch.Microsoft.Data.SqlClient.MakeReadAsyncBlocking";

		// Token: 0x0400009E RID: 158
		internal const string LegacyRowVersionNullString = "Switch.Microsoft.Data.SqlClient.LegacyRowVersionNullBehavior";

		// Token: 0x0400009F RID: 159
		internal const string SuppressInsecureTLSWarningString = "Switch.Microsoft.Data.SqlClient.SuppressInsecureTLSWarning";

		// Token: 0x040000A0 RID: 160
		private static bool s_makeReadAsyncBlocking;

		// Token: 0x040000A1 RID: 161
		private static bool? s_LegacyRowVersionNullBehavior;

		// Token: 0x040000A2 RID: 162
		private static bool? s_SuppressInsecureTLSWarning;

		// Token: 0x040000A3 RID: 163
		internal const string UseMinimumLoginTimeoutString = "Switch.Microsoft.Data.SqlClient.UseOneSecFloorInTimeoutCalculationDuringLogin";

		// Token: 0x040000A4 RID: 164
		private static bool _useMinimumLoginTimeout;

		// Token: 0x040000A5 RID: 165
		internal const string DisableTNIRByDefaultString = "Switch.Microsoft.Data.SqlClient.DisableTNIRByDefaultInConnectionString";

		// Token: 0x040000A6 RID: 166
		private static bool _disableTNIRByDefault;
	}
}
