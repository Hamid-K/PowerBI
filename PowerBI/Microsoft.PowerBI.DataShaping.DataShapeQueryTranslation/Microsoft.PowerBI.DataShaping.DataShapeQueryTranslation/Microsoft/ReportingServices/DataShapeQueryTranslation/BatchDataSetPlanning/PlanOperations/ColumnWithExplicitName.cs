using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001E7 RID: 487
	internal class ColumnWithExplicitName : IStructuredToString
	{
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x00046343 File Offset: 0x00044543
		public string Name { get; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0004634B File Offset: 0x0004454B
		public ExpressionId ExpressionId { get; }

		// Token: 0x060010D6 RID: 4310 RVA: 0x00046353 File Offset: 0x00044553
		public ColumnWithExplicitName(ExpressionId expressionId, string nativeReferenceName)
		{
			this.ExpressionId = expressionId;
			this.Name = nativeReferenceName;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0004636C File Offset: 0x0004456C
		public override bool Equals(object other)
		{
			bool flag;
			ColumnWithExplicitName columnWithExplicitName;
			if (!CompareUtil.CheckReferenceAndTypeEquality<ColumnWithExplicitName, object>(this, other, out flag, out columnWithExplicitName))
			{
				return QueryNameComparer.Instance.Equals(this.Name, columnWithExplicitName.Name) && this.ExpressionId == columnWithExplicitName.ExpressionId;
			}
			return flag;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x000463B3 File Offset: 0x000445B3
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<string>(this.Name, null), Hashing.GetHashCode<ExpressionId>(this.ExpressionId, null));
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000463D2 File Offset: 0x000445D2
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("ColumnWithExplicitName");
			builder.WriteProperty<string>("Name", this.Name, false);
			builder.WriteProperty<ExpressionId>("ExpressionId", this.ExpressionId, false);
			builder.EndObject();
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0004640C File Offset: 0x0004460C
		public override string ToString()
		{
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(ExpressionStringBuilderFactory.Create(null, false), 0, false);
			this.WriteTo(structuredStringBuilder);
			return structuredStringBuilder.ToString();
		}
	}
}
