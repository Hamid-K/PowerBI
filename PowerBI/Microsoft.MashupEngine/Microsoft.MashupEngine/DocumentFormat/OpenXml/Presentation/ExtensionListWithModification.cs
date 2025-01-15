using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A44 RID: 10820
	[ChildElementInfo(typeof(Extension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ExtensionListWithModification : OpenXmlCompositeElement
	{
		// Token: 0x17007174 RID: 29044
		// (get) Token: 0x06015C85 RID: 89221 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17007175 RID: 29045
		// (get) Token: 0x06015C86 RID: 89222 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007176 RID: 29046
		// (get) Token: 0x06015C87 RID: 89223 RVA: 0x003231CF File Offset: 0x003213CF
		internal override int ElementTypeId
		{
			get
			{
				return 12239;
			}
		}

		// Token: 0x06015C88 RID: 89224 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007177 RID: 29047
		// (get) Token: 0x06015C89 RID: 89225 RVA: 0x003231D6 File Offset: 0x003213D6
		internal override string[] AttributeTagNames
		{
			get
			{
				return ExtensionListWithModification.attributeTagNames;
			}
		}

		// Token: 0x17007178 RID: 29048
		// (get) Token: 0x06015C8A RID: 89226 RVA: 0x003231DD File Offset: 0x003213DD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ExtensionListWithModification.attributeNamespaceIds;
			}
		}

		// Token: 0x17007179 RID: 29049
		// (get) Token: 0x06015C8B RID: 89227 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06015C8C RID: 89228 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "mod")]
		public BooleanValue Modify
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015C8D RID: 89229 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExtensionListWithModification()
		{
		}

		// Token: 0x06015C8E RID: 89230 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExtensionListWithModification(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015C8F RID: 89231 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExtensionListWithModification(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015C90 RID: 89232 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExtensionListWithModification(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015C91 RID: 89233 RVA: 0x002E3E40 File Offset: 0x002E2040
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x06015C92 RID: 89234 RVA: 0x002E3E5B File Offset: 0x002E205B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "mod" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015C93 RID: 89235 RVA: 0x003231E4 File Offset: 0x003213E4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExtensionListWithModification>(deep);
		}

		// Token: 0x06015C94 RID: 89236 RVA: 0x003231F0 File Offset: 0x003213F0
		// Note: this type is marked as 'beforefieldinit'.
		static ExtensionListWithModification()
		{
			byte[] array = new byte[1];
			ExtensionListWithModification.attributeNamespaceIds = array;
		}

		// Token: 0x040094CA RID: 38090
		private const string tagName = "extLst";

		// Token: 0x040094CB RID: 38091
		private const byte tagNsId = 24;

		// Token: 0x040094CC RID: 38092
		internal const int ElementTypeIdConst = 12239;

		// Token: 0x040094CD RID: 38093
		private static string[] attributeTagNames = new string[] { "mod" };

		// Token: 0x040094CE RID: 38094
		private static byte[] attributeNamespaceIds;
	}
}
