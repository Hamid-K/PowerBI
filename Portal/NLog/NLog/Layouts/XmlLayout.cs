using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.Layouts
{
	// Token: 0x020000AF RID: 175
	[Layout("XmlLayout")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class XmlLayout : XmlElementBase
	{
		// Token: 0x06000B6A RID: 2922 RVA: 0x0001DE96 File Offset: 0x0001C096
		public XmlLayout()
			: this("logevent", null)
		{
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0001DEA4 File Offset: 0x0001C0A4
		public XmlLayout(string elementName, Layout elementValue)
			: base(elementName, elementValue)
		{
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0001DEAE File Offset: 0x0001C0AE
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x0001DEB6 File Offset: 0x0001C0B6
		[DefaultValue("logevent")]
		public string ElementName
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

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0001DEBF File Offset: 0x0001C0BF
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x0001DEC7 File Offset: 0x0001C0C7
		public Layout ElementValue
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

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0001DED0 File Offset: 0x0001C0D0
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x0001DED8 File Offset: 0x0001C0D8
		[DefaultValue(true)]
		public bool ElementEncode
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

		// Token: 0x040002B7 RID: 695
		private const string DefaultRootElementName = "logevent";
	}
}
