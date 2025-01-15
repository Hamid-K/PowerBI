using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002DC RID: 732
	[Serializable]
	internal class OpenCursorStatement : CursorStatement
	{
		// Token: 0x06002931 RID: 10545 RVA: 0x00166EFC File Offset: 0x001650FC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x00166F08 File Offset: 0x00165108
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
