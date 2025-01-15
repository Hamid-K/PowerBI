using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000467 RID: 1127
	[Serializable]
	internal class CreateBrokerPriorityStatement : BrokerPriorityStatement
	{
		// Token: 0x0600326E RID: 12910 RVA: 0x00170250 File Offset: 0x0016E450
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x0017025C File Offset: 0x0016E45C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
