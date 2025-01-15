using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000295 RID: 661
	[Serializable]
	internal abstract class CreateTypeStatement : TSqlStatement
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x001652EA File Offset: 0x001634EA
		// (set) Token: 0x0600278E RID: 10126 RVA: 0x001652F2 File Offset: 0x001634F2
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

		// Token: 0x0600278F RID: 10127 RVA: 0x00165302 File Offset: 0x00163502
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BA1 RID: 7073
		private SchemaObjectName _name;
	}
}
