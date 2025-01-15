using System;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x020000B5 RID: 181
	internal abstract class ODataReaderCoreAsync : ODataReaderCore
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x00013447 File Offset: 0x00011647
		protected ODataReaderCoreAsync(ODataInputContext inputContext, bool readingResourceSet, bool readingDelta, IODataReaderWriterListener listener)
			: base(inputContext, readingResourceSet, readingDelta, listener)
		{
		}

		// Token: 0x06000814 RID: 2068
		protected abstract Task<bool> ReadAtStartImplementationAsync();

		// Token: 0x06000815 RID: 2069
		protected abstract Task<bool> ReadAtResourceSetStartImplementationAsync();

		// Token: 0x06000816 RID: 2070
		protected abstract Task<bool> ReadAtResourceSetEndImplementationAsync();

		// Token: 0x06000817 RID: 2071
		protected abstract Task<bool> ReadAtResourceStartImplementationAsync();

		// Token: 0x06000818 RID: 2072
		protected abstract Task<bool> ReadAtResourceEndImplementationAsync();

		// Token: 0x06000819 RID: 2073
		protected abstract Task<bool> ReadAtDeletedResourceEndImplementationAsync();

		// Token: 0x0600081A RID: 2074 RVA: 0x00013454 File Offset: 0x00011654
		protected virtual Task<bool> ReadAtPrimitiveImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtPrimitiveImplementation));
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00013468 File Offset: 0x00011668
		protected virtual Task<bool> ReadAtNestedPropertyInfoImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtNestedPropertyInfoImplementation));
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001347C File Offset: 0x0001167C
		protected virtual Task<bool> ReadAtStreamImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtStreamImplementation));
		}

		// Token: 0x0600081D RID: 2077
		protected abstract Task<bool> ReadAtNestedResourceInfoStartImplementationAsync();

		// Token: 0x0600081E RID: 2078
		protected abstract Task<bool> ReadAtNestedResourceInfoEndImplementationAsync();

		// Token: 0x0600081F RID: 2079
		protected abstract Task<bool> ReadAtEntityReferenceLinkAsync();

		// Token: 0x06000820 RID: 2080 RVA: 0x00013490 File Offset: 0x00011690
		protected virtual Task<bool> ReadAtDeltaResourceSetStartImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtDeltaResourceSetStartImplementation));
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000134A4 File Offset: 0x000116A4
		protected virtual Task<bool> ReadAtDeltaResourceSetEndImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtDeltaResourceSetEndImplementation));
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000134B8 File Offset: 0x000116B8
		protected virtual Task<bool> ReadAtDeletedResourceStartImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtDeletedResourceStartImplementation));
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000134CC File Offset: 0x000116CC
		protected virtual Task<bool> ReadDeletedResourceEndImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtDeletedResourceEndImplementation));
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000134E0 File Offset: 0x000116E0
		protected virtual Task<bool> ReadAtDeltaLinkImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtDeltaLinkImplementation));
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000134F4 File Offset: 0x000116F4
		protected virtual Task<bool> ReadAtDeltaDeletedLinkImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtDeltaDeletedLinkImplementation));
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00013508 File Offset: 0x00011708
		protected override Task<bool> ReadAsynchronously()
		{
			Task<bool> task;
			switch (this.State)
			{
			case ODataReaderState.Start:
				task = this.ReadAtStartImplementationAsync();
				break;
			case ODataReaderState.ResourceSetStart:
				task = this.ReadAtResourceSetStartImplementationAsync();
				break;
			case ODataReaderState.ResourceSetEnd:
				task = this.ReadAtResourceSetEndImplementationAsync();
				break;
			case ODataReaderState.ResourceStart:
				task = TaskUtils.GetTaskForSynchronousOperation(delegate
				{
					base.IncreaseResourceDepth();
				}).FollowOnSuccessWithTask((Task t) => this.ReadAtResourceStartImplementationAsync());
				break;
			case ODataReaderState.ResourceEnd:
				task = TaskUtils.GetTaskForSynchronousOperation(delegate
				{
					base.DecreaseResourceDepth();
				}).FollowOnSuccessWithTask((Task t) => this.ReadAtResourceEndImplementationAsync());
				break;
			case ODataReaderState.NestedResourceInfoStart:
				task = this.ReadAtNestedResourceInfoStartImplementationAsync();
				break;
			case ODataReaderState.NestedResourceInfoEnd:
				task = this.ReadAtNestedResourceInfoEndImplementationAsync();
				break;
			case ODataReaderState.EntityReferenceLink:
				task = this.ReadAtEntityReferenceLinkAsync();
				break;
			case ODataReaderState.Exception:
			case ODataReaderState.Completed:
				task = TaskUtils.GetFaultedTask<bool>(new ODataException(Strings.ODataReaderCore_NoReadCallsAllowed(this.State)));
				break;
			case ODataReaderState.Primitive:
				task = this.ReadAtPrimitiveImplementationAsync();
				break;
			case ODataReaderState.DeltaResourceSetStart:
				task = this.ReadAtDeltaResourceSetStartImplementationAsync();
				break;
			case ODataReaderState.DeltaResourceSetEnd:
				task = this.ReadAtDeltaResourceSetEndImplementationAsync();
				break;
			case ODataReaderState.DeletedResourceStart:
				task = TaskUtils.GetTaskForSynchronousOperation(delegate
				{
					base.IncreaseResourceDepth();
				}).FollowOnSuccessWithTask((Task t) => this.ReadAtDeletedResourceStartImplementationAsync());
				break;
			case ODataReaderState.DeletedResourceEnd:
				task = TaskUtils.GetTaskForSynchronousOperation(delegate
				{
					base.DecreaseResourceDepth();
				}).FollowOnSuccessWithTask((Task t) => this.ReadAtDeletedResourceEndImplementationAsync());
				break;
			case ODataReaderState.DeltaLink:
				task = this.ReadAtDeltaLinkImplementationAsync();
				break;
			case ODataReaderState.DeltaDeletedLink:
				task = this.ReadAtDeltaDeletedLinkImplementationAsync();
				break;
			case ODataReaderState.NestedProperty:
				task = this.ReadAtNestedPropertyInfoImplementationAsync();
				break;
			case ODataReaderState.Stream:
				task = this.ReadAtStreamImplementationAsync();
				break;
			default:
				task = TaskUtils.GetFaultedTask<bool>(new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataReaderCoreAsync_ReadAsynchronously)));
				break;
			}
			return task.FollowOnSuccessWith((Task<bool> t) => t.Result);
		}
	}
}
