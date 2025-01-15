using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Pictures
{
	// Token: 0x02002874 RID: 10356
	[ChildElementInfo(typeof(Blip))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SourceRectangle))]
	[ChildElementInfo(typeof(Tile))]
	[ChildElementInfo(typeof(Stretch))]
	internal class BlipFill : OpenXmlCompositeElement
	{
		// Token: 0x170066AE RID: 26286
		// (get) Token: 0x06014411 RID: 82961 RVA: 0x002FC561 File Offset: 0x002FA761
		public override string LocalName
		{
			get
			{
				return "blipFill";
			}
		}

		// Token: 0x170066AF RID: 26287
		// (get) Token: 0x06014412 RID: 82962 RVA: 0x000E78AE File Offset: 0x000E5AAE
		internal override byte NamespaceId
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x170066B0 RID: 26288
		// (get) Token: 0x06014413 RID: 82963 RVA: 0x00310EFD File Offset: 0x0030F0FD
		internal override int ElementTypeId
		{
			get
			{
				return 10718;
			}
		}

		// Token: 0x06014414 RID: 82964 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170066B1 RID: 26289
		// (get) Token: 0x06014415 RID: 82965 RVA: 0x00310F04 File Offset: 0x0030F104
		internal override string[] AttributeTagNames
		{
			get
			{
				return BlipFill.attributeTagNames;
			}
		}

		// Token: 0x170066B2 RID: 26290
		// (get) Token: 0x06014416 RID: 82966 RVA: 0x00310F0B File Offset: 0x0030F10B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BlipFill.attributeNamespaceIds;
			}
		}

		// Token: 0x170066B3 RID: 26291
		// (get) Token: 0x06014417 RID: 82967 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06014418 RID: 82968 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170066B4 RID: 26292
		// (get) Token: 0x06014419 RID: 82969 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601441A RID: 82970 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x0601441B RID: 82971 RVA: 0x00293ECF File Offset: 0x002920CF
		public BlipFill()
		{
		}

		// Token: 0x0601441C RID: 82972 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BlipFill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601441D RID: 82973 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BlipFill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601441E RID: 82974 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BlipFill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601441F RID: 82975 RVA: 0x00310F14 File Offset: 0x0030F114
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

		// Token: 0x170066B5 RID: 26293
		// (get) Token: 0x06014420 RID: 82976 RVA: 0x00310F82 File Offset: 0x0030F182
		internal override string[] ElementTagNames
		{
			get
			{
				return BlipFill.eleTagNames;
			}
		}

		// Token: 0x170066B6 RID: 26294
		// (get) Token: 0x06014421 RID: 82977 RVA: 0x00310F89 File Offset: 0x0030F189
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BlipFill.eleNamespaceIds;
			}
		}

		// Token: 0x170066B7 RID: 26295
		// (get) Token: 0x06014422 RID: 82978 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170066B8 RID: 26296
		// (get) Token: 0x06014423 RID: 82979 RVA: 0x002FC5FC File Offset: 0x002FA7FC
		// (set) Token: 0x06014424 RID: 82980 RVA: 0x002FC605 File Offset: 0x002FA805
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

		// Token: 0x170066B9 RID: 26297
		// (get) Token: 0x06014425 RID: 82981 RVA: 0x002FC60F File Offset: 0x002FA80F
		// (set) Token: 0x06014426 RID: 82982 RVA: 0x002FC618 File Offset: 0x002FA818
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

		// Token: 0x06014427 RID: 82983 RVA: 0x002FC622 File Offset: 0x002FA822
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

		// Token: 0x06014428 RID: 82984 RVA: 0x00310F90 File Offset: 0x0030F190
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BlipFill>(deep);
		}

		// Token: 0x06014429 RID: 82985 RVA: 0x00310F9C File Offset: 0x0030F19C
		// Note: this type is marked as 'beforefieldinit'.
		static BlipFill()
		{
			byte[] array = new byte[2];
			BlipFill.attributeNamespaceIds = array;
			BlipFill.eleTagNames = new string[] { "blip", "srcRect", "tile", "stretch" };
			BlipFill.eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
		}

		// Token: 0x04008D49 RID: 36169
		private const string tagName = "blipFill";

		// Token: 0x04008D4A RID: 36170
		private const byte tagNsId = 17;

		// Token: 0x04008D4B RID: 36171
		internal const int ElementTypeIdConst = 10718;

		// Token: 0x04008D4C RID: 36172
		private static string[] attributeTagNames = new string[] { "dpi", "rotWithShape" };

		// Token: 0x04008D4D RID: 36173
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008D4E RID: 36174
		private static readonly string[] eleTagNames;

		// Token: 0x04008D4F RID: 36175
		private static readonly byte[] eleNamespaceIds;
	}
}
