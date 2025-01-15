using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x02002393 RID: 9107
	[ChildElementInfo(typeof(Extension))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ExtensionListModify : OpenXmlCompositeElement
	{
		// Token: 0x17004BCE RID: 19406
		// (get) Token: 0x06010793 RID: 67475 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17004BCF RID: 19407
		// (get) Token: 0x06010794 RID: 67476 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004BD0 RID: 19408
		// (get) Token: 0x06010795 RID: 67477 RVA: 0x002E3E2B File Offset: 0x002E202B
		internal override int ElementTypeId
		{
			get
			{
				return 12766;
			}
		}

		// Token: 0x06010796 RID: 67478 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004BD1 RID: 19409
		// (get) Token: 0x06010797 RID: 67479 RVA: 0x002E3E32 File Offset: 0x002E2032
		internal override string[] AttributeTagNames
		{
			get
			{
				return ExtensionListModify.attributeTagNames;
			}
		}

		// Token: 0x17004BD2 RID: 19410
		// (get) Token: 0x06010798 RID: 67480 RVA: 0x002E3E39 File Offset: 0x002E2039
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ExtensionListModify.attributeNamespaceIds;
			}
		}

		// Token: 0x17004BD3 RID: 19411
		// (get) Token: 0x06010799 RID: 67481 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601079A RID: 67482 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "mod")]
		public BooleanValue Modify
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601079B RID: 67483 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExtensionListModify()
		{
		}

		// Token: 0x0601079C RID: 67484 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExtensionListModify(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601079D RID: 67485 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExtensionListModify(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601079E RID: 67486 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExtensionListModify(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601079F RID: 67487 RVA: 0x002E3E40 File Offset: 0x002E2040
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x060107A0 RID: 67488 RVA: 0x002E3E5B File Offset: 0x002E205B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "mod" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060107A1 RID: 67489 RVA: 0x002E3E7B File Offset: 0x002E207B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExtensionListModify>(deep);
		}

		// Token: 0x060107A2 RID: 67490 RVA: 0x002E3E84 File Offset: 0x002E2084
		// Note: this type is marked as 'beforefieldinit'.
		static ExtensionListModify()
		{
			byte[] array = new byte[1];
			ExtensionListModify.attributeNamespaceIds = array;
		}

		// Token: 0x040074C1 RID: 29889
		private const string tagName = "extLst";

		// Token: 0x040074C2 RID: 29890
		private const byte tagNsId = 49;

		// Token: 0x040074C3 RID: 29891
		internal const int ElementTypeIdConst = 12766;

		// Token: 0x040074C4 RID: 29892
		private static string[] attributeTagNames = new string[] { "mod" };

		// Token: 0x040074C5 RID: 29893
		private static byte[] attributeNamespaceIds;
	}
}
