using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingGroup
{
	// Token: 0x020024F4 RID: 9460
	[ChildElementInfo(typeof(Extents))]
	[ChildElementInfo(typeof(Offset))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Transform2D : OpenXmlCompositeElement
	{
		// Token: 0x1700536D RID: 21357
		// (get) Token: 0x060118B1 RID: 71857 RVA: 0x002E002B File Offset: 0x002DE22B
		public override string LocalName
		{
			get
			{
				return "xfrm";
			}
		}

		// Token: 0x1700536E RID: 21358
		// (get) Token: 0x060118B2 RID: 71858 RVA: 0x002EF715 File Offset: 0x002ED915
		internal override byte NamespaceId
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x1700536F RID: 21359
		// (get) Token: 0x060118B3 RID: 71859 RVA: 0x002EF99D File Offset: 0x002EDB9D
		internal override int ElementTypeId
		{
			get
			{
				return 13125;
			}
		}

		// Token: 0x060118B4 RID: 71860 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005370 RID: 21360
		// (get) Token: 0x060118B5 RID: 71861 RVA: 0x002EF9A4 File Offset: 0x002EDBA4
		internal override string[] AttributeTagNames
		{
			get
			{
				return Transform2D.attributeTagNames;
			}
		}

		// Token: 0x17005371 RID: 21361
		// (get) Token: 0x060118B6 RID: 71862 RVA: 0x002EF9AB File Offset: 0x002EDBAB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Transform2D.attributeNamespaceIds;
			}
		}

		// Token: 0x17005372 RID: 21362
		// (get) Token: 0x060118B7 RID: 71863 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060118B8 RID: 71864 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005373 RID: 21363
		// (get) Token: 0x060118B9 RID: 71865 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060118BA RID: 71866 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17005374 RID: 21364
		// (get) Token: 0x060118BB RID: 71867 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060118BC RID: 71868 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x060118BD RID: 71869 RVA: 0x00293ECF File Offset: 0x002920CF
		public Transform2D()
		{
		}

		// Token: 0x060118BE RID: 71870 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Transform2D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060118BF RID: 71871 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Transform2D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060118C0 RID: 71872 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Transform2D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060118C1 RID: 71873 RVA: 0x002DF17C File Offset: 0x002DD37C
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

		// Token: 0x17005375 RID: 21365
		// (get) Token: 0x060118C2 RID: 71874 RVA: 0x002EF9B2 File Offset: 0x002EDBB2
		internal override string[] ElementTagNames
		{
			get
			{
				return Transform2D.eleTagNames;
			}
		}

		// Token: 0x17005376 RID: 21366
		// (get) Token: 0x060118C3 RID: 71875 RVA: 0x002EF9B9 File Offset: 0x002EDBB9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Transform2D.eleNamespaceIds;
			}
		}

		// Token: 0x17005377 RID: 21367
		// (get) Token: 0x060118C4 RID: 71876 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005378 RID: 21368
		// (get) Token: 0x060118C5 RID: 71877 RVA: 0x002DF1BD File Offset: 0x002DD3BD
		// (set) Token: 0x060118C6 RID: 71878 RVA: 0x002DF1C6 File Offset: 0x002DD3C6
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

		// Token: 0x17005379 RID: 21369
		// (get) Token: 0x060118C7 RID: 71879 RVA: 0x002DF1D0 File Offset: 0x002DD3D0
		// (set) Token: 0x060118C8 RID: 71880 RVA: 0x002DF1D9 File Offset: 0x002DD3D9
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

		// Token: 0x060118C9 RID: 71881 RVA: 0x002EF9C0 File Offset: 0x002EDBC0
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

		// Token: 0x060118CA RID: 71882 RVA: 0x002EFA17 File Offset: 0x002EDC17
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Transform2D>(deep);
		}

		// Token: 0x060118CB RID: 71883 RVA: 0x002EFA20 File Offset: 0x002EDC20
		// Note: this type is marked as 'beforefieldinit'.
		static Transform2D()
		{
			byte[] array = new byte[3];
			Transform2D.attributeNamespaceIds = array;
			Transform2D.eleTagNames = new string[] { "off", "ext" };
			Transform2D.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x04007B3E RID: 31550
		private const string tagName = "xfrm";

		// Token: 0x04007B3F RID: 31551
		private const byte tagNsId = 60;

		// Token: 0x04007B40 RID: 31552
		internal const int ElementTypeIdConst = 13125;

		// Token: 0x04007B41 RID: 31553
		private static string[] attributeTagNames = new string[] { "rot", "flipH", "flipV" };

		// Token: 0x04007B42 RID: 31554
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007B43 RID: 31555
		private static readonly string[] eleTagNames;

		// Token: 0x04007B44 RID: 31556
		private static readonly byte[] eleNamespaceIds;
	}
}
