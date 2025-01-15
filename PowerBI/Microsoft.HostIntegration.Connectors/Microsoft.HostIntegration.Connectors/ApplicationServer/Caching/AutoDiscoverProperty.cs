using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000D6 RID: 214
	internal class AutoDiscoverProperty : ConfigurationElement
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00015FAF File Offset: 0x000141AF
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x00015FC1 File Offset: 0x000141C1
		[ConfigurationProperty("isEnabled", DefaultValue = false, IsRequired = false)]
		public bool IsEnabled
		{
			get
			{
				return (bool)base["isEnabled"];
			}
			set
			{
				base["isEnabled"] = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00017F04 File Offset: 0x00016104
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x00017F16 File Offset: 0x00016116
		[ConfigurationProperty("identifier", IsRequired = false)]
		public string Identifier
		{
			get
			{
				return (string)base["identifier"];
			}
			set
			{
				base["identifier"] = value;
			}
		}

		// Token: 0x040003D0 RID: 976
		internal const string IS_ENABLED = "isEnabled";

		// Token: 0x040003D1 RID: 977
		internal const string IDENTIFIER = "identifier";
	}
}
