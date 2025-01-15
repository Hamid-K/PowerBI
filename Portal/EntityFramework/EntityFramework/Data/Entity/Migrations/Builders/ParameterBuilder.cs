using System;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Spatial;

namespace System.Data.Entity.Migrations.Builders
{
	// Token: 0x020000EA RID: 234
	public class ParameterBuilder
	{
		// Token: 0x060011CB RID: 4555 RVA: 0x0002DFF0 File Offset: 0x0002C1F0
		public ParameterModel Binary(int? maxLength = null, bool? fixedLength = null, byte[] defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Binary, defaultValue, defaultValueSql, maxLength, null, null, null, fixedLength, name, storeType, outParameter);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0002E02C File Offset: 0x0002C22C
		public ParameterModel Boolean(bool? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Boolean, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0002E078 File Offset: 0x0002C278
		public ParameterModel Byte(byte? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Byte, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0002E0C4 File Offset: 0x0002C2C4
		public ParameterModel DateTime(byte? precision = null, DateTime? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.DateTime, defaultValue, defaultValueSql, null, precision, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0002E10C File Offset: 0x0002C30C
		public ParameterModel Decimal(byte? precision = null, byte? scale = null, decimal? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Decimal, defaultValue, defaultValueSql, null, precision, scale, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0002E14C File Offset: 0x0002C34C
		public ParameterModel Double(double? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Double, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0002E198 File Offset: 0x0002C398
		public ParameterModel Guid(Guid? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Guid, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0002E1E4 File Offset: 0x0002C3E4
		public ParameterModel Single(float? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Single, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0002E230 File Offset: 0x0002C430
		public ParameterModel Short(short? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Int16, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0002E280 File Offset: 0x0002C480
		public ParameterModel Int(int? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Int32, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0002E2D0 File Offset: 0x0002C4D0
		public ParameterModel Long(long? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Int64, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0002E320 File Offset: 0x0002C520
		public ParameterModel String(int? maxLength = null, bool? fixedLength = null, bool? unicode = null, string defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.String, defaultValue, defaultValueSql, maxLength, null, null, unicode, fixedLength, name, storeType, outParameter);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0002E358 File Offset: 0x0002C558
		public ParameterModel Time(byte? precision = null, TimeSpan? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Time, defaultValue, defaultValueSql, null, precision, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0002E3A0 File Offset: 0x0002C5A0
		public ParameterModel DateTimeOffset(byte? precision = null, DateTimeOffset? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.DateTimeOffset, defaultValue, defaultValueSql, null, precision, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0002E3E8 File Offset: 0x0002C5E8
		public ParameterModel Geography(DbGeography defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Geography, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0002E430 File Offset: 0x0002C630
		public ParameterModel Geometry(DbGeometry defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return ParameterBuilder.BuildParameter(PrimitiveTypeKind.Geometry, defaultValue, defaultValueSql, null, null, null, null, null, name, storeType, outParameter);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0002E478 File Offset: 0x0002C678
		private static ParameterModel BuildParameter(PrimitiveTypeKind primitiveTypeKind, object defaultValue, string defaultValueSql = null, int? maxLength = null, byte? precision = null, byte? scale = null, bool? unicode = null, bool? fixedLength = null, string name = null, string storeType = null, bool outParameter = false)
		{
			return new ParameterModel(primitiveTypeKind)
			{
				MaxLength = maxLength,
				Precision = precision,
				Scale = scale,
				IsUnicode = unicode,
				IsFixedLength = fixedLength,
				DefaultValue = defaultValue,
				DefaultValueSql = defaultValueSql,
				Name = name,
				StoreType = storeType,
				IsOutParameter = outParameter
			};
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0002E4D8 File Offset: 0x0002C6D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0002E4E0 File Offset: 0x0002C6E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0002E4E9 File Offset: 0x0002C6E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0002E4F1 File Offset: 0x0002C6F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0002E4F9 File Offset: 0x0002C6F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected new object MemberwiseClone()
		{
			return base.MemberwiseClone();
		}
	}
}
