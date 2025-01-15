using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004F8 RID: 1272
	[Serializable]
	internal sealed class RecordField : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060040AB RID: 16555 RVA: 0x001109E6 File Offset: 0x0010EBE6
		internal RecordField()
		{
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x001109F0 File Offset: 0x0010EBF0
		internal RecordField(FieldImpl field, FieldInfo fieldInfo)
		{
			this.m_fieldStatus = field.FieldStatus;
			if (this.m_fieldStatus == DataFieldStatus.None)
			{
				this.m_fieldValue = field.Value;
				this.m_isAggregationField = field.IsAggregationField;
			}
			if (fieldInfo != null && 0 < fieldInfo.PropertyCount)
			{
				this.m_fieldPropertyValues = new List<object>(fieldInfo.PropertyCount);
				for (int i = 0; i < fieldInfo.PropertyCount; i++)
				{
					object property = field.GetProperty(fieldInfo.PropertyNames[i]);
					this.m_fieldPropertyValues.Add(property);
				}
			}
		}

		// Token: 0x17001B3A RID: 6970
		// (get) Token: 0x060040AD RID: 16557 RVA: 0x00110A7C File Offset: 0x0010EC7C
		// (set) Token: 0x060040AE RID: 16558 RVA: 0x00110A84 File Offset: 0x0010EC84
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

		// Token: 0x17001B3B RID: 6971
		// (get) Token: 0x060040AF RID: 16559 RVA: 0x00110A8D File Offset: 0x0010EC8D
		// (set) Token: 0x060040B0 RID: 16560 RVA: 0x00110A95 File Offset: 0x0010EC95
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

		// Token: 0x17001B3C RID: 6972
		// (get) Token: 0x060040B1 RID: 16561 RVA: 0x00110A9E File Offset: 0x0010EC9E
		// (set) Token: 0x060040B2 RID: 16562 RVA: 0x00110AA6 File Offset: 0x0010ECA6
		internal List<object> FieldPropertyValues
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

		// Token: 0x17001B3D RID: 6973
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x00110AAF File Offset: 0x0010ECAF
		internal bool IsOverflow
		{
			get
			{
				return DataFieldStatus.Overflow == this.m_fieldStatus;
			}
		}

		// Token: 0x17001B3E RID: 6974
		// (get) Token: 0x060040B4 RID: 16564 RVA: 0x00110ABA File Offset: 0x0010ECBA
		internal bool IsUnSupportedDataType
		{
			get
			{
				return DataFieldStatus.UnSupportedDataType == this.m_fieldStatus;
			}
		}

		// Token: 0x17001B3F RID: 6975
		// (get) Token: 0x060040B5 RID: 16565 RVA: 0x00110AC5 File Offset: 0x0010ECC5
		internal bool IsError
		{
			get
			{
				return DataFieldStatus.IsError == this.m_fieldStatus;
			}
		}

		// Token: 0x17001B40 RID: 6976
		// (get) Token: 0x060040B6 RID: 16566 RVA: 0x00110AD0 File Offset: 0x0010ECD0
		// (set) Token: 0x060040B7 RID: 16567 RVA: 0x00110AD8 File Offset: 0x0010ECD8
		internal DataFieldStatus FieldStatus
		{
			get
			{
				return this.m_fieldStatus;
			}
			set
			{
				Global.Tracer.Assert(this.m_fieldStatus == DataFieldStatus.None, "(DataFieldStatus.None == m_fieldStatus)");
				this.m_fieldStatus = value;
			}
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x00110AFC File Offset: 0x0010ECFC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RecordField, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new ReadOnlyMemberInfo(MemberName.FieldValue, Token.Object),
				new MemberInfo(MemberName.FieldValueSerializable, Token.Serializable),
				new MemberInfo(MemberName.FieldStatus, Token.Enum),
				new MemberInfo(MemberName.IsAggregateField, Token.Boolean),
				new MemberInfo(MemberName.FieldPropertyValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Object)
			});
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x00110B6C File Offset: 0x0010ED6C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RecordField.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.FieldStatus:
					writer.WriteEnum((int)this.m_fieldStatus);
					continue;
				case MemberName.FieldValue:
					break;
				case MemberName.IsAggregateField:
					writer.Write(this.m_isAggregationField);
					continue;
				case MemberName.FieldPropertyValues:
					writer.WriteListOfPrimitives<object>(this.m_fieldPropertyValues);
					continue;
				default:
					if (memberName == MemberName.FieldValueSerializable)
					{
						if (!writer.TryWriteSerializable(this.m_fieldValue))
						{
							this.m_fieldValue = null;
							writer.WriteNull();
							this.m_fieldStatus = DataFieldStatus.UnSupportedDataType;
							continue;
						}
						continue;
					}
					break;
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x00110C24 File Offset: 0x0010EE24
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RecordField.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.FieldStatus:
					this.m_fieldStatus = (DataFieldStatus)reader.ReadEnum();
					break;
				case MemberName.FieldValue:
					this.m_fieldValue = reader.ReadVariant();
					break;
				case MemberName.IsAggregateField:
					this.m_isAggregationField = reader.ReadBoolean();
					break;
				case MemberName.FieldPropertyValues:
					this.m_fieldPropertyValues = reader.ReadListOfPrimitives<object>();
					break;
				default:
					if (memberName != MemberName.FieldValueSerializable)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_fieldValue = reader.ReadSerializable();
					}
					break;
				}
			}
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x00110CD3 File Offset: 0x0010EED3
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x00110CE0 File Offset: 0x0010EEE0
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RecordField;
		}

		// Token: 0x04001DA8 RID: 7592
		private object m_fieldValue;

		// Token: 0x04001DA9 RID: 7593
		private bool m_isAggregationField;

		// Token: 0x04001DAA RID: 7594
		private List<object> m_fieldPropertyValues;

		// Token: 0x04001DAB RID: 7595
		private DataFieldStatus m_fieldStatus;

		// Token: 0x04001DAC RID: 7596
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = RecordField.GetDeclaration();
	}
}
