using System;
using System.ComponentModel;
using NLog.Config;
using NLog.Internal;
using NLog.LayoutRenderers.Wrappers;

namespace NLog.Layouts
{
	// Token: 0x020000AC RID: 172
	[NLogConfigurationItem]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public class XmlAttribute
	{
		// Token: 0x06000B1D RID: 2845 RVA: 0x0001CE30 File Offset: 0x0001B030
		public XmlAttribute()
			: this(null, null, true)
		{
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0001CE3B File Offset: 0x0001B03B
		public XmlAttribute(string name, Layout layout)
			: this(name, layout, true)
		{
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0001CE46 File Offset: 0x0001B046
		public XmlAttribute(string name, Layout layout, bool encode)
		{
			this.Name = name;
			this.Layout = layout;
			this.Encode = encode;
			this.IncludeEmptyValue = false;
			this.LayoutWrapper.XmlEncodeNewlines = true;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0001CE81 File Offset: 0x0001B081
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x0001CE89 File Offset: 0x0001B089
		[RequiredParameter]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = XmlHelper.XmlConvertToElementName((value != null) ? value.Trim() : null, true);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0001CEA3 File Offset: 0x0001B0A3
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x0001CEB0 File Offset: 0x0001B0B0
		[RequiredParameter]
		public Layout Layout
		{
			get
			{
				return this.LayoutWrapper.Inner;
			}
			set
			{
				this.LayoutWrapper.Inner = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0001CEBE File Offset: 0x0001B0BE
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0001CECB File Offset: 0x0001B0CB
		[DefaultValue(true)]
		public bool Encode
		{
			get
			{
				return this.LayoutWrapper.XmlEncode;
			}
			set
			{
				this.LayoutWrapper.XmlEncode = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0001CED9 File Offset: 0x0001B0D9
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x0001CEE1 File Offset: 0x0001B0E1
		public bool IncludeEmptyValue { get; set; }

		// Token: 0x0400029D RID: 669
		private string _name;

		// Token: 0x0400029F RID: 671
		internal readonly XmlEncodeLayoutRendererWrapper LayoutWrapper = new XmlEncodeLayoutRendererWrapper();
	}
}
