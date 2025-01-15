using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x02002392 RID: 9106
	[ChildElementInfo(typeof(Offset))]
	[ChildElementInfo(typeof(Extents))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Transform2D : OpenXmlCompositeElement
	{
		// Token: 0x17004BC1 RID: 19393
		// (get) Token: 0x06010778 RID: 67448 RVA: 0x002E002B File Offset: 0x002DE22B
		public override string LocalName
		{
			get
			{
				return "xfrm";
			}
		}

		// Token: 0x17004BC2 RID: 19394
		// (get) Token: 0x06010779 RID: 67449 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004BC3 RID: 19395
		// (get) Token: 0x0601077A RID: 67450 RVA: 0x002E3D34 File Offset: 0x002E1F34
		internal override int ElementTypeId
		{
			get
			{
				return 12765;
			}
		}

		// Token: 0x0601077B RID: 67451 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004BC4 RID: 19396
		// (get) Token: 0x0601077C RID: 67452 RVA: 0x002E3D3B File Offset: 0x002E1F3B
		internal override string[] AttributeTagNames
		{
			get
			{
				return Transform2D.attributeTagNames;
			}
		}

		// Token: 0x17004BC5 RID: 19397
		// (get) Token: 0x0601077D RID: 67453 RVA: 0x002E3D42 File Offset: 0x002E1F42
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Transform2D.attributeNamespaceIds;
			}
		}

		// Token: 0x17004BC6 RID: 19398
		// (get) Token: 0x0601077E RID: 67454 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601077F RID: 67455 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rot")]
		public Int32Value Rotation
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004BC7 RID: 19399
		// (get) Token: 0x06010780 RID: 67456 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06010781 RID: 67457 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "flipH")]
		public BooleanValue HorizontalFlip
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

		// Token: 0x17004BC8 RID: 19400
		// (get) Token: 0x06010782 RID: 67458 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06010783 RID: 67459 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "flipV")]
		public BooleanValue VerticalFlip
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

		// Token: 0x06010784 RID: 67460 RVA: 0x00293ECF File Offset: 0x002920CF
		public Transform2D()
		{
		}

		// Token: 0x06010785 RID: 67461 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Transform2D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010786 RID: 67462 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Transform2D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010787 RID: 67463 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Transform2D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010788 RID: 67464 RVA: 0x002DF17C File Offset: 0x002DD37C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "off" == name)
			{
				return new Offset();
			}
			if (10 == namespaceId && "ext" == name)
			{
				return new Extents();
			}
			return null;
		}

		// Token: 0x17004BC9 RID: 19401
		// (get) Token: 0x06010789 RID: 67465 RVA: 0x002E3D49 File Offset: 0x002E1F49
		internal override string[] ElementTagNames
		{
			get
			{
				return Transform2D.eleTagNames;
			}
		}

		// Token: 0x17004BCA RID: 19402
		// (get) Token: 0x0601078A RID: 67466 RVA: 0x002E3D50 File Offset: 0x002E1F50
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Transform2D.eleNamespaceIds;
			}
		}

		// Token: 0x17004BCB RID: 19403
		// (get) Token: 0x0601078B RID: 67467 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004BCC RID: 19404
		// (get) Token: 0x0601078C RID: 67468 RVA: 0x002DF1BD File Offset: 0x002DD3BD
		// (set) Token: 0x0601078D RID: 67469 RVA: 0x002DF1C6 File Offset: 0x002DD3C6
		public Offset Offset
		{
			get
			{
				return base.GetElement<Offset>(0);
			}
			set
			{
				base.SetElement<Offset>(0, value);
			}
		}

		// Token: 0x17004BCD RID: 19405
		// (get) Token: 0x0601078E RID: 67470 RVA: 0x002DF1D0 File Offset: 0x002DD3D0
		// (set) Token: 0x0601078F RID: 67471 RVA: 0x002DF1D9 File Offset: 0x002DD3D9
		public Extents Extents
		{
			get
			{
				return base.GetElement<Extents>(1);
			}
			set
			{
				base.SetElement<Extents>(1, value);
			}
		}

		// Token: 0x06010790 RID: 67472 RVA: 0x002E3D58 File Offset: 0x002E1F58
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rot" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "flipH" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "flipV" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010791 RID: 67473 RVA: 0x002E3DAF File Offset: 0x002E1FAF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Transform2D>(deep);
		}

		// Token: 0x06010792 RID: 67474 RVA: 0x002E3DB8 File Offset: 0x002E1FB8
		// Note: this type is marked as 'beforefieldinit'.
		static Transform2D()
		{
			byte[] array = new byte[3];
			Transform2D.attributeNamespaceIds = array;
			Transform2D.eleTagNames = new string[] { "off", "ext" };
			Transform2D.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x040074BA RID: 29882
		private const string tagName = "xfrm";

		// Token: 0x040074BB RID: 29883
		private const byte tagNsId = 49;

		// Token: 0x040074BC RID: 29884
		internal const int ElementTypeIdConst = 12765;

		// Token: 0x040074BD RID: 29885
		private static string[] attributeTagNames = new string[] { "rot", "flipH", "flipV" };

		// Token: 0x040074BE RID: 29886
		private static byte[] attributeNamespaceIds;

		// Token: 0x040074BF RID: 29887
		private static readonly string[] eleTagNames;

		// Token: 0x040074C0 RID: 29888
		private static readonly byte[] eleNamespaceIds;
	}
}
