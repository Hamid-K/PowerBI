using System;
using System.Threading;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003A6 RID: 934
	internal sealed class DataSetValidator
	{
		// Token: 0x06002613 RID: 9747 RVA: 0x000B5D0D File Offset: 0x000B3F0D
		private DataSetValidator()
		{
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x000B5D18 File Offset: 0x000B3F18
		internal static bool ValidateCollation(string collation, out uint lcid)
		{
			lcid = DataSetValidator.LOCALE_SYSTEM_DEFAULT;
			if (collation == null)
			{
				return true;
			}
			object obj = RdlCollations.Collations[collation];
			if (obj == null)
			{
				return false;
			}
			lcid = (uint)obj;
			return true;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000B5D50 File Offset: 0x000B3F50
		internal static uint LCIDfromRDLCollation(string collation)
		{
			uint num;
			if (collation != null && DataSetValidator.ValidateCollation(collation, out num))
			{
				return num;
			}
			return (uint)Thread.CurrentThread.CurrentCulture.LCID;
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000B5D7C File Offset: 0x000B3F7C
		internal static uint GetSQLSortCompareMask(bool caseSensitivity, bool accentSensitivity, bool kanatypeSensitivity, bool widthSensitivity)
		{
			uint num = 0U;
			if (!caseSensitivity)
			{
				num |= 1U;
			}
			if (!accentSensitivity)
			{
				num |= 2U;
			}
			if (!kanatypeSensitivity)
			{
				num |= 65536U;
			}
			if (!widthSensitivity)
			{
				num |= 131072U;
			}
			return num;
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x000B5DB0 File Offset: 0x000B3FB0
		internal static void GetCompareFlagsfromAutoDetectedCollation(string autoCollation, out bool caseSensitivity, out bool accentSensitivity, out bool kanatypeSensitivity, out bool widthSensitivity)
		{
			caseSensitivity = false;
			accentSensitivity = false;
			kanatypeSensitivity = false;
			widthSensitivity = false;
			if (autoCollation != null)
			{
				caseSensitivity = 0 < autoCollation.IndexOf("_CS", StringComparison.Ordinal);
				accentSensitivity = 0 < autoCollation.IndexOf("_AS", StringComparison.Ordinal);
				kanatypeSensitivity = 0 < autoCollation.IndexOf("_KS", StringComparison.Ordinal);
				widthSensitivity = 0 < autoCollation.IndexOf("_WS", StringComparison.Ordinal);
			}
		}

		// Token: 0x04001630 RID: 5680
		internal static uint LOCALE_SYSTEM_DEFAULT = 2048U;
	}
}
