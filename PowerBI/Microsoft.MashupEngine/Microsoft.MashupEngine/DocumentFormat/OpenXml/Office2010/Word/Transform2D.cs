using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024C1 RID: 9409
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extents))]
	[ChildElementInfo(typeof(Offset))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Transform2D : OpenXmlCompositeElement
	{
		// Token: 0x170052AD RID: 21165
		// (get) Token: 0x060116F9 RID: 71417 RVA: 0x002E002B File Offset: 0x002DE22B
		public override string LocalName
		{
			get
			{
				return "xfrm";
			}
		}

		// Token: 0x170052AE RID: 21166
		// (get) Token: 0x060116FA RID: 71418 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052AF RID: 21167
		// (get) Token: 0x060116FB RID: 71419 RVA: 0x002EE71F File Offset: 0x002EC91F
		internal override int ElementTypeId
		{
			get
			{
				return 12881;
			}
		}

		// Token: 0x060116FC RID: 71420 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170052B0 RID: 21168
		// (get) Token: 0x060116FD RID: 71421 RVA: 0x002EE726 File Offset: 0x002EC926
		internal override string[] AttributeTagNames
		{
			get
			{
				return Transform2D.attributeTagNames;
			}
		}

		// Token: 0x170052B1 RID: 21169
		// (get) Token: 0x060116FE RID: 71422 RVA: 0x002EE72D File Offset: 0x002EC92D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Transform2D.attributeNamespaceIds;
			}
		}

		// Token: 0x170052B2 RID: 21170
		// (get) Token: 0x060116FF RID: 71423 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06011700 RID: 71424 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170052B3 RID: 21171
		// (get) Token: 0x06011701 RID: 71425 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06011702 RID: 71426 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170052B4 RID: 21172
		// (get) Token: 0x06011703 RID: 71427 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06011704 RID: 71428 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x06011705 RID: 71429 RVA: 0x00293ECF File Offset: 0x002920CF
		public Transform2D()
		{
		}

		// Token: 0x06011706 RID: 71430 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Transform2D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011707 RID: 71431 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Transform2D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011708 RID: 71432 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Transform2D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011709 RID: 71433 RVA: 0x002DF17C File Offset: 0x002DD37C
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

		// Token: 0x170052B5 RID: 21173
		// (get) Token: 0x0601170A RID: 71434 RVA: 0x002EE734 File Offset: 0x002EC934
		internal override string[] ElementTagNames
		{
			get
			{
				return Transform2D.eleTagNames;
			}
		}

		// Token: 0x170052B6 RID: 21174
		// (get) Token: 0x0601170B RID: 71435 RVA: 0x002EE73B File Offset: 0x002EC93B
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Transform2D.eleNamespaceIds;
			}
		}

		// Token: 0x170052B7 RID: 21175
		// (get) Token: 0x0601170C RID: 71436 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170052B8 RID: 21176
		// (get) Token: 0x0601170D RID: 71437 RVA: 0x002DF1BD File Offset: 0x002DD3BD
		// (set) Token: 0x0601170E RID: 71438 RVA: 0x002DF1C6 File Offset: 0x002DD3C6
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

		// Token: 0x170052B9 RID: 21177
		// (get) Token: 0x0601170F RID: 71439 RVA: 0x002DF1D0 File Offset: 0x002DD3D0
		// (set) Token: 0x06011710 RID: 71440 RVA: 0x002DF1D9 File Offset: 0x002DD3D9
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

		// Token: 0x06011711 RID: 71441 RVA: 0x002EE744 File Offset: 0x002EC944
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

		// Token: 0x06011712 RID: 71442 RVA: 0x002EE79B File Offset: 0x002EC99B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Transform2D>(deep);
		}

		// Token: 0x06011713 RID: 71443 RVA: 0x002EE7A4 File Offset: 0x002EC9A4
		// Note: this type is marked as 'beforefieldinit'.
		static Transform2D()
		{
			byte[] array = new byte[3];
			Transform2D.attributeNamespaceIds = array;
			Transform2D.eleTagNames = new string[] { "off", "ext" };
			Transform2D.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x040079D1 RID: 31185
		private const string tagName = "xfrm";

		// Token: 0x040079D2 RID: 31186
		private const byte tagNsId = 52;

		// Token: 0x040079D3 RID: 31187
		internal const int ElementTypeIdConst = 12881;

		// Token: 0x040079D4 RID: 31188
		private static string[] attributeTagNames = new string[] { "rot", "flipH", "flipV" };

		// Token: 0x040079D5 RID: 31189
		private static byte[] attributeNamespaceIds;

		// Token: 0x040079D6 RID: 31190
		private static readonly string[] eleTagNames;

		// Token: 0x040079D7 RID: 31191
		private static readonly byte[] eleNamespaceIds;
	}
}
