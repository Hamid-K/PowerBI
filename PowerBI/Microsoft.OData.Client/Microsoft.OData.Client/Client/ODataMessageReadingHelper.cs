using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client
{
	// Token: 0x02000057 RID: 87
	internal class ODataMessageReadingHelper
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x0000ABAC File Offset: 0x00008DAC
		internal ODataMessageReadingHelper(ResponseInfo responseInfo)
		{
			this.responseInfo = responseInfo;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000ABBC File Offset: 0x00008DBC
		internal ODataMessageReaderSettings CreateSettings()
		{
			ODataMessageReaderSettings odataMessageReaderSettings = new ODataMessageReaderSettings();
			Func<IEdmType, string, IEdmType> func = new Func<IEdmType, string, IEdmType>(this.responseInfo.TypeResolver.ResolveWireTypeName);
			if (this.responseInfo.Context.Format.ServiceModel != null)
			{
				func = null;
			}
			odataMessageReaderSettings.Validations &= ~(ValidationKinds.ThrowOnDuplicatePropertyNames | ValidationKinds.ThrowIfTypeConflictsWithMetadata);
			odataMessageReaderSettings.ClientCustomTypeResolver = func;
			odataMessageReaderSettings.BaseUri = this.responseInfo.BaseUriResolver.BaseUriOrNull;
			odataMessageReaderSettings.MaxProtocolVersion = CommonUtil.ConvertToODataVersion(this.responseInfo.MaxProtocolVersion);
			if (!this.responseInfo.ThrowOnUndeclaredPropertyForNonOpenType)
			{
				odataMessageReaderSettings.Validations &= ~ValidationKinds.ThrowOnUndeclaredPropertyForNonOpenType;
			}
			CommonUtil.SetDefaultMessageQuotas(odataMessageReaderSettings.MessageQuotas);
			this.responseInfo.ResponsePipeline.ExecuteReaderSettingsConfiguration(odataMessageReaderSettings);
			return odataMessageReaderSettings;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000AC7A File Offset: 0x00008E7A
		internal ODataMessageReader CreateReader(IODataResponseMessage responseMessage, ODataMessageReaderSettings settings)
		{
			DataServiceClientFormat.ValidateCanReadResponseFormat(responseMessage);
			return new ODataMessageReader(responseMessage, settings, this.responseInfo.TypeResolver.ReaderModel);
		}

		// Token: 0x040000EB RID: 235
		private readonly ResponseInfo responseInfo;
	}
}
