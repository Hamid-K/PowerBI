using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200004E RID: 78
	internal interface ISystemResourceManager
	{
		// Token: 0x0600038F RID: 911
		SystemResource Install(Func<Stream> metadataStream, IDictionary<string, Func<Stream>> contentStreams, byte[] packageBytes, string packageName, string typeName, Func<string, ISystemResourcePackageContentValidator> validator, Func<string, string> contentTypeMapper);

		// Token: 0x06000390 RID: 912
		IEnumerable<SystemResource> LoadAll();

		// Token: 0x06000391 RID: 913
		bool TryLoadByTypeName(string typeName, out SystemResource systemResource);

		// Token: 0x06000392 RID: 914
		bool TryDelete(string typeName);

		// Token: 0x06000393 RID: 915
		bool TryLoadContentItem(string typeName, string key, out byte[] bytes);
	}
}
