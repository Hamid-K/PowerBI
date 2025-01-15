using System;
using System.IO;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000299 RID: 665
	internal class SessionStoreProviderDataSerializer : IDataCacheObjectSerializer
	{
		// Token: 0x0600183D RID: 6205 RVA: 0x00049AA8 File Offset: 0x00047CA8
		public void Serialize(Stream stream, object value)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			SessionStoreProviderData sessionStoreProviderData = value as SessionStoreProviderData;
			if (sessionStoreProviderData == null)
			{
				throw new SerializationException();
			}
			sessionStoreProviderData.WriteObject(stream);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00049AE8 File Offset: 0x00047CE8
		public object Deserialize(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			SessionStoreProviderData sessionStoreProviderData = new SessionStoreProviderData();
			sessionStoreProviderData.ReadObject(stream);
			return sessionStoreProviderData;
		}
	}
}
