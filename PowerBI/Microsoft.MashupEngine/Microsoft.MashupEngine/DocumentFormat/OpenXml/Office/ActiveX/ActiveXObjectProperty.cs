using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.ActiveX
{
	// Token: 0x0200229F RID: 8863
	[ChildElementInfo(typeof(SharedComPicture))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SharedComFont))]
	internal class ActiveXObjectProperty : OpenXmlCompositeElement
	{
		// Token: 0x170040F4 RID: 16628
		// (get) Token: 0x0600F042 RID: 61506 RVA: 0x002D08D4 File Offset: 0x002CEAD4
		public override string LocalName
		{
			get
			{
				return "ocxPr";
			}
		}

		// Token: 0x170040F5 RID: 16629
		// (get) Token: 0x0600F043 RID: 61507 RVA: 0x002D07C1 File Offset: 0x002CE9C1
		internal override byte NamespaceId
		{
			get
			{
				return 35;
			}
		}

		// Token: 0x170040F6 RID: 16630
		// (get) Token: 0x0600F044 RID: 61508 RVA: 0x002D08DB File Offset: 0x002CEADB
		internal override int ElementTypeId
		{
			get
			{
				return 12618;
			}
		}

		// Token: 0x0600F045 RID: 61509 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170040F7 RID: 16631
		// (get) Token: 0x0600F046 RID: 61510 RVA: 0x002D08E2 File Offset: 0x002CEAE2
		internal override string[] AttributeTagNames
		{
			get
			{
				return ActiveXObjectProperty.attributeTagNames;
			}
		}

		// Token: 0x170040F8 RID: 16632
		// (get) Token: 0x0600F047 RID: 61511 RVA: 0x002D08E9 File Offset: 0x002CEAE9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ActiveXObjectProperty.attributeNamespaceIds;
			}
		}

		// Token: 0x170040F9 RID: 16633
		// (get) Token: 0x0600F048 RID: 61512 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F049 RID: 61513 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(35, "name")]
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

		// Token: 0x170040FA RID: 16634
		// (get) Token: 0x0600F04A RID: 61514 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F04B RID: 61515 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(35, "value")]
		public StringValue Value
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

		// Token: 0x0600F04C RID: 61516 RVA: 0x00293ECF File Offset: 0x002920CF
		public ActiveXObjectProperty()
		{
		}

		// Token: 0x0600F04D RID: 61517 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ActiveXObjectProperty(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F04E RID: 61518 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ActiveXObjectProperty(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F04F RID: 61519 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ActiveXObjectProperty(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F050 RID: 61520 RVA: 0x002D08F0 File Offset: 0x002CEAF0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (35 == namespaceId && "font" == name)
			{
				return new SharedComFont();
			}
			if (35 == namespaceId && "picture" == name)
			{
				return new SharedComPicture();
			}
			return null;
		}

		// Token: 0x170040FB RID: 16635
		// (get) Token: 0x0600F051 RID: 61521 RVA: 0x002D0923 File Offset: 0x002CEB23
		internal override string[] ElementTagNames
		{
			get
			{
				return ActiveXObjectProperty.eleTagNames;
			}
		}

		// Token: 0x170040FC RID: 16636
		// (get) Token: 0x0600F052 RID: 61522 RVA: 0x002D092A File Offset: 0x002CEB2A
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ActiveXObjectProperty.eleNamespaceIds;
			}
		}

		// Token: 0x170040FD RID: 16637
		// (get) Token: 0x0600F053 RID: 61523 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170040FE RID: 16638
		// (get) Token: 0x0600F054 RID: 61524 RVA: 0x002D0931 File Offset: 0x002CEB31
		// (set) Token: 0x0600F055 RID: 61525 RVA: 0x002D093A File Offset: 0x002CEB3A
		public SharedComFont SharedComFont
		{
			get
			{
				return base.GetElement<SharedComFont>(0);
			}
			set
			{
				base.SetElement<SharedComFont>(0, value);
			}
		}

		// Token: 0x170040FF RID: 16639
		// (get) Token: 0x0600F056 RID: 61526 RVA: 0x002D0944 File Offset: 0x002CEB44
		// (set) Token: 0x0600F057 RID: 61527 RVA: 0x002D094D File Offset: 0x002CEB4D
		public SharedComPicture SharedComPicture
		{
			get
			{
				return base.GetElement<SharedComPicture>(1);
			}
			set
			{
				base.SetElement<SharedComPicture>(1, value);
			}
		}

		// Token: 0x0600F058 RID: 61528 RVA: 0x002D0957 File Offset: 0x002CEB57
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (35 == namespaceId && "name" == name)
			{
				return new StringValue();
			}
			if (35 == namespaceId && "value" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F059 RID: 61529 RVA: 0x002D0991 File Offset: 0x002CEB91
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ActiveXObjectProperty>(deep);
		}

		// Token: 0x04007067 RID: 28775
		private const string tagName = "ocxPr";

		// Token: 0x04007068 RID: 28776
		private const byte tagNsId = 35;

		// Token: 0x04007069 RID: 28777
		internal const int ElementTypeIdConst = 12618;

		// Token: 0x0400706A RID: 28778
		private static string[] attributeTagNames = new string[] { "name", "value" };

		// Token: 0x0400706B RID: 28779
		private static byte[] attributeNamespaceIds = new byte[] { 35, 35 };

		// Token: 0x0400706C RID: 28780
		private static readonly string[] eleTagNames = new string[] { "font", "picture" };

		// Token: 0x0400706D RID: 28781
		private static readonly byte[] eleNamespaceIds = new byte[] { 35, 35 };
	}
}
