using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000111 RID: 273
	public sealed class DsrBuilder : BaseBindingBuilder<DataShapeResult, DataShapeResult>
	{
		// Token: 0x0600074A RID: 1866 RVA: 0x0000F15E File Offset: 0x0000D35E
		public DsrBuilder()
			: base(null)
		{
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000F167 File Offset: 0x0000D367
		public override DataShapeResult Build()
		{
			this._dataShapes.Add(new DataShape
			{
				Error = BaseBindingBuilder<DataShapeResult, DataShapeResult>.SafeBuild<ODataError, DsrBuilder>(this._dsrErrorBuilder)
			});
			return new DataShapeResult
			{
				DataShapes = this._dataShapes
			};
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000F19B File Offset: 0x0000D39B
		public DsrBuilder WithDataShape()
		{
			if (this._dataShapes == null)
			{
				this._dataShapes = new List<DataShape>();
			}
			return this;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000F1B1 File Offset: 0x0000D3B1
		public DsrErrorBuilder WithError()
		{
			this._dsrErrorBuilder = new DsrErrorBuilder(this);
			return this._dsrErrorBuilder;
		}

		// Token: 0x04000326 RID: 806
		private List<DataShape> _dataShapes;

		// Token: 0x04000327 RID: 807
		private DsrErrorBuilder _dsrErrorBuilder;
	}
}
