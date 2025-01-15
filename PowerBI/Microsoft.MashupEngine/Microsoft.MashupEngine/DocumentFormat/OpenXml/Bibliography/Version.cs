using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028EA RID: 10474
	[GeneratedCode("DomGen", "2.0")]
	internal class Version : OpenXmlLeafTextElement
	{
		// Token: 0x17006966 RID: 26982
		// (get) Token: 0x06014A44 RID: 84548 RVA: 0x00315254 File Offset: 0x00313454
		public override string LocalName
		{
			get
			{
				return "Version";
			}
		}

		// Token: 0x17006967 RID: 26983
		// (get) Token: 0x06014A45 RID: 84549 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006968 RID: 26984
		// (get) Token: 0x06014A46 RID: 84550 RVA: 0x0031525B File Offset: 0x0031345B
		internal override int ElementTypeId
		{
			get
			{
				return 10829;
			}
		}

		// Token: 0x06014A47 RID: 84551 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A48 RID: 84552 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Version()
		{
		}

		// Token: 0x06014A49 RID: 84553 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Version(string text)
			: base(text)
		{
		}

		// Token: 0x06014A4A RID: 84554 RVA: 0x00315264 File Offset: 0x00313464
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014A4B RID: 84555 RVA: 0x0031527F File Offset: 0x0031347F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Version>(deep);
		}

		// Token: 0x04008F4A RID: 36682
		private const string tagName = "Version";

		// Token: 0x04008F4B RID: 36683
		private const byte tagNsId = 9;

		// Token: 0x04008F4C RID: 36684
		internal const int ElementTypeIdConst = 10829;
	}
}
