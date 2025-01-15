using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A68 RID: 10856
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SourceRectangle))]
	[ChildElementInfo(typeof(Stretch))]
	[ChildElementInfo(typeof(Blip))]
	[ChildElementInfo(typeof(Tile))]
	internal class BlipFill : OpenXmlCompositeElement
	{
		// Token: 0x170072A0 RID: 29344
		// (get) Token: 0x06015F10 RID: 89872 RVA: 0x002FC561 File Offset: 0x002FA761
		public override string LocalName
		{
			get
			{
				return "blipFill";
			}
		}

		// Token: 0x170072A1 RID: 29345
		// (get) Token: 0x06015F11 RID: 89873 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170072A2 RID: 29346
		// (get) Token: 0x06015F12 RID: 89874 RVA: 0x00324C30 File Offset: 0x00322E30
		internal override int ElementTypeId
		{
			get
			{
				return 12274;
			}
		}

		// Token: 0x06015F13 RID: 89875 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170072A3 RID: 29347
		// (get) Token: 0x06015F14 RID: 89876 RVA: 0x00324C37 File Offset: 0x00322E37
		internal override string[] AttributeTagNames
		{
			get
			{
				return BlipFill.attributeTagNames;
			}
		}

		// Token: 0x170072A4 RID: 29348
		// (get) Token: 0x06015F15 RID: 89877 RVA: 0x00324C3E File Offset: 0x00322E3E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BlipFill.attributeNamespaceIds;
			}
		}

		// Token: 0x170072A5 RID: 29349
		// (get) Token: 0x06015F16 RID: 89878 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06015F17 RID: 89879 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "dpi")]
		public UInt32Value Dpi
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170072A6 RID: 29350
		// (get) Token: 0x06015F18 RID: 89880 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06015F19 RID: 89881 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "rotWithShape")]
		public BooleanValue RotateWithShape
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

		// Token: 0x06015F1A RID: 89882 RVA: 0x00293ECF File Offset: 0x002920CF
		public BlipFill()
		{
		}

		// Token: 0x06015F1B RID: 89883 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BlipFill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F1C RID: 89884 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BlipFill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F1D RID: 89885 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BlipFill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015F1E RID: 89886 RVA: 0x00324C48 File Offset: 0x00322E48
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "blip" == name)
			{
				return new Blip();
			}
			if (10 == namespaceId && "srcRect" == name)
			{
				return new SourceRectangle();
			}
			if (10 == namespaceId && "tile" == name)
			{
				return new Tile();
			}
			if (10 == namespaceId && "stretch" == name)
			{
				return new Stretch();
			}
			return null;
		}

		// Token: 0x170072A7 RID: 29351
		// (get) Token: 0x06015F1F RID: 89887 RVA: 0x00324CB6 File Offset: 0x00322EB6
		internal override string[] ElementTagNames
		{
			get
			{
				return BlipFill.eleTagNames;
			}
		}

		// Token: 0x170072A8 RID: 29352
		// (get) Token: 0x06015F20 RID: 89888 RVA: 0x00324CBD File Offset: 0x00322EBD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BlipFill.eleNamespaceIds;
			}
		}

		// Token: 0x170072A9 RID: 29353
		// (get) Token: 0x06015F21 RID: 89889 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170072AA RID: 29354
		// (get) Token: 0x06015F22 RID: 89890 RVA: 0x002FC5FC File Offset: 0x002FA7FC
		// (set) Token: 0x06015F23 RID: 89891 RVA: 0x002FC605 File Offset: 0x002FA805
		public Blip Blip
		{
			get
			{
				return base.GetElement<Blip>(0);
			}
			set
			{
				base.SetElement<Blip>(0, value);
			}
		}

		// Token: 0x170072AB RID: 29355
		// (get) Token: 0x06015F24 RID: 89892 RVA: 0x002FC60F File Offset: 0x002FA80F
		// (set) Token: 0x06015F25 RID: 89893 RVA: 0x002FC618 File Offset: 0x002FA818
		public SourceRectangle SourceRectangle
		{
			get
			{
				return base.GetElement<SourceRectangle>(1);
			}
			set
			{
				base.SetElement<SourceRectangle>(1, value);
			}
		}

		// Token: 0x06015F26 RID: 89894 RVA: 0x002FC622 File Offset: 0x002FA822
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dpi" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "rotWithShape" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015F27 RID: 89895 RVA: 0x00324CC4 File Offset: 0x00322EC4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BlipFill>(deep);
		}

		// Token: 0x06015F28 RID: 89896 RVA: 0x00324CD0 File Offset: 0x00322ED0
		// Note: this type is marked as 'beforefieldinit'.
		static BlipFill()
		{
			byte[] array = new byte[2];
			BlipFill.attributeNamespaceIds = array;
			BlipFill.eleTagNames = new string[] { "blip", "srcRect", "tile", "stretch" };
			BlipFill.eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
		}

		// Token: 0x04009582 RID: 38274
		private const string tagName = "blipFill";

		// Token: 0x04009583 RID: 38275
		private const byte tagNsId = 24;

		// Token: 0x04009584 RID: 38276
		internal const int ElementTypeIdConst = 12274;

		// Token: 0x04009585 RID: 38277
		private static string[] attributeTagNames = new string[] { "dpi", "rotWithShape" };

		// Token: 0x04009586 RID: 38278
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009587 RID: 38279
		private static readonly string[] eleTagNames;

		// Token: 0x04009588 RID: 38280
		private static readonly byte[] eleNamespaceIds;
	}
}
