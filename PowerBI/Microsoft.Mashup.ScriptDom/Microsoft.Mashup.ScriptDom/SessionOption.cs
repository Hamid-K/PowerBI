using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000479 RID: 1145
	[Serializable]
	internal abstract class SessionOption : TSqlFragment
	{
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060032E0 RID: 13024 RVA: 0x00170A41 File Offset: 0x0016EC41
		// (set) Token: 0x060032E1 RID: 13025 RVA: 0x00170A49 File Offset: 0x0016EC49
		public SessionOptionKind OptionKind
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

		// Token: 0x060032E2 RID: 13026 RVA: 0x00170A52 File Offset: 0x0016EC52
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001ECC RID: 7884
		private SessionOptionKind _optionKind;
	}
}
