using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002F2 RID: 754
	[Serializable]
	internal class DropFunctionStatement : DropObjectsStatement
	{
		// Token: 0x0600299A RID: 10650 RVA: 0x0016759A File Offset: 0x0016579A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x001675A6 File Offset: 0x001657A6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
