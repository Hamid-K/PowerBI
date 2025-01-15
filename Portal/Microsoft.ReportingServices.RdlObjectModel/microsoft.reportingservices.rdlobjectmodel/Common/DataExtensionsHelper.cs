using System;
using System.Reflection;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000066 RID: 102
	internal sealed class DataExtensionsHelper
	{
		// Token: 0x060003DC RID: 988 RVA: 0x00016354 File Offset: 0x00014554
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
