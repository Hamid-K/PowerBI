using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000025 RID: 37
	internal sealed class DataIntersection : Scope
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00004F8B File Offset: 0x0000318B
		internal DataIntersection(string id, IList<DataShape> dataShapes, IList<Calculation> calculations, DataBinding dataBinding)
			: base(id)
		{
			this._dataShapes = dataShapes;
			this._calculations = calculations;
			this._dataBinding = dataBinding;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004FAA File Offset: 0x000031AA
		internal IList<DataShape> DataShapes
		{
			get
			{
				return this._dataShapes;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004FB2 File Offset: 0x000031B2
		internal IList<Calculation> Calculations
		{
			get
			{
				return this._calculations;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004FBA File Offset: 0x000031BA
		internal DataBinding DataBinding
		{
			get
			{
				return this._dataBinding;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00004FC2 File Offset: 0x000031C2
		internal bool IsEmpty
		{
			get
			{
				return this._dataShapes.IsNullOrEmpty<DataShape>() && this._calculations.IsNullOrEmpty<Calculation>();
			}
		}

		// Token: 0x040000AF RID: 175
		private readonly IList<DataShape> _dataShapes;

		// Token: 0x040000B0 RID: 176
		private readonly IList<Calculation> _calculations;

		// Token: 0x040000B1 RID: 177
		private readonly DataBinding _dataBinding;
	}
}
