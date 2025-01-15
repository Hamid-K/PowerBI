using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002442 RID: 9282
	[ChildElementInfo(typeof(OlapSlicerCacheSelection), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class OlapSlicerCacheSelections : OpenXmlCompositeElement
	{
		// Token: 0x1700507F RID: 20607
		// (get) Token: 0x06011200 RID: 70144 RVA: 0x002EADDF File Offset: 0x002E8FDF
		public override string LocalName
		{
			get
			{
				return "selections";
			}
		}

		// Token: 0x17005080 RID: 20608
		// (get) Token: 0x06011201 RID: 70145 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17005081 RID: 20609
		// (get) Token: 0x06011202 RID: 70146 RVA: 0x002EADE6 File Offset: 0x002E8FE6
		internal override int ElementTypeId
		{
			get
			{
				return 13006;
			}
		}

		// Token: 0x06011203 RID: 70147 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005082 RID: 20610
		// (get) Token: 0x06011204 RID: 70148 RVA: 0x002EADED File Offset: 0x002E8FED
		internal override string[] AttributeTagNames
		{
			get
			{
				return OlapSlicerCacheSelections.attributeTagNames;
			}
		}

		// Token: 0x17005083 RID: 20611
		// (get) Token: 0x06011205 RID: 70149 RVA: 0x002EADF4 File Offset: 0x002E8FF4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OlapSlicerCacheSelections.attributeNamespaceIds;
			}
		}

		// Token: 0x17005084 RID: 20612
		// (get) Token: 0x06011206 RID: 70150 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06011207 RID: 70151 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x06011208 RID: 70152 RVA: 0x00293ECF File Offset: 0x002920CF
		public OlapSlicerCacheSelections()
		{
		}

		// Token: 0x06011209 RID: 70153 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OlapSlicerCacheSelections(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601120A RID: 70154 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OlapSlicerCacheSelections(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601120B RID: 70155 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OlapSlicerCacheSelections(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601120C RID: 70156 RVA: 0x002EADFB File Offset: 0x002E8FFB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "selection" == name)
			{
				return new OlapSlicerCacheSelection();
			}
			return null;
		}

		// Token: 0x0601120D RID: 70157 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601120E RID: 70158 RVA: 0x002EAE16 File Offset: 0x002E9016
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OlapSlicerCacheSelections>(deep);
		}

		// Token: 0x0601120F RID: 70159 RVA: 0x002EAE20 File Offset: 0x002E9020
		// Note: this type is marked as 'beforefieldinit'.
		static OlapSlicerCacheSelections()
		{
			byte[] array = new byte[1];
			OlapSlicerCacheSelections.attributeNamespaceIds = array;
		}

		// Token: 0x040077C3 RID: 30659
		private const string tagName = "selections";

		// Token: 0x040077C4 RID: 30660
		private const byte tagNsId = 53;

		// Token: 0x040077C5 RID: 30661
		internal const int ElementTypeIdConst = 13006;

		// Token: 0x040077C6 RID: 30662
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x040077C7 RID: 30663
		private static byte[] attributeNamespaceIds;
	}
}
