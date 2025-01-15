using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000094 RID: 148
	public static class LsdlContentType
	{
		// Token: 0x0600029D RID: 669 RVA: 0x000061C6 File Offset: 0x000043C6
		public static bool IsXml(string contentType)
		{
			return string.Equals(contentType, "XML", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000061D4 File Offset: 0x000043D4
		public static bool IsJson(string contentType)
		{
			return string.Equals(contentType, "JSON", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000061E2 File Offset: 0x000043E2
		public static bool IsYaml(string contentType)
		{
			return string.Equals(contentType, "YAML", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0400032D RID: 813
		public const string Xml = "XML";

		// Token: 0x0400032E RID: 814
		public const string Json = "JSON";

		// Token: 0x0400032F RID: 815
		public const string Yaml = "YAML";
	}
}
