using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200011E RID: 286
	public struct StringSegment : IEquatable<StringSegment>
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x00007472 File Offset: 0x00005672
		public StringSegment(string text)
		{
			this = new StringSegment(text, 0, text.Length);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00007482 File Offset: 0x00005682
		public StringSegment(string text, int offset, int length)
		{
			this.text = text;
			this.offset = offset;
			this.length = length;
			this.hash = 0;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x000074A0 File Offset: 0x000056A0
		public string String
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x000074A8 File Offset: 0x000056A8
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x000074B0 File Offset: 0x000056B0
		public int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x170001C0 RID: 448
		public char this[int index]
		{
			get
			{
				if (index < 0 || index >= this.length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.text[this.offset + index];
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000074E5 File Offset: 0x000056E5
		public bool Equals(string other)
		{
			return this.length == other.Length && string.CompareOrdinal(this.text, this.offset, other, 0, this.length) == 0;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00007513 File Offset: 0x00005713
		public bool Equals(StringSegment other)
		{
			return this.length == other.length && string.CompareOrdinal(this.text, this.offset, other.text, other.offset, this.length) == 0;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000754C File Offset: 0x0000574C
		public override bool Equals(object obj)
		{
			if (obj is StringSegment)
			{
				return this.Equals((StringSegment)obj);
			}
			string text = obj as string;
			return text != null && this.Equals(text);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00007584 File Offset: 0x00005784
		public override int GetHashCode()
		{
			if (this.hash == 0)
			{
				int num = 5381 ^ this.length;
				for (int i = 0; i < this.length; i++)
				{
					num = ((num << 5) + num) ^ (int)this.text[this.offset + i];
				}
				if (num == 0)
				{
					num = 1;
				}
				this.hash = num;
			}
			return this.hash;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000075E3 File Offset: 0x000057E3
		public override string ToString()
		{
			return this.text.Substring(this.offset, this.length);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000075FC File Offset: 0x000057FC
		public static bool operator ==(StringSegment left, string right)
		{
			return left.Equals(right);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00007606 File Offset: 0x00005806
		public static bool operator !=(StringSegment left, string right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00007613 File Offset: 0x00005813
		public static bool operator ==(string left, StringSegment right)
		{
			return right.Equals(left);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000761D File Offset: 0x0000581D
		public static bool operator !=(string left, StringSegment right)
		{
			return !right.Equals(left);
		}

		// Token: 0x040002C4 RID: 708
		private string text;

		// Token: 0x040002C5 RID: 709
		private int offset;

		// Token: 0x040002C6 RID: 710
		private int length;

		// Token: 0x040002C7 RID: 711
		private int hash;
	}
}
