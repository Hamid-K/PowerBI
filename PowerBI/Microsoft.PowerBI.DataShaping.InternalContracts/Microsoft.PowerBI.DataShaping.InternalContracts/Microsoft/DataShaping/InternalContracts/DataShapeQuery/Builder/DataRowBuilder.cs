using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000ED RID: 237
	internal sealed class DataRowBuilder<TParent> : BuilderBase<DataRow, TParent>
	{
		// Token: 0x06000696 RID: 1686 RVA: 0x0000E423 File Offset: 0x0000C623
		internal DataRowBuilder(TParent parent, DataRow activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0000E430 File Offset: 0x0000C630
		public DataIntersectionBuilder<DataRowBuilder<TParent>> WithIntersection(string identifier)
		{
			DataRow activeObject = base.ActiveObject;
			List<DataIntersection> list = activeObject.Intersections;
			if (list == null)
			{
				list = new List<DataIntersection>();
				activeObject.Intersections = list;
			}
			DataIntersection dataIntersection = new DataIntersection
			{
				Id = new Identifier(identifier)
			};
			list.Add(dataIntersection);
			return new DataIntersectionBuilder<DataRowBuilder<TParent>>(this, dataIntersection);
		}
	}
}
