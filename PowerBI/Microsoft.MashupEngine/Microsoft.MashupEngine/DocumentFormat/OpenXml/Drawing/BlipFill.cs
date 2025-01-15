using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002702 RID: 9986
	[ChildElementInfo(typeof(Tile))]
	[ChildElementInfo(typeof(SourceRectangle))]
	[ChildElementInfo(typeof(Blip))]
	[ChildElementInfo(typeof(Stretch))]
	[GeneratedCode("DomGen", "2.0")]
	internal class BlipFill : OpenXmlCompositeElement
	{
		// Token: 0x17005E82 RID: 24194
		// (get) Token: 0x06013153 RID: 78163 RVA: 0x002FC561 File Offset: 0x002FA761
		public override string LocalName
		{
			get
			{
				return "blipFill";
			}
		}

		// Token: 0x17005E83 RID: 24195
		// (get) Token: 0x06013154 RID: 78164 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E84 RID: 24196
		// (get) Token: 0x06013155 RID: 78165 RVA: 0x003035BE File Offset: 0x003017BE
		internal override int ElementTypeId
		{
			get
			{
				return 10050;
			}
		}

		// Token: 0x06013156 RID: 78166 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E85 RID: 24197
		// (get) Token: 0x06013157 RID: 78167 RVA: 0x003035C5 File Offset: 0x003017C5
		internal override string[] AttributeTagNames
		{
			get
			{
				return BlipFill.attributeTagNames;
			}
		}

		// Token: 0x17005E86 RID: 24198
		// (get) Token: 0x06013158 RID: 78168 RVA: 0x003035CC File Offset: 0x003017CC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BlipFill.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E87 RID: 24199
		// (get) Token: 0x06013159 RID: 78169 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601315A RID: 78170 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005E88 RID: 24200
		// (get) Token: 0x0601315B RID: 78171 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601315C RID: 78172 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x0601315D RID: 78173 RVA: 0x00293ECF File Offset: 0x002920CF
		public BlipFill()
		{
		}

		// Token: 0x0601315E RID: 78174 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BlipFill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601315F RID: 78175 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BlipFill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013160 RID: 78176 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BlipFill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013161 RID: 78177 RVA: 0x003035D4 File Offset: 0x003017D4
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

		// Token: 0x17005E89 RID: 24201
		// (get) Token: 0x06013162 RID: 78178 RVA: 0x00303642 File Offset: 0x00301842
		internal override string[] ElementTagNames
		{
			get
			{
				return BlipFill.eleTagNames;
			}
		}

		// Token: 0x17005E8A RID: 24202
		// (get) Token: 0x06013163 RID: 78179 RVA: 0x00303649 File Offset: 0x00301849
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BlipFill.eleNamespaceIds;
			}
		}

		// Token: 0x17005E8B RID: 24203
		// (get) Token: 0x06013164 RID: 78180 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005E8C RID: 24204
		// (get) Token: 0x06013165 RID: 78181 RVA: 0x002FC5FC File Offset: 0x002FA7FC
		// (set) Token: 0x06013166 RID: 78182 RVA: 0x002FC605 File Offset: 0x002FA805
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

		// Token: 0x17005E8D RID: 24205
		// (get) Token: 0x06013167 RID: 78183 RVA: 0x002FC60F File Offset: 0x002FA80F
		// (set) Token: 0x06013168 RID: 78184 RVA: 0x002FC618 File Offset: 0x002FA818
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

		// Token: 0x06013169 RID: 78185 RVA: 0x002FC622 File Offset: 0x002FA822
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

		// Token: 0x0601316A RID: 78186 RVA: 0x00303650 File Offset: 0x00301850
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BlipFill>(deep);
		}

		// Token: 0x0601316B RID: 78187 RVA: 0x0030365C File Offset: 0x0030185C
		// Note: this type is marked as 'beforefieldinit'.
		static BlipFill()
		{
			byte[] array = new byte[2];
			BlipFill.attributeNamespaceIds = array;
			BlipFill.eleTagNames = new string[] { "blip", "srcRect", "tile", "stretch" };
			BlipFill.eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
		}

		// Token: 0x04008494 RID: 33940
		private const string tagName = "blipFill";

		// Token: 0x04008495 RID: 33941
		private const byte tagNsId = 10;

		// Token: 0x04008496 RID: 33942
		internal const int ElementTypeIdConst = 10050;

		// Token: 0x04008497 RID: 33943
		private static string[] attributeTagNames = new string[] { "dpi", "rotWithShape" };

		// Token: 0x04008498 RID: 33944
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008499 RID: 33945
		private static readonly string[] eleTagNames;

		// Token: 0x0400849A RID: 33946
		private static readonly byte[] eleNamespaceIds;
	}
}
