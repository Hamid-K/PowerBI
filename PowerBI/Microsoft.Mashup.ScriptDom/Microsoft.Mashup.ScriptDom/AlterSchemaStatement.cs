using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000419 RID: 1049
	[Serializable]
	internal class AlterSchemaStatement : TSqlStatement
	{
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x0016EA51 File Offset: 0x0016CC51
		// (set) Token: 0x060030CC RID: 12492 RVA: 0x0016EA59 File Offset: 0x0016CC59
		public Identifier Name
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

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060030CD RID: 12493 RVA: 0x0016EA69 File Offset: 0x0016CC69
		// (set) Token: 0x060030CE RID: 12494 RVA: 0x0016EA71 File Offset: 0x0016CC71
		public SchemaObjectName ObjectName
		{
			get
			{
				return this._objectName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._objectName = value;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060030CF RID: 12495 RVA: 0x0016EA81 File Offset: 0x0016CC81
		// (set) Token: 0x060030D0 RID: 12496 RVA: 0x0016EA89 File Offset: 0x0016CC89
		public SecurityObjectKind ObjectKind
		{
			get
			{
				return this._objectKind;
			}
			set
			{
				this._objectKind = value;
			}
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x0016EA92 File Offset: 0x0016CC92
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x0016EA9E File Offset: 0x0016CC9E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.ObjectName != null)
			{
				this.ObjectName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E41 RID: 7745
		private Identifier _name;

		// Token: 0x04001E42 RID: 7746
		private SchemaObjectName _objectName;

		// Token: 0x04001E43 RID: 7747
		private SecurityObjectKind _objectKind;
	}
}
