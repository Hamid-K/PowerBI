using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200005A RID: 90
	internal struct StringSequence1 : IEquatable<StringSequence1>, IStringSequence
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000AED3 File Offset: 0x000090D3
		public StringSequence1(string value1)
		{
			this.Value1 = value1;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000AEDC File Offset: 0x000090DC
		public override int GetHashCode()
		{
			return this.Value1.GetHashCode();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000AEE9 File Offset: 0x000090E9
		public bool Equals(StringSequence1 other)
		{
			return this.Value1 == other.Value1;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000AEFC File Offset: 0x000090FC
		public override bool Equals(object obj)
		{
			if (obj is StringSequence1)
			{
				StringSequence1 stringSequence = (StringSequence1)obj;
				return this.Equals(stringSequence);
			}
			return false;
		}

		// Token: 0x17000092 RID: 146
		public string this[int i]
		{
			get
			{
				if (i == 0)
				{
					return this.Value1;
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (i == 0)
				{
					this.Value1 = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000AF44 File Offset: 0x00009144
		public int Length
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x04000123 RID: 291
		public string Value1;
	}
}
