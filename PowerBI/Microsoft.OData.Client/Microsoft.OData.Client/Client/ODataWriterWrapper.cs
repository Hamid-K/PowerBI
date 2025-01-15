using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000068 RID: 104
	internal class ODataWriterWrapper
	{
		// Token: 0x06000387 RID: 903 RVA: 0x0000D752 File Offset: 0x0000B952
		private ODataWriterWrapper(ODataWriter odataWriter, DataServiceClientRequestPipelineConfiguration requestPipeline)
		{
			this.odataWriter = odataWriter;
			this.requestPipeline = requestPipeline;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000D768 File Offset: 0x0000B968
		internal static ODataWriterWrapper CreateForEntry(ODataMessageWriter messageWriter, DataServiceClientRequestPipelineConfiguration requestPipeline)
		{
			return new ODataWriterWrapper(messageWriter.CreateODataResourceWriter(), requestPipeline);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000D776 File Offset: 0x0000B976
		internal static ODataWriterWrapper CreateForEntryTest(ODataWriter writer, DataServiceClientRequestPipelineConfiguration requestPipeline)
		{
			return new ODataWriterWrapper(writer, requestPipeline);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000D77F File Offset: 0x0000B97F
		internal void WriteStart(ODataResourceSet resourceSet)
		{
			this.odataWriter.WriteStart(resourceSet);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000D78D File Offset: 0x0000B98D
		internal void WriteEnd()
		{
			this.odataWriter.WriteEnd();
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000D79A File Offset: 0x0000B99A
		internal void WriteStartResource(ODataResource resource)
		{
			this.odataWriter.WriteStart(resource);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D7A8 File Offset: 0x0000B9A8
		internal void WriteStart(ODataResource resource, object entity)
		{
			this.requestPipeline.ExecuteOnEntryStartActions(resource, entity);
			this.odataWriter.WriteStart(resource);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D7C3 File Offset: 0x0000B9C3
		internal void WriteEnd(ODataResource entry, object entity)
		{
			this.requestPipeline.ExecuteOnEntryEndActions(entry, entity);
			this.odataWriter.WriteEnd();
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000D7DD File Offset: 0x0000B9DD
		internal void WriteEnd(ODataNestedResourceInfo navlink, object source, object target)
		{
			this.requestPipeline.ExecuteOnNestedResourceInfoEndActions(navlink, source, target);
			this.odataWriter.WriteEnd();
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		internal void WriteNestedResourceInfoEnd(ODataNestedResourceInfo navlink, object source, object target)
		{
			this.requestPipeline.ExecuteOnNestedResourceInfoEndActions(navlink, source, target);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000D78D File Offset: 0x0000B98D
		internal void WriteNestedResourceInfoEnd()
		{
			this.odataWriter.WriteEnd();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000D808 File Offset: 0x0000BA08
		internal void WriteStart(ODataNestedResourceInfo navigationLink, object source, object target)
		{
			this.requestPipeline.ExecuteOnNestedResourceInfoStartActions(navigationLink, source, target);
			this.odataWriter.WriteStart(navigationLink);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000D824 File Offset: 0x0000BA24
		internal void WriteNestedResourceInfoStart(ODataNestedResourceInfo navigationLink, object source, object target)
		{
			this.requestPipeline.ExecuteOnNestedResourceInfoStartActions(navigationLink, source, target);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000D834 File Offset: 0x0000BA34
		internal void WriteNestedResourceInfoStart(ODataNestedResourceInfo navigationLink)
		{
			this.odataWriter.WriteStart(navigationLink);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D842 File Offset: 0x0000BA42
		internal void WriteEntityReferenceLink(ODataEntityReferenceLink referenceLink, object source, object target)
		{
			this.requestPipeline.ExecuteEntityReferenceLinkActions(referenceLink, source, target);
			this.odataWriter.WriteEntityReferenceLink(referenceLink);
		}

		// Token: 0x04000122 RID: 290
		private readonly ODataWriter odataWriter;

		// Token: 0x04000123 RID: 291
		private readonly DataServiceClientRequestPipelineConfiguration requestPipeline;
	}
}
