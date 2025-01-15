using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;

namespace System.Data.Entity.Core.Common.Internal
{
	// Token: 0x02000630 RID: 1584
	internal static class DbTypeMap
	{
		// Token: 0x06004C41 RID: 19521 RVA: 0x0010C790 File Offset: 0x0010A990
		internal static bool TryGetModelTypeUsage(DbType dbType, out TypeUsage modelType)
		{
			switch (dbType)
			{
			case DbType.AnsiString:
				modelType = DbTypeMap.AnsiString;
				goto IL_0161;
			case DbType.Binary:
				modelType = DbTypeMap.Binary;
				goto IL_0161;
			case DbType.Byte:
				modelType = DbTypeMap.Byte;
				goto IL_0161;
			case DbType.Boolean:
				modelType = DbTypeMap.Boolean;
				goto IL_0161;
			case DbType.Currency:
				modelType = DbTypeMap.Currency;
				goto IL_0161;
			case DbType.Date:
				modelType = DbTypeMap.Date;
				goto IL_0161;
			case DbType.DateTime:
				modelType = DbTypeMap.DateTime;
				goto IL_0161;
			case DbType.Decimal:
				modelType = DbTypeMap.Decimal;
				goto IL_0161;
			case DbType.Double:
				modelType = DbTypeMap.Double;
				goto IL_0161;
			case DbType.Guid:
				modelType = DbTypeMap.Guid;
				goto IL_0161;
			case DbType.Int16:
				modelType = DbTypeMap.Int16;
				goto IL_0161;
			case DbType.Int32:
				modelType = DbTypeMap.Int32;
				goto IL_0161;
			case DbType.Int64:
				modelType = DbTypeMap.Int64;
				goto IL_0161;
			case DbType.SByte:
				modelType = DbTypeMap.SByte;
				goto IL_0161;
			case DbType.Single:
				modelType = DbTypeMap.Single;
				goto IL_0161;
			case DbType.String:
				modelType = DbTypeMap.String;
				goto IL_0161;
			case DbType.Time:
				modelType = DbTypeMap.Time;
				goto IL_0161;
			case DbType.VarNumeric:
				modelType = null;
				goto IL_0161;
			case DbType.AnsiStringFixedLength:
				modelType = DbTypeMap.AnsiStringFixedLength;
				goto IL_0161;
			case DbType.StringFixedLength:
				modelType = DbTypeMap.StringFixedLength;
				goto IL_0161;
			case DbType.Xml:
				modelType = DbTypeMap.Xml;
				goto IL_0161;
			case DbType.DateTime2:
				modelType = DbTypeMap.DateTime2;
				goto IL_0161;
			case DbType.DateTimeOffset:
				modelType = DbTypeMap.DateTimeOffset;
				goto IL_0161;
			}
			modelType = null;
			IL_0161:
			return modelType != null;
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x0010C903 File Offset: 0x0010AB03
		private static TypeUsage CreateType(PrimitiveTypeKind type)
		{
			return DbTypeMap.CreateType(type, new FacetValues());
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0010C910 File Offset: 0x0010AB10
		private static TypeUsage CreateType(PrimitiveTypeKind type, FacetValues facets)
		{
			return TypeUsage.Create(EdmProviderManifest.Instance.GetPrimitiveType(type), facets);
		}

		// Token: 0x04001AB5 RID: 6837
		internal static readonly TypeUsage AnsiString = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(false),
			FixedLength = new bool?(false),
			MaxLength = null
		});

		// Token: 0x04001AB6 RID: 6838
		internal static readonly TypeUsage AnsiStringFixedLength = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(false),
			FixedLength = new bool?(true),
			MaxLength = null
		});

		// Token: 0x04001AB7 RID: 6839
		internal static readonly TypeUsage String = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(true),
			FixedLength = new bool?(false),
			MaxLength = null
		});

		// Token: 0x04001AB8 RID: 6840
		internal static readonly TypeUsage StringFixedLength = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(true),
			FixedLength = new bool?(true),
			MaxLength = null
		});

		// Token: 0x04001AB9 RID: 6841
		internal static readonly TypeUsage Xml = DbTypeMap.CreateType(PrimitiveTypeKind.String, new FacetValues
		{
			Unicode = new bool?(true),
			FixedLength = new bool?(false),
			MaxLength = null
		});

		// Token: 0x04001ABA RID: 6842
		internal static readonly TypeUsage Binary = DbTypeMap.CreateType(PrimitiveTypeKind.Binary, new FacetValues
		{
			MaxLength = null
		});

		// Token: 0x04001ABB RID: 6843
		internal static readonly TypeUsage Boolean = DbTypeMap.CreateType(PrimitiveTypeKind.Boolean);

		// Token: 0x04001ABC RID: 6844
		internal static readonly TypeUsage Byte = DbTypeMap.CreateType(PrimitiveTypeKind.Byte);

		// Token: 0x04001ABD RID: 6845
		internal static readonly TypeUsage DateTime = DbTypeMap.CreateType(PrimitiveTypeKind.DateTime);

		// Token: 0x04001ABE RID: 6846
		internal static readonly TypeUsage Date = DbTypeMap.CreateType(PrimitiveTypeKind.DateTime);

		// Token: 0x04001ABF RID: 6847
		internal static readonly TypeUsage DateTime2 = DbTypeMap.CreateType(PrimitiveTypeKind.DateTime, new FacetValues
		{
			Precision = null
		});

		// Token: 0x04001AC0 RID: 6848
		internal static readonly TypeUsage Time = DbTypeMap.CreateType(PrimitiveTypeKind.Time, new FacetValues
		{
			Precision = null
		});

		// Token: 0x04001AC1 RID: 6849
		internal static readonly TypeUsage DateTimeOffset = DbTypeMap.CreateType(PrimitiveTypeKind.DateTimeOffset, new FacetValues
		{
			Precision = null
		});

		// Token: 0x04001AC2 RID: 6850
		internal static readonly TypeUsage Decimal = DbTypeMap.CreateType(PrimitiveTypeKind.Decimal, new FacetValues
		{
			Precision = null,
			Scale = null
		});

		// Token: 0x04001AC3 RID: 6851
		internal static readonly TypeUsage Currency = DbTypeMap.CreateType(PrimitiveTypeKind.Decimal, new FacetValues
		{
			Precision = null,
			Scale = null
		});

		// Token: 0x04001AC4 RID: 6852
		internal static readonly TypeUsage Double = DbTypeMap.CreateType(PrimitiveTypeKind.Double);

		// Token: 0x04001AC5 RID: 6853
		internal static readonly TypeUsage Guid = DbTypeMap.CreateType(PrimitiveTypeKind.Guid);

		// Token: 0x04001AC6 RID: 6854
		internal static readonly TypeUsage Int16 = DbTypeMap.CreateType(PrimitiveTypeKind.Int16);

		// Token: 0x04001AC7 RID: 6855
		internal static readonly TypeUsage Int32 = DbTypeMap.CreateType(PrimitiveTypeKind.Int32);

		// Token: 0x04001AC8 RID: 6856
		internal static readonly TypeUsage Int64 = DbTypeMap.CreateType(PrimitiveTypeKind.Int64);

		// Token: 0x04001AC9 RID: 6857
		internal static readonly TypeUsage Single = DbTypeMap.CreateType(PrimitiveTypeKind.Single);

		// Token: 0x04001ACA RID: 6858
		internal static readonly TypeUsage SByte = DbTypeMap.CreateType(PrimitiveTypeKind.SByte);
	}
}
