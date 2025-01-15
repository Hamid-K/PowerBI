using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000EA RID: 234
	internal class DataTransformTableBuilder<TParent> : BuilderBase<DataTransformTable, TParent>
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x0000DE05 File Offset: 0x0000C005
		internal DataTransformTableBuilder(TParent parent, DataTransformTable activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000DE10 File Offset: 0x0000C010
		public DataTransformTableBuilder<TParent> WithColumn(Identifier id, Expression expression, string role = null)
		{
			DataTransformTableColumn dataTransformTableColumn = new DataTransformTableColumn
			{
				Id = id,
				Value = expression,
				Role = role
			};
			if (base.ActiveObject.Columns == null)
			{
				base.ActiveObject.Columns = new List<DataTransformTableColumn>();
			}
			base.ActiveObject.Columns.Add(dataTransformTableColumn);
			return this;
		}
	}
}
