using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x02000037 RID: 55
	[GeneratedCode("App_Packages", "")]
	internal struct HeaderSegmentCollection : IEnumerable<HeaderSegment>, IEnumerable, IEquatable<HeaderSegmentCollection>
	{
		// Token: 0x0600020D RID: 525 RVA: 0x000059CB File Offset: 0x00003BCB
		public HeaderSegmentCollection(string[] headers)
		{
			this._headers = headers;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000059D4 File Offset: 0x00003BD4
		public bool Equals(HeaderSegmentCollection other)
		{
			return object.Equals(this._headers, other._headers);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000059E7 File Offset: 0x00003BE7
		public override bool Equals(object obj)
		{
			return obj != null && obj is HeaderSegmentCollection && this.Equals((HeaderSegmentCollection)obj);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00005A04 File Offset: 0x00003C04
		public override int GetHashCode()
		{
			if (this._headers == null)
			{
				return 0;
			}
			return this._headers.GetHashCode();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00005A1B File Offset: 0x00003C1B
		public static bool operator ==(HeaderSegmentCollection left, HeaderSegmentCollection right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00005A25 File Offset: 0x00003C25
		public static bool operator !=(HeaderSegmentCollection left, HeaderSegmentCollection right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00005A32 File Offset: 0x00003C32
		public HeaderSegmentCollection.Enumerator GetEnumerator()
		{
			return new HeaderSegmentCollection.Enumerator(this._headers);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00005A3F File Offset: 0x00003C3F
		IEnumerator<HeaderSegment> IEnumerable<HeaderSegment>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00005A4C File Offset: 0x00003C4C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000068 RID: 104
		private readonly string[] _headers;

		// Token: 0x02000066 RID: 102
		internal struct Enumerator : IEnumerator<HeaderSegment>, IDisposable, IEnumerator
		{
			// Token: 0x060002E6 RID: 742 RVA: 0x00008288 File Offset: 0x00006488
			public Enumerator(string[] headers)
			{
				this._headers = headers ?? HeaderSegmentCollection.Enumerator.NoHeaders;
				this._header = string.Empty;
				this._headerLength = -1;
				this._index = -1;
				this._offset = -1;
				this._leadingStart = -1;
				this._leadingEnd = -1;
				this._valueStart = -1;
				this._valueEnd = -1;
				this._trailingStart = -1;
				this._mode = HeaderSegmentCollection.Enumerator.Mode.Leading;
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x060002E7 RID: 743 RVA: 0x000082F0 File Offset: 0x000064F0
			public HeaderSegment Current
			{
				get
				{
					return new HeaderSegment(new StringSegment(this._header, this._leadingStart, this._leadingEnd - this._leadingStart), new StringSegment(this._header, this._valueStart, this._valueEnd - this._valueStart));
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000833E File Offset: 0x0000653E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060002E9 RID: 745 RVA: 0x0000834B File Offset: 0x0000654B
			public void Dispose()
			{
			}

			// Token: 0x060002EA RID: 746 RVA: 0x00008350 File Offset: 0x00006550
			public bool MoveNext()
			{
				if (this._mode == HeaderSegmentCollection.Enumerator.Mode.Produce)
				{
					this._leadingStart = this._trailingStart;
					this._leadingEnd = -1;
					this._valueStart = -1;
					this._valueEnd = -1;
					this._trailingStart = -1;
					if (this._offset == this._headerLength && this._leadingStart != -1 && this._leadingStart != this._offset)
					{
						this._leadingEnd = this._offset;
						return true;
					}
					this._mode = HeaderSegmentCollection.Enumerator.Mode.Leading;
				}
				if (this._offset == this._headerLength)
				{
					this._index++;
					this._offset = -1;
					this._leadingStart = 0;
					this._leadingEnd = -1;
					this._valueStart = -1;
					this._valueEnd = -1;
					this._trailingStart = -1;
					if (this._index == this._headers.Length)
					{
						return false;
					}
					this._header = this._headers[this._index] ?? string.Empty;
					this._headerLength = this._header.Length;
				}
				do
				{
					this._offset++;
					char ch = ((this._offset == this._headerLength) ? '\0' : this._header[this._offset]);
					HeaderSegmentCollection.Enumerator.Attr attr = (char.IsWhiteSpace(ch) ? HeaderSegmentCollection.Enumerator.Attr.Whitespace : ((ch == '"') ? HeaderSegmentCollection.Enumerator.Attr.Quote : ((ch == ',' || ch == '\0') ? HeaderSegmentCollection.Enumerator.Attr.Delimiter : HeaderSegmentCollection.Enumerator.Attr.Value)));
					switch (this._mode)
					{
					case HeaderSegmentCollection.Enumerator.Mode.Leading:
						switch (attr)
						{
						case HeaderSegmentCollection.Enumerator.Attr.Value:
							this._leadingEnd = this._offset;
							this._valueStart = this._offset;
							this._mode = HeaderSegmentCollection.Enumerator.Mode.Value;
							break;
						case HeaderSegmentCollection.Enumerator.Attr.Quote:
							this._leadingEnd = this._offset;
							this._valueStart = this._offset;
							this._mode = HeaderSegmentCollection.Enumerator.Mode.ValueQuoted;
							break;
						case HeaderSegmentCollection.Enumerator.Attr.Delimiter:
							this._leadingEnd = this._offset;
							this._mode = HeaderSegmentCollection.Enumerator.Mode.Produce;
							break;
						}
						break;
					case HeaderSegmentCollection.Enumerator.Mode.Value:
						switch (attr)
						{
						case HeaderSegmentCollection.Enumerator.Attr.Quote:
							this._mode = HeaderSegmentCollection.Enumerator.Mode.ValueQuoted;
							break;
						case HeaderSegmentCollection.Enumerator.Attr.Delimiter:
							this._valueEnd = this._offset;
							this._trailingStart = this._offset;
							this._mode = HeaderSegmentCollection.Enumerator.Mode.Produce;
							break;
						case HeaderSegmentCollection.Enumerator.Attr.Whitespace:
							this._valueEnd = this._offset;
							this._trailingStart = this._offset;
							this._mode = HeaderSegmentCollection.Enumerator.Mode.Trailing;
							break;
						}
						break;
					case HeaderSegmentCollection.Enumerator.Mode.ValueQuoted:
						switch (attr)
						{
						case HeaderSegmentCollection.Enumerator.Attr.Quote:
							this._mode = HeaderSegmentCollection.Enumerator.Mode.Value;
							break;
						case HeaderSegmentCollection.Enumerator.Attr.Delimiter:
							if (ch == '\0')
							{
								this._valueEnd = this._offset;
								this._trailingStart = this._offset;
								this._mode = HeaderSegmentCollection.Enumerator.Mode.Produce;
							}
							break;
						}
						break;
					case HeaderSegmentCollection.Enumerator.Mode.Trailing:
						switch (attr)
						{
						case HeaderSegmentCollection.Enumerator.Attr.Value:
							this._trailingStart = -1;
							this._valueEnd = -1;
							this._mode = HeaderSegmentCollection.Enumerator.Mode.Value;
							break;
						case HeaderSegmentCollection.Enumerator.Attr.Quote:
							this._trailingStart = -1;
							this._valueEnd = -1;
							this._mode = HeaderSegmentCollection.Enumerator.Mode.ValueQuoted;
							break;
						case HeaderSegmentCollection.Enumerator.Attr.Delimiter:
							this._mode = HeaderSegmentCollection.Enumerator.Mode.Produce;
							break;
						}
						break;
					}
				}
				while (this._mode != HeaderSegmentCollection.Enumerator.Mode.Produce);
				return true;
			}

			// Token: 0x060002EB RID: 747 RVA: 0x00008649 File Offset: 0x00006849
			public void Reset()
			{
				this._index = 0;
				this._offset = 0;
				this._leadingStart = 0;
				this._leadingEnd = 0;
				this._valueStart = 0;
				this._valueEnd = 0;
			}

			// Token: 0x040000F1 RID: 241
			private readonly string[] _headers;

			// Token: 0x040000F2 RID: 242
			private int _index;

			// Token: 0x040000F3 RID: 243
			private string _header;

			// Token: 0x040000F4 RID: 244
			private int _headerLength;

			// Token: 0x040000F5 RID: 245
			private int _offset;

			// Token: 0x040000F6 RID: 246
			private int _leadingStart;

			// Token: 0x040000F7 RID: 247
			private int _leadingEnd;

			// Token: 0x040000F8 RID: 248
			private int _valueStart;

			// Token: 0x040000F9 RID: 249
			private int _valueEnd;

			// Token: 0x040000FA RID: 250
			private int _trailingStart;

			// Token: 0x040000FB RID: 251
			private HeaderSegmentCollection.Enumerator.Mode _mode;

			// Token: 0x040000FC RID: 252
			private static readonly string[] NoHeaders = new string[0];

			// Token: 0x02000070 RID: 112
			private enum Mode
			{
				// Token: 0x0400011D RID: 285
				Leading,
				// Token: 0x0400011E RID: 286
				Value,
				// Token: 0x0400011F RID: 287
				ValueQuoted,
				// Token: 0x04000120 RID: 288
				Trailing,
				// Token: 0x04000121 RID: 289
				Produce
			}

			// Token: 0x02000071 RID: 113
			private enum Attr
			{
				// Token: 0x04000123 RID: 291
				Value,
				// Token: 0x04000124 RID: 292
				Quote,
				// Token: 0x04000125 RID: 293
				Delimiter,
				// Token: 0x04000126 RID: 294
				Whitespace
			}
		}
	}
}
