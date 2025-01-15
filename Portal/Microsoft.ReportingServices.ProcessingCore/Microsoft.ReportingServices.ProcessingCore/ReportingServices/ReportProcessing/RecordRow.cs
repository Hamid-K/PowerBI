using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200070D RID: 1805
	[Serializable]
	internal sealed class RecordRow
	{
		// Token: 0x060064E2 RID: 25826 RVA: 0x0018EAA8 File Offset: 0x0018CCA8
		internal RecordRow(FieldsImpl fields, int fieldCount)
		{
			this.m_recordFields = new RecordField[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				if (!fields[i].IsMissing)
				{
					this.m_recordFields[i] = new RecordField(fields[i]);
				}
			}
			this.m_isAggregateRow = fields.IsAggregateRow;
			this.m_aggregationFieldCount = fields.AggregationFieldCount;
		}

		// Token: 0x060064E3 RID: 25827 RVA: 0x0018EB0D File Offset: 0x0018CD0D
		internal RecordRow()
		{
		}

		// Token: 0x170023BA RID: 9146
		// (get) Token: 0x060064E4 RID: 25828 RVA: 0x0018EB15 File Offset: 0x0018CD15
		// (set) Token: 0x060064E5 RID: 25829 RVA: 0x0018EB1D File Offset: 0x0018CD1D
		internal RecordField[] RecordFields
		{
			get
			{
				return this.m_recordFields;
			}
			set
			{
				this.m_recordFields = value;
			}
		}

		// Token: 0x170023BB RID: 9147
		// (get) Token: 0x060064E6 RID: 25830 RVA: 0x0018EB26 File Offset: 0x0018CD26
		// (set) Token: 0x060064E7 RID: 25831 RVA: 0x0018EB2E File Offset: 0x0018CD2E
		internal bool IsAggregateRow
		{
			get
			{
				return this.m_isAggregateRow;
			}
			set
			{
				this.m_isAggregateRow = value;
			}
		}

		// Token: 0x170023BC RID: 9148
		// (get) Token: 0x060064E8 RID: 25832 RVA: 0x0018EB37 File Offset: 0x0018CD37
		// (set) Token: 0x060064E9 RID: 25833 RVA: 0x0018EB3F File Offset: 0x0018CD3F
		internal int AggregationFieldCount
		{
			get
			{
				return this.m_aggregationFieldCount;
			}
			set
			{
				this.m_aggregationFieldCount = value;
			}
		}

		// Token: 0x060064EA RID: 25834 RVA: 0x0018EB48 File Offset: 0x0018CD48
		internal object GetFieldValue(int aliasIndex)
		{
			RecordField recordField = this.m_recordFields[aliasIndex];
			Global.Tracer.Assert(recordField != null);
			if (recordField.FieldStatus != DataFieldStatus.None)
			{
				throw new ReportProcessingException_FieldError(recordField.FieldStatus, ReportRuntime.GetErrorName(recordField.FieldStatus, null));
			}
			return recordField.FieldValue;
		}

		// Token: 0x060064EB RID: 25835 RVA: 0x0018EB92 File Offset: 0x0018CD92
		internal bool IsAggregationField(int aliasIndex)
		{
			return this.m_recordFields[aliasIndex].IsAggregationField;
		}

		// Token: 0x060064EC RID: 25836 RVA: 0x0018EBA4 File Offset: 0x0018CDA4
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.RecordFields, Token.Array, ObjectType.RecordField),
				new MemberInfo(MemberName.IsAggregateRow, Token.Boolean),
				new MemberInfo(MemberName.AggregationFieldCount, Token.Int32)
			});
		}

		// Token: 0x0400328F RID: 12943
		private RecordField[] m_recordFields;

		// Token: 0x04003290 RID: 12944
		private bool m_isAggregateRow;

		// Token: 0x04003291 RID: 12945
		private int m_aggregationFieldCount;
	}
}
