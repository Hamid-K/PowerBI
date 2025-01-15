using System;
using System.IO;
using Microsoft.Spatial;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000104 RID: 260
	internal static class LiteralUtils
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0002178D File Offset: 0x0001F98D
		private static WellKnownTextSqlFormatter Formatter
		{
			get
			{
				return SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter(false);
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002179C File Offset: 0x0001F99C
		internal static Geography ParseGeography(string text)
		{
			Geography geography;
			using (StringReader stringReader = new StringReader(text))
			{
				geography = LiteralUtils.Formatter.Read<Geography>(stringReader);
			}
			return geography;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x000217DC File Offset: 0x0001F9DC
		internal static Geometry ParseGeometry(string text)
		{
			Geometry geometry;
			using (StringReader stringReader = new StringReader(text))
			{
				geometry = LiteralUtils.Formatter.Read<Geometry>(stringReader);
			}
			return geometry;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002181C File Offset: 0x0001FA1C
		internal static string ToWellKnownText(Geography instance)
		{
			return LiteralUtils.Formatter.Write(instance);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002181C File Offset: 0x0001FA1C
		internal static string ToWellKnownText(Geometry instance)
		{
			return LiteralUtils.Formatter.Write(instance);
		}
	}
}
