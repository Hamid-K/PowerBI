using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x02000101 RID: 257
	internal sealed class EvaluationContext
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x0002752B File Offset: 0x0002572B
		internal EvaluationContext(IEnumerable<ContextElement> elements)
		{
			this.m_elements = elements.ToReadOnlyCollection<ContextElement>();
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002753F File Offset: 0x0002573F
		public ReadOnlyCollection<ContextElement> Elements
		{
			get
			{
				return this.m_elements;
			}
		}

		// Token: 0x040004E6 RID: 1254
		private readonly ReadOnlyCollection<ContextElement> m_elements;
	}
}
