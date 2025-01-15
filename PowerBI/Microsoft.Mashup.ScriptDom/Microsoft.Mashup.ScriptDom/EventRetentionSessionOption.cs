using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200047A RID: 1146
	[Serializable]
	internal class EventRetentionSessionOption : SessionOption
	{
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x00170A63 File Offset: 0x0016EC63
		// (set) Token: 0x060032E5 RID: 13029 RVA: 0x00170A6B File Offset: 0x0016EC6B
		public EventSessionEventRetentionModeType Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x00170A74 File Offset: 0x0016EC74
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x00170A80 File Offset: 0x0016EC80
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001ECD RID: 7885
		private EventSessionEventRetentionModeType _value;
	}
}
