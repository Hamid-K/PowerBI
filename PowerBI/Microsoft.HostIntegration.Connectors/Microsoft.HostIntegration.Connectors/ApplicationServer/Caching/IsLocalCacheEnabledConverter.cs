using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000F7 RID: 247
	internal sealed class IsLocalCacheEnabledConverter : ConfigurationConverterBase
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x0001ABE0 File Offset: 0x00018DE0
		public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
		{
			data = data ?? string.Empty;
			string text = data as string;
			bool flag;
			if (!bool.TryParse(text, out flag))
			{
				ConfigFile.ThrowException(8002, "localCache", text, false.ToString(), true.ToString());
			}
			return flag;
		}
	}
}
