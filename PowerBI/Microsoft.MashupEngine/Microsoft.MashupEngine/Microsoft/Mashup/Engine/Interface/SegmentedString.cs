using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000116 RID: 278
	public struct SegmentedString : IEquatable<SegmentedString>
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x000066BA File Offset: 0x000048BA
		public static SegmentedString New(string text)
		{
			return SegmentedString.New(text, 0, (text == null) ? 0 : text.Length);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000066D0 File Offset: 0x000048D0
		public static SegmentedString New(string text, int offset, int length)
		{
			if (text == null)
			{
				if (length != 0 || offset < 0)
				{
					throw new IndexOutOfRangeException();
				}
				return default(SegmentedString);
			}
			else
			{
				if (offset < 0)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (length < 0)
				{
					throw new ArgumentOutOfRangeException("length");
				}
				return SegmentedString.New(new StringSegment[]
				{
					new StringSegment(text, offset, length)
				});
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00006730 File Offset: 0x00004930
		public static SegmentedString New(IEnumerable<StringSegment> stringSegments)
		{
			int num = 0;
			List<SegmentedString.Segment> list = new List<SegmentedString.Segment>();
			using (IEnumerator<StringSegment> enumerator = stringSegments.GetEnumerator())
			{
				bool flag = enumerator.MoveNext();
				while (flag)
				{
					SegmentedString.Segment segment = SegmentedString.GetNextSegmentToAdd(enumerator, 32768, ref num, out flag);
					if (segment.Length == 32768 || !flag)
					{
						list.Add(segment);
					}
					else
					{
						StringBuilder stringBuilder = new StringBuilder(32768);
						stringBuilder.Append(segment.String, segment.Offset, segment.Length);
						int num2 = 32768 - segment.Length;
						while (num2 > 0 && flag)
						{
							segment = SegmentedString.GetNextSegmentToAdd(enumerator, num2, ref num, out flag);
							stringBuilder.Append(segment.String, segment.Offset, segment.Length);
							num2 -= segment.Length;
						}
						list.Add(new SegmentedString.Segment(stringBuilder.ToString(), 0, stringBuilder.Length));
					}
				}
			}
			if (list.Count == 0 || (list.Count == 1 && list[0].Length == 0))
			{
				return new SegmentedString(new SegmentedString.Segment[0], 0);
			}
			int num3 = 32768 * (list.Count - 1) + list[list.Count - 1].Length;
			return new SegmentedString(list.ToArray(), num3);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0000689C File Offset: 0x00004A9C
		private SegmentedString(SegmentedString.Segment[] segments, int length)
		{
			this.segments = segments;
			this.hash = 0;
			this.length = length;
		}

		// Token: 0x170001B5 RID: 437
		public char this[int index]
		{
			get
			{
				SegmentedString.Segment segment = this.segments[index >> 15];
				return segment.String[segment.Offset + (index & 32767)];
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x000068EA File Offset: 0x00004AEA
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000068F2 File Offset: 0x00004AF2
		public int SegmentCount
		{
			get
			{
				return this.segments.Length;
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000068FC File Offset: 0x00004AFC
		public bool Equals(SegmentedString other)
		{
			if (this.segments == null || other.segments == null)
			{
				return this.segments == null && other.segments == null;
			}
			if (this.length != other.length)
			{
				return false;
			}
			return this.segments.Select((SegmentedString.Segment segment, int i) => string.CompareOrdinal(segment.String, segment.Offset, other.segments[i].String, other.segments[i].Offset, segment.Length)).All((int comparisonResult) => comparisonResult == 0);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00006994 File Offset: 0x00004B94
		public override bool Equals(object obj)
		{
			return obj is SegmentedString && this.Equals((SegmentedString)obj);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000069AC File Offset: 0x00004BAC
		public override int GetHashCode()
		{
			if (this.hash == 0)
			{
				int num = 0;
				if (this.segments != null)
				{
					foreach (SegmentedString.Segment segment in this.segments)
					{
						int num2 = segment.Offset + segment.Length;
						string @string = segment.String;
						for (int j = segment.Offset; j < num2; j++)
						{
							num = ((num << 7) + num) ^ (int)@string[j];
						}
					}
					num ^= 7295 ^ this.length;
				}
				if (num == 0)
				{
					num = 1;
				}
				this.hash = num;
			}
			return this.hash;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00006A48 File Offset: 0x00004C48
		public StringSegment GetSegment(int index)
		{
			SegmentedString.Segment segment = this.segments[index];
			return new StringSegment(segment.String, segment.Offset, segment.Length);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00006A7C File Offset: 0x00004C7C
		public override string ToString()
		{
			if (this.segments == null)
			{
				return null;
			}
			if (this.segments.Length == 0)
			{
				return string.Empty;
			}
			if (this.segments.Length == 1)
			{
				SegmentedString.Segment segment = this.segments[0];
				return segment.String.Substring(segment.Offset, segment.Length);
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SegmentedString.Segment segment2 in this.segments)
			{
				stringBuilder.Append(segment2.String, segment2.Offset, segment2.Length);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00006B18 File Offset: 0x00004D18
		public static bool operator ==(SegmentedString left, SegmentedString right)
		{
			return left.Equals(right);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00006B22 File Offset: 0x00004D22
		public static bool operator !=(SegmentedString left, SegmentedString right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00006B2F File Offset: 0x00004D2F
		public static SegmentedString operator +(SegmentedString left, SegmentedString right)
		{
			if (!object.Equals(left, null))
			{
				return left.Add(right);
			}
			return right;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00006B4C File Offset: 0x00004D4C
		private SegmentedString Add(SegmentedString other)
		{
			if (this.length == 0)
			{
				if (other.segments != null)
				{
					return other;
				}
				return SegmentedString.New(string.Empty);
			}
			else
			{
				if (other.length == 0)
				{
					return this;
				}
				return SegmentedString.New(from segment in this.segments.Concat(other.segments)
					select new StringSegment(segment.String, segment.Offset, segment.Length));
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00006BC0 File Offset: 0x00004DC0
		private static SegmentedString.Segment GetNextSegmentToAdd(IEnumerator<StringSegment> apiSegment, int maxLength, ref int apiSegmentOffset, out bool hasMoreSegments)
		{
			StringSegment stringSegment = apiSegment.Current;
			string @string = stringSegment.String;
			stringSegment = apiSegment.Current;
			int num = stringSegment.Offset + apiSegmentOffset;
			int num2 = 0;
			hasMoreSegments = true;
			int num6;
			do
			{
				int num3 = maxLength - num2;
				stringSegment = apiSegment.Current;
				int num4 = Math.Min(num3, stringSegment.Length - apiSegmentOffset);
				apiSegmentOffset += num4;
				num2 += num4;
				int num5 = apiSegmentOffset;
				stringSegment = apiSegment.Current;
				if (num5 == stringSegment.Length)
				{
					hasMoreSegments = apiSegment.MoveNext();
					apiSegmentOffset = 0;
				}
				if (!hasMoreSegments || num2 >= maxLength)
				{
					break;
				}
				string text = @string;
				stringSegment = apiSegment.Current;
				if (!(text == stringSegment.String))
				{
					break;
				}
				num6 = num + num2;
				stringSegment = apiSegment.Current;
			}
			while (num6 == stringSegment.Offset);
			return new SegmentedString.Segment(@string, num, num2);
		}

		// Token: 0x040002A7 RID: 679
		public const int LogSegmentLength = 15;

		// Token: 0x040002A8 RID: 680
		public const int SegmentLength = 32768;

		// Token: 0x040002A9 RID: 681
		public const int MaskInternalIndex = 32767;

		// Token: 0x040002AA RID: 682
		private SegmentedString.Segment[] segments;

		// Token: 0x040002AB RID: 683
		private int length;

		// Token: 0x040002AC RID: 684
		private int hash;

		// Token: 0x02000117 RID: 279
		private struct Segment
		{
			// Token: 0x060004B3 RID: 1203 RVA: 0x00006C70 File Offset: 0x00004E70
			public Segment(string text, int offset, int length)
			{
				this.String = text;
				this.Offset = offset;
				this.Length = length;
			}

			// Token: 0x040002AD RID: 685
			public readonly string String;

			// Token: 0x040002AE RID: 686
			public readonly int Offset;

			// Token: 0x040002AF RID: 687
			public readonly int Length;
		}
	}
}
