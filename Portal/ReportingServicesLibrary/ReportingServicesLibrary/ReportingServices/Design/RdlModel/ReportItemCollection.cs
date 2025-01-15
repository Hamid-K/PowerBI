using System;
using System.Collections;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000402 RID: 1026
	public sealed class ReportItemCollection : ArrayList
	{
		// Token: 0x17000950 RID: 2384
		[XmlArrayItem("Textbox", typeof(TextboxItem))]
		[XmlArrayItem("Rectangle", typeof(RectangleItem))]
		[XmlArrayItem("List", typeof(ListItem))]
		[XmlArrayItem("Table", typeof(TableItem))]
		[XmlArrayItem("Image", typeof(Image))]
		[XmlArrayItem("Line", typeof(LineItem))]
		[XmlArrayItem("Subreport", typeof(SubreportItem))]
		[XmlArrayItem("Chart", typeof(ChartItem))]
		[XmlArrayItem("Matrix", typeof(MatrixItem))]
		public ReportItem this[int index]
		{
			get
			{
				return (ReportItem)base[index];
			}
		}
	}
}
