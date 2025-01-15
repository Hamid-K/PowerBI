using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000079 RID: 121
	internal class SdkVersionUtils
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x00011870 File Offset: 0x0000FA70
		internal static string GetSdkVersion(string versionPrefix)
		{
			string version = typeof(TelemetryClient).Assembly.GetCustomAttributes(false).OfType<AssemblyFileVersionAttribute>().First<AssemblyFileVersionAttribute>()
				.Version;
			Version version2;
			if (string.IsNullOrEmpty(version) || !Version.TryParse(version, out version2))
			{
				return null;
			}
			string text = version2.Revision.ToString(CultureInfo.InvariantCulture);
			return versionPrefix + version2.ToString(3) + "-" + text;
		}
	}
}
