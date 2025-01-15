using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x0200293C RID: 10556
	[GeneratedCode("DomGen", "2.0")]
	internal class Characters : OpenXmlLeafTextElement
	{
		// Token: 0x17006B2F RID: 27439
		// (get) Token: 0x06014E92 RID: 85650 RVA: 0x00318B7C File Offset: 0x00316D7C
		public override string LocalName
		{
			get
			{
				return "Characters";
			}
		}

		// Token: 0x17006B30 RID: 27440
		// (get) Token: 0x06014E93 RID: 85651 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B31 RID: 27441
		// (get) Token: 0x06014E94 RID: 85652 RVA: 0x00318B83 File Offset: 0x00316D83
		internal override int ElementTypeId
		{
			get
			{
				return 11004;
			}
		}

		// Token: 0x06014E95 RID: 85653 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014E96 RID: 85654 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Characters()
		{
		}

		// Token: 0x06014E97 RID: 85655 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Characters(string text)
			: base(text)
		{
		}

		// Token: 0x06014E98 RID: 85656 RVA: 0x00318B8C File Offset: 0x00316D8C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014E99 RID: 85657 RVA: 0x00318BA7 File Offset: 0x00316DA7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Characters>(deep);
		}

		// Token: 0x0400908E RID: 37006
		private const string tagName = "Characters";

		// Token: 0x0400908F RID: 37007
		private const byte tagNsId = 3;

		// Token: 0x04009090 RID: 37008
		internal const int ElementTypeIdConst = 11004;
	}
}
