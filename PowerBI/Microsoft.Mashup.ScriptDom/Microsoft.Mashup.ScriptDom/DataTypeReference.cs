using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001D3 RID: 467
	[Serializable]
	internal abstract class DataTypeReference : TSqlFragment
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x0016003F File Offset: 0x0015E23F
		// (set) Token: 0x060022F5 RID: 8949 RVA: 0x00160047 File Offset: 0x0015E247
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

		// Token: 0x060022F6 RID: 8950 RVA: 0x00160057 File Offset: 0x0015E257
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A4D RID: 6733
		private SchemaObjectName _name;
	}
}
