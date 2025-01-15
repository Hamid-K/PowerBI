using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000065 RID: 101
	[DebuggerDisplay("[DataIntersection] Id={Id}")]
	internal sealed class DataIntersection : IScope, IContextItem, IIdentifiable, IDataBoundItem
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00005EB9 File Offset: 0x000040B9
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00005EC1 File Offset: 0x000040C1
		public Identifier Id { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00005ECA File Offset: 0x000040CA
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00005ED2 File Offset: 0x000040D2
		public List<Calculation> Calculations { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00005EDB File Offset: 0x000040DB
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.DataIntersection;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00005EDF File Offset: 0x000040DF
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00005EE7 File Offset: 0x000040E7
		public List<DataShape> DataShapes { get; set; }
	}
}
