using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Edm;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.Data.Contracts.Utils
{
	// Token: 0x02000008 RID: 8
	public static class CsdlParserUtil
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002148 File Offset: 0x00000348
		public static List<string> ParseDaxExtensionFunctionsNode(XElement daxExtensionFunctionsElement, XName daxExtensionFunctionElementName)
		{
			if (daxExtensionFunctionsElement != null)
			{
				List<string> list = new List<string>();
				IEnumerable<XElement> enumerable = daxExtensionFunctionsElement.Elements(daxExtensionFunctionElementName);
				if (enumerable != null)
				{
					foreach (XElement xelement in enumerable)
					{
						if (xelement.Value == CsdlParserUtil.defaultSupportValue)
						{
							XAttribute xattribute = xelement.Attribute(CsdlParserUtil.NameAttr);
							if (xattribute != null)
							{
								string value = xattribute.Value;
								if (!string.IsNullOrWhiteSpace(value))
								{
									list.Add(value);
								}
							}
						}
					}
				}
				return list;
			}
			return null;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021E0 File Offset: 0x000003E0
		public static ParameterMetadata ParseParameterMetadata(ExtendedProperty extendedProperty, ITracer tracer = null)
		{
			return CsdlParserUtil.ParseJsonProperty<ParameterMetadata>(extendedProperty, tracer);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021EC File Offset: 0x000003EC
		public static T ParseJsonProperty<T>(ExtendedProperty extendedProperty, ITracer tracer = null) where T : class
		{
			Type typeFromHandle = typeof(T);
			T t = default(T);
			try
			{
				t = new DataContractJsonSerializer(typeFromHandle).FromJsonString(extendedProperty.Value);
			}
			catch (SerializationException ex)
			{
				if (tracer != null)
				{
					tracer.TraceError(StringUtil.FormatInvariant("{0} occurred when parsing JSON extended property to type {1}", ex.Message.MarkAsCustomerContent(), typeFromHandle.Name));
				}
			}
			return t;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002258 File Offset: 0x00000458
		public static ParameterMetadata GetParameterMetadataFromExtendedProperty(XElement extensionElem)
		{
			if (extensionElem == null)
			{
				return null;
			}
			List<ExtendedProperty> list = (from e in EdmExtensions.GetExtendedPropertyList(extensionElem)
				where e.Name.Equals(EdmConstants.ParameterMetadata.LocalName)
				select e).ToList<ExtendedProperty>();
			if (list.IsEmpty<ExtendedProperty>())
			{
				return null;
			}
			return CsdlParserUtil.ParseParameterMetadata(list.Single("There should be only one ParameterMetadata information", new string[0]), null);
		}

		// Token: 0x04000035 RID: 53
		private static readonly XName NameAttr = "Name";

		// Token: 0x04000036 RID: 54
		private static readonly string defaultSupportValue = "1";
	}
}
