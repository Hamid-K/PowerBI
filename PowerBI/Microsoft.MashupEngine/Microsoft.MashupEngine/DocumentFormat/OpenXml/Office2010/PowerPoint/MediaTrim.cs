using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023BD RID: 9149
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class MediaTrim : OpenXmlLeafElement
	{
		// Token: 0x17004CB9 RID: 19641
		// (get) Token: 0x06010986 RID: 67974 RVA: 0x002E5297 File Offset: 0x002E3497
		public override string LocalName
		{
			get
			{
				return "trim";
			}
		}

		// Token: 0x17004CBA RID: 19642
		// (get) Token: 0x06010987 RID: 67975 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004CBB RID: 19643
		// (get) Token: 0x06010988 RID: 67976 RVA: 0x002E529E File Offset: 0x002E349E
		internal override int ElementTypeId
		{
			get
			{
				return 12803;
			}
		}

		// Token: 0x06010989 RID: 67977 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004CBC RID: 19644
		// (get) Token: 0x0601098A RID: 67978 RVA: 0x002E52A5 File Offset: 0x002E34A5
		internal override string[] AttributeTagNames
		{
			get
			{
				return MediaTrim.attributeTagNames;
			}
		}

		// Token: 0x17004CBD RID: 19645
		// (get) Token: 0x0601098B RID: 67979 RVA: 0x002E52AC File Offset: 0x002E34AC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MediaTrim.attributeNamespaceIds;
			}
		}

		// Token: 0x17004CBE RID: 19646
		// (get) Token: 0x0601098C RID: 67980 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601098D RID: 67981 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "st")]
		public StringValue Start
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004CBF RID: 19647
		// (get) Token: 0x0601098E RID: 67982 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601098F RID: 67983 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "end")]
		public StringValue End
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06010991 RID: 67985 RVA: 0x002E52B3 File Offset: 0x002E34B3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "st" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "end" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010992 RID: 67986 RVA: 0x002E52E9 File Offset: 0x002E34E9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MediaTrim>(deep);
		}

		// Token: 0x06010993 RID: 67987 RVA: 0x002E52F4 File Offset: 0x002E34F4
		// Note: this type is marked as 'beforefieldinit'.
		static MediaTrim()
		{
			byte[] array = new byte[2];
			MediaTrim.attributeNamespaceIds = array;
		}

		// Token: 0x0400756A RID: 30058
		private const string tagName = "trim";

		// Token: 0x0400756B RID: 30059
		private const byte tagNsId = 49;

		// Token: 0x0400756C RID: 30060
		internal const int ElementTypeIdConst = 12803;

		// Token: 0x0400756D RID: 30061
		private static string[] attributeTagNames = new string[] { "st", "end" };

		// Token: 0x0400756E RID: 30062
		private static byte[] attributeNamespaceIds;
	}
}
