using System;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.ExploreHost
{
	// Token: 0x02000028 RID: 40
	internal class ExploreClientHandlerBaseFlow : IExploreClientFlow
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00004256 File Offset: 0x00002456
		// (set) Token: 0x0600011C RID: 284 RVA: 0x0000425E File Offset: 0x0000245E
		private protected string DatabaseID { protected get; private set; }

		// Token: 0x0600011D RID: 285 RVA: 0x00004267 File Offset: 0x00002467
		internal ExploreClientHandlerBaseFlow(ExploreClientHandlerContext context, string databaseID)
		{
			this.Context = context;
			this.DatabaseID = databaseID;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000427D File Offset: 0x0000247D
		protected void PreRun()
		{
			this.Context.PowerViewHandler.EnsureSession(this.DatabaseID);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004298 File Offset: 0x00002498
		protected async Task PreRunAsync()
		{
			await this.Context.PowerViewHandler.EnsureSessionAsync(this.DatabaseID);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000042DB File Offset: 0x000024DB
		public void Run()
		{
			this.PreRun();
			this.InternalRun();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000042EC File Offset: 0x000024EC
		public async Task RunAsync()
		{
			if (this.Context.FeatureSwitches.AsyncExploreClientFlowsEnabled)
			{
				await this.PreRunAsync();
				this.InternalRun();
			}
			else
			{
				await Task.Run(delegate
				{
					this.Run();
				});
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000432F File Offset: 0x0000252F
		protected virtual void InternalRun()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400008F RID: 143
		protected ExploreClientHandlerContext Context;
	}
}
