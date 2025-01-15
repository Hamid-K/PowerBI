using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000309 RID: 777
	[Serializable]
	internal abstract class SetCommand : TSqlFragment
	{
		// Token: 0x06002A19 RID: 10777 RVA: 0x00167C37 File Offset: 0x00165E37
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
