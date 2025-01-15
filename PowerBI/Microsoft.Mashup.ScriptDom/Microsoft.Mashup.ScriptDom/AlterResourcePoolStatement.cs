using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200045C RID: 1116
	[Serializable]
	internal class AlterResourcePoolStatement : ResourcePoolStatement
	{
		// Token: 0x0600323C RID: 12860 RVA: 0x0016FF82 File Offset: 0x0016E182
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x0016FF8E File Offset: 0x0016E18E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
