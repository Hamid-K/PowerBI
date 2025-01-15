using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FCA RID: 4042
	public class SequenceProgram : SequenceExtractionProgram<SequenceProgram, WebRegion>
	{
		// Token: 0x06006F8C RID: 28556 RVA: 0x0016C59C File Offset: 0x0016A79C
		public SequenceProgram(ProgramNode programNode, ReferenceKind refKind, double? score = null)
			: base(programNode, refKind, score ?? programNode.GetFeatureValue<double>(ExtractionLearner.Instance.ScoreFeature, null), null)
		{
		}

		// Token: 0x06006F8D RID: 28557 RVA: 0x0016C5D8 File Offset: 0x0016A7D8
		public override IEnumerable<IEnumerable<WebRegion>> Run(IEnumerable<WebRegion> references)
		{
			if (references == null)
			{
				return null;
			}
			return (from reference in references
				let allNodes = (reference != null) ? reference.GetAllChildrenAndSelf() : null
				let state = (allNodes == null) ? null : State.CreateForExecution(base.ProgramNode.Grammar.InputSymbol, allNodes)
				select new
				{
					<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1,
					run = ((state == null) ? null : base.ProgramNode.Invoke(state))
				}).Select(delegate(<>h__TransparentIdentifier2)
			{
				object run = <>h__TransparentIdentifier2.run;
				if (run == null)
				{
					return null;
				}
				return (from WebRegion ex in run.ToEnumerable<object>()
					orderby ex
					select ex).ToList<WebRegion>();
			}).Cast<IEnumerable<WebRegion>>().ToList<IEnumerable<WebRegion>>();
		}

		// Token: 0x06006F8E RID: 28558 RVA: 0x0016C65F File Offset: 0x0016A85F
		public IEnumerable<WebRegion> Run(WebRegion reference)
		{
			IEnumerable<IEnumerable<WebRegion>> enumerable = this.Run(new WebRegion[] { reference });
			if (enumerable == null)
			{
				return null;
			}
			return enumerable.SingleOrDefault<IEnumerable<WebRegion>>();
		}
	}
}
