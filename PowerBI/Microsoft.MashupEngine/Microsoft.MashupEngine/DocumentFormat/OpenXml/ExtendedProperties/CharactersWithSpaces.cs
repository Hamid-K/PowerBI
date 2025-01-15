using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002944 RID: 10564
	[GeneratedCode("DomGen", "2.0")]
	internal class CharactersWithSpaces : OpenXmlLeafTextElement
	{
		// Token: 0x17006B47 RID: 27463
		// (get) Token: 0x06014ED2 RID: 85714 RVA: 0x00318D14 File Offset: 0x00316F14
		public override string LocalName
		{
			get
			{
				return "CharactersWithSpaces";
			}
		}

		// Token: 0x17006B48 RID: 27464
		// (get) Token: 0x06014ED3 RID: 85715 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B49 RID: 27465
		// (get) Token: 0x06014ED4 RID: 85716 RVA: 0x00318D1B File Offset: 0x00316F1B
		internal override int ElementTypeId
		{
			get
			{
				return 11017;
			}
		}

		// Token: 0x06014ED5 RID: 85717 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014ED6 RID: 85718 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public CharactersWithSpaces()
		{
		}

		// Token: 0x06014ED7 RID: 85719 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public CharactersWithSpaces(string text)
			: base(text)
		{
		}

		// Token: 0x06014ED8 RID: 85720 RVA: 0x00318D24 File Offset: 0x00316F24
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014ED9 RID: 85721 RVA: 0x00318D3F File Offset: 0x00316F3F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CharactersWithSpaces>(deep);
		}

		// Token: 0x040090A6 RID: 37030
		private const string tagName = "CharactersWithSpaces";

		// Token: 0x040090A7 RID: 37031
		private const byte tagNsId = 3;

		// Token: 0x040090A8 RID: 37032
		internal const int ElementTypeIdConst = 11017;
	}
}
