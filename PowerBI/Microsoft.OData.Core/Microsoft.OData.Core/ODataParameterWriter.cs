using System;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x020000A6 RID: 166
	public abstract class ODataParameterWriter
	{
		// Token: 0x0600071B RID: 1819
		public abstract void WriteStart();

		// Token: 0x0600071C RID: 1820
		public abstract Task WriteStartAsync();

		// Token: 0x0600071D RID: 1821
		public abstract void WriteValue(string parameterName, object parameterValue);

		// Token: 0x0600071E RID: 1822
		public abstract Task WriteValueAsync(string parameterName, object parameterValue);

		// Token: 0x0600071F RID: 1823
		public abstract ODataCollectionWriter CreateCollectionWriter(string parameterName);

		// Token: 0x06000720 RID: 1824
		public abstract Task<ODataCollectionWriter> CreateCollectionWriterAsync(string parameterName);

		// Token: 0x06000721 RID: 1825
		public abstract ODataWriter CreateResourceWriter(string parameterName);

		// Token: 0x06000722 RID: 1826
		public abstract Task<ODataWriter> CreateResourceWriterAsync(string parameterName);

		// Token: 0x06000723 RID: 1827
		public abstract ODataWriter CreateResourceSetWriter(string parameterName);

		// Token: 0x06000724 RID: 1828
		public abstract Task<ODataWriter> CreateResourceSetWriterAsync(string parameterName);

		// Token: 0x06000725 RID: 1829
		public abstract void WriteEnd();

		// Token: 0x06000726 RID: 1830
		public abstract Task WriteEndAsync();

		// Token: 0x06000727 RID: 1831
		public abstract void Flush();

		// Token: 0x06000728 RID: 1832
		public abstract Task FlushAsync();
	}
}
