using System;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.DataExtension.Contracts;

namespace Microsoft.DataShaping.InternalContracts.DaxQueryResultWriter
{
	// Token: 0x02000031 RID: 49
	internal sealed class DaxQueryResultErrorWriter : DaxQueryResultObjectWriterBase
	{
		// Token: 0x0600011F RID: 287 RVA: 0x000040C6 File Offset: 0x000022C6
		internal void WriteException(HandledExceptionWrapper ex)
		{
			base.Writer.BeginProperty("error");
			base.Writer.BeginObject();
			this.WriteErrorCode(ex);
			this.WriteErrorMessage(ex);
			this.WriteErrorDetails(ex);
			base.Writer.EndObject();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004103 File Offset: 0x00002303
		private void WriteErrorCode(HandledExceptionWrapper ex)
		{
			base.Writer.BeginProperty("code");
			base.Writer.WriteValue(ex.ErrorCode);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004127 File Offset: 0x00002327
		private void WriteErrorMessage(HandledExceptionWrapper ex)
		{
			base.Writer.BeginProperty("message");
			base.Writer.WriteValue(ex.ErrorDetails.RemovePrivateAndInternalMarkup());
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004150 File Offset: 0x00002350
		private void WriteErrorDetails(HandledExceptionWrapper ex)
		{
			if (ex.DataExtensionException != null)
			{
				base.Writer.BeginProperty("details");
				base.Writer.BeginArray();
				this.WriteASErrorCode(ex.DataExtensionException);
				base.Writer.EndArray();
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004190 File Offset: 0x00002390
		private void WriteASErrorCode(DataExtensionException ex)
		{
			base.Writer.BeginObject();
			base.Writer.BeginProperty("code");
			base.Writer.WriteValue("AnalysisServicesErrorCode");
			base.Writer.BeginProperty("detail");
			base.Writer.BeginObject();
			base.Writer.BeginProperty("type");
			base.Writer.WriteValue("1");
			base.Writer.BeginProperty("value");
			base.Writer.WriteValue((long)((ulong)ex.ProviderErrorCode));
			base.Writer.EndObject();
			base.Writer.EndObject();
		}
	}
}
