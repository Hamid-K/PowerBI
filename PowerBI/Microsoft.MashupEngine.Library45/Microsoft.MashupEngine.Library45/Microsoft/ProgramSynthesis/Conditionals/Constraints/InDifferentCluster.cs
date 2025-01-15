using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Conditionals.Learning;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Conditionals.Constraints
{
	// Token: 0x02000A74 RID: 2676
	public class InDifferentCluster : Constraint<IEnumerable<string>, IEnumerable<IEnumerable<string>>>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06004261 RID: 16993 RVA: 0x000CFD23 File Offset: 0x000CDF23
		public InDifferentCluster(IEnumerable<string> elements)
		{
			this.Elements = elements.ConvertToHashSet<string>();
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06004262 RID: 16994 RVA: 0x000CFD37 File Offset: 0x000CDF37
		public HashSet<string> Elements { get; }

		// Token: 0x06004263 RID: 16995 RVA: 0x000CFD3F File Offset: 0x000CDF3F
		public void SetOptions(Witnesses.Options options)
		{
			options.InDifferentClusters = options.InDifferentClusters.AppendItem(this.Elements).ToList<HashSet<string>>();
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x000CFD60 File Offset: 0x000CDF60
		public override bool ConflictsWith(Constraint<IEnumerable<string>, IEnumerable<IEnumerable<string>>> other)
		{
			InSameCluster inSameCluster = other as InSameCluster;
			return inSameCluster != null && this.Elements.Intersect(inSameCluster.Elements).Count<string>() > 1;
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x000CFD98 File Offset: 0x000CDF98
		public override bool Equals(Constraint<IEnumerable<string>, IEnumerable<IEnumerable<string>>> other)
		{
			InDifferentCluster inDifferentCluster = other as InDifferentCluster;
			return inDifferentCluster != null && inDifferentCluster.Elements.SetEquals(this.Elements);
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x000CFDC8 File Offset: 0x000CDFC8
		public override int GetHashCode()
		{
			return 5077 * this.Elements.OrderIndependentHashCode<string>();
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<IEnumerable<string>, IEnumerable<IEnumerable<string>>> program)
		{
			return true;
		}
	}
}
