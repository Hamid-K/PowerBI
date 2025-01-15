using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter.Serialization;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x0200018B RID: 395
	internal static class ODataOutputFormatterHelper
	{
		// Token: 0x06000CF7 RID: 3319 RVA: 0x00033730 File Offset: 0x00031930
		internal static bool TryGetContentHeader(Type type, MediaTypeHeaderValue mediaType, out MediaTypeHeaderValue newMediaType)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			newMediaType = null;
			if (mediaType != null)
			{
				if (mediaType.MediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase))
				{
					if (!mediaType.Parameters.Any((NameValueHeaderValue p) => p.Name.Equals("odata.metadata", StringComparison.OrdinalIgnoreCase)))
					{
						mediaType.Parameters.Add(new NameValueHeaderValue("odata.metadata", "minimal"));
					}
				}
				newMediaType = (MediaTypeHeaderValue)((ICloneable)mediaType).Clone();
				return true;
			}
			return false;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000337C0 File Offset: 0x000319C0
		internal static bool TryGetCharSet(MediaTypeHeaderValue mediaType, IEnumerable<string> acceptCharsetValues, out string charSet)
		{
			charSet = string.Empty;
			return mediaType != null && !acceptCharsetValues.Any((string cs) => cs.Equals(mediaType.CharSet, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00033800 File Offset: 0x00031A00
		internal static bool CanWriteType(Type type, IEnumerable<ODataPayloadKind> payloadKinds, bool isGenericSingleResult, IWebApiRequestMessage internalRequest, Func<Type, ODataSerializer> getODataPayloadSerializer)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			Type type2;
			ODataPayloadKind? odataPayloadKind;
			if (typeof(IEdmObject).IsAssignableFrom(type) || (TypeHelper.IsCollection(type, out type2) && typeof(IEdmObject).IsAssignableFrom(type2)))
			{
				odataPayloadKind = ODataOutputFormatterHelper.GetEdmObjectPayloadKind(type, internalRequest);
			}
			else
			{
				odataPayloadKind = ODataOutputFormatterHelper.GetClrObjectResponsePayloadKind(type, isGenericSingleResult, getODataPayloadSerializer);
			}
			return odataPayloadKind != null && payloadKinds.Contains(odataPayloadKind.Value);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0003387C File Offset: 0x00031A7C
		internal static void WriteToStream(Type type, object value, IEdmModel model, ODataVersion version, Uri baseAddress, MediaTypeHeaderValue contentType, IWebApiUrlHelper internaUrlHelper, IWebApiRequestMessage internalRequest, IWebApiHeaders internalRequestHeaders, Func<IServiceProvider, ODataMessageWrapper> getODataMessageWrapper, Func<IEdmTypeReference, ODataSerializer> getEdmTypeSerializer, Func<Type, ODataSerializer> getODataPayloadSerializer, Func<ODataSerializerContext> getODataSerializerContext)
		{
			if (model == null)
			{
				throw Error.InvalidOperation(SRResources.RequestMustHaveModel, new object[0]);
			}
			ODataSerializer serializer = ODataOutputFormatterHelper.GetSerializer(type, value, internalRequest, getEdmTypeSerializer, getODataPayloadSerializer);
			Microsoft.AspNet.OData.Routing.ODataPath path = internalRequest.Context.Path;
			IEdmNavigationSource edmNavigationSource = ((path == null) ? null : path.NavigationSource);
			string requestPreferHeader = RequestPreferenceHelpers.GetRequestPreferHeader(internalRequestHeaders);
			string text = null;
			if (!string.IsNullOrEmpty(requestPreferHeader))
			{
				ODataMessageWrapper odataMessageWrapper = getODataMessageWrapper(null);
				odataMessageWrapper.SetHeader("Prefer", requestPreferHeader);
				text = odataMessageWrapper.PreferHeader().AnnotationFilter;
			}
			IODataResponseMessage iodataResponseMessage = getODataMessageWrapper(internalRequest.RequestContainer);
			if (text != null)
			{
				iodataResponseMessage.PreferenceAppliedHeader().AnnotationFilter = text;
			}
			ODataMessageWriterSettings writerSettings = internalRequest.WriterSettings;
			writerSettings.BaseUri = baseAddress;
			writerSettings.Version = new ODataVersion?(version);
			writerSettings.Validations &= ~ValidationKinds.ThrowOnUndeclaredPropertyForNonOpenType;
			if (internaUrlHelper.CreateODataLink(new ODataPathSegment[] { MetadataSegment.Instance }) == null)
			{
				throw new SerializationException(SRResources.UnableToDetermineMetadataUrl);
			}
			SelectExpandClause selectExpandClause = null;
			if (internalRequest.Context.QueryOptions != null && internalRequest.Context.QueryOptions.SelectExpand != null)
			{
				if (internalRequest.Context.QueryOptions.SelectExpand.ProcessedSelectExpandClause != internalRequest.Context.ProcessedSelectExpandClause)
				{
					selectExpandClause = internalRequest.Context.ProcessedSelectExpandClause;
				}
			}
			else if (internalRequest.Context.ProcessedSelectExpandClause != null)
			{
				selectExpandClause = internalRequest.Context.ProcessedSelectExpandClause;
			}
			writerSettings.ODataUri = new ODataUri
			{
				ServiceRoot = baseAddress,
				SelectAndExpand = internalRequest.Context.ProcessedSelectExpandClause,
				Apply = internalRequest.Context.ApplyClause,
				Path = ((path == null || ODataOutputFormatterHelper.IsOperationPath(path)) ? null : path.Path)
			};
			ODataMetadataLevel odataMetadataLevel = ODataMetadataLevel.MinimalMetadata;
			if (contentType != null)
			{
				IEnumerable<KeyValuePair<string, string>> enumerable = contentType.Parameters.Select((NameValueHeaderValue val) => new KeyValuePair<string, string>(val.Name, val.Value));
				odataMetadataLevel = ODataMediaTypes.GetMetadataLevel(contentType.MediaType, enumerable);
			}
			using (ODataMessageWriter odataMessageWriter = new ODataMessageWriter(iodataResponseMessage, writerSettings, model))
			{
				ODataSerializerContext odataSerializerContext = getODataSerializerContext();
				odataSerializerContext.NavigationSource = edmNavigationSource;
				odataSerializerContext.Model = model;
				odataSerializerContext.RootElementName = ODataOutputFormatterHelper.GetRootElementName(path) ?? "root";
				odataSerializerContext.SkipExpensiveAvailabilityChecks = serializer.ODataPayloadKind == ODataPayloadKind.ResourceSet;
				odataSerializerContext.Path = path;
				odataSerializerContext.MetadataLevel = odataMetadataLevel;
				odataSerializerContext.QueryOptions = internalRequest.Context.QueryOptions;
				if (selectExpandClause != null)
				{
					odataSerializerContext.SelectExpandClause = selectExpandClause;
				}
				serializer.WriteObject(value, type, odataMessageWriter, odataSerializerContext);
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00033B1C File Offset: 0x00031D1C
		private static ODataPayloadKind? GetClrObjectResponsePayloadKind(Type type, bool isGenericSingleResult, Func<Type, ODataSerializer> getODataPayloadSerializer)
		{
			if (isGenericSingleResult)
			{
				type = type.GetGenericArguments()[0];
			}
			ODataSerializer odataSerializer = getODataPayloadSerializer(type);
			if (odataSerializer != null)
			{
				return new ODataPayloadKind?(odataSerializer.ODataPayloadKind);
			}
			return null;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00033B58 File Offset: 0x00031D58
		private static ODataPayloadKind? GetEdmObjectPayloadKind(Type type, IWebApiRequestMessage internalRequest)
		{
			if (internalRequest.IsCountRequest())
			{
				return new ODataPayloadKind?(ODataPayloadKind.Value);
			}
			Type type2;
			if (TypeHelper.IsCollection(type, out type2))
			{
				if (typeof(IEdmComplexObject).IsAssignableFrom(type2) || typeof(IEdmEnumObject).IsAssignableFrom(type2))
				{
					return new ODataPayloadKind?(ODataPayloadKind.Collection);
				}
				if (typeof(IEdmEntityObject).IsAssignableFrom(type2))
				{
					return new ODataPayloadKind?(ODataPayloadKind.ResourceSet);
				}
				if (typeof(IEdmChangedObject).IsAssignableFrom(type2))
				{
					return new ODataPayloadKind?(ODataPayloadKind.Delta);
				}
			}
			else
			{
				if (typeof(IEdmComplexObject).IsAssignableFrom(type2) || typeof(IEdmEnumObject).IsAssignableFrom(type2))
				{
					return new ODataPayloadKind?(ODataPayloadKind.Property);
				}
				if (typeof(IEdmEntityObject).IsAssignableFrom(type2))
				{
					return new ODataPayloadKind?(ODataPayloadKind.Resource);
				}
			}
			return null;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00033C2C File Offset: 0x00031E2C
		private static ODataSerializer GetSerializer(Type type, object value, IWebApiRequestMessage internalRequest, Func<IEdmTypeReference, ODataSerializer> getEdmTypeSerializer, Func<Type, ODataSerializer> getODataPayloadSerializer)
		{
			IEdmObject edmObject = value as IEdmObject;
			ODataSerializer odataSerializer;
			if (edmObject != null)
			{
				IEdmTypeReference edmType = edmObject.GetEdmType();
				if (edmType == null)
				{
					throw new SerializationException(Error.Format(SRResources.EdmTypeCannotBeNull, new object[]
					{
						edmObject.GetType().FullName,
						typeof(IEdmObject).Name
					}));
				}
				odataSerializer = getEdmTypeSerializer(edmType);
				if (odataSerializer == null)
				{
					throw new SerializationException(Error.Format(SRResources.TypeCannotBeSerialized, new object[] { edmType.ToTraceString() }));
				}
			}
			else
			{
				if (internalRequest.Context.ApplyClause == null)
				{
					type = ((value == null) ? type : value.GetType());
				}
				odataSerializer = getODataPayloadSerializer(type);
				if (odataSerializer == null)
				{
					throw new SerializationException(Error.Format(SRResources.TypeCannotBeSerialized, new object[] { type.Name }));
				}
			}
			return odataSerializer;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00033CF4 File Offset: 0x00031EF4
		private static string GetRootElementName(Microsoft.AspNet.OData.Routing.ODataPath path)
		{
			if (path != null)
			{
				ODataPathSegment odataPathSegment = path.Segments.LastOrDefault<ODataPathSegment>();
				if (odataPathSegment != null)
				{
					OperationSegment operationSegment = odataPathSegment as OperationSegment;
					if (operationSegment != null)
					{
						IEdmAction edmAction = operationSegment.Operations.Single<IEdmOperation>() as IEdmAction;
						if (edmAction != null)
						{
							return edmAction.Name;
						}
					}
					PropertySegment propertySegment = odataPathSegment as PropertySegment;
					if (propertySegment != null)
					{
						return propertySegment.Property.Name;
					}
				}
			}
			return null;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00033D50 File Offset: 0x00031F50
		private static bool IsOperationPath(Microsoft.AspNet.OData.Routing.ODataPath path)
		{
			if (path == null)
			{
				return false;
			}
			foreach (ODataPathSegment odataPathSegment in path.Segments)
			{
				if (odataPathSegment is OperationSegment || odataPathSegment is OperationImportSegment)
				{
					return true;
				}
			}
			return false;
		}
	}
}
