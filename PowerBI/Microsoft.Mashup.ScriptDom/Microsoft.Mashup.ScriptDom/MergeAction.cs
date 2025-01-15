using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000434 RID: 1076
	[Serializable]
	internal abstract class MergeAction : TSqlFragment
	{
		// Token: 0x06003174 RID: 12660 RVA: 0x0016F413 File Offset: 0x0016D613
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
