using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020001A4 RID: 420
	public static class ODataUtils
	{
		// Token: 0x06000FBB RID: 4027 RVA: 0x00035E82 File Offset: 0x00034082
		public static ODataFormat SetHeadersForPayload(ODataMessageWriter messageWriter, ODataPayloadKind payloadKind)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriter>(messageWriter, "messageWriter");
			if (payloadKind == ODataPayloadKind.Unsupported)
			{
				throw new ArgumentException(Strings.ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind(payloadKind), "payloadKind");
			}
			return messageWriter.SetHeaders(payloadKind);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00035EB4 File Offset: 0x000340B4
		public static ODataFormat GetReadFormat(ODataMessageReader messageReader)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReader>(messageReader, "messageReader");
			return messageReader.GetFormat();
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00035EC8 File Offset: 0x000340C8
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

		// Token: 0x06000FBE RID: 4030 RVA: 0x00035F00 File Offset: 0x00034100
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

		// Token: 0x06000FBF RID: 4031 RVA: 0x00035F50 File Offset: 0x00034150
		public static string ODataVersionToString(ODataVersion version)
		{
			if (version == ODataVersion.V4)
			{
				return "4.0";
			}
			throw new ODataException(Strings.ODataUtils_UnsupportedVersionNumber);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00035F74 File Offset: 0x00034174
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
			if ((text2 = text.Trim()) != null && text2 == "4.0")
			{
				return ODataVersion.V4;
			}
			throw new ODataException(Strings.ODataUtils_UnsupportedVersionHeader(version));
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00035FC8 File Offset: 0x000341C8
		public static Func<string, bool> CreateAnnotationFilter(string annotationFilter)
		{
			AnnotationFilter annotationFilter2 = AnnotationFilter.Create(annotationFilter);
			return new Func<string, bool>(annotationFilter2.Matches);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000360B4 File Offset: 0x000342B4
		public static ODataServiceDocument GenerateServiceDocument(this IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (model.EntityContainer == null)
			{
				throw new ODataException(Strings.ODataUtils_ModelDoesNotHaveContainer);
			}
			ODataServiceDocument odataServiceDocument = new ODataServiceDocument();
			odataServiceDocument.EntitySets = Enumerable.ToList<ODataEntitySetInfo>(Enumerable.Select<IEdmEntitySet, ODataEntitySetInfo>(model.EntityContainer.EntitySets(), (IEdmEntitySet entitySet) => new ODataEntitySetInfo
			{
				Name = entitySet.Name,
				Title = entitySet.Name,
				Url = new Uri(entitySet.Name, 0)
			}));
			odataServiceDocument.Singletons = Enumerable.ToList<ODataSingletonInfo>(Enumerable.Select<IEdmSingleton, ODataSingletonInfo>(model.EntityContainer.Singletons(), (IEdmSingleton singleton) => new ODataSingletonInfo
			{
				Name = singleton.Name,
				Title = singleton.Name,
				Url = new Uri(singleton.Name, 0)
			}));
			odataServiceDocument.FunctionImports = Enumerable.ToList<ODataFunctionImportInfo>(Enumerable.Select<IEdmFunctionImport, ODataFunctionImportInfo>(Enumerable.Where<IEdmFunctionImport>(Enumerable.OfType<IEdmFunctionImport>(model.EntityContainer.OperationImports()), (IEdmFunctionImport functionImport) => functionImport.IncludeInServiceDocument), (IEdmFunctionImport functionImport) => new ODataFunctionImportInfo
			{
				Name = functionImport.Name,
				Title = functionImport.Name,
				Url = new Uri(functionImport.Name, 0)
			}));
			return odataServiceDocument;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000361B8 File Offset: 0x000343B8
		public static string AppendDefaultHeaderValue(string headerName, string headerValue)
		{
			if (string.CompareOrdinal(headerName, "Content-Type") != 0)
			{
				return headerValue;
			}
			if (headerValue == null)
			{
				return null;
			}
			IList<KeyValuePair<ODataMediaType, string>> list = HttpUtils.MediaTypesFromString(headerValue);
			ODataMediaType key = Enumerable.Single<KeyValuePair<ODataMediaType, string>>(list).Key;
			Encoding encodingFromCharsetName = HttpUtils.GetEncodingFromCharsetName(Enumerable.Single<KeyValuePair<ODataMediaType, string>>(list).Value);
			if (string.CompareOrdinal(key.FullTypeName, "application/json") != 0)
			{
				return headerValue;
			}
			List<KeyValuePair<string, string>> list2 = new List<KeyValuePair<string, string>>();
			ODataMediaType odataMediaType = new ODataMediaType(key.Type, key.SubType, list2);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (key.Parameters != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in key.Parameters)
				{
					list2.Add(keyValuePair);
					if (string.CompareOrdinal(keyValuePair.Key, "odata.metadata") == 0)
					{
						flag = true;
					}
					if (string.CompareOrdinal(keyValuePair.Key, "odata.streaming") == 0)
					{
						flag2 = true;
					}
					if (string.CompareOrdinal(keyValuePair.Key, "IEEE754Compatible") == 0)
					{
						flag3 = true;
					}
				}
			}
			if (!flag)
			{
				list2.Add(new KeyValuePair<string, string>("odata.metadata", "minimal"));
			}
			if (!flag2)
			{
				list2.Add(new KeyValuePair<string, string>("odata.streaming", "true"));
			}
			if (!flag3)
			{
				list2.Add(new KeyValuePair<string, string>("IEEE754Compatible", "false"));
			}
			return odataMediaType.ToText(encodingFromCharsetName);
		}

		// Token: 0x040006EC RID: 1772
		private const string Version4NumberString = "4.0";
	}
}
