using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A4F RID: 10831
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Picture))]
	internal class Control : OpenXmlCompositeElement
	{
		// Token: 0x170071C2 RID: 29122
		// (get) Token: 0x06015D36 RID: 89398 RVA: 0x002AD773 File Offset: 0x002AB973
		public override string LocalName
		{
			get
			{
				return "control";
			}
		}

		// Token: 0x170071C3 RID: 29123
		// (get) Token: 0x06015D37 RID: 89399 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170071C4 RID: 29124
		// (get) Token: 0x06015D38 RID: 89400 RVA: 0x003237FA File Offset: 0x003219FA
		internal override int ElementTypeId
		{
			get
			{
				return 12250;
			}
		}

		// Token: 0x06015D39 RID: 89401 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170071C5 RID: 29125
		// (get) Token: 0x06015D3A RID: 89402 RVA: 0x00323801 File Offset: 0x00321A01
		internal override string[] AttributeTagNames
		{
			get
			{
				return Control.attributeTagNames;
			}
		}

		// Token: 0x170071C6 RID: 29126
		// (get) Token: 0x06015D3B RID: 89403 RVA: 0x00323808 File Offset: 0x00321A08
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Control.attributeNamespaceIds;
			}
		}

		// Token: 0x170071C7 RID: 29127
		// (get) Token: 0x06015D3C RID: 89404 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015D3D RID: 89405 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "spid")]
		public StringValue ShapeId
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

		// Token: 0x170071C8 RID: 29128
		// (get) Token: 0x06015D3E RID: 89406 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06015D3F RID: 89407 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x170071C9 RID: 29129
		// (get) Token: 0x06015D40 RID: 89408 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06015D41 RID: 89409 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showAsIcon")]
		public BooleanValue ShowAsIcon
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170071CA RID: 29130
		// (get) Token: 0x06015D42 RID: 89410 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06015D43 RID: 89411 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x170071CB RID: 29131
		// (get) Token: 0x06015D44 RID: 89412 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x06015D45 RID: 89413 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "imgW")]
		public Int32Value ImageWidth
		{
			get
			{
				return (Int32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170071CC RID: 29132
		// (get) Token: 0x06015D46 RID: 89414 RVA: 0x002ED371 File Offset: 0x002EB571
		// (set) Token: 0x06015D47 RID: 89415 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "imgH")]
		public Int32Value ImageHeight
		{
			get
			{
				return (Int32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x06015D48 RID: 89416 RVA: 0x00293ECF File Offset: 0x002920CF
		public Control()
		{
		}

		// Token: 0x06015D49 RID: 89417 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Control(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015D4A RID: 89418 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Control(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015D4B RID: 89419 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Control(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015D4C RID: 89420 RVA: 0x0032380F File Offset: 0x00321A0F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			if (24 == namespaceId && "pic" == name)
			{
				return new Picture();
			}
			return null;
		}

		// Token: 0x170071CD RID: 29133
		// (get) Token: 0x06015D4D RID: 89421 RVA: 0x00323842 File Offset: 0x00321A42
		internal override string[] ElementTagNames
		{
			get
			{
				return Control.eleTagNames;
			}
		}

		// Token: 0x170071CE RID: 29134
		// (get) Token: 0x06015D4E RID: 89422 RVA: 0x00323849 File Offset: 0x00321A49
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Control.eleNamespaceIds;
			}
		}

		// Token: 0x170071CF RID: 29135
		// (get) Token: 0x06015D4F RID: 89423 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170071D0 RID: 29136
		// (get) Token: 0x06015D50 RID: 89424 RVA: 0x0031FDCB File Offset: 0x0031DFCB
		// (set) Token: 0x06015D51 RID: 89425 RVA: 0x0031FDD4 File Offset: 0x0031DFD4
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x170071D1 RID: 29137
		// (get) Token: 0x06015D52 RID: 89426 RVA: 0x00323850 File Offset: 0x00321A50
		// (set) Token: 0x06015D53 RID: 89427 RVA: 0x00323859 File Offset: 0x00321A59
		public Picture Picture
		{
			get
			{
				return base.GetElement<Picture>(1);
			}
			set
			{
				base.SetElement<Picture>(1, value);
			}
		}

		// Token: 0x06015D54 RID: 89428 RVA: 0x00323864 File Offset: 0x00321A64
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "spid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showAsIcon" == name)
			{
				return new BooleanValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "imgW" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "imgH" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015D55 RID: 89429 RVA: 0x003238FF File Offset: 0x00321AFF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Control>(deep);
		}

		// Token: 0x06015D56 RID: 89430 RVA: 0x00323908 File Offset: 0x00321B08
		// Note: this type is marked as 'beforefieldinit'.
		static Control()
		{
			byte[] array = new byte[6];
			array[3] = 19;
			Control.attributeNamespaceIds = array;
			Control.eleTagNames = new string[] { "extLst", "pic" };
			Control.eleNamespaceIds = new byte[] { 24, 24 };
		}

		// Token: 0x040094FC RID: 38140
		private const string tagName = "control";

		// Token: 0x040094FD RID: 38141
		private const byte tagNsId = 24;

		// Token: 0x040094FE RID: 38142
		internal const int ElementTypeIdConst = 12250;

		// Token: 0x040094FF RID: 38143
		private static string[] attributeTagNames = new string[] { "spid", "name", "showAsIcon", "id", "imgW", "imgH" };

		// Token: 0x04009500 RID: 38144
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009501 RID: 38145
		private static readonly string[] eleTagNames;

		// Token: 0x04009502 RID: 38146
		private static readonly byte[] eleNamespaceIds;
	}
}
