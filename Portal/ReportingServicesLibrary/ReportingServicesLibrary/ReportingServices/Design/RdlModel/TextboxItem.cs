using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000425 RID: 1061
	public sealed class TextboxItem : ReportItem
	{
		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x000812F2 File Offset: 0x0007F4F2
		// (set) Token: 0x060021B3 RID: 8627 RVA: 0x000812FA File Offset: 0x0007F4FA
		public string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x00081303 File Offset: 0x0007F503
		// (set) Token: 0x060021B5 RID: 8629 RVA: 0x0008130B File Offset: 0x0007F50B
		[XmlParentElement("ToggleImage")]
		[XmlElement("InitialState")]
		public ToggleImageState InitialToggleState
		{
			get
			{
				return this.m_toggleImage;
			}
			set
			{
				this.m_toggleImage = value;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x00081314 File Offset: 0x0007F514
		// (set) Token: 0x060021B7 RID: 8631 RVA: 0x0008131C File Offset: 0x0007F51C
		[DefaultValue(false)]
		public bool CanGrow
		{
			get
			{
				return this.m_canGrow;
			}
			set
			{
				this.m_canGrow = value;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x00081325 File Offset: 0x0007F525
		// (set) Token: 0x060021B9 RID: 8633 RVA: 0x0008132D File Offset: 0x0007F52D
		[DefaultValue(false)]
		public bool CanShrink
		{
			get
			{
				return this.m_canShrink;
			}
			set
			{
				this.m_canShrink = value;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x00081336 File Offset: 0x0007F536
		// (set) Token: 0x060021BB RID: 8635 RVA: 0x0008133E File Offset: 0x0007F53E
		[DefaultValue("")]
		public string HideDuplicates
		{
			get
			{
				return this.m_hideDuplicates;
			}
			set
			{
				this.m_hideDuplicates = value;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060021BC RID: 8636 RVA: 0x00081347 File Offset: 0x0007F547
		// (set) Token: 0x060021BD RID: 8637 RVA: 0x0008134F File Offset: 0x0007F54F
		[DefaultValue("")]
		public Action Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x00081358 File Offset: 0x0007F558
		// (set) Token: 0x060021BF RID: 8639 RVA: 0x00081360 File Offset: 0x0007F560
		[DefaultValue(TextboxDataElementStyles.Auto)]
		public TextboxDataElementStyles DataElementStyle
		{
			get
			{
				return this.m_dataElementStyle;
			}
			set
			{
				this.m_dataElementStyle = value;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x00081369 File Offset: 0x0007F569
		// (set) Token: 0x060021C1 RID: 8641 RVA: 0x00081371 File Offset: 0x0007F571
		public UserSort UserSort
		{
			get
			{
				return this.m_userSort;
			}
			set
			{
				this.m_userSort = value;
			}
		}

		// Token: 0x04000ECA RID: 3786
		private string m_value = "";

		// Token: 0x04000ECB RID: 3787
		private Action m_action;

		// Token: 0x04000ECC RID: 3788
		private bool m_canGrow;

		// Token: 0x04000ECD RID: 3789
		private bool m_canShrink;

		// Token: 0x04000ECE RID: 3790
		private string m_hideDuplicates;

		// Token: 0x04000ECF RID: 3791
		private ToggleImageState m_toggleImage;

		// Token: 0x04000ED0 RID: 3792
		private TextboxDataElementStyles m_dataElementStyle;

		// Token: 0x04000ED1 RID: 3793
		private UserSort m_userSort;
	}
}
