using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Azure.Core;

namespace Azure
{
	// Token: 0x02000020 RID: 32
	public readonly struct HttpRange : IEquatable<HttpRange>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000268B File Offset: 0x0000088B
		public long Offset { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002693 File Offset: 0x00000893
		public long? Length { get; }

		// Token: 0x06000061 RID: 97 RVA: 0x0000269C File Offset: 0x0000089C
		public HttpRange(long offset = 0L, long? length = null)
		{
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (length != null)
			{
				long? num = length;
				long num2 = 0L;
				if ((num.GetValueOrDefault() <= num2) & (num != null))
				{
					throw new ArgumentOutOfRangeException("length");
				}
			}
			this.Offset = offset;
			this.Length = length;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000026F8 File Offset: 0x000008F8
		[NullableContext(1)]
		public override string ToString()
		{
			if (this.Length != null)
			{
				long? length = this.Length;
				long num = 0L;
				if (!((length.GetValueOrDefault() == num) & (length != null)))
				{
					long num2 = this.Offset + this.Length.Value - 1L;
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}={1}-{2}", new object[] { "bytes", this.Offset, num2 }));
				}
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}={1}-", new object[] { "bytes", this.Offset }));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000027AC File Offset: 0x000009AC
		public static bool operator ==(HttpRange left, HttpRange right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000027B6 File Offset: 0x000009B6
		public static bool operator !=(HttpRange left, HttpRange right)
		{
			return !(left == right);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000027C4 File Offset: 0x000009C4
		public bool Equals(HttpRange other)
		{
			if (this.Offset == other.Offset)
			{
				long? length = this.Length;
				long? length2 = other.Length;
				return (length.GetValueOrDefault() == length2.GetValueOrDefault()) & (length != null == (length2 != null));
			}
			return false;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002814 File Offset: 0x00000A14
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			if (obj is HttpRange)
			{
				HttpRange httpRange = (HttpRange)obj;
				return this.Equals(httpRange);
			}
			return false;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002839 File Offset: 0x00000A39
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return HashCodeBuilder.Combine<long, long?>(this.Offset, this.Length);
		}

		// Token: 0x0400003A RID: 58
		[Nullable(1)]
		private const string Unit = "bytes";
	}
}
