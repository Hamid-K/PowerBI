using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000013 RID: 19
	public sealed class ParameterizedAuthenticationInfo : AuthenticationInfo
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0000210F File Offset: 0x0000030F
		public ParameterizedAuthenticationInfo()
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000024CF File Offset: 0x000006CF
		public ParameterizedAuthenticationInfo(string name, IList<CredentialProperty> properties = null)
		{
			this.name = name;
			base.Properties = properties;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000024E5 File Offset: 0x000006E5
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000024ED File Offset: 0x000006ED
		public override AuthenticationKind AuthenticationKind
		{
			get
			{
				return AuthenticationKind.Parameterized;
			}
		}

		// Token: 0x0400005C RID: 92
		private readonly string name;
	}
}
