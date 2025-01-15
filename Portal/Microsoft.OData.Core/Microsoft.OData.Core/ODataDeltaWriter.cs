using System;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x0200007A RID: 122
	public abstract class ODataDeltaWriter
	{
		// Token: 0x0600042C RID: 1068
		public abstract void WriteStart(ODataDeltaResourceSet deltaResourceSet);

		// Token: 0x0600042D RID: 1069
		public abstract Task WriteStartAsync(ODataDeltaResourceSet deltaResourceSet);

		// Token: 0x0600042E RID: 1070
		public abstract void WriteEnd();

		// Token: 0x0600042F RID: 1071
		public abstract Task WriteEndAsync();

		// Token: 0x06000430 RID: 1072
		public abstract void WriteStart(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x06000431 RID: 1073
		public abstract Task WriteStartAsync(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x06000432 RID: 1074
		public abstract void WriteStart(ODataResourceSet expandedResourceSet);

		// Token: 0x06000433 RID: 1075
		public abstract Task WriteStartAsync(ODataResourceSet expandedResourceSet);

		// Token: 0x06000434 RID: 1076
		public abstract void WriteStart(ODataResource deltaResource);

		// Token: 0x06000435 RID: 1077
		public abstract Task WriteStartAsync(ODataResource deltaResource);

		// Token: 0x06000436 RID: 1078
		public abstract void WriteDeltaDeletedEntry(ODataDeltaDeletedEntry deltaDeletedEntry);

		// Token: 0x06000437 RID: 1079
		public abstract Task WriteDeltaDeletedEntryAsync(ODataDeltaDeletedEntry deltaDeletedEntry);

		// Token: 0x06000438 RID: 1080
		public abstract void WriteDeltaLink(ODataDeltaLink deltaLink);

		// Token: 0x06000439 RID: 1081
		public abstract Task WriteDeltaLinkAsync(ODataDeltaLink deltaLink);

		// Token: 0x0600043A RID: 1082
		public abstract void WriteDeltaDeletedLink(ODataDeltaDeletedLink deltaDeletedLink);

		// Token: 0x0600043B RID: 1083
		public abstract Task WriteDeltaDeletedLinkAsync(ODataDeltaDeletedLink deltaDeletedLink);

		// Token: 0x0600043C RID: 1084
		public abstract void Flush();

		// Token: 0x0600043D RID: 1085
		public abstract Task FlushAsync();
	}
}
