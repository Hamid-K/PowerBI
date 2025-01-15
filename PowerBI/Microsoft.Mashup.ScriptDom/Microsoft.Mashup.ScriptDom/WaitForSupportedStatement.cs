using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000415 RID: 1045
	[Serializable]
	internal abstract class WaitForSupportedStatement : TSqlStatement
	{
		// Token: 0x060030AC RID: 12460 RVA: 0x0016E7E5 File Offset: 0x0016C9E5
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
