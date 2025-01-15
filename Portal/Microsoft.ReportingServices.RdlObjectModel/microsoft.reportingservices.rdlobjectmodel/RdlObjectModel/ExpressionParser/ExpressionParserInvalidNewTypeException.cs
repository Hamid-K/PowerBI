using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000235 RID: 565
	[Serializable]
	internal class ExpressionParserInvalidNewTypeException : ExpressionParserException
	{
		// Token: 0x0600130B RID: 4875 RVA: 0x0002C45D File Offset: 0x0002A65D
		public ExpressionParserInvalidNewTypeException(string typeName, string messageId, string method, int startPosition, int endPosition)
			: base(messageId, method, startPosition, endPosition)
		{
			this.m_typeName = typeName;
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x0002C472 File Offset: 0x0002A672
		internal string TypeName
		{
			get
			{
				return this.m_typeName;
			}
		}

		// Token: 0x04000607 RID: 1543
		private readonly string m_typeName;
	}
}
