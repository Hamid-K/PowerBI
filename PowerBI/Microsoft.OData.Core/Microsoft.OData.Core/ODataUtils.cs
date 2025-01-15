using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000BE RID: 190
	public static class ODataUtils
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x000139C9 File Offset: 0x00011BC9
		public static ODataFormat SetHeadersForPayload(ODataMessageWriter messageWriter, ODataPayloadKind payloadKind)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriter>(messageWriter, "messageWriter");
			if (payloadKind == ODataPayloadKind.Unsupported)
			{
				throw new ArgumentException(Strings.ODataMessageWriter_CannotSetHeadersWithInvalidPayloadKind(payloadKind), "payloadKind");
			}
			return messageWriter.SetHeaders(payloadKind);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000139FC File Offset: 0x00011BFC
		public static ODataFormat GetReadFormat(ODataMessageReader messageReader)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReader>(messageReader, "messageReader");
			return messageReader.GetFormat();
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00013A10 File Offset: 0x00011C10
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

		// Token: 0x06000862 RID: 2146 RVA: 0x00013A48 File Offset: 0x00011C48
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

		// Token: 0x06000863 RID: 2147 RVA: 0x00013A98 File Offset: 0x00011C98
		public static string ODataVersionToString(ODataVersion version)
		{
			if (version == ODataVersion.V4)
			{
				return "4.0";
			}
			if (version != ODataVersion.V401)
			{
				throw new ODataException(Strings.ODataUtils_UnsupportedVersionNumber);
			}
			return "4.01";
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00013ABC File Offset: 0x00011CBC
		public static ODataVersion StringToODataVersion(string version)
		{
			string text = version;
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(version, "version");
			int num = text.IndexOf(';');
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			string text2 = text.Trim();
			if (text2 == "4.0")
			{
				return ODataVersion.V4;
			}
			if (!(text2 == "4.01"))
			{
				throw new ODataException(Strings.ODataUtils_UnsupportedVersionHeader(version));
			}
			return ODataVersion.V401;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00013B20 File Offset: 0x00011D20
		public static Func<string, bool> CreateAnnotationFilter(string annotationFilter)
		{
			AnnotationFilter annotationFilter2 = AnnotationFilter.Create(annotationFilter);
			return new Func<string, bool>(annotationFilter2.Matches);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00013B44 File Offset: 0x00011D44
		public static ODataServiceDocument GenerateServiceDocument(this IEdmModel model)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			if (model.EntityContainer == null)
			{
				throw new ODataException(Strings.ODataUtils_ModelDoesNotHaveContainer);
			}
			ODataServiceDocument odataServiceDocument = new ODataServiceDocument();
			odataServiceDocument.EntitySets = (from entitySet in model.EntityContainer.EntitySets()
				select new ODataEntitySetInfo
				{
					Name = entitySet.Name,
					Title = entitySet.Name,
					Url = new Uri(entitySet.Name, UriKind.RelativeOrAbsolute)
				}).ToList<ODataEntitySetInfo>();
			odataServiceDocument.Singletons = (from singleton in model.EntityContainer.Singletons()
				select new ODataSingletonInfo
				{
					Name = singleton.Name,
					Title = singleton.Name,
					Url = new Uri(singleton.Name, UriKind.RelativeOrAbsolute)
				}).ToList<ODataSingletonInfo>();
			odataServiceDocument.FunctionImports = (from functionImport in model.EntityContainer.OperationImports().OfType<IEdmFunctionImport>()
				where functionImport.IncludeInServiceDocument && !functionImport.Function.Parameters.Any<IEdmOperationParameter>()
				select new ODataFunctionImportInfo
				{
					Name = functionImport.Name,
					Title = functionImport.Name,
					Url = new Uri(functionImport.Name, UriKind.RelativeOrAbsolute)
				}).ToList<ODataFunctionImportInfo>();
			return odataServiceDocument;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00013C4E File Offset: 0x00011E4E
		public static string AppendDefaultHeaderValue(string headerName, string headerValue)
		{
			return ODataUtils.AppendDefaultHeaderValue(headerName, headerValue, ODataVersion.V4);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00013C58 File Offset: 0x00011E58
		public static string AppendDefaultHeaderValue(string headerName, string headerValue, ODataVersion version)
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
			ODataMediaType key = list.Single<KeyValuePair<ODataMediaType, string>>().Key;
			Encoding encodingFromCharsetName = HttpUtils.GetEncodingFromCharsetName(list.Single<KeyValuePair<ODataMediaType, string>>().Value);
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
					if (HttpUtils.IsMetadataParameter(keyValuePair.Key))
					{
						flag = true;
					}
					if (HttpUtils.IsStreamingParameter(keyValuePair.Key))
					{
						flag2 = true;
					}
					if (string.Compare(keyValuePair.Key, "IEEE754Compatible", StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag3 = true;
					}
				}
			}
			if (!flag)
			{
				list2.Add(new KeyValuePair<string, string>((version < ODataVersion.V401) ? "odata.metadata" : "metadata", "minimal"));
			}
			if (!flag2)
			{
				list2.Add(new KeyValuePair<string, string>((version < ODataVersion.V401) ? "odata.streaming" : "streaming", "true"));
			}
			if (!flag3)
			{
				list2.Add(new KeyValuePair<string, string>("IEEE754Compatible", "false"));
			}
			return odataMediaType.ToText(encodingFromCharsetName);
		}

		// Token: 0x0400032D RID: 813
		private const string Version4NumberString = "4.0";

		// Token: 0x0400032E RID: 814
		private const string Version401NumberString = "4.01";
	}
}
