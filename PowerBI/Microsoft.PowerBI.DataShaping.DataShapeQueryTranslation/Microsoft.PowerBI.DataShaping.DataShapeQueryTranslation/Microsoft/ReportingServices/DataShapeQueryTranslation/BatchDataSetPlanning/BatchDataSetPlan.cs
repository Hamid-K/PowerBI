using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200019C RID: 412
	internal sealed class BatchDataSetPlan : IDataSetPlan, IStructuredToString
	{
		// Token: 0x06000E8B RID: 3723 RVA: 0x0003B69C File Offset: 0x0003989C
		internal BatchDataSetPlan(IReadOnlyList<PlanNamedTableContext> outputTables, IReadOnlyList<IPlanNamedItem> declarations, int planIndex, string name, ExtensionSchema extensionSchema, string dataSourceVariables, IReadOnlyList<ModelParameter> modelParameters, IReadOnlyList<QueryParameterDeclaration> queryParameters)
		{
			this.OutputTables = outputTables;
			this.Declarations = declarations;
			this.PlanIndex = planIndex;
			this.Name = name;
			this.ExtensionSchema = extensionSchema;
			this.DataSourceVariables = dataSourceVariables;
			this.ModelParameters = modelParameters;
			this.QueryParameters = queryParameters;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0003B6EC File Offset: 0x000398EC
		public IReadOnlyList<PlanNamedTableContext> OutputTables { get; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0003B6F4 File Offset: 0x000398F4
		public IReadOnlyList<IPlanNamedItem> Declarations { get; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0003B6FC File Offset: 0x000398FC
		public int PlanIndex { get; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0003B704 File Offset: 0x00039904
		public string Name { get; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0003B70C File Offset: 0x0003990C
		public ExtensionSchema ExtensionSchema { get; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x0003B714 File Offset: 0x00039914
		public string DataSourceVariables { get; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0003B71C File Offset: 0x0003991C
		public IReadOnlyList<ModelParameter> ModelParameters { get; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0003B724 File Offset: 0x00039924
		public IReadOnlyList<QueryParameterDeclaration> QueryParameters { get; }

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003B72C File Offset: 0x0003992C
		public override string ToString()
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(null, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003B758 File Offset: 0x00039958
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("BatchDataSetPlan");
			builder.WriteAttribute<string>("Name", this.Name, false, false);
			builder.WriteProperty<IReadOnlyList<IPlanNamedItem>>("Declarations", this.Declarations, false);
			builder.WriteProperty<IReadOnlyList<PlanNamedTableContext>>("OutputTables", this.OutputTables, false);
			builder.WriteProperty<bool>("HasExtensionSchema", this.ExtensionSchema != null, false);
			builder.WriteProperty<string>("DataSourceVariables", this.DataSourceVariables, false);
			builder.WriteProperty<IReadOnlyList<ModelParameter>>("ModelParameters", this.ModelParameters, false);
			builder.WriteProperty<IReadOnlyList<QueryParameterDeclaration>>("QueryParameters", this.QueryParameters, false);
			builder.EndObject();
		}
	}
}
