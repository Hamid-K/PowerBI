using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F6F RID: 12143
	[ChildElementInfo(typeof(Active))]
	[ChildElementInfo(typeof(UniqueTag))]
	[ChildElementInfo(typeof(ColumnIndex))]
	[GeneratedCode("DomGen", "2.0")]
	internal class RecipientData : OpenXmlCompositeElement
	{
		// Token: 0x170090E0 RID: 37088
		// (get) Token: 0x0601A1DD RID: 106973 RVA: 0x002EC0C0 File Offset: 0x002EA2C0
		public override string LocalName
		{
			get
			{
				return "recipientData";
			}
		}

		// Token: 0x170090E1 RID: 37089
		// (get) Token: 0x0601A1DE RID: 106974 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170090E2 RID: 37090
		// (get) Token: 0x0601A1DF RID: 106975 RVA: 0x0035D9E0 File Offset: 0x0035BBE0
		internal override int ElementTypeId
		{
			get
			{
				return 11799;
			}
		}

		// Token: 0x0601A1E0 RID: 106976 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A1E1 RID: 106977 RVA: 0x00293ECF File Offset: 0x002920CF
		public RecipientData()
		{
		}

		// Token: 0x0601A1E2 RID: 106978 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RecipientData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A1E3 RID: 106979 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RecipientData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A1E4 RID: 106980 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RecipientData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A1E5 RID: 106981 RVA: 0x0035D9E8 File Offset: 0x0035BBE8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "active" == name)
			{
				return new Active();
			}
			if (23 == namespaceId && "column" == name)
			{
				return new ColumnIndex();
			}
			if (23 == namespaceId && "uniqueTag" == name)
			{
				return new UniqueTag();
			}
			return null;
		}

		// Token: 0x170090E3 RID: 37091
		// (get) Token: 0x0601A1E6 RID: 106982 RVA: 0x0035DA3E File Offset: 0x0035BC3E
		internal override string[] ElementTagNames
		{
			get
			{
				return RecipientData.eleTagNames;
			}
		}

		// Token: 0x170090E4 RID: 37092
		// (get) Token: 0x0601A1E7 RID: 106983 RVA: 0x0035DA45 File Offset: 0x0035BC45
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RecipientData.eleNamespaceIds;
			}
		}

		// Token: 0x170090E5 RID: 37093
		// (get) Token: 0x0601A1E8 RID: 106984 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170090E6 RID: 37094
		// (get) Token: 0x0601A1E9 RID: 106985 RVA: 0x0035DA4C File Offset: 0x0035BC4C
		// (set) Token: 0x0601A1EA RID: 106986 RVA: 0x0035DA55 File Offset: 0x0035BC55
		public Active Active
		{
			get
			{
				return base.GetElement<Active>(0);
			}
			set
			{
				base.SetElement<Active>(0, value);
			}
		}

		// Token: 0x170090E7 RID: 37095
		// (get) Token: 0x0601A1EB RID: 106987 RVA: 0x0035DA5F File Offset: 0x0035BC5F
		// (set) Token: 0x0601A1EC RID: 106988 RVA: 0x0035DA68 File Offset: 0x0035BC68
		public ColumnIndex ColumnIndex
		{
			get
			{
				return base.GetElement<ColumnIndex>(1);
			}
			set
			{
				base.SetElement<ColumnIndex>(1, value);
			}
		}

		// Token: 0x170090E8 RID: 37096
		// (get) Token: 0x0601A1ED RID: 106989 RVA: 0x0035DA72 File Offset: 0x0035BC72
		// (set) Token: 0x0601A1EE RID: 106990 RVA: 0x0035DA7B File Offset: 0x0035BC7B
		public UniqueTag UniqueTag
		{
			get
			{
				return base.GetElement<UniqueTag>(2);
			}
			set
			{
				base.SetElement<UniqueTag>(2, value);
			}
		}

		// Token: 0x0601A1EF RID: 106991 RVA: 0x0035DA85 File Offset: 0x0035BC85
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RecipientData>(deep);
		}

		// Token: 0x0400ABE0 RID: 44000
		private const string tagName = "recipientData";

		// Token: 0x0400ABE1 RID: 44001
		private const byte tagNsId = 23;

		// Token: 0x0400ABE2 RID: 44002
		internal const int ElementTypeIdConst = 11799;

		// Token: 0x0400ABE3 RID: 44003
		private static readonly string[] eleTagNames = new string[] { "active", "column", "uniqueTag" };

		// Token: 0x0400ABE4 RID: 44004
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
