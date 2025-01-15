using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x0200001D RID: 29
	public abstract class AuthenticationMiddleware<TOptions> : OwinMiddleware where TOptions : AuthenticationOptions
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00002D37 File Offset: 0x00000F37
		protected AuthenticationMiddleware(OwinMiddleware next, TOptions options)
			: base(next)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this.Options = options;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002D5A File Offset: 0x00000F5A
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002D62 File Offset: 0x00000F62
		public TOptions Options { get; set; }

		// Token: 0x06000083 RID: 131 RVA: 0x00002D6C File Offset: 0x00000F6C
		public override async Task Invoke(IOwinContext context)
		{
			AuthenticationHandler<TOptions> handler = this.CreateHandler();
			await handler.Initialize(this.Options, context);
			TaskAwaiter<bool> taskAwaiter = handler.InvokeAsync().GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<bool> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<bool>);
			}
			if (!taskAwaiter.GetResult())
			{
				await base.Next.Invoke(context);
			}
			await handler.TeardownAsync();
		}

		// Token: 0x06000084 RID: 132
		protected abstract AuthenticationHandler<TOptions> CreateHandler();
	}
}
