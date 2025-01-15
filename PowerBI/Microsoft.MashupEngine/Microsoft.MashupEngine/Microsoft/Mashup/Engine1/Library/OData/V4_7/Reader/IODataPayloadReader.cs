using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x02000790 RID: 1936
	internal interface IODataPayloadReader : IDisposable
	{
		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x060038C3 RID: 14531
		bool IsNull { get; }

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x060038C4 RID: 14532
		string ContextUrl { get; }

		// Token: 0x060038C5 RID: 14533
		ODataReaderWithResponse ToResourceReader(bool isResourceSet);

		// Token: 0x060038C6 RID: 14534
		IODataPayloadReader TransferOwnership();

		// Token: 0x060038C7 RID: 14535
		ODataServiceDocument ReadServiceDocument();

		// Token: 0x060038C8 RID: 14536
		Microsoft.OData.Edm.IEdmModel ReadMetadataDocument();

		// Token: 0x060038C9 RID: 14537
		IEnumerable<object> ReadCollection();

		// Token: 0x060038CA RID: 14538
		ODataError ReadError();

		// Token: 0x060038CB RID: 14539
		ODataPropertyWrapper ReadProperty();

		// Token: 0x060038CC RID: 14540
		object ReadPrimitive(TypeValue expectedType);
	}
}
