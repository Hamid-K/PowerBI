using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200005B RID: 91
	internal struct StringSequence2 : IEquatable<StringSequence2>, IStringSequence
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000AF47 File Offset: 0x00009147
		public StringSequence2(string value1, string value2)
		{
			this.Value1 = value1;
			this.Value2 = value2;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000AF57 File Offset: 0x00009157
		public bool Equals(StringSequence2 other)
		{
			return this.Value1 == other.Value1 && this.Value2 == other.Value2;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000AF80 File Offset: 0x00009180
		public override bool Equals(object obj)
		{
			if (obj is StringSequence2)
			{
				StringSequence2 stringSequence = (StringSequence2)obj;
				return this.Equals(stringSequence);
			}
			return false;
		}

		// Token: 0x17000094 RID: 148
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
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000AFDF File Offset: 0x000091DF
		public int Length
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000AFE2 File Offset: 0x000091E2
		public override int GetHashCode()
		{
			string value = this.Value1;
			int num = ((value != null) ? value.GetHashCode() : 0);
			string value2 = this.Value2;
			return num ^ ((value2 != null) ? value2.GetHashCode() : 0);
		}

		// Token: 0x04000124 RID: 292
		public string Value1;

		// Token: 0x04000125 RID: 293
		public string Value2;
	}
}
