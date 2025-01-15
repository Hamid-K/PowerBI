using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002A4 RID: 676
	[Serializable]
	internal class AlterQueueStatement : QueueStatement
	{
		// Token: 0x060027DD RID: 10205 RVA: 0x00165815 File Offset: 0x00163A15
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x00165821 File Offset: 0x00163A21
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
