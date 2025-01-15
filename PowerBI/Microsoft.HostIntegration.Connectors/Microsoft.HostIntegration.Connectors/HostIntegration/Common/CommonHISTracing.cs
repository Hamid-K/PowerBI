using System;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020004F2 RID: 1266
	public class CommonHISTracing : ICommonHISTracing
	{
		// Token: 0x06002AD8 RID: 10968 RVA: 0x000940A3 File Offset: 0x000922A3
		public CommonHISTracing(long TracingOptions, InternalCommonHISTracing IntHISTracing)
		{
			this.tracingOptions = TracingOptions;
			this.intHISTracing = IntHISTracing;
			this.tracingOptions |= this.intHISTracing.Refresh();
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000940D1 File Offset: 0x000922D1
		public void HISTraceEntry(string traceData)
		{
			this.intHISTracing.HISTraceEntry(traceData);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000940DF File Offset: 0x000922DF
		public void CloseTraceFile()
		{
			this.intHISTracing.CloseTraceFile();
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x000940EC File Offset: 0x000922EC
		// (set) Token: 0x06002ADC RID: 10972 RVA: 0x000940F4 File Offset: 0x000922F4
		public long TracingOptions
		{
			get
			{
				return this.tracingOptions;
			}
			set
			{
				this.tracingOptions = value;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x000940FD File Offset: 0x000922FD
		// (set) Token: 0x06002ADE RID: 10974 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool GeneralStartupShutdown
		{
			get
			{
				return (1L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002ADF RID: 10975 RVA: 0x0009410D File Offset: 0x0009230D
		// (set) Token: 0x06002AE0 RID: 10976 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool GeneralBufferManagement
		{
			get
			{
				return (2L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x0009411D File Offset: 0x0009231D
		// (set) Token: 0x06002AE2 RID: 10978 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool GeneralEventLogging
		{
			get
			{
				return (4L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x0009412D File Offset: 0x0009232D
		// (set) Token: 0x06002AE4 RID: 10980 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool GeneralInternalMethodEntryExit
		{
			get
			{
				return (8L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x0009413D File Offset: 0x0009233D
		// (set) Token: 0x06002AE6 RID: 10982 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool TransportStates
		{
			get
			{
				return (256L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x00094151 File Offset: 0x00092351
		// (set) Token: 0x06002AE8 RID: 10984 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool TransportSIDCache
		{
			get
			{
				return (512L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x00094165 File Offset: 0x00092365
		// (set) Token: 0x06002AEA RID: 10986 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool TransportDPLHeaders
		{
			get
			{
				return (1024L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x00094179 File Offset: 0x00092379
		// (set) Token: 0x06002AEC RID: 10988 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool TransportTCPHeaders
		{
			get
			{
				return (2048L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x0009418D File Offset: 0x0009238D
		// (set) Token: 0x06002AEE RID: 10990 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool TransportDetails
		{
			get
			{
				return (4096L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x000941A1 File Offset: 0x000923A1
		// (set) Token: 0x06002AF0 RID: 10992 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool TransportEntryExit
		{
			get
			{
				return (8192L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x000941B5 File Offset: 0x000923B5
		// (set) Token: 0x06002AF2 RID: 10994 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool AggregateEntryExit
		{
			get
			{
				return (65536L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x000941C9 File Offset: 0x000923C9
		// (set) Token: 0x06002AF4 RID: 10996 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool PrimitiveEntryExit
		{
			get
			{
				return (131072L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x000941DD File Offset: 0x000923DD
		// (set) Token: 0x06002AF6 RID: 10998 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool AggregateDetails
		{
			get
			{
				return (262144L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x000941F1 File Offset: 0x000923F1
		// (set) Token: 0x06002AF8 RID: 11000 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool AggregateExtendedError
		{
			get
			{
				return (524288L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002AF9 RID: 11001 RVA: 0x00094205 File Offset: 0x00092405
		// (set) Token: 0x06002AFA RID: 11002 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool LibraryReaderDetails
		{
			get
			{
				return (2097152L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x00094219 File Offset: 0x00092419
		// (set) Token: 0x06002AFC RID: 11004 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool LibraryReaderEntryExit
		{
			get
			{
				return (4194304L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x0009422D File Offset: 0x0009242D
		// (set) Token: 0x06002AFE RID: 11006 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool ProxyObjectCreateDestroy
		{
			get
			{
				return (16777216L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x00094241 File Offset: 0x00092441
		// (set) Token: 0x06002B00 RID: 11008 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool ProxyUserMethodInvocationEntryExit
		{
			get
			{
				return (67108864L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x00094255 File Offset: 0x00092455
		// (set) Token: 0x06002B02 RID: 11010 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool ProxyStateMachineVerbose
		{
			get
			{
				return (268435456L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002B03 RID: 11011 RVA: 0x00094269 File Offset: 0x00092469
		// (set) Token: 0x06002B04 RID: 11012 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool ProxyStateMachineTerse
		{
			get
			{
				return (536870912L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06002B05 RID: 11013 RVA: 0x0009427D File Offset: 0x0009247D
		// (set) Token: 0x06002B06 RID: 11014 RVA: 0x000036A9 File Offset: 0x000018A9
		public bool Proxy2PCWork
		{
			get
			{
				return (1073741824L & this.tracingOptions) != 0L;
			}
			set
			{
			}
		}

		// Token: 0x04001A06 RID: 6662
		private const long generalStartupShutdown = 1L;

		// Token: 0x04001A07 RID: 6663
		private const long generalBufferManagement = 2L;

		// Token: 0x04001A08 RID: 6664
		private const long generalEventLogging = 4L;

		// Token: 0x04001A09 RID: 6665
		private const long generalInternalMethodEntryExit = 8L;

		// Token: 0x04001A0A RID: 6666
		private const long transportStates = 256L;

		// Token: 0x04001A0B RID: 6667
		private const long transportSIDCache = 512L;

		// Token: 0x04001A0C RID: 6668
		private const long transportDPLHeaders = 1024L;

		// Token: 0x04001A0D RID: 6669
		private const long transportTCPHeaders = 2048L;

		// Token: 0x04001A0E RID: 6670
		private const long transportDetails = 4096L;

		// Token: 0x04001A0F RID: 6671
		private const long transportEntryExit = 8192L;

		// Token: 0x04001A10 RID: 6672
		private const long aggregateEntryExit = 65536L;

		// Token: 0x04001A11 RID: 6673
		private const long primitiveEntryExit = 131072L;

		// Token: 0x04001A12 RID: 6674
		private const long aggregateDetails = 262144L;

		// Token: 0x04001A13 RID: 6675
		private const long aggregateExtendedError = 524288L;

		// Token: 0x04001A14 RID: 6676
		private const long libraryReaderDetails = 2097152L;

		// Token: 0x04001A15 RID: 6677
		private const long libraryReaderEntryExit = 4194304L;

		// Token: 0x04001A16 RID: 6678
		private const long proxyObjectCreateDestroy = 16777216L;

		// Token: 0x04001A17 RID: 6679
		private const long proxyUserMethodInvocationEntryExit = 67108864L;

		// Token: 0x04001A18 RID: 6680
		private const long proxyStateMachineVerbose = 268435456L;

		// Token: 0x04001A19 RID: 6681
		private const long proxyStateMachineTerse = 536870912L;

		// Token: 0x04001A1A RID: 6682
		private const long proxy2PCWork = 1073741824L;

		// Token: 0x04001A1B RID: 6683
		private long tracingOptions;

		// Token: 0x04001A1C RID: 6684
		private InternalCommonHISTracing intHISTracing;
	}
}
