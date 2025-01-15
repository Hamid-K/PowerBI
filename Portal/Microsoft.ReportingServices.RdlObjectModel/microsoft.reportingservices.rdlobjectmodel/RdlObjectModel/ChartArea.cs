using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000082 RID: 130
	public class ChartArea : ReportObject, INamedObject
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000180D1 File Offset: 0x000162D1
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x000180E4 File Offset: 0x000162E4
		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000180F3 File Offset: 0x000162F3
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x00018101 File Offset: 0x00016301
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00018115 File Offset: 0x00016315
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00018128 File Offset: 0x00016328
		[XmlElement(typeof(RdlCollection<ChartAxis>))]
		public IList<ChartAxis> ChartCategoryAxes
		{
			get
			{
				return (IList<ChartAxis>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00018137 File Offset: 0x00016337
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0001814A File Offset: 0x0001634A
		[XmlElement(typeof(RdlCollection<ChartAxis>))]
		public IList<ChartAxis> ChartValueAxes
		{
			get
			{
				return (IList<ChartAxis>)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00018159 File Offset: 0x00016359
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0001816C File Offset: 0x0001636C
		public ChartThreeDProperties ChartThreeDProperties
		{
			get
			{
				return (ChartThreeDProperties)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0001817B File Offset: 0x0001637B
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0001818E File Offset: 0x0001638E
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0001819D File Offset: 0x0001639D
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x000181AB File Offset: 0x000163AB
		[ReportExpressionDefaultValue(typeof(ChartAlignOrientations), ChartAlignOrientations.None)]
		public ReportExpression<ChartAlignOrientations> AlignOrientation
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartAlignOrientations>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x000181BF File Offset: 0x000163BF
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x000181D2 File Offset: 0x000163D2
		public ChartAlignType ChartAlignType
		{
			get
			{
				return (ChartAlignType)base.PropertyStore.GetObject(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000181E1 File Offset: 0x000163E1
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x000181F4 File Offset: 0x000163F4
		[DefaultValue("")]
		public string AlignWithChartArea
		{
			get
			{
				return (string)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00018203 File Offset: 0x00016403
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00018217 File Offset: 0x00016417
		public ChartElementPosition ChartElementPosition
		{
			get
			{
				return (ChartElementPosition)base.PropertyStore.GetObject(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00018227 File Offset: 0x00016427
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0001823B File Offset: 0x0001643B
		public ChartElementPosition ChartInnerPlotPosition
		{
			get
			{
				return (ChartElementPosition)base.PropertyStore.GetObject(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0001824B File Offset: 0x0001644B
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0001825A File Offset: 0x0001645A
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> EquallySizedAxesFont
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001826F File Offset: 0x0001646F
		public ChartArea()
		{
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00018277 File Offset: 0x00016477
		internal ChartArea(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00018280 File Offset: 0x00016480
		public override void Initialize()
		{
			base.Initialize();
			this.ChartCategoryAxes = new RdlCollection<ChartAxis>();
			this.ChartValueAxes = new RdlCollection<ChartAxis>();
		}

		// Token: 0x02000335 RID: 821
		internal class Definition : DefinitionStore<ChartArea, ChartArea.Definition.Properties>
		{
			// Token: 0x060017A0 RID: 6048 RVA: 0x0003A694 File Offset: 0x00038894
			private Definition()
			{
			}

			// Token: 0x02000457 RID: 1111
			internal enum Properties
			{
				// Token: 0x0400093C RID: 2364
				Name,
				// Token: 0x0400093D RID: 2365
				Hidden,
				// Token: 0x0400093E RID: 2366
				ChartCategoryAxes,
				// Token: 0x0400093F RID: 2367
				ChartValueAxes,
				// Token: 0x04000940 RID: 2368
				ChartThreeDProperties,
				// Token: 0x04000941 RID: 2369
				Style,
				// Token: 0x04000942 RID: 2370
				AlignOrientation,
				// Token: 0x04000943 RID: 2371
				ChartAlignType,
				// Token: 0x04000944 RID: 2372
				AlignWithChartArea,
				// Token: 0x04000945 RID: 2373
				ChartElementPosition,
				// Token: 0x04000946 RID: 2374
				ChartInnerPlotPosition,
				// Token: 0x04000947 RID: 2375
				EquallySizedAxesFont,
				// Token: 0x04000948 RID: 2376
				PropertyCount
			}
		}
	}
}
