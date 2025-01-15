using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200015C RID: 348
	public class BaseGaugeImage : ReportObject
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0001F5EF File Offset: 0x0001D7EF
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x0001F5FD File Offset: 0x0001D7FD
		public ReportExpression<SourceType> Source
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<SourceType>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x0001F611 File Offset: 0x0001D811
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x0001F61F File Offset: 0x0001D81F
		public ReportExpression Value
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

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0001F633 File Offset: 0x0001D833
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x0001F641 File Offset: 0x0001D841
		[ReportExpressionDefaultValue]
		public ReportExpression MIMEType
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

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0001F655 File Offset: 0x0001D855
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x0001F663 File Offset: 0x0001D863
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> TransparentColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0001F677 File Offset: 0x0001D877
		public BaseGaugeImage()
		{
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0001F67F File Offset: 0x0001D87F
		internal BaseGaugeImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001F688 File Offset: 0x0001D888
		protected override void GetDependenciesCore(IList<ReportObject> dependencies)
		{
			base.GetDependenciesCore(dependencies);
			Image.GetEmbeddedImgDependencies(base.GetAncestor<Report>(), dependencies, this.Source.Value, this.Value);
		}

		// Token: 0x0200038D RID: 909
		internal class Definition : DefinitionStore<BaseGaugeImage, BaseGaugeImage.Definition.Properties>
		{
			// Token: 0x06001830 RID: 6192 RVA: 0x0003B4AB File Offset: 0x000396AB
			private Definition()
			{
			}

			// Token: 0x020004A6 RID: 1190
			internal enum Properties
			{
				// Token: 0x04000D32 RID: 3378
				Source,
				// Token: 0x04000D33 RID: 3379
				Value,
				// Token: 0x04000D34 RID: 3380
				MIMEType,
				// Token: 0x04000D35 RID: 3381
				TransparentColor,
				// Token: 0x04000D36 RID: 3382
				PropertyCount
			}
		}
	}
}
