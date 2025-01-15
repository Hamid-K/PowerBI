using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x02001989 RID: 6537
	public class ArithmeticModel : PipelineModel, ICompositePipeline
	{
		// Token: 0x17002371 RID: 9073
		// (get) Token: 0x0600D5E8 RID: 54760 RVA: 0x002D973C File Offset: 0x002D793C
		// (set) Token: 0x0600D5E9 RID: 54761 RVA: 0x002D9744 File Offset: 0x002D7944
		public PipelineModel Left { get; set; }

		// Token: 0x17002372 RID: 9074
		// (get) Token: 0x0600D5EA RID: 54762 RVA: 0x002D974D File Offset: 0x002D794D
		// (set) Token: 0x0600D5EB RID: 54763 RVA: 0x002D9755 File Offset: 0x002D7955
		public string Operator { get; set; }

		// Token: 0x17002373 RID: 9075
		// (get) Token: 0x0600D5EC RID: 54764 RVA: 0x002D975E File Offset: 0x002D795E
		// (set) Token: 0x0600D5ED RID: 54765 RVA: 0x002D9766 File Offset: 0x002D7966
		public PipelineModel Right { get; set; }

		// Token: 0x0600D5EE RID: 54766 RVA: 0x002D9770 File Offset: 0x002D7970
		public override string ToOperatorString()
		{
			string text = "{0}:{1}    {2}{3}    {4}";
			object[] array = new object[5];
			int num = 0;
			string @operator = this.Operator;
			array[num] = ((@operator != null) ? @operator.ToLower() : null);
			array[1] = Environment.NewLine;
			array[2] = this.Left;
			array[3] = Environment.NewLine;
			array[4] = this.Right;
			return string.Format(text, array);
		}
	}
}
