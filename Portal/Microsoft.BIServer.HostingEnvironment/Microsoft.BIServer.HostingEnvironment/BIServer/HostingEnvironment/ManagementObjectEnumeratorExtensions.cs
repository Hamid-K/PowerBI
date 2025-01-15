using System;
using System.Collections.Generic;
using System.Management;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000013 RID: 19
	public static class ManagementObjectEnumeratorExtensions
	{
		// Token: 0x06000083 RID: 131 RVA: 0x000037B4 File Offset: 0x000019B4
		public static ManagementObject SelectFirstBy(this ManagementObjectCollection.ManagementObjectEnumerator enumerator, string propertyName, Predicate<string> criteria)
		{
			if (enumerator == null)
			{
				return null;
			}
			ManagementBaseObject managementBaseObject = enumerator.Current;
			bool flag = true;
			while (flag)
			{
				string text;
				if (managementBaseObject == null)
				{
					text = null;
				}
				else
				{
					PropertyDataCollection properties = managementBaseObject.Properties;
					if (properties == null)
					{
						text = null;
					}
					else
					{
						PropertyData propertyData = properties[propertyName];
						if (propertyData == null)
						{
							text = null;
						}
						else
						{
							object value = propertyData.Value;
							text = ((value != null) ? value.ToString() : null);
						}
					}
				}
				if (criteria(text ?? ""))
				{
					break;
				}
				flag = enumerator.MoveNext();
				managementBaseObject = (ManagementObject)enumerator.Current;
			}
			return (ManagementObject)managementBaseObject;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000382F File Offset: 0x00001A2F
		public static IEnumerable<ManagementObject> ToIEnumerable(this ManagementObjectCollection.ManagementObjectEnumerator enumerator)
		{
			while (enumerator.MoveNext())
			{
				ManagementBaseObject managementBaseObject = enumerator.Current;
				yield return managementBaseObject as ManagementObject;
			}
			yield break;
		}
	}
}
