using System;
using System.Threading.Tasks;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200023F RID: 575
	internal sealed class ODataJsonLightDeltaWriter : ODataDeltaWriter, IODataOutputInStreamErrorListener
	{
		// Token: 0x060018DB RID: 6363 RVA: 0x000479D7 File Offset: 0x00045BD7
		public ODataJsonLightDeltaWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmNavigationSource navigationSource, IEdmEntityType entityType)
		{
			this.navigationSource = navigationSource;
			this.entityType = entityType;
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.resourceWriter = new ODataJsonLightWriter(jsonLightOutputContext, navigationSource, entityType, true, false, true, null);
			this.inStreamErrorListener = this.resourceWriter;
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x00047A12 File Offset: 0x00045C12
		// (set) Token: 0x060018DD RID: 6365 RVA: 0x00047A1A File Offset: 0x00045C1A
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
			set
			{
				this.navigationSource = value;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x00047A23 File Offset: 0x00045C23
		// (set) Token: 0x060018DF RID: 6367 RVA: 0x00047A2B File Offset: 0x00045C2B
		public IEdmEntityType EntityType
		{
			get
			{
				return this.entityType;
			}
			set
			{
				this.entityType = value;
			}
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00047A34 File Offset: 0x00045C34
		public override void WriteStart(ODataDeltaResourceSet deltaResourceSet)
		{
			this.resourceWriter.WriteStart(deltaResourceSet);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00047A44 File Offset: 0x00045C44
		public override Task WriteStartAsync(ODataDeltaResourceSet deltaResourceSet)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.resourceWriter.WriteStart(deltaResourceSet);
			});
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x00047A76 File Offset: 0x00045C76
		public override void WriteEnd()
		{
			this.resourceWriter.WriteEnd();
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00047A83 File Offset: 0x00045C83
		public override Task WriteEndAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.resourceWriter.WriteEnd();
			});
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x00047A96 File Offset: 0x00045C96
		public override void WriteStart(ODataNestedResourceInfo nestedResourceInfo)
		{
			this.resourceWriter.WriteStart(nestedResourceInfo);
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00047AA4 File Offset: 0x00045CA4
		public override Task WriteStartAsync(ODataNestedResourceInfo nestedResourceInfo)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.resourceWriter.WriteStart(nestedResourceInfo);
			});
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00047AD6 File Offset: 0x00045CD6
		public override void WriteStart(ODataResourceSet expandedResourceSet)
		{
			this.resourceWriter.WriteStart(expandedResourceSet);
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00047AE4 File Offset: 0x00045CE4
		public override Task WriteStartAsync(ODataResourceSet expandedResourceSet)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.resourceWriter.WriteStart(expandedResourceSet);
			});
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00047B16 File Offset: 0x00045D16
		public override void WriteStart(ODataResource deltaResource)
		{
			this.resourceWriter.WriteStart(deltaResource);
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x00047B24 File Offset: 0x00045D24
		public override Task WriteStartAsync(ODataResource deltaResource)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.resourceWriter.WriteStart(deltaResource);
			});
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x00047B56 File Offset: 0x00045D56
		public override void WriteDeltaDeletedEntry(ODataDeltaDeletedEntry deltaDeletedEntry)
		{
			this.resourceWriter.WriteStart(ODataDeltaDeletedEntry.GetDeletedResource(deltaDeletedEntry));
			this.resourceWriter.WriteEnd();
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00047B74 File Offset: 0x00045D74
		public override Task WriteDeltaDeletedEntryAsync(ODataDeltaDeletedEntry deltaDeletedEntry)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.resourceWriter.WriteStart(ODataDeltaDeletedEntry.GetDeletedResource(deltaDeletedEntry));
			});
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00047BA6 File Offset: 0x00045DA6
		public override void WriteDeltaLink(ODataDeltaLink deltaLink)
		{
			this.resourceWriter.WriteDeltaLink(deltaLink);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00047BB4 File Offset: 0x00045DB4
		public override Task WriteDeltaLinkAsync(ODataDeltaLink deltaLink)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.resourceWriter.WriteDeltaLink(deltaLink);
			});
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00047BE6 File Offset: 0x00045DE6
		public override void WriteDeltaDeletedLink(ODataDeltaDeletedLink deltaDeletedLink)
		{
			this.resourceWriter.WriteDeltaDeletedLink(deltaDeletedLink);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00047BF4 File Offset: 0x00045DF4
		public override Task WriteDeltaDeletedLinkAsync(ODataDeltaDeletedLink deltaDeletedLink)
		{
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.resourceWriter.WriteDeltaDeletedLink(deltaDeletedLink);
			});
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00047C26 File Offset: 0x00045E26
		public override void Flush()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00047C33 File Offset: 0x00045E33
		public override Task FlushAsync()
		{
			return this.jsonLightOutputContext.FlushAsync();
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00047C40 File Offset: 0x00045E40
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.inStreamErrorListener.OnInStreamError();
		}

		// Token: 0x04000B2D RID: 2861
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000B2E RID: 2862
		private readonly ODataJsonLightWriter resourceWriter;

		// Token: 0x04000B2F RID: 2863
		private IEdmNavigationSource navigationSource;

		// Token: 0x04000B30 RID: 2864
		private IEdmEntityType entityType;

		// Token: 0x04000B31 RID: 2865
		private IODataOutputInStreamErrorListener inStreamErrorListener;
	}
}
