using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200236E RID: 9070
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ArtisticPhotocopy : OpenXmlLeafElement
	{
		// Token: 0x17004AB4 RID: 19124
		// (get) Token: 0x06010519 RID: 66841 RVA: 0x002E2127 File Offset: 0x002E0327
		public override string LocalName
		{
			get
			{
				return "artisticPhotocopy";
			}
		}

		// Token: 0x17004AB5 RID: 19125
		// (get) Token: 0x0601051A RID: 66842 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004AB6 RID: 19126
		// (get) Token: 0x0601051B RID: 66843 RVA: 0x002E212E File Offset: 0x002E032E
		internal override int ElementTypeId
		{
			get
			{
				return 12753;
			}
		}

		// Token: 0x0601051C RID: 66844 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004AB7 RID: 19127
		// (get) Token: 0x0601051D RID: 66845 RVA: 0x002E2135 File Offset: 0x002E0335
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticPhotocopy.attributeTagNames;
			}
		}

		// Token: 0x17004AB8 RID: 19128
		// (get) Token: 0x0601051E RID: 66846 RVA: 0x002E213C File Offset: 0x002E033C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticPhotocopy.attributeNamespaceIds;
			}
		}

		// Token: 0x17004AB9 RID: 19129
		// (get) Token: 0x0601051F RID: 66847 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010520 RID: 66848 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "trans")]
		public Int32Value Transparancy
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

		// Token: 0x17004ABA RID: 19130
		// (get) Token: 0x06010521 RID: 66849 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06010522 RID: 66850 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "detail")]
		public Int32Value Detail
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

		// Token: 0x06010524 RID: 66852 RVA: 0x002E2143 File Offset: 0x002E0343
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "detail" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010525 RID: 66853 RVA: 0x002E2179 File Offset: 0x002E0379
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticPhotocopy>(deep);
		}

		// Token: 0x06010526 RID: 66854 RVA: 0x002E2184 File Offset: 0x002E0384
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticPhotocopy()
		{
			byte[] array = new byte[2];
			ArtisticPhotocopy.attributeNamespaceIds = array;
		}

		// Token: 0x04007419 RID: 29721
		private const string tagName = "artisticPhotocopy";

		// Token: 0x0400741A RID: 29722
		private const byte tagNsId = 48;

		// Token: 0x0400741B RID: 29723
		internal const int ElementTypeIdConst = 12753;

		// Token: 0x0400741C RID: 29724
		private static string[] attributeTagNames = new string[] { "trans", "detail" };

		// Token: 0x0400741D RID: 29725
		private static byte[] attributeNamespaceIds;
	}
}
