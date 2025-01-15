using System;
using System.Data.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001155 RID: 4437
	internal sealed class TracingDbProviderFactoryService : IDbProviderFactoryService
	{
		// Token: 0x06007432 RID: 29746 RVA: 0x0018F410 File Offset: 0x0018D610
		private TracingDbProviderFactoryService(string providerName, Tracer tracer, IDbProviderFactoryService factoryService)
		{
			this.providerName = providerName;
			this.tracer = tracer;
			this.factoryService = factoryService;
		}

		// Token: 0x06007433 RID: 29747 RVA: 0x0018F42D File Offset: 0x0018D62D
		public DbProviderFactory GetProviderFactory()
		{
			return this.tracer.Trace<TracingDbProviderFactoryService.TracingDbProviderFactory>("GetProviderFactory", delegate(IHostTrace trace)
			{
				this.tracer.LogFeature(this.providerName);
				return new TracingDbProviderFactoryService.TracingDbProviderFactory(this.tracer, this.factoryService.GetProviderFactory());
			});
		}

		// Token: 0x06007434 RID: 29748 RVA: 0x0018F44C File Offset: 0x0018D64C
		public static IDbProviderFactoryService New(string providerName, string dataSourceName, IEngineHost host, IDbProviderFactoryService factoryService, IResource resource)
		{
			Tracer tracer = new Tracer(host, "Engine/IO/" + dataSourceName + "/", resource, null, null);
			return new TracingDbProviderFactoryService(providerName, tracer, factoryService);
		}

		// Token: 0x04003FF2 RID: 16370
		private readonly string providerName;

		// Token: 0x04003FF3 RID: 16371
		private readonly Tracer tracer;

		// Token: 0x04003FF4 RID: 16372
		private readonly IDbProviderFactoryService factoryService;

		// Token: 0x02001156 RID: 4438
		private class TracingDbProviderFactory : DelegatingDbProviderFactory
		{
			// Token: 0x06007436 RID: 29750 RVA: 0x0018F4A5 File Offset: 0x0018D6A5
			public TracingDbProviderFactory(Tracer tracer, DbProviderFactory factory)
				: base(factory)
			{
				this.tracer = tracer;
			}

			// Token: 0x06007437 RID: 29751 RVA: 0x0018F4B5 File Offset: 0x0018D6B5
			public override DbCommand CreateCommand()
			{
				return new TracingDbCommand(this.tracer, base.CreateCommand(), null);
			}

			// Token: 0x06007438 RID: 29752 RVA: 0x0018F4C9 File Offset: 0x0018D6C9
			public override DbConnection CreateConnection()
			{
				return new TracingDbConnection(this.tracer, base.CreateConnection(), null, false);
			}

			// Token: 0x04003FF5 RID: 16373
			private readonly Tracer tracer;
		}
	}
}
