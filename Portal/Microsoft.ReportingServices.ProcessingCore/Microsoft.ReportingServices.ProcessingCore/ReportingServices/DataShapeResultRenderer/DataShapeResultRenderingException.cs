using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.DataShapeResultRenderer
{
	// Token: 0x0200057F RID: 1407
	internal sealed class DataShapeResultRenderingException : RSException
	{
		// Token: 0x06005135 RID: 20789 RVA: 0x00158D72 File Offset: 0x00156F72
		internal DataShapeResultRenderingException(string localizedErrorMessage)
			: this(localizedErrorMessage, null)
		{
		}

		// Token: 0x06005136 RID: 20790 RVA: 0x00158D7C File Offset: 0x00156F7C
		internal DataShapeResultRenderingException(string localizedErrorMessage, DataShapeMessageCollection messages)
			: base(ErrorCode.rrRenderingError, localizedErrorMessage, null, Global.RenderingTracer, null, Array.Empty<object>())
		{
			this.m_messages = messages;
		}

		// Token: 0x06005137 RID: 20791 RVA: 0x00158D9D File Offset: 0x00156F9D
		internal DataShapeResultRenderingException(Exception innerException)
			: this(innerException, null, false)
		{
		}

		// Token: 0x06005138 RID: 20792 RVA: 0x00158DA8 File Offset: 0x00156FA8
		internal DataShapeResultRenderingException(Exception innerException, DataShapeMessageCollection messages)
			: this(innerException, messages, false)
		{
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x00158DB4 File Offset: 0x00156FB4
		internal DataShapeResultRenderingException(Exception innerException, DataShapeMessageCollection messages, bool includeNestedErrorCodeInAdditionalMessages)
			: base(DataShapeResultRenderingException.DetermineErrorCode(innerException), (innerException is RSException) ? innerException.Message : RPResWrapper.Keys.GetString(ErrorCode.rsUnexpectedError.ToString()), innerException, Global.RenderingTracer, null, Array.Empty<object>())
		{
			this.m_messages = messages;
			if (includeNestedErrorCodeInAdditionalMessages)
			{
				RSException innermostException = ExceptionUtils.GetInnermostException<RSException>(innerException);
				if (innermostException != null && innermostException.Code != base.Code)
				{
					this.m_innermostExceptionErrorCode = innermostException.Code.ToString();
				}
			}
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x00158E40 File Offset: 0x00157040
		private static ErrorCode DetermineErrorCode(Exception innerException)
		{
			ReportProcessingQueryException ex = innerException as ReportProcessingQueryException;
			if (ex != null)
			{
				return ex.Code;
			}
			ReportProcessingQueryOnPremiseServiceException ex2 = innerException as ReportProcessingQueryOnPremiseServiceException;
			if (ex2 != null)
			{
				return ex2.Code;
			}
			if (innerException is RSException)
			{
				return ErrorCode.rrRenderingError;
			}
			return ErrorCode.rsInternalError;
		}

		// Token: 0x0600513B RID: 20795 RVA: 0x00158E80 File Offset: 0x00157080
		protected override List<RSException.AdditionalMessage> GetAdditionalMessages()
		{
			ReportProcessingQueryOnPremiseServiceException ex = base.InnerException as ReportProcessingQueryOnPremiseServiceException;
			if (ex != null)
			{
				return ex.AdditionalMessages;
			}
			if (this.m_messages == null && this.m_innermostExceptionErrorCode == null)
			{
				return null;
			}
			List<RSException.AdditionalMessage> list = new List<RSException.AdditionalMessage>((this.m_messages != null) ? (this.m_messages.Count + 1) : 1);
			if (!this.m_innermostExceptionErrorCode.IsNullOrWhiteSpace())
			{
				list.Add(new RSException.AdditionalMessage(this.m_innermostExceptionErrorCode, "Error", string.Empty, string.Empty, string.Empty, string.Empty, null));
			}
			if (this.m_messages != null)
			{
				foreach (DataShapeErrorMessage dataShapeErrorMessage in this.m_messages)
				{
					list.Add(new RSException.AdditionalMessage(dataShapeErrorMessage.ErrorCode.ToString(), dataShapeErrorMessage.Severity.ToString(), dataShapeErrorMessage.Message, dataShapeErrorMessage.ObjectType, dataShapeErrorMessage.ObjectName, dataShapeErrorMessage.PropertyName, null));
				}
			}
			return list;
		}

		// Token: 0x040028F1 RID: 10481
		private DataShapeMessageCollection m_messages;

		// Token: 0x040028F2 RID: 10482
		private string m_innermostExceptionErrorCode;
	}
}
