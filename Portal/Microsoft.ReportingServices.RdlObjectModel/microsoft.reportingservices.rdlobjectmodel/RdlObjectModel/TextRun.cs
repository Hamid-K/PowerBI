using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000206 RID: 518
	public class TextRun : ReportElement
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00028024 File Offset: 0x00026224
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x00028032 File Offset: 0x00026232
		[DefaultValue("")]
		public string Label
		{
			get
			{
				return base.PropertyStore.GetObject<string>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00028041 File Offset: 0x00026241
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x0002804F File Offset: 0x0002624F
		public ReportExpression Value
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

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x00028063 File Offset: 0x00026263
		// (set) Token: 0x0600117B RID: 4475 RVA: 0x00028071 File Offset: 0x00026271
		public ActionInfo ActionInfo
		{
			get
			{
				return base.PropertyStore.GetObject<ActionInfo>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x00028080 File Offset: 0x00026280
		// (set) Token: 0x0600117D RID: 4477 RVA: 0x0002808E File Offset: 0x0002628E
		[ReportExpressionDefaultValue("")]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x000280A2 File Offset: 0x000262A2
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x000280B0 File Offset: 0x000262B0
		[ReportExpressionDefaultValue(typeof(MarkupType), Microsoft.ReportingServices.RdlObjectModel.MarkupType.None)]
		public ReportExpression<MarkupType> MarkupType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MarkupType>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x000280C4 File Offset: 0x000262C4
		public TextRun()
		{
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x000280CC File Offset: 0x000262CC
		internal TextRun(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x000280D8 File Offset: 0x000262D8
		public override void Initialize()
		{
			base.Initialize();
			this.Value = default(ReportExpression);
		}

		// Token: 0x0200040C RID: 1036
		internal new class Definition : DefinitionStore<TextRun, TextRun.Definition.Properties>
		{
			// Token: 0x060018E5 RID: 6373 RVA: 0x0003BF9B File Offset: 0x0003A19B
			private Definition()
			{
			}

			// Token: 0x0200051B RID: 1307
			internal enum Properties
			{
				// Token: 0x04001139 RID: 4409
				Style,
				// Token: 0x0400113A RID: 4410
				Label,
				// Token: 0x0400113B RID: 4411
				Value,
				// Token: 0x0400113C RID: 4412
				ValueLocID,
				// Token: 0x0400113D RID: 4413
				ActionInfo,
				// Token: 0x0400113E RID: 4414
				ToolTip,
				// Token: 0x0400113F RID: 4415
				ToolTipLocID,
				// Token: 0x04001140 RID: 4416
				MarkupType
			}
		}
	}
}
