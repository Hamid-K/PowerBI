using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C1B RID: 11291
	[ChildElementInfo(typeof(ExternalDefinedName))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ExternalDefinedNames : OpenXmlCompositeElement
	{
		// Token: 0x17008047 RID: 32839
		// (get) Token: 0x06017D32 RID: 97586 RVA: 0x002E5CD7 File Offset: 0x002E3ED7
		public override string LocalName
		{
			get
			{
				return "definedNames";
			}
		}

		// Token: 0x17008048 RID: 32840
		// (get) Token: 0x06017D33 RID: 97587 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008049 RID: 32841
		// (get) Token: 0x06017D34 RID: 97588 RVA: 0x0033B989 File Offset: 0x00339B89
		internal override int ElementTypeId
		{
			get
			{
				return 11272;
			}
		}

		// Token: 0x06017D35 RID: 97589 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017D36 RID: 97590 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExternalDefinedNames()
		{
		}

		// Token: 0x06017D37 RID: 97591 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExternalDefinedNames(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D38 RID: 97592 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExternalDefinedNames(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D39 RID: 97593 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExternalDefinedNames(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017D3A RID: 97594 RVA: 0x0033B990 File Offset: 0x00339B90
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "definedName" == name)
			{
				return new ExternalDefinedName();
			}
			return null;
		}

		// Token: 0x06017D3B RID: 97595 RVA: 0x0033B9AB File Offset: 0x00339BAB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExternalDefinedNames>(deep);
		}

		// Token: 0x04009DB8 RID: 40376
		private const string tagName = "definedNames";

		// Token: 0x04009DB9 RID: 40377
		private const byte tagNsId = 22;

		// Token: 0x04009DBA RID: 40378
		internal const int ElementTypeIdConst = 11272;
	}
}
