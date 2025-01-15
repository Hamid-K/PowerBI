using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001963 RID: 6499
	public class EvaluationSettings
	{
		// Token: 0x0600A4C6 RID: 42182 RVA: 0x00221CD6 File Offset: 0x0021FED6
		public EvaluationSettings()
		{
			this.AllowAutomaticCredentials = true;
			this.AllowWindowsAuthentication = true;
			this.MaxTempSize = -1L;
			this.MaxWorkingSetInMB = -1;
		}

		// Token: 0x17002A03 RID: 10755
		// (get) Token: 0x0600A4C7 RID: 42183 RVA: 0x00221CFB File Offset: 0x0021FEFB
		// (set) Token: 0x0600A4C8 RID: 42184 RVA: 0x00221D03 File Offset: 0x0021FF03
		public string Session { get; set; }

		// Token: 0x17002A04 RID: 10756
		// (get) Token: 0x0600A4C9 RID: 42185 RVA: 0x00221D0C File Offset: 0x0021FF0C
		// (set) Token: 0x0600A4CA RID: 42186 RVA: 0x00221D14 File Offset: 0x0021FF14
		public string CachePath { get; set; }

		// Token: 0x17002A05 RID: 10757
		// (get) Token: 0x0600A4CB RID: 42187 RVA: 0x00221D1D File Offset: 0x0021FF1D
		// (set) Token: 0x0600A4CC RID: 42188 RVA: 0x00221D25 File Offset: 0x0021FF25
		public CacheSettings MetadataCache { get; set; }

		// Token: 0x17002A06 RID: 10758
		// (get) Token: 0x0600A4CD RID: 42189 RVA: 0x00221D2E File Offset: 0x0021FF2E
		// (set) Token: 0x0600A4CE RID: 42190 RVA: 0x00221D36 File Offset: 0x0021FF36
		public CacheSettings DataCache { get; set; }

		// Token: 0x17002A07 RID: 10759
		// (get) Token: 0x0600A4CF RID: 42191 RVA: 0x00221D3F File Offset: 0x0021FF3F
		// (set) Token: 0x0600A4D0 RID: 42192 RVA: 0x00221D47 File Offset: 0x0021FF47
		public int MaxWorkingSetInMB { get; set; }

		// Token: 0x17002A08 RID: 10760
		// (get) Token: 0x0600A4D1 RID: 42193 RVA: 0x00221D50 File Offset: 0x0021FF50
		// (set) Token: 0x0600A4D2 RID: 42194 RVA: 0x00221D58 File Offset: 0x0021FF58
		public string DefaultCulture { get; set; }

		// Token: 0x17002A09 RID: 10761
		// (get) Token: 0x0600A4D3 RID: 42195 RVA: 0x00221D61 File Offset: 0x0021FF61
		// (set) Token: 0x0600A4D4 RID: 42196 RVA: 0x00221D69 File Offset: 0x0021FF69
		public long MaxTempSize { get; set; }

		// Token: 0x17002A0A RID: 10762
		// (get) Token: 0x0600A4D5 RID: 42197 RVA: 0x00221D72 File Offset: 0x0021FF72
		// (set) Token: 0x0600A4D6 RID: 42198 RVA: 0x00221D7A File Offset: 0x0021FF7A
		public string TempPath { get; set; }

		// Token: 0x17002A0B RID: 10763
		// (get) Token: 0x0600A4D7 RID: 42199 RVA: 0x00221D83 File Offset: 0x0021FF83
		// (set) Token: 0x0600A4D8 RID: 42200 RVA: 0x00221D8B File Offset: 0x0021FF8B
		public CredentialsStorage Credentials { get; set; }

		// Token: 0x17002A0C RID: 10764
		// (get) Token: 0x0600A4D9 RID: 42201 RVA: 0x00221D94 File Offset: 0x0021FF94
		// (set) Token: 0x0600A4DA RID: 42202 RVA: 0x00221D9C File Offset: 0x0021FF9C
		public QueryPermission[] QueryPermissions { get; set; }

		// Token: 0x17002A0D RID: 10765
		// (get) Token: 0x0600A4DB RID: 42203 RVA: 0x00221DA5 File Offset: 0x0021FFA5
		// (set) Token: 0x0600A4DC RID: 42204 RVA: 0x00221DAD File Offset: 0x0021FFAD
		public FirewallRule[] FirewallRules { get; set; }

		// Token: 0x17002A0E RID: 10766
		// (get) Token: 0x0600A4DD RID: 42205 RVA: 0x00221DB6 File Offset: 0x0021FFB6
		// (set) Token: 0x0600A4DE RID: 42206 RVA: 0x00221DBE File Offset: 0x0021FFBE
		public ConnectionGovernanceManager ConnectionGovernanceManager { get; set; }

		// Token: 0x17002A0F RID: 10767
		// (get) Token: 0x0600A4DF RID: 42207 RVA: 0x00221DC7 File Offset: 0x0021FFC7
		// (set) Token: 0x0600A4E0 RID: 42208 RVA: 0x00221DCF File Offset: 0x0021FFCF
		public bool AllowAutomaticCredentials { get; set; }

		// Token: 0x17002A10 RID: 10768
		// (get) Token: 0x0600A4E1 RID: 42209 RVA: 0x00221DD8 File Offset: 0x0021FFD8
		// (set) Token: 0x0600A4E2 RID: 42210 RVA: 0x00221DE0 File Offset: 0x0021FFE0
		public bool AllowWindowsAuthentication { get; set; }

		// Token: 0x17002A11 RID: 10769
		// (get) Token: 0x0600A4E3 RID: 42211 RVA: 0x00221DE9 File Offset: 0x0021FFE9
		// (set) Token: 0x0600A4E4 RID: 42212 RVA: 0x00221DF1 File Offset: 0x0021FFF1
		public bool AllowNativeQueries { get; set; }

		// Token: 0x17002A12 RID: 10770
		// (get) Token: 0x0600A4E5 RID: 42213 RVA: 0x00221DFA File Offset: 0x0021FFFA
		// (set) Token: 0x0600A4E6 RID: 42214 RVA: 0x00221E02 File Offset: 0x00220002
		public Guid? ActivityId { get; set; }

		// Token: 0x17002A13 RID: 10771
		// (get) Token: 0x0600A4E7 RID: 42215 RVA: 0x00221E0B File Offset: 0x0022000B
		// (set) Token: 0x0600A4E8 RID: 42216 RVA: 0x00221E13 File Offset: 0x00220013
		public string CorrelationId { get; set; }

		// Token: 0x17002A14 RID: 10772
		// (get) Token: 0x0600A4E9 RID: 42217 RVA: 0x00221E1C File Offset: 0x0022001C
		// (set) Token: 0x0600A4EA RID: 42218 RVA: 0x00221E24 File Offset: 0x00220024
		public string[] DefaultOptionalModules { get; set; }

		// Token: 0x17002A15 RID: 10773
		// (get) Token: 0x0600A4EB RID: 42219 RVA: 0x00221E2D File Offset: 0x0022002D
		// (set) Token: 0x0600A4EC RID: 42220 RVA: 0x00221E35 File Offset: 0x00220035
		public bool LegacyRedirects { get; set; }

		// Token: 0x17002A16 RID: 10774
		// (get) Token: 0x0600A4ED RID: 42221 RVA: 0x00221E3E File Offset: 0x0022003E
		// (set) Token: 0x0600A4EE RID: 42222 RVA: 0x00221E46 File Offset: 0x00220046
		public bool AllowActions { get; set; }

		// Token: 0x17002A17 RID: 10775
		// (get) Token: 0x0600A4EF RID: 42223 RVA: 0x00221E4F File Offset: 0x0022004F
		// (set) Token: 0x0600A4F0 RID: 42224 RVA: 0x00221E57 File Offset: 0x00220057
		public bool LogDataSourceAccess { get; set; }

		// Token: 0x17002A18 RID: 10776
		// (get) Token: 0x0600A4F1 RID: 42225 RVA: 0x00221E60 File Offset: 0x00220060
		// (set) Token: 0x0600A4F2 RID: 42226 RVA: 0x00221E68 File Offset: 0x00220068
		public bool ThrowOnFoldingFailure { get; set; }

		// Token: 0x17002A19 RID: 10777
		// (get) Token: 0x0600A4F3 RID: 42227 RVA: 0x00221E71 File Offset: 0x00220071
		// (set) Token: 0x0600A4F4 RID: 42228 RVA: 0x00221E79 File Offset: 0x00220079
		public bool ThrowOnVolatileFunctions { get; set; }

		// Token: 0x17002A1A RID: 10778
		// (get) Token: 0x0600A4F5 RID: 42229 RVA: 0x00221E82 File Offset: 0x00220082
		// (set) Token: 0x0600A4F6 RID: 42230 RVA: 0x00221E8A File Offset: 0x0022008A
		public SafeHandle ThreadIdentity { get; set; }

		// Token: 0x17002A1B RID: 10779
		// (get) Token: 0x0600A4F7 RID: 42231 RVA: 0x00221E93 File Offset: 0x00220093
		// (set) Token: 0x0600A4F8 RID: 42232 RVA: 0x00221E9B File Offset: 0x0022009B
		public DateTime? UtcNow { get; set; }

		// Token: 0x17002A1C RID: 10780
		// (get) Token: 0x0600A4F9 RID: 42233 RVA: 0x00221EA4 File Offset: 0x002200A4
		// (set) Token: 0x0600A4FA RID: 42234 RVA: 0x00221EAC File Offset: 0x002200AC
		public IDictionary<string, object> ConfigurationProperties { get; set; }

		// Token: 0x17002A1D RID: 10781
		// (get) Token: 0x0600A4FB RID: 42235 RVA: 0x00221EB5 File Offset: 0x002200B5
		// (set) Token: 0x0600A4FC RID: 42236 RVA: 0x00221EBD File Offset: 0x002200BD
		public string[] TracingOptions { get; set; }

		// Token: 0x17002A1E RID: 10782
		// (get) Token: 0x0600A4FD RID: 42237 RVA: 0x00221EC6 File Offset: 0x002200C6
		// (set) Token: 0x0600A4FE RID: 42238 RVA: 0x00221ECE File Offset: 0x002200CE
		public bool GCBetweenEvaluations { get; set; }

		// Token: 0x17002A1F RID: 10783
		// (get) Token: 0x0600A4FF RID: 42239 RVA: 0x00221ED7 File Offset: 0x002200D7
		// (set) Token: 0x0600A500 RID: 42240 RVA: 0x00221EDF File Offset: 0x002200DF
		public string DisableDecryptExcelFileError { get; set; }

		// Token: 0x17002A20 RID: 10784
		// (get) Token: 0x0600A501 RID: 42241 RVA: 0x00221EE8 File Offset: 0x002200E8
		// (set) Token: 0x0600A502 RID: 42242 RVA: 0x00221EF0 File Offset: 0x002200F0
		public string CacheGroup { get; set; }

		// Token: 0x17002A21 RID: 10785
		// (get) Token: 0x0600A503 RID: 42243 RVA: 0x00221EF9 File Offset: 0x002200F9
		// (set) Token: 0x0600A504 RID: 42244 RVA: 0x00221F01 File Offset: 0x00220101
		public string NamedMetadataCache { get; set; }

		// Token: 0x0600A505 RID: 42245 RVA: 0x00221F0C File Offset: 0x0022010C
		public EvaluationSettings Clone()
		{
			return new EvaluationSettings
			{
				Session = this.Session,
				CachePath = this.CachePath,
				MetadataCache = ((this.MetadataCache != null) ? this.MetadataCache.Clone() : null),
				DataCache = this.DataCache.Clone(),
				DefaultCulture = this.DefaultCulture,
				MaxTempSize = this.MaxTempSize,
				TempPath = this.TempPath,
				Credentials = this.Credentials,
				QueryPermissions = this.QueryPermissions,
				FirewallRules = this.FirewallRules,
				ConnectionGovernanceManager = this.ConnectionGovernanceManager,
				AllowAutomaticCredentials = this.AllowAutomaticCredentials,
				AllowWindowsAuthentication = this.AllowWindowsAuthentication,
				AllowNativeQueries = this.AllowNativeQueries,
				ActivityId = this.ActivityId,
				CorrelationId = this.CorrelationId,
				DefaultOptionalModules = this.DefaultOptionalModules,
				LegacyRedirects = this.LegacyRedirects,
				AllowActions = this.AllowActions,
				LogDataSourceAccess = this.LogDataSourceAccess,
				ThrowOnFoldingFailure = this.ThrowOnFoldingFailure,
				ThrowOnVolatileFunctions = this.ThrowOnVolatileFunctions,
				ThreadIdentity = this.ThreadIdentity,
				UtcNow = this.UtcNow,
				ConfigurationProperties = this.ConfigurationProperties,
				GCBetweenEvaluations = this.GCBetweenEvaluations,
				DisableDecryptExcelFileError = this.DisableDecryptExcelFileError,
				CacheGroup = this.CacheGroup,
				NamedMetadataCache = this.NamedMetadataCache
			};
		}

		// Token: 0x040055D1 RID: 21969
		public const long MaxTempSizeUnlimited = -1L;

		// Token: 0x040055D2 RID: 21970
		public const int MaxWorkingSetUnknown = -1;
	}
}
