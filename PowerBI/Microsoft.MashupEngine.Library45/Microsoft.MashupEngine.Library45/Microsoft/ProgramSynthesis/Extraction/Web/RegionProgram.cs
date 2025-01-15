using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FC2 RID: 4034
	public class RegionProgram : RegionExtractionProgram<RegionProgram, WebRegion>
	{
		// Token: 0x06006F59 RID: 28505 RVA: 0x0016BE4C File Offset: 0x0016A04C
		public RegionProgram(ProgramNode programNode, ReferenceKind refKind, double? score = null)
			: base(programNode, refKind, score ?? programNode.GetFeatureValue<double>(ExtractionLearner.Instance.ScoreFeature, null), null)
		{
		}

		// Token: 0x06006F5A RID: 28506 RVA: 0x0016BE88 File Offset: 0x0016A088
		public override IEnumerable<WebRegion> Run(IEnumerable<WebRegion> references)
		{
			if (references == null)
			{
				return null;
			}
			return (from reference in references
				let allNodes = (reference != null) ? reference.GetAllChildrenAndSelf() : null
				select new
				{
					<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
					state = ((allNodes == null) ? null : State.CreateForExecution(base.ProgramNode.Grammar.InputSymbol, allNodes))
				}).Select(delegate(<>h__TransparentIdentifier1)
			{
				if (<>h__TransparentIdentifier1.state != null)
				{
					return base.ProgramNode.Invoke(<>h__TransparentIdentifier1.state) as WebRegion;
				}
				return null;
			}).ToList<WebRegion>();
		}

		// Token: 0x06006F5B RID: 28507 RVA: 0x0016BEE6 File Offset: 0x0016A0E6
		public WebRegion Run(WebRegion reference)
		{
			IEnumerable<WebRegion> enumerable = this.Run(new WebRegion[] { reference });
			if (enumerable == null)
			{
				return null;
			}
			return enumerable.SingleOrDefault<WebRegion>();
		}
	}
}
