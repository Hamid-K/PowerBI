using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002444 RID: 9284
	[ChildElementInfo(typeof(TabularSlicerCacheItem), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class TabularSlicerCacheItems : OpenXmlCompositeElement
	{
		// Token: 0x1700508B RID: 20619
		// (get) Token: 0x06011220 RID: 70176 RVA: 0x002EAEA3 File Offset: 0x002E90A3
		public override string LocalName
		{
			get
			{
				return "items";
			}
		}

		// Token: 0x1700508C RID: 20620
		// (get) Token: 0x06011221 RID: 70177 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x1700508D RID: 20621
		// (get) Token: 0x06011222 RID: 70178 RVA: 0x002EAEAA File Offset: 0x002E90AA
		internal override int ElementTypeId
		{
			get
			{
				return 13008;
			}
		}

		// Token: 0x06011223 RID: 70179 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700508E RID: 20622
		// (get) Token: 0x06011224 RID: 70180 RVA: 0x002EAEB1 File Offset: 0x002E90B1
		internal override string[] AttributeTagNames
		{
			get
			{
				return TabularSlicerCacheItems.attributeTagNames;
			}
		}

		// Token: 0x1700508F RID: 20623
		// (get) Token: 0x06011225 RID: 70181 RVA: 0x002EAEB8 File Offset: 0x002E90B8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TabularSlicerCacheItems.attributeNamespaceIds;
			}
		}

		// Token: 0x17005090 RID: 20624
		// (get) Token: 0x06011226 RID: 70182 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06011227 RID: 70183 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06011228 RID: 70184 RVA: 0x00293ECF File Offset: 0x002920CF
		public TabularSlicerCacheItems()
		{
		}

		// Token: 0x06011229 RID: 70185 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TabularSlicerCacheItems(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601122A RID: 70186 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TabularSlicerCacheItems(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601122B RID: 70187 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TabularSlicerCacheItems(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601122C RID: 70188 RVA: 0x002EAEBF File Offset: 0x002E90BF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "i" == name)
			{
				return new TabularSlicerCacheItem();
			}
			return null;
		}

		// Token: 0x0601122D RID: 70189 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601122E RID: 70190 RVA: 0x002EAEDA File Offset: 0x002E90DA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TabularSlicerCacheItems>(deep);
		}

		// Token: 0x0601122F RID: 70191 RVA: 0x002EAEE4 File Offset: 0x002E90E4
		// Note: this type is marked as 'beforefieldinit'.
		static TabularSlicerCacheItems()
		{
			byte[] array = new byte[1];
			TabularSlicerCacheItems.attributeNamespaceIds = array;
		}

		// Token: 0x040077CD RID: 30669
		private const string tagName = "items";

		// Token: 0x040077CE RID: 30670
		private const byte tagNsId = 53;

		// Token: 0x040077CF RID: 30671
		internal const int ElementTypeIdConst = 13008;

		// Token: 0x040077D0 RID: 30672
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x040077D1 RID: 30673
		private static byte[] attributeNamespaceIds;
	}
}
