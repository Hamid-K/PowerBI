using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x0200078E RID: 1934
	internal sealed class FieldImpl : Field
	{
		// Token: 0x06006BFC RID: 27644 RVA: 0x001B6846 File Offset: 0x001B4A46
		internal FieldImpl(object value, bool isAggregationField, Field fieldDef)
		{
			this.m_value = value;
			this.m_isAggregationField = isAggregationField;
			this.m_aggregationFieldChecked = false;
			this.m_fieldStatus = DataFieldStatus.None;
			this.m_fieldDef = fieldDef;
			this.m_usedInExpression = false;
		}

		// Token: 0x06006BFD RID: 27645 RVA: 0x001B6878 File Offset: 0x001B4A78
		internal FieldImpl(DataFieldStatus status, string exceptionMessage, Field fieldDef)
		{
			this.m_value = null;
			this.m_isAggregationField = false;
			this.m_aggregationFieldChecked = false;
			Global.Tracer.Assert(status > DataFieldStatus.None, "(DataFieldStatus.None != status)");
			this.m_fieldStatus = status;
			this.m_exceptionMessage = exceptionMessage;
			this.m_fieldDef = fieldDef;
			this.m_usedInExpression = false;
		}

		// Token: 0x17002581 RID: 9601
		public override object this[string key]
		{
			get
			{
				if (key == null)
				{
					return null;
				}
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

		// Token: 0x17002582 RID: 9602
		// (get) Token: 0x06006BFF RID: 27647 RVA: 0x001B6932 File Offset: 0x001B4B32
		public override object Value
		{
			get
			{
				if (this.m_fieldStatus != DataFieldStatus.None)
				{
					throw new ReportProcessingException_FieldError(this.m_fieldStatus, this.m_exceptionMessage);
				}
				if (this.m_value is CalculatedFieldWrapper)
				{
					return ((CalculatedFieldWrapper)this.m_value).Value;
				}
				return this.m_value;
			}
		}

		// Token: 0x17002583 RID: 9603
		// (get) Token: 0x06006C00 RID: 27648 RVA: 0x001B6972 File Offset: 0x001B4B72
		public override bool IsMissing
		{
			get
			{
				return DataFieldStatus.IsMissing == this.m_fieldStatus;
			}
		}

		// Token: 0x17002584 RID: 9604
		// (get) Token: 0x06006C01 RID: 27649 RVA: 0x001B697D File Offset: 0x001B4B7D
		public override string UniqueName
		{
			get
			{
				return this.GetProperty("UniqueName") as string;
			}
		}

		// Token: 0x17002585 RID: 9605
		// (get) Token: 0x06006C02 RID: 27650 RVA: 0x001B698F File Offset: 0x001B4B8F
		public override string BackgroundColor
		{
			get
			{
				return this.GetProperty("BackgroundColor") as string;
			}
		}

		// Token: 0x17002586 RID: 9606
		// (get) Token: 0x06006C03 RID: 27651 RVA: 0x001B69A1 File Offset: 0x001B4BA1
		public override string Color
		{
			get
			{
				return this.GetProperty("Color") as string;
			}
		}

		// Token: 0x17002587 RID: 9607
		// (get) Token: 0x06006C04 RID: 27652 RVA: 0x001B69B3 File Offset: 0x001B4BB3
		public override string FontFamily
		{
			get
			{
				return this.GetProperty("FontFamily") as string;
			}
		}

		// Token: 0x17002588 RID: 9608
		// (get) Token: 0x06006C05 RID: 27653 RVA: 0x001B69C5 File Offset: 0x001B4BC5
		public override string FontSize
		{
			get
			{
				return this.GetProperty("FontSize") as string;
			}
		}

		// Token: 0x17002589 RID: 9609
		// (get) Token: 0x06006C06 RID: 27654 RVA: 0x001B69D7 File Offset: 0x001B4BD7
		public override string FontWeight
		{
			get
			{
				return this.GetProperty("FontWeight") as string;
			}
		}

		// Token: 0x1700258A RID: 9610
		// (get) Token: 0x06006C07 RID: 27655 RVA: 0x001B69E9 File Offset: 0x001B4BE9
		public override string FontStyle
		{
			get
			{
				return this.GetProperty("FontStyle") as string;
			}
		}

		// Token: 0x1700258B RID: 9611
		// (get) Token: 0x06006C08 RID: 27656 RVA: 0x001B69FB File Offset: 0x001B4BFB
		public override string TextDecoration
		{
			get
			{
				return this.GetProperty("TextDecoration") as string;
			}
		}

		// Token: 0x1700258C RID: 9612
		// (get) Token: 0x06006C09 RID: 27657 RVA: 0x001B6A0D File Offset: 0x001B4C0D
		public override string FormattedValue
		{
			get
			{
				return this.GetProperty("FormattedValue") as string;
			}
		}

		// Token: 0x1700258D RID: 9613
		// (get) Token: 0x06006C0A RID: 27658 RVA: 0x001B6A1F File Offset: 0x001B4C1F
		public override object Key
		{
			get
			{
				return this.GetProperty("Key");
			}
		}

		// Token: 0x1700258E RID: 9614
		// (get) Token: 0x06006C0B RID: 27659 RVA: 0x001B6A2C File Offset: 0x001B4C2C
		public override int LevelNumber
		{
			get
			{
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

		// Token: 0x1700258F RID: 9615
		// (get) Token: 0x06006C0C RID: 27660 RVA: 0x001B6A6E File Offset: 0x001B4C6E
		public override string ParentUniqueName
		{
			get
			{
				return this.GetProperty("ParentUniqueName") as string;
			}
		}

		// Token: 0x17002590 RID: 9616
		// (get) Token: 0x06006C0D RID: 27661 RVA: 0x001B6A80 File Offset: 0x001B4C80
		internal DataFieldStatus FieldStatus
		{
			get
			{
				return this.m_fieldStatus;
			}
		}

		// Token: 0x17002591 RID: 9617
		// (get) Token: 0x06006C0E RID: 27662 RVA: 0x001B6A88 File Offset: 0x001B4C88
		internal string ExceptionMessage
		{
			get
			{
				return this.m_exceptionMessage;
			}
		}

		// Token: 0x17002592 RID: 9618
		// (get) Token: 0x06006C0F RID: 27663 RVA: 0x001B6A90 File Offset: 0x001B4C90
		internal bool IsAggregationField
		{
			get
			{
				return this.m_isAggregationField;
			}
		}

		// Token: 0x17002593 RID: 9619
		// (get) Token: 0x06006C10 RID: 27664 RVA: 0x001B6A98 File Offset: 0x001B4C98
		// (set) Token: 0x06006C11 RID: 27665 RVA: 0x001B6AA0 File Offset: 0x001B4CA0
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

		// Token: 0x17002594 RID: 9620
		// (get) Token: 0x06006C12 RID: 27666 RVA: 0x001B6AA9 File Offset: 0x001B4CA9
		internal Hashtable Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x17002595 RID: 9621
		// (get) Token: 0x06006C13 RID: 27667 RVA: 0x001B6AB1 File Offset: 0x001B4CB1
		internal Field FieldDef
		{
			get
			{
				return this.m_fieldDef;
			}
		}

		// Token: 0x17002596 RID: 9622
		// (get) Token: 0x06006C14 RID: 27668 RVA: 0x001B6AB9 File Offset: 0x001B4CB9
		// (set) Token: 0x06006C15 RID: 27669 RVA: 0x001B6AC1 File Offset: 0x001B4CC1
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

		// Token: 0x06006C16 RID: 27670 RVA: 0x001B6ACA File Offset: 0x001B4CCA
		internal void SetValue(object value)
		{
			this.m_value = value;
		}

		// Token: 0x06006C17 RID: 27671 RVA: 0x001B6AD3 File Offset: 0x001B4CD3
		internal void SetProperty(string propertyName, object propertyValue)
		{
			if (this.m_properties == null)
			{
				this.m_properties = new Hashtable();
			}
			this.m_properties[propertyName] = propertyValue;
		}

		// Token: 0x06006C18 RID: 27672 RVA: 0x001B6AF5 File Offset: 0x001B4CF5
		private object GetProperty(string propertyName)
		{
			if (this.m_properties == null)
			{
				return null;
			}
			return this.m_properties[propertyName];
		}

		// Token: 0x04003638 RID: 13880
		private object m_value;

		// Token: 0x04003639 RID: 13881
		private bool m_isAggregationField;

		// Token: 0x0400363A RID: 13882
		private bool m_aggregationFieldChecked;

		// Token: 0x0400363B RID: 13883
		private DataFieldStatus m_fieldStatus;

		// Token: 0x0400363C RID: 13884
		private string m_exceptionMessage;

		// Token: 0x0400363D RID: 13885
		private Hashtable m_properties;

		// Token: 0x0400363E RID: 13886
		private Field m_fieldDef;

		// Token: 0x0400363F RID: 13887
		private bool m_usedInExpression;
	}
}
