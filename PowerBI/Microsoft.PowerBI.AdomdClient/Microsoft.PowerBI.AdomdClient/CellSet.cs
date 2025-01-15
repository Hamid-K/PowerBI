using System;
using System.Collections;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200007B RID: 123
	public sealed class CellSet
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x000253F5 File Offset: 0x000235F5
		internal CellSet(MDDatasetFormatter formatter)
			: this(null, formatter)
		{
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000253FF File Offset: 0x000235FF
		internal CellSet(AdomdConnection connection, MDDatasetFormatter formatter)
		{
			this.datasetFormatter = formatter;
			this.connection = connection;
			this.cubeName = formatter.CubeName;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00025421 File Offset: 0x00023621
		internal MDDatasetFormatter Formatter
		{
			get
			{
				return this.datasetFormatter;
			}
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0002542C File Offset: 0x0002362C
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

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x000254CC File Offset: 0x000236CC
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

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x000254F4 File Offset: 0x000236F4
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

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00025546 File Offset: 0x00023746
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

		// Token: 0x17000214 RID: 532
		public Cell this[int index]
		{
			get
			{
				return this.Cells[index];
			}
		}

		// Token: 0x17000215 RID: 533
		public Cell this[int index1, int index2]
		{
			get
			{
				return this.Cells[index1, index2];
			}
		}

		// Token: 0x17000216 RID: 534
		public Cell this[params int[] indexes]
		{
			get
			{
				return this.Cells[indexes];
			}
		}

		// Token: 0x17000217 RID: 535
		public Cell this[ICollection indexes]
		{
			get
			{
				return this.Cells[indexes];
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0002559B File Offset: 0x0002379B
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

		// Token: 0x04000542 RID: 1346
		internal const int FilterAxisOrdinal = -1;

		// Token: 0x04000543 RID: 1347
		private MDDatasetFormatter datasetFormatter;

		// Token: 0x04000544 RID: 1348
		private AxisCollection axes;

		// Token: 0x04000545 RID: 1349
		private CellCollection cells;

		// Token: 0x04000546 RID: 1350
		private AdomdConnection connection;

		// Token: 0x04000547 RID: 1351
		private Axis filterAxis;

		// Token: 0x04000548 RID: 1352
		private string cubeName;

		// Token: 0x04000549 RID: 1353
		private OlapInfo olapInfo;
	}
}
