using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003B5 RID: 949
	[Serializable]
	internal abstract class GroupingSpecification : TSqlFragment
	{
		// Token: 0x06002E96 RID: 11926 RVA: 0x0016C6CD File Offset: 0x0016A8CD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
