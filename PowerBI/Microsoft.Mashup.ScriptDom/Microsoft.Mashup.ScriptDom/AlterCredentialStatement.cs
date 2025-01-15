using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000377 RID: 887
	[Serializable]
	internal class AlterCredentialStatement : CredentialStatement
	{
		// Token: 0x06002D02 RID: 11522 RVA: 0x0016ACB5 File Offset: 0x00168EB5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x0016ACC1 File Offset: 0x00168EC1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
