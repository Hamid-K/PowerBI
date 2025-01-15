using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x02000291 RID: 657
	internal struct XmlTableOptions
	{
		// Token: 0x06001AAB RID: 6827 RVA: 0x00036058 File Offset: 0x00034258
		private XmlTableOptions(XmlTableOptions.OptionFlags optionFlags)
		{
			this.optionFlags = optionFlags;
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00036064 File Offset: 0x00034264
		public XmlTableOptions(Value options)
		{
			XmlTableOptions.OptionFlags optionFlags = (XmlTableOptions.OptionFlags)0;
			if (XmlTableOptions.DefaultTrue(options, "NavigationTables"))
			{
				optionFlags |= XmlTableOptions.OptionFlags.ConsiderNavShape;
			}
			this.optionFlags = optionFlags;
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0003608B File Offset: 0x0003428B
		private static bool DefaultTrue(Value options, string key)
		{
			return !options.IsRecord || !options.AsRecord.Keys.Contains(key) || options.AsRecord[key].AsBoolean;
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x000360BB File Offset: 0x000342BB
		public bool ConsiderNavShape
		{
			get
			{
				return (this.optionFlags & XmlTableOptions.OptionFlags.ConsiderNavShape) == XmlTableOptions.OptionFlags.ConsiderNavShape;
			}
		}

		// Token: 0x040007FA RID: 2042
		private XmlTableOptions.OptionFlags optionFlags;

		// Token: 0x02000292 RID: 658
		[Flags]
		private enum OptionFlags
		{
			// Token: 0x040007FC RID: 2044
			ConsiderNavShape = 1
		}
	}
}
