using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x0200307F RID: 12415
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Info))]
	internal class Node : OpenXmlCompositeElement
	{
		// Token: 0x17009732 RID: 38706
		// (get) Token: 0x0601AF6E RID: 110446 RVA: 0x0036A05C File Offset: 0x0036825C
		public override string LocalName
		{
			get
			{
				return "node";
			}
		}

		// Token: 0x17009733 RID: 38707
		// (get) Token: 0x0601AF6F RID: 110447 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x17009734 RID: 38708
		// (get) Token: 0x0601AF70 RID: 110448 RVA: 0x0036A063 File Offset: 0x00368263
		internal override int ElementTypeId
		{
			get
			{
				return 12684;
			}
		}

		// Token: 0x0601AF71 RID: 110449 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009735 RID: 38709
		// (get) Token: 0x0601AF72 RID: 110450 RVA: 0x0036A06A File Offset: 0x0036826A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Node.attributeTagNames;
			}
		}

		// Token: 0x17009736 RID: 38710
		// (get) Token: 0x0601AF73 RID: 110451 RVA: 0x0036A071 File Offset: 0x00368271
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Node.attributeNamespaceIds;
			}
		}

		// Token: 0x17009737 RID: 38711
		// (get) Token: 0x0601AF74 RID: 110452 RVA: 0x002EC050 File Offset: 0x002EA250
		// (set) Token: 0x0601AF75 RID: 110453 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "node-number")]
		public IntegerValue NodeNumber
		{
			get
			{
				return (IntegerValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17009738 RID: 38712
		// (get) Token: 0x0601AF76 RID: 110454 RVA: 0x0036A078 File Offset: 0x00368278
		// (set) Token: 0x0601AF77 RID: 110455 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(44, "confidence")]
		public DecimalValue Confidence
		{
			get
			{
				return (DecimalValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009739 RID: 38713
		// (get) Token: 0x0601AF78 RID: 110456 RVA: 0x0036A087 File Offset: 0x00368287
		// (set) Token: 0x0601AF79 RID: 110457 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(44, "cost")]
		public DecimalValue Cost
		{
			get
			{
				return (DecimalValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601AF7A RID: 110458 RVA: 0x00293ECF File Offset: 0x002920CF
		public Node()
		{
		}

		// Token: 0x0601AF7B RID: 110459 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Node(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AF7C RID: 110460 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Node(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AF7D RID: 110461 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Node(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AF7E RID: 110462 RVA: 0x0036A096 File Offset: 0x00368296
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (44 == namespaceId && "info" == name)
			{
				return new Info();
			}
			return null;
		}

		// Token: 0x0601AF7F RID: 110463 RVA: 0x0036A0B4 File Offset: 0x003682B4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "node-number" == name)
			{
				return new IntegerValue();
			}
			if (44 == namespaceId && "confidence" == name)
			{
				return new DecimalValue();
			}
			if (44 == namespaceId && "cost" == name)
			{
				return new DecimalValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AF80 RID: 110464 RVA: 0x0036A10F File Offset: 0x0036830F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Node>(deep);
		}

		// Token: 0x0400B24D RID: 45645
		private const string tagName = "node";

		// Token: 0x0400B24E RID: 45646
		private const byte tagNsId = 44;

		// Token: 0x0400B24F RID: 45647
		internal const int ElementTypeIdConst = 12684;

		// Token: 0x0400B250 RID: 45648
		private static string[] attributeTagNames = new string[] { "node-number", "confidence", "cost" };

		// Token: 0x0400B251 RID: 45649
		private static byte[] attributeNamespaceIds = new byte[] { 0, 44, 44 };
	}
}
