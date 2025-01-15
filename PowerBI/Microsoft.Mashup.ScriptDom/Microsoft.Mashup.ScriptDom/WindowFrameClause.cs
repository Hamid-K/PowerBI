using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020004A3 RID: 1187
	[Serializable]
	internal class WindowFrameClause : TSqlFragment
	{
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060033D7 RID: 13271 RVA: 0x00171976 File Offset: 0x0016FB76
		// (set) Token: 0x060033D8 RID: 13272 RVA: 0x0017197E File Offset: 0x0016FB7E
		public WindowDelimiter Top
		{
			get
			{
				return this._top;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._top = value;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060033D9 RID: 13273 RVA: 0x0017198E File Offset: 0x0016FB8E
		// (set) Token: 0x060033DA RID: 13274 RVA: 0x00171996 File Offset: 0x0016FB96
		public WindowDelimiter Bottom
		{
			get
			{
				return this._bottom;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._bottom = value;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x001719A6 File Offset: 0x0016FBA6
		// (set) Token: 0x060033DC RID: 13276 RVA: 0x001719AE File Offset: 0x0016FBAE
		public WindowFrameType WindowFrameType
		{
			get
			{
				return this._windowFrameType;
			}
			set
			{
				this._windowFrameType = value;
			}
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x001719B7 File Offset: 0x0016FBB7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x001719C3 File Offset: 0x0016FBC3
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Top != null)
			{
				this.Top.Accept(visitor);
			}
			if (this.Bottom != null)
			{
				this.Bottom.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001F12 RID: 7954
		private WindowDelimiter _top;

		// Token: 0x04001F13 RID: 7955
		private WindowDelimiter _bottom;

		// Token: 0x04001F14 RID: 7956
		private WindowFrameType _windowFrameType;
	}
}
