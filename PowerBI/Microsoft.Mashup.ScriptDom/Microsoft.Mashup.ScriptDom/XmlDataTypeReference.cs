using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001D7 RID: 471
	[Serializable]
	internal class XmlDataTypeReference : DataTypeReference
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x00160120 File Offset: 0x0015E320
		// (set) Token: 0x06002304 RID: 8964 RVA: 0x00160128 File Offset: 0x0015E328
		public XmlDataTypeOption XmlDataTypeOption
		{
			get
			{
				return this._xmlDataTypeOption;
			}
			set
			{
				this._xmlDataTypeOption = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x00160131 File Offset: 0x0015E331
		// (set) Token: 0x06002306 RID: 8966 RVA: 0x00160139 File Offset: 0x0015E339
		public SchemaObjectName XmlSchemaCollection
		{
			get
			{
				return this._xmlSchemaCollection;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._xmlSchemaCollection = value;
			}
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x00160149 File Offset: 0x0015E349
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x00160155 File Offset: 0x0015E355
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.XmlSchemaCollection != null)
			{
				this.XmlSchemaCollection.Accept(visitor);
			}
		}

		// Token: 0x04001A50 RID: 6736
		private XmlDataTypeOption _xmlDataTypeOption;

		// Token: 0x04001A51 RID: 6737
		private SchemaObjectName _xmlSchemaCollection;
	}
}
