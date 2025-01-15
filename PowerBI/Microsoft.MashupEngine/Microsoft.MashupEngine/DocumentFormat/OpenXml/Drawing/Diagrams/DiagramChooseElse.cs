using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002675 RID: 9845
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Algorithm))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(PresentationOf))]
	[ChildElementInfo(typeof(Constraints))]
	[ChildElementInfo(typeof(RuleList))]
	[ChildElementInfo(typeof(ForEach))]
	[ChildElementInfo(typeof(LayoutNode))]
	[ChildElementInfo(typeof(Choose))]
	internal class DiagramChooseElse : OpenXmlCompositeElement
	{
		// Token: 0x17005C5E RID: 23646
		// (get) Token: 0x06012CB0 RID: 76976 RVA: 0x002FF847 File Offset: 0x002FDA47
		public override string LocalName
		{
			get
			{
				return "else";
			}
		}

		// Token: 0x17005C5F RID: 23647
		// (get) Token: 0x06012CB1 RID: 76977 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C60 RID: 23648
		// (get) Token: 0x06012CB2 RID: 76978 RVA: 0x002FF84E File Offset: 0x002FDA4E
		internal override int ElementTypeId
		{
			get
			{
				return 10660;
			}
		}

		// Token: 0x06012CB3 RID: 76979 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005C61 RID: 23649
		// (get) Token: 0x06012CB4 RID: 76980 RVA: 0x002FF855 File Offset: 0x002FDA55
		internal override string[] AttributeTagNames
		{
			get
			{
				return DiagramChooseElse.attributeTagNames;
			}
		}

		// Token: 0x17005C62 RID: 23650
		// (get) Token: 0x06012CB5 RID: 76981 RVA: 0x002FF85C File Offset: 0x002FDA5C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DiagramChooseElse.attributeNamespaceIds;
			}
		}

		// Token: 0x17005C63 RID: 23651
		// (get) Token: 0x06012CB6 RID: 76982 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012CB7 RID: 76983 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012CB8 RID: 76984 RVA: 0x00293ECF File Offset: 0x002920CF
		public DiagramChooseElse()
		{
		}

		// Token: 0x06012CB9 RID: 76985 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DiagramChooseElse(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012CBA RID: 76986 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DiagramChooseElse(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012CBB RID: 76987 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DiagramChooseElse(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012CBC RID: 76988 RVA: 0x002FF864 File Offset: 0x002FDA64
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

		// Token: 0x06012CBD RID: 76989 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012CBE RID: 76990 RVA: 0x002FF94A File Offset: 0x002FDB4A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DiagramChooseElse>(deep);
		}

		// Token: 0x06012CBF RID: 76991 RVA: 0x002FF954 File Offset: 0x002FDB54
		// Note: this type is marked as 'beforefieldinit'.
		static DiagramChooseElse()
		{
			byte[] array = new byte[1];
			DiagramChooseElse.attributeNamespaceIds = array;
		}

		// Token: 0x04008198 RID: 33176
		private const string tagName = "else";

		// Token: 0x04008199 RID: 33177
		private const byte tagNsId = 14;

		// Token: 0x0400819A RID: 33178
		internal const int ElementTypeIdConst = 10660;

		// Token: 0x0400819B RID: 33179
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x0400819C RID: 33180
		private static byte[] attributeNamespaceIds;
	}
}
