using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002AB RID: 683
	internal sealed class FieldInstance
	{
		// Token: 0x06001A33 RID: 6707 RVA: 0x00069F46 File Offset: 0x00068146
		internal FieldInstance(FieldInfo fieldInfo, RecordField recordField)
		{
			this.m_fieldInfo = fieldInfo;
			this.m_recordField = recordField;
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x00069F5C File Offset: 0x0006815C
		public object Value
		{
			get
			{
				if (this.IsMissingRecord)
				{
					return null;
				}
				return this.m_recordField.FieldValue;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x00069F73 File Offset: 0x00068173
		public bool IsAggregationField
		{
			get
			{
				return !this.IsMissingRecord && this.m_recordField.IsAggregationField;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x00069F8A File Offset: 0x0006818A
		public bool IsOverflow
		{
			get
			{
				return !this.IsMissingRecord && this.m_recordField.IsOverflow;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x00069FA1 File Offset: 0x000681A1
		public bool IsUnSupportedDataType
		{
			get
			{
				return !this.IsMissingRecord && this.m_recordField.IsUnSupportedDataType;
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00069FB8 File Offset: 0x000681B8
		public bool IsError
		{
			get
			{
				return this.IsMissingRecord || this.m_recordField.IsError;
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x00069FCF File Offset: 0x000681CF
		public ExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				if (this.m_extendedProperties == null && this.m_fieldInfo != null)
				{
					this.m_extendedProperties = new ExtendedPropertyCollection(this.m_recordField, this.m_fieldInfo.PropertyNames);
				}
				return this.m_extendedProperties;
			}
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0006A003 File Offset: 0x00068203
		internal void UpdateRecordField(RecordField field)
		{
			this.m_recordField = field;
			if (this.m_extendedProperties != null)
			{
				this.m_extendedProperties.UpdateRecordField(this.m_recordField);
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x0006A025 File Offset: 0x00068225
		private bool IsMissingRecord
		{
			get
			{
				return this.m_recordField == null;
			}
		}

		// Token: 0x04000D0F RID: 3343
		private RecordField m_recordField;

		// Token: 0x04000D10 RID: 3344
		private FieldInfo m_fieldInfo;

		// Token: 0x04000D11 RID: 3345
		private ExtendedPropertyCollection m_extendedProperties;
	}
}
