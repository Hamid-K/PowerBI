using System;
using System.IO;
using Microsoft.Spatial;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x02000770 RID: 1904
	internal static class ODataLiteral
	{
		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x0600380C RID: 14348 RVA: 0x000B3A3E File Offset: 0x000B1C3E
		public static Microsoft.Spatial.WellKnownTextSqlFormatter Formatter
		{
			get
			{
				return ODataLiteral.spatialFormatter;
			}
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000B3A48 File Offset: 0x000B1C48
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

		// Token: 0x0600380E RID: 14350 RVA: 0x000B3A9C File Offset: 0x000B1C9C
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

		// Token: 0x0600380F RID: 14351 RVA: 0x000B3AF0 File Offset: 0x000B1CF0
		public static string ToWellKnownText(Microsoft.Spatial.Geography instance)
		{
			return ODataLiteral.Formatter.Write(instance);
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x000B3AF0 File Offset: 0x000B1CF0
		public static string ToWellKnownText(Microsoft.Spatial.Geometry instance)
		{
			return ODataLiteral.Formatter.Write(instance);
		}

		// Token: 0x04001D11 RID: 7441
		private static Microsoft.Spatial.WellKnownTextSqlFormatter spatialFormatter = Microsoft.Spatial.SpatialImplementation.CurrentImplementation.CreateWellKnownTextSqlFormatter(false);
	}
}
