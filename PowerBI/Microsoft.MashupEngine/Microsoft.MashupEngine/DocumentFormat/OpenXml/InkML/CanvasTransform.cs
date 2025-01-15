using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003098 RID: 12440
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Mapping))]
	internal class CanvasTransform : OpenXmlCompositeElement
	{
		// Token: 0x170097DD RID: 38877
		// (get) Token: 0x0601B0F7 RID: 110839 RVA: 0x0036B396 File Offset: 0x00369596
		public override string LocalName
		{
			get
			{
				return "canvasTransform";
			}
		}

		// Token: 0x170097DE RID: 38878
		// (get) Token: 0x0601B0F8 RID: 110840 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x170097DF RID: 38879
		// (get) Token: 0x0601B0F9 RID: 110841 RVA: 0x0036B39D File Offset: 0x0036959D
		internal override int ElementTypeId
		{
			get
			{
				return 12661;
			}
		}

		// Token: 0x0601B0FA RID: 110842 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170097E0 RID: 38880
		// (get) Token: 0x0601B0FB RID: 110843 RVA: 0x0036B3A4 File Offset: 0x003695A4
		internal override string[] AttributeTagNames
		{
			get
			{
				return CanvasTransform.attributeTagNames;
			}
		}

		// Token: 0x170097E1 RID: 38881
		// (get) Token: 0x0601B0FC RID: 110844 RVA: 0x0036B3AB File Offset: 0x003695AB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CanvasTransform.attributeNamespaceIds;
			}
		}

		// Token: 0x170097E2 RID: 38882
		// (get) Token: 0x0601B0FD RID: 110845 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B0FE RID: 110846 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(1, "id")]
		public StringValue Id
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

		// Token: 0x170097E3 RID: 38883
		// (get) Token: 0x0601B0FF RID: 110847 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601B100 RID: 110848 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "invertible")]
		public BooleanValue Invertible
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601B101 RID: 110849 RVA: 0x00293ECF File Offset: 0x002920CF
		public CanvasTransform()
		{
		}

		// Token: 0x0601B102 RID: 110850 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CanvasTransform(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B103 RID: 110851 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CanvasTransform(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B104 RID: 110852 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CanvasTransform(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B105 RID: 110853 RVA: 0x0036A9E3 File Offset: 0x00368BE3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (43 == namespaceId && "mapping" == name)
			{
				return new Mapping();
			}
			return null;
		}

		// Token: 0x0601B106 RID: 110854 RVA: 0x0036B3B2 File Offset: 0x003695B2
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "invertible" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B107 RID: 110855 RVA: 0x0036B3E9 File Offset: 0x003695E9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CanvasTransform>(deep);
		}

		// Token: 0x0601B108 RID: 110856 RVA: 0x0036B3F4 File Offset: 0x003695F4
		// Note: this type is marked as 'beforefieldinit'.
		static CanvasTransform()
		{
			byte[] array = new byte[2];
			array[0] = 1;
			CanvasTransform.attributeNamespaceIds = array;
		}

		// Token: 0x0400B2C7 RID: 45767
		private const string tagName = "canvasTransform";

		// Token: 0x0400B2C8 RID: 45768
		private const byte tagNsId = 43;

		// Token: 0x0400B2C9 RID: 45769
		internal const int ElementTypeIdConst = 12661;

		// Token: 0x0400B2CA RID: 45770
		private static string[] attributeTagNames = new string[] { "id", "invertible" };

		// Token: 0x0400B2CB RID: 45771
		private static byte[] attributeNamespaceIds;
	}
}
