using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200292A RID: 10538
	[GeneratedCode("DomGen", "2.0")]
	internal class VTBool : OpenXmlLeafTextElement
	{
		// Token: 0x17006AD9 RID: 27353
		// (get) Token: 0x06014DC3 RID: 85443 RVA: 0x003181B8 File Offset: 0x003163B8
		public override string LocalName
		{
			get
			{
				return "bool";
			}
		}

		// Token: 0x17006ADA RID: 27354
		// (get) Token: 0x06014DC4 RID: 85444 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006ADB RID: 27355
		// (get) Token: 0x06014DC5 RID: 85445 RVA: 0x003181BF File Offset: 0x003163BF
		internal override int ElementTypeId
		{
			get
			{
				return 10988;
			}
		}

		// Token: 0x06014DC6 RID: 85446 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014DC7 RID: 85447 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTBool()
		{
		}

		// Token: 0x06014DC8 RID: 85448 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTBool(string text)
			: base(text)
		{
		}

		// Token: 0x06014DC9 RID: 85449 RVA: 0x003181C8 File Offset: 0x003163C8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new BooleanValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014DCA RID: 85450 RVA: 0x003181E3 File Offset: 0x003163E3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTBool>(deep);
		}

		// Token: 0x04009030 RID: 36912
		private const string tagName = "bool";

		// Token: 0x04009031 RID: 36913
		private const byte tagNsId = 5;

		// Token: 0x04009032 RID: 36914
		internal const int ElementTypeIdConst = 10988;
	}
}
