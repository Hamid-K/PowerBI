using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001288 RID: 4744
	public abstract class StreamedBinaryValue : BinaryValue
	{
		// Token: 0x170021E9 RID: 8681
		// (get) Token: 0x06007CAB RID: 31915 RVA: 0x001AC14F File Offset: 0x001AA34F
		public sealed override TypeValue Type
		{
			get
			{
				return TypeValue.Binary;
			}
		}

		// Token: 0x170021EA RID: 8682
		// (get) Token: 0x06007CAC RID: 31916 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x06007CAD RID: 31917 RVA: 0x001AC156 File Offset: 0x001AA356
		public sealed override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsBinary && value.AsBinary.Equals(this);
		}

		// Token: 0x06007CAE RID: 31918 RVA: 0x001AC16E File Offset: 0x001AA36E
		public override bool Equals(BufferedBinaryValue value)
		{
			return object.Equals(this, value);
		}

		// Token: 0x06007CAF RID: 31919 RVA: 0x001AC177 File Offset: 0x001AA377
		public override bool Equals(StreamedBinaryValue value)
		{
			return BinaryValue.Equals(this, value);
		}

		// Token: 0x06007CB0 RID: 31920 RVA: 0x001AC180 File Offset: 0x001AA380
		public override int GetHashCode(_ValueComparer comparer)
		{
			HashBuilder hashBuilder = default(HashBuilder);
			using (Stream stream = this.Open())
			{
				for (;;)
				{
					int num = stream.ReadByte();
					if (num == -1)
					{
						break;
					}
					hashBuilder.Add(num);
				}
			}
			return hashBuilder.ToHash();
		}

		// Token: 0x06007CB1 RID: 31921 RVA: 0x001AC1D4 File Offset: 0x001AA3D4
		public override ListValue ToList()
		{
			return new StreamedBinaryValue.StreamedBinaryListValue(this);
		}

		// Token: 0x06007CB2 RID: 31922 RVA: 0x001AC1DC File Offset: 0x001AA3DC
		public override bool TryGetLength(out long length)
		{
			using (Stream stream = this.Open())
			{
				if (stream.CanSeek)
				{
					length = stream.Length - stream.Position;
					return true;
				}
			}
			length = 0L;
			return false;
		}

		// Token: 0x170021EB RID: 8683
		// (get) Token: 0x06007CB3 RID: 31923 RVA: 0x001AC230 File Offset: 0x001AA430
		public override long Length
		{
			get
			{
				long num2;
				using (Stream stream = this.Open())
				{
					if (stream.CanSeek)
					{
						long num = stream.Length - stream.Position;
						if (num > ListValue.MaxCount)
						{
							throw ValueException.ListCountTooLarge(num);
						}
						num2 = num;
					}
					else
					{
						long num3 = 0L;
						byte[] array = new byte[4096];
						int num4;
						while ((num4 = stream.Read(array, 0, array.Length)) > 0)
						{
							num3 += (long)num4;
							if (num3 > ListValue.MaxCount)
							{
								throw ValueException.ListCountTooLarge(num3);
							}
						}
						num2 = num3;
					}
				}
				return num2;
			}
		}

		// Token: 0x170021EC RID: 8684
		// (get) Token: 0x06007CB4 RID: 31924 RVA: 0x001AC2C8 File Offset: 0x001AA4C8
		public override byte[] AsBytes
		{
			get
			{
				byte[] bytes;
				using (Stream stream = this.Open())
				{
					bytes = StreamedBinaryValue.GetBytes(stream);
				}
				return bytes;
			}
		}

		// Token: 0x06007CB5 RID: 31925 RVA: 0x001AC300 File Offset: 0x001AA500
		public override BinaryValue Range(RowCount offset, RowCount count)
		{
			if (!offset.IsZero || !count.IsInfinite)
			{
				long num = (count.IsInfinite ? long.MaxValue : count.Value);
				return new StreamedBinaryValue.RangedStreamBinaryValue(this, offset.Value, num);
			}
			return this;
		}

		// Token: 0x06007CB6 RID: 31926 RVA: 0x001AC34C File Offset: 0x001AA54C
		public override ListValue Split(RowCount pageSize)
		{
			if (pageSize.IsInfinite)
			{
				return ListValue.New(new Value[] { this });
			}
			if (pageSize.Value <= 0L)
			{
				throw ValueException.InvalidArguments(this, new Value[] { NumberValue.New(pageSize.Value) });
			}
			return new PagedListBinaryValue(this, pageSize.Value);
		}

		// Token: 0x06007CB7 RID: 31927 RVA: 0x001AC3A8 File Offset: 0x001AA5A8
		protected static byte[] GetBytes(Stream stream)
		{
			if (!stream.CanSeek)
			{
				byte[] array = new byte[4096];
				MemoryStream memoryStream = new MemoryStream(array.Length);
				int num;
				while ((num = stream.Read(array, 0, array.Length)) > 0)
				{
					long num2 = memoryStream.Length + (long)num;
					if (num2 > 2147483647L)
					{
						throw ValueException.ListCountTooLarge(num2);
					}
					memoryStream.Write(array, 0, num);
				}
				return memoryStream.ToArray();
			}
			long num3 = stream.Length - stream.Position;
			if (num3 > 2147483647L)
			{
				throw ValueException.ListCountTooLarge(num3);
			}
			return stream.ReadBlock((int)num3);
		}

		// Token: 0x040044CE RID: 17614
		private const int DefaultReadChunkSize = 4096;

		// Token: 0x02001289 RID: 4745
		private class StreamedBinaryListValue : StreamedListValue
		{
			// Token: 0x06007CB9 RID: 31929 RVA: 0x001AC43E File Offset: 0x001AA63E
			public StreamedBinaryListValue(StreamedBinaryValue binary)
			{
				this.binary = binary;
			}

			// Token: 0x06007CBA RID: 31930 RVA: 0x001AC44D File Offset: 0x001AA64D
			public override BinaryValue ToBinary()
			{
				return this.binary;
			}

			// Token: 0x06007CBB RID: 31931 RVA: 0x001AC455 File Offset: 0x001AA655
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return new StreamedBinaryValue.StreamedBinaryListValue.StreamedBinaryEnumerator(this.binary);
			}

			// Token: 0x040044CF RID: 17615
			private readonly StreamedBinaryValue binary;

			// Token: 0x0200128A RID: 4746
			private class StreamedBinaryEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06007CBC RID: 31932 RVA: 0x001AC462 File Offset: 0x001AA662
				public StreamedBinaryEnumerator(StreamedBinaryValue binary)
				{
					this.stream = binary.Open();
				}

				// Token: 0x06007CBD RID: 31933 RVA: 0x001AC476 File Offset: 0x001AA676
				public void Dispose()
				{
					this.stream.Close();
				}

				// Token: 0x170021ED RID: 8685
				// (get) Token: 0x06007CBE RID: 31934 RVA: 0x001AC483 File Offset: 0x001AA683
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06007CBF RID: 31935 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x170021EE RID: 8686
				// (get) Token: 0x06007CC0 RID: 31936 RVA: 0x001AC48B File Offset: 0x001AA68B
				public IValueReference Current
				{
					get
					{
						if (this.current == null)
						{
							this.current = NumberValue.New(this.value);
						}
						return this.current;
					}
				}

				// Token: 0x06007CC1 RID: 31937 RVA: 0x001AC4AC File Offset: 0x001AA6AC
				public bool MoveNext()
				{
					this.current = null;
					this.value = this.stream.ReadByte();
					return this.value != -1;
				}

				// Token: 0x040044D0 RID: 17616
				private Stream stream;

				// Token: 0x040044D1 RID: 17617
				private int value;

				// Token: 0x040044D2 RID: 17618
				private NumberValue current;
			}
		}

		// Token: 0x0200128B RID: 4747
		private class RangedStreamBinaryValue : StreamedBinaryValue
		{
			// Token: 0x06007CC2 RID: 31938 RVA: 0x001AC4D2 File Offset: 0x001AA6D2
			public RangedStreamBinaryValue(StreamedBinaryValue value, long offset, long count)
			{
				this.value = value;
				this.offset = offset;
				this.count = count;
			}

			// Token: 0x06007CC3 RID: 31939 RVA: 0x001AC4EF File Offset: 0x001AA6EF
			public override Stream Open()
			{
				return this.value.Open().Skip(this.offset).Take(this.count);
			}

			// Token: 0x040044D3 RID: 17619
			private readonly StreamedBinaryValue value;

			// Token: 0x040044D4 RID: 17620
			private readonly long offset;

			// Token: 0x040044D5 RID: 17621
			private readonly long count;
		}
	}
}
