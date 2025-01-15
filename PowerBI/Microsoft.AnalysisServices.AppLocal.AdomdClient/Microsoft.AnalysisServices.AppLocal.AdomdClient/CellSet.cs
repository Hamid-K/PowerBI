using System;
using System.Collections;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200007B RID: 123
	public sealed class CellSet
	{
		// Token: 0x060007B0 RID: 1968 RVA: 0x00025725 File Offset: 0x00023925
		internal CellSet(MDDatasetFormatter formatter)
			: this(null, formatter)
		{
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0002572F File Offset: 0x0002392F
		internal CellSet(AdomdConnection connection, MDDatasetFormatter formatter)
		{
			this.datasetFormatter = formatter;
			this.connection = connection;
			this.cubeName = formatter.CubeName;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00025751 File Offset: 0x00023951
		internal MDDatasetFormatter Formatter
		{
			get
			{
				return this.datasetFormatter;
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0002575C File Offset: 0x0002395C
		public static CellSet LoadXml(XmlReader xmlTextReader)
		{
			if (xmlTextReader == null)
			{
				throw new ArgumentNullException("xmlTextReader");
			}
			if (xmlTextReader.ReadState != ReadState.Initial && xmlTextReader.ReadState != ReadState.Interactive)
			{
				throw new ArgumentException(SR.CellSet_InvalidStateOfReader(xmlTextReader.ReadState.ToString()), "xmlTextReader");
			}
			CellSet cellSet;
			try
			{
				XmlReader xmlReader;
				if (xmlTextReader is XmlaReader)
				{
					((XmlaReader)xmlTextReader).SkipElements = true;
					xmlReader = xmlTextReader;
				}
				else
				{
					xmlReader = new SkippingWrapperReader(xmlTextReader);
				}
				cellSet = new CellSet(SoapFormatter.ReadDataSetResponse(xmlReader));
			}
			catch (XmlException ex)
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			return cellSet;
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000257FC File Offset: 0x000239FC
		public AxisCollection Axes
		{
			get
			{
				if (this.axes == null)
				{
					this.axes = new AxisCollection(this.connection, this, this.cubeName);
				}
				return this.axes;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00025824 File Offset: 0x00023A24
		public Axis FilterAxis
		{
			get
			{
				if (null == this.filterAxis && this.datasetFormatter.FilterAxis != null)
				{
					this.filterAxis = new Axis(this.connection, this.datasetFormatter.FilterAxis, this.cubeName, this, -1);
				}
				return this.filterAxis;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00025876 File Offset: 0x00023A76
		public CellCollection Cells
		{
			get
			{
				if (this.cells == null)
				{
					this.cells = new CellCollection(this);
				}
				return this.cells;
			}
		}

		// Token: 0x1700021A RID: 538
		public Cell this[int index]
		{
			get
			{
				return this.Cells[index];
			}
		}

		// Token: 0x1700021B RID: 539
		public Cell this[int index1, int index2]
		{
			get
			{
				return this.Cells[index1, index2];
			}
		}

		// Token: 0x1700021C RID: 540
		public Cell this[params int[] indexes]
		{
			get
			{
				return this.Cells[indexes];
			}
		}

		// Token: 0x1700021D RID: 541
		public Cell this[ICollection indexes]
		{
			get
			{
				return this.Cells[indexes];
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x000258CB File Offset: 0x00023ACB
		public OlapInfo OlapInfo
		{
			get
			{
				if (this.olapInfo == null)
				{
					this.olapInfo = new OlapInfo(this.Formatter);
				}
				return this.olapInfo;
			}
		}

		// Token: 0x0400054F RID: 1359
		internal const int FilterAxisOrdinal = -1;

		// Token: 0x04000550 RID: 1360
		private MDDatasetFormatter datasetFormatter;

		// Token: 0x04000551 RID: 1361
		private AxisCollection axes;

		// Token: 0x04000552 RID: 1362
		private CellCollection cells;

		// Token: 0x04000553 RID: 1363
		private AdomdConnection connection;

		// Token: 0x04000554 RID: 1364
		private Axis filterAxis;

		// Token: 0x04000555 RID: 1365
		private string cubeName;

		// Token: 0x04000556 RID: 1366
		private OlapInfo olapInfo;
	}
}
