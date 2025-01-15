using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000401 RID: 1025
	[Serializable]
	internal class PasswordAlterPrincipalOption : PrincipalOption
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x0016E1A7 File Offset: 0x0016C3A7
		// (set) Token: 0x06003040 RID: 12352 RVA: 0x0016E1AF File Offset: 0x0016C3AF
		public Literal Password
		{
			get
			{
				return this._password;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._password = value;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x0016E1BF File Offset: 0x0016C3BF
		// (set) Token: 0x06003042 RID: 12354 RVA: 0x0016E1C7 File Offset: 0x0016C3C7
		public Literal OldPassword
		{
			get
			{
				return this._oldPassword;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._oldPassword = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06003043 RID: 12355 RVA: 0x0016E1D7 File Offset: 0x0016C3D7
		// (set) Token: 0x06003044 RID: 12356 RVA: 0x0016E1DF File Offset: 0x0016C3DF
		public bool MustChange
		{
			get
			{
				return this._mustChange;
			}
			set
			{
				this._mustChange = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06003045 RID: 12357 RVA: 0x0016E1E8 File Offset: 0x0016C3E8
		// (set) Token: 0x06003046 RID: 12358 RVA: 0x0016E1F0 File Offset: 0x0016C3F0
		public bool Unlock
		{
			get
			{
				return this._unlock;
			}
			set
			{
				this._unlock = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06003047 RID: 12359 RVA: 0x0016E1F9 File Offset: 0x0016C3F9
		// (set) Token: 0x06003048 RID: 12360 RVA: 0x0016E201 File Offset: 0x0016C401
		public bool Hashed
		{
			get
			{
				return this._hashed;
			}
			set
			{
				this._hashed = value;
			}
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x0016E20A File Offset: 0x0016C40A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x0016E216 File Offset: 0x0016C416
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Password != null)
			{
				this.Password.Accept(visitor);
			}
			if (this.OldPassword != null)
			{
				this.OldPassword.Accept(visitor);
			}
		}

		// Token: 0x04001E1B RID: 7707
		private Literal _password;

		// Token: 0x04001E1C RID: 7708
		private Literal _oldPassword;

		// Token: 0x04001E1D RID: 7709
		private bool _mustChange;

		// Token: 0x04001E1E RID: 7710
		private bool _unlock;

		// Token: 0x04001E1F RID: 7711
		private bool _hashed;
	}
}
