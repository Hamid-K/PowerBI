using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200128C RID: 4748
	public abstract class BufferedBinaryValue : BinaryValue
	{
		// Token: 0x170021EF RID: 8687
		// (get) Token: 0x06007CC4 RID: 31940 RVA: 0x001AC14F File Offset: 0x001AA34F
		public sealed override TypeValue Type
		{
			get
			{
				return TypeValue.Binary;
			}
		}

		// Token: 0x170021F0 RID: 8688
		// (get) Token: 0x06007CC5 RID: 31941 RVA: 0x00019E61 File Offset: 0x00018061
		public sealed override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x06007CC6 RID: 31942 RVA: 0x001AC512 File Offset: 0x001AA712
		public override bool TryGetLength(out long length)
		{
			length = this.Length;
			return true;
		}

		// Token: 0x170021F1 RID: 8689
		// (get) Token: 0x06007CC7 RID: 31943 RVA: 0x001AC51D File Offset: 0x001AA71D
		public override long Length
		{
			get
			{
				return (long)this.AsBytes.Length;
			}
		}

		// Token: 0x06007CC8 RID: 31944 RVA: 0x001AC528 File Offset: 0x001AA728
		public override Stream Open()
		{
			return new MemoryStream(this.AsBytes);
		}

		// Token: 0x06007CC9 RID: 31945 RVA: 0x001AC535 File Offset: 0x001AA735
		public sealed override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsBinary && value.AsBinary.Equals(this);
		}

		// Token: 0x06007CCA RID: 31946 RVA: 0x001AC54D File Offset: 0x001AA74D
		public sealed override bool Equals(BufferedBinaryValue value)
		{
			return BinaryValue.Equals(this, value);
		}

		// Token: 0x06007CCB RID: 31947 RVA: 0x001AC556 File Offset: 0x001AA756
		public sealed override bool Equals(StreamedBinaryValue value)
		{
			return BinaryValue.Equals(this, value);
		}

		// Token: 0x06007CCC RID: 31948 RVA: 0x001AC560 File Offset: 0x001AA760
		public sealed override int GetHashCode(_ValueComparer comparer)
		{
			HashBuilder hashBuilder = default(HashBuilder);
			byte[] asBytes = this.AsBytes;
			for (int i = 0; i < asBytes.Length; i++)
			{
				hashBuilder.Add((int)asBytes[i]);
			}
			return hashBuilder.ToHash();
		}

		// Token: 0x06007CCD RID: 31949 RVA: 0x001AC59B File Offset: 0x001AA79B
		public sealed override ListValue ToList()
		{
			return new BufferedBinaryValue.BufferedBinaryListValue(this);
		}

		// Token: 0x06007CCE RID: 31950 RVA: 0x001AC5A4 File Offset: 0x001AA7A4
		public override BinaryValue Range(RowCount offset, RowCount count)
		{
			long length = this.Length;
			long num = Math.Min(offset.Value, length);
			long num2 = (count.IsInfinite ? length : Math.Min(length, num + count.Value)) - num;
			if (num2 == 0L)
			{
				return BinaryValue.Empty;
			}
			byte[] array = new byte[num2];
			Array.Copy(this.AsBytes, num, array, 0L, num2);
			return BinaryValue.New(array);
		}

		// Token: 0x06007CCF RID: 31951 RVA: 0x001AC60C File Offset: 0x001AA80C
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
			long length = this.Length;
			if (length == 0L)
			{
				return ListValue.Empty;
			}
			long chunkLength = pageSize.Value;
			long num = (length - 1L) / chunkLength + 1L;
			if (num > ListValue.MaxCount)
			{
				throw ValueException.ListCountTooLarge(num);
			}
			return ListValue.New((int)num, (int i) => this.Range(new RowCount((long)i * chunkLength), new RowCount(chunkLength)));
		}

		// Token: 0x0200128D RID: 4749
		private class BufferedBinaryListValue : BufferedListValue
		{
			// Token: 0x06007CD1 RID: 31953 RVA: 0x001AC6B4 File Offset: 0x001AA8B4
			public BufferedBinaryListValue(BufferedBinaryValue binary)
			{
				this.binary = binary;
			}

			// Token: 0x06007CD2 RID: 31954 RVA: 0x001AC6C3 File Offset: 0x001AA8C3
			public override BinaryValue ToBinary()
			{
				return this.binary;
			}

			// Token: 0x170021F2 RID: 8690
			// (get) Token: 0x06007CD3 RID: 31955 RVA: 0x001AC6CB File Offset: 0x001AA8CB
			public override int Count
			{
				get
				{
					return this.binary.AsBytes.Length;
				}
			}

			// Token: 0x06007CD4 RID: 31956 RVA: 0x001AC6DA File Offset: 0x001AA8DA
			public override IValueReference GetReference(int index)
			{
				return this[index];
			}

			// Token: 0x170021F3 RID: 8691
			public override Value this[int index]
			{
				get
				{
					byte[] asBytes = this.binary.AsBytes;
					if (index >= 0 && index < asBytes.Length)
					{
						return NumberValue.New((int)asBytes[index]);
					}
					return base[index];
				}
			}

			// Token: 0x040044D6 RID: 17622
			private BufferedBinaryValue binary;
		}
	}
}
