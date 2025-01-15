using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200046F RID: 1135
	[Serializable]
	internal class AlterCryptographicProviderStatement : TSqlStatement
	{
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x0017052E File Offset: 0x0016E72E
		// (set) Token: 0x060032A1 RID: 12961 RVA: 0x00170536 File Offset: 0x0016E736
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

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060032A2 RID: 12962 RVA: 0x00170546 File Offset: 0x0016E746
		// (set) Token: 0x060032A3 RID: 12963 RVA: 0x0017054E File Offset: 0x0016E74E
		public EnableDisableOptionType Option
		{
			get
			{
				return this._option;
			}
			set
			{
				this._option = value;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x00170557 File Offset: 0x0016E757
		// (set) Token: 0x060032A5 RID: 12965 RVA: 0x0017055F File Offset: 0x0016E75F
		public Literal File
		{
			get
			{
				return this._file;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._file = value;
			}
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x0017056F File Offset: 0x0016E76F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x0017057B File Offset: 0x0016E77B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.File != null)
			{
				this.File.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EB8 RID: 7864
		private Identifier _name;

		// Token: 0x04001EB9 RID: 7865
		private EnableDisableOptionType _option;

		// Token: 0x04001EBA RID: 7866
		private Literal _file;
	}
}
