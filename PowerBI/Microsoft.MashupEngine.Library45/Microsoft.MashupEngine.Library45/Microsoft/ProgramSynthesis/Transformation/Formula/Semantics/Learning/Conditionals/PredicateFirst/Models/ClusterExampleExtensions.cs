using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.PredicateFirst.Models
{
	// Token: 0x0200174F RID: 5967
	public static class ClusterExampleExtensions
	{
		// Token: 0x0600C61A RID: 50714 RVA: 0x002A9B0C File Offset: 0x002A7D0C
		public static string Render(this IEnumerable<ClusterExample> subject)
		{
			return subject.Select((ClusterExample example) => example.ToString()).ToJoinNewlineString();
		}
	}
}
