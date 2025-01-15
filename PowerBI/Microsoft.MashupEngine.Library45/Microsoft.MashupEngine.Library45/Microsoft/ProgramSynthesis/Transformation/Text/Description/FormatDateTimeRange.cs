using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C8A RID: 7306
	public class FormatDateTimeRange : TransformationDescription
	{
		// Token: 0x0600F75D RID: 63325 RVA: 0x0034B8F0 File Offset: 0x00349AF0
		internal FormatDateTimeRange(FormatDateTimeRange programNode, string columnName)
			: this(programNode.Node, programNode.outputDtFormat.Value, programNode.s.Value, programNode.dtRoundingSpec1.Value, programNode.dtRoundingSpec2.Value, columnName)
		{
		}

		// Token: 0x0600F75E RID: 63326 RVA: 0x0034B947 File Offset: 0x00349B47
		internal FormatDateTimeRange(ProgramNode programNode, DateTimeFormat dateTimeFormat, string delimiter, DateTimeRoundingSpec lowerRoundingSpec, DateTimeRoundingSpec upperRoundingSpec, string columnName)
			: base(programNode, columnName, TransformationCategory.Mutation, TransformationKind.FormatDateTimeRange)
		{
			this.Format = dateTimeFormat;
			this.Delimiter = delimiter;
			this.LowerRoundingSpec = lowerRoundingSpec;
			this.UpperRoundingSpec = upperRoundingSpec;
		}

		// Token: 0x1700293D RID: 10557
		// (get) Token: 0x0600F75F RID: 63327 RVA: 0x0034B976 File Offset: 0x00349B76
		public DateTimeFormat Format { get; }

		// Token: 0x1700293E RID: 10558
		// (get) Token: 0x0600F760 RID: 63328 RVA: 0x0034B97E File Offset: 0x00349B7E
		public string Delimiter { get; }

		// Token: 0x1700293F RID: 10559
		// (get) Token: 0x0600F761 RID: 63329 RVA: 0x0034B986 File Offset: 0x00349B86
		public DateTimeRoundingSpec LowerRoundingSpec { get; }

		// Token: 0x17002940 RID: 10560
		// (get) Token: 0x0600F762 RID: 63330 RVA: 0x0034B98E File Offset: 0x00349B8E
		public DateTimeRoundingSpec UpperRoundingSpec { get; }
	}
}
