using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000BB RID: 187
	internal sealed class VisualAxisGroup : IStructuredToString, IEquatable<VisualAxisGroup>
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00007DC9 File Offset: 0x00005FC9
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00007DD1 File Offset: 0x00005FD1
		public Expression Member { get; set; }

		// Token: 0x06000460 RID: 1120 RVA: 0x00007DDA File Offset: 0x00005FDA
		public override bool Equals(object obj)
		{
			return this.Equals(obj as VisualAxisGroup);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00007DE8 File Offset: 0x00005FE8
		public bool Equals(VisualAxisGroup other)
		{
			return other != null && ExpressionComparerById.Instance.Equals(this.Member, other.Member);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00007E05 File Offset: 0x00006005
		public override int GetHashCode()
		{
			return Hashing.GetHashCode<Expression>(this.Member, ExpressionComparerById.Instance);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00007E17 File Offset: 0x00006017
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("AxisGroup");
			builder.WriteProperty<Expression>("Member", this.Member, false);
			builder.EndObject();
		}
	}
}
