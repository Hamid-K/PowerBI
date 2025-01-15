using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200047B RID: 1147
	[Serializable]
	internal class MemoryPartitionSessionOption : SessionOption
	{
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060032E9 RID: 13033 RVA: 0x00170A91 File Offset: 0x0016EC91
		// (set) Token: 0x060032EA RID: 13034 RVA: 0x00170A99 File Offset: 0x0016EC99
		public EventSessionMemoryPartitionModeType Value
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

		// Token: 0x060032EB RID: 13035 RVA: 0x00170AA2 File Offset: 0x0016ECA2
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x00170AAE File Offset: 0x0016ECAE
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001ECE RID: 7886
		private EventSessionMemoryPartitionModeType _value;
	}
}
