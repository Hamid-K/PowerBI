using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002939 RID: 10553
	[GeneratedCode("DomGen", "2.0")]
	internal class ApplicationVersion : OpenXmlLeafTextElement
	{
		// Token: 0x17006B26 RID: 27430
		// (get) Token: 0x06014E7A RID: 85626 RVA: 0x00318AE8 File Offset: 0x00316CE8
		public override string LocalName
		{
			get
			{
				return "AppVersion";
			}
		}

		// Token: 0x17006B27 RID: 27431
		// (get) Token: 0x06014E7B RID: 85627 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B28 RID: 27432
		// (get) Token: 0x06014E7C RID: 85628 RVA: 0x00318AEF File Offset: 0x00316CEF
		internal override int ElementTypeId
		{
			get
			{
				return 11024;
			}
		}

		// Token: 0x06014E7D RID: 85629 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014E7E RID: 85630 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ApplicationVersion()
		{
		}

		// Token: 0x06014E7F RID: 85631 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ApplicationVersion(string text)
			: base(text)
		{
		}

		// Token: 0x06014E80 RID: 85632 RVA: 0x00318AF8 File Offset: 0x00316CF8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014E81 RID: 85633 RVA: 0x00318B13 File Offset: 0x00316D13
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ApplicationVersion>(deep);
		}

		// Token: 0x04009085 RID: 36997
		private const string tagName = "AppVersion";

		// Token: 0x04009086 RID: 36998
		private const byte tagNsId = 3;

		// Token: 0x04009087 RID: 36999
		internal const int ElementTypeIdConst = 11024;
	}
}
