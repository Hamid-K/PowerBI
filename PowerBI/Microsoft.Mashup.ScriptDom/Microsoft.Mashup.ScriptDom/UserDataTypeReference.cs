using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001D6 RID: 470
	[Serializable]
	internal class UserDataTypeReference : ParameterizedDataTypeReference
	{
		// Token: 0x06002300 RID: 8960 RVA: 0x00160103 File Offset: 0x0015E303
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x0016010F File Offset: 0x0015E30F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
