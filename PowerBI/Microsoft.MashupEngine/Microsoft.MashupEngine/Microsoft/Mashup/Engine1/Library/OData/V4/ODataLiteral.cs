using System;
using System.IO;
using Microsoft.Spatial;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200086D RID: 2157
	internal static class ODataLiteral
	{
		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06003E2B RID: 15915 RVA: 0x000CB37E File Offset: 0x000C957E
		public static Microsoft.Spatial.WellKnownTextSqlFormatter Formatter
		{
			get
			{
				return ODataLiteral.spatialFormatter;
			}
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x000CB388 File Offset: 0x000C9588
		public static bool TryParseGeography(string text, out object geography)
		{
			bool flag;
			try
			{
				using (StringReader stringReader = new StringReader(text))
				{
					geography = ODataLiteral.Formatter.Read<Microsoft.Spatial.Geography>(stringReader);
					flag = true;
				}
			}
			catch (Microsoft.Spatial.ParseErrorException)
			{
				geography = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x000CB3DC File Offset: 0x000C95DC
		public static bool TryParseGeometry(string text, out object geometry)
		{
			bool flag;
			try
			{
				using (StringReader stringReader = new StringReader(text))
				{
					geometry = ODataLiteral.Formatter.Read<Microsoft.Spatial.Geometry>(stringReader);
					flag = true;
				}
			}
			catch (Microsoft.Spatial.ParseErrorException)
			{
				geometry = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x000CB430 File Offset: 0x000C9630
		public static string ToWellKnownText(Microsoft.Spatial.Geography instance)
		{
			return ODataLiteral.Formatter.Write(instance);
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x000CB430 File Offset: 0x000C9630
		public static string ToWellKnownText(Microsoft.Spatial.Geometry instance)
		{
			return ODataLiteral.Formatter.Write(instance);
		}

		// Token: 0x040020B6 RID: 8374
		private static Microsoft.Spatial.WellKnownTextSqlFormatter spatialFormatter = Microsoft.Spatial.SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter(false);
	}
}
