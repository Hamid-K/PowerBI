using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000710 RID: 1808
	[Serializable]
	internal sealed class RecordField
	{
		// Token: 0x060064F6 RID: 25846 RVA: 0x0018ECB5 File Offset: 0x0018CEB5
		internal RecordField(FieldImpl field)
		{
			this.m_fieldStatus = field.FieldStatus;
			this.m_properties = field.Properties;
			if (this.m_fieldStatus == DataFieldStatus.None)
			{
				this.m_fieldValue = field.Value;
				this.m_isAggregationField = field.IsAggregationField;
			}
		}

		// Token: 0x060064F7 RID: 25847 RVA: 0x0018ECF5 File Offset: 0x0018CEF5
		internal RecordField()
		{
		}

		// Token: 0x170023BF RID: 9151
		// (get) Token: 0x060064F8 RID: 25848 RVA: 0x0018ECFD File Offset: 0x0018CEFD
		// (set) Token: 0x060064F9 RID: 25849 RVA: 0x0018ED05 File Offset: 0x0018CF05
		internal object FieldValue
		{
			get
			{
				return this.m_fieldValue;
			}
			set
			{
				this.m_fieldValue = value;
			}
		}

		// Token: 0x170023C0 RID: 9152
		// (get) Token: 0x060064FA RID: 25850 RVA: 0x0018ED0E File Offset: 0x0018CF0E
		// (set) Token: 0x060064FB RID: 25851 RVA: 0x0018ED16 File Offset: 0x0018CF16
		internal bool IsAggregationField
		{
			get
			{
				return this.m_isAggregationField;
			}
			set
			{
				this.m_isAggregationField = value;
			}
		}

		// Token: 0x170023C1 RID: 9153
		// (get) Token: 0x060064FC RID: 25852 RVA: 0x0018ED1F File Offset: 0x0018CF1F
		// (set) Token: 0x060064FD RID: 25853 RVA: 0x0018ED27 File Offset: 0x0018CF27
		internal VariantList FieldPropertyValues
		{
			get
			{
				return this.m_fieldPropertyValues;
			}
			set
			{
				this.m_fieldPropertyValues = value;
			}
		}

		// Token: 0x170023C2 RID: 9154
		// (get) Token: 0x060064FE RID: 25854 RVA: 0x0018ED30 File Offset: 0x0018CF30
		internal bool IsOverflow
		{
			get
			{
				return DataFieldStatus.Overflow == this.m_fieldStatus;
			}
		}

		// Token: 0x170023C3 RID: 9155
		// (get) Token: 0x060064FF RID: 25855 RVA: 0x0018ED3B File Offset: 0x0018CF3B
		internal bool IsUnSupportedDataType
		{
			get
			{
				return DataFieldStatus.UnSupportedDataType == this.m_fieldStatus;
			}
		}

		// Token: 0x170023C4 RID: 9156
		// (get) Token: 0x06006500 RID: 25856 RVA: 0x0018ED46 File Offset: 0x0018CF46
		internal bool IsError
		{
			get
			{
				return DataFieldStatus.IsError == this.m_fieldStatus;
			}
		}

		// Token: 0x170023C5 RID: 9157
		// (get) Token: 0x06006501 RID: 25857 RVA: 0x0018ED51 File Offset: 0x0018CF51
		// (set) Token: 0x06006502 RID: 25858 RVA: 0x0018ED59 File Offset: 0x0018CF59
		internal DataFieldStatus FieldStatus
		{
			get
			{
				return this.m_fieldStatus;
			}
			set
			{
				Global.Tracer.Assert(this.m_fieldStatus == DataFieldStatus.None);
				this.m_fieldStatus = value;
			}
		}

		// Token: 0x06006503 RID: 25859 RVA: 0x0018ED75 File Offset: 0x0018CF75
		internal void SetProperty(string propertyName, object propertyValue)
		{
			if (this.m_properties == null)
			{
				this.m_properties = new Hashtable();
			}
			this.m_properties[propertyName] = propertyValue;
		}

		// Token: 0x06006504 RID: 25860 RVA: 0x0018ED97 File Offset: 0x0018CF97
		internal object GetProperty(string propertyName)
		{
			Global.Tracer.Assert(this.m_properties != null);
			return this.m_properties[propertyName];
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x0018EDB8 File Offset: 0x0018CFB8
		internal void PopulateFieldPropertyValues(StringList propertyNames)
		{
			if (propertyNames == null)
			{
				return;
			}
			int count = propertyNames.Count;
			this.m_fieldPropertyValues = new VariantList(count);
			for (int i = 0; i < count; i++)
			{
				object obj = null;
				if (this.m_properties != null)
				{
					obj = this.m_properties[propertyNames[i]];
				}
				this.m_fieldPropertyValues.Add(obj);
			}
		}

		// Token: 0x06006506 RID: 25862 RVA: 0x0018EE14 File Offset: 0x0018D014
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.FieldValue, ObjectType.Variant),
				new MemberInfo(MemberName.IsAggregateField, Token.Boolean),
				new MemberInfo(MemberName.FieldPropertyValues, ObjectType.VariantList)
			});
		}

		// Token: 0x04003293 RID: 12947
		private object m_fieldValue;

		// Token: 0x04003294 RID: 12948
		private bool m_isAggregationField;

		// Token: 0x04003295 RID: 12949
		private VariantList m_fieldPropertyValues;

		// Token: 0x04003296 RID: 12950
		[NonSerialized]
		private DataFieldStatus m_fieldStatus;

		// Token: 0x04003297 RID: 12951
		[NonSerialized]
		private Hashtable m_properties;
	}
}
