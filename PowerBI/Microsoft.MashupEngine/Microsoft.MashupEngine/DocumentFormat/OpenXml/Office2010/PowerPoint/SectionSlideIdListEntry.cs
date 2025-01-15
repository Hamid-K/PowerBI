using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023C2 RID: 9154
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SectionSlideIdListEntry : OpenXmlLeafElement
	{
		// Token: 0x17004CD9 RID: 19673
		// (get) Token: 0x060109CF RID: 68047 RVA: 0x002E552E File Offset: 0x002E372E
		public override string LocalName
		{
			get
			{
				return "sldId";
			}
		}

		// Token: 0x17004CDA RID: 19674
		// (get) Token: 0x060109D0 RID: 68048 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004CDB RID: 19675
		// (get) Token: 0x060109D1 RID: 68049 RVA: 0x002E5535 File Offset: 0x002E3735
		internal override int ElementTypeId
		{
			get
			{
				return 12808;
			}
		}

		// Token: 0x060109D2 RID: 68050 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004CDC RID: 19676
		// (get) Token: 0x060109D3 RID: 68051 RVA: 0x002E553C File Offset: 0x002E373C
		internal override string[] AttributeTagNames
		{
			get
			{
				return SectionSlideIdListEntry.attributeTagNames;
			}
		}

		// Token: 0x17004CDD RID: 19677
		// (get) Token: 0x060109D4 RID: 68052 RVA: 0x002E5543 File Offset: 0x002E3743
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SectionSlideIdListEntry.attributeNamespaceIds;
			}
		}

		// Token: 0x17004CDE RID: 19678
		// (get) Token: 0x060109D5 RID: 68053 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060109D6 RID: 68054 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
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

		// Token: 0x060109D8 RID: 68056 RVA: 0x002E554A File Offset: 0x002E374A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060109D9 RID: 68057 RVA: 0x002E556A File Offset: 0x002E376A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SectionSlideIdListEntry>(deep);
		}

		// Token: 0x060109DA RID: 68058 RVA: 0x002E5574 File Offset: 0x002E3774
		// Note: this type is marked as 'beforefieldinit'.
		static SectionSlideIdListEntry()
		{
			byte[] array = new byte[1];
			SectionSlideIdListEntry.attributeNamespaceIds = array;
		}

		// Token: 0x04007581 RID: 30081
		private const string tagName = "sldId";

		// Token: 0x04007582 RID: 30082
		private const byte tagNsId = 49;

		// Token: 0x04007583 RID: 30083
		internal const int ElementTypeIdConst = 12808;

		// Token: 0x04007584 RID: 30084
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04007585 RID: 30085
		private static byte[] attributeNamespaceIds;
	}
}
