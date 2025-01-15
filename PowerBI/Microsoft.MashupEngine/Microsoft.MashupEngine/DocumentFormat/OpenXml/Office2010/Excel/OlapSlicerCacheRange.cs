using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200243E RID: 9278
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OlapSlicerCacheItem), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class OlapSlicerCacheRange : OpenXmlCompositeElement
	{
		// Token: 0x17005062 RID: 20578
		// (get) Token: 0x060111B9 RID: 70073 RVA: 0x002EAB43 File Offset: 0x002E8D43
		public override string LocalName
		{
			get
			{
				return "range";
			}
		}

		// Token: 0x17005063 RID: 20579
		// (get) Token: 0x060111BA RID: 70074 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17005064 RID: 20580
		// (get) Token: 0x060111BB RID: 70075 RVA: 0x002EAB4A File Offset: 0x002E8D4A
		internal override int ElementTypeId
		{
			get
			{
				return 13002;
			}
		}

		// Token: 0x060111BC RID: 70076 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005065 RID: 20581
		// (get) Token: 0x060111BD RID: 70077 RVA: 0x002EAB51 File Offset: 0x002E8D51
		internal override string[] AttributeTagNames
		{
			get
			{
				return OlapSlicerCacheRange.attributeTagNames;
			}
		}

		// Token: 0x17005066 RID: 20582
		// (get) Token: 0x060111BE RID: 70078 RVA: 0x002EAB58 File Offset: 0x002E8D58
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OlapSlicerCacheRange.attributeNamespaceIds;
			}
		}

		// Token: 0x17005067 RID: 20583
		// (get) Token: 0x060111BF RID: 70079 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060111C0 RID: 70080 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "startItem")]
		public UInt32Value StartItem
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

		// Token: 0x060111C1 RID: 70081 RVA: 0x00293ECF File Offset: 0x002920CF
		public OlapSlicerCacheRange()
		{
		}

		// Token: 0x060111C2 RID: 70082 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OlapSlicerCacheRange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060111C3 RID: 70083 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OlapSlicerCacheRange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060111C4 RID: 70084 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OlapSlicerCacheRange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060111C5 RID: 70085 RVA: 0x002EAB5F File Offset: 0x002E8D5F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "i" == name)
			{
				return new OlapSlicerCacheItem();
			}
			return null;
		}

		// Token: 0x060111C6 RID: 70086 RVA: 0x002EAB7A File Offset: 0x002E8D7A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "startItem" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060111C7 RID: 70087 RVA: 0x002EAB9A File Offset: 0x002E8D9A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OlapSlicerCacheRange>(deep);
		}

		// Token: 0x060111C8 RID: 70088 RVA: 0x002EABA4 File Offset: 0x002E8DA4
		// Note: this type is marked as 'beforefieldinit'.
		static OlapSlicerCacheRange()
		{
			byte[] array = new byte[1];
			OlapSlicerCacheRange.attributeNamespaceIds = array;
		}

		// Token: 0x040077AF RID: 30639
		private const string tagName = "range";

		// Token: 0x040077B0 RID: 30640
		private const byte tagNsId = 53;

		// Token: 0x040077B1 RID: 30641
		internal const int ElementTypeIdConst = 13002;

		// Token: 0x040077B2 RID: 30642
		private static string[] attributeTagNames = new string[] { "startItem" };

		// Token: 0x040077B3 RID: 30643
		private static byte[] attributeNamespaceIds;
	}
}
