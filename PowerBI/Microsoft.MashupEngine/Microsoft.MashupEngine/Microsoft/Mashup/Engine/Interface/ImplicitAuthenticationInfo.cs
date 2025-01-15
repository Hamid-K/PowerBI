using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200000A RID: 10
	public sealed class ImplicitAuthenticationInfo : AuthenticationInfo
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002105 File Offset: 0x00000305
		public override AuthenticationKind AuthenticationKind
		{
			get
			{
				return AuthenticationKind.Implicit;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002108 File Offset: 0x00000308
		public override string Name
		{
			get
			{
				return "Implicit";
			}
		}
	}
}
