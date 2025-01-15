using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004C1 RID: 1217
	public class MoreSpecificTypeComparer : IComparer<Type>
	{
		// Token: 0x06001B30 RID: 6960 RVA: 0x00051E5A File Offset: 0x0005005A
		public int Compare(Type x, Type y)
		{
			return y.GetInheritanceDepth() - x.GetInheritanceDepth();
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00051E69 File Offset: 0x00050069
		public static MoreSpecificTypeComparer Instance
		{
			get
			{
				return MoreSpecificTypeComparer._instance.Value;
			}
		}

		// Token: 0x04000D62 RID: 3426
		private static readonly Lazy<MoreSpecificTypeComparer> _instance = new Lazy<MoreSpecificTypeComparer>(() => new MoreSpecificTypeComparer());
	}
}
