using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000597 RID: 1431
	[DataContract]
	internal sealed class FieldReferenceExpressionPart : ExpressionPart
	{
		// Token: 0x060051D5 RID: 20949 RVA: 0x0015A1E2 File Offset: 0x001583E2
		public FieldReferenceExpressionPart(string fieldName)
		{
			this.m_fieldName = fieldName;
		}

		// Token: 0x17001E6D RID: 7789
		// (get) Token: 0x060051D6 RID: 20950 RVA: 0x0015A1F1 File Offset: 0x001583F1
		public string FieldName
		{
			get
			{
				return this.m_fieldName;
			}
		}

		// Token: 0x17001E6E RID: 7790
		// (get) Token: 0x060051D7 RID: 20951 RVA: 0x0015A1F9 File Offset: 0x001583F9
		internal override ExpressionPartKind Kind
		{
			get
			{
				return ExpressionPartKind.FieldReference;
			}
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x0015A1FC File Offset: 0x001583FC
		public override bool Equals(ExpressionPart other)
		{
			FieldReferenceExpressionPart fieldReferenceExpressionPart = other as FieldReferenceExpressionPart;
			return fieldReferenceExpressionPart != null && this.FieldName == fieldReferenceExpressionPart.FieldName;
		}

		// Token: 0x04002956 RID: 10582
		[DataMember(Name = "FieldName", Order = 1)]
		private readonly string m_fieldName;
	}
}
