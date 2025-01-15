using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200017B RID: 379
	internal static class ApplicationSwitchesProviderUtil
	{
		// Token: 0x060009E4 RID: 2532 RVA: 0x00022420 File Offset: 0x00020620
		public static bool GetBoolSwitch(IApplicationSwitchesProvider provider, string name, out bool specified)
		{
			string text = provider[name];
			if (text == null)
			{
				specified = false;
				return false;
			}
			bool flag;
			if (bool.TryParse(text, out flag))
			{
				specified = true;
				return flag;
			}
			string text2 = "Switch '{0}' was registered as a boolean switch and must have a boolean format (true/false)".FormatWithInvariantCulture(new object[] { name });
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, text2);
			throw new SyntaxErrorException(text2);
		}
	}
}
