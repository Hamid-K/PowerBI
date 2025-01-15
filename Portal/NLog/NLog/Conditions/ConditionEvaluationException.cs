using System;
using System.Runtime.Serialization;

namespace NLog.Conditions
{
	// Token: 0x020001A3 RID: 419
	[Serializable]
	public class ConditionEvaluationException : Exception
	{
		// Token: 0x060012F3 RID: 4851 RVA: 0x000338C7 File Offset: 0x00031AC7
		public ConditionEvaluationException()
		{
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000338CF File Offset: 0x00031ACF
		public ConditionEvaluationException(string message)
			: base(message)
		{
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000338D8 File Offset: 0x00031AD8
		public ConditionEvaluationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000338E2 File Offset: 0x00031AE2
		protected ConditionEvaluationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
