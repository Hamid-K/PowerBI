using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200243D RID: 9277
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OlapSlicerCacheItemParent), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class OlapSlicerCacheItem : OpenXmlCompositeElement
	{
		// Token: 0x1700505A RID: 20570
		// (get) Token: 0x060111A5 RID: 70053 RVA: 0x002EAA6B File Offset: 0x002E8C6B
		public override string LocalName
		{
			get
			{
				return "i";
			}
		}

		// Token: 0x1700505B RID: 20571
		// (get) Token: 0x060111A6 RID: 70054 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x1700505C RID: 20572
		// (get) Token: 0x060111A7 RID: 70055 RVA: 0x002EAA72 File Offset: 0x002E8C72
		internal override int ElementTypeId
		{
			get
			{
				return 13001;
			}
		}

		// Token: 0x060111A8 RID: 70056 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700505D RID: 20573
		// (get) Token: 0x060111A9 RID: 70057 RVA: 0x002EAA79 File Offset: 0x002E8C79
		internal override string[] AttributeTagNames
		{
			get
			{
				return OlapSlicerCacheItem.attributeTagNames;
			}
		}

		// Token: 0x1700505E RID: 20574
		// (get) Token: 0x060111AA RID: 70058 RVA: 0x002EAA80 File Offset: 0x002E8C80
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OlapSlicerCacheItem.attributeNamespaceIds;
			}
		}

		// Token: 0x1700505F RID: 20575
		// (get) Token: 0x060111AB RID: 70059 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060111AC RID: 70060 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "n")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005060 RID: 20576
		// (get) Token: 0x060111AD RID: 70061 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060111AE RID: 70062 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "c")]
		public StringValue DisplayName
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005061 RID: 20577
		// (get) Token: 0x060111AF RID: 70063 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060111B0 RID: 70064 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "nd")]
		public BooleanValue NonDisplay
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x060111B1 RID: 70065 RVA: 0x00293ECF File Offset: 0x002920CF
		public OlapSlicerCacheItem()
		{
		}

		// Token: 0x060111B2 RID: 70066 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OlapSlicerCacheItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060111B3 RID: 70067 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OlapSlicerCacheItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060111B4 RID: 70068 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OlapSlicerCacheItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060111B5 RID: 70069 RVA: 0x002EAA87 File Offset: 0x002E8C87
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "p" == name)
			{
				return new OlapSlicerCacheItemParent();
			}
			return null;
		}

		// Token: 0x060111B6 RID: 70070 RVA: 0x002EAAA4 File Offset: 0x002E8CA4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "n" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "c" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "nd" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060111B7 RID: 70071 RVA: 0x002EAAFB File Offset: 0x002E8CFB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OlapSlicerCacheItem>(deep);
		}

		// Token: 0x060111B8 RID: 70072 RVA: 0x002EAB04 File Offset: 0x002E8D04
		// Note: this type is marked as 'beforefieldinit'.
		static OlapSlicerCacheItem()
		{
			byte[] array = new byte[3];
			OlapSlicerCacheItem.attributeNamespaceIds = array;
		}

		// Token: 0x040077AA RID: 30634
		private const string tagName = "i";

		// Token: 0x040077AB RID: 30635
		private const byte tagNsId = 53;

		// Token: 0x040077AC RID: 30636
		internal const int ElementTypeIdConst = 13001;

		// Token: 0x040077AD RID: 30637
		private static string[] attributeTagNames = new string[] { "n", "c", "nd" };

		// Token: 0x040077AE RID: 30638
		private static byte[] attributeNamespaceIds;
	}
}
