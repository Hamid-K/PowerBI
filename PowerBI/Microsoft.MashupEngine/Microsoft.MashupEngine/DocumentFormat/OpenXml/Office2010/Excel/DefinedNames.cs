using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023D9 RID: 9177
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DefinedName), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class DefinedNames : OpenXmlCompositeElement
	{
		// Token: 0x17004D38 RID: 19768
		// (get) Token: 0x06010AAF RID: 68271 RVA: 0x002E5CD7 File Offset: 0x002E3ED7
		public override string LocalName
		{
			get
			{
				return "definedNames";
			}
		}

		// Token: 0x17004D39 RID: 19769
		// (get) Token: 0x06010AB0 RID: 68272 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D3A RID: 19770
		// (get) Token: 0x06010AB1 RID: 68273 RVA: 0x002E5CDE File Offset: 0x002E3EDE
		internal override int ElementTypeId
		{
			get
			{
				return 12903;
			}
		}

		// Token: 0x06010AB2 RID: 68274 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010AB3 RID: 68275 RVA: 0x00293ECF File Offset: 0x002920CF
		public DefinedNames()
		{
		}

		// Token: 0x06010AB4 RID: 68276 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DefinedNames(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010AB5 RID: 68277 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DefinedNames(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010AB6 RID: 68278 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DefinedNames(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010AB7 RID: 68279 RVA: 0x002E5CE5 File Offset: 0x002E3EE5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "definedName" == name)
			{
				return new DefinedName();
			}
			return null;
		}

		// Token: 0x06010AB8 RID: 68280 RVA: 0x002E5D00 File Offset: 0x002E3F00
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefinedNames>(deep);
		}

		// Token: 0x040075D4 RID: 30164
		private const string tagName = "definedNames";

		// Token: 0x040075D5 RID: 30165
		private const byte tagNsId = 53;

		// Token: 0x040075D6 RID: 30166
		internal const int ElementTypeIdConst = 12903;
	}
}
