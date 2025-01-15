using System;

namespace antlr
{
	// Token: 0x02000013 RID: 19
	internal class CommonHiddenStreamToken : CommonToken, IHiddenStreamToken, IToken
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00003CB3 File Offset: 0x00001EB3
		public CommonHiddenStreamToken()
		{
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003CBB File Offset: 0x00001EBB
		public CommonHiddenStreamToken(int t, string txt)
			: base(t, txt)
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003CC5 File Offset: 0x00001EC5
		public CommonHiddenStreamToken(string s)
			: base(s)
		{
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003CCE File Offset: 0x00001ECE
		public virtual IHiddenStreamToken getHiddenAfter()
		{
			return this.hiddenAfter;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003CD6 File Offset: 0x00001ED6
		public virtual IHiddenStreamToken getHiddenBefore()
		{
			return this.hiddenBefore;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003CDE File Offset: 0x00001EDE
		public virtual void setHiddenAfter(IHiddenStreamToken t)
		{
			this.hiddenAfter = t;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003CE7 File Offset: 0x00001EE7
		public virtual void setHiddenBefore(IHiddenStreamToken t)
		{
			this.hiddenBefore = t;
		}

		// Token: 0x04000044 RID: 68
		public new static readonly CommonHiddenStreamToken.CommonHiddenStreamTokenCreator Creator = new CommonHiddenStreamToken.CommonHiddenStreamTokenCreator();

		// Token: 0x04000045 RID: 69
		protected internal IHiddenStreamToken hiddenBefore;

		// Token: 0x04000046 RID: 70
		protected internal IHiddenStreamToken hiddenAfter;

		// Token: 0x02000014 RID: 20
		internal class CommonHiddenStreamTokenCreator : TokenCreator
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003D04 File Offset: 0x00001F04
			public override string TokenTypeName
			{
				get
				{
					return typeof(CommonHiddenStreamToken).FullName;
				}
			}

			// Token: 0x060000D1 RID: 209 RVA: 0x00003D15 File Offset: 0x00001F15
			public override IToken Create()
			{
				return new CommonHiddenStreamToken();
			}
		}
	}
}
