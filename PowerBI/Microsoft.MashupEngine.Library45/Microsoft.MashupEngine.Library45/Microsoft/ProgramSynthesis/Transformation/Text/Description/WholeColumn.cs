using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C9A RID: 7322
	public class WholeColumn : TransformationDescription
	{
		// Token: 0x0600F79D RID: 63389 RVA: 0x0034C02D File Offset: 0x0034A22D
		internal WholeColumn(ProgramNode programNode, string columnName)
			: base(programNode, columnName, TransformationCategory.Substring, TransformationKind.WholeColumn)
		{
		}

		// Token: 0x17002960 RID: 10592
		// (get) Token: 0x0600F79E RID: 63390 RVA: 0x0034BD04 File Offset: 0x00349F04
		protected override object ExtraIdentity
		{
			get
			{
				return base.ColumnName;
			}
		}
	}
}
