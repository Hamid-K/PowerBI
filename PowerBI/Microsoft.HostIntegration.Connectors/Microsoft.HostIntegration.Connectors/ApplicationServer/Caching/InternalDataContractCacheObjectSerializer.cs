using System;
using System.IO;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000294 RID: 660
	internal class InternalDataContractCacheObjectSerializer : IDataCacheObjectSerializer
	{
		// Token: 0x06001821 RID: 6177 RVA: 0x00003D71 File Offset: 0x00001F71
		public void Serialize(Stream stream, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00049054 File Offset: 0x00047254
		public object Deserialize(Stream stream)
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(object), DataContractKnownTypes.KnownTypes);
			object obj;
			try
			{
				obj = dataContractSerializer.ReadObject(stream);
			}
			catch (InvalidCastException)
			{
				throw new SerializationException();
			}
			return obj;
		}
	}
}
