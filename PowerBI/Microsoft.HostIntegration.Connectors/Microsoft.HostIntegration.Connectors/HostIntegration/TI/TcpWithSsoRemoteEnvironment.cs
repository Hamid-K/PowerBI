using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000749 RID: 1865
	[DataContract]
	[Serializable]
	public class TcpWithSsoRemoteEnvironment : BaseWithSsoRemoteEnvironment
	{
		// Token: 0x06003B3A RID: 15162 RVA: 0x000C5B04 File Offset: 0x000C3D04
		protected TcpWithSsoRemoteEnvironment()
		{
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x000C70AC File Offset: 0x000C52AC
		protected TcpWithSsoRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication)
		{
			this.ipAddress = address;
			if (!string.IsNullOrEmpty(ports))
			{
				ports = ports.Trim(new char[] { '"' });
				string[] array = ports.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					try
					{
						int.Parse(array[i]);
					}
					catch
					{
						throw new Exception("Wrong format of ports. It needs to be an integer or integers separated by \";\".");
					}
				}
			}
			this.tcpPorts = ports;
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06003B3C RID: 15164 RVA: 0x000C7144 File Offset: 0x000C5344
		// (set) Token: 0x06003B3D RID: 15165 RVA: 0x000C714C File Offset: 0x000C534C
		[DataMember]
		[Description("IP Address of the host")]
		[DisplayName("IpAddress *")]
		[Category("TCP/IP")]
		public string IpAddress
		{
			get
			{
				return this.ipAddress;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("IpAddress");
				}
				this.ipAddress = value;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06003B3E RID: 15166 RVA: 0x000C7168 File Offset: 0x000C5368
		// (set) Token: 0x06003B3F RID: 15167 RVA: 0x000C7170 File Offset: 0x000C5370
		[DataMember]
		[Description("TCP port(s) of the host to connect in the format of integers separated by \";\", e.g. 7511 or 7511;7512.")]
		[DisplayName("TCPPorts *")]
		[Category("TCP/IP")]
		public string Ports
		{
			get
			{
				return this.tcpPorts;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("Ports");
				}
				value = value.Trim(new char[] { '"' });
				string[] array = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					try
					{
						int.Parse(array[i]);
					}
					catch
					{
						throw new Exception("Wrong format of ports. It needs to be an integer or integers separated by \";\".");
					}
				}
				this.tcpPorts = value;
			}
		}

		// Token: 0x04002389 RID: 9097
		private string ipAddress;

		// Token: 0x0400238A RID: 9098
		private string tcpPorts;
	}
}
