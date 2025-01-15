using System;
using System.Reflection;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200036C RID: 876
	internal sealed class DataExtensionsHelper
	{
		// Token: 0x06001CC8 RID: 7368 RVA: 0x00073F84 File Offset: 0x00072184
		internal static Type GetDataExtensionConnectionType(string extensionProvider, string getProviderConnectionTypeMethod)
		{
			Type type;
			try
			{
				type = (Type)Assembly.Load("Microsoft.ReportingServices.DataExtensions.dll").GetType(extensionProvider).InvokeMember(getProviderConnectionTypeMethod, BindingFlags.Static | BindingFlags.Public, null, null, null);
			}
			catch
			{
				type = null;
			}
			return type;
		}
	}
}
