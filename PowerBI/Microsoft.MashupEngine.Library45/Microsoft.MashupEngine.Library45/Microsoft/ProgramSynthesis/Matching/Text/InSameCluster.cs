using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Matching.Text.Learning;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011A3 RID: 4515
	public class InSameCluster : ClusterConstraint<InSameCluster, string, bool>
	{
		// Token: 0x06008676 RID: 34422 RVA: 0x001C3C3D File Offset: 0x001C1E3D
		public InSameCluster(IReadOnlyList<string> inputs)
			: base(inputs)
		{
		}

		// Token: 0x06008677 RID: 34423 RVA: 0x001C3C48 File Offset: 0x001C1E48
		public override bool ConflictsWith(Constraint<string, bool> other)
		{
			InDifferentCluster inDifferentCluster = other as InDifferentCluster;
			return inDifferentCluster != null && inDifferentCluster.Inputs.Count((string i) => this.InputSet.Contains(i)) >= 2;
		}

		// Token: 0x06008678 RID: 34424 RVA: 0x001C3C7C File Offset: 0x001C1E7C
		public override bool Valid(Program<string, bool> program)
		{
			Program mtextProgram = program as Program;
			if (mtextProgram != null)
			{
				return base.Inputs.Select((string i) => mtextProgram.GetLabel(i)).Distinct<MatchingLabel>().Count<MatchingLabel>() == 1;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06008679 RID: 34425 RVA: 0x001C3CCD File Offset: 0x001C1ECD
		public override void SetOptions(Witnesses.Options options)
		{
			options.InSameCluster.Add(base.Inputs.Cast<object>().ToList<object>());
		}
	}
}
