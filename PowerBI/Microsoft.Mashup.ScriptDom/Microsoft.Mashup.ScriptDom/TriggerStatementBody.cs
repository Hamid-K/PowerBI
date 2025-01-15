using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001BD RID: 445
	[Serializable]
	internal abstract class TriggerStatementBody : TSqlStatement
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x0015F60B File Offset: 0x0015D80B
		// (set) Token: 0x06002275 RID: 8821 RVA: 0x0015F613 File Offset: 0x0015D813
		public SchemaObjectName Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x0015F623 File Offset: 0x0015D823
		// (set) Token: 0x06002277 RID: 8823 RVA: 0x0015F62B File Offset: 0x0015D82B
		public TriggerObject TriggerObject
		{
			get
			{
				return this._triggerObject;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._triggerObject = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x0015F63B File Offset: 0x0015D83B
		public IList<TriggerOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x0015F643 File Offset: 0x0015D843
		// (set) Token: 0x0600227A RID: 8826 RVA: 0x0015F64B File Offset: 0x0015D84B
		public TriggerType TriggerType
		{
			get
			{
				return this._triggerType;
			}
			set
			{
				this._triggerType = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x0015F654 File Offset: 0x0015D854
		public IList<TriggerAction> TriggerActions
		{
			get
			{
				return this._triggerActions;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x0015F65C File Offset: 0x0015D85C
		// (set) Token: 0x0600227D RID: 8829 RVA: 0x0015F664 File Offset: 0x0015D864
		public bool WithAppend
		{
			get
			{
				return this._withAppend;
			}
			set
			{
				this._withAppend = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x0015F66D File Offset: 0x0015D86D
		// (set) Token: 0x0600227F RID: 8831 RVA: 0x0015F675 File Offset: 0x0015D875
		public bool IsNotForReplication
		{
			get
			{
				return this._isNotForReplication;
			}
			set
			{
				this._isNotForReplication = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x0015F67E File Offset: 0x0015D87E
		// (set) Token: 0x06002281 RID: 8833 RVA: 0x0015F686 File Offset: 0x0015D886
		public StatementList StatementList
		{
			get
			{
				return this._statementList;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._statementList = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x0015F696 File Offset: 0x0015D896
		// (set) Token: 0x06002283 RID: 8835 RVA: 0x0015F69E File Offset: 0x0015D89E
		public MethodSpecifier MethodSpecifier
		{
			get
			{
				return this._methodSpecifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._methodSpecifier = value;
			}
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x0015F6B0 File Offset: 0x0015D8B0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.TriggerObject != null)
			{
				this.TriggerObject.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.TriggerActions.Count;
			while (j < count2)
			{
				this.TriggerActions[j].Accept(visitor);
				j++;
			}
			if (this.StatementList != null)
			{
				this.StatementList.Accept(visitor);
			}
			if (this.MethodSpecifier != null)
			{
				this.MethodSpecifier.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A27 RID: 6695
		private SchemaObjectName _name;

		// Token: 0x04001A28 RID: 6696
		private TriggerObject _triggerObject;

		// Token: 0x04001A29 RID: 6697
		private List<TriggerOption> _options = new List<TriggerOption>();

		// Token: 0x04001A2A RID: 6698
		private TriggerType _triggerType;

		// Token: 0x04001A2B RID: 6699
		private List<TriggerAction> _triggerActions = new List<TriggerAction>();

		// Token: 0x04001A2C RID: 6700
		private bool _withAppend;

		// Token: 0x04001A2D RID: 6701
		private bool _isNotForReplication;

		// Token: 0x04001A2E RID: 6702
		private StatementList _statementList;

		// Token: 0x04001A2F RID: 6703
		private MethodSpecifier _methodSpecifier;
	}
}
