using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002A9 RID: 681
	[Serializable]
	internal class CreateXmlIndexStatement : IndexStatement
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x00165A8F File Offset: 0x00163C8F
		// (set) Token: 0x060027FD RID: 10237 RVA: 0x00165A97 File Offset: 0x00163C97
		public bool Primary
		{
			get
			{
				return this._primary;
			}
			set
			{
				this._primary = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x00165AA0 File Offset: 0x00163CA0
		// (set) Token: 0x060027FF RID: 10239 RVA: 0x00165AA8 File Offset: 0x00163CA8
		public Identifier XmlColumn
		{
			get
			{
				return this._xmlColumn;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._xmlColumn = value;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x00165AB8 File Offset: 0x00163CB8
		// (set) Token: 0x06002801 RID: 10241 RVA: 0x00165AC0 File Offset: 0x00163CC0
		public Identifier SecondaryXmlIndexName
		{
			get
			{
				return this._secondaryXmlIndexName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._secondaryXmlIndexName = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x00165AD0 File Offset: 0x00163CD0
		// (set) Token: 0x06002803 RID: 10243 RVA: 0x00165AD8 File Offset: 0x00163CD8
		public SecondaryXmlIndexType SecondaryXmlIndexType
		{
			get
			{
				return this._secondaryXmlIndexType;
			}
			set
			{
				this._secondaryXmlIndexType = value;
			}
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x00165AE1 File Offset: 0x00163CE1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x00165AF0 File Offset: 0x00163CF0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			if (base.OnName != null)
			{
				base.OnName.Accept(visitor);
			}
			if (this.XmlColumn != null)
			{
				this.XmlColumn.Accept(visitor);
			}
			if (this.SecondaryXmlIndexName != null)
			{
				this.SecondaryXmlIndexName.Accept(visitor);
			}
			int i = 0;
			int count = base.IndexOptions.Count;
			while (i < count)
			{
				base.IndexOptions[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001BBE RID: 7102
		private bool _primary;

		// Token: 0x04001BBF RID: 7103
		private Identifier _xmlColumn;

		// Token: 0x04001BC0 RID: 7104
		private Identifier _secondaryXmlIndexName;

		// Token: 0x04001BC1 RID: 7105
		private SecondaryXmlIndexType _secondaryXmlIndexType;
	}
}
