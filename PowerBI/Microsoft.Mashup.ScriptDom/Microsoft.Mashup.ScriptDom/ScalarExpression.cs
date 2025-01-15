using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000058 RID: 88
	[Serializable]
	internal abstract class ScalarExpression : TSqlFragment
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x000065B4 File Offset: 0x000047B4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
