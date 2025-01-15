using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000234 RID: 564
	[Serializable]
	internal class ExpressionParserInvalidMemberNameException : ExpressionParserException
	{
		// Token: 0x06001309 RID: 4873 RVA: 0x0002C43E File Offset: 0x0002A63E
		public ExpressionParserInvalidMemberNameException(string memberName, string messageId, string method, string expectedvalue, int startPosition, int endPosition)
			: base(messageId, method, expectedvalue, startPosition, endPosition)
		{
			this.m_memberName = memberName;
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x0002C455 File Offset: 0x0002A655
		public string MemberName
		{
			get
			{
				return this.m_memberName;
			}
		}

		// Token: 0x04000606 RID: 1542
		private readonly string m_memberName;
	}
}
