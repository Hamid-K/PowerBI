using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023D8 RID: 9176
	[ChildElementInfo(typeof(IgnoredError), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class IgnoredErrors : OpenXmlCompositeElement
	{
		// Token: 0x17004D35 RID: 19765
		// (get) Token: 0x06010AA5 RID: 68261 RVA: 0x002E5C8D File Offset: 0x002E3E8D
		public override string LocalName
		{
			get
			{
				return "ignoredErrors";
			}
		}

		// Token: 0x17004D36 RID: 19766
		// (get) Token: 0x06010AA6 RID: 68262 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D37 RID: 19767
		// (get) Token: 0x06010AA7 RID: 68263 RVA: 0x002E5C94 File Offset: 0x002E3E94
		internal override int ElementTypeId
		{
			get
			{
				return 12902;
			}
		}

		// Token: 0x06010AA8 RID: 68264 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010AA9 RID: 68265 RVA: 0x00293ECF File Offset: 0x002920CF
		public IgnoredErrors()
		{
		}

		// Token: 0x06010AAA RID: 68266 RVA: 0x00293ED7 File Offset: 0x002920D7
		public IgnoredErrors(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010AAB RID: 68267 RVA: 0x00293EE0 File Offset: 0x002920E0
		public IgnoredErrors(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010AAC RID: 68268 RVA: 0x00293EE9 File Offset: 0x002920E9
		public IgnoredErrors(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010AAD RID: 68269 RVA: 0x002E5C9B File Offset: 0x002E3E9B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "ignoredError" == name)
			{
				return new IgnoredError();
			}
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06010AAE RID: 68270 RVA: 0x002E5CCE File Offset: 0x002E3ECE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IgnoredErrors>(deep);
		}

		// Token: 0x040075D1 RID: 30161
		private const string tagName = "ignoredErrors";

		// Token: 0x040075D2 RID: 30162
		private const byte tagNsId = 53;

		// Token: 0x040075D3 RID: 30163
		internal const int ElementTypeIdConst = 12902;
	}
}
