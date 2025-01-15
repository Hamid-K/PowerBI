using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A1C RID: 10780
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CommonBehavior))]
	internal class AnimateRotation : OpenXmlCompositeElement
	{
		// Token: 0x1700702F RID: 28719
		// (get) Token: 0x060159B4 RID: 88500 RVA: 0x003212EB File Offset: 0x0031F4EB
		public override string LocalName
		{
			get
			{
				return "animRot";
			}
		}

		// Token: 0x17007030 RID: 28720
		// (get) Token: 0x060159B5 RID: 88501 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007031 RID: 28721
		// (get) Token: 0x060159B6 RID: 88502 RVA: 0x003212F2 File Offset: 0x0031F4F2
		internal override int ElementTypeId
		{
			get
			{
				return 12206;
			}
		}

		// Token: 0x060159B7 RID: 88503 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007032 RID: 28722
		// (get) Token: 0x060159B8 RID: 88504 RVA: 0x003212F9 File Offset: 0x0031F4F9
		internal override string[] AttributeTagNames
		{
			get
			{
				return AnimateRotation.attributeTagNames;
			}
		}

		// Token: 0x17007033 RID: 28723
		// (get) Token: 0x060159B9 RID: 88505 RVA: 0x00321300 File Offset: 0x0031F500
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AnimateRotation.attributeNamespaceIds;
			}
		}

		// Token: 0x17007034 RID: 28724
		// (get) Token: 0x060159BA RID: 88506 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060159BB RID: 88507 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "by")]
		public Int32Value By
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

		// Token: 0x17007035 RID: 28725
		// (get) Token: 0x060159BC RID: 88508 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060159BD RID: 88509 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "from")]
		public Int32Value From
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007036 RID: 28726
		// (get) Token: 0x060159BE RID: 88510 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x060159BF RID: 88511 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "to")]
		public Int32Value To
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007037 RID: 28727
		// (get) Token: 0x060159C0 RID: 88512 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x060159C1 RID: 88513 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(49, "bounceEnd")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Int32Value BounceEnd
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x060159C2 RID: 88514 RVA: 0x00293ECF File Offset: 0x002920CF
		public AnimateRotation()
		{
		}

		// Token: 0x060159C3 RID: 88515 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AnimateRotation(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060159C4 RID: 88516 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AnimateRotation(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060159C5 RID: 88517 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AnimateRotation(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060159C6 RID: 88518 RVA: 0x00321307 File Offset: 0x0031F507
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cBhvr" == name)
			{
				return new CommonBehavior();
			}
			return null;
		}

		// Token: 0x17007038 RID: 28728
		// (get) Token: 0x060159C7 RID: 88519 RVA: 0x00321322 File Offset: 0x0031F522
		internal override string[] ElementTagNames
		{
			get
			{
				return AnimateRotation.eleTagNames;
			}
		}

		// Token: 0x17007039 RID: 28729
		// (get) Token: 0x060159C8 RID: 88520 RVA: 0x00321329 File Offset: 0x0031F529
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AnimateRotation.eleNamespaceIds;
			}
		}

		// Token: 0x1700703A RID: 28730
		// (get) Token: 0x060159C9 RID: 88521 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700703B RID: 28731
		// (get) Token: 0x060159CA RID: 88522 RVA: 0x00320C27 File Offset: 0x0031EE27
		// (set) Token: 0x060159CB RID: 88523 RVA: 0x00320C30 File Offset: 0x0031EE30
		public CommonBehavior CommonBehavior
		{
			get
			{
				return base.GetElement<CommonBehavior>(0);
			}
			set
			{
				base.SetElement<CommonBehavior>(0, value);
			}
		}

		// Token: 0x060159CC RID: 88524 RVA: 0x00321330 File Offset: 0x0031F530
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "by" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "from" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "to" == name)
			{
				return new Int32Value();
			}
			if (49 == namespaceId && "bounceEnd" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060159CD RID: 88525 RVA: 0x0032139F File Offset: 0x0031F59F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AnimateRotation>(deep);
		}

		// Token: 0x0400940F RID: 37903
		private const string tagName = "animRot";

		// Token: 0x04009410 RID: 37904
		private const byte tagNsId = 24;

		// Token: 0x04009411 RID: 37905
		internal const int ElementTypeIdConst = 12206;

		// Token: 0x04009412 RID: 37906
		private static string[] attributeTagNames = new string[] { "by", "from", "to", "bounceEnd" };

		// Token: 0x04009413 RID: 37907
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 0, 49 };

		// Token: 0x04009414 RID: 37908
		private static readonly string[] eleTagNames = new string[] { "cBhvr" };

		// Token: 0x04009415 RID: 37909
		private static readonly byte[] eleNamespaceIds = new byte[] { 24 };
	}
}
