using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007AF RID: 1967
	public class FieldImpl : Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.Field, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06006FA4 RID: 28580 RVA: 0x001D19C6 File Offset: 0x001CFBC6
		internal FieldImpl()
		{
		}

		// Token: 0x06006FA5 RID: 28581 RVA: 0x001D19CE File Offset: 0x001CFBCE
		internal FieldImpl(ObjectModelImpl reportOM, object value, bool isAggregationField, Microsoft.ReportingServices.ReportIntermediateFormat.Field fieldDef)
		{
			this.m_reportOM = reportOM;
			this.m_fieldDef = fieldDef;
			this.UpdateValue(value, isAggregationField, DataFieldStatus.None, null);
		}

		// Token: 0x06006FA6 RID: 28582 RVA: 0x001D19EF File Offset: 0x001CFBEF
		internal FieldImpl(ObjectModelImpl reportOM, DataFieldStatus status, string exceptionMessage, Microsoft.ReportingServices.ReportIntermediateFormat.Field fieldDef)
		{
			this.m_reportOM = reportOM;
			this.m_fieldDef = fieldDef;
			Global.Tracer.Assert(status > DataFieldStatus.None, "(DataFieldStatus.None != status)");
			this.UpdateValue(null, false, status, exceptionMessage);
		}

		// Token: 0x06006FA7 RID: 28583 RVA: 0x001D1A23 File Offset: 0x001CFC23
		internal void UpdateValue(object value, bool isAggregationField, DataFieldStatus status, string exceptionMessage)
		{
			this.m_value = value;
			this.m_isAggregationField = isAggregationField;
			this.m_aggregationFieldChecked = false;
			this.m_fieldStatus = status;
			this.m_exceptionMessage = exceptionMessage;
			this.m_usedInExpression = false;
			this.m_properties = null;
		}

		// Token: 0x06006FA8 RID: 28584 RVA: 0x001D1A57 File Offset: 0x001CFC57
		internal bool ResetCalculatedField()
		{
			if (this.m_value == null)
			{
				return false;
			}
			this.m_fieldStatus = DataFieldStatus.None;
			((CalculatedFieldWrapperImpl)this.m_value).ResetValue();
			this.m_usedInExpression = false;
			return true;
		}

		// Token: 0x170025FE RID: 9726
		public override object this[string key]
		{
			get
			{
				if (key == null)
				{
					return null;
				}
				this.m_reportOM.PerformPendingFieldValueUpdate();
				this.m_usedInExpression = true;
				if (ReportProcessing.CompareWithInvariantCulture(key, "Value", true) == 0)
				{
					return this.Value;
				}
				if (ReportProcessing.CompareWithInvariantCulture(key, "IsMissing", true) == 0)
				{
					return this.IsMissing;
				}
				if (ReportProcessing.CompareWithInvariantCulture(key, "LevelNumber", true) == 0)
				{
					return this.LevelNumber;
				}
				return this.GetProperty(key);
			}
		}

		// Token: 0x170025FF RID: 9727
		// (get) Token: 0x06006FAA RID: 28586 RVA: 0x001D1AF8 File Offset: 0x001CFCF8
		public override object Value
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				this.m_usedInExpression = true;
				if (this.m_fieldStatus != DataFieldStatus.None)
				{
					throw new ReportProcessingException_FieldError(this.m_fieldStatus, this.m_exceptionMessage);
				}
				if (this.IsCalculatedField && this.m_value != null)
				{
					return ((CalculatedFieldWrapper)this.m_value).Value;
				}
				return this.m_value;
			}
		}

		// Token: 0x17002600 RID: 9728
		// (get) Token: 0x06006FAB RID: 28587 RVA: 0x001D1B58 File Offset: 0x001CFD58
		public override bool IsMissing
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				return DataFieldStatus.IsMissing == this.m_fieldStatus;
			}
		}

		// Token: 0x17002601 RID: 9729
		// (get) Token: 0x06006FAC RID: 28588 RVA: 0x001D1B6E File Offset: 0x001CFD6E
		public override string UniqueName
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				return this.GetProperty("UniqueName") as string;
			}
		}

		// Token: 0x17002602 RID: 9730
		// (get) Token: 0x06006FAD RID: 28589 RVA: 0x001D1B8B File Offset: 0x001CFD8B
		public override string BackgroundColor
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				return this.GetProperty("BackgroundColor") as string;
			}
		}

		// Token: 0x17002603 RID: 9731
		// (get) Token: 0x06006FAE RID: 28590 RVA: 0x001D1BA8 File Offset: 0x001CFDA8
		public override string Color
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				return this.GetProperty("Color") as string;
			}
		}

		// Token: 0x17002604 RID: 9732
		// (get) Token: 0x06006FAF RID: 28591 RVA: 0x001D1BC5 File Offset: 0x001CFDC5
		public override string FontFamily
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				return this.GetProperty("FontFamily") as string;
			}
		}

		// Token: 0x17002605 RID: 9733
		// (get) Token: 0x06006FB0 RID: 28592 RVA: 0x001D1BE2 File Offset: 0x001CFDE2
		public override string FontSize
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				return this.GetProperty("FontSize") as string;
			}
		}

		// Token: 0x17002606 RID: 9734
		// (get) Token: 0x06006FB1 RID: 28593 RVA: 0x001D1BFF File Offset: 0x001CFDFF
		public override string FontWeight
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				return this.GetProperty("FontWeight") as string;
			}
		}

		// Token: 0x17002607 RID: 9735
		// (get) Token: 0x06006FB2 RID: 28594 RVA: 0x001D1C1C File Offset: 0x001CFE1C
		public override string FontStyle
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				return this.GetProperty("FontStyle") as string;
			}
		}

		// Token: 0x17002608 RID: 9736
		// (get) Token: 0x06006FB3 RID: 28595 RVA: 0x001D1C39 File Offset: 0x001CFE39
		public override string TextDecoration
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				return this.GetProperty("TextDecoration") as string;
			}
		}

		// Token: 0x17002609 RID: 9737
		// (get) Token: 0x06006FB4 RID: 28596 RVA: 0x001D1C56 File Offset: 0x001CFE56
		public override string FormattedValue
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				return this.GetProperty("FormattedValue") as string;
			}
		}

		// Token: 0x1700260A RID: 9738
		// (get) Token: 0x06006FB5 RID: 28597 RVA: 0x001D1C73 File Offset: 0x001CFE73
		public override object Key
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				return this.GetProperty("Key");
			}
		}

		// Token: 0x1700260B RID: 9739
		// (get) Token: 0x06006FB6 RID: 28598 RVA: 0x001D1C8C File Offset: 0x001CFE8C
		public override int LevelNumber
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				object property = this.GetProperty("LevelNumber");
				if (property == null)
				{
					return 0;
				}
				if (property is int)
				{
					return (int)property;
				}
				bool flag;
				int num = DataTypeUtility.ConvertToInt32(DataAggregate.GetTypeCode(property), property, out flag);
				if (flag)
				{
					return num;
				}
				return 0;
			}
		}

		// Token: 0x1700260C RID: 9740
		// (get) Token: 0x06006FB7 RID: 28599 RVA: 0x001D1CD9 File Offset: 0x001CFED9
		public override string ParentUniqueName
		{
			get
			{
				this.m_reportOM.PerformPendingFieldValueUpdate();
				return this.GetProperty("ParentUniqueName") as string;
			}
		}

		// Token: 0x1700260D RID: 9741
		// (get) Token: 0x06006FB8 RID: 28600 RVA: 0x001D1CF8 File Offset: 0x001CFEF8
		internal DataFieldStatus FieldStatus
		{
			get
			{
				if (DataFieldStatus.IsMissing != this.m_fieldStatus)
				{
					this.m_reportOM.PerformPendingFieldValueUpdate();
				}
				if (this.IsCalculatedField)
				{
					if (this.m_value == null)
					{
						return DataFieldStatus.None;
					}
					if (((CalculatedFieldWrapperImpl)this.m_value).ErrorOccurred)
					{
						this.m_exceptionMessage = ((CalculatedFieldWrapperImpl)this.m_value).ExceptionMessage;
						return DataFieldStatus.IsError;
					}
				}
				return this.m_fieldStatus;
			}
		}

		// Token: 0x1700260E RID: 9742
		// (get) Token: 0x06006FB9 RID: 28601 RVA: 0x001D1D5B File Offset: 0x001CFF5B
		internal string ExceptionMessage
		{
			get
			{
				if (!this.IsCalculatedField)
				{
					this.m_reportOM.PerformPendingFieldValueUpdate();
				}
				return this.m_exceptionMessage;
			}
		}

		// Token: 0x1700260F RID: 9743
		// (get) Token: 0x06006FBA RID: 28602 RVA: 0x001D1D76 File Offset: 0x001CFF76
		internal bool IsAggregationField
		{
			get
			{
				if (!this.IsCalculatedField)
				{
					this.m_reportOM.PerformPendingFieldValueUpdate();
				}
				return this.m_isAggregationField;
			}
		}

		// Token: 0x17002610 RID: 9744
		// (get) Token: 0x06006FBB RID: 28603 RVA: 0x001D1D91 File Offset: 0x001CFF91
		// (set) Token: 0x06006FBC RID: 28604 RVA: 0x001D1D99 File Offset: 0x001CFF99
		internal bool AggregationFieldChecked
		{
			get
			{
				return this.m_aggregationFieldChecked;
			}
			set
			{
				this.m_aggregationFieldChecked = value;
			}
		}

		// Token: 0x17002611 RID: 9745
		// (get) Token: 0x06006FBD RID: 28605 RVA: 0x001D1DA2 File Offset: 0x001CFFA2
		internal Hashtable Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x17002612 RID: 9746
		// (get) Token: 0x06006FBE RID: 28606 RVA: 0x001D1DAA File Offset: 0x001CFFAA
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Field FieldDef
		{
			get
			{
				return this.m_fieldDef;
			}
		}

		// Token: 0x17002613 RID: 9747
		// (get) Token: 0x06006FBF RID: 28607 RVA: 0x001D1DB2 File Offset: 0x001CFFB2
		// (set) Token: 0x06006FC0 RID: 28608 RVA: 0x001D1DBA File Offset: 0x001CFFBA
		internal bool UsedInExpression
		{
			get
			{
				return this.m_usedInExpression;
			}
			set
			{
				this.m_usedInExpression = value;
			}
		}

		// Token: 0x17002614 RID: 9748
		// (get) Token: 0x06006FC1 RID: 28609 RVA: 0x001D1DC3 File Offset: 0x001CFFC3
		internal bool IsCalculatedField
		{
			get
			{
				return this.m_fieldDef != null && this.m_fieldDef.IsCalculatedField;
			}
		}

		// Token: 0x06006FC2 RID: 28610 RVA: 0x001D1DDA File Offset: 0x001CFFDA
		internal void SetValue(object value)
		{
			this.m_value = value;
		}

		// Token: 0x06006FC3 RID: 28611 RVA: 0x001D1DE3 File Offset: 0x001CFFE3
		internal void SetProperty(string propertyName, object propertyValue)
		{
			if (this.m_properties == null)
			{
				this.m_properties = new Hashtable();
			}
			this.m_properties[propertyName] = propertyValue;
		}

		// Token: 0x06006FC4 RID: 28612 RVA: 0x001D1E05 File Offset: 0x001D0005
		internal object GetProperty(string propertyName)
		{
			if (this.m_properties == null)
			{
				return null;
			}
			this.m_usedInExpression = true;
			return this.m_properties[propertyName];
		}

		// Token: 0x06006FC5 RID: 28613 RVA: 0x001D1E24 File Offset: 0x001D0024
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(FieldImpl.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.IsAggregateField)
				{
					if (memberName == MemberName.FieldStatus)
					{
						writer.WriteEnum((int)this.m_fieldStatus);
						continue;
					}
					if (memberName == MemberName.IsAggregateField)
					{
						writer.Write(this.m_isAggregationField);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Value)
					{
						writer.WriteVariantOrPersistable(this.m_value);
						continue;
					}
					switch (memberName)
					{
					case MemberName.ReportObjectModel:
					{
						int num = scalabilityCache.StoreStaticReference(this.m_reportOM);
						writer.Write(num);
						continue;
					}
					case MemberName.AggregationFieldChecked:
						writer.Write(this.m_aggregationFieldChecked);
						continue;
					case MemberName.Properties:
						writer.WriteStringObjectHashtable(this.m_properties);
						continue;
					case MemberName.FieldDef:
					{
						int num2 = scalabilityCache.StoreStaticReference(this.m_fieldDef);
						writer.Write(num2);
						continue;
					}
					case MemberName.UsedInExpression:
						writer.Write(this.m_usedInExpression);
						continue;
					default:
						if (memberName == MemberName.Message)
						{
							writer.Write(this.m_exceptionMessage);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06006FC6 RID: 28614 RVA: 0x001D1F54 File Offset: 0x001D0154
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(FieldImpl.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.IsAggregateField)
				{
					if (memberName == MemberName.FieldStatus)
					{
						this.m_fieldStatus = (DataFieldStatus)reader.ReadEnum();
						continue;
					}
					if (memberName == MemberName.IsAggregateField)
					{
						this.m_isAggregationField = reader.ReadBoolean();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Value)
					{
						this.m_value = reader.ReadVariant();
						continue;
					}
					switch (memberName)
					{
					case MemberName.ReportObjectModel:
					{
						int num = reader.ReadInt32();
						this.m_reportOM = (ObjectModelImpl)scalabilityCache.FetchStaticReference(num);
						continue;
					}
					case MemberName.AggregationFieldChecked:
						this.m_aggregationFieldChecked = reader.ReadBoolean();
						continue;
					case MemberName.Properties:
						this.m_properties = reader.ReadStringObjectHashtable<Hashtable>();
						continue;
					case MemberName.FieldDef:
					{
						int num2 = reader.ReadInt32();
						this.m_fieldDef = (Microsoft.ReportingServices.ReportIntermediateFormat.Field)scalabilityCache.FetchStaticReference(num2);
						continue;
					}
					case MemberName.UsedInExpression:
						this.m_usedInExpression = reader.ReadBoolean();
						continue;
					default:
						if (memberName == MemberName.Message)
						{
							this.m_exceptionMessage = reader.ReadString();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06006FC7 RID: 28615 RVA: 0x001D208F File Offset: 0x001D028F
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06006FC8 RID: 28616 RVA: 0x001D2091 File Offset: 0x001D0291
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FieldImpl;
		}

		// Token: 0x06006FC9 RID: 28617 RVA: 0x001D2098 File Offset: 0x001D0298
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (FieldImpl.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FieldImpl, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.ReportObjectModel, Token.Int32),
					new MemberInfo(MemberName.Value, Token.Object),
					new MemberInfo(MemberName.IsAggregateField, Token.Boolean),
					new MemberInfo(MemberName.AggregationFieldChecked, Token.Boolean),
					new MemberInfo(MemberName.FieldStatus, Token.Enum),
					new MemberInfo(MemberName.Message, Token.String),
					new MemberInfo(MemberName.Properties, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringObjectHashtable),
					new MemberInfo(MemberName.FieldDef, Token.Int32),
					new MemberInfo(MemberName.UsedInExpression, Token.Boolean)
				});
			}
			return FieldImpl.m_declaration;
		}

		// Token: 0x17002615 RID: 9749
		// (get) Token: 0x06006FCA RID: 28618 RVA: 0x001D215D File Offset: 0x001D035D
		public int Size
		{
			get
			{
				return ItemSizes.ReferenceSize + ItemSizes.SizeOf(this.m_value) + 1 + 1 + 4 + ItemSizes.SizeOf(this.m_exceptionMessage) + ItemSizes.SizeOf(this.m_properties) + ItemSizes.ReferenceSize + 1;
			}
		}

		// Token: 0x040039B8 RID: 14776
		[StaticReference]
		private ObjectModelImpl m_reportOM;

		// Token: 0x040039B9 RID: 14777
		private object m_value;

		// Token: 0x040039BA RID: 14778
		private bool m_isAggregationField;

		// Token: 0x040039BB RID: 14779
		private bool m_aggregationFieldChecked;

		// Token: 0x040039BC RID: 14780
		private DataFieldStatus m_fieldStatus;

		// Token: 0x040039BD RID: 14781
		private string m_exceptionMessage;

		// Token: 0x040039BE RID: 14782
		private Hashtable m_properties;

		// Token: 0x040039BF RID: 14783
		[StaticReference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Field m_fieldDef;

		// Token: 0x040039C0 RID: 14784
		private bool m_usedInExpression;

		// Token: 0x040039C1 RID: 14785
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = FieldImpl.GetDeclaration();
	}
}
