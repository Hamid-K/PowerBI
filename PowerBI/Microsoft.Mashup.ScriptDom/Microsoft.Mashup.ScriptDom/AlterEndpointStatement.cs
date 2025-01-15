using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200037E RID: 894
	[Serializable]
	internal class AlterEndpointStatement : AlterCreateEndpointStatementBase
	{
		// Token: 0x06002D32 RID: 11570 RVA: 0x0016B0A9 File Offset: 0x001692A9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x0016B0B5 File Offset: 0x001692B5
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
