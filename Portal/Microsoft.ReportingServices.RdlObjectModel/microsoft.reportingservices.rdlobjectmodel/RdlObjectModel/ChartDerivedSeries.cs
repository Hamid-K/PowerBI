using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000098 RID: 152
	public class ChartDerivedSeries : ReportObject
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001A20B File Offset: 0x0001840B
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0001A21E File Offset: 0x0001841E
		public ChartSeries ChartSeries
		{
			get
			{
				return (ChartSeries)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001A22D File Offset: 0x0001842D
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x0001A240 File Offset: 0x00018440
		public string SourceChartSeriesName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001A24F File Offset: 0x0001844F
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x0001A25D File Offset: 0x0001845D
		public ChartFormulas DerivedSeriesFormula
		{
			get
			{
				return (ChartFormulas)base.PropertyStore.GetInteger(2);
			}
			set
			{
				base.PropertyStore.SetInteger(2, (int)value);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001A26C File Offset: 0x0001846C
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x0001A27F File Offset: 0x0001847F
		[XmlElement(typeof(RdlCollection<ChartFormulaParameter>))]
		public IList<ChartFormulaParameter> ChartFormulaParameters
		{
			get
			{
				return (IList<ChartFormulaParameter>)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001A28E File Offset: 0x0001848E
		public ChartDerivedSeries()
		{
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001A296 File Offset: 0x00018496
		internal ChartDerivedSeries(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001A29F File Offset: 0x0001849F
		public override void Initialize()
		{
			base.Initialize();
			this.ChartSeries = new ChartSeries();
			this.ChartFormulaParameters = new RdlCollection<ChartFormulaParameter>();
		}

		// Token: 0x0200034E RID: 846
		internal class Definition : DefinitionStore<ChartDerivedSeries, ChartDerivedSeries.Definition.Properties>
		{
			// Token: 0x060017D1 RID: 6097 RVA: 0x0003ABAB File Offset: 0x00038DAB
			private Definition()
			{
			}

			// Token: 0x0200046D RID: 1133
			internal enum Properties
			{
				// Token: 0x04000A48 RID: 2632
				ChartSeries,
				// Token: 0x04000A49 RID: 2633
				SourceChartSeriesName,
				// Token: 0x04000A4A RID: 2634
				DerivedSeriesFormula,
				// Token: 0x04000A4B RID: 2635
				ChartFormulaParameters,
				// Token: 0x04000A4C RID: 2636
				PropertyCount
			}
		}
	}
}
