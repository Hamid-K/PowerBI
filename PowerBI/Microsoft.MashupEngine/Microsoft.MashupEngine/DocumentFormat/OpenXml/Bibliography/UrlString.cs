using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028E9 RID: 10473
	[GeneratedCode("DomGen", "2.0")]
	internal class UrlString : OpenXmlLeafTextElement
	{
		// Token: 0x17006963 RID: 26979
		// (get) Token: 0x06014A3C RID: 84540 RVA: 0x00315220 File Offset: 0x00313420
		public override string LocalName
		{
			get
			{
				return "URL";
			}
		}

		// Token: 0x17006964 RID: 26980
		// (get) Token: 0x06014A3D RID: 84541 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006965 RID: 26981
		// (get) Token: 0x06014A3E RID: 84542 RVA: 0x00315227 File Offset: 0x00313427
		internal override int ElementTypeId
		{
			get
			{
				return 10828;
			}
		}

		// Token: 0x06014A3F RID: 84543 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A40 RID: 84544 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public UrlString()
		{
		}

		// Token: 0x06014A41 RID: 84545 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public UrlString(string text)
			: base(text)
		{
		}

		// Token: 0x06014A42 RID: 84546 RVA: 0x00315230 File Offset: 0x00313430
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014A43 RID: 84547 RVA: 0x0031524B File Offset: 0x0031344B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UrlString>(deep);
		}

		// Token: 0x04008F47 RID: 36679
		private const string tagName = "URL";

		// Token: 0x04008F48 RID: 36680
		private const byte tagNsId = 9;

		// Token: 0x04008F49 RID: 36681
		internal const int ElementTypeIdConst = 10828;
	}
}
