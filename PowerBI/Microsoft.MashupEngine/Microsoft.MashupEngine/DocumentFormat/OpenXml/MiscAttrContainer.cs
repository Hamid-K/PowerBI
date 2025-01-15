using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200210B RID: 8459
	internal class MiscAttrContainer
	{
		// Token: 0x17003277 RID: 12919
		// (get) Token: 0x0600D0EB RID: 53483 RVA: 0x00299B8D File Offset: 0x00297D8D
		// (set) Token: 0x0600D0EC RID: 53484 RVA: 0x00299B95 File Offset: 0x00297D95
		internal List<OpenXmlAttribute> ExtendedAttributesField { get; set; }

		// Token: 0x17003278 RID: 12920
		// (get) Token: 0x0600D0ED RID: 53485 RVA: 0x00299B9E File Offset: 0x00297D9E
		// (set) Token: 0x0600D0EE RID: 53486 RVA: 0x00299BA6 File Offset: 0x00297DA6
		internal MarkupCompatibilityAttributes _mcAttributes { get; set; }

		// Token: 0x17003279 RID: 12921
		// (get) Token: 0x0600D0EF RID: 53487 RVA: 0x00299BAF File Offset: 0x00297DAF
		// (set) Token: 0x0600D0F0 RID: 53488 RVA: 0x00299BB7 File Offset: 0x00297DB7
		internal List<KeyValuePair<string, string>> _nsMappings { get; set; }
	}
}
