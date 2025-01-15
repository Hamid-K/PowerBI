using System;

namespace Microsoft.OData
{
	// Token: 0x0200007D RID: 125
	public abstract class ODataParameterReader
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004CB RID: 1227
		public abstract ODataParameterReaderState State { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004CC RID: 1228
		public abstract string Name { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004CD RID: 1229
		public abstract object Value { get; }

		// Token: 0x060004CE RID: 1230
		public abstract ODataReader CreateResourceReader();

		// Token: 0x060004CF RID: 1231
		public abstract ODataReader CreateResourceSetReader();

		// Token: 0x060004D0 RID: 1232
		public abstract ODataCollectionReader CreateCollectionReader();

		// Token: 0x060004D1 RID: 1233
		public abstract bool Read();
	}
}
