using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CC6 RID: 11462
	[ChildElementInfo(typeof(RangeProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DiscreteProperties))]
	[ChildElementInfo(typeof(GroupItems))]
	internal class FieldGroup : OpenXmlCompositeElement
	{
		// Token: 0x1700851A RID: 34074
		// (get) Token: 0x060188AC RID: 100524 RVA: 0x0034266A File Offset: 0x0034086A
		public override string LocalName
		{
			get
			{
				return "fieldGroup";
			}
		}

		// Token: 0x1700851B RID: 34075
		// (get) Token: 0x060188AD RID: 100525 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700851C RID: 34076
		// (get) Token: 0x060188AE RID: 100526 RVA: 0x00342671 File Offset: 0x00340871
		internal override int ElementTypeId
		{
			get
			{
				return 11442;
			}
		}

		// Token: 0x060188AF RID: 100527 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700851D RID: 34077
		// (get) Token: 0x060188B0 RID: 100528 RVA: 0x00342678 File Offset: 0x00340878
		internal override string[] AttributeTagNames
		{
			get
			{
				return FieldGroup.attributeTagNames;
			}
		}

		// Token: 0x1700851E RID: 34078
		// (get) Token: 0x060188B1 RID: 100529 RVA: 0x0034267F File Offset: 0x0034087F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FieldGroup.attributeNamespaceIds;
			}
		}

		// Token: 0x1700851F RID: 34079
		// (get) Token: 0x060188B2 RID: 100530 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060188B3 RID: 100531 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "par")]
		public UInt32Value ParentId
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008520 RID: 34080
		// (get) Token: 0x060188B4 RID: 100532 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x060188B5 RID: 100533 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "base")]
		public UInt32Value Base
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060188B6 RID: 100534 RVA: 0x00293ECF File Offset: 0x002920CF
		public FieldGroup()
		{
		}

		// Token: 0x060188B7 RID: 100535 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FieldGroup(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060188B8 RID: 100536 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FieldGroup(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060188B9 RID: 100537 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FieldGroup(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060188BA RID: 100538 RVA: 0x00342688 File Offset: 0x00340888
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "rangePr" == name)
			{
				return new RangeProperties();
			}
			if (22 == namespaceId && "discretePr" == name)
			{
				return new DiscreteProperties();
			}
			if (22 == namespaceId && "groupItems" == name)
			{
				return new GroupItems();
			}
			return null;
		}

		// Token: 0x060188BB RID: 100539 RVA: 0x003426DE File Offset: 0x003408DE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "par" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "base" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060188BC RID: 100540 RVA: 0x00342714 File Offset: 0x00340914
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FieldGroup>(deep);
		}

		// Token: 0x060188BD RID: 100541 RVA: 0x00342720 File Offset: 0x00340920
		// Note: this type is marked as 'beforefieldinit'.
		static FieldGroup()
		{
			byte[] array = new byte[2];
			FieldGroup.attributeNamespaceIds = array;
		}

		// Token: 0x0400A0BD RID: 41149
		private const string tagName = "fieldGroup";

		// Token: 0x0400A0BE RID: 41150
		private const byte tagNsId = 22;

		// Token: 0x0400A0BF RID: 41151
		internal const int ElementTypeIdConst = 11442;

		// Token: 0x0400A0C0 RID: 41152
		private static string[] attributeTagNames = new string[] { "par", "base" };

		// Token: 0x0400A0C1 RID: 41153
		private static byte[] attributeNamespaceIds;
	}
}
