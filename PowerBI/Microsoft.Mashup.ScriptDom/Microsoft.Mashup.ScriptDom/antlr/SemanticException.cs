using System;
using System.Runtime.Serialization;

namespace antlr
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	internal class SemanticException : RecognitionException
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00004C95 File Offset: 0x00002E95
		public SemanticException(string s)
			: base(s)
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004C9E File Offset: 0x00002E9E
		public SemanticException(string s, string fileName, int line, int column)
			: base(s, fileName, line, column)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004CAB File Offset: 0x00002EAB
		protected SemanticException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
