using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000036 RID: 54
	public interface IEvaluationConstants
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600012A RID: 298
		Guid ActivityId { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600012B RID: 299
		string CorrelationId { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600012C RID: 300
		IEnumerable<EvaluationConstant> TracedConstants { get; }
	}
}
