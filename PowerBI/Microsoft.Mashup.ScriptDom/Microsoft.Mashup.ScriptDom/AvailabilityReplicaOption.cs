using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000490 RID: 1168
	[Serializable]
	internal abstract class AvailabilityReplicaOption : TSqlFragment
	{
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x00171289 File Offset: 0x0016F489
		// (set) Token: 0x06003363 RID: 13155 RVA: 0x00171291 File Offset: 0x0016F491
		public AvailabilityReplicaOptionKind OptionKind
		{
			get
			{
				return this._optionKind;
			}
			set
			{
				this._optionKind = value;
			}
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x0017129A File Offset: 0x0016F49A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EF1 RID: 7921
		private AvailabilityReplicaOptionKind _optionKind;
	}
}
