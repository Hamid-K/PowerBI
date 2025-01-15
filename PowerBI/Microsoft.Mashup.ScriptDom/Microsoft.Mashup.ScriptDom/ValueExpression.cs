using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200005B RID: 91
	[Serializable]
	internal abstract class ValueExpression : PrimaryExpression
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00006602 File Offset: 0x00004802
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
