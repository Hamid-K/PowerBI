using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000119 RID: 281
	internal interface IHostConfiguration : IClientSideHostConfiguration, ICloneable
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060007B6 RID: 1974
		// (set) Token: 0x060007B7 RID: 1975
		int ServicePortInternal { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060007B8 RID: 1976
		// (set) Token: 0x060007B9 RID: 1977
		int ReplicationPort { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060007BA RID: 1978
		// (set) Token: 0x060007BB RID: 1979
		int ArbitrationPort { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060007BC RID: 1980
		// (set) Token: 0x060007BD RID: 1981
		int ClusterPort { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060007BE RID: 1982
		// (set) Token: 0x060007BF RID: 1983
		int RestPort { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060007C0 RID: 1984
		// (set) Token: 0x060007C1 RID: 1985
		int RestSslPort { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060007C2 RID: 1986
		// (set) Token: 0x060007C3 RID: 1987
		int CacheSocketPort { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060007C4 RID: 1988
		// (set) Token: 0x060007C5 RID: 1989
		int CacheDiscoveryPort { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060007C6 RID: 1990
		// (set) Token: 0x060007C7 RID: 1991
		int SslSocketPort { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060007C8 RID: 1992
		// (set) Token: 0x060007C9 RID: 1993
		int SslDiscoveryPort { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060007CA RID: 1994
		// (set) Token: 0x060007CB RID: 1995
		string ServiceName { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060007CC RID: 1996
		// (set) Token: 0x060007CD RID: 1997
		int EvictionInterval { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060007CE RID: 1998
		// (set) Token: 0x060007CF RID: 1999
		long HighWaterMarkPercentage { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060007D0 RID: 2000
		// (set) Token: 0x060007D1 RID: 2001
		bool IsQuorumHost { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060007D2 RID: 2002
		// (set) Token: 0x060007D3 RID: 2003
		long LowWaterMarkPercentage { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060007D4 RID: 2004
		// (set) Token: 0x060007D5 RID: 2005
		int NodeId { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060007D6 RID: 2006
		// (set) Token: 0x060007D7 RID: 2007
		string DisplayFriendlyNodeId { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060007D8 RID: 2008
		// (set) Token: 0x060007D9 RID: 2009
		long Size { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060007DA RID: 2010
		// (set) Token: 0x060007DB RID: 2011
		int CacheLineBuffer { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060007DC RID: 2012
		// (set) Token: 0x060007DD RID: 2013
		string Account { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060007DE RID: 2014
		string ServiceURIInternal { get; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060007DF RID: 2015
		IHostNodeDomainConfiguration[] DomainInformation { get; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060007E0 RID: 2016
		string ServiceSslUri { get; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060007E1 RID: 2017
		string RestServiceURI { get; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060007E2 RID: 2018
		string RestSslServiceURI { get; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060007E3 RID: 2019
		IDictionary<string, int> MemcacheSocketPorts { get; }
	}
}
