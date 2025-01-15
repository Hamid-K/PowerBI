using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002A6 RID: 678
	[Serializable]
	internal abstract class IndexStatement : TSqlStatement
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060027E5 RID: 10213 RVA: 0x001658BF File Offset: 0x00163ABF
		// (set) Token: 0x060027E6 RID: 10214 RVA: 0x001658C7 File Offset: 0x00163AC7
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

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060027E7 RID: 10215 RVA: 0x001658D7 File Offset: 0x00163AD7
		// (set) Token: 0x060027E8 RID: 10216 RVA: 0x001658DF File Offset: 0x00163ADF
		public SchemaObjectName OnName
		{
			get
			{
				return this._onName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._onName = value;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x001658EF File Offset: 0x00163AEF
		public IList<IndexOption> IndexOptions
		{
			get
			{
				return this._indexOptions;
			}
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x001658F8 File Offset: 0x00163AF8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.OnName != null)
			{
				this.OnName.Accept(visitor);
			}
			int i = 0;
			int count = this.IndexOptions.Count;
			while (i < count)
			{
				this.IndexOptions[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BB6 RID: 7094
		private Identifier _name;

		// Token: 0x04001BB7 RID: 7095
		private SchemaObjectName _onName;

		// Token: 0x04001BB8 RID: 7096
		private List<IndexOption> _indexOptions = new List<IndexOption>();
	}
}
