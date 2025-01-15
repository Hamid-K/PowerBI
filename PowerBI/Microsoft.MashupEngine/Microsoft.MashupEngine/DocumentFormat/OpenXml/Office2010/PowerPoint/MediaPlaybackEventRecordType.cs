using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023C8 RID: 9160
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class MediaPlaybackEventRecordType : OpenXmlLeafElement
	{
		// Token: 0x17004D01 RID: 19713
		// (get) Token: 0x06010A28 RID: 68136 RVA: 0x002E58C3 File Offset: 0x002E3AC3
		internal override string[] AttributeTagNames
		{
			get
			{
				return MediaPlaybackEventRecordType.attributeTagNames;
			}
		}

		// Token: 0x17004D02 RID: 19714
		// (get) Token: 0x06010A29 RID: 68137 RVA: 0x002E58CA File Offset: 0x002E3ACA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MediaPlaybackEventRecordType.attributeNamespaceIds;
			}
		}

		// Token: 0x17004D03 RID: 19715
		// (get) Token: 0x06010A2A RID: 68138 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010A2B RID: 68139 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "time")]
		public StringValue Time
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

		// Token: 0x17004D04 RID: 19716
		// (get) Token: 0x06010A2C RID: 68140 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06010A2D RID: 68141 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "objId")]
		public UInt32Value ObjectId
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06010A2E RID: 68142 RVA: 0x002E58D1 File Offset: 0x002E3AD1
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "time" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "objId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010A30 RID: 68144 RVA: 0x002E5908 File Offset: 0x002E3B08
		// Note: this type is marked as 'beforefieldinit'.
		static MediaPlaybackEventRecordType()
		{
			byte[] array = new byte[2];
			MediaPlaybackEventRecordType.attributeNamespaceIds = array;
		}

		// Token: 0x0400759D RID: 30109
		private static string[] attributeTagNames = new string[] { "time", "objId" };

		// Token: 0x0400759E RID: 30110
		private static byte[] attributeNamespaceIds;
	}
}
