using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200019E RID: 414
	internal sealed class PlanNamedEntity : IEquatable<PlanNamedEntity>, IPlanNamedItem, IStructuredToString
	{
		// Token: 0x06000E99 RID: 3737 RVA: 0x0003B7F8 File Offset: 0x000399F8
		public PlanNamedEntity(string name, PlanOperation table, PlanVisualShape visualShape, IReadOnlyList<Calculation> additionalColumns, IReadOnlyList<Calculation> subtotalsOverAdditionalColumns)
		{
			this.Name = name;
			this.Value = table;
			this.VisualShape = visualShape;
			this.AdditionalColumns = additionalColumns;
			this.SubtotalsOverAdditionalColumns = subtotalsOverAdditionalColumns;
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0003B825 File Offset: 0x00039A25
		public string Name { get; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0003B82D File Offset: 0x00039A2D
		public PlanOperation Value { get; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0003B835 File Offset: 0x00039A35
		public PlanVisualShape VisualShape { get; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x0003B83D File Offset: 0x00039A3D
		public IReadOnlyList<Calculation> AdditionalColumns { get; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x0003B845 File Offset: 0x00039A45
		public IReadOnlyList<Calculation> SubtotalsOverAdditionalColumns { get; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x0003B84D File Offset: 0x00039A4D
		public PlanNamedItemKind Kind
		{
			get
			{
				return PlanNamedItemKind.Entity;
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003B850 File Offset: 0x00039A50
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanNamedEntity);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003B860 File Offset: 0x00039A60
		public bool Equals(PlanNamedEntity other)
		{
			return other != null && this.Name == other.Name && this.Value.Equals(other.Value) && object.Equals(this.VisualShape, other.VisualShape) && this.AdditionalColumns.SequenceEqualReadOnly(other.AdditionalColumns) && this.SubtotalsOverAdditionalColumns.SequenceEqualReadOnly(other.SubtotalsOverAdditionalColumns);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0003B8D0 File Offset: 0x00039AD0
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Name.GetHashCode(), this.Value.GetHashCode(), Hashing.GetHashCode<PlanVisualShape>(this.VisualShape, null), Hashing.CombineHashReadonly<Calculation>(this.AdditionalColumns, null), Hashing.CombineHashReadonly<Calculation>(this.SubtotalsOverAdditionalColumns, null));
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0003B91C File Offset: 0x00039B1C
		public string ToString(ExpressionTable expressionTable)
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(expressionTable, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0003B945 File Offset: 0x00039B45
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0003B950 File Offset: 0x00039B50
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("NamedEntity");
			builder.WriteAttribute<string>("Name", this.Name, false, false);
			builder.WriteProperty<PlanOperation>("Value", this.Value, false);
			builder.WriteProperty<PlanVisualShape>("VisualShape", this.VisualShape, false);
			builder.WriteProperty<IReadOnlyList<Calculation>>("AdditionalColumns", this.AdditionalColumns, false);
			builder.WriteProperty<IReadOnlyList<Calculation>>("SubtotalsOverAdditionalColumns", this.SubtotalsOverAdditionalColumns, false);
			builder.EndObject();
		}
	}
}
