using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200036A RID: 874
	[Serializable]
	internal class AlterRemoteServiceBindingStatement : RemoteServiceBindingStatementBase
	{
		// Token: 0x06002CAD RID: 11437 RVA: 0x0016A6B7 File Offset: 0x001688B7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x0016A6C3 File Offset: 0x001688C3
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
