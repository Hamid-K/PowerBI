using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client
{
	// Token: 0x020000DF RID: 223
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Class needs refactoring.")]
	public class Serializer
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x0001E9FD File Offset: 0x0001CBFD
		internal Serializer(RequestInfo requestInfo)
		{
			this.requestInfo = requestInfo;
			this.propertyConverter = new ODataPropertyConverter(this.requestInfo);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001EA1D File Offset: 0x0001CC1D
		internal Serializer(RequestInfo requestInfo, EntityParameterSendOption sendOption)
			: this(requestInfo)
		{
			this.sendOption = sendOption;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001EA2D File Offset: 0x0001CC2D
		internal Serializer(RequestInfo requestInfo, SaveChangesOptions options)
			: this(requestInfo)
		{
			this.options = options;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001EA40 File Offset: 0x0001CC40
		public static string GetKeyString(DataServiceContext context, Dictionary<string, object> keys)
		{
			RequestInfo requestInfo = new RequestInfo(context);
			Serializer serializer = new Serializer(requestInfo);
			if (keys.Count == 1)
			{
				return serializer.ConvertToEscapedUriValue(keys.First<KeyValuePair<string, object>>().Key, keys.First<KeyValuePair<string, object>>().Value, false);
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, object> keyValuePair in keys)
			{
				stringBuilder.Append(keyValuePair.Key);
				stringBuilder.Append('=');
				stringBuilder.Append(serializer.ConvertToEscapedUriValue(keyValuePair.Key, keyValuePair.Value, false));
				stringBuilder.Append(',');
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			return stringBuilder.ToString();
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001EB1C File Offset: 0x0001CD1C
		public static string GetParameterString(DataServiceContext context, params OperationParameter[] parameters)
		{
			RequestInfo requestInfo = new RequestInfo(context);
			Serializer serializer = new Serializer(requestInfo);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			foreach (OperationParameter operationParameter in parameters)
			{
				stringBuilder.Append(operationParameter.Name);
				stringBuilder.Append('=');
				stringBuilder.Append(serializer.ConvertToEscapedUriValue(operationParameter.Name, operationParameter.Value, false));
				stringBuilder.Append(',');
			}
			if (parameters.Any<OperationParameter>())
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001EBC4 File Offset: 0x0001CDC4
		internal static string GetParameterValue(DataServiceContext context, OperationParameter parameter)
		{
			RequestInfo requestInfo = new RequestInfo(context);
			Serializer serializer = new Serializer(requestInfo);
			UriEntityOperationParameter uriEntityOperationParameter = parameter as UriEntityOperationParameter;
			return serializer.ConvertToEscapedUriValue(parameter.Name, parameter.Value, uriEntityOperationParameter != null && uriEntityOperationParameter.UseEntityReference);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001EC04 File Offset: 0x0001CE04
		internal static ODataMessageWriter CreateMessageWriter(ODataRequestMessageWrapper requestMessage, RequestInfo requestInfo, bool isParameterPayload)
		{
			ODataMessageWriterSettings odataMessageWriterSettings = requestInfo.WriteHelper.CreateSettings(requestMessage.IsBatchPartRequest, requestInfo.Context.EnableWritingODataAnnotationWithoutPrefix);
			return requestMessage.CreateWriter(odataMessageWriterSettings, isParameterPayload);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001EC38 File Offset: 0x0001CE38
		internal static ODataResource CreateODataEntry(EntityDescriptor entityDescriptor, string serverTypeName, ClientTypeAnnotation entityType, DataServiceClientFormat clientFormat)
		{
			ODataResource odataResource = new ODataResource();
			if (entityType.ElementTypeName != serverTypeName)
			{
				odataResource.TypeAnnotation = new ODataTypeAnnotation(serverTypeName);
			}
			odataResource.TypeName = entityType.ElementTypeName;
			if (entityDescriptor.IsMediaLinkEntry || entityType.IsMediaLinkEntry)
			{
				odataResource.MediaResource = new ODataStreamReferenceValue();
			}
			return odataResource;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001EC90 File Offset: 0x0001CE90
		internal void WriteBodyOperationParameters(List<BodyOperationParameter> operationParameters, ODataRequestMessageWrapper requestMessage)
		{
			using (ODataMessageWriter odataMessageWriter = Serializer.CreateMessageWriter(requestMessage, this.requestInfo, true))
			{
				ODataParameterWriter odataParameterWriter = odataMessageWriter.CreateODataParameterWriter(null);
				odataParameterWriter.WriteStart();
				foreach (BodyOperationParameter bodyOperationParameter in operationParameters)
				{
					if (bodyOperationParameter.Value != null)
					{
						ClientEdmModel model = this.requestInfo.Model;
						IEdmType orCreateEdmType = model.GetOrCreateEdmType(bodyOperationParameter.Value.GetType());
						switch (orCreateEdmType.TypeKind)
						{
						case EdmTypeKind.Primitive:
						{
							object obj = ODataPropertyConverter.ConvertPrimitiveValueToRecognizedODataType(bodyOperationParameter.Value, bodyOperationParameter.Value.GetType());
							odataParameterWriter.WriteValue(bodyOperationParameter.Name, obj);
							continue;
						}
						case EdmTypeKind.Entity:
						case EdmTypeKind.Complex:
						{
							ODataResourceWrapper odataResourceWrapper = this.CreateODataResourceFromEntityOperationParameter(model.GetClientTypeAnnotation(orCreateEdmType), bodyOperationParameter.Value);
							ODataWriter odataWriter = odataParameterWriter.CreateResourceWriter(bodyOperationParameter.Name);
							ODataWriterHelper.WriteResource(odataWriter, odataResourceWrapper);
							continue;
						}
						case EdmTypeKind.Collection:
							this.WriteCollectionValueInBodyOperationParameter(odataParameterWriter, bodyOperationParameter, (IEdmCollectionType)orCreateEdmType);
							continue;
						case EdmTypeKind.Enum:
						{
							ODataEnumValue odataEnumValue = this.propertyConverter.CreateODataEnumValue(model.GetClientTypeAnnotation(orCreateEdmType).ElementType, bodyOperationParameter.Value, false);
							odataParameterWriter.WriteValue(bodyOperationParameter.Name, odataEnumValue);
							continue;
						}
						}
						throw new NotSupportedException(Strings.Serializer_InvalidParameterType(bodyOperationParameter.Name, orCreateEdmType.TypeKind));
					}
					odataParameterWriter.WriteValue(bodyOperationParameter.Name, bodyOperationParameter.Value);
				}
				odataParameterWriter.WriteEnd();
				odataParameterWriter.Flush();
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001EE60 File Offset: 0x0001D060
		internal void WriteEntry(EntityDescriptor entityDescriptor, IEnumerable<LinkDescriptor> relatedLinks, ODataRequestMessageWrapper requestMessage)
		{
			ClientEdmModel model = this.requestInfo.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entityDescriptor.Entity.GetType()));
			using (ODataMessageWriter odataMessageWriter = Serializer.CreateMessageWriter(requestMessage, this.requestInfo, false))
			{
				ODataWriterWrapper odataWriterWrapper = ODataWriterWrapper.CreateForEntry(odataMessageWriter, this.requestInfo.Configurations.RequestPipeline);
				string text = this.requestInfo.GetServerTypeName(entityDescriptor);
				ODataResource odataResource = Serializer.CreateODataEntry(entityDescriptor, text, clientTypeAnnotation, this.requestInfo.Format);
				if (text == null)
				{
					text = this.requestInfo.InferServerTypeNameFromServerModel(entityDescriptor);
				}
				IEnumerable<ClientPropertyAnnotation> enumerable;
				if ((!Util.IsFlagSet(this.options, SaveChangesOptions.ReplaceOnUpdate) && entityDescriptor.State == EntityStates.Modified && entityDescriptor.PropertiesToSerialize.Any<string>()) || (Util.IsFlagSet(this.options, SaveChangesOptions.PostOnlySetProperties) && entityDescriptor.State == EntityStates.Added))
				{
					enumerable = from prop in clientTypeAnnotation.PropertiesToSerialize()
						where entityDescriptor.PropertiesToSerialize.Contains(prop.PropertyName)
						select prop;
				}
				else
				{
					enumerable = clientTypeAnnotation.PropertiesToSerialize();
				}
				odataResource.Properties = this.propertyConverter.PopulateProperties(entityDescriptor.Entity, text, enumerable);
				odataWriterWrapper.WriteStart(odataResource, entityDescriptor.Entity);
				this.WriteNestedComplexProperties(entityDescriptor.Entity, text, enumerable, odataWriterWrapper);
				if (EntityStates.Added == entityDescriptor.State)
				{
					this.WriteNestedResourceInfo(entityDescriptor, relatedLinks, odataWriterWrapper);
				}
				odataWriterWrapper.WriteEnd(odataResource, entityDescriptor.Entity);
			}
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001F024 File Offset: 0x0001D224
		internal void WriteNestedComplexProperties(object entity, string serverTypeName, IEnumerable<ClientPropertyAnnotation> properties, ODataWriterWrapper odataWriter)
		{
			IEnumerable<ClientPropertyAnnotation> enumerable = properties.Where((ClientPropertyAnnotation p) => p.IsComplex || p.IsComplexCollection);
			IEnumerable<ODataNestedResourceInfoWrapper> enumerable2 = this.propertyConverter.PopulateNestedComplexProperties(entity, serverTypeName, enumerable, null);
			foreach (ODataNestedResourceInfoWrapper odataNestedResourceInfoWrapper in enumerable2)
			{
				Serializer.WriteNestedResourceInfo(odataWriter, odataNestedResourceInfoWrapper);
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001F0A4 File Offset: 0x0001D2A4
		internal void WriteNestedResourceInfo(EntityDescriptor entityDescriptor, IEnumerable<LinkDescriptor> relatedLinks, ODataWriterWrapper odataWriter)
		{
			Dictionary<string, List<LinkDescriptor>> dictionary = new Dictionary<string, List<LinkDescriptor>>(EqualityComparer<string>.Default);
			foreach (LinkDescriptor linkDescriptor in relatedLinks)
			{
				List<LinkDescriptor> list = null;
				if (!dictionary.TryGetValue(linkDescriptor.SourceProperty, out list))
				{
					list = new List<LinkDescriptor>();
					dictionary.Add(linkDescriptor.SourceProperty, list);
				}
				list.Add(linkDescriptor);
			}
			ClientTypeAnnotation clientTypeAnnotation = null;
			foreach (KeyValuePair<string, List<LinkDescriptor>> keyValuePair in dictionary)
			{
				if (clientTypeAnnotation == null)
				{
					ClientEdmModel model = this.requestInfo.Model;
					clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entityDescriptor.Entity.GetType()));
				}
				bool isEntityCollection = clientTypeAnnotation.GetProperty(keyValuePair.Key, UndeclaredPropertyBehavior.ThrowException).IsEntityCollection;
				bool flag = false;
				foreach (LinkDescriptor linkDescriptor2 in keyValuePair.Value)
				{
					linkDescriptor2.ContentGeneratedForSave = true;
					ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo();
					odataNestedResourceInfo.Url = this.requestInfo.EntityTracker.GetEntityDescriptor(linkDescriptor2.Target).GetLatestEditLink();
					odataNestedResourceInfo.IsCollection = new bool?(isEntityCollection);
					odataNestedResourceInfo.Name = keyValuePair.Key;
					if (!flag)
					{
						odataWriter.WriteNestedResourceInfoStart(odataNestedResourceInfo);
						flag = true;
					}
					odataWriter.WriteNestedResourceInfoStart(odataNestedResourceInfo, linkDescriptor2.Source, linkDescriptor2.Target);
					odataWriter.WriteEntityReferenceLink(new ODataEntityReferenceLink
					{
						Url = odataNestedResourceInfo.Url
					}, linkDescriptor2.Source, linkDescriptor2.Target);
					odataWriter.WriteNestedResourceInfoEnd(odataNestedResourceInfo, linkDescriptor2.Source, linkDescriptor2.Target);
				}
				odataWriter.WriteNestedResourceInfoEnd();
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001F2C0 File Offset: 0x0001D4C0
		internal void WriteEntityReferenceLink(LinkDescriptor binding, ODataRequestMessageWrapper requestMessage)
		{
			using (ODataMessageWriter odataMessageWriter = Serializer.CreateMessageWriter(requestMessage, this.requestInfo, false))
			{
				EntityDescriptor entityDescriptor = this.requestInfo.EntityTracker.GetEntityDescriptor(binding.Target);
				Uri uri = entityDescriptor.GetLatestIdentity();
				if (uri == null)
				{
					uri = UriUtil.CreateUri("$" + entityDescriptor.ChangeOrder.ToString(CultureInfo.InvariantCulture), UriKind.Relative);
				}
				odataMessageWriter.WriteEntityReferenceLink(new ODataEntityReferenceLink
				{
					Url = uri
				});
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001F358 File Offset: 0x0001D558
		internal Uri WriteUriOperationParametersToUri(Uri requestUri, List<UriOperationParameter> operationParameters)
		{
			UriBuilder uriBuilder = new UriBuilder(requestUri);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(uriBuilder.Path);
			string text = uriBuilder.Path.Substring(uriBuilder.Path.LastIndexOf('/') + 1);
			StringBuilder stringBuilder2 = new StringBuilder();
			string text2 = UriUtil.UriToString(uriBuilder.Uri);
			if (!string.IsNullOrEmpty(uriBuilder.Query))
			{
				stringBuilder2.Append(uriBuilder.Query.Substring(1));
				stringBuilder2.Append('&');
			}
			if (!text.Contains(char.ToString('@')))
			{
				stringBuilder.Append('(');
			}
			else if (stringBuilder.ToString().EndsWith(char.ToString(')'), StringComparison.OrdinalIgnoreCase))
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
				stringBuilder.Append(',');
			}
			foreach (UriOperationParameter uriOperationParameter in operationParameters)
			{
				string text3 = uriOperationParameter.Name.Trim();
				if (text3.StartsWith(char.ToString('@'), StringComparison.OrdinalIgnoreCase) && !text2.Contains(text3))
				{
					throw new DataServiceRequestException(Strings.Serializer_UriDoesNotContainParameterAlias(uriOperationParameter.Name));
				}
				if (text3.StartsWith(char.ToString('@'), StringComparison.OrdinalIgnoreCase))
				{
					stringBuilder2.Append(text3);
					stringBuilder2.Append('=');
					stringBuilder2.Append(this.ConvertToEscapedUriValue(text3, uriOperationParameter.Value, false));
					stringBuilder2.Append('&');
				}
				string text4 = this.ConvertToEscapedUriValue(text3, uriOperationParameter.Value, false);
				if (!UriHelper.IsPrimitiveValue(text4))
				{
					stringBuilder.Append(text3);
					stringBuilder.Append('=');
					stringBuilder.Append("%40");
					stringBuilder.Append(text3);
					stringBuilder.Append(',');
					stringBuilder2.Append("%40");
					stringBuilder2.Append(text3);
					stringBuilder2.Append('=');
					stringBuilder2.Append(text4);
					stringBuilder2.Append('&');
				}
				else
				{
					stringBuilder.Append(text3);
					stringBuilder.Append('=');
					stringBuilder.Append(text4);
					stringBuilder.Append(',');
				}
			}
			if (stringBuilder.ToString().EndsWith(char.ToString(','), StringComparison.OrdinalIgnoreCase))
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			stringBuilder.Append(')');
			if (stringBuilder2.ToString().EndsWith(char.ToString('&'), StringComparison.OrdinalIgnoreCase))
			{
				stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
			}
			uriBuilder.Path = stringBuilder.ToString();
			uriBuilder.Query = stringBuilder2.ToString();
			return uriBuilder.Uri;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001F600 File Offset: 0x0001D800
		private void WriteCollectionValueInBodyOperationParameter(ODataParameterWriter parameterWriter, BodyOperationParameter operationParameter, IEdmCollectionType edmCollectionType)
		{
			ClientEdmModel model = this.requestInfo.Model;
			EdmTypeKind edmTypeKind = edmCollectionType.ElementType.TypeKind();
			if (edmTypeKind == EdmTypeKind.Entity || edmTypeKind == EdmTypeKind.Complex)
			{
				ODataWriter odataWriter = parameterWriter.CreateResourceSetWriter(operationParameter.Name);
				odataWriter.WriteStart(new ODataResourceSet());
				foreach (object obj in ((ICollection)operationParameter.Value))
				{
					if (obj == null)
					{
						if (edmTypeKind != EdmTypeKind.Complex)
						{
							throw new NotSupportedException(Strings.Serializer_NullCollectionParamterItemValue(operationParameter.Name));
						}
						odataWriter.WriteStart(null);
						odataWriter.WriteEnd();
					}
					else
					{
						IEdmType orCreateEdmType = model.GetOrCreateEdmType(obj.GetType());
						if (orCreateEdmType.TypeKind != EdmTypeKind.Entity && orCreateEdmType.TypeKind != EdmTypeKind.Complex)
						{
							throw new NotSupportedException(Strings.Serializer_InvalidCollectionParamterItemType(operationParameter.Name, orCreateEdmType.TypeKind));
						}
						ODataResourceWrapper odataResourceWrapper = this.CreateODataResourceFromEntityOperationParameter(model.GetClientTypeAnnotation(orCreateEdmType), obj);
						ODataWriterHelper.WriteResource(odataWriter, odataResourceWrapper);
					}
				}
				odataWriter.WriteEnd();
				odataWriter.Flush();
				return;
			}
			ODataCollectionWriter odataCollectionWriter = parameterWriter.CreateCollectionWriter(operationParameter.Name);
			ODataCollectionStart odataCollectionStart = new ODataCollectionStart();
			odataCollectionWriter.WriteStart(odataCollectionStart);
			foreach (object obj2 in ((ICollection)operationParameter.Value))
			{
				if (obj2 == null)
				{
					odataCollectionWriter.WriteItem(null);
				}
				else
				{
					IEdmType orCreateEdmType2 = model.GetOrCreateEdmType(obj2.GetType());
					EdmTypeKind typeKind = orCreateEdmType2.TypeKind;
					if (typeKind != EdmTypeKind.Primitive)
					{
						if (typeKind != EdmTypeKind.Enum)
						{
							throw new NotSupportedException(Strings.Serializer_InvalidCollectionParamterItemType(operationParameter.Name, orCreateEdmType2.TypeKind));
						}
						ODataEnumValue odataEnumValue = this.propertyConverter.CreateODataEnumValue(model.GetClientTypeAnnotation(orCreateEdmType2).ElementType, obj2, false);
						odataCollectionWriter.WriteItem(odataEnumValue);
					}
					else
					{
						object obj3 = ODataPropertyConverter.ConvertPrimitiveValueToRecognizedODataType(obj2, obj2.GetType());
						odataCollectionWriter.WriteItem(obj3);
					}
				}
			}
			odataCollectionWriter.WriteEnd();
			odataCollectionWriter.Flush();
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
		private static void WriteResourceSet(ODataWriterWrapper writer, ODataResourceSetWrapper resourceSetWrapper)
		{
			writer.WriteStart(resourceSetWrapper.ResourceSet);
			if (resourceSetWrapper.Resources != null)
			{
				foreach (ODataResourceWrapper odataResourceWrapper in resourceSetWrapper.Resources)
				{
					Serializer.WriteResource(writer, odataResourceWrapper);
				}
			}
			writer.WriteEnd();
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001F858 File Offset: 0x0001DA58
		private static void WriteResource(ODataWriterWrapper writer, ODataResourceWrapper resourceWrapper)
		{
			if (resourceWrapper.Resource == null)
			{
				writer.WriteStartResource(resourceWrapper.Resource);
			}
			else
			{
				writer.WriteStart(resourceWrapper.Resource, resourceWrapper.Instance);
			}
			if (resourceWrapper.NestedResourceInfoWrappers != null)
			{
				foreach (ODataNestedResourceInfoWrapper odataNestedResourceInfoWrapper in resourceWrapper.NestedResourceInfoWrappers)
				{
					Serializer.WriteNestedResourceInfo(writer, odataNestedResourceInfoWrapper);
				}
			}
			if (resourceWrapper.Resource == null)
			{
				writer.WriteEnd();
				return;
			}
			writer.WriteEnd(resourceWrapper.Resource, resourceWrapper.Instance);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001F8F8 File Offset: 0x0001DAF8
		private static void WriteNestedResourceInfo(ODataWriterWrapper writer, ODataNestedResourceInfoWrapper nestedResourceInfo)
		{
			writer.WriteNestedResourceInfoStart(nestedResourceInfo.NestedResourceInfo);
			if (nestedResourceInfo.NestedResourceOrResourceSet != null)
			{
				Serializer.WriteItem(writer, nestedResourceInfo.NestedResourceOrResourceSet);
			}
			writer.WriteNestedResourceInfoEnd();
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001F920 File Offset: 0x0001DB20
		private static void WriteItem(ODataWriterWrapper writer, ODataItemWrapper odataItemWrapper)
		{
			ODataResourceWrapper odataResourceWrapper = odataItemWrapper as ODataResourceWrapper;
			if (odataResourceWrapper != null)
			{
				Serializer.WriteResource(writer, odataResourceWrapper);
				return;
			}
			ODataResourceSetWrapper odataResourceSetWrapper = odataItemWrapper as ODataResourceSetWrapper;
			if (odataResourceSetWrapper != null)
			{
				Serializer.WriteResourceSet(writer, odataResourceSetWrapper);
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001F950 File Offset: 0x0001DB50
		private string ConvertToEscapedUriValue(string paramName, object value, bool useEntityReference = false)
		{
			bool flag = false;
			object obj = this.ConvertToODataValue(paramName, value, ref flag, useEntityReference);
			string text = ODataUriUtils.ConvertToUriLiteral(obj, CommonUtil.ConvertToODataVersion(this.requestInfo.MaxProtocolVersionAsVersion), null);
			if (flag)
			{
				return DataStringEscapeBuilder.EscapeDataString(text);
			}
			return Uri.EscapeDataString(text);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001F994 File Offset: 0x0001DB94
		private object ConvertToODataValue(string paramName, object value, ref bool needsSpecialEscaping, bool useEntityReference)
		{
			object obj = null;
			if (value == null)
			{
				needsSpecialEscaping = true;
			}
			else
			{
				if (!(value is ODataNullValue))
				{
					ClientEdmModel model = this.requestInfo.Model;
					IEdmType orCreateEdmType = model.GetOrCreateEdmType(value.GetType());
					ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(orCreateEdmType);
					switch (orCreateEdmType.TypeKind)
					{
					case EdmTypeKind.Primitive:
						if (value is DateTime)
						{
							obj = PlatformHelper.ConvertDateTimeToDateTimeOffset((DateTime)value);
						}
						else
						{
							obj = value;
						}
						needsSpecialEscaping = true;
						return obj;
					case EdmTypeKind.Entity:
					case EdmTypeKind.Complex:
						return this.ConvertToEntityValue(value, clientTypeAnnotation.ElementType, useEntityReference);
					case EdmTypeKind.Collection:
					{
						IEdmCollectionType edmCollectionType = orCreateEdmType as IEdmCollectionType;
						IEdmTypeReference elementType = edmCollectionType.ElementType;
						ClientTypeAnnotation clientTypeAnnotation2 = model.GetClientTypeAnnotation(elementType.Definition);
						return this.ConvertToCollectionValue(paramName, value, clientTypeAnnotation2, useEntityReference);
					}
					case EdmTypeKind.Enum:
					{
						string serverTypeName = this.requestInfo.GetServerTypeName(model.GetClientTypeAnnotation(orCreateEdmType));
						obj = new ODataEnumValue(ClientTypeUtil.GetEnumValuesString(value.ToString(), clientTypeAnnotation.ElementType), serverTypeName ?? clientTypeAnnotation.ElementTypeName);
						needsSpecialEscaping = true;
						return obj;
					}
					}
					throw new NotSupportedException(Strings.Serializer_InvalidParameterType(paramName, orCreateEdmType.TypeKind));
				}
				obj = value;
				needsSpecialEscaping = true;
			}
			return obj;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001FACC File Offset: 0x0001DCCC
		private object ConvertToCollectionValue(string paramName, object value, ClientTypeAnnotation itemTypeAnnotation, bool useEntityReference)
		{
			switch (itemTypeAnnotation.EdmType.TypeKind)
			{
			case EdmTypeKind.Primitive:
			case EdmTypeKind.Enum:
				return this.propertyConverter.CreateODataCollection(itemTypeAnnotation.ElementType, null, value, null, false, false);
			case EdmTypeKind.Entity:
			case EdmTypeKind.Complex:
				if (useEntityReference)
				{
					IEnumerable enumerable = value as IEnumerable;
					List<ODataEntityReferenceLink> list = (from object o in enumerable
						select new ODataEntityReferenceLink
						{
							Url = this.requestInfo.EntityTracker.GetEntityDescriptor(o).GetLatestIdentity()
						}).ToList<ODataEntityReferenceLink>();
					return new ODataEntityReferenceLinks
					{
						Links = list
					};
				}
				return this.propertyConverter.CreateODataEntries(itemTypeAnnotation.ElementType, value);
			}
			throw new NotSupportedException(Strings.Serializer_InvalidCollectionParamterItemType(paramName, itemTypeAnnotation.EdmType.TypeKind));
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001FB8C File Offset: 0x0001DD8C
		private object ConvertToEntityValue(object value, Type elementType, bool useEntityReference)
		{
			object obj;
			if (!useEntityReference)
			{
				obj = this.propertyConverter.CreateODataEntry(elementType, value, new ClientPropertyAnnotation[0]);
				ODataResource odataResource = (ODataResource)obj;
				if (odataResource.TypeAnnotation == null || string.IsNullOrEmpty(odataResource.TypeAnnotation.TypeName))
				{
					throw Error.InvalidOperation(Strings.DataServiceException_GeneralError);
				}
			}
			else
			{
				EntityDescriptor entityDescriptor = this.requestInfo.EntityTracker.GetEntityDescriptor(value);
				Uri latestIdentity = entityDescriptor.GetLatestIdentity();
				obj = new ODataEntityReferenceLink
				{
					Url = latestIdentity
				};
			}
			return obj;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001FC04 File Offset: 0x0001DE04
		private ODataResourceWrapper CreateODataResourceFromEntityOperationParameter(ClientTypeAnnotation clientTypeAnnotation, object parameterValue)
		{
			ClientPropertyAnnotation[] array = new ClientPropertyAnnotation[0];
			if (this.sendOption == EntityParameterSendOption.SendOnlySetProperties)
			{
				try
				{
					EntityDescriptor descripter = this.requestInfo.Context.EntityTracker.GetEntityDescriptor(parameterValue);
					array = (from p in clientTypeAnnotation.PropertiesToSerialize()
						where descripter.PropertiesToSerialize.Contains(p.PropertyName)
						select p).ToArray<ClientPropertyAnnotation>();
				}
				catch (InvalidOperationException)
				{
					throw Error.InvalidOperation(Strings.Context_MustBeUsedWith("EntityParameterSendOption.SendOnlySetProperties", "DataServiceCollection"));
				}
			}
			return this.propertyConverter.CreateODataResourceWrapper(clientTypeAnnotation.ElementType, parameterValue, array);
		}

		// Token: 0x0400035A RID: 858
		private readonly RequestInfo requestInfo;

		// Token: 0x0400035B RID: 859
		private readonly ODataPropertyConverter propertyConverter;

		// Token: 0x0400035C RID: 860
		private readonly SaveChangesOptions options;

		// Token: 0x0400035D RID: 861
		private readonly EntityParameterSendOption sendOption;
	}
}
