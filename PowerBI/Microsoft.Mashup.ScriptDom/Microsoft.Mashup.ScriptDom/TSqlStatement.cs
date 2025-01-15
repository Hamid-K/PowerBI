using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001A5 RID: 421
	[Serializable]
	internal abstract class TSqlStatement : TSqlFragment
	{
		// Token: 0x060021E8 RID: 8680 RVA: 0x0015ED05 File Offset: 0x0015CF05
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
