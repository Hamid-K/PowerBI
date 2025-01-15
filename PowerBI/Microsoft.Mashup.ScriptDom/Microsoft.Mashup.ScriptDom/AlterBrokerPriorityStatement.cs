using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000468 RID: 1128
	[Serializable]
	internal class AlterBrokerPriorityStatement : BrokerPriorityStatement
	{
		// Token: 0x06003271 RID: 12913 RVA: 0x0017026D File Offset: 0x0016E46D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x00170279 File Offset: 0x0016E479
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
