using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000058 RID: 88
	internal class ODataMessageWritingHelper
	{
		// Token: 0x060002BC RID: 700 RVA: 0x0000AC99 File Offset: 0x00008E99
		internal ODataMessageWritingHelper(RequestInfo requestInfo)
		{
			this.requestInfo = requestInfo;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		internal ODataMessageWriterSettings CreateSettings(bool isBatchPartRequest, bool enableWritingODataAnnotationWithoutPrefix)
		{
			ODataMessageWriterSettings odataMessageWriterSettings = new ODataMessageWriterSettings
			{
				EnableCharactersCheck = false,
				EnableMessageStreamDisposal = isBatchPartRequest
			};
			CommonUtil.SetDefaultMessageQuotas(odataMessageWriterSettings.MessageQuotas);
			this.requestInfo.Configurations.RequestPipeline.ExecuteWriterSettingsConfiguration(odataMessageWriterSettings);
			return odataMessageWriterSettings;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000ACEC File Offset: 0x00008EEC
		internal ODataMessageWriter CreateWriter(IODataRequestMessage requestMessage, ODataMessageWriterSettings writerSettings, bool isParameterPayload)
		{
			DataServiceClientFormat.ValidateCanWriteRequestFormat(requestMessage);
			ClientEdmModel clientEdmModel = (isParameterPayload ? null : this.requestInfo.Model);
			return new ODataMessageWriter(requestMessage, writerSettings, clientEdmModel);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000AD19 File Offset: 0x00008F19
		internal ODataRequestMessageWrapper CreateRequestMessage(BuildingRequestEventArgs requestMessageArgs)
		{
			return ODataRequestMessageWrapper.CreateRequestMessageWrapper(requestMessageArgs, this.requestInfo);
		}

		// Token: 0x040000EC RID: 236
		private readonly RequestInfo requestInfo;
	}
}
