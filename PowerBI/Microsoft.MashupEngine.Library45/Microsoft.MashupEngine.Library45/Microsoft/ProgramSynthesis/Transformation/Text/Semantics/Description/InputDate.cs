using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Description
{
	// Token: 0x02001D83 RID: 7555
	internal class InputDate : TransformationDescription
	{
		// Token: 0x0600FDFD RID: 65021 RVA: 0x0036436A File Offset: 0x0036256A
		internal InputDate(ProgramNode programNode, string columnName)
			: base(programNode, columnName, TransformationCategory.Input, TransformationKind.InputDate)
		{
		}

		// Token: 0x17002A4E RID: 10830
		// (get) Token: 0x0600FDFE RID: 65022 RVA: 0x0034BD04 File Offset: 0x00349F04
		protected override object ExtraIdentity
		{
			get
			{
				return base.ColumnName;
			}
		}
	}
}
