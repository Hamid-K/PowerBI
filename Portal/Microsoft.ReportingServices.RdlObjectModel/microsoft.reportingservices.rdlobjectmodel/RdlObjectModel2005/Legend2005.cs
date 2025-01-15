using System;
using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200000D RID: 13
	internal class Legend2005 : ChartLegend
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000297C File Offset: 0x00000B7C
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000298B File Offset: 0x00000B8B
		[DefaultValue(false)]
		public bool Visible
		{
			get
			{
				return base.PropertyStore.GetBoolean(24);
			}
			set
			{
				base.PropertyStore.SetBoolean(24, value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000299B File Offset: 0x00000B9B
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000029AF File Offset: 0x00000BAF
		public new Style2005 Style
		{
			get
			{
				return (Style2005)base.PropertyStore.GetObject(25);
			}
			set
			{
				base.PropertyStore.SetObject(25, value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000029BF File Offset: 0x00000BBF
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000029CE File Offset: 0x00000BCE
		[DefaultValue(LegendLayouts2005.Column)]
		public new LegendLayouts2005 Layout
		{
			get
			{
				return (LegendLayouts2005)base.PropertyStore.GetInteger(27);
			}
			set
			{
				base.PropertyStore.SetInteger(27, (int)value);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000029DE File Offset: 0x00000BDE
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000029ED File Offset: 0x00000BED
		[DefaultValue(false)]
		public bool InsidePlotArea
		{
			get
			{
				return base.PropertyStore.GetBoolean(28);
			}
			set
			{
				base.PropertyStore.SetBoolean(28, value);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000029FD File Offset: 0x00000BFD
		public Legend2005()
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002A05 File Offset: 0x00000C05
		public Legend2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020002F6 RID: 758
		internal new class Definition : DefinitionStore<Legend2005, Legend2005.Definition.Properties>
		{
			// Token: 0x060016F2 RID: 5874 RVA: 0x0003642A File Offset: 0x0003462A
			private Definition()
			{
			}

			// Token: 0x0200042A RID: 1066
			public enum Properties
			{
				// Token: 0x04000837 RID: 2103
				Visible = 24,
				// Token: 0x04000838 RID: 2104
				Style,
				// Token: 0x04000839 RID: 2105
				Position,
				// Token: 0x0400083A RID: 2106
				Layout,
				// Token: 0x0400083B RID: 2107
				InsidePlotArea
			}
		}
	}
}
