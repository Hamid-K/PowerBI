using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002672 RID: 9842
	[ChildElementInfo(typeof(VariableList))]
	[ChildElementInfo(typeof(RuleList))]
	[ChildElementInfo(typeof(LayoutNode))]
	[ChildElementInfo(typeof(Choose))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Algorithm))]
	[ChildElementInfo(typeof(ForEach))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(PresentationOf))]
	[ChildElementInfo(typeof(Constraints))]
	internal class LayoutNode : OpenXmlCompositeElement
	{
		// Token: 0x17005C3F RID: 23615
		// (get) Token: 0x06012C66 RID: 76902 RVA: 0x002FF2C3 File Offset: 0x002FD4C3
		public override string LocalName
		{
			get
			{
				return "layoutNode";
			}
		}

		// Token: 0x17005C40 RID: 23616
		// (get) Token: 0x06012C67 RID: 76903 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C41 RID: 23617
		// (get) Token: 0x06012C68 RID: 76904 RVA: 0x002FF2CA File Offset: 0x002FD4CA
		internal override int ElementTypeId
		{
			get
			{
				return 10657;
			}
		}

		// Token: 0x06012C69 RID: 76905 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005C42 RID: 23618
		// (get) Token: 0x06012C6A RID: 76906 RVA: 0x002FF2D1 File Offset: 0x002FD4D1
		internal override string[] AttributeTagNames
		{
			get
			{
				return LayoutNode.attributeTagNames;
			}
		}

		// Token: 0x17005C43 RID: 23619
		// (get) Token: 0x06012C6B RID: 76907 RVA: 0x002FF2D8 File Offset: 0x002FD4D8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LayoutNode.attributeNamespaceIds;
			}
		}

		// Token: 0x17005C44 RID: 23620
		// (get) Token: 0x06012C6C RID: 76908 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012C6D RID: 76909 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
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

		// Token: 0x17005C45 RID: 23621
		// (get) Token: 0x06012C6E RID: 76910 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012C6F RID: 76911 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "styleLbl")]
		public StringValue StyleLabel
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

		// Token: 0x17005C46 RID: 23622
		// (get) Token: 0x06012C70 RID: 76912 RVA: 0x002FF2DF File Offset: 0x002FD4DF
		// (set) Token: 0x06012C71 RID: 76913 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "chOrder")]
		public EnumValue<ChildOrderValues> ChildOrder
		{
			get
			{
				return (EnumValue<ChildOrderValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005C47 RID: 23623
		// (get) Token: 0x06012C72 RID: 76914 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06012C73 RID: 76915 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "moveWith")]
		public StringValue MoveWith
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06012C74 RID: 76916 RVA: 0x00293ECF File Offset: 0x002920CF
		public LayoutNode()
		{
		}

		// Token: 0x06012C75 RID: 76917 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LayoutNode(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C76 RID: 76918 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LayoutNode(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C77 RID: 76919 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LayoutNode(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012C78 RID: 76920 RVA: 0x002FF2F0 File Offset: 0x002FD4F0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "alg" == name)
			{
				return new Algorithm();
			}
			if (14 == namespaceId && "shape" == name)
			{
				return new Shape();
			}
			if (14 == namespaceId && "presOf" == name)
			{
				return new PresentationOf();
			}
			if (14 == namespaceId && "constrLst" == name)
			{
				return new Constraints();
			}
			if (14 == namespaceId && "ruleLst" == name)
			{
				return new RuleList();
			}
			if (14 == namespaceId && "varLst" == name)
			{
				return new VariableList();
			}
			if (14 == namespaceId && "forEach" == name)
			{
				return new ForEach();
			}
			if (14 == namespaceId && "layoutNode" == name)
			{
				return new LayoutNode();
			}
			if (14 == namespaceId && "choose" == name)
			{
				return new Choose();
			}
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06012C79 RID: 76921 RVA: 0x002FF3F0 File Offset: 0x002FD5F0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "styleLbl" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "chOrder" == name)
			{
				return new EnumValue<ChildOrderValues>();
			}
			if (namespaceId == 0 && "moveWith" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012C7A RID: 76922 RVA: 0x002FF45D File Offset: 0x002FD65D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LayoutNode>(deep);
		}

		// Token: 0x06012C7B RID: 76923 RVA: 0x002FF468 File Offset: 0x002FD668
		// Note: this type is marked as 'beforefieldinit'.
		static LayoutNode()
		{
			byte[] array = new byte[4];
			LayoutNode.attributeNamespaceIds = array;
		}

		// Token: 0x04008189 RID: 33161
		private const string tagName = "layoutNode";

		// Token: 0x0400818A RID: 33162
		private const byte tagNsId = 14;

		// Token: 0x0400818B RID: 33163
		internal const int ElementTypeIdConst = 10657;

		// Token: 0x0400818C RID: 33164
		private static string[] attributeTagNames = new string[] { "name", "styleLbl", "chOrder", "moveWith" };

		// Token: 0x0400818D RID: 33165
		private static byte[] attributeNamespaceIds;
	}
}
