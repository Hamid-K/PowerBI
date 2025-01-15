using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter.Deserialization;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000189 RID: 393
	internal static class ODataInputFormatterHelper
	{
		// Token: 0x06000CEA RID: 3306 RVA: 0x00032EB0 File Offset: 0x000310B0
		internal static bool CanReadType(Type type, IEdmModel model, ODataPath path, IEnumerable<ODataPayloadKind> payloadKinds, Func<IEdmTypeReference, ODataDeserializer> getEdmTypeDeserializer, Func<Type, ODataDeserializer> getODataPayloadDeserializer)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			IEdmTypeReference edmTypeReference;
			ODataDeserializer deserializer = ODataInputFormatterHelper.GetDeserializer(type, path, model, getEdmTypeDeserializer, getODataPayloadDeserializer, out edmTypeReference);
			return deserializer != null && payloadKinds.Contains(deserializer.ODataPayloadKind);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00032EF4 File Offset: 0x000310F4
		internal static object ReadFromStream(Type type, object defaultValue, IEdmModel model, Uri baseAddress, IWebApiRequestMessage internalRequest, Func<IODataRequestMessage> getODataRequestMessage, Func<IEdmTypeReference, ODataDeserializer> getEdmTypeDeserializer, Func<Type, ODataDeserializer> getODataPayloadDeserializer, Func<ODataDeserializerContext> getODataDeserializerContext, Action<IDisposable> registerForDisposeAction, Action<Exception> logErrorAction)
		{
			IEdmTypeReference edmTypeReference;
			ODataDeserializer deserializer = ODataInputFormatterHelper.GetDeserializer(type, internalRequest.Context.Path, model, getEdmTypeDeserializer, getODataPayloadDeserializer, out edmTypeReference);
			if (deserializer == null)
			{
				throw Error.Argument("type", SRResources.FormatterReadIsNotSupportedForType, new object[]
				{
					type.FullName,
					typeof(ODataInputFormatterHelper).FullName
				});
			}
			object obj;
			try
			{
				ODataMessageReaderSettings readerSettings = internalRequest.ReaderSettings;
				readerSettings.BaseUri = baseAddress;
				readerSettings.Validations &= ~ValidationKinds.ThrowOnUndeclaredPropertyForNonOpenType;
				ODataMessageReader odataMessageReader = new ODataMessageReader(getODataRequestMessage(), readerSettings, model);
				registerForDisposeAction(odataMessageReader);
				ODataPath path = internalRequest.Context.Path;
				ODataDeserializerContext odataDeserializerContext = getODataDeserializerContext();
				odataDeserializerContext.Path = path;
				odataDeserializerContext.Model = model;
				odataDeserializerContext.ResourceType = type;
				odataDeserializerContext.ResourceEdmType = edmTypeReference;
				obj = deserializer.Read(odataMessageReader, type, odataDeserializerContext);
			}
			catch (Exception ex)
			{
				logErrorAction(ex);
				obj = defaultValue;
			}
			return obj;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00032FF0 File Offset: 0x000311F0
		private static ODataDeserializer GetDeserializer(Type type, ODataPath path, IEdmModel model, Func<IEdmTypeReference, ODataDeserializer> getEdmTypeDeserializer, Func<Type, ODataDeserializer> getODataPayloadDeserializer, out IEdmTypeReference expectedPayloadType)
		{
			expectedPayloadType = EdmLibHelpers.GetExpectedPayloadType(type, path, model);
			ODataDeserializer odataDeserializer = getODataPayloadDeserializer(type);
			if (odataDeserializer == null && expectedPayloadType != null)
			{
				odataDeserializer = getEdmTypeDeserializer(expectedPayloadType);
			}
			return odataDeserializer;
		}
	}
}
