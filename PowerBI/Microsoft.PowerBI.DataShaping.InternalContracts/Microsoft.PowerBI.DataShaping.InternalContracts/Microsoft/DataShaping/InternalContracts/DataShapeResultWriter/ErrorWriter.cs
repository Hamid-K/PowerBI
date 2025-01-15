using System;
using Microsoft.DataShaping.InternalContracts.Errors;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000050 RID: 80
	internal sealed class ErrorWriter : DsrObjectWriterBase
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00004F20 File Offset: 0x00003120
		internal void WriteException(HandledExceptionWrapper exceptionWrapper, bool enableRemoteErrors, IFeatureSwitchProvider featureSwitchProvider)
		{
			ODataError odataError = ExceptionToODataErrorConverter.Convert(exceptionWrapper, enableRemoteErrors, featureSwitchProvider);
			this.WriteExceptionInternal(odataError);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00004F3D File Offset: 0x0000313D
		private void WriteExceptionInternal(ODataError errorObject)
		{
			this.WriteErrorCodeAndSource(errorObject.Code, errorObject.Source);
			this.WriteMessage(errorObject.Message);
			this.WriteAzureValues(errorObject.AzureValues);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00004F69 File Offset: 0x00003169
		internal void WriteErrorCodeAndSource(string errorCode, string source)
		{
			base.Writer.WriteProperty(base.DsrNames.CodeLower, errorCode);
			base.Writer.WriteProperty(base.DsrNames.Source, source);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00004F9C File Offset: 0x0000319C
		internal void WriteMessage(ErrorMessage message)
		{
			base.Writer.BeginProperty(base.DsrNames.MessageLower);
			base.Writer.BeginObject();
			base.Writer.WriteProperty(base.DsrNames.Lang, message.Language);
			base.Writer.WriteProperty(base.DsrNames.ValueLower, message.Value);
			base.Writer.EndObject();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005010 File Offset: 0x00003210
		internal void WriteAzureValues(AzureValue[] azureValues)
		{
			base.Writer.BeginProperty(base.DsrNames.AzureValues);
			base.Writer.BeginArray();
			for (int i = 0; i < azureValues.Length; i++)
			{
				this.WriteAzureValue(azureValues[i]);
			}
			base.Writer.EndArray();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005060 File Offset: 0x00003260
		private void WriteAzureValue(AzureValue azureValue)
		{
			base.Writer.BeginObject();
			this.WriteOptionalProperty(base.DsrNames.Timestamp, azureValue.Timestamp);
			this.WriteOptionalProperty(base.DsrNames.Source, azureValue.Source);
			this.WriteOptionalProperty(base.DsrNames.Details, azureValue.Details);
			this.WriteOptionalProperty(base.DsrNames.ODataPowerBiErrorDetails, azureValue.PowerBiErrorDetails);
			this.WriteAdditionalMessages(azureValue.AdditionalMessages);
			this.WriteMoreInformation(azureValue.MoreInformation);
			base.Writer.EndObject();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000050F8 File Offset: 0x000032F8
		private void WriteAdditionalMessages(Microsoft.InfoNav.Data.Contracts.DataShapeResult.AdditionalMessage[] additionalMessages)
		{
			if (additionalMessages.IsNullOrEmpty<Microsoft.InfoNav.Data.Contracts.DataShapeResult.AdditionalMessage>())
			{
				return;
			}
			base.Writer.BeginProperty(base.DsrNames.AdditionalMessages);
			base.Writer.BeginArray();
			for (int i = 0; i < additionalMessages.Length; i++)
			{
				this.WriteAdditionalMessage(additionalMessages[i]);
			}
			base.Writer.EndArray();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005154 File Offset: 0x00003354
		private void WriteAdditionalMessage(Microsoft.InfoNav.Data.Contracts.DataShapeResult.AdditionalMessage additionalMessage)
		{
			base.Writer.BeginObject();
			base.Writer.WriteProperty(base.DsrNames.CodeUpper, additionalMessage.Code);
			base.Writer.WriteProperty(base.DsrNames.Severity, additionalMessage.Severity);
			base.Writer.WriteProperty(base.DsrNames.MessageUpper, additionalMessage.Message);
			this.WriteOptionalProperty(base.DsrNames.ObjectType, additionalMessage.ObjectType);
			this.WriteOptionalProperty(base.DsrNames.ObjectName, additionalMessage.ObjectName);
			this.WriteOptionalProperty(base.DsrNames.PropertyName, additionalMessage.PropertyName);
			this.WriteOptionalProperty(base.DsrNames.Line, additionalMessage.Line);
			this.WriteOptionalProperty(base.DsrNames.Position, additionalMessage.Position);
			if (!additionalMessage.AffectedItems.IsNullOrEmpty<string>())
			{
				base.Writer.BeginProperty(base.DsrNames.AffectedItems);
				base.Writer.BeginArray();
				foreach (string text in additionalMessage.AffectedItems)
				{
					base.Writer.WriteValue(text);
				}
				base.Writer.EndArray();
			}
			base.Writer.EndObject();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000529C File Offset: 0x0000349C
		private void WriteMoreInformation(MoreInformation moreInformation)
		{
			if (moreInformation == null)
			{
				return;
			}
			base.Writer.BeginProperty(base.DsrNames.MoreInformation);
			base.Writer.BeginObject();
			base.Writer.BeginProperty(base.DsrNames.OdataError);
			base.Writer.BeginObject();
			this.WriteExceptionInternal(moreInformation.ODataError);
			base.Writer.EndObject();
			base.Writer.EndObject();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005311 File Offset: 0x00003511
		private void WriteOptionalProperty(string propertyName, string propertyValue)
		{
			if (propertyValue != null)
			{
				base.Writer.WriteProperty(propertyName, propertyValue);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005323 File Offset: 0x00003523
		private void WriteOptionalProperty(string propertyName, int? propertyValue)
		{
			if (propertyValue != null)
			{
				base.Writer.WriteProperty(propertyName, propertyValue.Value);
			}
		}

		// Token: 0x040000D7 RID: 215
		private const string OnPremiseServiceErrorSeverity = "Error";
	}
}
