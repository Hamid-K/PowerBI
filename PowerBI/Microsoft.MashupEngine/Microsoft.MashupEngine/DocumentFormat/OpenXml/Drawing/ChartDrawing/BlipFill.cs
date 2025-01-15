using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002638 RID: 9784
	[ChildElementInfo(typeof(SourceRectangle))]
	[ChildElementInfo(typeof(Tile))]
	[ChildElementInfo(typeof(Blip))]
	[ChildElementInfo(typeof(Stretch))]
	[GeneratedCode("DomGen", "2.0")]
	internal class BlipFill : OpenXmlCompositeElement
	{
		// Token: 0x17005A9B RID: 23195
		// (get) Token: 0x06012883 RID: 75907 RVA: 0x002FC561 File Offset: 0x002FA761
		public override string LocalName
		{
			get
			{
				return "blipFill";
			}
		}

		// Token: 0x17005A9C RID: 23196
		// (get) Token: 0x06012884 RID: 75908 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A9D RID: 23197
		// (get) Token: 0x06012885 RID: 75909 RVA: 0x002FC568 File Offset: 0x002FA768
		internal override int ElementTypeId
		{
			get
			{
				return 10603;
			}
		}

		// Token: 0x06012886 RID: 75910 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005A9E RID: 23198
		// (get) Token: 0x06012887 RID: 75911 RVA: 0x002FC56F File Offset: 0x002FA76F
		internal override string[] AttributeTagNames
		{
			get
			{
				return BlipFill.attributeTagNames;
			}
		}

		// Token: 0x17005A9F RID: 23199
		// (get) Token: 0x06012888 RID: 75912 RVA: 0x002FC576 File Offset: 0x002FA776
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BlipFill.attributeNamespaceIds;
			}
		}

		// Token: 0x17005AA0 RID: 23200
		// (get) Token: 0x06012889 RID: 75913 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601288A RID: 75914 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005AA1 RID: 23201
		// (get) Token: 0x0601288B RID: 75915 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601288C RID: 75916 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x0601288D RID: 75917 RVA: 0x00293ECF File Offset: 0x002920CF
		public BlipFill()
		{
		}

		// Token: 0x0601288E RID: 75918 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BlipFill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601288F RID: 75919 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BlipFill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012890 RID: 75920 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BlipFill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012891 RID: 75921 RVA: 0x002FC580 File Offset: 0x002FA780
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

		// Token: 0x17005AA2 RID: 23202
		// (get) Token: 0x06012892 RID: 75922 RVA: 0x002FC5EE File Offset: 0x002FA7EE
		internal override string[] ElementTagNames
		{
			get
			{
				return BlipFill.eleTagNames;
			}
		}

		// Token: 0x17005AA3 RID: 23203
		// (get) Token: 0x06012893 RID: 75923 RVA: 0x002FC5F5 File Offset: 0x002FA7F5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BlipFill.eleNamespaceIds;
			}
		}

		// Token: 0x17005AA4 RID: 23204
		// (get) Token: 0x06012894 RID: 75924 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005AA5 RID: 23205
		// (get) Token: 0x06012895 RID: 75925 RVA: 0x002FC5FC File Offset: 0x002FA7FC
		// (set) Token: 0x06012896 RID: 75926 RVA: 0x002FC605 File Offset: 0x002FA805
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

		// Token: 0x17005AA6 RID: 23206
		// (get) Token: 0x06012897 RID: 75927 RVA: 0x002FC60F File Offset: 0x002FA80F
		// (set) Token: 0x06012898 RID: 75928 RVA: 0x002FC618 File Offset: 0x002FA818
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

		// Token: 0x06012899 RID: 75929 RVA: 0x002FC622 File Offset: 0x002FA822
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

		// Token: 0x0601289A RID: 75930 RVA: 0x002FC658 File Offset: 0x002FA858
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BlipFill>(deep);
		}

		// Token: 0x0601289B RID: 75931 RVA: 0x002FC664 File Offset: 0x002FA864
		// Note: this type is marked as 'beforefieldinit'.
		static BlipFill()
		{
			byte[] array = new byte[2];
			BlipFill.attributeNamespaceIds = array;
			BlipFill.eleTagNames = new string[] { "blip", "srcRect", "tile", "stretch" };
			BlipFill.eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
		}

		// Token: 0x0400808A RID: 32906
		private const string tagName = "blipFill";

		// Token: 0x0400808B RID: 32907
		private const byte tagNsId = 12;

		// Token: 0x0400808C RID: 32908
		internal const int ElementTypeIdConst = 10603;

		// Token: 0x0400808D RID: 32909
		private static string[] attributeTagNames = new string[] { "dpi", "rotWithShape" };

		// Token: 0x0400808E RID: 32910
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400808F RID: 32911
		private static readonly string[] eleTagNames;

		// Token: 0x04008090 RID: 32912
		private static readonly byte[] eleNamespaceIds;
	}
}
