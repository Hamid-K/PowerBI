using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x020001A1 RID: 417
	internal class PlanNamedTable : IEquatable<PlanNamedTable>, IPlanNamedItem, IStructuredToString
	{
		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003BAA2 File Offset: 0x00039CA2
		internal PlanNamedTable(string name, PlanOperation table, bool canExpandToMultiTables = false, bool isReusable = false, bool isFragmentOfExistingDeclaration = false, string namingContextId = null, bool useGlobalNamingContext = false)
		{
			this.Name = name;
			this.Table = table;
			this.CanExpandToMultiTables = canExpandToMultiTables;
			this.IsReusable = isReusable;
			this.IsFragmentOfExistingDeclaration = isFragmentOfExistingDeclaration;
			this.NamingContextId = namingContextId;
			this.UseGlobalNamingContext = useGlobalNamingContext;
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x0003BADF File Offset: 0x00039CDF
		public PlanNamedItemKind Kind
		{
			get
			{
				return PlanNamedItemKind.Table;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0003BAE2 File Offset: 0x00039CE2
		public string Name { get; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0003BAEA File Offset: 0x00039CEA
		public PlanOperation Table { get; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0003BAF2 File Offset: 0x00039CF2
		public bool CanExpandToMultiTables { get; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0003BAFA File Offset: 0x00039CFA
		public bool IsReusable { get; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0003BB02 File Offset: 0x00039D02
		public bool IsFragmentOfExistingDeclaration { get; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0003BB0A File Offset: 0x00039D0A
		public string NamingContextId { get; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0003BB12 File Offset: 0x00039D12
		public bool UseGlobalNamingContext { get; }

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0003BB1A File Offset: 0x00039D1A
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PlanNamedTable);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0003BB28 File Offset: 0x00039D28
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0003BB38 File Offset: 0x00039D38
		public bool Equals(PlanNamedTable other)
		{
			return other != null && this.Name == other.Name && this.Table.Equals(other.Table) && this.CanExpandToMultiTables == other.CanExpandToMultiTables && this.IsFragmentOfExistingDeclaration == other.IsFragmentOfExistingDeclaration && this.NamingContextId == other.NamingContextId && this.UseGlobalNamingContext == other.UseGlobalNamingContext;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0003BBB0 File Offset: 0x00039DB0
		public string ToString(ExpressionTable expressionTable)
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(expressionTable, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0003BBD9 File Offset: 0x00039DD9
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0003BBE4 File Offset: 0x00039DE4
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("NamedTable");
			builder.WriteAttribute<string>("Name", this.Name, false, false);
			builder.WriteAttribute<bool>("CanExpandToMultiTable", this.CanExpandToMultiTables, false, false);
			builder.WriteAttribute<bool>("IsFragmentOfExistingDeclaration", this.IsFragmentOfExistingDeclaration, false, false);
			builder.WriteAttribute<string>("NamingContextId", this.NamingContextId, false, false);
			builder.WriteAttribute<bool>("UseGlobalNamingContext", this.UseGlobalNamingContext, false, false);
			builder.WriteProperty<PlanOperation>("Table", this.Table, false);
			builder.EndObject();
		}
	}
}
