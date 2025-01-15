using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023D7 RID: 9175
	[ChildElementInfo(typeof(ProtectedRange), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ProtectedRanges : OpenXmlCompositeElement
	{
		// Token: 0x17004D32 RID: 19762
		// (get) Token: 0x06010A9B RID: 68251 RVA: 0x002E5C5B File Offset: 0x002E3E5B
		public override string LocalName
		{
			get
			{
				return "protectedRanges";
			}
		}

		// Token: 0x17004D33 RID: 19763
		// (get) Token: 0x06010A9C RID: 68252 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D34 RID: 19764
		// (get) Token: 0x06010A9D RID: 68253 RVA: 0x002E5C62 File Offset: 0x002E3E62
		internal override int ElementTypeId
		{
			get
			{
				return 12901;
			}
		}

		// Token: 0x06010A9E RID: 68254 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010A9F RID: 68255 RVA: 0x00293ECF File Offset: 0x002920CF
		public ProtectedRanges()
		{
		}

		// Token: 0x06010AA0 RID: 68256 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ProtectedRanges(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010AA1 RID: 68257 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ProtectedRanges(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010AA2 RID: 68258 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ProtectedRanges(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010AA3 RID: 68259 RVA: 0x002E5C69 File Offset: 0x002E3E69
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "protectedRange" == name)
			{
				return new ProtectedRange();
			}
			return null;
		}

		// Token: 0x06010AA4 RID: 68260 RVA: 0x002E5C84 File Offset: 0x002E3E84
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ProtectedRanges>(deep);
		}

		// Token: 0x040075CE RID: 30158
		private const string tagName = "protectedRanges";

		// Token: 0x040075CF RID: 30159
		private const byte tagNsId = 53;

		// Token: 0x040075D0 RID: 30160
		internal const int ElementTypeIdConst = 12901;
	}
}
