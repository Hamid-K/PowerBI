using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002945 RID: 10565
	[GeneratedCode("DomGen", "2.0")]
	internal class DocumentSecurity : OpenXmlLeafTextElement
	{
		// Token: 0x17006B4A RID: 27466
		// (get) Token: 0x06014EDA RID: 85722 RVA: 0x00318D48 File Offset: 0x00316F48
		public override string LocalName
		{
			get
			{
				return "DocSecurity";
			}
		}

		// Token: 0x17006B4B RID: 27467
		// (get) Token: 0x06014EDB RID: 85723 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B4C RID: 27468
		// (get) Token: 0x06014EDC RID: 85724 RVA: 0x00318D4F File Offset: 0x00316F4F
		internal override int ElementTypeId
		{
			get
			{
				return 11025;
			}
		}

		// Token: 0x06014EDD RID: 85725 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014EDE RID: 85726 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public DocumentSecurity()
		{
		}

		// Token: 0x06014EDF RID: 85727 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public DocumentSecurity(string text)
			: base(text)
		{
		}

		// Token: 0x06014EE0 RID: 85728 RVA: 0x00318D58 File Offset: 0x00316F58
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014EE1 RID: 85729 RVA: 0x00318D73 File Offset: 0x00316F73
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocumentSecurity>(deep);
		}

		// Token: 0x040090A9 RID: 37033
		private const string tagName = "DocSecurity";

		// Token: 0x040090AA RID: 37034
		private const byte tagNsId = 3;

		// Token: 0x040090AB RID: 37035
		internal const int ElementTypeIdConst = 11025;
	}
}
