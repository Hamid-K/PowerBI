using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003F9 RID: 1017
	[Serializable]
	internal class PasswordCreateLoginSource : CreateLoginSource
	{
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x0016DEB4 File Offset: 0x0016C0B4
		// (set) Token: 0x06003010 RID: 12304 RVA: 0x0016DEBC File Offset: 0x0016C0BC
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

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06003011 RID: 12305 RVA: 0x0016DECC File Offset: 0x0016C0CC
		// (set) Token: 0x06003012 RID: 12306 RVA: 0x0016DED4 File Offset: 0x0016C0D4
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

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06003013 RID: 12307 RVA: 0x0016DEDD File Offset: 0x0016C0DD
		// (set) Token: 0x06003014 RID: 12308 RVA: 0x0016DEE5 File Offset: 0x0016C0E5
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

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06003015 RID: 12309 RVA: 0x0016DEEE File Offset: 0x0016C0EE
		public IList<PrincipalOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x0016DEF6 File Offset: 0x0016C0F6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x0016DF04 File Offset: 0x0016C104
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Password != null)
			{
				this.Password.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E0E RID: 7694
		private Literal _password;

		// Token: 0x04001E0F RID: 7695
		private bool _hashed;

		// Token: 0x04001E10 RID: 7696
		private bool _mustChange;

		// Token: 0x04001E11 RID: 7697
		private List<PrincipalOption> _options = new List<PrincipalOption>();
	}
}
