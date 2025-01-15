using System;
using System.Reflection;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000077 RID: 119
	internal sealed class DataExtensionsHelper
	{
		// Token: 0x0600051C RID: 1308 RVA: 0x00015C70 File Offset: 0x00013E70
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
