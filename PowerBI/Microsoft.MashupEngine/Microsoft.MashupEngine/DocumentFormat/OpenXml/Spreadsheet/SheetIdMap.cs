using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BBD RID: 11197
	[ChildElementInfo(typeof(SheetId))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SheetIdMap : OpenXmlCompositeElement
	{
		// Token: 0x17007C5A RID: 31834
		// (get) Token: 0x060174C8 RID: 95432 RVA: 0x00335247 File Offset: 0x00333447
		public override string LocalName
		{
			get
			{
				return "sheetIdMap";
			}
		}

		// Token: 0x17007C5B RID: 31835
		// (get) Token: 0x060174C9 RID: 95433 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007C5C RID: 31836
		// (get) Token: 0x060174CA RID: 95434 RVA: 0x0033524E File Offset: 0x0033344E
		internal override int ElementTypeId
		{
			get
			{
				return 11168;
			}
		}

		// Token: 0x060174CB RID: 95435 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007C5D RID: 31837
		// (get) Token: 0x060174CC RID: 95436 RVA: 0x00335255 File Offset: 0x00333455
		internal override string[] AttributeTagNames
		{
			get
			{
				return SheetIdMap.attributeTagNames;
			}
		}

		// Token: 0x17007C5E RID: 31838
		// (get) Token: 0x060174CD RID: 95437 RVA: 0x0033525C File Offset: 0x0033345C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SheetIdMap.attributeNamespaceIds;
			}
		}

		// Token: 0x17007C5F RID: 31839
		// (get) Token: 0x060174CE RID: 95438 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060174CF RID: 95439 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060174D0 RID: 95440 RVA: 0x00293ECF File Offset: 0x002920CF
		public SheetIdMap()
		{
		}

		// Token: 0x060174D1 RID: 95441 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SheetIdMap(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060174D2 RID: 95442 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SheetIdMap(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060174D3 RID: 95443 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SheetIdMap(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060174D4 RID: 95444 RVA: 0x00335263 File Offset: 0x00333463
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "sheetId" == name)
			{
				return new SheetId();
			}
			return null;
		}

		// Token: 0x060174D5 RID: 95445 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060174D6 RID: 95446 RVA: 0x0033527E File Offset: 0x0033347E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SheetIdMap>(deep);
		}

		// Token: 0x060174D7 RID: 95447 RVA: 0x00335288 File Offset: 0x00333488
		// Note: this type is marked as 'beforefieldinit'.
		static SheetIdMap()
		{
			byte[] array = new byte[1];
			SheetIdMap.attributeNamespaceIds = array;
		}

		// Token: 0x04009BDA RID: 39898
		private const string tagName = "sheetIdMap";

		// Token: 0x04009BDB RID: 39899
		private const byte tagNsId = 22;

		// Token: 0x04009BDC RID: 39900
		internal const int ElementTypeIdConst = 11168;

		// Token: 0x04009BDD RID: 39901
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009BDE RID: 39902
		private static byte[] attributeNamespaceIds;
	}
}
