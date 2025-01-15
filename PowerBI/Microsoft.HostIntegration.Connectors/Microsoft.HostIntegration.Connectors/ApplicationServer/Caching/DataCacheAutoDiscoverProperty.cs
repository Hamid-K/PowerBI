using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000A0 RID: 160
	public class DataCacheAutoDiscoverProperty
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00012CEB File Offset: 0x00010EEB
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00012CF3 File Offset: 0x00010EF3
		internal bool IsEnabled { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00012CFC File Offset: 0x00010EFC
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x00012D04 File Offset: 0x00010F04
		internal string Identifier { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00012D0D File Offset: 0x00010F0D
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00012D15 File Offset: 0x00010F15
		internal IdentifierType IdentifierTypeSpecified { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00012D1E File Offset: 0x00010F1E
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00012D26 File Offset: 0x00010F26
		internal int StartPort { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00012D2F File Offset: 0x00010F2F
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00012D37 File Offset: 0x00010F37
		internal int DiscoveryPort { get; private set; }

		// Token: 0x060003AC RID: 940 RVA: 0x00012D40 File Offset: 0x00010F40
		public DataCacheAutoDiscoverProperty(bool enable)
			: this(enable, string.Empty)
		{
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00012D4E File Offset: 0x00010F4E
		public DataCacheAutoDiscoverProperty(bool enable, string identifier)
			: this(enable, IdentifierType.RoleName, identifier)
		{
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00012D59 File Offset: 0x00010F59
		internal DataCacheAutoDiscoverProperty(bool enable, IdentifierType identifiertype, string identifier)
			: this(enable, identifiertype, identifier, 24233, 22233)
		{
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00012D6E File Offset: 0x00010F6E
		internal DataCacheAutoDiscoverProperty(bool enable, IdentifierType identifierType, string identifier, int discoveryport, int startport)
		{
			this.IsEnabled = enable;
			this.Identifier = identifier;
			this.IdentifierTypeSpecified = identifierType;
			this.InitializePorts(discoveryport, startport);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00012D95 File Offset: 0x00010F95
		internal void UpdatePortsForSsl()
		{
			this.InitializePorts(25233, 23233);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00012DA8 File Offset: 0x00010FA8
		private void InitializePorts(int discoveryport, int startport)
		{
			if (discoveryport < 1 || discoveryport > 65535)
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "PortOutOfRange"));
			}
			this.DiscoveryPort = discoveryport;
			if (startport >= 1 && startport <= 65535)
			{
				this.StartPort = startport;
				return;
			}
			throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "PortOutOfRange"));
		}

		// Token: 0x040002D8 RID: 728
		private const int MinValidPortNumber = 1;

		// Token: 0x040002D9 RID: 729
		private const int MaxValidPortNumber = 65535;

		// Token: 0x040002DA RID: 730
		private const int DefaultDiscoveryPort = 24233;

		// Token: 0x040002DB RID: 731
		private const int DefaultStartPort = 22233;

		// Token: 0x040002DC RID: 732
		private const int DefaultSslDiscoveryPort = 25233;

		// Token: 0x040002DD RID: 733
		private const int DefaultSslStartPort = 23233;
	}
}
