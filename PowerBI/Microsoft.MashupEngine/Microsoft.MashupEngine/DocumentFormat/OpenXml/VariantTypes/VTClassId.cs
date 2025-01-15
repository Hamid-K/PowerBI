using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200292E RID: 10542
	[GeneratedCode("DomGen", "2.0")]
	internal class VTClassId : OpenXmlLeafTextElement
	{
		// Token: 0x17006AE8 RID: 27368
		// (get) Token: 0x06014DE9 RID: 85481 RVA: 0x003182E3 File Offset: 0x003164E3
		public override string LocalName
		{
			get
			{
				return "clsid";
			}
		}

		// Token: 0x17006AE9 RID: 27369
		// (get) Token: 0x06014DEA RID: 85482 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AEA RID: 27370
		// (get) Token: 0x06014DEB RID: 85483 RVA: 0x003182EA File Offset: 0x003164EA
		internal override int ElementTypeId
		{
			get
			{
				return 10996;
			}
		}

		// Token: 0x06014DEC RID: 85484 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014DED RID: 85485 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTClassId()
		{
		}

		// Token: 0x06014DEE RID: 85486 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTClassId(string text)
			: base(text)
		{
		}

		// Token: 0x06014DEF RID: 85487 RVA: 0x003182F4 File Offset: 0x003164F4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014DF0 RID: 85488 RVA: 0x0031830F File Offset: 0x0031650F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTClassId>(deep);
		}

		// Token: 0x0400903E RID: 36926
		private const string tagName = "clsid";

		// Token: 0x0400903F RID: 36927
		private const byte tagNsId = 5;

		// Token: 0x04009040 RID: 36928
		internal const int ElementTypeIdConst = 10996;
	}
}
