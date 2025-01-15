using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x0200262D RID: 9773
	[ChildElementInfo(typeof(Style))]
	[ChildElementInfo(typeof(BlipFill))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualPictureProperties))]
	[ChildElementInfo(typeof(ShapeProperties))]
	internal class Picture : OpenXmlCompositeElement
	{
		// Token: 0x17005A29 RID: 23081
		// (get) Token: 0x0601278E RID: 75662 RVA: 0x002FB9AA File Offset: 0x002F9BAA
		public override string LocalName
		{
			get
			{
				return "pic";
			}
		}

		// Token: 0x17005A2A RID: 23082
		// (get) Token: 0x0601278F RID: 75663 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A2B RID: 23083
		// (get) Token: 0x06012790 RID: 75664 RVA: 0x002FB9B1 File Offset: 0x002F9BB1
		internal override int ElementTypeId
		{
			get
			{
				return 10592;
			}
		}

		// Token: 0x06012791 RID: 75665 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005A2C RID: 23084
		// (get) Token: 0x06012792 RID: 75666 RVA: 0x002FB9B8 File Offset: 0x002F9BB8
		internal override string[] AttributeTagNames
		{
			get
			{
				return Picture.attributeTagNames;
			}
		}

		// Token: 0x17005A2D RID: 23085
		// (get) Token: 0x06012793 RID: 75667 RVA: 0x002FB9BF File Offset: 0x002F9BBF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Picture.attributeNamespaceIds;
			}
		}

		// Token: 0x17005A2E RID: 23086
		// (get) Token: 0x06012794 RID: 75668 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012795 RID: 75669 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "macro")]
		public StringValue Macro
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

		// Token: 0x17005A2F RID: 23087
		// (get) Token: 0x06012796 RID: 75670 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06012797 RID: 75671 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fPublished")]
		public BooleanValue Published
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

		// Token: 0x06012798 RID: 75672 RVA: 0x00293ECF File Offset: 0x002920CF
		public Picture()
		{
		}

		// Token: 0x06012799 RID: 75673 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Picture(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601279A RID: 75674 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Picture(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601279B RID: 75675 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Picture(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601279C RID: 75676 RVA: 0x002FB9C8 File Offset: 0x002F9BC8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "nvPicPr" == name)
			{
				return new NonVisualPictureProperties();
			}
			if (12 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (12 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (12 == namespaceId && "style" == name)
			{
				return new Style();
			}
			return null;
		}

		// Token: 0x17005A30 RID: 23088
		// (get) Token: 0x0601279D RID: 75677 RVA: 0x002FBA36 File Offset: 0x002F9C36
		internal override string[] ElementTagNames
		{
			get
			{
				return Picture.eleTagNames;
			}
		}

		// Token: 0x17005A31 RID: 23089
		// (get) Token: 0x0601279E RID: 75678 RVA: 0x002FBA3D File Offset: 0x002F9C3D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Picture.eleNamespaceIds;
			}
		}

		// Token: 0x17005A32 RID: 23090
		// (get) Token: 0x0601279F RID: 75679 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A33 RID: 23091
		// (get) Token: 0x060127A0 RID: 75680 RVA: 0x002FBA44 File Offset: 0x002F9C44
		// (set) Token: 0x060127A1 RID: 75681 RVA: 0x002FBA4D File Offset: 0x002F9C4D
		public NonVisualPictureProperties NonVisualPictureProperties
		{
			get
			{
				return base.GetElement<NonVisualPictureProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualPictureProperties>(0, value);
			}
		}

		// Token: 0x17005A34 RID: 23092
		// (get) Token: 0x060127A2 RID: 75682 RVA: 0x002FBA57 File Offset: 0x002F9C57
		// (set) Token: 0x060127A3 RID: 75683 RVA: 0x002FBA60 File Offset: 0x002F9C60
		public BlipFill BlipFill
		{
			get
			{
				return base.GetElement<BlipFill>(1);
			}
			set
			{
				base.SetElement<BlipFill>(1, value);
			}
		}

		// Token: 0x17005A35 RID: 23093
		// (get) Token: 0x060127A4 RID: 75684 RVA: 0x002FBA6A File Offset: 0x002F9C6A
		// (set) Token: 0x060127A5 RID: 75685 RVA: 0x002FBA73 File Offset: 0x002F9C73
		public ShapeProperties ShapeProperties
		{
			get
			{
				return base.GetElement<ShapeProperties>(2);
			}
			set
			{
				base.SetElement<ShapeProperties>(2, value);
			}
		}

		// Token: 0x17005A36 RID: 23094
		// (get) Token: 0x060127A6 RID: 75686 RVA: 0x002FBA7D File Offset: 0x002F9C7D
		// (set) Token: 0x060127A7 RID: 75687 RVA: 0x002FBA86 File Offset: 0x002F9C86
		public Style Style
		{
			get
			{
				return base.GetElement<Style>(3);
			}
			set
			{
				base.SetElement<Style>(3, value);
			}
		}

		// Token: 0x060127A8 RID: 75688 RVA: 0x002DFFB5 File Offset: 0x002DE1B5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "macro" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fPublished" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060127A9 RID: 75689 RVA: 0x002FBA90 File Offset: 0x002F9C90
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Picture>(deep);
		}

		// Token: 0x060127AA RID: 75690 RVA: 0x002FBA9C File Offset: 0x002F9C9C
		// Note: this type is marked as 'beforefieldinit'.
		static Picture()
		{
			byte[] array = new byte[2];
			Picture.attributeNamespaceIds = array;
			Picture.eleTagNames = new string[] { "nvPicPr", "blipFill", "spPr", "style" };
			Picture.eleNamespaceIds = new byte[] { 12, 12, 12, 12 };
		}

		// Token: 0x04008049 RID: 32841
		private const string tagName = "pic";

		// Token: 0x0400804A RID: 32842
		private const byte tagNsId = 12;

		// Token: 0x0400804B RID: 32843
		internal const int ElementTypeIdConst = 10592;

		// Token: 0x0400804C RID: 32844
		private static string[] attributeTagNames = new string[] { "macro", "fPublished" };

		// Token: 0x0400804D RID: 32845
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400804E RID: 32846
		private static readonly string[] eleTagNames;

		// Token: 0x0400804F RID: 32847
		private static readonly byte[] eleNamespaceIds;
	}
}
