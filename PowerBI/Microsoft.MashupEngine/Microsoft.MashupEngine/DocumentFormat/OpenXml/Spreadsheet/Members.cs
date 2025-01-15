using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CA7 RID: 11431
	[ChildElementInfo(typeof(Member))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Members : OpenXmlCompositeElement
	{
		// Token: 0x17008454 RID: 33876
		// (get) Token: 0x060186B5 RID: 100021 RVA: 0x00341617 File Offset: 0x0033F817
		public override string LocalName
		{
			get
			{
				return "members";
			}
		}

		// Token: 0x17008455 RID: 33877
		// (get) Token: 0x060186B6 RID: 100022 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008456 RID: 33878
		// (get) Token: 0x060186B7 RID: 100023 RVA: 0x0034161E File Offset: 0x0033F81E
		internal override int ElementTypeId
		{
			get
			{
				return 11411;
			}
		}

		// Token: 0x060186B8 RID: 100024 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008457 RID: 33879
		// (get) Token: 0x060186B9 RID: 100025 RVA: 0x00341625 File Offset: 0x0033F825
		internal override string[] AttributeTagNames
		{
			get
			{
				return Members.attributeTagNames;
			}
		}

		// Token: 0x17008458 RID: 33880
		// (get) Token: 0x060186BA RID: 100026 RVA: 0x0034162C File Offset: 0x0033F82C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Members.attributeNamespaceIds;
			}
		}

		// Token: 0x17008459 RID: 33881
		// (get) Token: 0x060186BB RID: 100027 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060186BC RID: 100028 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700845A RID: 33882
		// (get) Token: 0x060186BD RID: 100029 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x060186BE RID: 100030 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "level")]
		public UInt32Value Level
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

		// Token: 0x060186BF RID: 100031 RVA: 0x00293ECF File Offset: 0x002920CF
		public Members()
		{
		}

		// Token: 0x060186C0 RID: 100032 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Members(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186C1 RID: 100033 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Members(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186C2 RID: 100034 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Members(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060186C3 RID: 100035 RVA: 0x00341633 File Offset: 0x0033F833
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "member" == name)
			{
				return new Member();
			}
			return null;
		}

		// Token: 0x060186C4 RID: 100036 RVA: 0x0034164E File Offset: 0x0033F84E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "level" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060186C5 RID: 100037 RVA: 0x00341684 File Offset: 0x0033F884
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Members>(deep);
		}

		// Token: 0x060186C6 RID: 100038 RVA: 0x00341690 File Offset: 0x0033F890
		// Note: this type is marked as 'beforefieldinit'.
		static Members()
		{
			byte[] array = new byte[2];
			Members.attributeNamespaceIds = array;
		}

		// Token: 0x0400A02E RID: 41006
		private const string tagName = "members";

		// Token: 0x0400A02F RID: 41007
		private const byte tagNsId = 22;

		// Token: 0x0400A030 RID: 41008
		internal const int ElementTypeIdConst = 11411;

		// Token: 0x0400A031 RID: 41009
		private static string[] attributeTagNames = new string[] { "count", "level" };

		// Token: 0x0400A032 RID: 41010
		private static byte[] attributeNamespaceIds;
	}
}
