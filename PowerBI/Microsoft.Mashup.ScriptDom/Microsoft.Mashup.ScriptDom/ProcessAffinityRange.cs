using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200048B RID: 1163
	[Serializable]
	internal class ProcessAffinityRange : LiteralRange
	{
		// Token: 0x06003348 RID: 13128 RVA: 0x00171059 File Offset: 0x0016F259
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x00171065 File Offset: 0x0016F265
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
