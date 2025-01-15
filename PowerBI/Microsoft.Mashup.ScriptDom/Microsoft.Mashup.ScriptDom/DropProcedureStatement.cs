using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002F1 RID: 753
	[Serializable]
	internal class DropProcedureStatement : DropObjectsStatement
	{
		// Token: 0x06002997 RID: 10647 RVA: 0x0016757D File Offset: 0x0016577D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x00167589 File Offset: 0x00165789
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
