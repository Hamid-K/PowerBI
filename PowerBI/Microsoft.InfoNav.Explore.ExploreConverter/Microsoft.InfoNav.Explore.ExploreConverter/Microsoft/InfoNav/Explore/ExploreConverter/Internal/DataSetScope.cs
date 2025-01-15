using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000030 RID: 48
	internal sealed class DataSetScope : IDisposable
	{
		// Token: 0x06000166 RID: 358 RVA: 0x000070EF File Offset: 0x000052EF
		internal DataSetScope(IReportDeserializationContext ctx, DataRegion dataRegion)
		{
			this._ctx = ctx;
			ctx.PushDataScope(dataRegion);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007105 File Offset: 0x00005305
		internal DataSetScope(IReportDeserializationContext ctx, Group group)
		{
			this._ctx = ctx;
			ctx.PushDataScope(group);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000711B File Offset: 0x0000531B
		internal DataSetScope(IReportDeserializationContext ctx, LabelData labelData)
		{
			this._ctx = ctx;
			ctx.PushDataScope(labelData);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007131 File Offset: 0x00005331
		public void Dispose()
		{
			this._ctx.PopDataScope();
		}

		// Token: 0x040000BD RID: 189
		private readonly IReportDeserializationContext _ctx;
	}
}
