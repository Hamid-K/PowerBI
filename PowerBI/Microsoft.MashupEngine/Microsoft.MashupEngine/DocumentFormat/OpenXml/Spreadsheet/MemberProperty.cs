using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B80 RID: 11136
	[GeneratedCode("DomGen", "2.0")]
	internal class MemberProperty : OpenXmlLeafElement
	{
		// Token: 0x17007A35 RID: 31285
		// (get) Token: 0x0601703A RID: 94266 RVA: 0x00331B3D File Offset: 0x0032FD3D
		public override string LocalName
		{
			get
			{
				return "mp";
			}
		}

		// Token: 0x17007A36 RID: 31286
		// (get) Token: 0x0601703B RID: 94267 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A37 RID: 31287
		// (get) Token: 0x0601703C RID: 94268 RVA: 0x00331B44 File Offset: 0x0032FD44
		internal override int ElementTypeId
		{
			get
			{
				return 11114;
			}
		}

		// Token: 0x0601703D RID: 94269 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007A38 RID: 31288
		// (get) Token: 0x0601703E RID: 94270 RVA: 0x00331B4B File Offset: 0x0032FD4B
		internal override string[] AttributeTagNames
		{
			get
			{
				return MemberProperty.attributeTagNames;
			}
		}

		// Token: 0x17007A39 RID: 31289
		// (get) Token: 0x0601703F RID: 94271 RVA: 0x00331B52 File Offset: 0x0032FD52
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MemberProperty.attributeNamespaceIds;
			}
		}

		// Token: 0x17007A3A RID: 31290
		// (get) Token: 0x06017040 RID: 94272 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017041 RID: 94273 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007A3B RID: 31291
		// (get) Token: 0x06017042 RID: 94274 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017043 RID: 94275 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "showCell")]
		public BooleanValue ShowCell
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007A3C RID: 31292
		// (get) Token: 0x06017044 RID: 94276 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017045 RID: 94277 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showTip")]
		public BooleanValue ShowTip
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007A3D RID: 31293
		// (get) Token: 0x06017046 RID: 94278 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017047 RID: 94279 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "showAsCaption")]
		public BooleanValue ShowAsCaption
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007A3E RID: 31294
		// (get) Token: 0x06017048 RID: 94280 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06017049 RID: 94281 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "nameLen")]
		public UInt32Value NameLength
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007A3F RID: 31295
		// (get) Token: 0x0601704A RID: 94282 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x0601704B RID: 94283 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "pPos")]
		public UInt32Value PropertyNamePosition
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007A40 RID: 31296
		// (get) Token: 0x0601704C RID: 94284 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x0601704D RID: 94285 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "pLen")]
		public UInt32Value PropertyNameLength
		{
			get
			{
				return (UInt32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007A41 RID: 31297
		// (get) Token: 0x0601704E RID: 94286 RVA: 0x0032B268 File Offset: 0x00329468
		// (set) Token: 0x0601704F RID: 94287 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "level")]
		public UInt32Value Level
		{
			get
			{
				return (UInt32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007A42 RID: 31298
		// (get) Token: 0x06017050 RID: 94288 RVA: 0x002F6806 File Offset: 0x002F4A06
		// (set) Token: 0x06017051 RID: 94289 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "field")]
		public UInt32Value Field
		{
			get
			{
				return (UInt32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x06017053 RID: 94291 RVA: 0x00331B5C File Offset: 0x0032FD5C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showCell" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showTip" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showAsCaption" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "nameLen" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "pPos" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "pLen" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "level" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "field" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017054 RID: 94292 RVA: 0x00331C37 File Offset: 0x0032FE37
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MemberProperty>(deep);
		}

		// Token: 0x06017055 RID: 94293 RVA: 0x00331C40 File Offset: 0x0032FE40
		// Note: this type is marked as 'beforefieldinit'.
		static MemberProperty()
		{
			byte[] array = new byte[9];
			MemberProperty.attributeNamespaceIds = array;
		}

		// Token: 0x04009AC1 RID: 39617
		private const string tagName = "mp";

		// Token: 0x04009AC2 RID: 39618
		private const byte tagNsId = 22;

		// Token: 0x04009AC3 RID: 39619
		internal const int ElementTypeIdConst = 11114;

		// Token: 0x04009AC4 RID: 39620
		private static string[] attributeTagNames = new string[] { "name", "showCell", "showTip", "showAsCaption", "nameLen", "pPos", "pLen", "level", "field" };

		// Token: 0x04009AC5 RID: 39621
		private static byte[] attributeNamespaceIds;
	}
}
