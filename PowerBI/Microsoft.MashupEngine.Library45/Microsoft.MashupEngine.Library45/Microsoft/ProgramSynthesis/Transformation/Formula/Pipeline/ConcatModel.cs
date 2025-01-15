using System;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x02001992 RID: 6546
	public class ConcatModel : CompositePipelineModel, ICompositePipeline
	{
		// Token: 0x0600D60D RID: 54797 RVA: 0x002D9918 File Offset: 0x002D7B18
		public override string ToOperatorString()
		{
			return "concat: " + Environment.NewLine + base.Children.Select(delegate(PipelineModel c)
			{
				if (c == null)
				{
					return null;
				}
				return c.ToString();
			}).ToJoinNewlineString().Indent(4, false);
		}
	}
}
