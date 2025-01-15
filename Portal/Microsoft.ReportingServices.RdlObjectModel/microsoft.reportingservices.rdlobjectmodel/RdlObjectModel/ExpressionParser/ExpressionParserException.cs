using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000231 RID: 561
	[Serializable]
	internal class ExpressionParserException : Exception
	{
		// Token: 0x060012FC RID: 4860 RVA: 0x0002C2C9 File Offset: 0x0002A4C9
		public ExpressionParserException(string messageId)
			: base(messageId)
		{
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0002C2DD File Offset: 0x0002A4DD
		public ExpressionParserException(string messageId, string method, int startPosition, int endPosition)
			: base(messageId)
		{
			this._Method = method;
			this._StartPosition = startPosition;
			this._EndPosition = endPosition;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0002C307 File Offset: 0x0002A507
		public ExpressionParserException(string messageId, string method, string expectedvalue, int startPosition, int endPosition)
			: base(messageId)
		{
			this._Method = method;
			this._StartPosition = startPosition;
			this._EndPosition = endPosition;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0002C332 File Offset: 0x0002A532
		public ExpressionParserException(string messageId, string method, int startPosition, int endPosition, params object[] args)
			: base(messageId)
		{
			this._Method = method;
			this._StartPosition = startPosition;
			this._EndPosition = endPosition;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0002C35C File Offset: 0x0002A55C
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		private ExpressionParserException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._Method = info.GetString("Method");
			this._StartPosition = info.GetInt32("StartPosition");
			this._EndPosition = info.GetInt32("EndPosition");
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x0002C3AF File Offset: 0x0002A5AF
		public string Method
		{
			get
			{
				return this._Method;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0002C3B7 File Offset: 0x0002A5B7
		public int StartPosition
		{
			get
			{
				return this._StartPosition;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0002C3BF File Offset: 0x0002A5BF
		public int EndPosition
		{
			get
			{
				return this._EndPosition;
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0002C3C7 File Offset: 0x0002A5C7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Method", this._Method);
			info.AddValue("StartPosition", this._StartPosition);
			info.AddValue("EndPosition", this._EndPosition);
		}

		// Token: 0x04000601 RID: 1537
		private readonly string _Method = "";

		// Token: 0x04000602 RID: 1538
		private readonly int _StartPosition;

		// Token: 0x04000603 RID: 1539
		private readonly int _EndPosition;
	}
}
