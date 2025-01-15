using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000340 RID: 832
	[Serializable]
	internal class CreateTableStatement : TSqlStatement, IFileStreamSpecifier
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06002B82 RID: 11138 RVA: 0x00169127 File Offset: 0x00167327
		// (set) Token: 0x06002B83 RID: 11139 RVA: 0x0016912F File Offset: 0x0016732F
		public SchemaObjectName SchemaObjectName
		{
			get
			{
				return this._schemaObjectName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._schemaObjectName = value;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06002B84 RID: 11140 RVA: 0x0016913F File Offset: 0x0016733F
		// (set) Token: 0x06002B85 RID: 11141 RVA: 0x00169147 File Offset: 0x00167347
		public bool AsFileTable
		{
			get
			{
				return this._asFileTable;
			}
			set
			{
				this._asFileTable = value;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06002B86 RID: 11142 RVA: 0x00169150 File Offset: 0x00167350
		// (set) Token: 0x06002B87 RID: 11143 RVA: 0x00169158 File Offset: 0x00167358
		public TableDefinition Definition
		{
			get
			{
				return this._definition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._definition = value;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x00169168 File Offset: 0x00167368
		// (set) Token: 0x06002B89 RID: 11145 RVA: 0x00169170 File Offset: 0x00167370
		public FileGroupOrPartitionScheme OnFileGroupOrPartitionScheme
		{
			get
			{
				return this._onFileGroupOrPartitionScheme;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._onFileGroupOrPartitionScheme = value;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x00169180 File Offset: 0x00167380
		// (set) Token: 0x06002B8B RID: 11147 RVA: 0x00169188 File Offset: 0x00167388
		public FederationScheme FederationScheme
		{
			get
			{
				return this._federationScheme;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._federationScheme = value;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06002B8C RID: 11148 RVA: 0x00169198 File Offset: 0x00167398
		// (set) Token: 0x06002B8D RID: 11149 RVA: 0x001691A0 File Offset: 0x001673A0
		public IdentifierOrValueExpression TextImageOn
		{
			get
			{
				return this._textImageOn;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._textImageOn = value;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x001691B0 File Offset: 0x001673B0
		public IList<TableOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x001691B8 File Offset: 0x001673B8
		// (set) Token: 0x06002B90 RID: 11152 RVA: 0x001691C0 File Offset: 0x001673C0
		public IdentifierOrValueExpression FileStreamOn
		{
			get
			{
				return this._fileStreamOn;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fileStreamOn = value;
			}
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x001691D0 File Offset: 0x001673D0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x001691DC File Offset: 0x001673DC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SchemaObjectName != null)
			{
				this.SchemaObjectName.Accept(visitor);
			}
			if (this.Definition != null)
			{
				this.Definition.Accept(visitor);
			}
			if (this.OnFileGroupOrPartitionScheme != null)
			{
				this.OnFileGroupOrPartitionScheme.Accept(visitor);
			}
			if (this.FederationScheme != null)
			{
				this.FederationScheme.Accept(visitor);
			}
			if (this.TextImageOn != null)
			{
				this.TextImageOn.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			if (this.FileStreamOn != null)
			{
				this.FileStreamOn.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CB6 RID: 7350
		private SchemaObjectName _schemaObjectName;

		// Token: 0x04001CB7 RID: 7351
		private bool _asFileTable;

		// Token: 0x04001CB8 RID: 7352
		private TableDefinition _definition;

		// Token: 0x04001CB9 RID: 7353
		private FileGroupOrPartitionScheme _onFileGroupOrPartitionScheme;

		// Token: 0x04001CBA RID: 7354
		private FederationScheme _federationScheme;

		// Token: 0x04001CBB RID: 7355
		private IdentifierOrValueExpression _textImageOn;

		// Token: 0x04001CBC RID: 7356
		private List<TableOption> _options = new List<TableOption>();

		// Token: 0x04001CBD RID: 7357
		private IdentifierOrValueExpression _fileStreamOn;
	}
}
