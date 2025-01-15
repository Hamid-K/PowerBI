using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000E9 RID: 233
	internal class DataTransformOutputBuilder<TParent> : BuilderBase<DataTransformOutput, TParent>
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x0000DDCD File Offset: 0x0000BFCD
		internal DataTransformOutputBuilder(TParent parent, DataTransformOutput activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		public DataTransformTableBuilder<DataTransformOutputBuilder<TParent>> WithTable(Identifier id)
		{
			DataTransformTable dataTransformTable = new DataTransformTable();
			dataTransformTable.Id = id;
			base.ActiveObject.Table = dataTransformTable;
			return new DataTransformTableBuilder<DataTransformOutputBuilder<TParent>>(this, dataTransformTable);
		}
	}
}
