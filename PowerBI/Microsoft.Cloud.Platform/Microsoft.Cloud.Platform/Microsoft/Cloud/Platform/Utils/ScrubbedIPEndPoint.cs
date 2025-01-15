using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001B3 RID: 435
	[DataContract]
	[Serializable]
	public class ScrubbedIPEndPoint : IContainsPrivateInformation
	{
		// Token: 0x06000B37 RID: 2871 RVA: 0x00027310 File Offset: 0x00025510
		public ScrubbedIPEndPoint(IPEndPoint ip)
		{
			this.m_ip = ip;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002731F File Offset: 0x0002551F
		public ScrubbedIPEndPoint(IPEndPoint ip, bool scrub)
		{
			this.m_ip = ip;
			this.m_markAsPrivate = scrub;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00027335 File Offset: 0x00025535
		public ScrubbedIPEndPoint(IPAddress ipAddress)
		{
			this.m_ip = new IPEndPoint(ipAddress, 0);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002734A File Offset: 0x0002554A
		public ScrubbedIPEndPoint(IPAddress ipAddress, bool scrub)
			: this(ipAddress)
		{
			this.m_markAsPrivate = scrub;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002735A File Offset: 0x0002555A
		public override string ToString()
		{
			if (!this.m_markAsPrivate)
			{
				return this.m_ip.ToString();
			}
			return this.m_ip.ToString().MarkAsPrivate();
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00027380 File Offset: 0x00025580
		public string ToPrivateString()
		{
			byte[] addressBytes = this.m_ip.Address.GetAddressBytes();
			if (this.m_ip.AddressFamily == AddressFamily.InterNetwork)
			{
				return "{0}.{1}.{2}.***".FormatWithInvariantCulture(new object[]
				{
					addressBytes[0],
					addressBytes[1],
					addressBytes[2]
				});
			}
			string[] array = new string[addressBytes.Length];
			for (int i = 0; i < addressBytes.Length; i++)
			{
				array[i] = addressBytes[i].ToString("X2", CultureInfo.InvariantCulture);
			}
			return "{0}{1}:{2}{3}:{4}{5}:{6}{7}:{8}{9}:{10}{11}:{12}{13}:****".FormatWithInvariantCulture(new object[]
			{
				array[0],
				array[1],
				array[2],
				array[3],
				array[4],
				array[5],
				array[6],
				array[7],
				array[8],
				array[9],
				array[10],
				array[11],
				array[12],
				array[13]
			});
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002747D File Offset: 0x0002567D
		public string ToInternalString()
		{
			return this.ToOriginalString().MarkAsInternal();
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002748A File Offset: 0x0002568A
		public string ToOriginalString()
		{
			return this.m_ip.ToString();
		}

		// Token: 0x04000468 RID: 1128
		[DataMember]
		private IPEndPoint m_ip;

		// Token: 0x04000469 RID: 1129
		private readonly bool m_markAsPrivate;
	}
}
