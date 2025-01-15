using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001DE RID: 478
	public class Action : ReportObject
	{
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00026074 File Offset: 0x00024274
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x00026082 File Offset: 0x00024282
		[ReportExpressionDefaultValue]
		public ReportExpression Hyperlink
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00026096 File Offset: 0x00024296
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x000260A9 File Offset: 0x000242A9
		public Drillthrough Drillthrough
		{
			get
			{
				return (Drillthrough)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x000260B8 File Offset: 0x000242B8
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x000260C6 File Offset: 0x000242C6
		[ReportExpressionDefaultValue]
		public ReportExpression BookmarkLink
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000260DA File Offset: 0x000242DA
		public Action()
		{
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x000260E2 File Offset: 0x000242E2
		internal Action(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020003EC RID: 1004
		internal class Definition : DefinitionStore<Action, Action.Definition.Properties>
		{
			// Token: 0x060018AE RID: 6318 RVA: 0x0003BB2F File Offset: 0x00039D2F
			private Definition()
			{
			}

			// Token: 0x020004FE RID: 1278
			internal enum Properties
			{
				// Token: 0x040010A2 RID: 4258
				Hyperlink,
				// Token: 0x040010A3 RID: 4259
				Drillthrough,
				// Token: 0x040010A4 RID: 4260
				BookmarkLink
			}
		}
	}
}
