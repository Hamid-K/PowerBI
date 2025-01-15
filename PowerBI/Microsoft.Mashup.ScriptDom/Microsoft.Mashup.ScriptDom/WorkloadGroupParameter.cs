using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200045F RID: 1119
	[Serializable]
	internal abstract class WorkloadGroupParameter : TSqlFragment
	{
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06003249 RID: 12873 RVA: 0x0017006D File Offset: 0x0016E26D
		// (set) Token: 0x0600324A RID: 12874 RVA: 0x00170075 File Offset: 0x0016E275
		public WorkloadGroupParameterType ParameterType
		{
			get
			{
				return this._parameterType;
			}
			set
			{
				this._parameterType = value;
			}
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x0017007E File Offset: 0x0016E27E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EA3 RID: 7843
		private WorkloadGroupParameterType _parameterType;
	}
}
