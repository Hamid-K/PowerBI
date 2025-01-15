using System;
using Microsoft.Data.Metadata.Edm;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200024E RID: 590
	internal sealed class RowType : StructuralType
	{
		// Token: 0x060019E4 RID: 6628 RVA: 0x00047651 File Offset: 0x00045851
		private RowType(RowType rowType)
		{
			this._rowType = ArgumentValidation.CheckNotNull<RowType>(rowType, "rowType");
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0004766A File Offset: 0x0004586A
		internal sealed override StructuralType InternalStructuralType
		{
			get
			{
				return this._rowType;
			}
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00047672 File Offset: 0x00045872
		internal static RowType Create(RowType rowType)
		{
			RowType rowType2 = new RowType(rowType);
			rowType2.InternalInit();
			return rowType2;
		}

		// Token: 0x04000E6E RID: 3694
		private readonly RowType _rowType;
	}
}
