using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000087 RID: 135
	public class ChartTitle : ReportObject, INamedObject
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000184E4 File Offset: 0x000166E4
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x000184F7 File Offset: 0x000166F7
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

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00018506 File Offset: 0x00016706
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00018514 File Offset: 0x00016714
		public ReportExpression Caption
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00018528 File Offset: 0x00016728
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00018536 File Offset: 0x00016736
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0001854A File Offset: 0x0001674A
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x0001855D File Offset: 0x0001675D
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0001856C File Offset: 0x0001676C
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x0001857A File Offset: 0x0001677A
		[ReportExpressionDefaultValue(typeof(ChartPositions), ChartPositions.TopCenter)]
		public ReportExpression<ChartPositions> Position
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartPositions>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0001858E File Offset: 0x0001678E
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x000185A1 File Offset: 0x000167A1
		[DefaultValue("")]
		public string DockToChartArea
		{
			get
			{
				return (string)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x000185B0 File Offset: 0x000167B0
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x000185BE File Offset: 0x000167BE
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> DockOutsideChartArea
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x000185D2 File Offset: 0x000167D2
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x000185E0 File Offset: 0x000167E0
		[ReportExpressionDefaultValue(typeof(int), 0)]
		public ReportExpression<int> DockOffset
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x000185F4 File Offset: 0x000167F4
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x00018608 File Offset: 0x00016808
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

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00018618 File Offset: 0x00016818
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x00018627 File Offset: 0x00016827
		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0001863C File Offset: 0x0001683C
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x00018650 File Offset: 0x00016850
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00018660 File Offset: 0x00016860
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0001866F File Offset: 0x0001686F
		[ReportExpressionDefaultValue(typeof(TextOrientations), TextOrientations.Auto)]
		public ReportExpression<TextOrientations> TextOrientation
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<TextOrientations>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00018684 File Offset: 0x00016884
		public ChartTitle()
		{
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001868C File Offset: 0x0001688C
		internal ChartTitle(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00018695 File Offset: 0x00016895
		public override void Initialize()
		{
			base.Initialize();
			this.Position = ChartPositions.TopCenter;
		}

		// Token: 0x0200033D RID: 829
		internal class Definition : DefinitionStore<ChartTitle, ChartTitle.Definition.Properties>
		{
			// Token: 0x060017C0 RID: 6080 RVA: 0x0003AB23 File Offset: 0x00038D23
			private Definition()
			{
			}

			// Token: 0x0200045C RID: 1116
			internal enum Properties
			{
				// Token: 0x0400095E RID: 2398
				Name,
				// Token: 0x0400095F RID: 2399
				Caption,
				// Token: 0x04000960 RID: 2400
				CaptionLocID,
				// Token: 0x04000961 RID: 2401
				Hidden,
				// Token: 0x04000962 RID: 2402
				Style,
				// Token: 0x04000963 RID: 2403
				Position,
				// Token: 0x04000964 RID: 2404
				DockToChartArea,
				// Token: 0x04000965 RID: 2405
				DockOutsideChartArea,
				// Token: 0x04000966 RID: 2406
				DockOffset,
				// Token: 0x04000967 RID: 2407
				ChartElementPosition,
				// Token: 0x04000968 RID: 2408
				ToolTip,
				// Token: 0x04000969 RID: 2409
				ToolTipLocID,
				// Token: 0x0400096A RID: 2410
				ActionInfo,
				// Token: 0x0400096B RID: 2411
				TextOrientation,
				// Token: 0x0400096C RID: 2412
				PropertyCount
			}
		}
	}
}
