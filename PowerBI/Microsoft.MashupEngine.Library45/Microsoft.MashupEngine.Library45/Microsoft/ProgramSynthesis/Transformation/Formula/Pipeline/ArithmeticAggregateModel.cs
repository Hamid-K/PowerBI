using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x0200198A RID: 6538
	public class ArithmeticAggregateModel : PipelineModel, ICompositePipeline
	{
		// Token: 0x17002374 RID: 9076
		// (get) Token: 0x0600D5F0 RID: 54768 RVA: 0x002D97CC File Offset: 0x002D79CC
		// (set) Token: 0x0600D5F1 RID: 54769 RVA: 0x002D97D4 File Offset: 0x002D79D4
		public IReadOnlyList<string> ColumnNames { get; set; }

		// Token: 0x17002375 RID: 9077
		// (get) Token: 0x0600D5F2 RID: 54770 RVA: 0x002D97DD File Offset: 0x002D79DD
		// (set) Token: 0x0600D5F3 RID: 54771 RVA: 0x002D97E5 File Offset: 0x002D79E5
		public string Operator { get; set; }

		// Token: 0x17002376 RID: 9078
		// (get) Token: 0x0600D5F4 RID: 54772 RVA: 0x002D97EE File Offset: 0x002D79EE
		// (set) Token: 0x0600D5F5 RID: 54773 RVA: 0x002D97F6 File Offset: 0x002D79F6
		public NumberTransformModel Transform { get; set; }

		// Token: 0x0600D5F6 RID: 54774 RVA: 0x002D9800 File Offset: 0x002D7A00
		public override string ToOperatorString()
		{
			string[] array = new string[5];
			int num = 0;
			string @operator = this.Operator;
			array[num] = ((@operator != null) ? @operator.ToLower() : null);
			array[1] = ":";
			array[2] = Environment.NewLine;
			array[3] = "    ";
			array[4] = this.ColumnNames.ToJoinString(",");
			return string.Concat(array);
		}
	}
}
