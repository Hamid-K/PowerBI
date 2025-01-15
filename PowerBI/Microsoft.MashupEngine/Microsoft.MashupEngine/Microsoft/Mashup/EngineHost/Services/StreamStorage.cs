using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B3E RID: 6974
	internal class StreamStorage : IStorage, IDisposable
	{
		// Token: 0x0600AE91 RID: 44689 RVA: 0x0023BEEE File Offset: 0x0023A0EE
		public static StreamStorage Create(Stream stream)
		{
			return new StreamStorage(stream, new Dictionary<int, StreamStorage.FileRange>(), false);
		}

		// Token: 0x0600AE92 RID: 44690 RVA: 0x0023BEFC File Offset: 0x0023A0FC
		public static StreamStorage Open(Stream stream)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			stream.Position = stream.Length - 4L;
			int num = binaryReader.ReadInt32();
			stream.Position = stream.Length - (long)(4 + 20 * num);
			Dictionary<int, StreamStorage.FileRange> dictionary = new Dictionary<int, StreamStorage.FileRange>();
			for (int i = 0; i < num; i++)
			{
				int num2 = binaryReader.ReadInt32();
				long num3 = binaryReader.ReadInt64();
				long num4 = binaryReader.ReadInt64();
				dictionary.Add(num2, new StreamStorage.FileRange(num3, num4));
			}
			return new StreamStorage(stream, dictionary, true);
		}

		// Token: 0x0600AE93 RID: 44691 RVA: 0x0023BF7D File Offset: 0x0023A17D
		private StreamStorage(Stream stream, Dictionary<int, StreamStorage.FileRange> dictionary, bool readOnly)
		{
			this.sharer = new StreamSharer(stream);
			this.dictionary = dictionary;
			this.readOnly = readOnly;
		}

		// Token: 0x17002BCF RID: 11215
		// (get) Token: 0x0600AE94 RID: 44692 RVA: 0x0023BFAC File Offset: 0x0023A1AC
		public IEnumerable<int> StreamIds
		{
			get
			{
				object obj = this.syncRoot;
				IEnumerable<int> enumerable;
				lock (obj)
				{
					enumerable = this.dictionary.Keys.ToArray<int>();
				}
				return enumerable;
			}
		}

		// Token: 0x0600AE95 RID: 44693 RVA: 0x0023BFF8 File Offset: 0x0023A1F8
		public Stream OpenStream(int id)
		{
			object obj = this.syncRoot;
			StreamStorage.FileRange fileRange;
			lock (obj)
			{
				fileRange = this.dictionary[id];
			}
			return this.sharer.Open(fileRange.Offset, fileRange.Length);
		}

		// Token: 0x0600AE96 RID: 44694 RVA: 0x0023C058 File Offset: 0x0023A258
		public Stream CreateStream()
		{
			if (this.readOnly)
			{
				throw new InvalidOperationException();
			}
			return this.sharer.Create();
		}

		// Token: 0x0600AE97 RID: 44695 RVA: 0x0023C074 File Offset: 0x0023A274
		public Stream CommitStream(int id, Stream stream)
		{
			if (this.readOnly)
			{
				throw new InvalidOperationException();
			}
			stream.Flush();
			Stream stream2 = this.sharer.Stream;
			StreamStorage.FileRange fileRange = new StreamStorage.FileRange(stream2.Length - stream.Length, stream.Length);
			object obj = this.syncRoot;
			lock (obj)
			{
				this.dictionary.Add(id, fileRange);
			}
			stream.Close();
			return this.sharer.Open(fileRange.Offset, fileRange.Length);
		}

		// Token: 0x0600AE98 RID: 44696 RVA: 0x0023C114 File Offset: 0x0023A314
		public Stream Commit()
		{
			if (this.readOnly)
			{
				throw new InvalidOperationException();
			}
			Stream stream = this.sharer.Stream;
			long position = stream.Position;
			stream.Position = stream.Length;
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			object obj = this.syncRoot;
			KeyValuePair<int, StreamStorage.FileRange>[] array;
			lock (obj)
			{
				array = this.dictionary.ToArray<KeyValuePair<int, StreamStorage.FileRange>>();
			}
			foreach (KeyValuePair<int, StreamStorage.FileRange> keyValuePair in array)
			{
				binaryWriter.Write(keyValuePair.Key);
				binaryWriter.Write(keyValuePair.Value.Offset);
				binaryWriter.Write(keyValuePair.Value.Length);
			}
			binaryWriter.Write(this.dictionary.Count);
			binaryWriter.Flush();
			stream.Position = position;
			this.readOnly = true;
			return stream;
		}

		// Token: 0x0600AE99 RID: 44697 RVA: 0x0023C20C File Offset: 0x0023A40C
		public void Close()
		{
			this.sharer.Close();
		}

		// Token: 0x0600AE9A RID: 44698 RVA: 0x0023C219 File Offset: 0x0023A419
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x04005A04 RID: 23044
		private const int int32Size = 4;

		// Token: 0x04005A05 RID: 23045
		private const int int64Size = 8;

		// Token: 0x04005A06 RID: 23046
		private StreamSharer sharer;

		// Token: 0x04005A07 RID: 23047
		private Dictionary<int, StreamStorage.FileRange> dictionary;

		// Token: 0x04005A08 RID: 23048
		private bool readOnly;

		// Token: 0x04005A09 RID: 23049
		private readonly object syncRoot = new object();

		// Token: 0x02001B3F RID: 6975
		private struct FileRange
		{
			// Token: 0x0600AE9B RID: 44699 RVA: 0x0023C221 File Offset: 0x0023A421
			public FileRange(long offset, long length)
			{
				this.Offset = offset;
				this.Length = length;
			}

			// Token: 0x04005A0A RID: 23050
			public long Offset;

			// Token: 0x04005A0B RID: 23051
			public long Length;
		}
	}
}
