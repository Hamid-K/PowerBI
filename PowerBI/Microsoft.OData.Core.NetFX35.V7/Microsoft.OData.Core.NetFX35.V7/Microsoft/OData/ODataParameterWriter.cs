using System;

namespace Microsoft.OData
{
	// Token: 0x02000081 RID: 129
	public abstract class ODataParameterWriter
	{
		// Token: 0x060004F0 RID: 1264
		public abstract void WriteStart();

		// Token: 0x060004F1 RID: 1265
		public abstract void WriteValue(string parameterName, object parameterValue);

		// Token: 0x060004F2 RID: 1266
		public abstract ODataCollectionWriter CreateCollectionWriter(string parameterName);

		// Token: 0x060004F3 RID: 1267
		public abstract ODataWriter CreateResourceWriter(string parameterName);

		// Token: 0x060004F4 RID: 1268
		public abstract ODataWriter CreateResourceSetWriter(string parameterName);

		// Token: 0x060004F5 RID: 1269
		public abstract void WriteEnd();

		// Token: 0x060004F6 RID: 1270
		public abstract void Flush();
	}
}
