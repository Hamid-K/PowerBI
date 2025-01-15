using System;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x02001994 RID: 6548
	public class IfModel : CompositePipelineModel, ICompositePipeline
	{
		// Token: 0x0600D612 RID: 54802 RVA: 0x002D998B File Offset: 0x002D7B8B
		public override string ToOperatorString()
		{
			return base.Children.Select(delegate(PipelineModel c)
			{
				if (c == null)
				{
					return null;
				}
				return c.ToString();
			}).ToJoinNewlineString();
		}
	}
}
