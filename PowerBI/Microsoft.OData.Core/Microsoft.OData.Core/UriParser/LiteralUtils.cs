using System;
using System.IO;
using Microsoft.Spatial;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000144 RID: 324
	internal static class LiteralUtils
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0002EDEF File Offset: 0x0002CFEF
		private static WellKnownTextSqlFormatter Formatter
		{
			get
			{
				return SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter(false);
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0002EDFC File Offset: 0x0002CFFC
		internal static Geography ParseGeography(string text)
		{
			Geography geography;
			using (StringReader stringReader = new StringReader(text))
			{
				geography = LiteralUtils.Formatter.Read<Geography>(stringReader);
			}
			return geography;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0002EE3C File Offset: 0x0002D03C
		internal static Geometry ParseGeometry(string text)
		{
			Geometry geometry;
			using (StringReader stringReader = new StringReader(text))
			{
				geometry = LiteralUtils.Formatter.Read<Geometry>(stringReader);
			}
			return geometry;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0002EE7C File Offset: 0x0002D07C
		internal static string ToWellKnownText(Geography instance)
		{
			return FormatterExtensions.Write(LiteralUtils.Formatter, instance);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0002EE7C File Offset: 0x0002D07C
		internal static string ToWellKnownText(Geometry instance)
		{
			return FormatterExtensions.Write(LiteralUtils.Formatter, instance);
		}
	}
}
