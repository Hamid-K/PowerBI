using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000236 RID: 566
	[Serializable]
	internal class ExpressionParserInvalidArrayTypeException : ExpressionParserException
	{
		// Token: 0x0600130D RID: 4877 RVA: 0x0002C47A File Offset: 0x0002A67A
		public ExpressionParserInvalidArrayTypeException(string typeName, string messageId, string method, int startPosition, int endPosition)
			: base(messageId, method, startPosition, endPosition)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x0002C48F File Offset: 0x0002A68F
		internal string TypeName
		{
			get
			{
				return this.m_typeName;
			}
		}

		// Token: 0x04000608 RID: 1544
		private readonly string m_typeName;
	}
}
