using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000DE RID: 222
	internal sealed class DataShapeQueryBuilder : BuilderBase<DataShapeQuery>, IWithDataShape<DataShapeQueryBuilder>
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x0000D24A File Offset: 0x0000B44A
		internal DataShapeQueryBuilder()
			: this(new DataShapeQuery())
		{
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0000D257 File Offset: 0x0000B457
		internal DataShapeQueryBuilder(DataShapeQuery activeObject)
			: base(activeObject)
		{
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0000D260 File Offset: 0x0000B460
		public DataShapeQueryBuilder WithDataSource(string identifier, string dataSourceName = null, string itemPath = null)
		{
			DataShapeQuery activeObject = base.ActiveObject;
			List<DataSource> list = activeObject.DataSources;
			if (list == null)
			{
				list = new List<DataSource>();
				activeObject.DataSources = list;
			}
			DataSource dataSource = new DataSource
			{
				Id = new Identifier(identifier),
				DataSourceReference = new DataSourceReference
				{
					DataSourceName = dataSourceName,
					ItemPath = itemPath
				}
			};
			list.Add(dataSource);
			return this;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		public DataShapeBuilder<DataShapeQueryBuilder> WithDataShape(string identifier, string dataSourceId = null, bool filterEmptyGroups = true, Candidate<bool> contextOnly = null, bool independent = false, DataShapeUsage usage = DataShapeUsage.Query)
		{
			return base.AddDataShape<DataShapeQueryBuilder>(this, this.GetDataShapesList(), identifier, dataSourceId, filterEmptyGroups, contextOnly, independent, usage);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000D2E3 File Offset: 0x0000B4E3
		public DataShapeBuilder<DataShapeQueryBuilder> WithDataShape(DataShape dataShape)
		{
			return base.AddDataShape<DataShapeQueryBuilder>(this, this.GetDataShapesList(), dataShape);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000D2F4 File Offset: 0x0000B4F4
		private List<DataShape> GetDataShapesList()
		{
			return base.ActiveObject.DataShapes = base.ActiveObject.DataShapes ?? new List<DataShape>();
		}
	}
}
