using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024C8 RID: 9416
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SphereCoordinates), FileFormatVersions.Office2010)]
	internal class LightRig : OpenXmlCompositeElement
	{
		// Token: 0x170052E2 RID: 21218
		// (get) Token: 0x0601176D RID: 71533 RVA: 0x002EEB5C File Offset: 0x002ECD5C
		public override string LocalName
		{
			get
			{
				return "lightRig";
			}
		}

		// Token: 0x170052E3 RID: 21219
		// (get) Token: 0x0601176E RID: 71534 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052E4 RID: 21220
		// (get) Token: 0x0601176F RID: 71535 RVA: 0x002EEB63 File Offset: 0x002ECD63
		internal override int ElementTypeId
		{
			get
			{
				return 12888;
			}
		}

		// Token: 0x06011770 RID: 71536 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170052E5 RID: 21221
		// (get) Token: 0x06011771 RID: 71537 RVA: 0x002EEB6A File Offset: 0x002ECD6A
		internal override string[] AttributeTagNames
		{
			get
			{
				return LightRig.attributeTagNames;
			}
		}

		// Token: 0x170052E6 RID: 21222
		// (get) Token: 0x06011772 RID: 71538 RVA: 0x002EEB71 File Offset: 0x002ECD71
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LightRig.attributeNamespaceIds;
			}
		}

		// Token: 0x170052E7 RID: 21223
		// (get) Token: 0x06011773 RID: 71539 RVA: 0x002EEB78 File Offset: 0x002ECD78
		// (set) Token: 0x06011774 RID: 71540 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "rig")]
		public EnumValue<LightRigTypeValues> LightRigType
		{
			get
			{
				return (EnumValue<LightRigTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170052E8 RID: 21224
		// (get) Token: 0x06011775 RID: 71541 RVA: 0x002EEB87 File Offset: 0x002ECD87
		// (set) Token: 0x06011776 RID: 71542 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(52, "dir")]
		public EnumValue<LightRigDirectionValues> LightDirectionType
		{
			get
			{
				return (EnumValue<LightRigDirectionValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06011777 RID: 71543 RVA: 0x00293ECF File Offset: 0x002920CF
		public LightRig()
		{
		}

		// Token: 0x06011778 RID: 71544 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LightRig(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011779 RID: 71545 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LightRig(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601177A RID: 71546 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LightRig(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601177B RID: 71547 RVA: 0x002EEB96 File Offset: 0x002ECD96
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "rot" == name)
			{
				return new SphereCoordinates();
			}
			return null;
		}

		// Token: 0x170052E9 RID: 21225
		// (get) Token: 0x0601177C RID: 71548 RVA: 0x002EEBB1 File Offset: 0x002ECDB1
		internal override string[] ElementTagNames
		{
			get
			{
				return LightRig.eleTagNames;
			}
		}

		// Token: 0x170052EA RID: 21226
		// (get) Token: 0x0601177D RID: 71549 RVA: 0x002EEBB8 File Offset: 0x002ECDB8
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LightRig.eleNamespaceIds;
			}
		}

		// Token: 0x170052EB RID: 21227
		// (get) Token: 0x0601177E RID: 71550 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170052EC RID: 21228
		// (get) Token: 0x0601177F RID: 71551 RVA: 0x002EEBBF File Offset: 0x002ECDBF
		// (set) Token: 0x06011780 RID: 71552 RVA: 0x002EEBC8 File Offset: 0x002ECDC8
		public SphereCoordinates SphereCoordinates
		{
			get
			{
				return base.GetElement<SphereCoordinates>(0);
			}
			set
			{
				base.SetElement<SphereCoordinates>(0, value);
			}
		}

		// Token: 0x06011781 RID: 71553 RVA: 0x002EEBD2 File Offset: 0x002ECDD2
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "rig" == name)
			{
				return new EnumValue<LightRigTypeValues>();
			}
			if (52 == namespaceId && "dir" == name)
			{
				return new EnumValue<LightRigDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011782 RID: 71554 RVA: 0x002EEC0C File Offset: 0x002ECE0C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LightRig>(deep);
		}

		// Token: 0x040079F4 RID: 31220
		private const string tagName = "lightRig";

		// Token: 0x040079F5 RID: 31221
		private const byte tagNsId = 52;

		// Token: 0x040079F6 RID: 31222
		internal const int ElementTypeIdConst = 12888;

		// Token: 0x040079F7 RID: 31223
		private static string[] attributeTagNames = new string[] { "rig", "dir" };

		// Token: 0x040079F8 RID: 31224
		private static byte[] attributeNamespaceIds = new byte[] { 52, 52 };

		// Token: 0x040079F9 RID: 31225
		private static readonly string[] eleTagNames = new string[] { "rot" };

		// Token: 0x040079FA RID: 31226
		private static readonly byte[] eleNamespaceIds = new byte[] { 52 };
	}
}
