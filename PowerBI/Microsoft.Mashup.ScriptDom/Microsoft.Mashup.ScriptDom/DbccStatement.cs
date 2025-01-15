using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200035E RID: 862
	[Serializable]
	internal class DbccStatement : TSqlStatement
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06002C52 RID: 11346 RVA: 0x0016A06F File Offset: 0x0016826F
		// (set) Token: 0x06002C53 RID: 11347 RVA: 0x0016A077 File Offset: 0x00168277
		public string DllName
		{
			get
			{
				return this._dllName;
			}
			set
			{
				this._dllName = value;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x0016A080 File Offset: 0x00168280
		// (set) Token: 0x06002C55 RID: 11349 RVA: 0x0016A088 File Offset: 0x00168288
		public DbccCommand Command
		{
			get
			{
				return this._command;
			}
			set
			{
				this._command = value;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06002C56 RID: 11350 RVA: 0x0016A091 File Offset: 0x00168291
		// (set) Token: 0x06002C57 RID: 11351 RVA: 0x0016A099 File Offset: 0x00168299
		public bool ParenthesisRequired
		{
			get
			{
				return this._parenthesisRequired;
			}
			set
			{
				this._parenthesisRequired = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x0016A0A2 File Offset: 0x001682A2
		public IList<DbccNamedLiteral> Literals
		{
			get
			{
				return this._literals;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06002C59 RID: 11353 RVA: 0x0016A0AA File Offset: 0x001682AA
		public IList<DbccOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x0016A0B2 File Offset: 0x001682B2
		// (set) Token: 0x06002C5B RID: 11355 RVA: 0x0016A0BA File Offset: 0x001682BA
		public bool OptionsUseJoin
		{
			get
			{
				return this._optionsUseJoin;
			}
			set
			{
				this._optionsUseJoin = value;
			}
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x0016A0C3 File Offset: 0x001682C3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x0016A0D0 File Offset: 0x001682D0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Literals.Count;
			while (i < count)
			{
				this.Literals[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.Options.Count;
			while (j < count2)
			{
				this.Options[j].Accept(visitor);
				j++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CFB RID: 7419
		private string _dllName;

		// Token: 0x04001CFC RID: 7420
		private DbccCommand _command;

		// Token: 0x04001CFD RID: 7421
		private bool _parenthesisRequired;

		// Token: 0x04001CFE RID: 7422
		private List<DbccNamedLiteral> _literals = new List<DbccNamedLiteral>();

		// Token: 0x04001CFF RID: 7423
		private List<DbccOption> _options = new List<DbccOption>();

		// Token: 0x04001D00 RID: 7424
		private bool _optionsUseJoin;
	}
}
