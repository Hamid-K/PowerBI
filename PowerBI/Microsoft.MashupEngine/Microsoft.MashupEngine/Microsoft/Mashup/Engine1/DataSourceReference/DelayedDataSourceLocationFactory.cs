using System;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Resources;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018C2 RID: 6338
	internal class DelayedDataSourceLocationFactory : IDataSourceLocationFactory
	{
		// Token: 0x0600A19D RID: 41373 RVA: 0x00218A58 File Offset: 0x00216C58
		public DelayedDataSourceLocationFactory(string protocol, string moduleName, string resourceKind)
		{
			this.protocol = protocol;
			this.moduleName = moduleName;
			this.resourceKind = resourceKind;
		}

		// Token: 0x1700294D RID: 10573
		// (get) Token: 0x0600A19E RID: 41374 RVA: 0x00218A75 File Offset: 0x00216C75
		public string Protocol
		{
			get
			{
				return this.protocol;
			}
		}

		// Token: 0x1700294E RID: 10574
		// (get) Token: 0x0600A19F RID: 41375 RVA: 0x00218A7D File Offset: 0x00216C7D
		public string ResourceKind
		{
			get
			{
				return this.resourceKind;
			}
		}

		// Token: 0x0600A1A0 RID: 41376 RVA: 0x00218A85 File Offset: 0x00216C85
		public IDataSourceLocation New()
		{
			return this.Factory.New();
		}

		// Token: 0x0600A1A1 RID: 41377 RVA: 0x00218A92 File Offset: 0x00216C92
		public bool TryCreateFromResource(IResource resource, bool normalize, out IDataSourceLocation location)
		{
			return this.Factory.TryCreateFromResource(resource, normalize, out location);
		}

		// Token: 0x1700294F RID: 10575
		// (get) Token: 0x0600A1A2 RID: 41378 RVA: 0x00218AA2 File Offset: 0x00216CA2
		private IDataSourceLocationFactory Factory
		{
			get
			{
				if (this.factory == null)
				{
					this.factory = this.LoadDataSourceLocationFactory();
				}
				return this.factory;
			}
		}

		// Token: 0x0600A1A3 RID: 41379 RVA: 0x00218AC0 File Offset: 0x00216CC0
		private IDataSourceLocationFactory LoadDataSourceLocationFactory()
		{
			Modules.EnsureLoaded(EngineHost.Empty, this.moduleName);
			IDataSourceLocationFactory dataSourceLocationFactory;
			ResourceKinds.TryGetDataSourceLocationFactory(this.protocol, out dataSourceLocationFactory);
			return dataSourceLocationFactory;
		}

		// Token: 0x04005491 RID: 21649
		private readonly string protocol;

		// Token: 0x04005492 RID: 21650
		private readonly string moduleName;

		// Token: 0x04005493 RID: 21651
		private readonly string resourceKind;

		// Token: 0x04005494 RID: 21652
		private IDataSourceLocationFactory factory;
	}
}
