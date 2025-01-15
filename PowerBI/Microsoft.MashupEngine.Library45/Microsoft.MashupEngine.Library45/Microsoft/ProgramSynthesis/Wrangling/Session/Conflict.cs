using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Wrangling.Session
{
	// Token: 0x0200010C RID: 268
	public class Conflict
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x0001406D File Offset: 0x0001226D
		public Conflict(IEnumerable<IConstraint> conflictingConstraints)
		{
			this.ConflictingConstraints = (conflictingConstraints as IImmutableSet<IConstraint>) ?? conflictingConstraints.ToImmutableHashSet<IConstraint>();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001408B File Offset: 0x0001228B
		public Conflict(params IConstraint[] conflictingConstraints)
			: this(conflictingConstraints)
		{
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00014094 File Offset: 0x00012294
		public IImmutableSet<IConstraint> ConflictingConstraints { get; }
	}
}
