using System;
using System.Collections;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003F3 RID: 1011
	public sealed class MatrixRow : RowBase
	{
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x0007F21B File Offset: 0x0007D41B
		[XmlArrayItem("MatrixCell", typeof(MatrixCell))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ArrayList MatrixCells
		{
			get
			{
				return base.Cells;
			}
		}
	}
}
