using System;
using System.Xml;

namespace Microsoft.DataShaping.Common
{
	// Token: 0x0200001B RID: 27
	internal static class XmlUtils
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00003FCF File Offset: 0x000021CF
		internal static XmlReaderSettings ApplyDtdDosDefense(XmlReaderSettings settings)
		{
			settings.ProhibitDtd = true;
			settings.XmlResolver = null;
			return settings;
		}
	}
}
