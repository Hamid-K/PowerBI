using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000197 RID: 407
	public abstract class ODataParameterWriter
	{
		// Token: 0x06000B9C RID: 2972
		public abstract void WriteStart();

		// Token: 0x06000B9D RID: 2973
		public abstract void WriteValue(string parameterName, object parameterValue);

		// Token: 0x06000B9E RID: 2974
		public abstract ODataCollectionWriter CreateCollectionWriter(string parameterName);

		// Token: 0x06000B9F RID: 2975
		public abstract void WriteEnd();

		// Token: 0x06000BA0 RID: 2976
		public abstract void Flush();
	}
}
