using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Microsoft.Cloud.Platform.RequestProtection
{
	// Token: 0x0200045C RID: 1116
	public static class HttpRequestNameValueCollectionExtensions
	{
		// Token: 0x060022EA RID: 8938 RVA: 0x0007ED6F File Offset: 0x0007CF6F
		public static Dictionary<string, IEnumerable<string>> ToDictionaryRepresentation(this NameValueCollection nameValueCollection)
		{
			return nameValueCollection.ToDictionaryRepresentation(NameValueCollectionConversionOptions.None);
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0007ED78 File Offset: 0x0007CF78
		public static Dictionary<string, IEnumerable<string>> ToDictionaryRepresentation(this NameValueCollection nameValueCollection, NameValueCollectionConversionOptions options)
		{
			bool includeKeylessValues = options.HasFlag(NameValueCollectionConversionOptions.IncludeKeyLessValues);
			return nameValueCollection.AllKeys.Where((string key) => (includeKeylessValues || !string.IsNullOrEmpty(key)) && nameValueCollection.Get(key) != null).ToDictionary((string k) => k ?? string.Empty, (string k) => nameValueCollection.Get(k).Split(new char[] { ',' }), StringComparer.OrdinalIgnoreCase);
		}
	}
}
