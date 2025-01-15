using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.DeltaLake.Commands;
using Microsoft.DI.RoaringBitmap;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.DeltaLake;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.DeltaLake
{
	// Token: 0x02001EDF RID: 7903
	internal static class DeletionVectors
	{
		// Token: 0x06010A8B RID: 68235 RVA: 0x00395A6C File Offset: 0x00393C6C
		public static IEnumerable<T> GetFilteredItems<T>(DeltaSource source, IEnumerable<T> items, DeletionVector dv)
		{
			IEnumerable<T> filteredItems;
			try
			{
				filteredItems = DeletionVectors.GetFilteredItems<T>(items, DeletionVectors.GetDeletedRows(source, dv));
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex) && !(ex is RuntimeException))
			{
				throw ValueException.NewDataFormatError<Message1>(Resources.DeletionVectorInvalid(PiiFree.New(new string(dv.StorageType, 1))), Value.Null, null);
			}
			return filteredItems;
		}

		// Token: 0x06010A8C RID: 68236 RVA: 0x00395AE8 File Offset: 0x00393CE8
		public static IEnumerable<T> GetFilteredItems<T>(IEnumerable<T> items, IEnumerable<ulong> itemsToDelete)
		{
			using (IEnumerator<T> values = items.GetEnumerator())
			{
				using (IEnumerator<ulong> deletions = itemsToDelete.GetEnumerator())
				{
					ulong index = 0UL;
					ulong next = (deletions.MoveNext() ? deletions.Current : ulong.MaxValue);
					while (values.MoveNext())
					{
						if (index < next)
						{
							yield return values.Current;
						}
						else
						{
							next = (deletions.MoveNext() ? deletions.Current : ulong.MaxValue);
						}
						ulong num = index;
						index = num + 1UL;
					}
				}
				IEnumerator<ulong> deletions = null;
			}
			IEnumerator<T> values = null;
			yield break;
			yield break;
		}

		// Token: 0x06010A8D RID: 68237 RVA: 0x00395B00 File Offset: 0x00393D00
		private static IEnumerable<ulong> GetDeletedRows(DeltaSource source, DeletionVector dv)
		{
			char storageType = dv.StorageType;
			if (storageType != 'i')
			{
				if (storageType == 'u')
				{
					return DeletionVectors.GetBitmapsFromFile(source, dv).Values();
				}
			}
			else
			{
				using (Stream stream = new MemoryStream(Base85.Decode(dv.PathOrInlineDv)).Take((long)dv.SizeInBytes))
				{
					using (BinaryReader binaryReader = new BinaryReader(stream))
					{
						if (DeletionVectors.ReadLittleEndianInt32(binaryReader) != 1681511377)
						{
							throw new InvalidOperationException();
						}
						RoaringBitmap<ulong> roaringBitmap = new RoaringBitmap<ulong>();
						roaringBitmap.Deserialize(stream);
						return roaringBitmap.Values();
					}
				}
			}
			throw ValueException.NewDataFormatError<Message1>(Resources.DeletionVectorTypeNotSupported(PiiFree.New(new string(dv.StorageType, 1))), Value.Null, null);
		}

		// Token: 0x06010A8E RID: 68238 RVA: 0x00395BCC File Offset: 0x00393DCC
		private static int ReadLittleEndianInt32(BinaryReader reader)
		{
			byte[] array = reader.ReadBytes(4);
			return ((int)array[3] << 24) | ((int)array[2] << 16) | ((int)array[1] << 8) | (int)array[0];
		}

		// Token: 0x06010A8F RID: 68239 RVA: 0x00395BF8 File Offset: 0x00393DF8
		private static int ReadBigEndianInt32(BinaryReader reader)
		{
			byte[] array = reader.ReadBytes(4);
			return ((int)array[0] << 24) | ((int)array[1] << 16) | ((int)array[2] << 8) | (int)array[3];
		}

		// Token: 0x06010A90 RID: 68240 RVA: 0x00395C24 File Offset: 0x00393E24
		private static RoaringBitmap<ulong> GetBitmapsFromFile(DeltaSource source, DeletionVector dv)
		{
			string pathOrInlineDv = dv.PathOrInlineDv;
			int num = ((pathOrInlineDv != null) ? pathOrInlineDv.Length : 0);
			if (num < 20)
			{
				throw new InvalidOperationException();
			}
			string text;
			if (num > 20)
			{
				text = dv.PathOrInlineDv.Substring(0, num - 20) + "/deletion_vector_" + Base85.DecodeDelta(dv.PathOrInlineDv.Substring(num - 20)).ToString("D") + ".bin";
			}
			else
			{
				text = "deletion_vector_" + Base85.DecodeDelta(dv.PathOrInlineDv).ToString("D") + ".bin";
			}
			RoaringBitmap<ulong> roaringBitmap2;
			using (Stream stream = source.GetRawFile(text).Open().Skip((long)dv.Offset.GetValueOrDefault()))
			{
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					int num2 = DeletionVectors.ReadBigEndianInt32(binaryReader);
					if (num2 != dv.SizeInBytes)
					{
						throw new InvalidOperationException();
					}
					if (DeletionVectors.ReadLittleEndianInt32(binaryReader) != 1681511377)
					{
						throw new InvalidOperationException();
					}
					RoaringBitmap<ulong> roaringBitmap = new RoaringBitmap<ulong>();
					roaringBitmap.Deserialize(stream.Take((long)num2));
					roaringBitmap2 = roaringBitmap;
				}
			}
			return roaringBitmap2;
		}

		// Token: 0x04006398 RID: 25496
		private const string prefix = "deletion_vector_";

		// Token: 0x04006399 RID: 25497
		private const string suffix = ".bin";

		// Token: 0x0400639A RID: 25498
		private const int MAGIC = 1681511377;

		// Token: 0x0400639B RID: 25499
		private const int MinimumDvPathLength = 20;
	}
}
