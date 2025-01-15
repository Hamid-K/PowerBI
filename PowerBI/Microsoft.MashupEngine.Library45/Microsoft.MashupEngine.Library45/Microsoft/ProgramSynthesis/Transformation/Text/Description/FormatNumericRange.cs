using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C8C RID: 7308
	public class FormatNumericRange : TransformationDescription
	{
		// Token: 0x0600F765 RID: 63333 RVA: 0x0034B9E0 File Offset: 0x00349BE0
		internal FormatNumericRange(FormatNumericRange programNode, string columnName)
			: this(programNode.Node, (NumberFormat)programNode.numberFormat.Node.Invoke(null), programNode.s.Value, programNode.roundingSpec1.Value, programNode.roundingSpec2.Value, columnName)
		{
		}

		// Token: 0x0600F766 RID: 63334 RVA: 0x0034BA42 File Offset: 0x00349C42
		internal FormatNumericRange(ProgramNode programNode, NumberFormat numberFormat, string delimiter, RoundingSpec lowerRoundingSpec, RoundingSpec upperRoundingSpec, string columnName)
			: base(programNode, columnName, TransformationCategory.Mutation, TransformationKind.FormatNumericRange)
		{
			this.Format = numberFormat;
			this.Delimiter = delimiter;
			this.LowerRoundingSpec = lowerRoundingSpec;
			this.UpperRoundingSpec = upperRoundingSpec;
		}

		// Token: 0x17002942 RID: 10562
		// (get) Token: 0x0600F767 RID: 63335 RVA: 0x0034BA71 File Offset: 0x00349C71
		public NumberFormat Format { get; }

		// Token: 0x17002943 RID: 10563
		// (get) Token: 0x0600F768 RID: 63336 RVA: 0x0034BA79 File Offset: 0x00349C79
		public string Delimiter { get; }

		// Token: 0x17002944 RID: 10564
		// (get) Token: 0x0600F769 RID: 63337 RVA: 0x0034BA81 File Offset: 0x00349C81
		public RoundingSpec LowerRoundingSpec { get; }

		// Token: 0x17002945 RID: 10565
		// (get) Token: 0x0600F76A RID: 63338 RVA: 0x0034BA89 File Offset: 0x00349C89
		public RoundingSpec UpperRoundingSpec { get; }
	}
}
