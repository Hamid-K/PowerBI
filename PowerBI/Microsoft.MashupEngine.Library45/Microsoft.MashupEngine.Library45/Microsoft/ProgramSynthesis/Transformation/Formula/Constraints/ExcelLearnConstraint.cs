using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019CF RID: 6607
	public class ExcelLearnConstraint : LearnConstraint
	{
		// Token: 0x0600D78D RID: 55181 RVA: 0x002DD184 File Offset: 0x002DB384
		public ExcelLearnConstraint()
		{
			base.EnableMatchNames = (MatchName)(-2147483136);
			base.EnableParseDateTimePartial = false;
			base.EnableFromNumberStr = true;
			base.EnableTrim = false;
		}

		// Token: 0x170023D7 RID: 9175
		// (get) Token: 0x0600D78E RID: 55182 RVA: 0x002DD1DC File Offset: 0x002DB3DC
		// (set) Token: 0x0600D78F RID: 55183 RVA: 0x002DD1E4 File Offset: 0x002DB3E4
		public bool EnableForwardFill { get; set; } = true;

		// Token: 0x170023D8 RID: 9176
		// (get) Token: 0x0600D790 RID: 55184 RVA: 0x002DD1ED File Offset: 0x002DB3ED
		// (set) Token: 0x0600D791 RID: 55185 RVA: 0x002DD1F5 File Offset: 0x002DB3F5
		public bool EnableFromNumberCoalesced { get; set; }

		// Token: 0x170023D9 RID: 9177
		// (get) Token: 0x0600D792 RID: 55186 RVA: 0x002DD1FE File Offset: 0x002DB3FE
		// (set) Token: 0x0600D793 RID: 55187 RVA: 0x002DD206 File Offset: 0x002DB406
		public bool EnableTrimFull { get; set; } = true;

		// Token: 0x170023DA RID: 9178
		// (get) Token: 0x0600D794 RID: 55188 RVA: 0x002DD20F File Offset: 0x002DB40F
		// (set) Token: 0x0600D795 RID: 55189 RVA: 0x002DD217 File Offset: 0x002DB417
		public int ForwardFillMaxExampleCount { get; set; } = 100;

		// Token: 0x170023DB RID: 9179
		// (get) Token: 0x0600D796 RID: 55190 RVA: 0x002DD220 File Offset: 0x002DB420
		// (set) Token: 0x0600D797 RID: 55191 RVA: 0x002DD228 File Offset: 0x002DB428
		public int ForwardFillMaxScale { get; set; } = 1;

		// Token: 0x170023DC RID: 9180
		// (get) Token: 0x0600D798 RID: 55192 RVA: 0x002DD231 File Offset: 0x002DB431
		// (set) Token: 0x0600D799 RID: 55193 RVA: 0x002DD239 File Offset: 0x002DB439
		public int ForwardFillMinExampleCount { get; set; } = 3;

		// Token: 0x0600D79A RID: 55194 RVA: 0x002DD244 File Offset: 0x002DB444
		public override void SetOptions(LearnOptions options)
		{
			base.SetOptions(options);
			options.EnableForwardFill = this.EnableForwardFill;
			options.EnableFromNumberCoalesced = this.EnableFromNumberCoalesced;
			options.ForwardFillMaxExampleCount = this.ForwardFillMaxExampleCount;
			options.EnableTrimFull = this.EnableTrimFull;
			options.ForwardFillMaxScale = this.ForwardFillMaxScale;
			options.ForwardFillMinExampleCount = this.ForwardFillMinExampleCount;
		}

		// Token: 0x0600D79B RID: 55195 RVA: 0x002DD2A0 File Offset: 0x002DB4A0
		internal override string ToEqualString()
		{
			return string.Concat(new string[]
			{
				base.ToEqualString(),
				string.Format(" {0}={1};", "EnableForwardFill", this.EnableForwardFill),
				string.Format(" {0}={1};", "EnableFromNumberCoalesced", this.EnableFromNumberCoalesced),
				string.Format(" {0}={1};", "ForwardFillMaxExampleCount", this.ForwardFillMaxExampleCount),
				string.Format(" {0}={1};", "ForwardFillMaxScale", this.ForwardFillMaxScale),
				string.Format(" {0}={1};", "ForwardFillMinExampleCount", this.ForwardFillMinExampleCount),
				string.Format(" {0}={1};", "EnableTrimFull", this.EnableTrimFull),
				string.Format(" {0}={1};", "EnableConditional", base.EnableConditional)
			});
		}
	}
}
