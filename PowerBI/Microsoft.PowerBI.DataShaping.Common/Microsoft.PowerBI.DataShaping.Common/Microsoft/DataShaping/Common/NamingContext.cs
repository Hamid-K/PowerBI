using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Common
{
	// Token: 0x02000016 RID: 22
	internal sealed class NamingContext
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00003AAD File Offset: 0x00001CAD
		internal NamingContext(StringComparer comparer = null)
		{
			this._usedNames = new HashSet<string>(comparer ?? StringComparer.Ordinal);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003ACA File Offset: 0x00001CCA
		public string GenerateUniqueName(string candidate)
		{
			return NamingContext.GenerateUniqueName(this._usedNames, candidate);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public static string GenerateUniqueName(HashSet<string> usedNames, string candidate)
		{
			while (!usedNames.Add(candidate))
			{
				candidate = StringUtil.IncrementDigitSuffix(candidate);
			}
			return candidate;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003AEE File Offset: 0x00001CEE
		public bool RegisterUniqueName(string name)
		{
			return this._usedNames.Add(name);
		}

		// Token: 0x04000044 RID: 68
		private readonly HashSet<string> _usedNames;
	}
}
