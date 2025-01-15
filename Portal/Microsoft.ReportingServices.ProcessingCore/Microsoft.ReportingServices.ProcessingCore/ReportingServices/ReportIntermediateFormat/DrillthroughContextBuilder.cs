using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003D3 RID: 979
	internal sealed class DrillthroughContextBuilder
	{
		// Token: 0x06002787 RID: 10119 RVA: 0x000BABA2 File Offset: 0x000B8DA2
		internal DrillthroughContextBuilder()
		{
			this.m_builder = new DrillthroughContextBuilder();
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000BABB5 File Offset: 0x000B8DB5
		internal string CreateDrillthroughContext(List<string> selectedFields, Dictionary<string, object> groupingValues)
		{
			if (selectedFields == null)
			{
				selectedFields = new List<string>();
			}
			if (groupingValues == null)
			{
				groupingValues = new Dictionary<string, object>();
			}
			return this.m_builder.CreateAsString(selectedFields, null, groupingValues);
		}

		// Token: 0x04001689 RID: 5769
		private readonly DrillthroughContextBuilder m_builder;
	}
}
