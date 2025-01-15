using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x020005A7 RID: 1447
	[DataContract]
	internal sealed class ScopeValueDefinition
	{
		// Token: 0x06005216 RID: 21014 RVA: 0x0015A59B File Offset: 0x0015879B
		internal ScopeValueDefinition(Expression value)
		{
			this.m_value = value;
		}

		// Token: 0x17001E93 RID: 7827
		// (get) Token: 0x06005217 RID: 21015 RVA: 0x0015A5AA File Offset: 0x001587AA
		internal Expression Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04002978 RID: 10616
		[DataMember(Name = "Value", Order = 1)]
		private readonly Expression m_value;
	}
}
