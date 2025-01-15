using System;
using System.Linq;
using System.Management;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000014 RID: 20
	internal class OsInfoProvider
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003840 File Offset: 0x00001A40
		private ManagementObjectCollection.ManagementObjectEnumerator GetWmiObjectEnumerator(string wmiClassName)
		{
			ManagementClass managementClass = new ManagementClass(wmiClassName);
			ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = null;
			try
			{
				managementObjectEnumerator = managementClass.GetInstances().GetEnumerator();
				managementObjectEnumerator.MoveNext();
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Exception thrown while retrieving system info.", Array.Empty<object>());
			}
			return managementObjectEnumerator;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003890 File Offset: 0x00001A90
		private string GetWmiObjectPropertyOrEmpty(ManagementObject wmiObject, string propertyName)
		{
			string text;
			if (wmiObject == null)
			{
				text = null;
			}
			else
			{
				PropertyDataCollection properties = wmiObject.Properties;
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
			return text ?? "";
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000038CC File Offset: 0x00001ACC
		private ManagementObject GetCpuObject()
		{
			return this.GetWmiObjectEnumerator("Win32_Processor").SelectFirstBy("ProcessorType", (string type) => type == "3");
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003902 File Offset: 0x00001B02
		private ManagementObject GetOsObject()
		{
			return (ManagementObject)this.GetWmiObjectEnumerator("Win32_OperatingSystem").Current;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003919 File Offset: 0x00001B19
		public string GetOsName()
		{
			return this.GetWmiObjectPropertyOrEmpty(this.GetOsObject(), "Name").Split(new char[] { '|' }).FirstOrDefault<string>();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003941 File Offset: 0x00001B41
		public string GetOsVersion()
		{
			return this.GetWmiObjectPropertyOrEmpty(this.GetOsObject(), "Version");
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003954 File Offset: 0x00001B54
		public string GetVirtualizationEnabled()
		{
			return this.GetWmiObjectPropertyOrEmpty(this.GetCpuObject(), "VirtualizationFirmwareEnabled");
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003967 File Offset: 0x00001B67
		public string GetCpuArchitecture()
		{
			return this.GetWmiObjectPropertyOrEmpty(this.GetCpuObject(), "Caption").Split(new char[] { ' ' }).FirstOrDefault<string>();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003990 File Offset: 0x00001B90
		public string GetAppProductVersion(string productName)
		{
			Func<ManagementObject, bool> func = (ManagementObject mo) => this.GetWmiObjectPropertyOrEmpty(mo, "Name").Contains(productName);
			ManagementObject managementObject = this.GetWmiObjectEnumerator("Win32_Product").ToIEnumerable().AsParallel<ManagementObject>()
				.FirstOrDefault(func);
			return this.GetWmiObjectPropertyOrEmpty(managementObject, "Version");
		}
	}
}
