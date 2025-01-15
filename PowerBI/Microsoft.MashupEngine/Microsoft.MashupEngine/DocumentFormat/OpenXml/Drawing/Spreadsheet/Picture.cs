using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x0200287D RID: 10365
	[ChildElementInfo(typeof(NonVisualPictureProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(ShapeStyle))]
	internal class Picture : OpenXmlCompositeElement
	{
		// Token: 0x17006711 RID: 26385
		// (get) Token: 0x060144E3 RID: 83171 RVA: 0x002FB9AA File Offset: 0x002F9BAA
		public override string LocalName
		{
			get
			{
				return "pic";
			}
		}

		// Token: 0x17006712 RID: 26386
		// (get) Token: 0x060144E4 RID: 83172 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17006713 RID: 26387
		// (get) Token: 0x060144E5 RID: 83173 RVA: 0x00311D5A File Offset: 0x0030FF5A
		internal override int ElementTypeId
		{
			get
			{
				return 10727;
			}
		}

		// Token: 0x060144E6 RID: 83174 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006714 RID: 26388
		// (get) Token: 0x060144E7 RID: 83175 RVA: 0x00311D61 File Offset: 0x0030FF61
		internal override string[] AttributeTagNames
		{
			get
			{
				return Picture.attributeTagNames;
			}
		}

		// Token: 0x17006715 RID: 26389
		// (get) Token: 0x060144E8 RID: 83176 RVA: 0x00311D68 File Offset: 0x0030FF68
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Picture.attributeNamespaceIds;
			}
		}

		// Token: 0x17006716 RID: 26390
		// (get) Token: 0x060144E9 RID: 83177 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060144EA RID: 83178 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17006717 RID: 26391
		// (get) Token: 0x060144EB RID: 83179 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060144EC RID: 83180 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x060144ED RID: 83181 RVA: 0x00293ECF File Offset: 0x002920CF
		public Picture()
		{
		}

		// Token: 0x060144EE RID: 83182 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Picture(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060144EF RID: 83183 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Picture(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060144F0 RID: 83184 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Picture(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060144F1 RID: 83185 RVA: 0x00311D70 File Offset: 0x0030FF70
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "nvPicPr" == name)
			{
				return new NonVisualPictureProperties();
			}
			if (18 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (18 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (18 == namespaceId && "style" == name)
			{
				return new ShapeStyle();
			}
			return null;
		}

		// Token: 0x17006718 RID: 26392
		// (get) Token: 0x060144F2 RID: 83186 RVA: 0x00311DDE File Offset: 0x0030FFDE
		internal override string[] ElementTagNames
		{
			get
			{
				return Picture.eleTagNames;
			}
		}

		// Token: 0x17006719 RID: 26393
		// (get) Token: 0x060144F3 RID: 83187 RVA: 0x00311DE5 File Offset: 0x0030FFE5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Picture.eleNamespaceIds;
			}
		}

		// Token: 0x1700671A RID: 26394
		// (get) Token: 0x060144F4 RID: 83188 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700671B RID: 26395
		// (get) Token: 0x060144F5 RID: 83189 RVA: 0x00311DEC File Offset: 0x0030FFEC
		// (set) Token: 0x060144F6 RID: 83190 RVA: 0x00311DF5 File Offset: 0x0030FFF5
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

		// Token: 0x1700671C RID: 26396
		// (get) Token: 0x060144F7 RID: 83191 RVA: 0x00311DFF File Offset: 0x0030FFFF
		// (set) Token: 0x060144F8 RID: 83192 RVA: 0x00311E08 File Offset: 0x00310008
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

		// Token: 0x1700671D RID: 26397
		// (get) Token: 0x060144F9 RID: 83193 RVA: 0x00311E12 File Offset: 0x00310012
		// (set) Token: 0x060144FA RID: 83194 RVA: 0x00311E1B File Offset: 0x0031001B
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

		// Token: 0x1700671E RID: 26398
		// (get) Token: 0x060144FB RID: 83195 RVA: 0x00311E25 File Offset: 0x00310025
		// (set) Token: 0x060144FC RID: 83196 RVA: 0x00311E2E File Offset: 0x0031002E
		public ShapeStyle ShapeStyle
		{
			get
			{
				return base.GetElement<ShapeStyle>(3);
			}
			set
			{
				base.SetElement<ShapeStyle>(3, value);
			}
		}

		// Token: 0x060144FD RID: 83197 RVA: 0x002DFFB5 File Offset: 0x002DE1B5
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

		// Token: 0x060144FE RID: 83198 RVA: 0x00311E38 File Offset: 0x00310038
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Picture>(deep);
		}

		// Token: 0x060144FF RID: 83199 RVA: 0x00311E44 File Offset: 0x00310044
		// Note: this type is marked as 'beforefieldinit'.
		static Picture()
		{
			byte[] array = new byte[2];
			Picture.attributeNamespaceIds = array;
			Picture.eleTagNames = new string[] { "nvPicPr", "blipFill", "spPr", "style" };
			Picture.eleNamespaceIds = new byte[] { 18, 18, 18, 18 };
		}

		// Token: 0x04008D82 RID: 36226
		private const string tagName = "pic";

		// Token: 0x04008D83 RID: 36227
		private const byte tagNsId = 18;

		// Token: 0x04008D84 RID: 36228
		internal const int ElementTypeIdConst = 10727;

		// Token: 0x04008D85 RID: 36229
		private static string[] attributeTagNames = new string[] { "macro", "fPublished" };

		// Token: 0x04008D86 RID: 36230
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008D87 RID: 36231
		private static readonly string[] eleTagNames;

		// Token: 0x04008D88 RID: 36232
		private static readonly byte[] eleNamespaceIds;
	}
}
