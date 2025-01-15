using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000011 RID: 17
	public sealed class ExchangeAuthenticationInfo : AuthenticationInfo
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002461 File Offset: 0x00000661
		public override AuthenticationKind AuthenticationKind
		{
			get
			{
				return AuthenticationKind.Exchange;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000213C File Offset: 0x0000033C
		public override string Name
		{
			get
			{
				return "UsernamePassword";
			}
		}
	}
}
