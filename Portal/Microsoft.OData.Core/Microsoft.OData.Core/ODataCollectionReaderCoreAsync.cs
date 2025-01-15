using System;
using System.Threading.Tasks;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000063 RID: 99
	internal abstract class ODataCollectionReaderCoreAsync : ODataCollectionReaderCore
	{
		// Token: 0x06000386 RID: 902 RVA: 0x0000A530 File Offset: 0x00008730
		protected ODataCollectionReaderCoreAsync(ODataInputContext inputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
			: base(inputContext, expectedItemTypeReference, listener)
		{
		}

		// Token: 0x06000387 RID: 903
		protected abstract Task<bool> ReadAtStartImplementationAsync();

		// Token: 0x06000388 RID: 904
		protected abstract Task<bool> ReadAtCollectionStartImplementationAsync();

		// Token: 0x06000389 RID: 905
		protected abstract Task<bool> ReadAtValueImplementationAsync();

		// Token: 0x0600038A RID: 906
		protected abstract Task<bool> ReadAtCollectionEndImplementationAsync();

		// Token: 0x0600038B RID: 907 RVA: 0x0000A53C File Offset: 0x0000873C
		protected override Task<bool> ReadAsynchronously()
		{
			switch (this.State)
			{
			case ODataCollectionReaderState.Start:
				return this.ReadAtStartImplementationAsync();
			case ODataCollectionReaderState.CollectionStart:
				return this.ReadAtCollectionStartImplementationAsync();
			case ODataCollectionReaderState.Value:
				return this.ReadAtValueImplementationAsync();
			case ODataCollectionReaderState.CollectionEnd:
				return this.ReadAtCollectionEndImplementationAsync();
			default:
				return TaskUtils.GetFaultedTask<bool>(new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataCollectionReaderCoreAsync_ReadAsynchronously)));
			}
		}
	}
}
