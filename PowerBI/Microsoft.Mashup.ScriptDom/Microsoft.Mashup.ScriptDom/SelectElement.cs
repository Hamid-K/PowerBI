using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003C8 RID: 968
	[Serializable]
	internal abstract class SelectElement : TSqlFragment
	{
		// Token: 0x06002F01 RID: 12033 RVA: 0x0016CF1D File Offset: 0x0016B11D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
