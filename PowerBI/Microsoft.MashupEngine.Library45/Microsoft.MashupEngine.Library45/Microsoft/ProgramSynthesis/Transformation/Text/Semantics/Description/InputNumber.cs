using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Description
{
	// Token: 0x02001D84 RID: 7556
	internal class InputNumber : TransformationDescription
	{
		// Token: 0x0600FDFF RID: 65023 RVA: 0x0036437A File Offset: 0x0036257A
		internal InputNumber(ProgramNode programNode, string columnName)
			: base(programNode, columnName, TransformationCategory.Input, TransformationKind.InputNumber)
		{
		}

		// Token: 0x17002A4F RID: 10831
		// (get) Token: 0x0600FE00 RID: 65024 RVA: 0x0034BD04 File Offset: 0x00349F04
		protected override object ExtraIdentity
		{
			get
			{
				return base.ColumnName;
			}
		}
	}
}
