using System;
using System.IO;
using System.Spatial;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000028 RID: 40
	internal static class LiteralUtils
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000492F File Offset: 0x00002B2F
		private static WellKnownTextSqlFormatter Formatter
		{
			get
			{
				return SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter();
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000493C File Offset: 0x00002B3C
		internal static Geography ParseGeography(string text)
		{
			Geography geography;
			using (StringReader stringReader = new StringReader(text))
			{
				geography = LiteralUtils.Formatter.Read<Geography>(stringReader);
			}
			return geography;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000497C File Offset: 0x00002B7C
		internal static Geometry ParseGeometry(string text)
		{
			Geometry geometry;
			using (StringReader stringReader = new StringReader(text))
			{
				geometry = LiteralUtils.Formatter.Read<Geometry>(stringReader);
			}
			return geometry;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000049BC File Offset: 0x00002BBC
		internal static string ToWellKnownText(Geography instance)
		{
			return LiteralUtils.Formatter.Write(instance);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000049C9 File Offset: 0x00002BC9
		internal static string ToWellKnownText(Geometry instance)
		{
			return LiteralUtils.Formatter.Write(instance);
		}
	}
}
