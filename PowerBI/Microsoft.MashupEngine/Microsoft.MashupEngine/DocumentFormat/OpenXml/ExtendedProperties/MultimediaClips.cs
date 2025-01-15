using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002943 RID: 10563
	[GeneratedCode("DomGen", "2.0")]
	internal class MultimediaClips : OpenXmlLeafTextElement
	{
		// Token: 0x17006B44 RID: 27460
		// (get) Token: 0x06014ECA RID: 85706 RVA: 0x00318CE0 File Offset: 0x00316EE0
		public override string LocalName
		{
			get
			{
				return "MMClips";
			}
		}

		// Token: 0x17006B45 RID: 27461
		// (get) Token: 0x06014ECB RID: 85707 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B46 RID: 27462
		// (get) Token: 0x06014ECC RID: 85708 RVA: 0x00318CE7 File Offset: 0x00316EE7
		internal override int ElementTypeId
		{
			get
			{
				return 11012;
			}
		}

		// Token: 0x06014ECD RID: 85709 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014ECE RID: 85710 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public MultimediaClips()
		{
		}

		// Token: 0x06014ECF RID: 85711 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public MultimediaClips(string text)
			: base(text)
		{
		}

		// Token: 0x06014ED0 RID: 85712 RVA: 0x00318CF0 File Offset: 0x00316EF0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014ED1 RID: 85713 RVA: 0x00318D0B File Offset: 0x00316F0B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MultimediaClips>(deep);
		}

		// Token: 0x040090A3 RID: 37027
		private const string tagName = "MMClips";

		// Token: 0x040090A4 RID: 37028
		private const byte tagNsId = 3;

		// Token: 0x040090A5 RID: 37029
		internal const int ElementTypeIdConst = 11012;
	}
}
