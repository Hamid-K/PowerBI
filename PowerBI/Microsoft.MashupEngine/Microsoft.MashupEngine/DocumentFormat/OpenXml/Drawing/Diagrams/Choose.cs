using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002673 RID: 9843
	[ChildElementInfo(typeof(DiagramChooseElse))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DiagramChooseIf))]
	internal class Choose : OpenXmlCompositeElement
	{
		// Token: 0x17005C48 RID: 23624
		// (get) Token: 0x06012C7C RID: 76924 RVA: 0x002FF4AF File Offset: 0x002FD6AF
		public override string LocalName
		{
			get
			{
				return "choose";
			}
		}

		// Token: 0x17005C49 RID: 23625
		// (get) Token: 0x06012C7D RID: 76925 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C4A RID: 23626
		// (get) Token: 0x06012C7E RID: 76926 RVA: 0x002FF4B6 File Offset: 0x002FD6B6
		internal override int ElementTypeId
		{
			get
			{
				return 10658;
			}
		}

		// Token: 0x06012C7F RID: 76927 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005C4B RID: 23627
		// (get) Token: 0x06012C80 RID: 76928 RVA: 0x002FF4BD File Offset: 0x002FD6BD
		internal override string[] AttributeTagNames
		{
			get
			{
				return Choose.attributeTagNames;
			}
		}

		// Token: 0x17005C4C RID: 23628
		// (get) Token: 0x06012C81 RID: 76929 RVA: 0x002FF4C4 File Offset: 0x002FD6C4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Choose.attributeNamespaceIds;
			}
		}

		// Token: 0x17005C4D RID: 23629
		// (get) Token: 0x06012C82 RID: 76930 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012C83 RID: 76931 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012C84 RID: 76932 RVA: 0x00293ECF File Offset: 0x002920CF
		public Choose()
		{
		}

		// Token: 0x06012C85 RID: 76933 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Choose(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C86 RID: 76934 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Choose(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C87 RID: 76935 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Choose(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012C88 RID: 76936 RVA: 0x002FF4CB File Offset: 0x002FD6CB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "if" == name)
			{
				return new DiagramChooseIf();
			}
			if (14 == namespaceId && "else" == name)
			{
				return new DiagramChooseElse();
			}
			return null;
		}

		// Token: 0x06012C89 RID: 76937 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012C8A RID: 76938 RVA: 0x002FF4FE File Offset: 0x002FD6FE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Choose>(deep);
		}

		// Token: 0x06012C8B RID: 76939 RVA: 0x002FF508 File Offset: 0x002FD708
		// Note: this type is marked as 'beforefieldinit'.
		static Choose()
		{
			byte[] array = new byte[1];
			Choose.attributeNamespaceIds = array;
		}

		// Token: 0x0400818E RID: 33166
		private const string tagName = "choose";

		// Token: 0x0400818F RID: 33167
		private const byte tagNsId = 14;

		// Token: 0x04008190 RID: 33168
		internal const int ElementTypeIdConst = 10658;

		// Token: 0x04008191 RID: 33169
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x04008192 RID: 33170
		private static byte[] attributeNamespaceIds;
	}
}
