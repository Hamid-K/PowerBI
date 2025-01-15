using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Common
{
	// Token: 0x0200001A RID: 26
	internal class UniqueStringGenerator
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00003F7B File Offset: 0x0000217B
		internal UniqueStringGenerator(IEqualityComparer<string> comparer)
		{
			this.usedStrings = new HashSet<string>(comparer);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003F8F File Offset: 0x0000218F
		internal UniqueStringGenerator(IEnumerable<string> stringsToRegister, IEqualityComparer<string> comparer)
		{
			this.usedStrings = new HashSet<string>(stringsToRegister, comparer);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003FA4 File Offset: 0x000021A4
		internal bool RegisterString(string str)
		{
			return this.usedStrings.Add(str);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003FB2 File Offset: 0x000021B2
		internal string MakeUniqueString(string candidate)
		{
			return StringUtil.MakeUniqueId(candidate, this.usedStrings);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003FC0 File Offset: 0x000021C0
		internal static string MakeUniqueString(string name, IEnumerable<string> previouslyUsedNames, IEqualityComparer<string> comparer)
		{
			return new UniqueStringGenerator(previouslyUsedNames, comparer).MakeUniqueString(name);
		}

		// Token: 0x04000048 RID: 72
		private readonly HashSet<string> usedStrings;
	}
}
