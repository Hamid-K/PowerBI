using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FD4 RID: 12244
	[ChildElementInfo(typeof(DocPartType))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DocPartTypes : OpenXmlCompositeElement
	{
		// Token: 0x17009454 RID: 37972
		// (get) Token: 0x0601A937 RID: 108855 RVA: 0x003646A9 File Offset: 0x003628A9
		public override string LocalName
		{
			get
			{
				return "types";
			}
		}

		// Token: 0x17009455 RID: 37973
		// (get) Token: 0x0601A938 RID: 108856 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009456 RID: 37974
		// (get) Token: 0x0601A939 RID: 108857 RVA: 0x003646B0 File Offset: 0x003628B0
		internal override int ElementTypeId
		{
			get
			{
				return 11951;
			}
		}

		// Token: 0x0601A93A RID: 108858 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009457 RID: 37975
		// (get) Token: 0x0601A93B RID: 108859 RVA: 0x003646B7 File Offset: 0x003628B7
		internal override string[] AttributeTagNames
		{
			get
			{
				return DocPartTypes.attributeTagNames;
			}
		}

		// Token: 0x17009458 RID: 37976
		// (get) Token: 0x0601A93C RID: 108860 RVA: 0x003646BE File Offset: 0x003628BE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DocPartTypes.attributeNamespaceIds;
			}
		}

		// Token: 0x17009459 RID: 37977
		// (get) Token: 0x0601A93D RID: 108861 RVA: 0x002EBFC4 File Offset: 0x002EA1C4
		// (set) Token: 0x0601A93E RID: 108862 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "all")]
		public OnOffValue All
		{
			get
			{
				return (OnOffValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A93F RID: 108863 RVA: 0x00293ECF File Offset: 0x002920CF
		public DocPartTypes()
		{
		}

		// Token: 0x0601A940 RID: 108864 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DocPartTypes(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A941 RID: 108865 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DocPartTypes(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A942 RID: 108866 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DocPartTypes(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A943 RID: 108867 RVA: 0x003646C5 File Offset: 0x003628C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "type" == name)
			{
				return new DocPartType();
			}
			return null;
		}

		// Token: 0x0601A944 RID: 108868 RVA: 0x003646E0 File Offset: 0x003628E0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "all" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A945 RID: 108869 RVA: 0x00364702 File Offset: 0x00362902
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocPartTypes>(deep);
		}

		// Token: 0x0400AD95 RID: 44437
		private const string tagName = "types";

		// Token: 0x0400AD96 RID: 44438
		private const byte tagNsId = 23;

		// Token: 0x0400AD97 RID: 44439
		internal const int ElementTypeIdConst = 11951;

		// Token: 0x0400AD98 RID: 44440
		private static string[] attributeTagNames = new string[] { "all" };

		// Token: 0x0400AD99 RID: 44441
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
