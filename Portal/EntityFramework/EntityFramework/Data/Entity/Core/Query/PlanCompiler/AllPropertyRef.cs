using System;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200032E RID: 814
	internal class AllPropertyRef : PropertyRef
	{
		// Token: 0x060026E8 RID: 9960 RVA: 0x0007039A File Offset: 0x0006E59A
		private AllPropertyRef()
		{
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000703A2 File Offset: 0x0006E5A2
		internal override PropertyRef CreateNestedPropertyRef(PropertyRef p)
		{
			return p;
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000703A5 File Offset: 0x0006E5A5
		public override string ToString()
		{
			return "ALL";
		}

		// Token: 0x04000D87 RID: 3463
		internal static AllPropertyRef Instance = new AllPropertyRef();
	}
}
