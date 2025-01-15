using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Model;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x0200004D RID: 77
	internal static class CatalogItemJsonContentExtensions
	{
		// Token: 0x0600029A RID: 666 RVA: 0x00011C44 File Offset: 0x0000FE44
		internal static string GetJsonStringForProperties(this CatalogItem item, IEnumerable<string> includedProperties)
		{
			Dictionary<Type, IEnumerable<string>> dictionary = new Dictionary<Type, IEnumerable<string>> { 
			{
				typeof(object),
				includedProperties
			} };
			return item.GetJsonStringForProperties(dictionary);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00011C70 File Offset: 0x0000FE70
		internal static string GetJsonStringForProperties(this CatalogItem item, IDictionary<Type, IEnumerable<string>> includedPropertiesMap = null)
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

		// Token: 0x0600029C RID: 668 RVA: 0x00011CA0 File Offset: 0x0000FEA0
		internal static void PopulateFromJsonBinaryContent(this CatalogItem item, byte[] content, IEnumerable<string> includedProperties)
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

		// Token: 0x0600029D RID: 669 RVA: 0x00011CDC File Offset: 0x0000FEDC
		internal static void PopulateFromJsonBinaryContent(this CatalogItem catalogItem, byte[] content, IDictionary<Type, IEnumerable<string>> includedPropertiesMap = null)
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
