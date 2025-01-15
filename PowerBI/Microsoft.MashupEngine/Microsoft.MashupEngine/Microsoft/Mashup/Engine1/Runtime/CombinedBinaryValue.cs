using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001290 RID: 4752
	internal class CombinedBinaryValue : StreamedBinaryValue
	{
		// Token: 0x06007CDA RID: 31962 RVA: 0x001AC754 File Offset: 0x001AA954
		public CombinedBinaryValue(IEnumerable<IValueReference> binaries)
		{
			this.binaries = binaries;
		}

		// Token: 0x06007CDB RID: 31963 RVA: 0x001AC763 File Offset: 0x001AA963
		public override Stream Open()
		{
			return new CombinedBinaryValue.CombinedStream(this.binaries);
		}

		// Token: 0x06007CDC RID: 31964 RVA: 0x001AC770 File Offset: 0x001AA970
		public sealed override int GetHashCode(_ValueComparer comparer)
		{
			HashBuilder hashBuilder = default(HashBuilder);
			foreach (IValueReference valueReference in this.binaries)
			{
				hashBuilder.Add(valueReference.Value.GetHashCode(comparer));
			}
			return hashBuilder.ToHash();
		}

		// Token: 0x06007CDD RID: 31965 RVA: 0x001AC7D8 File Offset: 0x001AA9D8
		public override bool TryGetLength(out long length)
		{
			length = 0L;
			using (IEnumerator<IValueReference> enumerator = this.binaries.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					long num;
					if (!enumerator.Current.Value.AsBinary.TryGetLength(out num))
					{
						return false;
					}
					length += num;
					if (length > ListValue.MaxCount)
					{
						throw ValueException.ListCountTooLarge(length);
					}
				}
			}
			return true;
		}

		// Token: 0x170021F5 RID: 8693
		// (get) Token: 0x06007CDE RID: 31966 RVA: 0x001AC854 File Offset: 0x001AAA54
		public override long Length
		{
			get
			{
				long num = 0L;
				foreach (IValueReference valueReference in this.binaries)
				{
					num += valueReference.Value.AsBinary.Length;
					if (num > ListValue.MaxCount)
					{
						throw ValueException.ListCountTooLarge(num);
					}
				}
				return num;
			}
		}

		// Token: 0x040044DA RID: 17626
		private IEnumerable<IValueReference> binaries;

		// Token: 0x02001291 RID: 4753
		private class CombinedStream : Stream
		{
			// Token: 0x06007CDF RID: 31967 RVA: 0x001AC8C0 File Offset: 0x001AAAC0
			public CombinedStream(IEnumerable<IValueReference> binaries)
			{
				this.binaries = binaries.GetEnumerator();
				this.current = (this.binaries.MoveNext() ? this.binaries.Current.Value.AsBinary.Open() : null);
			}

			// Token: 0x06007CE0 RID: 31968 RVA: 0x001AC90F File Offset: 0x001AAB0F
			protected override void Dispose(bool disposing)
			{
				if (this.current != null)
				{
					this.current.Dispose();
					this.current = null;
				}
				if (this.binaries != null)
				{
					this.binaries.Dispose();
					this.binaries = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x170021F6 RID: 8694
			// (get) Token: 0x06007CE1 RID: 31969 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170021F7 RID: 8695
			// (get) Token: 0x06007CE2 RID: 31970 RVA: 0x00002105 File Offset: 0x00000305
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170021F8 RID: 8696
			// (get) Token: 0x06007CE3 RID: 31971 RVA: 0x00002105 File Offset: 0x00000305
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06007CE4 RID: 31972 RVA: 0x001AC94C File Offset: 0x001AAB4C
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num = 0;
				while (count > 0 && this.current != null)
				{
					int num2 = this.current.Read(buffer, offset, count);
					if (num2 > 0)
					{
						num += num2;
						offset += num2;
						count -= num2;
						break;
					}
					this.MoveNext();
				}
				return num;
			}

			// Token: 0x06007CE5 RID: 31973 RVA: 0x001AC994 File Offset: 0x001AAB94
			public override int ReadByte()
			{
				while (this.current != null)
				{
					int num = this.current.ReadByte();
					if (num >= 0)
					{
						return num;
					}
					this.MoveNext();
				}
				return -1;
			}

			// Token: 0x06007CE6 RID: 31974 RVA: 0x000033E7 File Offset: 0x000015E7
			public override void Flush()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170021F9 RID: 8697
			// (get) Token: 0x06007CE7 RID: 31975 RVA: 0x000033E7 File Offset: 0x000015E7
			public override long Length
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x170021FA RID: 8698
			// (get) Token: 0x06007CE8 RID: 31976 RVA: 0x000033E7 File Offset: 0x000015E7
			// (set) Token: 0x06007CE9 RID: 31977 RVA: 0x000033E7 File Offset: 0x000015E7
			public override long Position
			{
				get
				{
					throw new NotSupportedException();
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x06007CEA RID: 31978 RVA: 0x000033E7 File Offset: 0x000015E7
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06007CEB RID: 31979 RVA: 0x000033E7 File Offset: 0x000015E7
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06007CEC RID: 31980 RVA: 0x000033E7 File Offset: 0x000015E7
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06007CED RID: 31981 RVA: 0x001AC9C8 File Offset: 0x001AABC8
			private bool MoveNext()
			{
				this.current.Dispose();
				this.current = (this.binaries.MoveNext() ? this.binaries.Current.Value.AsBinary.Open() : null);
				return this.current != null;
			}

			// Token: 0x040044DB RID: 17627
			private IEnumerator<IValueReference> binaries;

			// Token: 0x040044DC RID: 17628
			private Stream current;
		}
	}
}
