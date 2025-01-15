using System;
using System.CodeDom.Compiler;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023C7 RID: 9159
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TriggerEventRecord : OpenXmlLeafElement
	{
		// Token: 0x17004CF9 RID: 19705
		// (get) Token: 0x06010A18 RID: 68120 RVA: 0x002E57E9 File Offset: 0x002E39E9
		public override string LocalName
		{
			get
			{
				return "triggerEvt";
			}
		}

		// Token: 0x17004CFA RID: 19706
		// (get) Token: 0x06010A19 RID: 68121 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004CFB RID: 19707
		// (get) Token: 0x06010A1A RID: 68122 RVA: 0x002E57F0 File Offset: 0x002E39F0
		internal override int ElementTypeId
		{
			get
			{
				return 12813;
			}
		}

		// Token: 0x06010A1B RID: 68123 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004CFC RID: 19708
		// (get) Token: 0x06010A1C RID: 68124 RVA: 0x002E57F7 File Offset: 0x002E39F7
		internal override string[] AttributeTagNames
		{
			get
			{
				return TriggerEventRecord.attributeTagNames;
			}
		}

		// Token: 0x17004CFD RID: 19709
		// (get) Token: 0x06010A1D RID: 68125 RVA: 0x002E57FE File Offset: 0x002E39FE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TriggerEventRecord.attributeNamespaceIds;
			}
		}

		// Token: 0x17004CFE RID: 19710
		// (get) Token: 0x06010A1E RID: 68126 RVA: 0x002E5805 File Offset: 0x002E3A05
		// (set) Token: 0x06010A1F RID: 68127 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<TriggerEventValues> Type
		{
			get
			{
				return (EnumValue<TriggerEventValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004CFF RID: 19711
		// (get) Token: 0x06010A20 RID: 68128 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010A21 RID: 68129 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004D00 RID: 19712
		// (get) Token: 0x06010A22 RID: 68130 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06010A23 RID: 68131 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "objId")]
		public UInt32Value ObjectId
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06010A25 RID: 68133 RVA: 0x002E5824 File Offset: 0x002E3A24
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<TriggerEventValues>();
			}
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

		// Token: 0x06010A26 RID: 68134 RVA: 0x002E587B File Offset: 0x002E3A7B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TriggerEventRecord>(deep);
		}

		// Token: 0x06010A27 RID: 68135 RVA: 0x002E5884 File Offset: 0x002E3A84
		// Note: this type is marked as 'beforefieldinit'.
		static TriggerEventRecord()
		{
			byte[] array = new byte[3];
			TriggerEventRecord.attributeNamespaceIds = array;
		}

		// Token: 0x04007598 RID: 30104
		private const string tagName = "triggerEvt";

		// Token: 0x04007599 RID: 30105
		private const byte tagNsId = 49;

		// Token: 0x0400759A RID: 30106
		internal const int ElementTypeIdConst = 12813;

		// Token: 0x0400759B RID: 30107
		private static string[] attributeTagNames = new string[] { "type", "time", "objId" };

		// Token: 0x0400759C RID: 30108
		private static byte[] attributeNamespaceIds;
	}
}
