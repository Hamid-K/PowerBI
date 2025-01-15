using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AB0 RID: 10928
	[ChildElementInfo(typeof(HandoutMasterId))]
	[GeneratedCode("DomGen", "2.0")]
	internal class HandoutMasterIdList : OpenXmlCompositeElement
	{
		// Token: 0x170074B5 RID: 29877
		// (get) Token: 0x060163DA RID: 91098 RVA: 0x003281C4 File Offset: 0x003263C4
		public override string LocalName
		{
			get
			{
				return "handoutMasterIdLst";
			}
		}

		// Token: 0x170074B6 RID: 29878
		// (get) Token: 0x060163DB RID: 91099 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074B7 RID: 29879
		// (get) Token: 0x060163DC RID: 91100 RVA: 0x003281CB File Offset: 0x003263CB
		internal override int ElementTypeId
		{
			get
			{
				return 12343;
			}
		}

		// Token: 0x060163DD RID: 91101 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060163DE RID: 91102 RVA: 0x00293ECF File Offset: 0x002920CF
		public HandoutMasterIdList()
		{
		}

		// Token: 0x060163DF RID: 91103 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HandoutMasterIdList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163E0 RID: 91104 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HandoutMasterIdList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163E1 RID: 91105 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HandoutMasterIdList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060163E2 RID: 91106 RVA: 0x003281D2 File Offset: 0x003263D2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "handoutMasterId" == name)
			{
				return new HandoutMasterId();
			}
			return null;
		}

		// Token: 0x170074B8 RID: 29880
		// (get) Token: 0x060163E3 RID: 91107 RVA: 0x003281ED File Offset: 0x003263ED
		internal override string[] ElementTagNames
		{
			get
			{
				return HandoutMasterIdList.eleTagNames;
			}
		}

		// Token: 0x170074B9 RID: 29881
		// (get) Token: 0x060163E4 RID: 91108 RVA: 0x003281F4 File Offset: 0x003263F4
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return HandoutMasterIdList.eleNamespaceIds;
			}
		}

		// Token: 0x170074BA RID: 29882
		// (get) Token: 0x060163E5 RID: 91109 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170074BB RID: 29883
		// (get) Token: 0x060163E6 RID: 91110 RVA: 0x003281FB File Offset: 0x003263FB
		// (set) Token: 0x060163E7 RID: 91111 RVA: 0x00328204 File Offset: 0x00326404
		public HandoutMasterId HandoutMasterId
		{
			get
			{
				return base.GetElement<HandoutMasterId>(0);
			}
			set
			{
				base.SetElement<HandoutMasterId>(0, value);
			}
		}

		// Token: 0x060163E8 RID: 91112 RVA: 0x0032820E File Offset: 0x0032640E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HandoutMasterIdList>(deep);
		}

		// Token: 0x040096D2 RID: 38610
		private const string tagName = "handoutMasterIdLst";

		// Token: 0x040096D3 RID: 38611
		private const byte tagNsId = 24;

		// Token: 0x040096D4 RID: 38612
		internal const int ElementTypeIdConst = 12343;

		// Token: 0x040096D5 RID: 38613
		private static readonly string[] eleTagNames = new string[] { "handoutMasterId" };

		// Token: 0x040096D6 RID: 38614
		private static readonly byte[] eleNamespaceIds = new byte[] { 24 };
	}
}
