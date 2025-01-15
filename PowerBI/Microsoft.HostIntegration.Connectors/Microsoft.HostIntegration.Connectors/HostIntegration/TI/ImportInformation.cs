using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip;
using Microsoft.HostIntegration.TI.Linq;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000752 RID: 1874
	public class ImportInformation
	{
		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x000CB1E0 File Offset: 0x000C93E0
		// (set) Token: 0x06003B70 RID: 15216 RVA: 0x000CB1E8 File Offset: 0x000C93E8
		public List<string> Warnings { get; set; }

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x000CB1F1 File Offset: 0x000C93F1
		// (set) Token: 0x06003B72 RID: 15218 RVA: 0x000CB1F9 File Offset: 0x000C93F9
		public List<string> Errors { get; set; }

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x000CB202 File Offset: 0x000C9402
		// (set) Token: 0x06003B74 RID: 15220 RVA: 0x000CB20A File Offset: 0x000C940A
		public HipConfigurationSectionHandler HipConfigurationSection { get; set; }

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06003B75 RID: 15221 RVA: 0x000CB213 File Offset: 0x000C9413
		// (set) Token: 0x06003B76 RID: 15222 RVA: 0x000CB21B File Offset: 0x000C941B
		internal ImportedHIPDatabase ImportedDatabase { get; set; }

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06003B77 RID: 15223 RVA: 0x000CB224 File Offset: 0x000C9424
		// (set) Token: 0x06003B78 RID: 15224 RVA: 0x000CB22C File Offset: 0x000C942C
		internal Computer ReplacementComputer { get; set; }

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x000CB235 File Offset: 0x000C9435
		// (set) Token: 0x06003B7A RID: 15226 RVA: 0x000CB23D File Offset: 0x000C943D
		internal string UsedDirectory { get; set; }

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06003B7B RID: 15227 RVA: 0x000CB246 File Offset: 0x000C9446
		// (set) Token: 0x06003B7C RID: 15228 RVA: 0x000CB24E File Offset: 0x000C944E
		internal Dictionary<int, LinqHostEnvironment> ValidHostEnvironments { get; set; }

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06003B7D RID: 15229 RVA: 0x000CB257 File Offset: 0x000C9457
		// (set) Token: 0x06003B7E RID: 15230 RVA: 0x000CB25F File Offset: 0x000C945F
		internal Dictionary<int, Microsoft.HostIntegration.TI.Linq.Object> ValidObjects { get; set; }

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06003B7F RID: 15231 RVA: 0x000CB268 File Offset: 0x000C9468
		// (set) Token: 0x06003B80 RID: 15232 RVA: 0x000CB270 File Offset: 0x000C9470
		internal Dictionary<int, SecurityPolicy> ValidSecurityPolicies { get; set; }

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x000CB279 File Offset: 0x000C9479
		// (set) Token: 0x06003B82 RID: 15234 RVA: 0x000CB281 File Offset: 0x000C9481
		internal Dictionary<int, LocalEnvironment> ValidLocalEnvironments { get; set; }

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06003B83 RID: 15235 RVA: 0x000CB28A File Offset: 0x000C948A
		// (set) Token: 0x06003B84 RID: 15236 RVA: 0x000CB292 File Offset: 0x000C9492
		internal Dictionary<int, LEEndpoint> ValidEndpoints { get; set; }

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06003B85 RID: 15237 RVA: 0x000CB29B File Offset: 0x000C949B
		// (set) Token: 0x06003B86 RID: 15238 RVA: 0x000CB2A3 File Offset: 0x000C94A3
		internal Dictionary<int, Determinant> UsedDeterminants { get; set; }

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06003B87 RID: 15239 RVA: 0x000CB2AC File Offset: 0x000C94AC
		// (set) Token: 0x06003B88 RID: 15240 RVA: 0x000CB2B4 File Offset: 0x000C94B4
		internal Dictionary<int, LinqHostEnvironment> UsedHostEnvironments { get; set; }

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06003B89 RID: 15241 RVA: 0x000CB2BD File Offset: 0x000C94BD
		// (set) Token: 0x06003B8A RID: 15242 RVA: 0x000CB2C5 File Offset: 0x000C94C5
		internal Dictionary<int, Microsoft.HostIntegration.TI.Linq.Object> UsedObjects { get; set; }

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06003B8B RID: 15243 RVA: 0x000CB2CE File Offset: 0x000C94CE
		// (set) Token: 0x06003B8C RID: 15244 RVA: 0x000CB2D6 File Offset: 0x000C94D6
		internal Dictionary<int, SecurityPolicy> UsedSecurityPolicies { get; set; }

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06003B8D RID: 15245 RVA: 0x000CB2DF File Offset: 0x000C94DF
		// (set) Token: 0x06003B8E RID: 15246 RVA: 0x000CB2E7 File Offset: 0x000C94E7
		internal Dictionary<int, DeterminantInformation> DeterminantTypeToDeterminantInformations { get; set; }
	}
}
