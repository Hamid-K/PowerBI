using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F72 RID: 12146
	[ChildElementInfo(typeof(Name))]
	[ChildElementInfo(typeof(LanguageId))]
	[ChildElementInfo(typeof(MailMergeFieldType))]
	[ChildElementInfo(typeof(ColumnIndex))]
	[ChildElementInfo(typeof(DynamicAddress))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MappedName))]
	internal class FieldMapData : OpenXmlCompositeElement
	{
		// Token: 0x170090F5 RID: 37109
		// (get) Token: 0x0601A209 RID: 107017 RVA: 0x0035DBE0 File Offset: 0x0035BDE0
		public override string LocalName
		{
			get
			{
				return "fieldMapData";
			}
		}

		// Token: 0x170090F6 RID: 37110
		// (get) Token: 0x0601A20A RID: 107018 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170090F7 RID: 37111
		// (get) Token: 0x0601A20B RID: 107019 RVA: 0x0035DBE7 File Offset: 0x0035BDE7
		internal override int ElementTypeId
		{
			get
			{
				return 11810;
			}
		}

		// Token: 0x0601A20C RID: 107020 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A20D RID: 107021 RVA: 0x00293ECF File Offset: 0x002920CF
		public FieldMapData()
		{
		}

		// Token: 0x0601A20E RID: 107022 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FieldMapData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A20F RID: 107023 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FieldMapData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A210 RID: 107024 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FieldMapData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A211 RID: 107025 RVA: 0x0035DBF0 File Offset: 0x0035BDF0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "type" == name)
			{
				return new MailMergeFieldType();
			}
			if (23 == namespaceId && "name" == name)
			{
				return new Name();
			}
			if (23 == namespaceId && "mappedName" == name)
			{
				return new MappedName();
			}
			if (23 == namespaceId && "column" == name)
			{
				return new ColumnIndex();
			}
			if (23 == namespaceId && "lid" == name)
			{
				return new LanguageId();
			}
			if (23 == namespaceId && "dynamicAddress" == name)
			{
				return new DynamicAddress();
			}
			return null;
		}

		// Token: 0x170090F8 RID: 37112
		// (get) Token: 0x0601A212 RID: 107026 RVA: 0x0035DC8E File Offset: 0x0035BE8E
		internal override string[] ElementTagNames
		{
			get
			{
				return FieldMapData.eleTagNames;
			}
		}

		// Token: 0x170090F9 RID: 37113
		// (get) Token: 0x0601A213 RID: 107027 RVA: 0x0035DC95 File Offset: 0x0035BE95
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FieldMapData.eleNamespaceIds;
			}
		}

		// Token: 0x170090FA RID: 37114
		// (get) Token: 0x0601A214 RID: 107028 RVA: 0x0000240C File Offset: 0x0000060C
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneAll;
			}
		}

		// Token: 0x170090FB RID: 37115
		// (get) Token: 0x0601A215 RID: 107029 RVA: 0x0035DC9C File Offset: 0x0035BE9C
		// (set) Token: 0x0601A216 RID: 107030 RVA: 0x0035DCA5 File Offset: 0x0035BEA5
		public MailMergeFieldType MailMergeFieldType
		{
			get
			{
				return base.GetElement<MailMergeFieldType>(0);
			}
			set
			{
				base.SetElement<MailMergeFieldType>(0, value);
			}
		}

		// Token: 0x170090FC RID: 37116
		// (get) Token: 0x0601A217 RID: 107031 RVA: 0x0035DCAF File Offset: 0x0035BEAF
		// (set) Token: 0x0601A218 RID: 107032 RVA: 0x0035DCB8 File Offset: 0x0035BEB8
		public Name Name
		{
			get
			{
				return base.GetElement<Name>(1);
			}
			set
			{
				base.SetElement<Name>(1, value);
			}
		}

		// Token: 0x170090FD RID: 37117
		// (get) Token: 0x0601A219 RID: 107033 RVA: 0x0035DCC2 File Offset: 0x0035BEC2
		// (set) Token: 0x0601A21A RID: 107034 RVA: 0x0035DCCB File Offset: 0x0035BECB
		public MappedName MappedName
		{
			get
			{
				return base.GetElement<MappedName>(2);
			}
			set
			{
				base.SetElement<MappedName>(2, value);
			}
		}

		// Token: 0x170090FE RID: 37118
		// (get) Token: 0x0601A21B RID: 107035 RVA: 0x0035DCD5 File Offset: 0x0035BED5
		// (set) Token: 0x0601A21C RID: 107036 RVA: 0x0035DCDE File Offset: 0x0035BEDE
		public ColumnIndex ColumnIndex
		{
			get
			{
				return base.GetElement<ColumnIndex>(3);
			}
			set
			{
				base.SetElement<ColumnIndex>(3, value);
			}
		}

		// Token: 0x170090FF RID: 37119
		// (get) Token: 0x0601A21D RID: 107037 RVA: 0x0035AAC0 File Offset: 0x00358CC0
		// (set) Token: 0x0601A21E RID: 107038 RVA: 0x0035AAC9 File Offset: 0x00358CC9
		public LanguageId LanguageId
		{
			get
			{
				return base.GetElement<LanguageId>(4);
			}
			set
			{
				base.SetElement<LanguageId>(4, value);
			}
		}

		// Token: 0x17009100 RID: 37120
		// (get) Token: 0x0601A21F RID: 107039 RVA: 0x0035DCE8 File Offset: 0x0035BEE8
		// (set) Token: 0x0601A220 RID: 107040 RVA: 0x0035DCF1 File Offset: 0x0035BEF1
		public DynamicAddress DynamicAddress
		{
			get
			{
				return base.GetElement<DynamicAddress>(5);
			}
			set
			{
				base.SetElement<DynamicAddress>(5, value);
			}
		}

		// Token: 0x0601A221 RID: 107041 RVA: 0x0035DCFB File Offset: 0x0035BEFB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FieldMapData>(deep);
		}

		// Token: 0x0400ABEF RID: 44015
		private const string tagName = "fieldMapData";

		// Token: 0x0400ABF0 RID: 44016
		private const byte tagNsId = 23;

		// Token: 0x0400ABF1 RID: 44017
		internal const int ElementTypeIdConst = 11810;

		// Token: 0x0400ABF2 RID: 44018
		private static readonly string[] eleTagNames = new string[] { "type", "name", "mappedName", "column", "lid", "dynamicAddress" };

		// Token: 0x0400ABF3 RID: 44019
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23 };
	}
}
