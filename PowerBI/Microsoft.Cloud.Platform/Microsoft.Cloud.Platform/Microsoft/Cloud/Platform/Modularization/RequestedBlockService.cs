using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000C3 RID: 195
	public class RequestedBlockService
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x00014471 File Offset: 0x00012671
		public RequestedBlockService(string name, IBlock serviceConsumer, [NotNull] Type serviceType, BlockServiceProviderIdentity serviceIdentity, object context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(serviceType, "serviceType");
			this.Name = name;
			this.m_serviceConsumer = serviceConsumer;
			this.m_serviceType = serviceType;
			this.m_serviceIdentity = serviceIdentity;
			this.m_context = context;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000144A9 File Offset: 0x000126A9
		public RequestedBlockService(IBlock serviceConsumer, Type serviceType, BlockServiceProviderIdentity serviceIdentity, object context)
			: this(null, serviceConsumer, serviceType, serviceIdentity, context)
		{
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000144B7 File Offset: 0x000126B7
		public RequestedBlockService(IBlock serviceConsumer, Type serviceType, BlockServiceProviderIdentity serviceIdentity)
			: this(null, serviceConsumer, serviceType, serviceIdentity, null)
		{
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000144C4 File Offset: 0x000126C4
		public RequestedBlockService(IBlock serviceConsumer, Type serviceType)
			: this(null, serviceConsumer, serviceType, BlockServiceProviderIdentity.Default, null)
		{
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x000144D1 File Offset: 0x000126D1
		public IBlock ServiceConsumer
		{
			get
			{
				return this.m_serviceConsumer;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x000144D9 File Offset: 0x000126D9
		public Type ServiceType
		{
			get
			{
				return this.m_serviceType;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x000144E1 File Offset: 0x000126E1
		public BlockServiceProviderIdentity ServiceIdentity
		{
			get
			{
				return this.m_serviceIdentity;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x000144E9 File Offset: 0x000126E9
		public object Context
		{
			get
			{
				return this.m_context;
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000144F4 File Offset: 0x000126F4
		internal bool Matches(RequestedBlockService other)
		{
			if (this.Name == null)
			{
				return other.Name == null && this.ServiceType == other.ServiceType && this.ServiceIdentity == other.ServiceIdentity;
			}
			return this.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00014547 File Offset: 0x00012747
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0001454F File Offset: 0x0001274F
		internal string Name { get; private set; }

		// Token: 0x040001F9 RID: 505
		private IBlock m_serviceConsumer;

		// Token: 0x040001FA RID: 506
		private object m_context;

		// Token: 0x040001FB RID: 507
		private Type m_serviceType;

		// Token: 0x040001FC RID: 508
		private BlockServiceProviderIdentity m_serviceIdentity;
	}
}
