using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000462 RID: 1122
	[Serializable]
	internal class CreateWorkloadGroupStatement : WorkloadGroupStatement
	{
		// Token: 0x06003257 RID: 12887 RVA: 0x00170106 File Offset: 0x0016E306
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x00170112 File Offset: 0x0016E312
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
