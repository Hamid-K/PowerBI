using System;
using System.Data.Entity.Core;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000004 RID: 4
	internal class DbGeometryAdapter : IDbSpatialValue
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002278 File Offset: 0x00000478
		internal DbGeometryAdapter(DbGeometry value)
		{
			this._value = value;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002287 File Offset: 0x00000487
		public bool IsGeography
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000228A File Offset: 0x0000048A
		public object ProviderValue
		{
			get
			{
				return (() => this._value.ProviderValue).NullIfNotImplemented<object>();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000229D File Offset: 0x0000049D
		public int? CoordinateSystemId
		{
			get
			{
				return (() => new int?(this._value.CoordinateSystemId)).NullIfNotImplemented<int?>();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022B0 File Offset: 0x000004B0
		public string WellKnownText
		{
			get
			{
				return (() => this._value.Provider.AsTextIncludingElevationAndMeasure(this._value)).NullIfNotImplemented<string>() ?? (() => this._value.AsText()).NullIfNotImplemented<string>();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022D8 File Offset: 0x000004D8
		public byte[] WellKnownBinary
		{
			get
			{
				return (() => this._value.AsBinary()).NullIfNotImplemented<byte[]>();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022EB File Offset: 0x000004EB
		public string GmlString
		{
			get
			{
				return (() => this._value.AsGml()).NullIfNotImplemented<string>();
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022FE File Offset: 0x000004FE
		public Exception NotSqlCompatible()
		{
			return new ProviderIncompatibleException(Strings.SqlProvider_GeometryValueNotSqlCompatible);
		}

		// Token: 0x04000004 RID: 4
		private readonly DbGeometry _value;
	}
}
