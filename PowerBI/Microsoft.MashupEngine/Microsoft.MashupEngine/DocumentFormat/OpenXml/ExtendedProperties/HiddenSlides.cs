using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002942 RID: 10562
	[GeneratedCode("DomGen", "2.0")]
	internal class HiddenSlides : OpenXmlLeafTextElement
	{
		// Token: 0x17006B41 RID: 27457
		// (get) Token: 0x06014EC2 RID: 85698 RVA: 0x00318CAC File Offset: 0x00316EAC
		public override string LocalName
		{
			get
			{
				return "HiddenSlides";
			}
		}

		// Token: 0x17006B42 RID: 27458
		// (get) Token: 0x06014EC3 RID: 85699 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B43 RID: 27459
		// (get) Token: 0x06014EC4 RID: 85700 RVA: 0x00318CB3 File Offset: 0x00316EB3
		internal override int ElementTypeId
		{
			get
			{
				return 11011;
			}
		}

		// Token: 0x06014EC5 RID: 85701 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014EC6 RID: 85702 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public HiddenSlides()
		{
		}

		// Token: 0x06014EC7 RID: 85703 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public HiddenSlides(string text)
			: base(text)
		{
		}

		// Token: 0x06014EC8 RID: 85704 RVA: 0x00318CBC File Offset: 0x00316EBC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014EC9 RID: 85705 RVA: 0x00318CD7 File Offset: 0x00316ED7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HiddenSlides>(deep);
		}

		// Token: 0x040090A0 RID: 37024
		private const string tagName = "HiddenSlides";

		// Token: 0x040090A1 RID: 37025
		private const byte tagNsId = 3;

		// Token: 0x040090A2 RID: 37026
		internal const int ElementTypeIdConst = 11011;
	}
}
