using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000388 RID: 904
	[Serializable]
	internal class SoapMethod : PayloadOption
	{
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x0016B3CE File Offset: 0x001695CE
		// (set) Token: 0x06002D6F RID: 11631 RVA: 0x0016B3D6 File Offset: 0x001695D6
		public Literal Alias
		{
			get
			{
				return this._alias;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._alias = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x0016B3E6 File Offset: 0x001695E6
		// (set) Token: 0x06002D71 RID: 11633 RVA: 0x0016B3EE File Offset: 0x001695EE
		public Literal Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._namespace = value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x0016B3FE File Offset: 0x001695FE
		// (set) Token: 0x06002D73 RID: 11635 RVA: 0x0016B406 File Offset: 0x00169606
		public SoapMethodAction Action
		{
			get
			{
				return this._action;
			}
			set
			{
				this._action = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x0016B40F File Offset: 0x0016960F
		// (set) Token: 0x06002D75 RID: 11637 RVA: 0x0016B417 File Offset: 0x00169617
		public Literal Name
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

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06002D76 RID: 11638 RVA: 0x0016B427 File Offset: 0x00169627
		// (set) Token: 0x06002D77 RID: 11639 RVA: 0x0016B42F File Offset: 0x0016962F
		public SoapMethodFormat Format
		{
			get
			{
				return this._format;
			}
			set
			{
				this._format = value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x0016B438 File Offset: 0x00169638
		// (set) Token: 0x06002D79 RID: 11641 RVA: 0x0016B440 File Offset: 0x00169640
		public SoapMethodSchemas Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				this._schema = value;
			}
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x0016B449 File Offset: 0x00169649
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x0016B458 File Offset: 0x00169658
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Alias != null)
			{
				this.Alias.Accept(visitor);
			}
			if (this.Namespace != null)
			{
				this.Namespace.Accept(visitor);
			}
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
		}

		// Token: 0x04001D54 RID: 7508
		private Literal _alias;

		// Token: 0x04001D55 RID: 7509
		private Literal _namespace;

		// Token: 0x04001D56 RID: 7510
		private SoapMethodAction _action;

		// Token: 0x04001D57 RID: 7511
		private Literal _name;

		// Token: 0x04001D58 RID: 7512
		private SoapMethodFormat _format;

		// Token: 0x04001D59 RID: 7513
		private SoapMethodSchemas _schema;
	}
}
