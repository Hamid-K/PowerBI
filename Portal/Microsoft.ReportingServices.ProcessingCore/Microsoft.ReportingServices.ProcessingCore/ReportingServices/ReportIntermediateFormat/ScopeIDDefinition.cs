using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003D1 RID: 977
	internal sealed class ScopeIDDefinition
	{
		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x000BAB0A File Offset: 0x000B8D0A
		// (set) Token: 0x0600277E RID: 10110 RVA: 0x000BAB12 File Offset: 0x000B8D12
		internal List<ScopeValueDefinition> ScopeValues
		{
			get
			{
				return this.m_scopeValues;
			}
			set
			{
				this.m_scopeValues = value;
			}
		}

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x000BAB1B File Offset: 0x000B8D1B
		// (set) Token: 0x06002780 RID: 10112 RVA: 0x000BAB23 File Offset: 0x000B8D23
		internal bool OmitScopeIdFromDataShapeResult
		{
			get
			{
				return this.m_omitScopeIdFromDataShapeResult;
			}
			set
			{
				this.m_omitScopeIdFromDataShapeResult = value;
			}
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000BAB2C File Offset: 0x000B8D2C
		internal void Initialize(string propertyName, InitializationContext context)
		{
			if (this.m_scopeValues != null)
			{
				for (int i = 0; i < this.m_scopeValues.Count; i++)
				{
					this.m_scopeValues[i].Initialize(propertyName, context);
				}
			}
		}

		// Token: 0x04001686 RID: 5766
		private List<ScopeValueDefinition> m_scopeValues;

		// Token: 0x04001687 RID: 5767
		private bool m_omitScopeIdFromDataShapeResult;
	}
}
