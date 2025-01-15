using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001285 RID: 4741
	internal abstract class DelegatingBinaryValue : BinaryValue
	{
		// Token: 0x06007C90 RID: 31888 RVA: 0x001ABFF0 File Offset: 0x001AA1F0
		protected DelegatingBinaryValue(BinaryValue binary)
		{
			this.binary = binary;
		}

		// Token: 0x170021E1 RID: 8673
		// (get) Token: 0x06007C91 RID: 31889 RVA: 0x001ABFFF File Offset: 0x001AA1FF
		protected BinaryValue Binary
		{
			get
			{
				return this.binary;
			}
		}

		// Token: 0x170021E2 RID: 8674
		// (get) Token: 0x06007C92 RID: 31890 RVA: 0x001AC007 File Offset: 0x001AA207
		public override RecordValue MetaValue
		{
			get
			{
				return this.binary.MetaValue;
			}
		}

		// Token: 0x170021E3 RID: 8675
		// (get) Token: 0x06007C93 RID: 31891 RVA: 0x001AC014 File Offset: 0x001AA214
		public override TypeValue Type
		{
			get
			{
				return this.binary.Type;
			}
		}

		// Token: 0x170021E4 RID: 8676
		// (get) Token: 0x06007C94 RID: 31892 RVA: 0x001AC021 File Offset: 0x001AA221
		public override IExpression Expression
		{
			get
			{
				return this.binary.Expression;
			}
		}

		// Token: 0x06007C95 RID: 31893 RVA: 0x001AC02E File Offset: 0x001AA22E
		public override bool TryGetAs<T>(out T contract)
		{
			return this.binary.TryGetAs<T>(out contract);
		}

		// Token: 0x170021E5 RID: 8677
		// (get) Token: 0x06007C96 RID: 31894 RVA: 0x001AC03C File Offset: 0x001AA23C
		public override byte[] AsBytes
		{
			get
			{
				return this.binary.AsBytes;
			}
		}

		// Token: 0x06007C97 RID: 31895 RVA: 0x001AC049 File Offset: 0x001AA249
		public override Stream Open()
		{
			return this.binary.Open();
		}

		// Token: 0x06007C98 RID: 31896 RVA: 0x001AC056 File Offset: 0x001AA256
		public override Stream Open(bool preferCanSeek)
		{
			return this.binary.Open(preferCanSeek);
		}

		// Token: 0x06007C99 RID: 31897 RVA: 0x001AC064 File Offset: 0x001AA264
		public override Stream OpenForWrite()
		{
			return this.binary.OpenForWrite();
		}

		// Token: 0x06007C9A RID: 31898 RVA: 0x001AC071 File Offset: 0x001AA271
		public override bool TryGetLength(out long length)
		{
			return this.binary.TryGetLength(out length);
		}

		// Token: 0x170021E6 RID: 8678
		// (get) Token: 0x06007C9B RID: 31899 RVA: 0x001AC07F File Offset: 0x001AA27F
		public override long Length
		{
			get
			{
				return this.binary.Length;
			}
		}

		// Token: 0x06007C9C RID: 31900 RVA: 0x001AC08C File Offset: 0x001AA28C
		public override ListValue ToList()
		{
			return this.binary.ToList();
		}

		// Token: 0x170021E7 RID: 8679
		// (get) Token: 0x06007C9D RID: 31901 RVA: 0x001AC099 File Offset: 0x001AA299
		public override BinaryValue End
		{
			get
			{
				return this.binary.End;
			}
		}

		// Token: 0x06007C9E RID: 31902 RVA: 0x001AC0A6 File Offset: 0x001AA2A6
		public override BinaryValue Range(RowCount offset, RowCount count)
		{
			return this.binary.Range(offset, count);
		}

		// Token: 0x06007C9F RID: 31903 RVA: 0x001AC0B5 File Offset: 0x001AA2B5
		public override ListValue Split(RowCount pageSize)
		{
			return this.binary.Split(pageSize);
		}

		// Token: 0x06007CA0 RID: 31904 RVA: 0x001AC0C3 File Offset: 0x001AA2C3
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			return this.binary.TryInvokeAsArgument(function, arguments, index, out result);
		}

		// Token: 0x06007CA1 RID: 31905 RVA: 0x001AC0D5 File Offset: 0x001AA2D5
		public override BinaryValue Optimize()
		{
			return this.binary.Optimize();
		}

		// Token: 0x06007CA2 RID: 31906 RVA: 0x001AC0E2 File Offset: 0x001AA2E2
		public override ActionValue Replace(Value value)
		{
			return this.binary.Replace(value);
		}

		// Token: 0x06007CA3 RID: 31907 RVA: 0x001AC0F0 File Offset: 0x001AA2F0
		public override bool Equals(Value value, _ValueComparer comparer)
		{
			return this.binary.Equals(value, comparer);
		}

		// Token: 0x06007CA4 RID: 31908 RVA: 0x001AC0FF File Offset: 0x001AA2FF
		public override bool Equals(BufferedBinaryValue value)
		{
			return this.binary.Equals(value);
		}

		// Token: 0x06007CA5 RID: 31909 RVA: 0x001AC10D File Offset: 0x001AA30D
		public override bool Equals(StreamedBinaryValue value)
		{
			return this.binary.Equals(value);
		}

		// Token: 0x06007CA6 RID: 31910 RVA: 0x001AC11B File Offset: 0x001AA31B
		public override int GetHashCode(_ValueComparer comparer)
		{
			return this.binary.GetHashCode(comparer);
		}

		// Token: 0x040044CC RID: 17612
		private readonly BinaryValue binary;
	}
}
