using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003E8 RID: 1000
	internal sealed class SimpleRule : Rule
	{
		// Token: 0x06002F10 RID: 12048 RVA: 0x000955B8 File Offset: 0x000937B8
		internal SimpleRule(OpType opType, Rule.ProcessNodeDelegate processDelegate)
			: base(opType, processDelegate)
		{
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000955C2 File Offset: 0x000937C2
		internal override bool Match(Node node)
		{
			return node.Op.OpType == base.RuleOpType;
		}
	}
}
