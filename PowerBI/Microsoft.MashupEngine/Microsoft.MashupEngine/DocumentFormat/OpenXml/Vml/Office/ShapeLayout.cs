using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002202 RID: 8706
	[ChildElementInfo(typeof(ShapeIdMap))]
	[ChildElementInfo(typeof(RegroupTable))]
	[ChildElementInfo(typeof(Rules))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeLayout : OpenXmlCompositeElement
	{
		// Token: 0x17003824 RID: 14372
		// (get) Token: 0x0600DDDD RID: 56797 RVA: 0x002BD95A File Offset: 0x002BBB5A
		public override string LocalName
		{
			get
			{
				return "shapelayout";
			}
		}

		// Token: 0x17003825 RID: 14373
		// (get) Token: 0x0600DDDE RID: 56798 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x17003826 RID: 14374
		// (get) Token: 0x0600DDDF RID: 56799 RVA: 0x002BD961 File Offset: 0x002BBB61
		internal override int ElementTypeId
		{
			get
			{
				return 12400;
			}
		}

		// Token: 0x0600DDE0 RID: 56800 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003827 RID: 14375
		// (get) Token: 0x0600DDE1 RID: 56801 RVA: 0x002BD968 File Offset: 0x002BBB68
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeLayout.attributeTagNames;
			}
		}

		// Token: 0x17003828 RID: 14376
		// (get) Token: 0x0600DDE2 RID: 56802 RVA: 0x002BD96F File Offset: 0x002BBB6F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeLayout.attributeNamespaceIds;
			}
		}

		// Token: 0x17003829 RID: 14377
		// (get) Token: 0x0600DDE3 RID: 56803 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DDE4 RID: 56804 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(26, "ext")]
		public EnumValue<ExtensionHandlingBehaviorValues> Extension
		{
			get
			{
				return (EnumValue<ExtensionHandlingBehaviorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0600DDE5 RID: 56805 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeLayout()
		{
		}

		// Token: 0x0600DDE6 RID: 56806 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeLayout(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DDE7 RID: 56807 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeLayout(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DDE8 RID: 56808 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeLayout(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600DDE9 RID: 56809 RVA: 0x002BD978 File Offset: 0x002BBB78
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (27 == namespaceId && "idmap" == name)
			{
				return new ShapeIdMap();
			}
			if (27 == namespaceId && "regrouptable" == name)
			{
				return new RegroupTable();
			}
			if (27 == namespaceId && "rules" == name)
			{
				return new Rules();
			}
			return null;
		}

		// Token: 0x1700382A RID: 14378
		// (get) Token: 0x0600DDEA RID: 56810 RVA: 0x002BD9CE File Offset: 0x002BBBCE
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeLayout.eleTagNames;
			}
		}

		// Token: 0x1700382B RID: 14379
		// (get) Token: 0x0600DDEB RID: 56811 RVA: 0x002BD9D5 File Offset: 0x002BBBD5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeLayout.eleNamespaceIds;
			}
		}

		// Token: 0x1700382C RID: 14380
		// (get) Token: 0x0600DDEC RID: 56812 RVA: 0x0000240C File Offset: 0x0000060C
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneAll;
			}
		}

		// Token: 0x1700382D RID: 14381
		// (get) Token: 0x0600DDED RID: 56813 RVA: 0x002BD9DC File Offset: 0x002BBBDC
		// (set) Token: 0x0600DDEE RID: 56814 RVA: 0x002BD9E5 File Offset: 0x002BBBE5
		public ShapeIdMap ShapeIdMap
		{
			get
			{
				return base.GetElement<ShapeIdMap>(0);
			}
			set
			{
				base.SetElement<ShapeIdMap>(0, value);
			}
		}

		// Token: 0x1700382E RID: 14382
		// (get) Token: 0x0600DDEF RID: 56815 RVA: 0x002BD9EF File Offset: 0x002BBBEF
		// (set) Token: 0x0600DDF0 RID: 56816 RVA: 0x002BD9F8 File Offset: 0x002BBBF8
		public RegroupTable RegroupTable
		{
			get
			{
				return base.GetElement<RegroupTable>(1);
			}
			set
			{
				base.SetElement<RegroupTable>(1, value);
			}
		}

		// Token: 0x1700382F RID: 14383
		// (get) Token: 0x0600DDF1 RID: 56817 RVA: 0x002BDA02 File Offset: 0x002BBC02
		// (set) Token: 0x0600DDF2 RID: 56818 RVA: 0x002BDA0B File Offset: 0x002BBC0B
		public Rules Rules
		{
			get
			{
				return base.GetElement<Rules>(2);
			}
			set
			{
				base.SetElement<Rules>(2, value);
			}
		}

		// Token: 0x0600DDF3 RID: 56819 RVA: 0x002BDA15 File Offset: 0x002BBC15
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DDF4 RID: 56820 RVA: 0x002BDA37 File Offset: 0x002BBC37
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeLayout>(deep);
		}

		// Token: 0x04006D55 RID: 27989
		private const string tagName = "shapelayout";

		// Token: 0x04006D56 RID: 27990
		private const byte tagNsId = 27;

		// Token: 0x04006D57 RID: 27991
		internal const int ElementTypeIdConst = 12400;

		// Token: 0x04006D58 RID: 27992
		private static string[] attributeTagNames = new string[] { "ext" };

		// Token: 0x04006D59 RID: 27993
		private static byte[] attributeNamespaceIds = new byte[] { 26 };

		// Token: 0x04006D5A RID: 27994
		private static readonly string[] eleTagNames = new string[] { "idmap", "regrouptable", "rules" };

		// Token: 0x04006D5B RID: 27995
		private static readonly byte[] eleNamespaceIds = new byte[] { 27, 27, 27 };
	}
}
