using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000599 RID: 1433
	[DataContract]
	internal sealed class ScopedFieldReferenceExpressionPart : ExpressionPart
	{
		// Token: 0x060051DE RID: 20958 RVA: 0x0015A28F File Offset: 0x0015848F
		public ScopedFieldReferenceExpressionPart(string fieldName, string scopeName)
		{
			this.m_fieldName = fieldName;
			this.m_scopeName = scopeName;
		}

		// Token: 0x17001E72 RID: 7794
		// (get) Token: 0x060051DF RID: 20959 RVA: 0x0015A2A5 File Offset: 0x001584A5
		public string FieldName
		{
			get
			{
				return this.m_fieldName;
			}
		}

		// Token: 0x17001E73 RID: 7795
		// (get) Token: 0x060051E0 RID: 20960 RVA: 0x0015A2AD File Offset: 0x001584AD
		public string ScopeName
		{
			get
			{
				return this.m_scopeName;
			}
		}

		// Token: 0x17001E74 RID: 7796
		// (get) Token: 0x060051E1 RID: 20961 RVA: 0x0015A2B5 File Offset: 0x001584B5
		internal override ExpressionPartKind Kind
		{
			get
			{
				return ExpressionPartKind.ScopedFieldReference;
			}
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x0015A2B8 File Offset: 0x001584B8
		public override bool Equals(ExpressionPart other)
		{
			ScopedFieldReferenceExpressionPart scopedFieldReferenceExpressionPart = other as ScopedFieldReferenceExpressionPart;
			return scopedFieldReferenceExpressionPart != null && this.FieldName == scopedFieldReferenceExpressionPart.FieldName && this.ScopeName == scopedFieldReferenceExpressionPart.ScopeName;
		}

		// Token: 0x04002959 RID: 10585
		[DataMember(Name = "FieldName", Order = 1)]
		private readonly string m_fieldName;

		// Token: 0x0400295A RID: 10586
		[DataMember(Name = "ScopeName", Order = 2)]
		private readonly string m_scopeName;
	}
}
