using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023CD RID: 9165
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SeekEventRecord : OpenXmlLeafElement
	{
		// Token: 0x17004D11 RID: 19729
		// (get) Token: 0x06010A49 RID: 68169 RVA: 0x002E59A3 File Offset: 0x002E3BA3
		public override string LocalName
		{
			get
			{
				return "seekEvt";
			}
		}

		// Token: 0x17004D12 RID: 19730
		// (get) Token: 0x06010A4A RID: 68170 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004D13 RID: 19731
		// (get) Token: 0x06010A4B RID: 68171 RVA: 0x002E59AA File Offset: 0x002E3BAA
		internal override int ElementTypeId
		{
			get
			{
				return 12818;
			}
		}

		// Token: 0x06010A4C RID: 68172 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004D14 RID: 19732
		// (get) Token: 0x06010A4D RID: 68173 RVA: 0x002E59B1 File Offset: 0x002E3BB1
		internal override string[] AttributeTagNames
		{
			get
			{
				return SeekEventRecord.attributeTagNames;
			}
		}

		// Token: 0x17004D15 RID: 19733
		// (get) Token: 0x06010A4E RID: 68174 RVA: 0x002E59B8 File Offset: 0x002E3BB8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SeekEventRecord.attributeNamespaceIds;
			}
		}

		// Token: 0x17004D16 RID: 19734
		// (get) Token: 0x06010A4F RID: 68175 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010A50 RID: 68176 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004D17 RID: 19735
		// (get) Token: 0x06010A51 RID: 68177 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06010A52 RID: 68178 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004D18 RID: 19736
		// (get) Token: 0x06010A53 RID: 68179 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06010A54 RID: 68180 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "seek")]
		public StringValue Seek
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06010A56 RID: 68182 RVA: 0x002E59C0 File Offset: 0x002E3BC0
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
			if (namespaceId == 0 && "seek" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010A57 RID: 68183 RVA: 0x002E5A17 File Offset: 0x002E3C17
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SeekEventRecord>(deep);
		}

		// Token: 0x06010A58 RID: 68184 RVA: 0x002E5A20 File Offset: 0x002E3C20
		// Note: this type is marked as 'beforefieldinit'.
		static SeekEventRecord()
		{
			byte[] array = new byte[3];
			SeekEventRecord.attributeNamespaceIds = array;
		}

		// Token: 0x040075AB RID: 30123
		private const string tagName = "seekEvt";

		// Token: 0x040075AC RID: 30124
		private const byte tagNsId = 49;

		// Token: 0x040075AD RID: 30125
		internal const int ElementTypeIdConst = 12818;

		// Token: 0x040075AE RID: 30126
		private static string[] attributeTagNames = new string[] { "time", "objId", "seek" };

		// Token: 0x040075AF RID: 30127
		private static byte[] attributeNamespaceIds;
	}
}
