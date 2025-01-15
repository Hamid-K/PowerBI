using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000160 RID: 352
	internal class BatchColumnReference : IStructuredToString
	{
		// Token: 0x06000CD4 RID: 3284 RVA: 0x00035086 File Offset: 0x00033286
		internal BatchColumnReference(BatchDataBinding dataBinding, string columnName)
		{
			this.m_dataBinding = dataBinding;
			this.m_columnName = columnName;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0003509C File Offset: 0x0003329C
		public BatchDataBinding DataBinding
		{
			get
			{
				return this.m_dataBinding;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x000350A4 File Offset: 0x000332A4
		public string ColumnName
		{
			get
			{
				return this.m_columnName;
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x000350AC File Offset: 0x000332AC
		public virtual void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("ColumnReference");
			builder.WriteAttribute<string>("ColumnName", this.ColumnName, false, false);
			builder.WriteProperty<BatchDataBinding>("DataBinding", this.DataBinding, false);
			builder.EndObject();
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000350E4 File Offset: 0x000332E4
		public override string ToString()
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(null, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x0400065B RID: 1627
		private readonly BatchDataBinding m_dataBinding;

		// Token: 0x0400065C RID: 1628
		private readonly string m_columnName;
	}
}
