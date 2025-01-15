using System;
using System.Threading.Tasks;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000A4 RID: 164
	internal abstract class ODataParameterReaderCoreAsync : ODataParameterReaderCore
	{
		// Token: 0x06000714 RID: 1812 RVA: 0x00010FEB File Offset: 0x0000F1EB
		protected ODataParameterReaderCoreAsync(ODataInputContext inputContext, IEdmOperation operation)
			: base(inputContext, operation)
		{
		}

		// Token: 0x06000715 RID: 1813
		protected abstract Task<bool> ReadAtStartImplementationAsync();

		// Token: 0x06000716 RID: 1814
		protected abstract Task<bool> ReadNextParameterImplementationAsync();

		// Token: 0x06000717 RID: 1815
		protected abstract Task<ODataReader> CreateResourceReaderAsync(IEdmStructuredType expectedResourceType);

		// Token: 0x06000718 RID: 1816
		protected abstract Task<ODataReader> CreateResourceSetReaderAsync(IEdmStructuredType expectedResourceType);

		// Token: 0x06000719 RID: 1817
		protected abstract Task<ODataCollectionReader> CreateCollectionReaderAsync(IEdmTypeReference expectedItemTypeReference);

		// Token: 0x0600071A RID: 1818 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		protected override Task<bool> ReadAsynchronously()
		{
			switch (this.State)
			{
			case ODataParameterReaderState.Start:
				return this.ReadAtStartImplementationAsync();
			case ODataParameterReaderState.Value:
			case ODataParameterReaderState.Collection:
			case ODataParameterReaderState.Resource:
			case ODataParameterReaderState.ResourceSet:
				base.OnParameterCompleted();
				return this.ReadNextParameterImplementationAsync();
			case ODataParameterReaderState.Exception:
			case ODataParameterReaderState.Completed:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterReaderCoreAsync_ReadAsynchronously));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterReaderCoreAsync_ReadAsynchronously));
			}
		}
	}
}
