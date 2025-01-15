using System;
using System.IO;
using System.Spatial;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000B0 RID: 176
	internal static class LiteralUtils
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000D448 File Offset: 0x0000B648
		private static WellKnownTextSqlFormatter Formatter
		{
			get
			{
				return SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter(false);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000D458 File Offset: 0x0000B658
		internal static Geography ParseGeography(string text)
		{
			Geography geography;
			using (StringReader stringReader = new StringReader(text))
			{
				geography = LiteralUtils.Formatter.Read<Geography>(stringReader);
			}
			return geography;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000D498 File Offset: 0x0000B698
		internal static Geometry ParseGeometry(string text)
		{
			Geometry geometry;
			using (StringReader stringReader = new StringReader(text))
			{
				geometry = LiteralUtils.Formatter.Read<Geometry>(stringReader);
			}
			return geometry;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
		internal static string ToWellKnownText(Geography instance)
		{
			return LiteralUtils.Formatter.Write(instance);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000D4E5 File Offset: 0x0000B6E5
		internal static string ToWellKnownText(Geometry instance)
		{
			return LiteralUtils.Formatter.Write(instance);
		}
	}
}
