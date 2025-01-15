using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000232 RID: 562
	[Serializable]
	internal class ExpressionParserInvalidTypeNameException : ExpressionParserException
	{
		// Token: 0x06001305 RID: 4869 RVA: 0x0002C404 File Offset: 0x0002A604
		public ExpressionParserInvalidTypeNameException(string typeName, string messageId, string method, int startPosition, int endPosition)
			: base(messageId, method, startPosition, endPosition)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x0002C419 File Offset: 0x0002A619
		internal string TypeName
		{
			get
			{
				return this.m_typeName;
			}
		}

		// Token: 0x04000604 RID: 1540
		private readonly string m_typeName;
	}
}
