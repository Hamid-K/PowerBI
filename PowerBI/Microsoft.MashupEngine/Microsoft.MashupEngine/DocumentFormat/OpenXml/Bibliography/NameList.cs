using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028EE RID: 10478
	[ChildElementInfo(typeof(Person))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NameList : OpenXmlCompositeElement
	{
		// Token: 0x17006972 RID: 26994
		// (get) Token: 0x06014A64 RID: 84580 RVA: 0x00315324 File Offset: 0x00313524
		public override string LocalName
		{
			get
			{
				return "NameList";
			}
		}

		// Token: 0x17006973 RID: 26995
		// (get) Token: 0x06014A65 RID: 84581 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006974 RID: 26996
		// (get) Token: 0x06014A66 RID: 84582 RVA: 0x0031532B File Offset: 0x0031352B
		internal override int ElementTypeId
		{
			get
			{
				return 10763;
			}
		}

		// Token: 0x06014A67 RID: 84583 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A68 RID: 84584 RVA: 0x00293ECF File Offset: 0x002920CF
		public NameList()
		{
		}

		// Token: 0x06014A69 RID: 84585 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NameList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014A6A RID: 84586 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NameList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014A6B RID: 84587 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NameList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014A6C RID: 84588 RVA: 0x00315332 File Offset: 0x00313532
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (9 == namespaceId && "Person" == name)
			{
				return new Person();
			}
			return null;
		}

		// Token: 0x06014A6D RID: 84589 RVA: 0x0031534D File Offset: 0x0031354D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NameList>(deep);
		}

		// Token: 0x04008F56 RID: 36694
		private const string tagName = "NameList";

		// Token: 0x04008F57 RID: 36695
		private const byte tagNsId = 9;

		// Token: 0x04008F58 RID: 36696
		internal const int ElementTypeIdConst = 10763;
	}
}
