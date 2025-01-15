using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200048D RID: 1165
	[Serializable]
	internal class CreateAvailabilityGroupStatement : AvailabilityGroupStatement
	{
		// Token: 0x06003352 RID: 13138 RVA: 0x0017117E File Offset: 0x0016F37E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x0017118A File Offset: 0x0016F38A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
