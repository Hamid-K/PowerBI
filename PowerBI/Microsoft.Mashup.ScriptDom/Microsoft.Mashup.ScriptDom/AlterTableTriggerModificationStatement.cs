using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000292 RID: 658
	[Serializable]
	internal class AlterTableTriggerModificationStatement : AlterTableStatement
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06002774 RID: 10100 RVA: 0x0016512E File Offset: 0x0016332E
		// (set) Token: 0x06002775 RID: 10101 RVA: 0x00165136 File Offset: 0x00163336
		public TriggerEnforcement TriggerEnforcement
		{
			get
			{
				return this._triggerEnforcement;
			}
			set
			{
				this._triggerEnforcement = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x0016513F File Offset: 0x0016333F
		// (set) Token: 0x06002777 RID: 10103 RVA: 0x00165147 File Offset: 0x00163347
		public bool All
		{
			get
			{
				return this._all;
			}
			set
			{
				this._all = value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06002778 RID: 10104 RVA: 0x00165150 File Offset: 0x00163350
		public IList<Identifier> TriggerNames
		{
			get
			{
				return this._triggerNames;
			}
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x00165158 File Offset: 0x00163358
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x00165164 File Offset: 0x00163364
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.SchemaObjectName != null)
			{
				base.SchemaObjectName.Accept(visitor);
			}
			int i = 0;
			int count = this.TriggerNames.Count;
			while (i < count)
			{
				this.TriggerNames[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001B98 RID: 7064
		private TriggerEnforcement _triggerEnforcement;

		// Token: 0x04001B99 RID: 7065
		private bool _all;

		// Token: 0x04001B9A RID: 7066
		private List<Identifier> _triggerNames = new List<Identifier>();
	}
}
