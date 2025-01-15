using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x0200199E RID: 6558
	public class DateTimePartTransformModel : PipelineModel
	{
		// Token: 0x17002393 RID: 9107
		// (get) Token: 0x0600D651 RID: 54865 RVA: 0x002D9D8E File Offset: 0x002D7F8E
		// (set) Token: 0x0600D652 RID: 54866 RVA: 0x002D9D96 File Offset: 0x002D7F96
		public string FormatMask { get; set; }

		// Token: 0x17002394 RID: 9108
		// (get) Token: 0x0600D653 RID: 54867 RVA: 0x002D9D9F File Offset: 0x002D7F9F
		// (set) Token: 0x0600D654 RID: 54868 RVA: 0x002D9DA7 File Offset: 0x002D7FA7
		public DateTimePartKind Kind { get; set; }

		// Token: 0x17002395 RID: 9109
		// (get) Token: 0x0600D655 RID: 54869 RVA: 0x002D9DB0 File Offset: 0x002D7FB0
		// (set) Token: 0x0600D656 RID: 54870 RVA: 0x002D9DB8 File Offset: 0x002D7FB8
		public string Locale { get; set; }

		// Token: 0x0600D657 RID: 54871 RVA: 0x002D9DC1 File Offset: 0x002D7FC1
		public override string ToOperatorString()
		{
			return string.Format("Part({0})", this.Kind);
		}
	}
}
