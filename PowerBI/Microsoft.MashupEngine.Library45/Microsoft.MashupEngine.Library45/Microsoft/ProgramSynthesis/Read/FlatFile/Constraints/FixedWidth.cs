using System;
using Microsoft.ProgramSynthesis.Read.FlatFile.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Constraints
{
	// Token: 0x020012E1 RID: 4833
	public class FixedWidth : FixedWidthConstraint
	{
		// Token: 0x060091C4 RID: 37316 RVA: 0x001EB434 File Offset: 0x001E9634
		public FixedWidth(string schema = null)
		{
			this.Schema = schema;
		}

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x060091C5 RID: 37317 RVA: 0x001EB443 File Offset: 0x001E9643
		public string Schema { get; }

		// Token: 0x060091C6 RID: 37318 RVA: 0x001EB44B File Offset: 0x001E964B
		public override void SetOptions(Options options)
		{
			base.SetOptions(options);
			options.FwSchema = this.Schema;
		}

		// Token: 0x060091C7 RID: 37319 RVA: 0x001EB460 File Offset: 0x001E9660
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			FixedWidth fixedWidth = other as FixedWidth;
			return fixedWidth != null && this.Schema == fixedWidth.Schema;
		}

		// Token: 0x060091C8 RID: 37320 RVA: 0x001EB48A File Offset: 0x001E968A
		public override int GetHashCode()
		{
			if (this.Schema != null)
			{
				return this.Schema.GetHashCode();
			}
			return 2273;
		}
	}
}
