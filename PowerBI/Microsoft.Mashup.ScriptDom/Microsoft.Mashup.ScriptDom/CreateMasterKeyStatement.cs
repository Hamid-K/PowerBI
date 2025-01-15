using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002BC RID: 700
	[Serializable]
	internal class CreateMasterKeyStatement : MasterKeyStatement
	{
		// Token: 0x06002887 RID: 10375 RVA: 0x001663F0 File Offset: 0x001645F0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x001663FC File Offset: 0x001645FC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
