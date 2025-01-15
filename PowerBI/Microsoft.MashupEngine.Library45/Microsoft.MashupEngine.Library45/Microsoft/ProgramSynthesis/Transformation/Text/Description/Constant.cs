using System;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C80 RID: 7296
	public class Constant : TransformationDescription
	{
		// Token: 0x0600F726 RID: 63270 RVA: 0x0034A4D4 File Offset: 0x003486D4
		internal Constant(ConstStr programNode)
			: base(programNode.Node, TransformationCategory.Constant, TransformationKind.Constant)
		{
			this.ConstantString = programNode.s.Value;
			this.FormatPartIndex = null;
		}

		// Token: 0x0600F727 RID: 63271 RVA: 0x0034A514 File Offset: 0x00348714
		internal Constant(FormatPartialDateTime programNode, int formatPartIdx)
			: base(programNode.Node, TransformationCategory.Constant, TransformationKind.Constant)
		{
			this.ConstantString = ((ConstantDateTimeFormatPart)programNode.outputDtFormat.Value.FormatParts[formatPartIdx]).ConstantString;
			this.FormatPartIndex = new int?(formatPartIdx);
		}

		// Token: 0x17002935 RID: 10549
		// (get) Token: 0x0600F728 RID: 63272 RVA: 0x0034A566 File Offset: 0x00348766
		public string ConstantString { get; }

		// Token: 0x17002936 RID: 10550
		// (get) Token: 0x0600F729 RID: 63273 RVA: 0x0034A56E File Offset: 0x0034876E
		private int? FormatPartIndex { get; }

		// Token: 0x17002937 RID: 10551
		// (get) Token: 0x0600F72A RID: 63274 RVA: 0x0034A576 File Offset: 0x00348776
		protected override object ExtraIdentity
		{
			get
			{
				return this.FormatPartIndex;
			}
		}
	}
}
