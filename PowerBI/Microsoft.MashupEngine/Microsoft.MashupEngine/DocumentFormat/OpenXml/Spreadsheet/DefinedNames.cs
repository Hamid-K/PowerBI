using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C58 RID: 11352
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DefinedName))]
	internal class DefinedNames : OpenXmlCompositeElement
	{
		// Token: 0x17008246 RID: 33350
		// (get) Token: 0x060181C0 RID: 98752 RVA: 0x002E5CD7 File Offset: 0x002E3ED7
		public override string LocalName
		{
			get
			{
				return "definedNames";
			}
		}

		// Token: 0x17008247 RID: 33351
		// (get) Token: 0x060181C1 RID: 98753 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008248 RID: 33352
		// (get) Token: 0x060181C2 RID: 98754 RVA: 0x0033E94E File Offset: 0x0033CB4E
		internal override int ElementTypeId
		{
			get
			{
				return 11333;
			}
		}

		// Token: 0x060181C3 RID: 98755 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060181C4 RID: 98756 RVA: 0x00293ECF File Offset: 0x002920CF
		public DefinedNames()
		{
		}

		// Token: 0x060181C5 RID: 98757 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DefinedNames(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060181C6 RID: 98758 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DefinedNames(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060181C7 RID: 98759 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DefinedNames(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060181C8 RID: 98760 RVA: 0x0033E955 File Offset: 0x0033CB55
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "definedName" == name)
			{
				return new DefinedName();
			}
			return null;
		}

		// Token: 0x060181C9 RID: 98761 RVA: 0x0033E970 File Offset: 0x0033CB70
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefinedNames>(deep);
		}

		// Token: 0x04009EE3 RID: 40675
		private const string tagName = "definedNames";

		// Token: 0x04009EE4 RID: 40676
		private const byte tagNsId = 22;

		// Token: 0x04009EE5 RID: 40677
		internal const int ElementTypeIdConst = 11333;
	}
}
