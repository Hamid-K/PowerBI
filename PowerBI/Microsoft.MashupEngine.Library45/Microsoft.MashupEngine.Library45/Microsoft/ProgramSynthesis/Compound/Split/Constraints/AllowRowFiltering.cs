using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Constraints
{
	// Token: 0x020009EB RID: 2539
	public class AllowRowFiltering : Constraint<StringRegion, ITable<StringRegion>>
	{
		// Token: 0x06003D49 RID: 15689 RVA: 0x000C00B8 File Offset: 0x000BE2B8
		public AllowRowFiltering(int maxRowPrefixRegexTokens = 3)
		{
			this.MaxRowPrefixRegexTokens = maxRowPrefixRegexTokens;
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06003D4A RID: 15690 RVA: 0x000C00C7 File Offset: 0x000BE2C7
		public int MaxRowPrefixRegexTokens { get; }

		// Token: 0x06003D4B RID: 15691 RVA: 0x000C00D0 File Offset: 0x000BE2D0
		public override bool Equals(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			AllowRowFiltering allowRowFiltering = other as AllowRowFiltering;
			return allowRowFiltering != null && this.MaxRowPrefixRegexTokens == allowRowFiltering.MaxRowPrefixRegexTokens;
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x000C00F7 File Offset: 0x000BE2F7
		public override bool ConflictsWith(Constraint<StringRegion, ITable<StringRegion>> other)
		{
			return other is AllowRowFiltering || other is IgnoreSelectData || other is IgnoreFilterHeader;
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x000C0114 File Offset: 0x000BE314
		public override bool Valid(Program<StringRegion, ITable<StringRegion>> program)
		{
			Program program2 = program as Program;
			if (program2 != null)
			{
				ProgramProperties properties = program2.Properties;
				int? num;
				if (properties == null)
				{
					num = null;
				}
				else
				{
					RegularExpression dataRegex = properties.DataRegex;
					num = ((dataRegex != null) ? new int?(dataRegex.Count) : null);
				}
				int? num2 = num;
				int valueOrDefault = num2.GetValueOrDefault();
				ProgramProperties properties2 = program2.Properties;
				int? num3;
				if (properties2 == null)
				{
					num3 = null;
				}
				else
				{
					RegularExpression headerRegex = properties2.HeaderRegex;
					num3 = ((headerRegex != null) ? new int?(headerRegex.Count) : null);
				}
				num2 = num3;
				return Math.Max(valueOrDefault, num2.GetValueOrDefault()) <= this.MaxRowPrefixRegexTokens;
			}
			return false;
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x000C01B6 File Offset: 0x000BE3B6
		public override int GetHashCode()
		{
			return 839 ^ this.MaxRowPrefixRegexTokens;
		}

		// Token: 0x04001CB8 RID: 7352
		private const int DefaultRowPrefixRegexTokens = 3;
	}
}
