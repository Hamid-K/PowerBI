using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000205 RID: 517
	public static class ExtendedFunc
	{
		// Token: 0x06000DA9 RID: 3497 RVA: 0x000302AC File Offset: 0x0002E4AC
		[Pure]
		public static bool And<T>(T input, [InstantHandle] IEnumerable<Func<T, bool>> predicates)
		{
			bool flag = true;
			foreach (Func<T, bool> func in predicates)
			{
				flag &= func(input);
				if (!flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00030300 File Offset: 0x0002E500
		[Pure]
		public static bool Or<T>(T input, [InstantHandle] IEnumerable<Func<T, bool>> predicates)
		{
			bool flag = false;
			foreach (Func<T, bool> func in predicates)
			{
				flag |= func(input);
				if (flag)
				{
					break;
				}
			}
			return flag;
		}
	}
}
