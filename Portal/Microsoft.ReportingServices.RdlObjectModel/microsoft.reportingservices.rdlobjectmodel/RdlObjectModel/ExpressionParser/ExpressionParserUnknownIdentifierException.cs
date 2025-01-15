using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000233 RID: 563
	[Serializable]
	internal class ExpressionParserUnknownIdentifierException : ExpressionParserException
	{
		// Token: 0x06001307 RID: 4871 RVA: 0x0002C421 File Offset: 0x0002A621
		public ExpressionParserUnknownIdentifierException(string name, string messageId, string method, int startPosition, int endPosition)
			: base(messageId, method, startPosition, endPosition)
		{
			this.m_name = name;
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x0002C436 File Offset: 0x0002A636
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x04000605 RID: 1541
		private readonly string m_name;
	}
}
