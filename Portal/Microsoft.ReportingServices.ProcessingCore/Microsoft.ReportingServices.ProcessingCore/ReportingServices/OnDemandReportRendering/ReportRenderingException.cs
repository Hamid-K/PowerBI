using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200007B RID: 123
	[Serializable]
	public class ReportRenderingException : Exception
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x0001BBF2 File Offset: 0x00019DF2
		protected ReportRenderingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001BBFC File Offset: 0x00019DFC
		public ReportRenderingException(ErrorCode errCode)
			: this(errCode, RenderErrorsWrapper.Keys.GetString(errCode.ToString()), false)
		{
			this.m_ErrorCode = errCode;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001BC1F File Offset: 0x00019E1F
		public ReportRenderingException(string message)
			: this(ErrorCode.rrRenderingError, message, false)
		{
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001BC2E File Offset: 0x00019E2E
		public ReportRenderingException(string message, bool unexpected)
			: this(ErrorCode.rrRenderingError, message, unexpected)
		{
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001BC3D File Offset: 0x00019E3D
		public ReportRenderingException(ErrorCode errCode, string message, bool unexpected)
			: base(message)
		{
			this.m_ErrorCode = errCode;
			this.m_Unexpected = unexpected;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001BC54 File Offset: 0x00019E54
		public ReportRenderingException(Exception innerException)
			: this(ErrorCode.rrRenderingError, innerException, false)
		{
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001BC63 File Offset: 0x00019E63
		public ReportRenderingException(Exception innerException, bool unexpected)
			: this(ErrorCode.rrRenderingError, innerException, unexpected)
		{
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001BC72 File Offset: 0x00019E72
		public ReportRenderingException(ErrorCode errCode, Exception innerException)
			: this(errCode, RenderErrorsWrapper.Keys.GetString(errCode.ToString()), innerException, false)
		{
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001BC8F File Offset: 0x00019E8F
		public ReportRenderingException(ErrorCode errCode, Exception innerException, bool unexpected)
			: this(errCode, RenderErrorsWrapper.Keys.GetString(errCode.ToString()), innerException, unexpected)
		{
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001BCAC File Offset: 0x00019EAC
		public ReportRenderingException(string message, Exception innerException)
			: this(ErrorCode.rrRenderingError, message, innerException, false)
		{
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001BCBC File Offset: 0x00019EBC
		public ReportRenderingException(string message, Exception innerException, bool unexpected)
			: this(ErrorCode.rrRenderingError, message, innerException, unexpected)
		{
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001BCCC File Offset: 0x00019ECC
		public ReportRenderingException(ErrorCode errCode, string message, Exception innerException, bool unexpected)
			: base(message, innerException)
		{
			this.m_ErrorCode = errCode;
			this.m_Unexpected = unexpected;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001BCE5 File Offset: 0x00019EE5
		public ReportRenderingException(ErrorCode errCode, params object[] arguments)
			: base(string.Format(CultureInfo.CurrentCulture, RenderErrorsWrapper.Keys.GetString(errCode.ToString()), arguments))
		{
			this.m_ErrorCode = errCode;
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0001BD11 File Offset: 0x00019F11
		public ErrorCode ErrorCode
		{
			get
			{
				return this.m_ErrorCode;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0001BD19 File Offset: 0x00019F19
		public bool Unexpected
		{
			get
			{
				return this.m_Unexpected;
			}
		}

		// Token: 0x04000207 RID: 519
		private ErrorCode m_ErrorCode;

		// Token: 0x04000208 RID: 520
		private bool m_Unexpected;
	}
}
