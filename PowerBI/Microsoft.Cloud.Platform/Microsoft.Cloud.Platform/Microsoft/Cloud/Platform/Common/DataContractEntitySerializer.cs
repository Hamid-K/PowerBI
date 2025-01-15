using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000521 RID: 1313
	[BlockServiceProvider(typeof(IEntitySerializer))]
	public sealed class DataContractEntitySerializer : Block, IEntitySerializer
	{
		// Token: 0x0600288C RID: 10380 RVA: 0x00092121 File Offset: 0x00090321
		public DataContractEntitySerializer()
			: this(typeof(DataContractEntitySerializer).Name)
		{
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x00010777 File Offset: 0x0000E977
		public DataContractEntitySerializer(string name)
			: base(name)
		{
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x00092138 File Offset: 0x00090338
		protected override BlockInitializationStatus OnInitialize()
		{
			BlockInitializationStatus blockInitializationStatus = base.OnInitialize();
			if (blockInitializationStatus == BlockInitializationStatus.Done)
			{
				this.m_eventsKit = this.m_eventsKitFactory.CreateEventsKit<IEntitySerializerEventsKit>();
			}
			return blockInitializationStatus;
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x00092154 File Offset: 0x00090354
		public void SerializeToStream(Stream target, object o, Type type, SerializationOptions options, IEnumerable<Type> knownTypes)
		{
			try
			{
				DataContractSerializer dataContractSerializer = new DataContractSerializer(type, knownTypes);
				if (options.HasFlag(SerializationOptions.Compression))
				{
					using (DeflateStream deflateStream = new DeflateStream(target, CompressionMode.Compress, false))
					{
						dataContractSerializer.WriteObject(deflateStream, o);
						goto IL_0042;
					}
				}
				dataContractSerializer.WriteObject(target, o);
				IL_0042:
				return;
			}
			catch (InvalidDataContractException ex)
			{
			}
			catch (XmlException ex)
			{
			}
			catch (SerializationException ex)
			{
			}
			Exception ex;
			EntityCannotBeSerializedException ex2 = new EntityCannotBeSerializedException(type, string.Empty, ex);
			this.m_eventsKit.NotifySerializationError(type.FullName, "serialization", ex2);
			throw ex2;
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x00092208 File Offset: 0x00090408
		public object Deserialize(Stream stream, Type type, SerializationOptions options, IEnumerable<Type> knownTypes)
		{
			try
			{
				DataContractSerializer dataContractSerializer = new DataContractSerializer(type, knownTypes);
				if (options.HasFlag(SerializationOptions.Compression))
				{
					using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress, false))
					{
						return dataContractSerializer.ReadObject(deflateStream);
					}
				}
				return dataContractSerializer.ReadObject(stream);
			}
			catch (InvalidDataContractException ex)
			{
			}
			catch (XmlException ex)
			{
			}
			catch (SerializationException ex)
			{
			}
			Exception ex;
			EntityCannotBeDeserializedException ex2 = new EntityCannotBeDeserializedException(type, string.Empty, ex);
			this.m_eventsKit.NotifySerializationError(type.FullName, "deserialization", ex2);
			throw ex2;
		}

		// Token: 0x04000E0E RID: 3598
		[BlockServiceDependency]
		private readonly IEventsKitFactory m_eventsKitFactory;

		// Token: 0x04000E0F RID: 3599
		private IEntitySerializerEventsKit m_eventsKit;
	}
}
