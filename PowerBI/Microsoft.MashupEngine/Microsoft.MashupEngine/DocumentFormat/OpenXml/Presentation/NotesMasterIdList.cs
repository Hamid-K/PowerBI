using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AAF RID: 10927
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NotesMasterId))]
	internal class NotesMasterIdList : OpenXmlCompositeElement
	{
		// Token: 0x170074AE RID: 29870
		// (get) Token: 0x060163CA RID: 91082 RVA: 0x0032813B File Offset: 0x0032633B
		public override string LocalName
		{
			get
			{
				return "notesMasterIdLst";
			}
		}

		// Token: 0x170074AF RID: 29871
		// (get) Token: 0x060163CB RID: 91083 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074B0 RID: 29872
		// (get) Token: 0x060163CC RID: 91084 RVA: 0x00328142 File Offset: 0x00326342
		internal override int ElementTypeId
		{
			get
			{
				return 12342;
			}
		}

		// Token: 0x060163CD RID: 91085 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060163CE RID: 91086 RVA: 0x00293ECF File Offset: 0x002920CF
		public NotesMasterIdList()
		{
		}

		// Token: 0x060163CF RID: 91087 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NotesMasterIdList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163D0 RID: 91088 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NotesMasterIdList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163D1 RID: 91089 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NotesMasterIdList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060163D2 RID: 91090 RVA: 0x00328149 File Offset: 0x00326349
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "notesMasterId" == name)
			{
				return new NotesMasterId();
			}
			return null;
		}

		// Token: 0x170074B1 RID: 29873
		// (get) Token: 0x060163D3 RID: 91091 RVA: 0x00328164 File Offset: 0x00326364
		internal override string[] ElementTagNames
		{
			get
			{
				return NotesMasterIdList.eleTagNames;
			}
		}

		// Token: 0x170074B2 RID: 29874
		// (get) Token: 0x060163D4 RID: 91092 RVA: 0x0032816B File Offset: 0x0032636B
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NotesMasterIdList.eleNamespaceIds;
			}
		}

		// Token: 0x170074B3 RID: 29875
		// (get) Token: 0x060163D5 RID: 91093 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170074B4 RID: 29876
		// (get) Token: 0x060163D6 RID: 91094 RVA: 0x00328172 File Offset: 0x00326372
		// (set) Token: 0x060163D7 RID: 91095 RVA: 0x0032817B File Offset: 0x0032637B
		public NotesMasterId NotesMasterId
		{
			get
			{
				return base.GetElement<NotesMasterId>(0);
			}
			set
			{
				base.SetElement<NotesMasterId>(0, value);
			}
		}

		// Token: 0x060163D8 RID: 91096 RVA: 0x00328185 File Offset: 0x00326385
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NotesMasterIdList>(deep);
		}

		// Token: 0x040096CD RID: 38605
		private const string tagName = "notesMasterIdLst";

		// Token: 0x040096CE RID: 38606
		private const byte tagNsId = 24;

		// Token: 0x040096CF RID: 38607
		internal const int ElementTypeIdConst = 12342;

		// Token: 0x040096D0 RID: 38608
		private static readonly string[] eleTagNames = new string[] { "notesMasterId" };

		// Token: 0x040096D1 RID: 38609
		private static readonly byte[] eleNamespaceIds = new byte[] { 24 };
	}
}
