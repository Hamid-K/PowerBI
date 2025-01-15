using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000E8 RID: 232
	internal class DataTransformInputBuilder<TParent> : BuilderBase<DataTransformInput, TParent>
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x0000DD43 File Offset: 0x0000BF43
		internal DataTransformInputBuilder(TParent parent, DataTransformInput activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0000DD50 File Offset: 0x0000BF50
		public DataTransformInputBuilder<TParent> WithParameter(Identifier id, Expression expression)
		{
			if (base.ActiveObject.Parameters == null)
			{
				base.ActiveObject.Parameters = new List<DataTransformParameter>();
			}
			base.ActiveObject.Parameters.Add(new DataTransformParameter
			{
				Id = id,
				Value = expression
			});
			return this;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0000DDA0 File Offset: 0x0000BFA0
		public DataTransformTableBuilder<DataTransformInputBuilder<TParent>> WithTable(Identifier id)
		{
			DataTransformTable dataTransformTable = new DataTransformTable();
			dataTransformTable.Id = id;
			base.ActiveObject.Table = dataTransformTable;
			return new DataTransformTableBuilder<DataTransformInputBuilder<TParent>>(this, dataTransformTable);
		}
	}
}
