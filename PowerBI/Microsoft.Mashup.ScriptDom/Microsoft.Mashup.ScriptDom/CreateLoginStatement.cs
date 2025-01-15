using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003F7 RID: 1015
	[Serializable]
	internal class CreateLoginStatement : TSqlStatement
	{
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06003006 RID: 12294 RVA: 0x0016DE2E File Offset: 0x0016C02E
		// (set) Token: 0x06003007 RID: 12295 RVA: 0x0016DE36 File Offset: 0x0016C036
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

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x0016DE46 File Offset: 0x0016C046
		// (set) Token: 0x06003009 RID: 12297 RVA: 0x0016DE4E File Offset: 0x0016C04E
		public CreateLoginSource Source
		{
			get
			{
				return this._source;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._source = value;
			}
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x0016DE5E File Offset: 0x0016C05E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x0016DE6A File Offset: 0x0016C06A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.Source != null)
			{
				this.Source.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E0C RID: 7692
		private Identifier _name;

		// Token: 0x04001E0D RID: 7693
		private CreateLoginSource _source;
	}
}
