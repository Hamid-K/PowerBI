using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CAA RID: 11434
	[ChildElementInfo(typeof(Item))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Items : OpenXmlCompositeElement
	{
		// Token: 0x17008461 RID: 33889
		// (get) Token: 0x060186DB RID: 100059 RVA: 0x002EAEA3 File Offset: 0x002E90A3
		public override string LocalName
		{
			get
			{
				return "items";
			}
		}

		// Token: 0x17008462 RID: 33890
		// (get) Token: 0x060186DC RID: 100060 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008463 RID: 33891
		// (get) Token: 0x060186DD RID: 100061 RVA: 0x0034171D File Offset: 0x0033F91D
		internal override int ElementTypeId
		{
			get
			{
				return 11414;
			}
		}

		// Token: 0x060186DE RID: 100062 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008464 RID: 33892
		// (get) Token: 0x060186DF RID: 100063 RVA: 0x00341724 File Offset: 0x0033F924
		internal override string[] AttributeTagNames
		{
			get
			{
				return Items.attributeTagNames;
			}
		}

		// Token: 0x17008465 RID: 33893
		// (get) Token: 0x060186E0 RID: 100064 RVA: 0x0034172B File Offset: 0x0033F92B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Items.attributeNamespaceIds;
			}
		}

		// Token: 0x17008466 RID: 33894
		// (get) Token: 0x060186E1 RID: 100065 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060186E2 RID: 100066 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060186E3 RID: 100067 RVA: 0x00293ECF File Offset: 0x002920CF
		public Items()
		{
		}

		// Token: 0x060186E4 RID: 100068 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Items(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186E5 RID: 100069 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Items(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186E6 RID: 100070 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Items(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060186E7 RID: 100071 RVA: 0x00341732 File Offset: 0x0033F932
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "item" == name)
			{
				return new Item();
			}
			return null;
		}

		// Token: 0x060186E8 RID: 100072 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060186E9 RID: 100073 RVA: 0x0034174D File Offset: 0x0033F94D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Items>(deep);
		}

		// Token: 0x060186EA RID: 100074 RVA: 0x00341758 File Offset: 0x0033F958
		// Note: this type is marked as 'beforefieldinit'.
		static Items()
		{
			byte[] array = new byte[1];
			Items.attributeNamespaceIds = array;
		}

		// Token: 0x0400A039 RID: 41017
		private const string tagName = "items";

		// Token: 0x0400A03A RID: 41018
		private const byte tagNsId = 22;

		// Token: 0x0400A03B RID: 41019
		internal const int ElementTypeIdConst = 11414;

		// Token: 0x0400A03C RID: 41020
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A03D RID: 41021
		private static byte[] attributeNamespaceIds;
	}
}
