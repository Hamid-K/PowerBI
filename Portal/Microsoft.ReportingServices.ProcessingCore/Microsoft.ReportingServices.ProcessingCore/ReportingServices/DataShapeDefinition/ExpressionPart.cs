using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000593 RID: 1427
	[DataContract]
	[KnownType(typeof(FieldReferenceExpressionPart))]
	[KnownType(typeof(FirstFieldValueExpressionPart))]
	[KnownType(typeof(FunctionCallExpressionPart))]
	[KnownType(typeof(LiteralExpressionPart))]
	[KnownType(typeof(ScopedFieldReferenceExpressionPart))]
	[KnownType(typeof(ServerAggregateExpressionPart))]
	internal abstract class ExpressionPart : IEquatable<ExpressionPart>
	{
		// Token: 0x17001E67 RID: 7783
		// (get) Token: 0x060051C9 RID: 20937
		internal abstract ExpressionPartKind Kind { get; }

		// Token: 0x060051CA RID: 20938
		public abstract bool Equals(ExpressionPart other);
	}
}
