using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000252 RID: 594
	public static class ODataUtils
	{
		// Token: 0x0600125C RID: 4700 RVA: 0x00045756 File Offset: 0x00043956
		public static ODataFormat SetHeadersForPayload(ODataMessageWriter messageWriter, ODataPayloadKind payloadKind)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriter>(messageWriter, "messageWriter");
			if (payloadKind == ODataPayloadKind.Unsupported)
			{
				throw new ArgumentException(Strings.ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind(payloadKind), "payloadKind");
			}
			return messageWriter.SetHeaders(payloadKind);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00045788 File Offset: 0x00043988
		public static ODataFormat GetReadFormat(ODataMessageReader messageReader)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReader>(messageReader, "messageReader");
			return messageReader.GetFormat();
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0004579B File Offset: 0x0004399B
		public static void LoadODataAnnotations(this IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			model.LoadODataAnnotations(100);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x000457B0 File Offset: 0x000439B0
		public static void LoadODataAnnotations(this IEdmModel model, int maxEntityPropertyMappingsPerType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			foreach (IEdmEntityType edmEntityType in model.EntityTypes())
			{
				model.LoadODataAnnotations(edmEntityType, maxEntityPropertyMappingsPerType);
			}
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0004580C File Offset: 0x00043A0C
		public static void LoadODataAnnotations(this IEdmModel model, IEdmEntityType entityType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			model.LoadODataAnnotations(entityType, 100);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0004582D File Offset: 0x00043A2D
		public static void LoadODataAnnotations(this IEdmModel model, IEdmEntityType entityType, int maxEntityPropertyMappingsPerType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			model.ClearInMemoryEpmAnnotations(entityType);
			model.EnsureEpmCache(entityType, maxEntityPropertyMappingsPerType);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00045858 File Offset: 0x00043A58
		public static void SaveODataAnnotations(this IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (!model.IsUserModel())
			{
				throw new ODataException(Strings.ODataUtils_CannotSaveAnnotationsToBuiltInModel);
			}
			foreach (IEdmEntityType edmEntityType in model.EntityTypes())
			{
				ODataUtils.SaveODataAnnotationsImplementation(model, edmEntityType);
			}
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000458C4 File Offset: 0x00043AC4
		public static void SaveODataAnnotations(this IEdmModel model, IEdmEntityType entityType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			ODataUtils.SaveODataAnnotationsImplementation(model, entityType);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x000458E4 File Offset: 0x00043AE4
		public static bool HasDefaultStream(this IEdmModel model, IEdmEntityType entityType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			bool flag;
			return ODataUtils.TryGetBooleanAnnotation(model, entityType, "HasStream", true, out flag) && flag;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004591B File Offset: 0x00043B1B
		public static void SetHasDefaultStream(this IEdmModel model, IEdmEntityType entityType, bool hasStream)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			ODataUtils.SetBooleanAnnotation(model, entityType, "HasStream", hasStream);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00045940 File Offset: 0x00043B40
		public static bool IsDefaultEntityContainer(this IEdmModel model, IEdmEntityContainer entityContainer)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityContainer>(entityContainer, "entityContainer");
			bool flag;
			return ODataUtils.TryGetBooleanAnnotation(model, entityContainer, "IsDefaultEntityContainer", out flag) && flag;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00045976 File Offset: 0x00043B76
		public static void SetIsDefaultEntityContainer(this IEdmModel model, IEdmEntityContainer entityContainer, bool isDefaultContainer)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityContainer>(entityContainer, "entityContainer");
			ODataUtils.SetBooleanAnnotation(model, entityContainer, "IsDefaultEntityContainer", isDefaultContainer);
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0004599C File Offset: 0x00043B9C
		public static string GetMimeType(this IEdmModel model, IEdmElement annotatable)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmElement>(annotatable, "annotatable");
			string text;
			if (!model.TryGetODataAnnotation(annotatable, "MimeType", out text))
			{
				return null;
			}
			if (text == null)
			{
				throw new ODataException(Strings.ODataUtils_NullValueForMimeTypeAnnotation);
			}
			return text;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000459E0 File Offset: 0x00043BE0
		public static void SetMimeType(this IEdmModel model, IEdmElement annotatable, string mimeType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmElement>(annotatable, "annotatable");
			model.SetODataAnnotation(annotatable, "MimeType", mimeType);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00045A08 File Offset: 0x00043C08
		public static string GetHttpMethod(this IEdmModel model, IEdmElement annotatable)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmElement>(annotatable, "annotatable");
			string text;
			if (!model.TryGetODataAnnotation(annotatable, "HttpMethod", out text))
			{
				return null;
			}
			if (text == null)
			{
				throw new ODataException(Strings.ODataUtils_NullValueForHttpMethodAnnotation);
			}
			return text;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00045A4C File Offset: 0x00043C4C
		public static void SetHttpMethod(this IEdmModel model, IEdmElement annotatable, string httpMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmElement>(annotatable, "annotatable");
			model.SetODataAnnotation(annotatable, "HttpMethod", httpMethod);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00045A74 File Offset: 0x00043C74
		public static bool IsAlwaysBindable(this IEdmModel model, IEdmFunctionImport functionImport)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmFunctionImport>(functionImport, "functionImport");
			bool flag;
			if (!ODataUtils.TryGetBooleanAnnotation(model, functionImport, "IsAlwaysBindable", out flag))
			{
				return false;
			}
			if (!functionImport.IsBindable && flag)
			{
				throw new ODataException(Strings.ODataUtils_UnexpectedIsAlwaysBindableAnnotationInANonBindableFunctionImport);
			}
			return flag;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00045AC0 File Offset: 0x00043CC0
		public static void SetIsAlwaysBindable(this IEdmModel model, IEdmFunctionImport functionImport, bool isAlwaysBindable)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmFunctionImport>(functionImport, "functionImport");
			if (!functionImport.IsBindable && isAlwaysBindable)
			{
				throw new ODataException(Strings.ODataUtils_IsAlwaysBindableAnnotationSetForANonBindableFunctionImport);
			}
			ODataUtils.SetBooleanAnnotation(model, functionImport, "IsAlwaysBindable", isAlwaysBindable);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00045AFC File Offset: 0x00043CFC
		public static ODataNullValueBehaviorKind NullValueReadBehaviorKind(this IEdmModel model, IEdmProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			ODataEdmPropertyAnnotation annotationValue = model.GetAnnotationValue(property);
			if (annotationValue != null)
			{
				return annotationValue.NullValueReadBehaviorKind;
			}
			return ODataNullValueBehaviorKind.Default;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00045B34 File Offset: 0x00043D34
		public static void SetNullValueReaderBehavior(this IEdmModel model, IEdmProperty property, ODataNullValueBehaviorKind nullValueReadBehaviorKind)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmProperty>(property, "property");
			ODataEdmPropertyAnnotation odataEdmPropertyAnnotation = model.GetAnnotationValue(property);
			if (odataEdmPropertyAnnotation == null)
			{
				if (nullValueReadBehaviorKind != ODataNullValueBehaviorKind.Default)
				{
					odataEdmPropertyAnnotation = new ODataEdmPropertyAnnotation
					{
						NullValueReadBehaviorKind = nullValueReadBehaviorKind
					};
					model.SetAnnotationValue(property, odataEdmPropertyAnnotation);
					return;
				}
			}
			else
			{
				odataEdmPropertyAnnotation.NullValueReadBehaviorKind = nullValueReadBehaviorKind;
			}
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00045B84 File Offset: 0x00043D84
		public static string ODataVersionToString(ODataVersion version)
		{
			switch (version)
			{
			case ODataVersion.V1:
				return "1.0";
			case ODataVersion.V2:
				return "2.0";
			case ODataVersion.V3:
				return "3.0";
			default:
				throw new ODataException(Strings.ODataUtils_UnsupportedVersionNumber);
			}
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00045BC4 File Offset: 0x00043DC4
		public static ODataVersion StringToODataVersion(string version)
		{
			string text = version;
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(version, "version");
			int num = text.IndexOf(';');
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			string text2;
			if ((text2 = text.Trim()) != null)
			{
				if (text2 == "1.0")
				{
					return ODataVersion.V1;
				}
				if (text2 == "2.0")
				{
					return ODataVersion.V2;
				}
				if (text2 == "3.0")
				{
					return ODataVersion.V3;
				}
			}
			throw new ODataException(Strings.ODataUtils_UnsupportedVersionHeader(version));
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00045C38 File Offset: 0x00043E38
		public static Func<string, bool> CreateAnnotationFilter(string annotationFilter)
		{
			AnnotationFilter annotationFilter2 = AnnotationFilter.Create(annotationFilter);
			return new Func<string, bool>(annotationFilter2.Matches);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00045C5C File Offset: 0x00043E5C
		private static void SaveODataAnnotationsImplementation(IEdmModel model, IEdmEntityType entityType)
		{
			ODataEntityPropertyMappingCache odataEntityPropertyMappingCache = model.EnsureEpmCache(entityType, int.MaxValue);
			if (odataEntityPropertyMappingCache != null)
			{
				model.SaveEpmAnnotations(entityType, odataEntityPropertyMappingCache.MappingsForInheritedProperties, false, false);
				IEnumerable<IEdmProperty> declaredProperties = entityType.DeclaredProperties;
				if (declaredProperties != null)
				{
					foreach (IEdmProperty edmProperty in declaredProperties)
					{
						if (edmProperty.Type.IsODataPrimitiveTypeKind() || edmProperty.Type.IsODataComplexTypeKind())
						{
							model.SaveEpmAnnotationsForProperty(edmProperty, odataEntityPropertyMappingCache);
						}
						else if (edmProperty.Type.IsNonEntityCollectionType())
						{
							model.SaveEpmAnnotationsForProperty(edmProperty, odataEntityPropertyMappingCache);
						}
					}
				}
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00045D00 File Offset: 0x00043F00
		private static bool TryGetBooleanAnnotation(IEdmModel model, IEdmStructuredType structuredType, string annotationLocalName, bool recursive, out bool boolValue)
		{
			string text = null;
			bool flag;
			do
			{
				flag = model.TryGetODataAnnotation(structuredType, annotationLocalName, out text);
				if (flag)
				{
					break;
				}
				structuredType = structuredType.BaseType;
			}
			while (recursive && structuredType != null);
			if (!flag)
			{
				boolValue = false;
				return false;
			}
			boolValue = XmlConvert.ToBoolean(text);
			return true;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00045D40 File Offset: 0x00043F40
		private static bool TryGetBooleanAnnotation(IEdmModel model, IEdmElement annotatable, string annotationLocalName, out bool boolValue)
		{
			string text;
			if (model.TryGetODataAnnotation(annotatable, annotationLocalName, out text))
			{
				boolValue = XmlConvert.ToBoolean(text);
				return true;
			}
			boolValue = false;
			return false;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00045D67 File Offset: 0x00043F67
		private static void SetBooleanAnnotation(IEdmModel model, IEdmElement annotatable, string annotationLocalName, bool boolValue)
		{
			model.SetODataAnnotation(annotatable, annotationLocalName, boolValue ? "true" : null);
		}

		// Token: 0x040006F5 RID: 1781
		private const string Version1NumberString = "1.0";

		// Token: 0x040006F6 RID: 1782
		private const string Version2NumberString = "2.0";

		// Token: 0x040006F7 RID: 1783
		private const string Version3NumberString = "3.0";
	}
}
