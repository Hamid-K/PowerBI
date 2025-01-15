using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000496 RID: 1174
	[Serializable]
	internal abstract class AvailabilityGroupOption : TSqlFragment
	{
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x001713AC File Offset: 0x0016F5AC
		// (set) Token: 0x06003380 RID: 13184 RVA: 0x001713B4 File Offset: 0x0016F5B4
		public AvailabilityGroupOptionKind OptionKind
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

		// Token: 0x06003381 RID: 13185 RVA: 0x001713BD File Offset: 0x0016F5BD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EF7 RID: 7927
		private AvailabilityGroupOptionKind _optionKind;
	}
}
