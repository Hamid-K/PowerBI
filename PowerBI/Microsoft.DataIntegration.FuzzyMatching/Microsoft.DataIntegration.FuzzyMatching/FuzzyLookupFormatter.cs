using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200004D RID: 77
	public class FuzzyLookupFormatter : IFormatter
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000DD30 File Offset: 0x0000BF30
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000DD3D File Offset: 0x0000BF3D
		public SerializationBinder Binder
		{
			get
			{
				return this.m_baseFormatter.Binder;
			}
			set
			{
				this.m_baseFormatter.Binder = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000DD4B File Offset: 0x0000BF4B
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000DD58 File Offset: 0x0000BF58
		public StreamingContext Context
		{
			get
			{
				return this.m_baseFormatter.Context;
			}
			set
			{
				this.m_baseFormatter.Context = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000DD66 File Offset: 0x0000BF66
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000DD73 File Offset: 0x0000BF73
		public ISurrogateSelector SurrogateSelector
		{
			get
			{
				return this.m_baseFormatter.SurrogateSelector;
			}
			set
			{
				this.m_baseFormatter.SurrogateSelector = value;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000DD81 File Offset: 0x0000BF81
		public FuzzyLookupFormatter()
		{
			this.m_baseFormatter = new BinaryFormatter
			{
				AssemblyFormat = 0,
				FilterLevel = 2,
				TypeFormat = 0
			};
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000DDAC File Offset: 0x0000BFAC
		public void Serialize(Stream serializationStream, object graph)
		{
			FLSurrogateSelector flsurrogateSelector = new FLSurrogateSelector();
			this.m_baseFormatter.SurrogateSelector = flsurrogateSelector;
			this.m_baseFormatter.Serialize(serializationStream, graph);
			this.SortById(flsurrogateSelector);
			foreach (IRawSerializable rawSerializable in flsurrogateSelector.m_serializationSurrogate.m_rawSerializableObjects)
			{
				long position = serializationStream.Position;
				rawSerializable.Serialize(serializationStream);
				long position2 = serializationStream.Position;
				StreamUtilities.WriteInt32(serializationStream, rawSerializable.RawSerializationID);
				StreamUtilities.WriteInt64(serializationStream, position2 - position);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000DE50 File Offset: 0x0000C050
		private void SortById(FLSurrogateSelector ss)
		{
			ss.m_serializationSurrogate.m_rawSerializableObjects.Sort(delegate(IRawSerializable x, IRawSerializable y)
			{
				if (x.RawSerializationID < y.RawSerializationID)
				{
					return -1;
				}
				if (x.RawSerializationID != y.RawSerializationID)
				{
					return 1;
				}
				return 0;
			});
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000DE84 File Offset: 0x0000C084
		public object Deserialize(Stream serializationStream)
		{
			FLSurrogateSelector flsurrogateSelector = new FLSurrogateSelector();
			this.m_baseFormatter.SurrogateSelector = flsurrogateSelector;
			object obj = this.m_baseFormatter.Deserialize(serializationStream);
			this.SortById(flsurrogateSelector);
			foreach (IRawSerializable rawSerializable in flsurrogateSelector.m_serializationSurrogate.m_rawSerializableObjects)
			{
				long position = serializationStream.Position;
				rawSerializable.Deserialize(serializationStream);
				long position2 = serializationStream.Position;
				int num = StreamUtilities.ReadInt32(serializationStream);
				long num2 = StreamUtilities.ReadInt64(serializationStream);
				if (rawSerializable.RawSerializationID != num)
				{
					throw new SerializationException("Unexpected object id encountered when deserializing.");
				}
				if (num2 != position2 - position)
				{
					throw new SerializationException("Unexpected object length encountered when deserializing.");
				}
			}
			return obj;
		}

		// Token: 0x040000E5 RID: 229
		private IFormatter m_baseFormatter;
	}
}
