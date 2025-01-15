using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200037A RID: 890
	[Serializable]
	internal class AlterMessageTypeStatement : MessageTypeStatementBase
	{
		// Token: 0x06002D12 RID: 11538 RVA: 0x0016ADB6 File Offset: 0x00168FB6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x0016ADC2 File Offset: 0x00168FC2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}
	}
}
