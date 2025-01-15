using System;
using System.Globalization;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BDC RID: 7132
	public static class CultureExtensions
	{
		// Token: 0x0600B215 RID: 45589 RVA: 0x0024514C File Offset: 0x0024334C
		public static CultureInfo GetCulture(string name)
		{
			name = name.Replace('\0', '\ufffd');
			return new CultureInfo(name, false);
		}
	}
}
