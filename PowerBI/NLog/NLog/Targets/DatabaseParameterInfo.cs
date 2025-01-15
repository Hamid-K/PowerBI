using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000032 RID: 50
	[NLogConfigurationItem]
	public class DatabaseParameterInfo
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x0000B558 File Offset: 0x00009758
		public DatabaseParameterInfo()
			: this(null, null)
		{
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000B562 File Offset: 0x00009762
		public DatabaseParameterInfo(string parameterName, Layout parameterLayout)
		{
			this.Name = parameterName;
			this.Layout = parameterLayout;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000B578 File Offset: 0x00009778
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0000B580 File Offset: 0x00009780
		[RequiredParameter]
		public string Name { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000B589 File Offset: 0x00009789
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0000B591 File Offset: 0x00009791
		[RequiredParameter]
		public Layout Layout { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000B59A File Offset: 0x0000979A
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x0000B5A2 File Offset: 0x000097A2
		[DefaultValue(null)]
		public string DbType { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0000B5AB File Offset: 0x000097AB
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x0000B5B3 File Offset: 0x000097B3
		[DefaultValue(0)]
		public int Size { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0000B5BC File Offset: 0x000097BC
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x0000B5C4 File Offset: 0x000097C4
		[DefaultValue(0)]
		public byte Precision { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0000B5CD File Offset: 0x000097CD
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x0000B5D5 File Offset: 0x000097D5
		[DefaultValue(0)]
		public byte Scale { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0000B5DE File Offset: 0x000097DE
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x0000B60A File Offset: 0x0000980A
		[DefaultValue(typeof(string))]
		public Type ParameterType
		{
			get
			{
				Type type;
				if ((type = this._parameterType) == null)
				{
					DatabaseParameterInfo.DbTypeSetter cachedDbTypeSetter = this._cachedDbTypeSetter;
					type = ((cachedDbTypeSetter != null) ? cachedDbTypeSetter.ParameterType : null) ?? typeof(string);
				}
				return type;
			}
			set
			{
				this._parameterType = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0000B613 File Offset: 0x00009813
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x0000B61B File Offset: 0x0000981B
		[DefaultValue(null)]
		public string Format { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000B624 File Offset: 0x00009824
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x0000B62C File Offset: 0x0000982C
		[DefaultValue(null)]
		public CultureInfo Culture { get; set; }

		// Token: 0x06000568 RID: 1384 RVA: 0x0000B638 File Offset: 0x00009838
		internal bool SetDbType(IDbDataParameter dbParameter)
		{
			if (!string.IsNullOrEmpty(this.DbType))
			{
				if (this._cachedDbTypeSetter == null || !this._cachedDbTypeSetter.IsValid(dbParameter.GetType(), this.DbType))
				{
					this._cachedDbTypeSetter = new DatabaseParameterInfo.DbTypeSetter(dbParameter.GetType(), this.DbType);
				}
				return this._cachedDbTypeSetter.SetDbType(dbParameter);
			}
			return true;
		}

		// Token: 0x040000A0 RID: 160
		private Type _parameterType;

		// Token: 0x040000A3 RID: 163
		private DatabaseParameterInfo.DbTypeSetter _cachedDbTypeSetter;

		// Token: 0x02000223 RID: 547
		private class DbTypeSetter
		{
			// Token: 0x170003E2 RID: 994
			// (get) Token: 0x060014EC RID: 5356 RVA: 0x0003798C File Offset: 0x00035B8C
			public Type ParameterType { get; }

			// Token: 0x060014ED RID: 5357 RVA: 0x00037994 File Offset: 0x00035B94
			public DbTypeSetter(Type dbParameterType, string dbTypeName)
			{
				this._dbPropertyInfoType = dbParameterType;
				this._dbTypeName = dbTypeName;
				if (!StringHelpers.IsNullOrWhiteSpace(dbTypeName))
				{
					string[] array = dbTypeName.SplitAndTrimTokens('.');
					if (array.Length > 1 && !string.Equals(array[0], "DbType", StringComparison.OrdinalIgnoreCase))
					{
						PropertyInfo property = dbParameterType.GetProperty(array[0], BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
						Enum @enum;
						if (property != null && ((DatabaseParameterInfo.DbTypeSetter.IEnumTypeConverter)Activator.CreateInstance(typeof(DatabaseParameterInfo.DbTypeSetter.EnumTypeConverter<>).MakeGenericType(new Type[] { property.PropertyType }))).TryParseEnum(array[1], out @enum))
						{
							this._dbTypeSetter = property;
							this._dbTypeValue = @enum;
							this.ParameterType = this.TryParseParameterType(@enum.ToString());
							return;
						}
					}
					else
					{
						DbType dbType;
						if (ConversionHelpers.TryParse<DbType>(array[array.Length - 1], out dbType))
						{
							this._dbTypeValue = dbType;
							this.ParameterType = DatabaseParameterInfo.DbTypeSetter.TryLookupParameterType(dbType);
							this._dbTypeSetterFast = delegate(IDbDataParameter p)
							{
								p.DbType = dbType;
							};
						}
					}
				}
			}

			// Token: 0x060014EE RID: 5358 RVA: 0x00037A9C File Offset: 0x00035C9C
			private static Type TryLookupParameterType(DbType dbType)
			{
				switch (dbType)
				{
				case global::System.Data.DbType.AnsiString:
				case global::System.Data.DbType.String:
				case global::System.Data.DbType.AnsiStringFixedLength:
				case global::System.Data.DbType.StringFixedLength:
				case global::System.Data.DbType.Xml:
					return typeof(string);
				case global::System.Data.DbType.Byte:
					return typeof(byte);
				case global::System.Data.DbType.Boolean:
					return typeof(bool);
				case global::System.Data.DbType.Currency:
				case global::System.Data.DbType.Decimal:
				case global::System.Data.DbType.VarNumeric:
					return typeof(decimal);
				case global::System.Data.DbType.Date:
				case global::System.Data.DbType.DateTime:
				case global::System.Data.DbType.DateTime2:
					return typeof(DateTime);
				case global::System.Data.DbType.Double:
					return typeof(double);
				case global::System.Data.DbType.Guid:
					return typeof(Guid);
				case global::System.Data.DbType.Int16:
					return typeof(short);
				case global::System.Data.DbType.Int32:
					return typeof(int);
				case global::System.Data.DbType.Int64:
					return typeof(long);
				case global::System.Data.DbType.Object:
					return typeof(object);
				case global::System.Data.DbType.SByte:
					return typeof(sbyte);
				case global::System.Data.DbType.Single:
					return typeof(float);
				case global::System.Data.DbType.Time:
					return typeof(TimeSpan);
				case global::System.Data.DbType.UInt16:
					return typeof(ushort);
				case global::System.Data.DbType.UInt32:
					return typeof(uint);
				case global::System.Data.DbType.UInt64:
					return typeof(ulong);
				case global::System.Data.DbType.DateTimeOffset:
					return typeof(DateTimeOffset);
				}
				return null;
			}

			// Token: 0x060014EF RID: 5359 RVA: 0x00037BEC File Offset: 0x00035DEC
			private Type TryParseParameterType(string dbTypeString)
			{
				if (dbTypeString.IndexOf("Date", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return typeof(DateTime);
				}
				if (dbTypeString.IndexOf("Timestamp", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return typeof(DateTime);
				}
				if (dbTypeString.IndexOf("Double", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return typeof(double);
				}
				if (dbTypeString.IndexOf("Decimal", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return typeof(decimal);
				}
				if (dbTypeString.IndexOf("Bool", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return typeof(bool);
				}
				if (dbTypeString.IndexOf("Guid", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return typeof(Guid);
				}
				return null;
			}

			// Token: 0x060014F0 RID: 5360 RVA: 0x00037C98 File Offset: 0x00035E98
			public bool IsValid(Type dbParameterType, string dbTypeName)
			{
				if (this._dbPropertyInfoType == dbParameterType && this._dbTypeName == dbTypeName)
				{
					if (this._dbTypeSetterFast == null && this._dbTypeSetter != null && this._dbTypeValue != null)
					{
						ReflectionHelpers.LateBoundMethod dbTypeSetterLambda = ReflectionHelpers.CreateLateBoundMethod(this._dbTypeSetter.GetSetMethod());
						object[] dbTypeSetterParams = new object[] { this._dbTypeValue };
						this._dbTypeSetterFast = delegate(IDbDataParameter p)
						{
							dbTypeSetterLambda(p, dbTypeSetterParams);
						};
					}
					return true;
				}
				return false;
			}

			// Token: 0x060014F1 RID: 5361 RVA: 0x00037D1C File Offset: 0x00035F1C
			public bool SetDbType(IDbDataParameter dbParameter)
			{
				if (this._dbTypeSetterFast != null)
				{
					this._dbTypeSetterFast(dbParameter);
					return true;
				}
				if (this._dbTypeSetter != null && this._dbTypeValue != null)
				{
					this._dbTypeSetter.SetValue(dbParameter, this._dbTypeValue, null);
					return true;
				}
				return false;
			}

			// Token: 0x040005E5 RID: 1509
			private readonly Type _dbPropertyInfoType;

			// Token: 0x040005E6 RID: 1510
			private readonly string _dbTypeName;

			// Token: 0x040005E7 RID: 1511
			private readonly PropertyInfo _dbTypeSetter;

			// Token: 0x040005E8 RID: 1512
			private readonly Enum _dbTypeValue;

			// Token: 0x040005E9 RID: 1513
			private Action<IDbDataParameter> _dbTypeSetterFast;

			// Token: 0x020002C7 RID: 711
			private interface IEnumTypeConverter
			{
				// Token: 0x06001776 RID: 6006
				bool TryParseEnum(string value, out Enum enumValue);
			}

			// Token: 0x020002C8 RID: 712
			private class EnumTypeConverter<TEnum> : DatabaseParameterInfo.DbTypeSetter.IEnumTypeConverter where TEnum : struct
			{
				// Token: 0x06001777 RID: 6007 RVA: 0x0003D5C4 File Offset: 0x0003B7C4
				bool DatabaseParameterInfo.DbTypeSetter.IEnumTypeConverter.TryParseEnum(string value, out Enum enumValue)
				{
					TEnum tenum;
					if (ConversionHelpers.TryParse<TEnum>(value, out tenum))
					{
						enumValue = tenum as Enum;
						return enumValue != null;
					}
					enumValue = null;
					return false;
				}
			}
		}
	}
}
