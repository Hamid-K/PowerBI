using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BF0 RID: 7152
	public static class EvaluationConstantsExtensions
	{
		// Token: 0x0600B298 RID: 45720 RVA: 0x00245BB4 File Offset: 0x00243DB4
		public static IEvaluationConstants GetEvaluationConstants(this IEngineHost host)
		{
			if (host == null)
			{
				return null;
			}
			return host.QueryService<IEvaluationConstants>();
		}

		// Token: 0x0600B299 RID: 45721 RVA: 0x00245BC4 File Offset: 0x00243DC4
		public static Guid? GetActivityId(this IEvaluationConstants evaluationConstants)
		{
			if (evaluationConstants == null)
			{
				return null;
			}
			return new Guid?(evaluationConstants.ActivityId);
		}

		// Token: 0x0600B29A RID: 45722 RVA: 0x00245BE9 File Offset: 0x00243DE9
		public static string GetCorrelationId(this IEvaluationConstants evaluationConstants)
		{
			if (evaluationConstants == null)
			{
				return string.Empty;
			}
			return evaluationConstants.CorrelationId;
		}

		// Token: 0x0600B29B RID: 45723 RVA: 0x00245BFA File Offset: 0x00243DFA
		public static IEnumerable<EvaluationConstant> GetTracedConstants(this IEvaluationConstants evaluationConstants)
		{
			if (evaluationConstants == null)
			{
				return Enumerable.Empty<EvaluationConstant>();
			}
			return evaluationConstants.TracedConstants;
		}
	}
}
