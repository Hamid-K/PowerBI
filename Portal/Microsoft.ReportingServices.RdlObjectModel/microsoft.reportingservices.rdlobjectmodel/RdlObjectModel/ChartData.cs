using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000092 RID: 146
	public class ChartData : DataRegionBody
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00019A77 File Offset: 0x00017C77
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00019A8A File Offset: 0x00017C8A
		[XmlElement(typeof(RdlCollection<ChartSeries>))]
		public IList<ChartSeries> ChartSeriesCollection
		{
			get
			{
				return (IList<ChartSeries>)base.PropertyStore.GetObject(0);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00019AA7 File Offset: 0x00017CA7
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x00019ABA File Offset: 0x00017CBA
		[XmlElement(typeof(RdlCollection<ChartDerivedSeries>))]
		public IList<ChartDerivedSeries> ChartDerivedSeriesCollection
		{
			get
			{
				return (IList<ChartDerivedSeries>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00019AC9 File Offset: 0x00017CC9
		public ChartData()
		{
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00019AD1 File Offset: 0x00017CD1
		internal ChartData(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00019ADA File Offset: 0x00017CDA
		public override void Initialize()
		{
			base.Initialize();
			this.ChartSeriesCollection = new RdlCollection<ChartSeries>();
			this.ChartDerivedSeriesCollection = new RdlCollection<ChartDerivedSeries>();
		}

		// Token: 0x02000348 RID: 840
		internal class Definition : DefinitionStore<ChartData, ChartData.Definition.Properties>
		{
			// Token: 0x060017CB RID: 6091 RVA: 0x0003AB7B File Offset: 0x00038D7B
			private Definition()
			{
			}

			// Token: 0x02000467 RID: 1127
			internal enum Properties
			{
				// Token: 0x04000A0B RID: 2571
				ChartSeriesCollection,
				// Token: 0x04000A0C RID: 2572
				ChartDerivedSeriesCollection,
				// Token: 0x04000A0D RID: 2573
				PropertyCount
			}
		}
	}
}
