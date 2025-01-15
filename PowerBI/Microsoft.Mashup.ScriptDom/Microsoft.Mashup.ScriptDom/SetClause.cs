using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000259 RID: 601
	[Serializable]
	internal abstract class SetClause : TSqlFragment
	{
		// Token: 0x0600263C RID: 9788 RVA: 0x00163D46 File Offset: 0x00161F46
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
