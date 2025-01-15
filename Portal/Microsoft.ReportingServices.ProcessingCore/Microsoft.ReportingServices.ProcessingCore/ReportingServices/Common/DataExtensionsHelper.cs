using System;
using System.Reflection;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005DB RID: 1499
	internal sealed class DataExtensionsHelper
	{
		// Token: 0x060053ED RID: 21485 RVA: 0x00161724 File Offset: 0x0015F924
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
