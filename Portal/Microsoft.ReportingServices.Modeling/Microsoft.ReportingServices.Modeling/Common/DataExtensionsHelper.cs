using System;
using System.Reflection;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200000F RID: 15
	internal sealed class DataExtensionsHelper
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002D2C File Offset: 0x00000F2C
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
