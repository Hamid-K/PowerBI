using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000461 RID: 1121
	[Serializable]
	internal class WorkloadGroupImportanceParameter : WorkloadGroupParameter
	{
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x001700D8 File Offset: 0x0016E2D8
		// (set) Token: 0x06003253 RID: 12883 RVA: 0x001700E0 File Offset: 0x0016E2E0
		public ImportanceParameterType ParameterValue
		{
			get
			{
				return this._parameterValue;
			}
			set
			{
				this._parameterValue = value;
			}
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x001700E9 File Offset: 0x0016E2E9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x001700F5 File Offset: 0x0016E2F5
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EA5 RID: 7845
		private ImportanceParameterType _parameterValue;
	}
}
