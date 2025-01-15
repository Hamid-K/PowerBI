using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200052A RID: 1322
	internal struct OutputFromComputeCellGroups
	{
		// Token: 0x0400169D RID: 5789
		internal List<Cell> Cells;

		// Token: 0x0400169E RID: 5790
		internal CqlIdentifiers Identifiers;

		// Token: 0x0400169F RID: 5791
		internal List<Set<Cell>> CellGroups;

		// Token: 0x040016A0 RID: 5792
		internal List<ForeignConstraint> ForeignKeyConstraints;

		// Token: 0x040016A1 RID: 5793
		internal bool Success;
	}
}
