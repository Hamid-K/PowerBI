using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.IdentityModel.Json;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000120 RID: 288
	public abstract class BaseConfiguration
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0003944D File Offset: 0x0003764D
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x00039455 File Offset: 0x00037655
		public virtual string Issuer { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0003945E File Offset: 0x0003765E
		public virtual ICollection<SecurityKey> SigningKeys { get; } = new Collection<SecurityKey>();

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00039466 File Offset: 0x00037666
		// (set) Token: 0x06000E57 RID: 3671 RVA: 0x0003946E File Offset: 0x0003766E
		public virtual string TokenEndpoint { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00039477 File Offset: 0x00037677
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x0003947F File Offset: 0x0003767F
		public virtual string ActiveTokenEndpoint { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00039488 File Offset: 0x00037688
		[JsonIgnore]
		public virtual ICollection<SecurityKey> TokenDecryptionKeys { get; } = new Collection<SecurityKey>();
	}
}
