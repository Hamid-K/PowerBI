using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001E9 RID: 489
	internal sealed class PlanDataTransformColumnMeasure : IEquatable<PlanDataTransformColumnMeasure>, IStructuredToString
	{
		// Token: 0x060010FA RID: 4346 RVA: 0x00046435 File Offset: 0x00044635
		internal PlanDataTransformColumnMeasure(DataTransformTable table, DataTransformTableColumn column)
		{
			this.m_table = table;
			this.m_column = column;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0004644B File Offset: 0x0004464B
		public DataTransformTable Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00046453 File Offset: 0x00044653
		public DataTransformTableColumn Column
		{
			get
			{
				return this.m_column;
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0004645B File Offset: 0x0004465B
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.m_table.GetHashCode(), this.m_column.GetHashCode());
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00046478 File Offset: 0x00044678
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanDataTransformColumnMeasure);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00046486 File Offset: 0x00044686
		public bool Equals(PlanDataTransformColumnMeasure other)
		{
			return other != null && this.Table == other.Table && this.Column == other.Column;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000464A9 File Offset: 0x000446A9
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("DataTransformColumnMeasure");
			builder.WriteAttribute<DataTransformTable>("Table", this.m_table, false, false);
			builder.WriteAttribute<DataTransformTableColumn>("Column", this.m_column, false, false);
			builder.EndObject();
		}

		// Token: 0x040007E0 RID: 2016
		private readonly DataTransformTable m_table;

		// Token: 0x040007E1 RID: 2017
		private readonly DataTransformTableColumn m_column;
	}
}
