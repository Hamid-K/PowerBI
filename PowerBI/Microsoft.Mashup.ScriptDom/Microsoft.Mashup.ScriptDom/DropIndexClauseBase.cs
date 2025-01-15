using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002EA RID: 746
	[Serializable]
	internal abstract class DropIndexClauseBase : TSqlFragment
	{
		// Token: 0x06002978 RID: 10616 RVA: 0x00167399 File Offset: 0x00165599
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
