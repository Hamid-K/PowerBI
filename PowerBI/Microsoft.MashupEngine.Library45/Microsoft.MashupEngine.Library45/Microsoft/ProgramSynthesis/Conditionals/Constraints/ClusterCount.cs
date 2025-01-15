using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Conditionals.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Conditionals.Constraints
{
	// Token: 0x02000A73 RID: 2675
	public class ClusterCount : Constraint<IEnumerable<string>, IEnumerable<IEnumerable<string>>>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x0600425A RID: 16986 RVA: 0x000CFC62 File Offset: 0x000CDE62
		public ClusterCount(int count)
		{
			if (count <= 0)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid number of clusters {0}", new object[] { count })), "count");
			}
			this.Count = count;
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x0600425B RID: 16987 RVA: 0x000CFC9E File Offset: 0x000CDE9E
		public int Count { get; }

		// Token: 0x0600425C RID: 16988 RVA: 0x000CFCA6 File Offset: 0x000CDEA6
		public void SetOptions(Witnesses.Options options)
		{
			options.ClusterCount = new int?(this.Count);
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x000CFCBC File Offset: 0x000CDEBC
		public override bool ConflictsWith(Constraint<IEnumerable<string>, IEnumerable<IEnumerable<string>>> other)
		{
			ClusterCount clusterCount = other as ClusterCount;
			return clusterCount != null && !this.Equals(clusterCount);
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x000CFCE8 File Offset: 0x000CDEE8
		public override bool Equals(Constraint<IEnumerable<string>, IEnumerable<IEnumerable<string>>> other)
		{
			ClusterCount clusterCount = other as ClusterCount;
			return clusterCount != null && clusterCount.Count == this.Count;
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x000CFD15 File Offset: 0x000CDF15
		public override int GetHashCode()
		{
			return 6577 ^ this.Count;
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<IEnumerable<string>, IEnumerable<IEnumerable<string>>> program)
		{
			return true;
		}
	}
}
