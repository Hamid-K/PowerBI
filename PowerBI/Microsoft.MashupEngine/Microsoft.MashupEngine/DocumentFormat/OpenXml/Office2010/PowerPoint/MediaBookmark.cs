using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023BC RID: 9148
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class MediaBookmark : OpenXmlLeafElement
	{
		// Token: 0x17004CB2 RID: 19634
		// (get) Token: 0x06010978 RID: 67960 RVA: 0x002E5202 File Offset: 0x002E3402
		public override string LocalName
		{
			get
			{
				return "bmk";
			}
		}

		// Token: 0x17004CB3 RID: 19635
		// (get) Token: 0x06010979 RID: 67961 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004CB4 RID: 19636
		// (get) Token: 0x0601097A RID: 67962 RVA: 0x002E5209 File Offset: 0x002E3409
		internal override int ElementTypeId
		{
			get
			{
				return 12802;
			}
		}

		// Token: 0x0601097B RID: 67963 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004CB5 RID: 19637
		// (get) Token: 0x0601097C RID: 67964 RVA: 0x002E5210 File Offset: 0x002E3410
		internal override string[] AttributeTagNames
		{
			get
			{
				return MediaBookmark.attributeTagNames;
			}
		}

		// Token: 0x17004CB6 RID: 19638
		// (get) Token: 0x0601097D RID: 67965 RVA: 0x002E5217 File Offset: 0x002E3417
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MediaBookmark.attributeNamespaceIds;
			}
		}

		// Token: 0x17004CB7 RID: 19639
		// (get) Token: 0x0601097E RID: 67966 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601097F RID: 67967 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17004CB8 RID: 19640
		// (get) Token: 0x06010980 RID: 67968 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010981 RID: 67969 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "time")]
		public StringValue Time
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

		// Token: 0x06010983 RID: 67971 RVA: 0x002E521E File Offset: 0x002E341E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "time" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010984 RID: 67972 RVA: 0x002E5254 File Offset: 0x002E3454
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MediaBookmark>(deep);
		}

		// Token: 0x06010985 RID: 67973 RVA: 0x002E5260 File Offset: 0x002E3460
		// Note: this type is marked as 'beforefieldinit'.
		static MediaBookmark()
		{
			byte[] array = new byte[2];
			MediaBookmark.attributeNamespaceIds = array;
		}

		// Token: 0x04007565 RID: 30053
		private const string tagName = "bmk";

		// Token: 0x04007566 RID: 30054
		private const byte tagNsId = 49;

		// Token: 0x04007567 RID: 30055
		internal const int ElementTypeIdConst = 12802;

		// Token: 0x04007568 RID: 30056
		private static string[] attributeTagNames = new string[] { "name", "time" };

		// Token: 0x04007569 RID: 30057
		private static byte[] attributeNamespaceIds;
	}
}
