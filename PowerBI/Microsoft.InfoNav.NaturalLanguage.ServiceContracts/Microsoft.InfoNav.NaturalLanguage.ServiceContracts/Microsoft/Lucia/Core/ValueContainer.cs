using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000075 RID: 117
	[DataContract(Name = "Value", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	internal sealed class ValueContainer
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x00004C97 File Offset: 0x00002E97
		internal ValueContainer(DataValue value)
		{
			this._value = value;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00004CA6 File Offset: 0x00002EA6
		internal DataValue Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00004CAE File Offset: 0x00002EAE
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00004CB6 File Offset: 0x00002EB6
		[DataMember(Name = "StringValue", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private string StringValue
		{
			get
			{
				return this.GetValue<string>();
			}
			set
			{
				this.SetValue(value);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00004CC4 File Offset: 0x00002EC4
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00004CCC File Offset: 0x00002ECC
		[DataMember(Name = "NumberValue", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		private decimal? NumberValue
		{
			get
			{
				return this.GetNullableValue<decimal>();
			}
			set
			{
				this.SetValue((value != null) ? value.Value : null);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00004CEC File Offset: 0x00002EEC
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00004CF4 File Offset: 0x00002EF4
		[DataMember(Name = "IntegerValue", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		private long? IntegerValue
		{
			get
			{
				return this.GetNullableValue<long>();
			}
			set
			{
				this.SetValue((value != null) ? value.Value : null);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00004D14 File Offset: 0x00002F14
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00004D1C File Offset: 0x00002F1C
		[DataMember(Name = "BooleanValue", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		private bool? BooleanValue
		{
			get
			{
				return this.GetNullableValue<bool>();
			}
			set
			{
				this.SetValue((value != null) ? value.Value : null);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00004D3C File Offset: 0x00002F3C
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00004E04 File Offset: 0x00003004
		[DataMember(Name = "DateTimeValue", IsRequired = false, EmitDefaultValue = false, Order = 5)]
		private DateTime? DateTimeValue
		{
			get
			{
				DateItemValue dateItemValue = this._value as DateItemValue;
				if (dateItemValue == null || this._value.Type != DataType.DateTime)
				{
					return null;
				}
				return new DateTime?(new DateTime(dateItemValue.Value.Year.Value, dateItemValue.Value.Month.Value, dateItemValue.Value.Day.Value, dateItemValue.Value.Hour.Value, dateItemValue.Value.Minute.Value, dateItemValue.Value.Second.Value, dateItemValue.Value.Millisecond.Value, DateTimeKind.Utc));
			}
			set
			{
				this.SetValue((value != null) ? value.Value : null);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00004E24 File Offset: 0x00003024
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00004E68 File Offset: 0x00003068
		[DataMember(Name = "YearValue", IsRequired = false, EmitDefaultValue = false, Order = 6)]
		private int? YearValue
		{
			get
			{
				DateItemValue dateItemValue = this._value as DateItemValue;
				if (dateItemValue == null || this._value.Type != DataType.Year)
				{
					return null;
				}
				return dateItemValue.Value.Year;
			}
			set
			{
				this.SetValue((value != null) ? new DateItemValue(new int?(value.Value), null, null, null, null, null, null) : null);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00004ED0 File Offset: 0x000030D0
		private T GetValue<T>() where T : class
		{
			DataValue<T> dataValue = this._value as DataValue<T>;
			if (dataValue == null)
			{
				return default(T);
			}
			return dataValue.Value;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00004EFC File Offset: 0x000030FC
		private T? GetNullableValue<T>() where T : struct
		{
			DataValue<T> dataValue = this._value as DataValue<T>;
			if (dataValue == null)
			{
				return null;
			}
			return new T?(dataValue.Value);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00004F2D File Offset: 0x0000312D
		private void SetValue(DataValue value)
		{
			this._value = value;
		}

		// Token: 0x0400027B RID: 635
		private DataValue _value;
	}
}
