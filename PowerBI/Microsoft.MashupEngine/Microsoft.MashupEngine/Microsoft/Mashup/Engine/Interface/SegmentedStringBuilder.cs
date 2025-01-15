using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200011A RID: 282
	public struct SegmentedStringBuilder
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x00006D04 File Offset: 0x00004F04
		public static SegmentedStringBuilder New()
		{
			return new SegmentedStringBuilder
			{
				segments = new List<StringSegment>(),
				length = 0
			};
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00006D2E File Offset: 0x00004F2E
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00006D36 File Offset: 0x00004F36
		public SegmentedStringBuilder Append(SegmentedString segmentedString)
		{
			return this.Append(segmentedString, 0);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00006D40 File Offset: 0x00004F40
		public SegmentedStringBuilder Append(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return this;
			}
			return this.Append(text, 0, text.Length);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00006D60 File Offset: 0x00004F60
		public SegmentedStringBuilder Append(SegmentedString segmentedString, int offset)
		{
			foreach (StringSegment stringSegment in segmentedString.GetSubstringSegments(offset, segmentedString.Length - offset))
			{
				this.Append(stringSegment.String, stringSegment.Offset, stringSegment.Length);
			}
			return this;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00006DD4 File Offset: 0x00004FD4
		public SegmentedStringBuilder Append(SegmentedString segmentedString, int offset, int length)
		{
			foreach (StringSegment stringSegment in segmentedString.GetSubstringSegments(offset, length))
			{
				this.Append(stringSegment.String, stringSegment.Offset, stringSegment.Length);
			}
			return this;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00006E40 File Offset: 0x00005040
		public SegmentedStringBuilder Append(char ch)
		{
			return this.Append(new string(new char[] { ch }));
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00006E57 File Offset: 0x00005057
		public SegmentedStringBuilder Append(string text, int offset, int length)
		{
			if (length == 0)
			{
				return this;
			}
			this.segments.Add(new StringSegment(text, offset, length));
			this.length += length;
			return this;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00006E8A File Offset: 0x0000508A
		public SegmentedStringBuilder Append(StringSegment segment)
		{
			return this.Append(segment.String, segment.Offset, segment.Length);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00006EA7 File Offset: 0x000050A7
		public SegmentedStringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
		{
			return this.Append(string.Format(provider, format, args));
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00006EB7 File Offset: 0x000050B7
		public SegmentedStringBuilder AppendLine(string line = null)
		{
			this.Append(line);
			return this.Append(Environment.NewLine);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00006ECC File Offset: 0x000050CC
		public SegmentedString ToSegmentedString()
		{
			return SegmentedString.New(this.segments);
		}

		// Token: 0x040002B4 RID: 692
		private List<StringSegment> segments;

		// Token: 0x040002B5 RID: 693
		private int length;
	}
}
