using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x0200001F RID: 31
	public class AuthenticationTokenProvider : IAuthenticationTokenProvider
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002E36 File Offset: 0x00001036
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002E3E File Offset: 0x0000103E
		public Action<AuthenticationTokenCreateContext> OnCreate { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002E47 File Offset: 0x00001047
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002E4F File Offset: 0x0000104F
		public Func<AuthenticationTokenCreateContext, Task> OnCreateAsync { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002E58 File Offset: 0x00001058
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002E60 File Offset: 0x00001060
		public Action<AuthenticationTokenReceiveContext> OnReceive { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002E69 File Offset: 0x00001069
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00002E71 File Offset: 0x00001071
		public Func<AuthenticationTokenReceiveContext, Task> OnReceiveAsync { get; set; }

		// Token: 0x06000094 RID: 148 RVA: 0x00002E7A File Offset: 0x0000107A
		public virtual void Create(AuthenticationTokenCreateContext context)
		{
			if (this.OnCreateAsync != null && this.OnCreate == null)
			{
				throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
			}
			if (this.OnCreate != null)
			{
				this.OnCreate(context);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002EAC File Offset: 0x000010AC
		public virtual async Task CreateAsync(AuthenticationTokenCreateContext context)
		{
			if (this.OnCreateAsync != null && this.OnCreate == null)
			{
				throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
			}
			if (this.OnCreateAsync != null)
			{
				await this.OnCreateAsync(context);
			}
			else
			{
				this.Create(context);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002EF7 File Offset: 0x000010F7
		public virtual void Receive(AuthenticationTokenReceiveContext context)
		{
			if (this.OnReceiveAsync != null && this.OnReceive == null)
			{
				throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
			}
			if (this.OnReceive != null)
			{
				this.OnReceive(context);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002F28 File Offset: 0x00001128
		public virtual async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
		{
			if (this.OnReceiveAsync != null && this.OnReceive == null)
			{
				throw new InvalidOperationException(Resources.Exception_AuthenticationTokenDoesNotProvideSyncMethods);
			}
			if (this.OnReceiveAsync != null)
			{
				await this.OnReceiveAsync(context);
			}
			else
			{
				this.Receive(context);
			}
		}
	}
}
