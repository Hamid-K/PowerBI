using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000205 RID: 517
	public class Paragraph : ReportElement
	{
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x00027EEF File Offset: 0x000260EF
		// (set) Token: 0x06001164 RID: 4452 RVA: 0x00027EFD File Offset: 0x000260FD
		[XmlElement(typeof(RdlCollection<TextRun>))]
		public IList<TextRun> TextRuns
		{
			get
			{
				return base.PropertyStore.GetObject<IList<TextRun>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00027F0C File Offset: 0x0002610C
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x00027F1A File Offset: 0x0002611A
		[ReportExpressionDefaultValueConstant(typeof(ReportSize), "DefaultZeroSize")]
		public ReportExpression<ReportSize> LeftIndent
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00027F2E File Offset: 0x0002612E
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x00027F3C File Offset: 0x0002613C
		[ReportExpressionDefaultValueConstant(typeof(ReportSize), "DefaultZeroSize")]
		public ReportExpression<ReportSize> RightIndent
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x00027F50 File Offset: 0x00026150
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x00027F5E File Offset: 0x0002615E
		[ReportExpressionDefaultValueConstant(typeof(ReportSize), "DefaultZeroSize")]
		public ReportExpression<ReportSize> HangingIndent
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x00027F72 File Offset: 0x00026172
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x00027F80 File Offset: 0x00026180
		[DefaultValue(ListStyle.None)]
		public ListStyle ListStyle
		{
			get
			{
				return (ListStyle)base.PropertyStore.GetInteger(5);
			}
			set
			{
				base.PropertyStore.SetInteger(5, (int)value);
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x00027F8F File Offset: 0x0002618F
		// (set) Token: 0x0600116E RID: 4462 RVA: 0x00027F9D File Offset: 0x0002619D
		[DefaultValue(0)]
		public int ListLevel
		{
			get
			{
				return base.PropertyStore.GetInteger(6);
			}
			set
			{
				base.PropertyStore.SetInteger(6, value);
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x00027FAC File Offset: 0x000261AC
		// (set) Token: 0x06001170 RID: 4464 RVA: 0x00027FBA File Offset: 0x000261BA
		[ReportExpressionDefaultValueConstant(typeof(ReportSize), "DefaultZeroSize")]
		public ReportExpression<ReportSize> SpaceBefore
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x00027FCE File Offset: 0x000261CE
		// (set) Token: 0x06001172 RID: 4466 RVA: 0x00027FDC File Offset: 0x000261DC
		[ReportExpressionDefaultValueConstant(typeof(ReportSize), "DefaultZeroSize")]
		public ReportExpression<ReportSize> SpaceAfter
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00027FF0 File Offset: 0x000261F0
		public Paragraph()
		{
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00027FF8 File Offset: 0x000261F8
		internal Paragraph(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00028001 File Offset: 0x00026201
		public override void Initialize()
		{
			base.Initialize();
			this.TextRuns = new RdlCollection<TextRun>();
			this.TextRuns.Add(new TextRun());
		}

		// Token: 0x0200040B RID: 1035
		internal new class Definition : DefinitionStore<Paragraph, Paragraph.Definition.Properties>
		{
			// Token: 0x060018E4 RID: 6372 RVA: 0x0003BF93 File Offset: 0x0003A193
			private Definition()
			{
			}

			// Token: 0x0200051A RID: 1306
			internal enum Properties
			{
				// Token: 0x0400112F RID: 4399
				Style,
				// Token: 0x04001130 RID: 4400
				TextRuns,
				// Token: 0x04001131 RID: 4401
				LeftIndent,
				// Token: 0x04001132 RID: 4402
				RightIndent,
				// Token: 0x04001133 RID: 4403
				HangingIndent,
				// Token: 0x04001134 RID: 4404
				ListStyle,
				// Token: 0x04001135 RID: 4405
				ListLevel,
				// Token: 0x04001136 RID: 4406
				SpaceBefore,
				// Token: 0x04001137 RID: 4407
				SpaceAfter
			}
		}
	}
}
