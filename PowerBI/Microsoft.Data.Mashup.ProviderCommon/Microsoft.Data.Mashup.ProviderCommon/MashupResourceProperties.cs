using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Storage;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000015 RID: 21
	internal sealed class MashupResourceProperties
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x0000532C File Offset: 0x0000352C
		public MashupResourceProperties(IPackage package, bool partitionValues, byte[] filterDataTable, bool allowAutomaticCredentials, bool allowWindowsAuthentication, bool allowNativeQueries, bool fastCombine, long maxCacheSize, bool memoryCache, string cachePath, long maxTempSize, string tempPath, string cacheEncryptionCertificateThumbprint, string session, Guid? activityId, string correlationId, bool legacyRedirects, bool throwFoldingFailures, bool throwOnVolatileFunctions, bool ignorePreviouslyCachedData, SafeHandle threadIdentity, string uiCulture, DateTimeOffset? now, IDictionary<string, object> configurationProperties, string[] tracingOptions, CredentialsStorage credentialsStorage, QueryPermissionsStorage queryPermissionsStorage, FirewallStorage firewallStorage, IFirewallPlan firewallPlan, string dataSourcePool, bool debug, string cacheGroup, string metadataCache, Action<Func<IEnumerable<PartitionProgress>>> partitionProgressUpdater = null, IContainerEvaluationMonitorService containerEvaluationMonitorService = null)
		{
			this.Package = package;
			this.PartitionValues = partitionValues;
			this.FilterDataTable = filterDataTable;
			this.AllowAutomaticCredentials = allowAutomaticCredentials;
			this.AllowWindowsAuthentication = allowWindowsAuthentication;
			this.AllowNativeQueries = allowNativeQueries;
			this.FastCombine = fastCombine;
			this.MaxCacheSize = maxCacheSize;
			this.MemoryCache = memoryCache;
			this.CachePath = cachePath;
			this.MaxTempSize = maxTempSize;
			this.TempPath = tempPath;
			this.CacheEncryptionCertificateThumbprint = cacheEncryptionCertificateThumbprint;
			this.Session = session;
			this.ActivityId = activityId;
			this.CorrelationId = correlationId;
			this.LegacyRedirects = legacyRedirects;
			this.ThrowFoldingFailures = throwFoldingFailures;
			this.ThrowOnVolatileFunctions = throwOnVolatileFunctions;
			this.IgnorePreviouslyCachedData = ignorePreviouslyCachedData;
			this.ThreadIdentity = threadIdentity;
			this.UiCulture = uiCulture;
			this.Now = now;
			this.ConfigurationProperties = configurationProperties;
			this.TracingOptions = tracingOptions;
			this.CredentialsStorage = credentialsStorage;
			this.QueryPermissionsStorage = queryPermissionsStorage;
			this.FirewallStorage = firewallStorage;
			this.FirewallPlan = firewallPlan;
			this.DataSourcePool = dataSourcePool;
			this.Debug = debug;
			this.CacheGroup = cacheGroup;
			this.MetadataCache = metadataCache;
			this.PartitionProgressUpdater = partitionProgressUpdater;
			this.ContainerEvaluationMonitorService = containerEvaluationMonitorService;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005454 File Offset: 0x00003654
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x0000545C File Offset: 0x0000365C
		public IPackage Package { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00005465 File Offset: 0x00003665
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x0000546D File Offset: 0x0000366D
		public bool PartitionValues { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00005476 File Offset: 0x00003676
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000547E File Offset: 0x0000367E
		public byte[] FilterDataTable { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00005487 File Offset: 0x00003687
		// (set) Token: 0x060000AB RID: 171 RVA: 0x0000548F File Offset: 0x0000368F
		public bool AllowAutomaticCredentials { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00005498 File Offset: 0x00003698
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000054A0 File Offset: 0x000036A0
		public bool AllowWindowsAuthentication { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000054A9 File Offset: 0x000036A9
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000054B1 File Offset: 0x000036B1
		public bool AllowNativeQueries { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000054BA File Offset: 0x000036BA
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000054C2 File Offset: 0x000036C2
		public bool FastCombine { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000054CB File Offset: 0x000036CB
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000054D3 File Offset: 0x000036D3
		public long MaxCacheSize { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000054DC File Offset: 0x000036DC
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000054E4 File Offset: 0x000036E4
		public bool MemoryCache { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000054ED File Offset: 0x000036ED
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000054F5 File Offset: 0x000036F5
		public string CachePath { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000054FE File Offset: 0x000036FE
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00005506 File Offset: 0x00003706
		public long MaxTempSize { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000550F File Offset: 0x0000370F
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00005517 File Offset: 0x00003717
		public string TempPath { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00005520 File Offset: 0x00003720
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00005528 File Offset: 0x00003728
		public string CacheEncryptionCertificateThumbprint { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00005531 File Offset: 0x00003731
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00005539 File Offset: 0x00003739
		public string Session { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005542 File Offset: 0x00003742
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000554A File Offset: 0x0000374A
		public Guid? ActivityId { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005553 File Offset: 0x00003753
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000555B File Offset: 0x0000375B
		public string CorrelationId { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005564 File Offset: 0x00003764
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x0000556C File Offset: 0x0000376C
		public bool LegacyRedirects { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005575 File Offset: 0x00003775
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000557D File Offset: 0x0000377D
		public bool ThrowFoldingFailures { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005586 File Offset: 0x00003786
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000558E File Offset: 0x0000378E
		public bool ThrowOnVolatileFunctions { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00005597 File Offset: 0x00003797
		// (set) Token: 0x060000CB RID: 203 RVA: 0x0000559F File Offset: 0x0000379F
		public bool IgnorePreviouslyCachedData { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000055A8 File Offset: 0x000037A8
		// (set) Token: 0x060000CD RID: 205 RVA: 0x000055B0 File Offset: 0x000037B0
		public SafeHandle ThreadIdentity { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000055B9 File Offset: 0x000037B9
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000055C1 File Offset: 0x000037C1
		public string UiCulture { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000055CA File Offset: 0x000037CA
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000055D2 File Offset: 0x000037D2
		public DateTimeOffset? Now { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000055DB File Offset: 0x000037DB
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000055E3 File Offset: 0x000037E3
		public IDictionary<string, object> ConfigurationProperties { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000055EC File Offset: 0x000037EC
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000055F4 File Offset: 0x000037F4
		public string[] TracingOptions { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000055FD File Offset: 0x000037FD
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00005605 File Offset: 0x00003805
		public CredentialsStorage CredentialsStorage { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000560E File Offset: 0x0000380E
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00005616 File Offset: 0x00003816
		public QueryPermissionsStorage QueryPermissionsStorage { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000561F File Offset: 0x0000381F
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00005627 File Offset: 0x00003827
		public FirewallStorage FirewallStorage { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005630 File Offset: 0x00003830
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00005638 File Offset: 0x00003838
		public IFirewallPlan FirewallPlan { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005641 File Offset: 0x00003841
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00005649 File Offset: 0x00003849
		public string DataSourcePool { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005652 File Offset: 0x00003852
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x0000565A File Offset: 0x0000385A
		public bool Debug { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005663 File Offset: 0x00003863
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000566B File Offset: 0x0000386B
		public string CacheGroup { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005674 File Offset: 0x00003874
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000567C File Offset: 0x0000387C
		public string MetadataCache { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005685 File Offset: 0x00003885
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x0000568D File Offset: 0x0000388D
		public Action<Func<IEnumerable<PartitionProgress>>> PartitionProgressUpdater { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005696 File Offset: 0x00003896
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x0000569E File Offset: 0x0000389E
		public IContainerEvaluationMonitorService ContainerEvaluationMonitorService { get; private set; }
	}
}
