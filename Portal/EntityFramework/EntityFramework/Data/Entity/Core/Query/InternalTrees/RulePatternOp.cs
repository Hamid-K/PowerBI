using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003DA RID: 986
	internal abstract class RulePatternOp : Op
	{
		// Token: 0x06002ED3 RID: 11987 RVA: 0x0009504F File Offset: 0x0009324F
		internal RulePatternOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002ED4 RID: 11988 RVA: 0x00095058 File Offset: 0x00093258
		internal override bool IsRulePatternOp
		{
			get
			{
				return true;
			}
		}
	}
}
