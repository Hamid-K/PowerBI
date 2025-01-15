using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Split.Text.Semantics
{
	// Token: 0x02001398 RID: 5016
	public class SplitCell : ICell<StringRegion>, IEquatable<SplitCell>
	{
		// Token: 0x06009BCE RID: 39886 RVA: 0x0020EBC7 File Offset: 0x0020CDC7
		public SplitCell(StringRegion cellValue, bool isDelimiter)
		{
			this.CellValue = cellValue;
			this.IsDelimiter = isDelimiter;
		}

		// Token: 0x17001AB3 RID: 6835
		// (get) Token: 0x06009BCF RID: 39887 RVA: 0x0020EBDD File Offset: 0x0020CDDD
		public static SplitCell Empty { get; } = new SplitCell(null, false);

		// Token: 0x17001AB4 RID: 6836
		// (get) Token: 0x06009BD0 RID: 39888 RVA: 0x0020EBE4 File Offset: 0x0020CDE4
		public StringRegion CellValue { get; }

		// Token: 0x17001AB5 RID: 6837
		// (get) Token: 0x06009BD1 RID: 39889 RVA: 0x0020EBEC File Offset: 0x0020CDEC
		public bool IsDelimiter { get; }

		// Token: 0x06009BD2 RID: 39890 RVA: 0x0020EBF4 File Offset: 0x0020CDF4
		public bool Equals(SplitCell other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			StringRegion cellValue = this.CellValue;
			string text = ((cellValue != null) ? cellValue.Value : null);
			StringRegion cellValue2 = other.CellValue;
			return text == ((cellValue2 != null) ? cellValue2.Value : null) && this.IsDelimiter == other.IsDelimiter;
		}

		// Token: 0x06009BD3 RID: 39891 RVA: 0x0020EC47 File Offset: 0x0020CE47
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((SplitCell)obj)));
		}

		// Token: 0x06009BD4 RID: 39892 RVA: 0x0020EC75 File Offset: 0x0020CE75
		public override int GetHashCode()
		{
			return (391 + ((this.CellValue == null) ? 0 : this.CellValue.GetHashCode())) * 23 + ((!this.IsDelimiter) ? 1 : 0);
		}
	}
}
