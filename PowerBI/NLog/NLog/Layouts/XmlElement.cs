using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.Layouts
{
	// Token: 0x020000AD RID: 173
	[NLogConfigurationItem]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public class XmlElement : XmlElementBase
	{
		// Token: 0x06000B28 RID: 2856 RVA: 0x0001CEEA File Offset: 0x0001B0EA
		public XmlElement()
			: this("item", null)
		{
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0001CEF8 File Offset: 0x0001B0F8
		public XmlElement(string elementName, Layout elementValue)
			: base(elementName, elementValue)
		{
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0001CF02 File Offset: 0x0001B102
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x0001CF0A File Offset: 0x0001B10A
		[DefaultValue("item")]
		public string Name
		{
			get
			{
				return base.ElementNameInternal;
			}
			set
			{
				base.ElementNameInternal = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0001CF13 File Offset: 0x0001B113
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x0001CF1B File Offset: 0x0001B11B
		public Layout Value
		{
			get
			{
				return base.ElementValueInternal;
			}
			set
			{
				base.ElementValueInternal = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0001CF24 File Offset: 0x0001B124
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x0001CF2C File Offset: 0x0001B12C
		[DefaultValue(true)]
		public bool Encode
		{
			get
			{
				return base.ElementEncodeInternal;
			}
			set
			{
				base.ElementEncodeInternal = value;
			}
		}

		// Token: 0x040002A0 RID: 672
		private const string DefaultElementName = "item";
	}
}
