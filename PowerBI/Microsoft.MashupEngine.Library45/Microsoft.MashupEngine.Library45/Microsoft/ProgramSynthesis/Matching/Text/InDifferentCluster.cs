using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Matching.Text.Learning;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011A5 RID: 4517
	public class InDifferentCluster : ClusterConstraint<InDifferentCluster, string, bool>
	{
		// Token: 0x0600867D RID: 34429 RVA: 0x001C3D06 File Offset: 0x001C1F06
		public InDifferentCluster(IReadOnlyList<string> inputs)
			: base(inputs)
		{
		}

		// Token: 0x0600867E RID: 34430 RVA: 0x001C3D10 File Offset: 0x001C1F10
		public override bool ConflictsWith(Constraint<string, bool> other)
		{
			InSameCluster inSameCluster = other as InSameCluster;
			return inSameCluster != null && inSameCluster.Inputs.Count((string i) => this.InputSet.Contains(i)) >= 2;
		}

		// Token: 0x0600867F RID: 34431 RVA: 0x001C3D44 File Offset: 0x001C1F44
		public override bool Valid(Program<string, bool> program)
		{
			Program mtextProgram = program as Program;
			if (mtextProgram != null)
			{
				return base.Inputs.Select((string i) => mtextProgram.GetLabel(i)).Distinct<MatchingLabel>().Count<MatchingLabel>() == this.InputSet.Count;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06008680 RID: 34432 RVA: 0x001C3D9F File Offset: 0x001C1F9F
		public override void SetOptions(Witnesses.Options options)
		{
			options.InDifferentCluster.Add(base.Inputs.Cast<object>().ToList<object>());
		}
	}
}
