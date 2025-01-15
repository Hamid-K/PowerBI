using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001D0 RID: 464
	internal sealed class QueryVisualShape : IEquatable<QueryVisualShape>
	{
		// Token: 0x06001690 RID: 5776 RVA: 0x0003E387 File Offset: 0x0003C587
		internal QueryVisualShape(IReadOnlyList<QueryVisualAxis> axes, string isDensifiedColumnName)
		{
			this.Axes = axes;
			this.IsDensifiedColumnName = isDensifiedColumnName;
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x0003E39D File Offset: 0x0003C59D
		public IReadOnlyList<QueryVisualAxis> Axes { get; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0003E3A5 File Offset: 0x0003C5A5
		public string IsDensifiedColumnName { get; }

		// Token: 0x06001693 RID: 5779 RVA: 0x0003E3AD File Offset: 0x0003C5AD
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryVisualShape);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0003E3BB File Offset: 0x0003C5BB
		public bool Equals(QueryVisualShape other)
		{
			return other != null && this.Axes.SequenceEqualReadOnly(other.Axes) && QueryNamingContext.NameComparer.Equals(this.IsDensifiedColumnName, other.IsDensifiedColumnName);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0003E3EB File Offset: 0x0003C5EB
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHashReadonly<QueryVisualAxis>(this.Axes, null), Hashing.GetHashCode<string>(this.IsDensifiedColumnName, QueryNamingContext.NameComparer));
		}
	}
}
