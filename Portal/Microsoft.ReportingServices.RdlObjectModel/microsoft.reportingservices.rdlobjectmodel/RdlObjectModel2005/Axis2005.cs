using System;
using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000003 RID: 3
	internal class Axis2005 : ChartAxis
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000022E7 File Offset: 0x000004E7
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000022F6 File Offset: 0x000004F6
		[DefaultValue(false)]
		public new bool Visible
		{
			get
			{
				return base.PropertyStore.GetBoolean(46);
			}
			set
			{
				base.PropertyStore.SetBoolean(46, value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002306 File Offset: 0x00000506
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000231A File Offset: 0x0000051A
		public new Style2005 Style
		{
			get
			{
				return (Style2005)base.PropertyStore.GetObject(47);
			}
			set
			{
				base.PropertyStore.SetObject(47, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000232A File Offset: 0x0000052A
		// (set) Token: 0x06000030 RID: 48 RVA: 0x0000233E File Offset: 0x0000053E
		public Title2005 Title
		{
			get
			{
				return (Title2005)base.PropertyStore.GetObject(48);
			}
			set
			{
				base.PropertyStore.SetObject(48, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000234E File Offset: 0x0000054E
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000235D File Offset: 0x0000055D
		[DefaultValue(false)]
		public new bool Margin
		{
			get
			{
				return base.PropertyStore.GetBoolean(49);
			}
			set
			{
				base.PropertyStore.SetBoolean(49, value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000236D File Offset: 0x0000056D
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000237C File Offset: 0x0000057C
		[DefaultValue(TickMarks2005.None)]
		public TickMarks2005 MajorTickMarks
		{
			get
			{
				return (TickMarks2005)base.PropertyStore.GetInteger(50);
			}
			set
			{
				base.PropertyStore.SetInteger(50, (int)value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000238C File Offset: 0x0000058C
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000239B File Offset: 0x0000059B
		[DefaultValue(TickMarks2005.None)]
		public TickMarks2005 MinorTickMarks
		{
			get
			{
				return (TickMarks2005)base.PropertyStore.GetInteger(51);
			}
			set
			{
				base.PropertyStore.SetInteger(51, (int)value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000023AB File Offset: 0x000005AB
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000023BF File Offset: 0x000005BF
		public GridLines2005 MajorGridLines
		{
			get
			{
				return (GridLines2005)base.PropertyStore.GetObject(52);
			}
			set
			{
				base.PropertyStore.SetObject(52, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000023CF File Offset: 0x000005CF
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000023E3 File Offset: 0x000005E3
		public GridLines2005 MinorGridLines
		{
			get
			{
				return (GridLines2005)base.PropertyStore.GetObject(53);
			}
			set
			{
				base.PropertyStore.SetObject(53, value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000023F3 File Offset: 0x000005F3
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002402 File Offset: 0x00000602
		[ReportExpressionDefaultValue]
		public ReportExpression MajorInterval
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(54);
			}
			set
			{
				base.PropertyStore.SetObject(54, value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002417 File Offset: 0x00000617
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002426 File Offset: 0x00000626
		[ReportExpressionDefaultValue]
		public ReportExpression MinorInterval
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(55);
			}
			set
			{
				base.PropertyStore.SetObject(55, value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000243B File Offset: 0x0000063B
		// (set) Token: 0x06000040 RID: 64 RVA: 0x0000244A File Offset: 0x0000064A
		[DefaultValue(false)]
		public new bool Reverse
		{
			get
			{
				return base.PropertyStore.GetBoolean(56);
			}
			set
			{
				base.PropertyStore.SetBoolean(56, value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000245A File Offset: 0x0000065A
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002469 File Offset: 0x00000669
		[DefaultValue(false)]
		public new bool Interlaced
		{
			get
			{
				return base.PropertyStore.GetBoolean(58);
			}
			set
			{
				base.PropertyStore.SetBoolean(58, value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002479 File Offset: 0x00000679
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002488 File Offset: 0x00000688
		[ReportExpressionDefaultValue]
		public ReportExpression Min
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(59);
			}
			set
			{
				base.PropertyStore.SetObject(59, value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000249D File Offset: 0x0000069D
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000024AC File Offset: 0x000006AC
		[ReportExpressionDefaultValue]
		public ReportExpression Max
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(60);
			}
			set
			{
				base.PropertyStore.SetObject(60, value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000024C1 File Offset: 0x000006C1
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000024D0 File Offset: 0x000006D0
		[DefaultValue(false)]
		public new bool LogScale
		{
			get
			{
				return base.PropertyStore.GetBoolean(61);
			}
			set
			{
				base.PropertyStore.SetBoolean(61, value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000024E0 File Offset: 0x000006E0
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000024F4 File Offset: 0x000006F4
		[DefaultValue("")]
		public new string Angle
		{
			get
			{
				return (string)base.PropertyStore.GetObject(62);
			}
			set
			{
				base.PropertyStore.SetObject(62, value);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002504 File Offset: 0x00000704
		public Axis2005()
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000250C File Offset: 0x0000070C
		public Axis2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002515 File Offset: 0x00000715
		public override void Initialize()
		{
			base.CustomProperties = new RdlCollection<CustomProperty>();
			this.MajorTickMarks = TickMarks2005.None;
			this.MinorTickMarks = TickMarks2005.None;
		}

		// Token: 0x020002EC RID: 748
		internal new class Definition : DefinitionStore<Axis2005, Axis2005.Definition.Properties>
		{
			// Token: 0x060016E8 RID: 5864 RVA: 0x000363DA File Offset: 0x000345DA
			private Definition()
			{
			}

			// Token: 0x02000420 RID: 1056
			public enum Properties
			{
				// Token: 0x04000804 RID: 2052
				Visible = 46,
				// Token: 0x04000805 RID: 2053
				Style,
				// Token: 0x04000806 RID: 2054
				Title,
				// Token: 0x04000807 RID: 2055
				Margin,
				// Token: 0x04000808 RID: 2056
				MajorTickMarks,
				// Token: 0x04000809 RID: 2057
				MinorTickMarks,
				// Token: 0x0400080A RID: 2058
				MajorGridLines,
				// Token: 0x0400080B RID: 2059
				MinorGridLines,
				// Token: 0x0400080C RID: 2060
				MajorInterval,
				// Token: 0x0400080D RID: 2061
				MinorInterval,
				// Token: 0x0400080E RID: 2062
				Reverse,
				// Token: 0x0400080F RID: 2063
				Location,
				// Token: 0x04000810 RID: 2064
				Interlaced,
				// Token: 0x04000811 RID: 2065
				Min,
				// Token: 0x04000812 RID: 2066
				Max,
				// Token: 0x04000813 RID: 2067
				LogScale,
				// Token: 0x04000814 RID: 2068
				Angle
			}
		}
	}
}
