using System;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x020005E9 RID: 1513
	public class DbCommandDefinition
	{
		// Token: 0x060049DE RID: 18910 RVA: 0x001061F1 File Offset: 0x001043F1
		protected internal DbCommandDefinition(DbCommand prototype, Func<DbCommand, DbCommand> cloneMethod)
		{
			Check.NotNull<DbCommand>(prototype, "prototype");
			Check.NotNull<Func<DbCommand, DbCommand>>(cloneMethod, "cloneMethod");
			this._prototype = prototype;
			this._cloneMethod = cloneMethod;
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x0010621F File Offset: 0x0010441F
		protected DbCommandDefinition()
		{
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x00106227 File Offset: 0x00104427
		public virtual DbCommand CreateCommand()
		{
			return this._cloneMethod(this._prototype);
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x0010623C File Offset: 0x0010443C
		internal static void PopulateParameterFromTypeUsage(DbParameter parameter, TypeUsage type, bool isOutParam)
		{
			parameter.IsNullable = TypeSemantics.IsNullable(type);
			DbType dbType;
			if (Helper.IsPrimitiveType(type.EdmType) && DbCommandDefinition.TryGetDbTypeFromPrimitiveType((PrimitiveType)type.EdmType, out dbType))
			{
				if (dbType <= DbType.Decimal)
				{
					if (dbType == DbType.Binary)
					{
						DbCommandDefinition.PopulateBinaryParameter(parameter, type, dbType, isOutParam);
						return;
					}
					if (dbType != DbType.DateTime)
					{
						if (dbType != DbType.Decimal)
						{
							goto IL_0075;
						}
						DbCommandDefinition.PopulateDecimalParameter(parameter, type, dbType);
						return;
					}
				}
				else
				{
					if (dbType == DbType.String)
					{
						DbCommandDefinition.PopulateStringParameter(parameter, type, isOutParam);
						return;
					}
					if (dbType != DbType.Time && dbType != DbType.DateTimeOffset)
					{
						goto IL_0075;
					}
				}
				DbCommandDefinition.PopulateDateTimeParameter(parameter, type, dbType);
				return;
				IL_0075:
				parameter.DbType = dbType;
			}
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x001062C8 File Offset: 0x001044C8
		internal static bool TryGetDbTypeFromPrimitiveType(PrimitiveType type, out DbType dbType)
		{
			switch (type.PrimitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				dbType = DbType.Binary;
				return true;
			case PrimitiveTypeKind.Boolean:
				dbType = DbType.Boolean;
				return true;
			case PrimitiveTypeKind.Byte:
				dbType = DbType.Byte;
				return true;
			case PrimitiveTypeKind.DateTime:
				dbType = DbType.DateTime;
				return true;
			case PrimitiveTypeKind.Decimal:
				dbType = DbType.Decimal;
				return true;
			case PrimitiveTypeKind.Double:
				dbType = DbType.Double;
				return true;
			case PrimitiveTypeKind.Guid:
				dbType = DbType.Guid;
				return true;
			case PrimitiveTypeKind.Single:
				dbType = DbType.Single;
				return true;
			case PrimitiveTypeKind.SByte:
				dbType = DbType.SByte;
				return true;
			case PrimitiveTypeKind.Int16:
				dbType = DbType.Int16;
				return true;
			case PrimitiveTypeKind.Int32:
				dbType = DbType.Int32;
				return true;
			case PrimitiveTypeKind.Int64:
				dbType = DbType.Int64;
				return true;
			case PrimitiveTypeKind.String:
				dbType = DbType.String;
				return true;
			case PrimitiveTypeKind.Time:
				dbType = DbType.Time;
				return true;
			case PrimitiveTypeKind.DateTimeOffset:
				dbType = DbType.DateTimeOffset;
				return true;
			default:
				dbType = DbType.AnsiString;
				return false;
			}
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x00106378 File Offset: 0x00104578
		private static void PopulateBinaryParameter(DbParameter parameter, TypeUsage type, DbType dbType, bool isOutParam)
		{
			parameter.DbType = dbType;
			DbCommandDefinition.SetParameterSize(parameter, type, isOutParam);
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x0010638C File Offset: 0x0010458C
		private static void PopulateDecimalParameter(DbParameter parameter, TypeUsage type, DbType dbType)
		{
			parameter.DbType = dbType;
			byte b;
			if (TypeHelpers.TryGetPrecision(type, out b))
			{
				((IDbDataParameter)parameter).Precision = b;
			}
			byte b2;
			if (TypeHelpers.TryGetScale(type, out b2))
			{
				((IDbDataParameter)parameter).Scale = b2;
			}
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x001063C4 File Offset: 0x001045C4
		private static void PopulateDateTimeParameter(DbParameter parameter, TypeUsage type, DbType dbType)
		{
			parameter.DbType = dbType;
			byte b;
			if (TypeHelpers.TryGetPrecision(type, out b))
			{
				((IDbDataParameter)parameter).Precision = b;
			}
		}

		// Token: 0x060049E6 RID: 18918 RVA: 0x001063EC File Offset: 0x001045EC
		private static void PopulateStringParameter(DbParameter parameter, TypeUsage type, bool isOutParam)
		{
			bool flag = true;
			bool flag2 = false;
			if (!TypeHelpers.TryGetIsFixedLength(type, out flag2))
			{
				flag2 = false;
			}
			if (!TypeHelpers.TryGetIsUnicode(type, out flag))
			{
				flag = true;
			}
			if (flag2)
			{
				parameter.DbType = (flag ? DbType.StringFixedLength : DbType.AnsiStringFixedLength);
			}
			else
			{
				parameter.DbType = (flag ? DbType.String : DbType.AnsiString);
			}
			DbCommandDefinition.SetParameterSize(parameter, type, isOutParam);
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x00106440 File Offset: 0x00104640
		private static void SetParameterSize(DbParameter parameter, TypeUsage type, bool isOutParam)
		{
			Facet facet;
			if (type.Facets.TryGetValue("MaxLength", true, out facet) && facet.Value != null)
			{
				if (!Helper.IsUnboundedFacetValue(facet))
				{
					parameter.Size = (int)facet.Value;
					return;
				}
				if (isOutParam)
				{
					parameter.Size = int.MaxValue;
				}
			}
		}

		// Token: 0x04001A0E RID: 6670
		private readonly DbCommand _prototype;

		// Token: 0x04001A0F RID: 6671
		private readonly Func<DbCommand, DbCommand> _cloneMethod;
	}
}
