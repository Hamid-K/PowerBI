using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005FF RID: 1535
	internal class TrailingSpaceStringComparer : IEqualityComparer<string>
	{
		// Token: 0x06004B1D RID: 19229 RVA: 0x00109D6C File Offset: 0x00107F6C
		private TrailingSpaceStringComparer()
		{
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x00109D74 File Offset: 0x00107F74
		public bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(TrailingSpaceStringComparer.NormalizeString(x), TrailingSpaceStringComparer.NormalizeString(y));
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x00109D8C File Offset: 0x00107F8C
		public int GetHashCode(string obj)
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(TrailingSpaceStringComparer.NormalizeString(obj));
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x00109D9E File Offset: 0x00107F9E
		internal static string NormalizeString(string value)
		{
			if (value == null || !value.EndsWith(" ", StringComparison.Ordinal))
			{
				return value;
			}
			return value.TrimEnd(new char[] { ' ' });
		}

		// Token: 0x04001A49 RID: 6729
		internal static readonly TrailingSpaceStringComparer Instance = new TrailingSpaceStringComparer();
	}
}
