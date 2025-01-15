using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000378 RID: 888
	[Serializable]
	internal abstract class MessageTypeStatementBase : TSqlStatement
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x0016ACD2 File Offset: 0x00168ED2
		// (set) Token: 0x06002D06 RID: 11526 RVA: 0x0016ACDA File Offset: 0x00168EDA
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

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x0016ACEA File Offset: 0x00168EEA
		// (set) Token: 0x06002D08 RID: 11528 RVA: 0x0016ACF2 File Offset: 0x00168EF2
		public MessageValidationMethod ValidationMethod
		{
			get
			{
				return this._validationMethod;
			}
			set
			{
				this._validationMethod = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06002D09 RID: 11529 RVA: 0x0016ACFB File Offset: 0x00168EFB
		// (set) Token: 0x06002D0A RID: 11530 RVA: 0x0016AD03 File Offset: 0x00168F03
		public SchemaObjectName XmlSchemaCollectionName
		{
			get
			{
				return this._xmlSchemaCollectionName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._xmlSchemaCollectionName = value;
			}
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x0016AD13 File Offset: 0x00168F13
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.XmlSchemaCollectionName != null)
			{
				this.XmlSchemaCollectionName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D34 RID: 7476
		private Identifier _name;

		// Token: 0x04001D35 RID: 7477
		private MessageValidationMethod _validationMethod;

		// Token: 0x04001D36 RID: 7478
		private SchemaObjectName _xmlSchemaCollectionName;
	}
}
