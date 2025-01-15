using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C9D RID: 11421
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Hyperlink))]
	internal class Hyperlinks : OpenXmlCompositeElement
	{
		// Token: 0x17008430 RID: 33840
		// (get) Token: 0x06018645 RID: 99909 RVA: 0x003413AB File Offset: 0x0033F5AB
		public override string LocalName
		{
			get
			{
				return "hyperlinks";
			}
		}

		// Token: 0x17008431 RID: 33841
		// (get) Token: 0x06018646 RID: 99910 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008432 RID: 33842
		// (get) Token: 0x06018647 RID: 99911 RVA: 0x003413B2 File Offset: 0x0033F5B2
		internal override int ElementTypeId
		{
			get
			{
				return 11401;
			}
		}

		// Token: 0x06018648 RID: 99912 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018649 RID: 99913 RVA: 0x00293ECF File Offset: 0x002920CF
		public Hyperlinks()
		{
		}

		// Token: 0x0601864A RID: 99914 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Hyperlinks(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601864B RID: 99915 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Hyperlinks(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601864C RID: 99916 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Hyperlinks(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601864D RID: 99917 RVA: 0x003413B9 File Offset: 0x0033F5B9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "hyperlink" == name)
			{
				return new Hyperlink();
			}
			return null;
		}

		// Token: 0x0601864E RID: 99918 RVA: 0x003413D4 File Offset: 0x0033F5D4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Hyperlinks>(deep);
		}

		// Token: 0x0400A00C RID: 40972
		private const string tagName = "hyperlinks";

		// Token: 0x0400A00D RID: 40973
		private const byte tagNsId = 22;

		// Token: 0x0400A00E RID: 40974
		internal const int ElementTypeIdConst = 11401;
	}
}
