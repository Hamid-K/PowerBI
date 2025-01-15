using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002933 RID: 10547
	[GeneratedCode("DomGen", "2.0")]
	internal class Template : OpenXmlLeafTextElement
	{
		// Token: 0x17006B14 RID: 27412
		// (get) Token: 0x06014E4A RID: 85578 RVA: 0x003189B0 File Offset: 0x00316BB0
		public override string LocalName
		{
			get
			{
				return "Template";
			}
		}

		// Token: 0x17006B15 RID: 27413
		// (get) Token: 0x06014E4B RID: 85579 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B16 RID: 27414
		// (get) Token: 0x06014E4C RID: 85580 RVA: 0x003189B7 File Offset: 0x00316BB7
		internal override int ElementTypeId
		{
			get
			{
				return 10999;
			}
		}

		// Token: 0x06014E4D RID: 85581 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014E4E RID: 85582 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Template()
		{
		}

		// Token: 0x06014E4F RID: 85583 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Template(string text)
			: base(text)
		{
		}

		// Token: 0x06014E50 RID: 85584 RVA: 0x003189C0 File Offset: 0x00316BC0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014E51 RID: 85585 RVA: 0x003189DB File Offset: 0x00316BDB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Template>(deep);
		}

		// Token: 0x04009073 RID: 36979
		private const string tagName = "Template";

		// Token: 0x04009074 RID: 36980
		private const byte tagNsId = 3;

		// Token: 0x04009075 RID: 36981
		internal const int ElementTypeIdConst = 10999;
	}
}
