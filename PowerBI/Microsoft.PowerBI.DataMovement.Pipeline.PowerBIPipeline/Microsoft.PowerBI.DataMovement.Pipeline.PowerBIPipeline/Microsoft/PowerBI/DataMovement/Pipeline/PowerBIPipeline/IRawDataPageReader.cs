using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x02000019 RID: 25
	public interface IRawDataPageReader : IPageReader, IDisposable
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000092 RID: 146
		[global::System.Runtime.CompilerServices.Nullable(1)]
		IEnumerable<DataTable> SchemaTables
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}
	}
}
