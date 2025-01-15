using System;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Data.SqlTypes;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000013 RID: 19
	internal sealed class SqlSpatialDataReader : DbSpatialDataReader
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00009084 File Offset: 0x00007284
		internal SqlSpatialDataReader(DbSpatialServices spatialServices, SqlDataReaderWrapper underlyingReader)
		{
			this._spatialServices = spatialServices;
			this._reader = underlyingReader;
			int fieldCount = this._reader.FieldCount;
			this._geographyColumns = new bool[fieldCount];
			this._geometryColumns = new bool[fieldCount];
			for (int i = 0; i < this._reader.FieldCount; i++)
			{
				string dataTypeName = this._reader.GetDataTypeName(i);
				if (dataTypeName.EndsWith("sys.geography", StringComparison.Ordinal))
				{
					this._geographyColumns[i] = true;
				}
				else if (dataTypeName.EndsWith("sys.geometry", StringComparison.Ordinal))
				{
					this._geometryColumns[i] = true;
				}
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000911C File Offset: 0x0000731C
		public override DbGeography GetGeography(int ordinal)
		{
			this.EnsureGeographyColumn(ordinal);
			SqlBytes sqlBytes = this._reader.GetSqlBytes(ordinal);
			object obj = SqlSpatialDataReader._sqlGeographyFromBinaryReader.Value(new BinaryReader(sqlBytes.Stream));
			return this._spatialServices.GeographyFromProviderValue(obj);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00009164 File Offset: 0x00007364
		public override DbGeometry GetGeometry(int ordinal)
		{
			this.EnsureGeometryColumn(ordinal);
			SqlBytes sqlBytes = this._reader.GetSqlBytes(ordinal);
			object obj = SqlSpatialDataReader._sqlGeometryFromBinaryReader.Value(new BinaryReader(sqlBytes.Stream));
			return this._spatialServices.GeometryFromProviderValue(obj);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000091AC File Offset: 0x000073AC
		public override bool IsGeographyColumn(int ordinal)
		{
			return this._geographyColumns[ordinal];
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000091B6 File Offset: 0x000073B6
		public override bool IsGeometryColumn(int ordinal)
		{
			return this._geometryColumns[ordinal];
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000091C0 File Offset: 0x000073C0
		private void EnsureGeographyColumn(int ordinal)
		{
			if (!this.IsGeographyColumn(ordinal))
			{
				throw new InvalidDataException(Strings.SqlProvider_InvalidGeographyColumn(this._reader.GetDataTypeName(ordinal)));
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000091E2 File Offset: 0x000073E2
		private void EnsureGeometryColumn(int ordinal)
		{
			if (!this.IsGeometryColumn(ordinal))
			{
				throw new InvalidDataException(Strings.SqlProvider_InvalidGeometryColumn(this._reader.GetDataTypeName(ordinal)));
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00009204 File Offset: 0x00007404
		private static Func<BinaryReader, object> CreateBinaryReadDelegate(Type spatialType)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(BinaryReader));
			ParameterExpression parameterExpression2 = Expression.Variable(spatialType);
			MethodInfo publicInstanceMethod = spatialType.GetPublicInstanceMethod("Read", new Type[] { typeof(BinaryReader) });
			return Expression.Lambda<Func<BinaryReader, object>>(Expression.Block(new ParameterExpression[] { parameterExpression2 }, new Expression[]
			{
				Expression.Assign(parameterExpression2, Expression.New(spatialType)),
				Expression.Call(parameterExpression2, publicInstanceMethod, new Expression[] { parameterExpression }),
				parameterExpression2
			}), new ParameterExpression[] { parameterExpression }).Compile();
		}

		// Token: 0x04000026 RID: 38
		private static readonly Lazy<Func<BinaryReader, object>> _sqlGeographyFromBinaryReader = new Lazy<Func<BinaryReader, object>>(() => SqlSpatialDataReader.CreateBinaryReadDelegate(SqlTypesAssemblyLoader.DefaultInstance.GetSqlTypesAssembly().SqlGeographyType), true);

		// Token: 0x04000027 RID: 39
		private static readonly Lazy<Func<BinaryReader, object>> _sqlGeometryFromBinaryReader = new Lazy<Func<BinaryReader, object>>(() => SqlSpatialDataReader.CreateBinaryReadDelegate(SqlTypesAssemblyLoader.DefaultInstance.GetSqlTypesAssembly().SqlGeometryType), true);

		// Token: 0x04000028 RID: 40
		private const string GeometrySqlType = "sys.geometry";

		// Token: 0x04000029 RID: 41
		private const string GeographySqlType = "sys.geography";

		// Token: 0x0400002A RID: 42
		private readonly DbSpatialServices _spatialServices;

		// Token: 0x0400002B RID: 43
		private readonly SqlDataReaderWrapper _reader;

		// Token: 0x0400002C RID: 44
		private readonly bool[] _geographyColumns;

		// Token: 0x0400002D RID: 45
		private readonly bool[] _geometryColumns;
	}
}
