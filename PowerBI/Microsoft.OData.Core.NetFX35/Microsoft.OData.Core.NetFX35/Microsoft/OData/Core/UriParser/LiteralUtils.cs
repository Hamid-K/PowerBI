using System;
using System.IO;
using Microsoft.Spatial;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001E8 RID: 488
	internal static class LiteralUtils
	{
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x00040144 File Offset: 0x0003E344
		private static WellKnownTextSqlFormatter Formatter
		{
			get
			{
				return SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter(false);
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00040154 File Offset: 0x0003E354
		internal static Geography ParseGeography(string text)
		{
			Geography geography;
			using (StringReader stringReader = new StringReader(text))
			{
				geography = LiteralUtils.Formatter.Read<Geography>(stringReader);
			}
			return geography;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00040194 File Offset: 0x0003E394
		internal static Geometry ParseGeometry(string text)
		{
			Geometry geometry;
			using (StringReader stringReader = new StringReader(text))
			{
				geometry = LiteralUtils.Formatter.Read<Geometry>(stringReader);
			}
			return geometry;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x000401D4 File Offset: 0x0003E3D4
		internal static string ToWellKnownText(Geography instance)
		{
			return LiteralUtils.Formatter.Write(instance);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000401E1 File Offset: 0x0003E3E1
		internal static string ToWellKnownText(Geometry instance)
		{
			return LiteralUtils.Formatter.Write(instance);
		}
	}
}
