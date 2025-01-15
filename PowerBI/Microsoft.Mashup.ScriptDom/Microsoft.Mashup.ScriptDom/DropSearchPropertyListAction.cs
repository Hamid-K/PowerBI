using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003F5 RID: 1013
	[Serializable]
	internal class DropSearchPropertyListAction : SearchPropertyListAction
	{
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06002FFE RID: 12286 RVA: 0x0016DDC8 File Offset: 0x0016BFC8
		// (set) Token: 0x06002FFF RID: 12287 RVA: 0x0016DDD0 File Offset: 0x0016BFD0
		public StringLiteral PropertyName
		{
			get
			{
				return this._propertyName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._propertyName = value;
			}
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x0016DDE0 File Offset: 0x0016BFE0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x0016DDEC File Offset: 0x0016BFEC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.PropertyName != null)
			{
				this.PropertyName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E0B RID: 7691
		private StringLiteral _propertyName;
	}
}
