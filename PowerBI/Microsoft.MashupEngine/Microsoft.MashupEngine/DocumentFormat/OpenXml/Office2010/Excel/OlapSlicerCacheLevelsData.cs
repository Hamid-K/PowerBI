using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002441 RID: 9281
	[ChildElementInfo(typeof(OlapSlicerCacheLevelData), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class OlapSlicerCacheLevelsData : OpenXmlCompositeElement
	{
		// Token: 0x17005079 RID: 20601
		// (get) Token: 0x060111F0 RID: 70128 RVA: 0x002EAD6E File Offset: 0x002E8F6E
		public override string LocalName
		{
			get
			{
				return "levels";
			}
		}

		// Token: 0x1700507A RID: 20602
		// (get) Token: 0x060111F1 RID: 70129 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x1700507B RID: 20603
		// (get) Token: 0x060111F2 RID: 70130 RVA: 0x002EAD75 File Offset: 0x002E8F75
		internal override int ElementTypeId
		{
			get
			{
				return 13005;
			}
		}

		// Token: 0x060111F3 RID: 70131 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700507C RID: 20604
		// (get) Token: 0x060111F4 RID: 70132 RVA: 0x002EAD7C File Offset: 0x002E8F7C
		internal override string[] AttributeTagNames
		{
			get
			{
				return OlapSlicerCacheLevelsData.attributeTagNames;
			}
		}

		// Token: 0x1700507D RID: 20605
		// (get) Token: 0x060111F5 RID: 70133 RVA: 0x002EAD83 File Offset: 0x002E8F83
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OlapSlicerCacheLevelsData.attributeNamespaceIds;
			}
		}

		// Token: 0x1700507E RID: 20606
		// (get) Token: 0x060111F6 RID: 70134 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060111F7 RID: 70135 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060111F8 RID: 70136 RVA: 0x00293ECF File Offset: 0x002920CF
		public OlapSlicerCacheLevelsData()
		{
		}

		// Token: 0x060111F9 RID: 70137 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OlapSlicerCacheLevelsData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060111FA RID: 70138 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OlapSlicerCacheLevelsData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060111FB RID: 70139 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OlapSlicerCacheLevelsData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060111FC RID: 70140 RVA: 0x002EAD8A File Offset: 0x002E8F8A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "level" == name)
			{
				return new OlapSlicerCacheLevelData();
			}
			return null;
		}

		// Token: 0x060111FD RID: 70141 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060111FE RID: 70142 RVA: 0x002EADA5 File Offset: 0x002E8FA5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OlapSlicerCacheLevelsData>(deep);
		}

		// Token: 0x060111FF RID: 70143 RVA: 0x002EADB0 File Offset: 0x002E8FB0
		// Note: this type is marked as 'beforefieldinit'.
		static OlapSlicerCacheLevelsData()
		{
			byte[] array = new byte[1];
			OlapSlicerCacheLevelsData.attributeNamespaceIds = array;
		}

		// Token: 0x040077BE RID: 30654
		private const string tagName = "levels";

		// Token: 0x040077BF RID: 30655
		private const byte tagNsId = 53;

		// Token: 0x040077C0 RID: 30656
		internal const int ElementTypeIdConst = 13005;

		// Token: 0x040077C1 RID: 30657
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x040077C2 RID: 30658
		private static byte[] attributeNamespaceIds;
	}
}
