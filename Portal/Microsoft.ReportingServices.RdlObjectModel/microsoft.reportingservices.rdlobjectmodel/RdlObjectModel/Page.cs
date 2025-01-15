using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001B1 RID: 433
	public class Page : ReportObject
	{
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x000233B8 File Offset: 0x000215B8
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x000233CB File Offset: 0x000215CB
		public PageSection PageHeader
		{
			get
			{
				return (PageSection)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x000233DA File Offset: 0x000215DA
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x000233ED File Offset: 0x000215ED
		public PageSection PageFooter
		{
			get
			{
				return (PageSection)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x000233FC File Offset: 0x000215FC
		// (set) Token: 0x06000E43 RID: 3651 RVA: 0x0002340A File Offset: 0x0002160A
		[DefaultValueConstant("DefaultPageHeight")]
		public ReportSize PageHeight
		{
			get
			{
				return base.PropertyStore.GetSize(2);
			}
			set
			{
				base.PropertyStore.SetSize(2, value);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x00023419 File Offset: 0x00021619
		// (set) Token: 0x06000E45 RID: 3653 RVA: 0x00023427 File Offset: 0x00021627
		[DefaultValueConstant("DefaultPageWidth")]
		public ReportSize PageWidth
		{
			get
			{
				return base.PropertyStore.GetSize(3);
			}
			set
			{
				base.PropertyStore.SetSize(3, value);
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00023436 File Offset: 0x00021636
		// (set) Token: 0x06000E47 RID: 3655 RVA: 0x00023444 File Offset: 0x00021644
		public ReportSize InteractiveHeight
		{
			get
			{
				return base.PropertyStore.GetSize(4);
			}
			set
			{
				base.PropertyStore.SetSize(4, value);
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x00023453 File Offset: 0x00021653
		// (set) Token: 0x06000E49 RID: 3657 RVA: 0x00023461 File Offset: 0x00021661
		public ReportSize InteractiveWidth
		{
			get
			{
				return base.PropertyStore.GetSize(5);
			}
			set
			{
				base.PropertyStore.SetSize(5, value);
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x00023470 File Offset: 0x00021670
		// (set) Token: 0x06000E4B RID: 3659 RVA: 0x0002347E File Offset: 0x0002167E
		[DefaultValueConstant("DefaultZeroSize")]
		public ReportSize LeftMargin
		{
			get
			{
				return base.PropertyStore.GetSize(6);
			}
			set
			{
				base.PropertyStore.SetSize(6, value);
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0002348D File Offset: 0x0002168D
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x0002349B File Offset: 0x0002169B
		[DefaultValueConstant("DefaultZeroSize")]
		public ReportSize RightMargin
		{
			get
			{
				return base.PropertyStore.GetSize(7);
			}
			set
			{
				base.PropertyStore.SetSize(7, value);
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x000234AA File Offset: 0x000216AA
		// (set) Token: 0x06000E4F RID: 3663 RVA: 0x000234B8 File Offset: 0x000216B8
		[DefaultValueConstant("DefaultZeroSize")]
		public ReportSize TopMargin
		{
			get
			{
				return base.PropertyStore.GetSize(8);
			}
			set
			{
				base.PropertyStore.SetSize(8, value);
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x000234C7 File Offset: 0x000216C7
		// (set) Token: 0x06000E51 RID: 3665 RVA: 0x000234D6 File Offset: 0x000216D6
		[DefaultValueConstant("DefaultZeroSize")]
		public ReportSize BottomMargin
		{
			get
			{
				return base.PropertyStore.GetSize(9);
			}
			set
			{
				base.PropertyStore.SetSize(9, value);
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x000234E6 File Offset: 0x000216E6
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x000234F5 File Offset: 0x000216F5
		[DefaultValue(1)]
		[ValidValues(1, 100)]
		public int Columns
		{
			get
			{
				return base.PropertyStore.GetInteger(10);
			}
			set
			{
				((IntProperty)DefinitionStore<Page, Page.Definition.Properties>.GetProperty(10)).Validate(this, value);
				base.PropertyStore.SetInteger(10, value);
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x00023518 File Offset: 0x00021718
		// (set) Token: 0x06000E55 RID: 3669 RVA: 0x00023527 File Offset: 0x00021727
		[DefaultValueConstant("DefaultColumnSpacing")]
		public ReportSize ColumnSpacing
		{
			get
			{
				return base.PropertyStore.GetSize(11);
			}
			set
			{
				base.PropertyStore.SetSize(11, value);
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00023537 File Offset: 0x00021737
		// (set) Token: 0x06000E57 RID: 3671 RVA: 0x0002354B File Offset: 0x0002174B
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0002355B File Offset: 0x0002175B
		public Page()
		{
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00023563 File Offset: 0x00021763
		internal Page(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0002356C File Offset: 0x0002176C
		public override void Initialize()
		{
			base.Initialize();
			this.Columns = 1;
			this.PageHeight = Constants.DefaultPageHeight;
			this.PageWidth = Constants.DefaultPageWidth;
			this.ColumnSpacing = Constants.DefaultColumnSpacing;
		}

		// Token: 0x020003E0 RID: 992
		internal class Definition : DefinitionStore<Page, Page.Definition.Properties>
		{
			// Token: 0x06001897 RID: 6295 RVA: 0x0003B9E1 File Offset: 0x00039BE1
			private Definition()
			{
			}

			// Token: 0x020004F4 RID: 1268
			internal enum Properties
			{
				// Token: 0x0400104A RID: 4170
				PageHeader,
				// Token: 0x0400104B RID: 4171
				PageFooter,
				// Token: 0x0400104C RID: 4172
				PageHeight,
				// Token: 0x0400104D RID: 4173
				PageWidth,
				// Token: 0x0400104E RID: 4174
				InteractiveHeight,
				// Token: 0x0400104F RID: 4175
				InteractiveWidth,
				// Token: 0x04001050 RID: 4176
				LeftMargin,
				// Token: 0x04001051 RID: 4177
				RightMargin,
				// Token: 0x04001052 RID: 4178
				TopMargin,
				// Token: 0x04001053 RID: 4179
				BottomMargin,
				// Token: 0x04001054 RID: 4180
				Columns,
				// Token: 0x04001055 RID: 4181
				ColumnSpacing,
				// Token: 0x04001056 RID: 4182
				Style
			}
		}
	}
}
