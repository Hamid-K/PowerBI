using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000463 RID: 1123
	[Serializable]
	internal class AlterWorkloadGroupStatement : WorkloadGroupStatement
	{
		// Token: 0x0600325A RID: 12890 RVA: 0x00170123 File Offset: 0x0016E323
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x0017012F File Offset: 0x0016E32F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
