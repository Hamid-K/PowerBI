using System;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001161 RID: 4449
	internal static class VersionHelper
	{
		// Token: 0x06007493 RID: 29843 RVA: 0x00190160 File Offset: 0x0018E360
		public static bool TryParse(string value, out Version version)
		{
			try
			{
				version = new Version(value);
				return true;
			}
			catch (OverflowException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (FormatException)
			{
			}
			version = null;
			return false;
		}
	}
}
