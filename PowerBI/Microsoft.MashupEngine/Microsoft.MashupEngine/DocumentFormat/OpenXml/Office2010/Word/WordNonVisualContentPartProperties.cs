using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024BF RID: 9407
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class WordNonVisualContentPartProperties : WordContentPartNonVisualType
	{
		// Token: 0x170052A7 RID: 21159
		// (get) Token: 0x060116E7 RID: 71399 RVA: 0x002DFF48 File Offset: 0x002DE148
		public override string LocalName
		{
			get
			{
				return "nvContentPr";
			}
		}

		// Token: 0x170052A8 RID: 21160
		// (get) Token: 0x060116E8 RID: 71400 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052A9 RID: 21161
		// (get) Token: 0x060116E9 RID: 71401 RVA: 0x002EE6DC File Offset: 0x002EC8DC
		internal override int ElementTypeId
		{
			get
			{
				return 12879;
			}
		}

		// Token: 0x060116EA RID: 71402 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060116EB RID: 71403 RVA: 0x002EE6E3 File Offset: 0x002EC8E3
		public WordNonVisualContentPartProperties()
		{
		}

		// Token: 0x060116EC RID: 71404 RVA: 0x002EE6EB File Offset: 0x002EC8EB
		public WordNonVisualContentPartProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116ED RID: 71405 RVA: 0x002EE6F4 File Offset: 0x002EC8F4
		public WordNonVisualContentPartProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116EE RID: 71406 RVA: 0x002EE6FD File Offset: 0x002EC8FD
		public WordNonVisualContentPartProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060116EF RID: 71407 RVA: 0x002EE706 File Offset: 0x002EC906
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WordNonVisualContentPartProperties>(deep);
		}

		// Token: 0x040079CB RID: 31179
		private const string tagName = "nvContentPr";

		// Token: 0x040079CC RID: 31180
		private const byte tagNsId = 52;

		// Token: 0x040079CD RID: 31181
		internal const int ElementTypeIdConst = 12879;
	}
}
