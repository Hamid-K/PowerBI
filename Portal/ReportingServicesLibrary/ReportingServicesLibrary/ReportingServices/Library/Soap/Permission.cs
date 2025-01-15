using System;
using System.Collections.Specialized;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200034F RID: 847
	public class Permission
	{
		// Token: 0x06001C0D RID: 7181 RVA: 0x00071EA8 File Offset: 0x000700A8
		internal static string[] StringCollectionToThisArray(StringCollection permissions)
		{
			if (permissions == null)
			{
				return null;
			}
			string[] array = new string[permissions.Count];
			for (int i = 0; i < permissions.Count; i++)
			{
				array[i] = permissions[i];
			}
			return array;
		}
	}
}
