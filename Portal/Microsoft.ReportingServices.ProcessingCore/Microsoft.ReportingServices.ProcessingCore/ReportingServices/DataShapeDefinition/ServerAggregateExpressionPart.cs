using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x0200059A RID: 1434
	[DataContract]
	internal sealed class ServerAggregateExpressionPart : ExpressionPart
	{
		// Token: 0x060051E3 RID: 20963 RVA: 0x0015A2F7 File Offset: 0x001584F7
		public ServerAggregateExpressionPart(string fieldName)
		{
			this.m_fieldReference = new FieldReferenceExpressionPart(fieldName);
		}

		// Token: 0x17001E75 RID: 7797
		// (get) Token: 0x060051E4 RID: 20964 RVA: 0x0015A30B File Offset: 0x0015850B
		public FieldReferenceExpressionPart FieldReference
		{
			get
			{
				return this.m_fieldReference;
			}
		}

		// Token: 0x17001E76 RID: 7798
		// (get) Token: 0x060051E5 RID: 20965 RVA: 0x0015A313 File Offset: 0x00158513
		internal override ExpressionPartKind Kind
		{
			get
			{
				return ExpressionPartKind.ServerAggregate;
			}
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x0015A318 File Offset: 0x00158518
		public override bool Equals(ExpressionPart other)
		{
			ServerAggregateExpressionPart serverAggregateExpressionPart = other as ServerAggregateExpressionPart;
			return serverAggregateExpressionPart != null && this.FieldReference.Equals(serverAggregateExpressionPart.FieldReference);
		}

		// Token: 0x0400295B RID: 10587
		[DataMember(Name = "FieldReference", Order = 1)]
		private readonly FieldReferenceExpressionPart m_fieldReference;
	}
}
