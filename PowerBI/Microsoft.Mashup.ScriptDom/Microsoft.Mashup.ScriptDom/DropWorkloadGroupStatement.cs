using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000464 RID: 1124
	[Serializable]
	internal class DropWorkloadGroupStatement : DropUnownedObjectStatement
	{
		// Token: 0x0600325D RID: 12893 RVA: 0x00170140 File Offset: 0x0016E340
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x0017014C File Offset: 0x0016E34C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
