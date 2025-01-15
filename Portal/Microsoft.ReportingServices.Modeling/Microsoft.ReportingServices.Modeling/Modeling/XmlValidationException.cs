using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200004D RID: 77
	[Serializable]
	internal class XmlValidationException : ValidationException
	{
		// Token: 0x06000321 RID: 801 RVA: 0x0000AF48 File Offset: 0x00009148
		internal XmlValidationException(ModelingErrorCode code, string message)
			: base(code, message)
		{
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000AF52 File Offset: 0x00009152
		internal XmlValidationException(ModelingErrorCode code, string message, Exception inner)
			: base(code, message, inner)
		{
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000AF5D File Offset: 0x0000915D
		internal XmlValidationException(ModelingErrorCode code, string message, int lineNumber, int linePosition)
			: this(code, message, null, lineNumber, linePosition)
		{
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000AF6B File Offset: 0x0000916B
		internal XmlValidationException(ModelingErrorCode code, string message, Exception inner, int lineNumber, int linePosition)
			: base(code, message, inner)
		{
			this.m_lineNumber = lineNumber;
			this.m_linePosition = linePosition;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000AF86 File Offset: 0x00009186
		internal XmlValidationException(ModelingErrorCode code, string message, IXmlLineInfo lineInfo)
			: this(code, message, null, lineInfo)
		{
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000AF92 File Offset: 0x00009192
		internal XmlValidationException(ModelingErrorCode code, string message, Exception inner, IXmlLineInfo lineInfo)
			: base(code, message, inner)
		{
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				this.m_lineNumber = lineInfo.LineNumber;
				this.m_linePosition = lineInfo.LinePosition;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000AFC4 File Offset: 0x000091C4
		internal XmlValidationException(ModelingErrorCode code, string message, Exception inner, int lineNumber, int linePosition, bool messageHasLineInfo)
			: this(code, message, inner, lineNumber, linePosition)
		{
			this.m_messageHasLineInfo = messageHasLineInfo;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000AFDB File Offset: 0x000091DB
		protected XmlValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_lineNumber = info.GetInt32("LineNumber");
			this.m_linePosition = info.GetInt32("LinePosition");
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000B007 File Offset: 0x00009207
		public override string Message
		{
			get
			{
				if (this.m_lineNumber > 0 && !this.m_messageHasLineInfo)
				{
					return SRErrors.XmlValidationException_MessageWithLineInfo(base.Message, this.m_lineNumber, this.m_linePosition);
				}
				return base.Message;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000B038 File Offset: 0x00009238
		public int LineNumber
		{
			get
			{
				return this.m_lineNumber;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000B040 File Offset: 0x00009240
		public int LinePosition
		{
			get
			{
				return this.m_linePosition;
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000B048 File Offset: 0x00009248
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("LineNumber", this.m_lineNumber);
			info.AddValue("LinePosition", this.m_linePosition);
		}

		// Token: 0x040001F7 RID: 503
		private readonly int m_lineNumber;

		// Token: 0x040001F8 RID: 504
		private readonly int m_linePosition;

		// Token: 0x040001F9 RID: 505
		private readonly bool m_messageHasLineInfo;
	}
}
