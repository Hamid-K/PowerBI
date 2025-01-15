using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000748 RID: 1864
	[DataContract]
	[Serializable]
	public class TcpBaseRemoteEnvironment : BaseRemoteEnvironment
	{
		// Token: 0x06003B34 RID: 15156 RVA: 0x000BF5F5 File Offset: 0x000BD7F5
		protected TcpBaseRemoteEnvironment()
		{
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000C6F68 File Offset: 0x000C5168
		protected TcpBaseRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, string address, string ports)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault)
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

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06003B36 RID: 15158 RVA: 0x000C6FFC File Offset: 0x000C51FC
		// (set) Token: 0x06003B37 RID: 15159 RVA: 0x000C7004 File Offset: 0x000C5204
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

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06003B38 RID: 15160 RVA: 0x000C7020 File Offset: 0x000C5220
		// (set) Token: 0x06003B39 RID: 15161 RVA: 0x000C7028 File Offset: 0x000C5228
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

		// Token: 0x04002387 RID: 9095
		private string ipAddress;

		// Token: 0x04002388 RID: 9096
		private string tcpPorts;
	}
}
