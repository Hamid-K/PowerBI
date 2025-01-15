using System;
using System.Runtime.Serialization;

namespace NLog.Conditions
{
	// Token: 0x020001B0 RID: 432
	[Serializable]
	public class ConditionParseException : Exception
	{
		// Token: 0x0600132F RID: 4911 RVA: 0x00033F1C File Offset: 0x0003211C
		public ConditionParseException()
		{
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00033F24 File Offset: 0x00032124
		public ConditionParseException(string message)
			: base(message)
		{
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00033F2D File Offset: 0x0003212D
		public ConditionParseException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00033F37 File Offset: 0x00032137
		protected ConditionParseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
