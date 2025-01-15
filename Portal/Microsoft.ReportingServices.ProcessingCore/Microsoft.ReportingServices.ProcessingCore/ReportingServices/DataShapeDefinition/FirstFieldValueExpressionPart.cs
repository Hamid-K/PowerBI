using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000598 RID: 1432
	[DataContract]
	internal sealed class FirstFieldValueExpressionPart : ExpressionPart
	{
		// Token: 0x060051D9 RID: 20953 RVA: 0x0015A226 File Offset: 0x00158426
		public FirstFieldValueExpressionPart(string fieldName, string dataSetName)
		{
			this.m_fieldName = fieldName;
			this.m_dataSetName = dataSetName;
		}

		// Token: 0x17001E6F RID: 7791
		// (get) Token: 0x060051DA RID: 20954 RVA: 0x0015A23C File Offset: 0x0015843C
		public string FieldName
		{
			get
			{
				return this.m_fieldName;
			}
		}

		// Token: 0x17001E70 RID: 7792
		// (get) Token: 0x060051DB RID: 20955 RVA: 0x0015A244 File Offset: 0x00158444
		public string DataSetName
		{
			get
			{
				return this.m_dataSetName;
			}
		}

		// Token: 0x17001E71 RID: 7793
		// (get) Token: 0x060051DC RID: 20956 RVA: 0x0015A24C File Offset: 0x0015844C
		internal override ExpressionPartKind Kind
		{
			get
			{
				return ExpressionPartKind.FirstFieldValue;
			}
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x0015A250 File Offset: 0x00158450
		public override bool Equals(ExpressionPart other)
		{
			FirstFieldValueExpressionPart firstFieldValueExpressionPart = other as FirstFieldValueExpressionPart;
			return firstFieldValueExpressionPart != null && this.FieldName == firstFieldValueExpressionPart.FieldName && this.DataSetName == firstFieldValueExpressionPart.DataSetName;
		}

		// Token: 0x04002957 RID: 10583
		[DataMember(Name = "FieldName", Order = 1)]
		private readonly string m_fieldName;

		// Token: 0x04002958 RID: 10584
		[DataMember(Name = "DataSetName", Order = 2)]
		private readonly string m_dataSetName;
	}
}
