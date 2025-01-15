using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Xml;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000297 RID: 663
	internal class NetDataContractCacheObjectSerializer : IDataCacheObjectSerializer
	{
		// Token: 0x0600182B RID: 6187 RVA: 0x0004970C File Offset: 0x0004790C
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
			NetDataContractSerializer netDataContractSerializer = new NetDataContractSerializer();
			using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateBinaryWriter(stream))
			{
				netDataContractSerializer.WriteObject(xmlDictionaryWriter, value);
				stream.Flush();
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0004976C File Offset: 0x0004796C
		public object Deserialize(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			NetDataContractSerializer netDataContractSerializer = new NetDataContractSerializer();
			netDataContractSerializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
			object obj;
			using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max))
			{
				obj = netDataContractSerializer.ReadObject(xmlDictionaryReader);
			}
			return obj;
		}
	}
}
