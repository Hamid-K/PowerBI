using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNet.OData.Formatter;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000008 RID: 8
	internal static class ODataMessageWrapperHelper
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000027E8 File Offset: 0x000009E8
		internal static ODataMessageWrapper Create(Stream stream, HttpContentHeaders headers)
		{
			return ODataMessageWrapperHelper.Create(stream, headers, null);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027F2 File Offset: 0x000009F2
		internal static ODataMessageWrapper Create(Stream stream, HttpContentHeaders headers, IServiceProvider container)
		{
			return ODataMessageWrapperHelper.Create(stream, headers, null, container);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027FD File Offset: 0x000009FD
		internal static ODataMessageWrapper Create(Stream stream, HttpContentHeaders headers, IDictionary<string, string> contentIdMapping, IServiceProvider container)
		{
			ODataMessageWrapper odataMessageWrapper = ODataMessageWrapperHelper.Create(stream, headers, contentIdMapping);
			odataMessageWrapper.Container = container;
			return odataMessageWrapper;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002810 File Offset: 0x00000A10
		internal static ODataMessageWrapper Create(Stream stream, HttpContentHeaders headers, IDictionary<string, string> contentIdMapping)
		{
			return new ODataMessageWrapper(stream, headers.ToDictionary((KeyValuePair<string, IEnumerable<string>> kvp) => kvp.Key, (KeyValuePair<string, IEnumerable<string>> kvp) => string.Join(";", kvp.Value)), contentIdMapping);
		}
	}
}
