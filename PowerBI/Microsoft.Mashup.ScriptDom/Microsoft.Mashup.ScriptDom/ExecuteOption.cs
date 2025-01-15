using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001A7 RID: 423
	[Serializable]
	internal class ExecuteOption : TSqlFragment
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x0015EDA9 File Offset: 0x0015CFA9
		// (set) Token: 0x060021F1 RID: 8689 RVA: 0x0015EDB1 File Offset: 0x0015CFB1
		public ExecuteOptionKind OptionKind
		{
			get
			{
				return this._optionKind;
			}
			set
			{
				this._optionKind = value;
			}
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0015EDBA File Offset: 0x0015CFBA
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0015EDC6 File Offset: 0x0015CFC6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A02 RID: 6658
		private ExecuteOptionKind _optionKind;
	}
}
