using System;
using System.Collections.Generic;
using System.Management;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.BIServer.Configuration.WMI
{
	// Token: 0x02000030 RID: 48
	internal class RSWmiInstance
	{
		// Token: 0x0600019F RID: 415 RVA: 0x00006754 File Offset: 0x00004954
		public RSWmiInstance(ManagementObject mo, string machineName)
		{
			this.machineName = machineName;
			this._internalObject = mo;
			this._internalObject.Options.Timeout = new TimeSpan(0, 0, 10);
			try
			{
				this._internalObject.Get();
			}
			catch (Exception)
			{
				Logger.Warning("RS WMI Instance {0} is not available on machine {1}", new object[] { mo.Path, machineName });
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000067CC File Offset: 0x000049CC
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000067D4 File Offset: 0x000049D4
		public string Name
		{
			get
			{
				return (string)this._internalObject["InstanceName"];
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000067EB File Offset: 0x000049EB
		public string ID
		{
			get
			{
				return (string)this._internalObject["InstanceID"];
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006804 File Offset: 0x00004A04
		public bool IsSharePointIntegrated
		{
			get
			{
				bool flag = false;
				try
				{
					flag = (bool)this._internalObject["IsSharePointIntegrated"];
				}
				catch (ManagementException ex)
				{
					if (ex.ErrorCode != ManagementStatus.NotFound)
					{
						throw;
					}
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006850 File Offset: 0x00004A50
		public uint EditionID
		{
			get
			{
				return (uint)this._internalObject["EditionID"];
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00006867 File Offset: 0x00004A67
		public string EditionName
		{
			get
			{
				return (string)this._internalObject["EditionName"];
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000687E File Offset: 0x00004A7E
		public string Version
		{
			get
			{
				return (string)this._internalObject["Version"];
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00006898 File Offset: 0x00004A98
		public string[] ReportServerUrls
		{
			get
			{
				InvokeMethodOptions invokeMethodOptions = new InvokeMethodOptions();
				invokeMethodOptions.Timeout = new TimeSpan(0, 0, 10);
				ManagementBaseObject methodParameters = this._internalObject.GetMethodParameters("GetReportServerUrls");
				ManagementBaseObject managementBaseObject = this._internalObject.InvokeMethod("GetReportServerUrls", methodParameters, invokeMethodOptions);
				if ((int)managementBaseObject["HRESULT"] == 0)
				{
					string[] array = (string[])managementBaseObject["ApplicationName"];
					string[] array2 = (string[])managementBaseObject["URLs"];
					List<string> list = new List<string>();
					for (int i = 0; i < array2.Length; i++)
					{
						if ("ReportServerWebService".Equals(array[i], StringComparison.OrdinalIgnoreCase))
						{
							list.Add(array2[i]);
						}
					}
					return list.ToArray();
				}
				return null;
			}
		}

		// Token: 0x04000136 RID: 310
		private const string PropertyName_InstanceName = "InstanceName";

		// Token: 0x04000137 RID: 311
		private const string PropertyName_InstanceID = "InstanceID";

		// Token: 0x04000138 RID: 312
		private const string PropertyName_EditionID = "EditionID";

		// Token: 0x04000139 RID: 313
		private const string PropertyName_EditionName = "EditionName";

		// Token: 0x0400013A RID: 314
		private const string PropertyName_IsSharePointIntegrated = "IsSharePointIntegrated";

		// Token: 0x0400013B RID: 315
		private const string PropertyName_Version = "Version";

		// Token: 0x0400013C RID: 316
		private const string MethodName_GetReportServerUrls = "GetReportServerUrls";

		// Token: 0x0400013D RID: 317
		protected ManagementObject _internalObject;

		// Token: 0x0400013E RID: 318
		private string machineName;
	}
}
