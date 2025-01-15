using System;
using System.IO;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001DC RID: 476
	public interface IDataCacheObjectSerializer
	{
		// Token: 0x06000F8B RID: 3979
		void Serialize(Stream stream, object value);

		// Token: 0x06000F8C RID: 3980
		object Deserialize(Stream stream);
	}
}
