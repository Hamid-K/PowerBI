using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200067F RID: 1663
	internal sealed class DrillthroughContextBuilder
	{
		// Token: 0x06005B24 RID: 23332 RVA: 0x001769A5 File Offset: 0x00174BA5
		internal DrillthroughContextBuilder()
		{
			this.m_builder = new DrillthroughContextBuilder();
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x001769B8 File Offset: 0x00174BB8
		internal string CreateDrillthroughContext(IActionOwner owner, ReportProcessing.IScope currentScope)
		{
			List<string> list;
			if (owner != null && owner.FieldsUsedInValueExpression != null)
			{
				list = owner.FieldsUsedInValueExpression;
			}
			else
			{
				list = new List<string>();
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.Ordinal);
			currentScope.GetGroupNameValuePairs(dictionary);
			return this.m_builder.CreateAsString(list, null, dictionary);
		}

		// Token: 0x04002F6E RID: 12142
		private readonly DrillthroughContextBuilder m_builder;
	}
}
