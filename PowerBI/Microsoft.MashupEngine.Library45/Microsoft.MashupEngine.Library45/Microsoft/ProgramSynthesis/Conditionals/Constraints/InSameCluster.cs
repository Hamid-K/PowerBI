using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Conditionals.Learning;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Conditionals.Constraints
{
	// Token: 0x02000A75 RID: 2677
	public class InSameCluster : Constraint<IEnumerable<string>, IEnumerable<IEnumerable<string>>>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x06004268 RID: 17000 RVA: 0x000CFDDB File Offset: 0x000CDFDB
		public InSameCluster(IEnumerable<string> elements)
		{
			this.Elements = elements.ConvertToHashSet<string>();
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06004269 RID: 17001 RVA: 0x000CFDEF File Offset: 0x000CDFEF
		public HashSet<string> Elements { get; }

		// Token: 0x0600426A RID: 17002 RVA: 0x000CFDF7 File Offset: 0x000CDFF7
		public void SetOptions(Witnesses.Options options)
		{
			options.InSameCluster = options.InSameCluster.AppendItem(this.Elements).ToList<HashSet<string>>();
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x000CFE18 File Offset: 0x000CE018
		public override bool ConflictsWith(Constraint<IEnumerable<string>, IEnumerable<IEnumerable<string>>> other)
		{
			InDifferentCluster inDifferentCluster = other as InDifferentCluster;
			return inDifferentCluster != null && this.Elements.Intersect(inDifferentCluster.Elements).Count<string>() > 1;
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x000CFE50 File Offset: 0x000CE050
		public override bool Equals(Constraint<IEnumerable<string>, IEnumerable<IEnumerable<string>>> other)
		{
			InSameCluster inSameCluster = other as InSameCluster;
			return inSameCluster != null && inSameCluster.Elements.SetEquals(this.Elements);
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x000CFE80 File Offset: 0x000CE080
		public override int GetHashCode()
		{
			return 2969 * this.Elements.OrderIndependentHashCode<string>();
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<IEnumerable<string>, IEnumerable<IEnumerable<string>>> program)
		{
			return true;
		}
	}
}
