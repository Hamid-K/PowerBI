using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003F4 RID: 1012
	[Serializable]
	internal class AddSearchPropertyListAction : SearchPropertyListAction
	{
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06002FF3 RID: 12275 RVA: 0x0016DCEE File Offset: 0x0016BEEE
		// (set) Token: 0x06002FF4 RID: 12276 RVA: 0x0016DCF6 File Offset: 0x0016BEF6
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

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06002FF5 RID: 12277 RVA: 0x0016DD06 File Offset: 0x0016BF06
		// (set) Token: 0x06002FF6 RID: 12278 RVA: 0x0016DD0E File Offset: 0x0016BF0E
		public StringLiteral Guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._guid = value;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06002FF7 RID: 12279 RVA: 0x0016DD1E File Offset: 0x0016BF1E
		// (set) Token: 0x06002FF8 RID: 12280 RVA: 0x0016DD26 File Offset: 0x0016BF26
		public IntegerLiteral Id
		{
			get
			{
				return this._id;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._id = value;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06002FF9 RID: 12281 RVA: 0x0016DD36 File Offset: 0x0016BF36
		// (set) Token: 0x06002FFA RID: 12282 RVA: 0x0016DD3E File Offset: 0x0016BF3E
		public StringLiteral Description
		{
			get
			{
				return this._description;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._description = value;
			}
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x0016DD4E File Offset: 0x0016BF4E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x0016DD5C File Offset: 0x0016BF5C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.PropertyName != null)
			{
				this.PropertyName.Accept(visitor);
			}
			if (this.Guid != null)
			{
				this.Guid.Accept(visitor);
			}
			if (this.Id != null)
			{
				this.Id.Accept(visitor);
			}
			if (this.Description != null)
			{
				this.Description.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E07 RID: 7687
		private StringLiteral _propertyName;

		// Token: 0x04001E08 RID: 7688
		private StringLiteral _guid;

		// Token: 0x04001E09 RID: 7689
		private IntegerLiteral _id;

		// Token: 0x04001E0A RID: 7690
		private StringLiteral _description;
	}
}
