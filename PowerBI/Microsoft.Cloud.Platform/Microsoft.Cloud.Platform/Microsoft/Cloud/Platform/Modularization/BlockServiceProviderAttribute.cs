using System;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000AB RID: 171
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true)]
	public sealed class BlockServiceProviderAttribute : Attribute
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00012078 File Offset: 0x00010278
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00012080 File Offset: 0x00010280
		public Type ServiceType { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00012089 File Offset: 0x00010289
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00012091 File Offset: 0x00010291
		public string ServiceIdentity { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001209A File Offset: 0x0001029A
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x000120A2 File Offset: 0x000102A2
		public BlockServiceProviderIdentity ServiceLevel { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x000120AB File Offset: 0x000102AB
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x000120B3 File Offset: 0x000102B3
		public BlockServicePublish PublishWhen { get; set; }

		// Token: 0x060004EE RID: 1262 RVA: 0x000120BC File Offset: 0x000102BC
		public BlockServiceProviderAttribute()
			: this(null, BlockServiceProviderIdentity.Implementation)
		{
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000120C6 File Offset: 0x000102C6
		public BlockServiceProviderAttribute(Type serviceType)
			: this(serviceType, BlockServiceProviderIdentity.Implementation)
		{
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000120D0 File Offset: 0x000102D0
		public BlockServiceProviderAttribute(BlockServiceProviderIdentity serviceIdentity)
			: this(null, serviceIdentity)
		{
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000120DA File Offset: 0x000102DA
		public BlockServiceProviderAttribute(Type serviceType, BlockServiceProviderIdentity serviceIdentity)
		{
			this.ServiceType = serviceType;
			this.ServiceIdentity = null;
			this.ServiceLevel = serviceIdentity;
			this.PublishWhen = BlockServicePublish.Default;
		}
	}
}
