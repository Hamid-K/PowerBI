using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200005C RID: 92
	internal struct StringSequence3 : IEquatable<StringSequence3>, IStringSequence
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x0000B009 File Offset: 0x00009209
		public StringSequence3(string value1, string value2, string value3)
		{
			this.Value1 = value1;
			this.Value2 = value2;
			this.Value3 = value3;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B020 File Offset: 0x00009220
		public bool Equals(StringSequence3 other)
		{
			return this.Value1 == other.Value1 && this.Value2 == other.Value2 && this.Value3 == other.Value3;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B05C File Offset: 0x0000925C
		public override bool Equals(object obj)
		{
			if (obj is StringSequence3)
			{
				StringSequence3 stringSequence = (StringSequence3)obj;
				return this.Equals(stringSequence);
			}
			return false;
		}

		// Token: 0x17000096 RID: 150
		public string this[int i]
		{
			get
			{
				if (i == 0)
				{
					return this.Value1;
				}
				if (i == 1)
				{
					return this.Value2;
				}
				if (i == 2)
				{
					return this.Value3;
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
				if (i == 1)
				{
					this.Value2 = value;
					return;
				}
				if (i == 2)
				{
					this.Value3 = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000B0D2 File Offset: 0x000092D2
		public int Length
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B0D5 File Offset: 0x000092D5
		public override int GetHashCode()
		{
			string value = this.Value1;
			int num = ((value != null) ? value.GetHashCode() : 0);
			string value2 = this.Value2;
			int num2 = num ^ ((value2 != null) ? value2.GetHashCode() : 0);
			string value3 = this.Value3;
			return num2 ^ ((value3 != null) ? value3.GetHashCode() : 0);
		}

		// Token: 0x04000126 RID: 294
		public string Value1;

		// Token: 0x04000127 RID: 295
		public string Value2;

		// Token: 0x04000128 RID: 296
		public string Value3;
	}
}
