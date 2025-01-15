using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002A3 RID: 675
	internal class IndexedStreamHeader
	{
		// Token: 0x060018A4 RID: 6308 RVA: 0x00063AB2 File Offset: 0x00061CB2
		internal IndexedStreamHeader()
		{
			this.m_compressedIndex = new List<long>();
			this.m_uncompressedIndex = new List<long>();
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00063AD8 File Offset: 0x00061CD8
		internal IndexedStreamHeader(Stream store, short version)
		{
			if (!store.CanSeek)
			{
				throw new InternalCatalogException("Expected stream with Seek capabilities");
			}
			if (!store.CanRead)
			{
				throw new InternalCatalogException("Expected stream with Read capabilities");
			}
			this.m_headerIndex = this.ReadHeaderOffset(store);
			this.ReadHeader(store);
			this.m_version = (long)version;
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Reading header\r\n");
				stringBuilder.Append("compressesIndexes:\r\n");
				foreach (long num in this.m_compressedIndex)
				{
					stringBuilder.Append(num + "\r\n");
				}
				stringBuilder.Append("uncompressesIndexes:\r\n");
				foreach (long num2 in this.m_uncompressedIndex)
				{
					stringBuilder.Append(num2 + "\r\n");
				}
				RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, stringBuilder.ToString());
			}
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00063C28 File Offset: 0x00061E28
		private long ReadHeaderOffset(Stream store)
		{
			store.Seek(0L, SeekOrigin.Begin);
			return new BinaryReader(store).ReadInt64();
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00063C40 File Offset: 0x00061E40
		private void ReadHeader(Stream store)
		{
			store.Seek(this.m_headerIndex, SeekOrigin.Begin);
			BinaryReader binaryReader = new BinaryReader(store);
			long num = binaryReader.ReadInt64();
			if (num != this.m_version && num != 1L)
			{
				throw new InternalCatalogException("unexpected index version");
			}
			long num2 = binaryReader.ReadInt64();
			RSTrace.CatalogTrace.Assert(num2 <= 2147483647L, "listSize is too large");
			int num3 = (int)num2;
			this.m_uncompressedIndex = new List<long>(num3);
			for (int i = 0; i < num3; i++)
			{
				this.m_uncompressedIndex.Add(binaryReader.ReadInt64());
			}
			this.m_compressedIndex = new List<long>(num3);
			for (int j = 0; j < num3; j++)
			{
				this.m_compressedIndex.Add(binaryReader.ReadInt64());
			}
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00063D04 File Offset: 0x00061F04
		internal void TruncateIndexes(long uncompressedOffset)
		{
			int count = this.m_uncompressedIndex.Count;
			int num = count;
			while (num > 0 && uncompressedOffset < this.m_uncompressedIndex[num - 1])
			{
				num--;
			}
			int num2 = count - num;
			if (num2 > 0)
			{
				this.m_compressedIndex.RemoveRange(num, num2);
				this.m_uncompressedIndex.RemoveRange(num, num2);
			}
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00063D5C File Offset: 0x00061F5C
		internal void AddIndexRecord(long compressedOffset, long uncompressedOffset)
		{
			this.m_compressedIndex.Add(compressedOffset);
			this.m_uncompressedIndex.Add(uncompressedOffset);
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00063D78 File Offset: 0x00061F78
		internal void Save(Stream store)
		{
			if (!store.CanSeek)
			{
				throw new InternalCatalogException("Expected stream with Seek capabilities");
			}
			if (!store.CanWrite)
			{
				throw new InternalCatalogException("Expected stream with Write capabilities");
			}
			this.m_headerIndex = this.LastCompressedOffset;
			if (this.m_headerIndex == 0L)
			{
				this.m_headerIndex = (long)Marshal.SizeOf<long>(this.m_headerIndex);
			}
			store.Seek(0L, SeekOrigin.Begin);
			BinaryWriter binaryWriter = new BinaryWriter(store);
			binaryWriter.Write(this.m_headerIndex);
			store.Seek(this.m_headerIndex, SeekOrigin.Begin);
			MemoryStream memoryStream = new MemoryStream();
			binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(this.m_version);
			binaryWriter.Write((long)this.m_uncompressedIndex.Count);
			foreach (long num in this.m_uncompressedIndex)
			{
				binaryWriter.Write(num);
			}
			foreach (long num2 in this.m_compressedIndex)
			{
				binaryWriter.Write(num2);
			}
			byte[] array = memoryStream.ToArray();
			store.Write(array, 0, array.Length);
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00063EC8 File Offset: 0x000620C8
		internal void GetBufferForOffset(long offset, out IndexStruct index)
		{
			int count = this.m_uncompressedIndex.Count;
			index = null;
			int i = count - 1;
			while (i >= 0)
			{
				long num = this.m_uncompressedIndex[i];
				if (offset >= num)
				{
					index = new IndexStruct();
					index.UncompressedBufferStart = num;
					index.CompressedBufferStart = this.m_compressedIndex[i];
					if (i == count - 1)
					{
						index.UncompressedBufferEnd = num;
						index.CompressedBufferEnd = this.m_compressedIndex[i];
						return;
					}
					index.UncompressedBufferEnd = this.m_uncompressedIndex[i + 1];
					index.CompressedBufferEnd = this.m_compressedIndex[i + 1];
					return;
				}
				else
				{
					i--;
				}
			}
			throw new InternalCatalogException("offset not found in index");
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00063F84 File Offset: 0x00062184
		internal long GetUncompressedBufferStartPosition(long offset)
		{
			IndexStruct indexStruct = null;
			this.GetBufferForOffset(offset, out indexStruct);
			return indexStruct.UncompressedBufferStart;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00063FA4 File Offset: 0x000621A4
		internal long GetRelativeUncompressedPosition(long offset)
		{
			IndexStruct indexStruct = null;
			this.GetBufferForOffset(offset, out indexStruct);
			if (offset < indexStruct.UncompressedBufferStart)
			{
				throw new InternalCatalogException("unexpected offset");
			}
			return offset - indexStruct.UncompressedBufferStart;
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x00063FD8 File Offset: 0x000621D8
		internal long HeaderOffset
		{
			get
			{
				return this.m_headerIndex;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x00063FE0 File Offset: 0x000621E0
		internal long Length
		{
			get
			{
				return (long)(8 + Marshal.SizeOf<long>(this.m_version) + this.m_compressedIndex.Count * 8 + this.m_uncompressedIndex.Count * 8);
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x0006400C File Offset: 0x0006220C
		internal long LastUncompressedOffset
		{
			get
			{
				if (this.m_uncompressedIndex.Count == 0)
				{
					return 0L;
				}
				return this.m_uncompressedIndex[this.m_uncompressedIndex.Count - 1];
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x00064036 File Offset: 0x00062236
		internal long LastCompressedOffset
		{
			get
			{
				if (this.m_compressedIndex.Count == 0)
				{
					return 0L;
				}
				return this.m_compressedIndex[this.m_compressedIndex.Count - 1];
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x00064060 File Offset: 0x00062260
		internal long Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x00064068 File Offset: 0x00062268
		internal int IndexEntryCount
		{
			get
			{
				return this.m_uncompressedIndex.Count;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x00064075 File Offset: 0x00062275
		internal static long OffsetLength
		{
			get
			{
				return 8L;
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00064079 File Offset: 0x00062279
		internal static long OffsetToInternal(long offset)
		{
			return offset + IndexedStreamHeader.OffsetLength;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00064082 File Offset: 0x00062282
		internal static long OffsetToExternal(long offset)
		{
			return offset - IndexedStreamHeader.OffsetLength;
		}

		// Token: 0x040008DE RID: 2270
		internal const long VersionOne = 1L;

		// Token: 0x040008DF RID: 2271
		internal const long VersionTwo = 2L;

		// Token: 0x040008E0 RID: 2272
		private long m_headerIndex;

		// Token: 0x040008E1 RID: 2273
		private long m_version = 2L;

		// Token: 0x040008E2 RID: 2274
		private List<long> m_compressedIndex;

		// Token: 0x040008E3 RID: 2275
		private List<long> m_uncompressedIndex;

		// Token: 0x040008E4 RID: 2276
		private const int m_sizeofLong = 8;
	}
}
