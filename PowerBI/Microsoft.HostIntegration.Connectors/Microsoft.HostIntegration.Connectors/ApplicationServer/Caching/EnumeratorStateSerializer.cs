using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001D7 RID: 471
	internal static class EnumeratorStateSerializer
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x000341BC File Offset: 0x000323BC
		public static byte[][] SerializeEnumeratorState(EnumeratorState state)
		{
			if (state == null)
			{
				return null;
			}
			byte[][] array;
			using (ChunkStream chunkStream = new ChunkStream())
			{
				chunkStream.WriteByte(EnumeratorStateSerializer.markValue);
				chunkStream.WriteByte(EnumeratorStateSerializer.markValue);
				using (StreamBackedWriter streamBackedWriter = new StreamBackedWriter(chunkStream, false))
				{
					EnumeratorStateSerializer.SerializeEnumeratorStateHelper(state, streamBackedWriter);
					array = chunkStream.ToChunkedArray();
				}
			}
			return array;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00034238 File Offset: 0x00032438
		public static EnumeratorState DeserializeEnumeratorState(byte[][] buffer)
		{
			if (buffer == null)
			{
				return null;
			}
			EnumeratorState enumeratorState;
			using (ChunkStream chunkStream = new ChunkStream(buffer))
			{
				int num = chunkStream.ReadByte();
				int num2 = chunkStream.ReadByte();
				if (SerializationUtility.IsDataContractSerialization(num, num2))
				{
					enumeratorState = SerializationUtility.Deserialize(buffer, true) as EnumeratorState;
				}
				else
				{
					using (StreamBackedReader streamBackedReader = new StreamBackedReader(chunkStream, false))
					{
						enumeratorState = EnumeratorStateSerializer.DeserializeEnumeratorStateHelper(streamBackedReader);
					}
				}
			}
			return enumeratorState;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x000342BC File Offset: 0x000324BC
		private static void SerializeEnumeratorStateHelper(EnumeratorState state, GenericWriter writer)
		{
			if (state == null)
			{
				EnumeratorStateSerializer.WriteEnumeratorStateType(EnumeratorStateType.Invalid, writer);
				return;
			}
			EnumeratorStateSerializer.WriteEnumeratorStateType(state.EnumeratorStateType, writer);
			switch (state.EnumeratorStateType)
			{
			case EnumeratorStateType.BaseEnumeratorState:
				EnumeratorStateSerializer.SerializeBaseEnumeratorState(state as BaseEnumeratorState, writer);
				return;
			case EnumeratorStateType.FindEnumeratorState:
				EnumeratorStateSerializer.SerializeFindEnumeratorState(state as FindEnumeratorState, writer);
				return;
			case EnumeratorStateType.FixedDepthEnumeratorState:
				EnumeratorStateSerializer.SerializeFixedDepthEnumeratorState(state as FixedDepthEnumeratorState, writer);
				return;
			case EnumeratorStateType.EnumeratorStateForTagsIntersection:
				EnumeratorStateSerializer.SerializeOmEnumeratorStateForTagsIntersection(state as OmEnumeratorStateForTagsIntersection, writer);
				return;
			case EnumeratorStateType.EnumeratorStateForUnionAll:
				EnumeratorStateSerializer.SerializeUnionAllEnumeratorState(state as UnionAllEnumeratorState, writer);
				return;
			default:
				throw new InvalidOperationException("Invalid state type");
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00034350 File Offset: 0x00032550
		private static EnumeratorState DeserializeEnumeratorStateHelper(GenericReader reader)
		{
			EnumeratorState enumeratorState;
			switch (EnumeratorStateSerializer.ReadEnumeratorStateType(reader))
			{
			case EnumeratorStateType.Invalid:
				enumeratorState = null;
				break;
			case EnumeratorStateType.BaseEnumeratorState:
				enumeratorState = EnumeratorStateSerializer.DeserializeBaseEnumeratorState(reader);
				break;
			case EnumeratorStateType.FindEnumeratorState:
				enumeratorState = EnumeratorStateSerializer.DeserializeFindEnumeratorState(reader);
				break;
			case EnumeratorStateType.FixedDepthEnumeratorState:
				enumeratorState = EnumeratorStateSerializer.DeserializeFixedDepthEnumeratorState(reader);
				break;
			case EnumeratorStateType.EnumeratorStateForTagsIntersection:
				enumeratorState = EnumeratorStateSerializer.DeserializeOmEnumeratorStateForTagsIntersection(reader);
				break;
			case EnumeratorStateType.EnumeratorStateForUnionAll:
				enumeratorState = EnumeratorStateSerializer.DeserializeUnionAllEnumeratorState(reader);
				break;
			default:
				throw new InvalidOperationException("Invalid state type");
			}
			return enumeratorState;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x000343C3 File Offset: 0x000325C3
		private static void SerializeBaseEnumeratorState(BaseEnumeratorState state, GenericWriter writer)
		{
			writer.Write(state.Path);
			writer.Write(state.Index);
			writer.Write(state.Exhausted);
			writer.Write(state.CreationCompactionEpoch);
			writer.Write(state.ParentId);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00034404 File Offset: 0x00032604
		private static BaseEnumeratorState DeserializeBaseEnumeratorState(GenericReader reader)
		{
			SerializationUtility.AssertBufferAvailable(reader, 21);
			uint num = reader.ReadUInt32();
			int num2 = reader.ReadInt32();
			bool flag = reader.ReadBooleanFromByte();
			long num3 = reader.ReadInt64();
			int num4 = reader.ReadInt32();
			return new BaseEnumeratorState(num, num2, flag, num3, num4);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00034448 File Offset: 0x00032648
		private static void SerializeFindEnumeratorState(FindEnumeratorState state, GenericWriter writer)
		{
			writer.Write(state.ParentId);
			writer.Write(state.Exhausted);
			EnumeratorStateSerializer.WriteLength((state.List == null) ? (-1) : state.List.Count, writer);
			EnumeratorStateSerializer.SerializeEnumeratorStateHelper(state.CurrentState, writer);
			if (state.List != null)
			{
				foreach (FixedDepthEnumeratorState fixedDepthEnumeratorState in state.List)
				{
					EnumeratorStateSerializer.SerializeEnumeratorStateHelper(fixedDepthEnumeratorState, writer);
				}
			}
			byte[] array = null;
			if (state.Keys != null && state.Keys.Length > 0)
			{
				array = SerializationUtility.SerializeTags(state.Keys.Select((object t) => t as DataCacheTag), EnumeratorStateSerializer._encoding);
			}
			EnumeratorStateSerializer.WriteBytes(array, writer);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00034534 File Offset: 0x00032734
		private static FindEnumeratorState DeserializeFindEnumeratorState(GenericReader reader)
		{
			SerializationUtility.AssertBufferAvailable(reader, 9);
			int num = reader.ReadInt32();
			bool flag = reader.ReadBooleanFromByte();
			List<FixedDepthEnumeratorState> list = EnumeratorStateSerializer.InitializeList<FixedDepthEnumeratorState>(reader);
			BaseEnumeratorState baseEnumeratorState = EnumeratorStateSerializer.DeserializeEnumeratorStateHelper(reader) as BaseEnumeratorState;
			if (list != null)
			{
				for (int i = 0; i < list.Capacity; i++)
				{
					list.Add(EnumeratorStateSerializer.DeserializeEnumeratorStateHelper(reader) as FixedDepthEnumeratorState);
				}
			}
			byte[] array = EnumeratorStateSerializer.ReadBytes(reader);
			object[] array2 = null;
			if (array != null && array.Length > 0)
			{
				IEnumerable<DataCacheTag> enumerable = SerializationUtility.DeserializeTags(array, EnumeratorStateSerializer._encoding);
				array2 = enumerable.ToArray<DataCacheTag>();
			}
			return new FindEnumeratorState(num, array2, list, baseEnumeratorState, flag);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x000345CB File Offset: 0x000327CB
		private static void SerializeFixedDepthEnumeratorState(FixedDepthEnumeratorState state, GenericWriter writer)
		{
			writer.Write(state.Level);
			EnumeratorStateSerializer.SerializeBaseEnumeratorState(state, writer);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x000345E0 File Offset: 0x000327E0
		private static FixedDepthEnumeratorState DeserializeFixedDepthEnumeratorState(GenericReader reader)
		{
			SerializationUtility.AssertBufferAvailable(reader, 4);
			int num = reader.ReadInt32();
			BaseEnumeratorState baseEnumeratorState = EnumeratorStateSerializer.DeserializeBaseEnumeratorState(reader);
			return new FixedDepthEnumeratorState(num, baseEnumeratorState);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0003460C File Offset: 0x0003280C
		private static void SerializeUnionAllEnumeratorState(UnionAllEnumeratorState state, GenericWriter writer)
		{
			writer.Write(state.Exhausted);
			writer.Write(state.CurrentIndex);
			EnumeratorStateSerializer.WriteLength((state.ListKeys == null) ? (-1) : state.ListKeys.Count, writer);
			EnumeratorStateSerializer.SerializeEnumeratorStateHelper(state.CurrentState, writer);
			if (state.ListKeys != null)
			{
				foreach (object[] array in state.ListKeys)
				{
					IEnumerable<DataCacheTag> enumerable;
					if (array != null)
					{
						enumerable = array.Select((object key) => key as DataCacheTag);
					}
					else
					{
						enumerable = null;
					}
					IEnumerable<DataCacheTag> enumerable2 = enumerable;
					byte[] array2 = SerializationUtility.SerializeTags(enumerable2, EnumeratorStateSerializer._encoding);
					EnumeratorStateSerializer.WriteBytes(array2, writer);
				}
			}
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000346E0 File Offset: 0x000328E0
		private static UnionAllEnumeratorState DeserializeUnionAllEnumeratorState(GenericReader reader)
		{
			SerializationUtility.AssertBufferAvailable(reader, 9);
			bool flag = reader.ReadBooleanFromByte();
			int num = reader.ReadInt32();
			List<object[]> list = EnumeratorStateSerializer.InitializeList<object[]>(reader);
			FindEnumeratorState findEnumeratorState = EnumeratorStateSerializer.DeserializeEnumeratorStateHelper(reader) as FindEnumeratorState;
			if (list != null)
			{
				for (int i = 0; i < list.Capacity; i++)
				{
					byte[] array = EnumeratorStateSerializer.ReadBytes(reader);
					if (array != null && array.Length > 0)
					{
						list.Add((from t in SerializationUtility.DeserializeTags(array, EnumeratorStateSerializer._encoding)
							select (t)).ToArray<object>());
					}
					else
					{
						list.Add(null);
					}
				}
			}
			return new UnionAllEnumeratorState(num, list, findEnumeratorState, flag);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0003478C File Offset: 0x0003298C
		private static void SerializeOmEnumeratorStateForTagsIntersection(OmEnumeratorStateForTagsIntersection state, GenericWriter writer)
		{
			byte[] array = SerializationUtility.SerializeTags(state.Tags, EnumeratorStateSerializer._encoding);
			EnumeratorStateSerializer.WriteBytes(array, writer);
			EnumeratorStateSerializer.SerializeEnumeratorStateHelper(state.EnumState, writer);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000347C0 File Offset: 0x000329C0
		private static OmEnumeratorStateForTagsIntersection DeserializeOmEnumeratorStateForTagsIntersection(GenericReader reader)
		{
			SerializationUtility.AssertBufferAvailable(reader, 4);
			byte[] array = EnumeratorStateSerializer.ReadBytes(reader);
			DataCacheTag[] array2 = SerializationUtility.DeserializeTags(array, EnumeratorStateSerializer._encoding).ToArray<DataCacheTag>();
			EnumeratorState enumeratorState = EnumeratorStateSerializer.DeserializeEnumeratorStateHelper(reader);
			return new OmEnumeratorStateForTagsIntersection(enumeratorState, array2);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000347FA File Offset: 0x000329FA
		private static void WriteEnumeratorStateType(EnumeratorStateType type, GenericWriter writer)
		{
			writer.Write((byte)type);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00034804 File Offset: 0x00032A04
		private static EnumeratorStateType ReadEnumeratorStateType(GenericReader reader)
		{
			SerializationUtility.AssertBufferAvailable(reader, 1);
			return (EnumeratorStateType)reader.ReadByte();
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00034820 File Offset: 0x00032A20
		private static void WriteBytes(byte[] buffer, GenericWriter writer)
		{
			EnumeratorStateSerializer.WriteLength((buffer == null) ? (-1) : buffer.Length, writer);
			if (buffer != null && buffer.Length > 0)
			{
				writer.Write(buffer);
			}
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00034844 File Offset: 0x00032A44
		private static byte[] ReadBytes(GenericReader reader)
		{
			byte[] array = EnumeratorStateSerializer.InitializeArray<byte>(reader);
			if (array != null && array.Length > 0)
			{
				SerializationUtility.AssertBufferAvailable(reader, array.Length);
				reader.ReadBytes(array, 0, array.Length);
			}
			return array;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00034876 File Offset: 0x00032A76
		private static void WriteLength(int length, GenericWriter writer)
		{
			writer.Write(length);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00034880 File Offset: 0x00032A80
		private static T[] InitializeArray<T>(GenericReader reader)
		{
			int num = reader.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			return new T[num];
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x000348A0 File Offset: 0x00032AA0
		private static List<T> InitializeList<T>(GenericReader reader)
		{
			int num = reader.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			return new List<T>(num);
		}

		// Token: 0x04000A8B RID: 2699
		private static Encoding _encoding = new UTF8Encoding(false, false);

		// Token: 0x04000A8C RID: 2700
		private static byte markValue = byte.MaxValue;
	}
}
