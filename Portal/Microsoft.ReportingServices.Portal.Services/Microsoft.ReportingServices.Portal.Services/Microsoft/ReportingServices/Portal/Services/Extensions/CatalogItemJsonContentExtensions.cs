using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Portal.Interfaces.Models;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.Services.Extensions
{
	// Token: 0x0200005D RID: 93
	internal static class CatalogItemJsonContentExtensions
	{
		// Token: 0x060002EE RID: 750 RVA: 0x00012AC0 File Offset: 0x00010CC0
		internal static string GetJsonStringForProperties(this ICatalogItem item, IEnumerable<string> includedProperties)
		{
			Dictionary<Type, IEnumerable<string>> dictionary = new Dictionary<Type, IEnumerable<string>> { 
			{
				typeof(object),
				includedProperties
			} };
			return item.GetJsonStringForProperties(dictionary);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00012AEC File Offset: 0x00010CEC
		internal static string GetJsonStringForProperties(this ICatalogItem item, IDictionary<Type, IEnumerable<string>> includedPropertiesMap = null)
		{
			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			};
			if (includedPropertiesMap != null)
			{
				jsonSerializerSettings.ContractResolver = new IncludedPropertiesContractResolver(includedPropertiesMap);
			}
			return JsonConvert.SerializeObject(item, jsonSerializerSettings);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00012B1C File Offset: 0x00010D1C
		internal static void PopulateFromJsonBinaryContent(this ICatalogItem item, byte[] content, IEnumerable<string> includedProperties)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			Dictionary<Type, IEnumerable<string>> dictionary = new Dictionary<Type, IEnumerable<string>> { 
			{
				typeof(object),
				includedProperties
			} };
			item.PopulateFromJsonBinaryContent(content, dictionary);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00012B58 File Offset: 0x00010D58
		internal static void PopulateFromJsonBinaryContent(this ICatalogItem catalogItem, byte[] content, IDictionary<Type, IEnumerable<string>> includedPropertiesMap = null)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			try
			{
				string @string = Encoding.UTF8.GetString(content);
				if (includedPropertiesMap != null)
				{
					JsonConvert.PopulateObject(@string, catalogItem, new JsonSerializerSettings
					{
						ContractResolver = new IncludedPropertiesContractResolver(includedPropertiesMap)
					});
				}
				else
				{
					JsonConvert.PopulateObject(@string, catalogItem);
				}
			}
			catch (Exception)
			{
				throw new CatalogItemContentInvalidException(catalogItem.Path);
			}
		}
	}
}
