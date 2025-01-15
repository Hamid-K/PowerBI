using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000482 RID: 1154
	[Serializable]
	internal class CreateSpatialIndexStatement : TSqlStatement
	{
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06003312 RID: 13074 RVA: 0x00170CB1 File Offset: 0x0016EEB1
		// (set) Token: 0x06003313 RID: 13075 RVA: 0x00170CB9 File Offset: 0x0016EEB9
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

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x00170CC9 File Offset: 0x0016EEC9
		// (set) Token: 0x06003315 RID: 13077 RVA: 0x00170CD1 File Offset: 0x0016EED1
		public SchemaObjectName Object
		{
			get
			{
				return this._object;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._object = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x00170CE1 File Offset: 0x0016EEE1
		// (set) Token: 0x06003317 RID: 13079 RVA: 0x00170CE9 File Offset: 0x0016EEE9
		public Identifier SpatialColumnName
		{
			get
			{
				return this._spatialColumnName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._spatialColumnName = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x00170CF9 File Offset: 0x0016EEF9
		// (set) Token: 0x06003319 RID: 13081 RVA: 0x00170D01 File Offset: 0x0016EF01
		public SpatialIndexingSchemeType SpatialIndexingScheme
		{
			get
			{
				return this._spatialIndexingScheme;
			}
			set
			{
				this._spatialIndexingScheme = value;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x00170D0A File Offset: 0x0016EF0A
		public IList<SpatialIndexOption> SpatialIndexOptions
		{
			get
			{
				return this._spatialIndexOptions;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x00170D12 File Offset: 0x0016EF12
		// (set) Token: 0x0600331C RID: 13084 RVA: 0x00170D1A File Offset: 0x0016EF1A
		public IdentifierOrValueExpression OnFileGroup
		{
			get
			{
				return this._onFileGroup;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._onFileGroup = value;
			}
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x00170D2A File Offset: 0x0016EF2A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x00170D38 File Offset: 0x0016EF38
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.Object != null)
			{
				this.Object.Accept(visitor);
			}
			if (this.SpatialColumnName != null)
			{
				this.SpatialColumnName.Accept(visitor);
			}
			int i = 0;
			int count = this.SpatialIndexOptions.Count;
			while (i < count)
			{
				this.SpatialIndexOptions[i].Accept(visitor);
				i++;
			}
			if (this.OnFileGroup != null)
			{
				this.OnFileGroup.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001ED9 RID: 7897
		private Identifier _name;

		// Token: 0x04001EDA RID: 7898
		private SchemaObjectName _object;

		// Token: 0x04001EDB RID: 7899
		private Identifier _spatialColumnName;

		// Token: 0x04001EDC RID: 7900
		private SpatialIndexingSchemeType _spatialIndexingScheme;

		// Token: 0x04001EDD RID: 7901
		private List<SpatialIndexOption> _spatialIndexOptions = new List<SpatialIndexOption>();

		// Token: 0x04001EDE RID: 7902
		private IdentifierOrValueExpression _onFileGroup;
	}
}
