using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200129D RID: 4765
	internal class CharacterRangeListValue : BufferedListValue
	{
		// Token: 0x06007D22 RID: 32034 RVA: 0x001AD2DC File Offset: 0x001AB4DC
		public CharacterRangeListValue(char lower, char upper)
		{
			this.lower = lower;
			this.count = Math.Max(0, (int)(upper - lower + '\u0001'));
		}

		// Token: 0x17002208 RID: 8712
		// (get) Token: 0x06007D23 RID: 32035 RVA: 0x001AD2FC File Offset: 0x001AB4FC
		public override int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17002209 RID: 8713
		public override Value this[int index]
		{
			get
			{
				if (index >= 0 && index < this.count)
				{
					return TextValue.New((char)((int)this.lower + index));
				}
				return base[index];
			}
		}

		// Token: 0x040044F5 RID: 17653
		private char lower;

		// Token: 0x040044F6 RID: 17654
		private int count;
	}
}
