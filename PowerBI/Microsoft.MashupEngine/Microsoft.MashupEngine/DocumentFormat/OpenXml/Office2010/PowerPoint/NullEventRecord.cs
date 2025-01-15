using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023CE RID: 9166
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NullEventRecord : OpenXmlLeafElement
	{
		// Token: 0x17004D19 RID: 19737
		// (get) Token: 0x06010A59 RID: 68185 RVA: 0x002E5A5F File Offset: 0x002E3C5F
		public override string LocalName
		{
			get
			{
				return "nullEvt";
			}
		}

		// Token: 0x17004D1A RID: 19738
		// (get) Token: 0x06010A5A RID: 68186 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004D1B RID: 19739
		// (get) Token: 0x06010A5B RID: 68187 RVA: 0x002E5A66 File Offset: 0x002E3C66
		internal override int ElementTypeId
		{
			get
			{
				return 12819;
			}
		}

		// Token: 0x06010A5C RID: 68188 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004D1C RID: 19740
		// (get) Token: 0x06010A5D RID: 68189 RVA: 0x002E5A6D File Offset: 0x002E3C6D
		internal override string[] AttributeTagNames
		{
			get
			{
				return NullEventRecord.attributeTagNames;
			}
		}

		// Token: 0x17004D1D RID: 19741
		// (get) Token: 0x06010A5E RID: 68190 RVA: 0x002E5A74 File Offset: 0x002E3C74
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NullEventRecord.attributeNamespaceIds;
			}
		}

		// Token: 0x17004D1E RID: 19742
		// (get) Token: 0x06010A5F RID: 68191 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010A60 RID: 68192 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004D1F RID: 19743
		// (get) Token: 0x06010A61 RID: 68193 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06010A62 RID: 68194 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06010A64 RID: 68196 RVA: 0x002E58D1 File Offset: 0x002E3AD1
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

		// Token: 0x06010A65 RID: 68197 RVA: 0x002E5A7B File Offset: 0x002E3C7B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NullEventRecord>(deep);
		}

		// Token: 0x06010A66 RID: 68198 RVA: 0x002E5A84 File Offset: 0x002E3C84
		// Note: this type is marked as 'beforefieldinit'.
		static NullEventRecord()
		{
			byte[] array = new byte[2];
			NullEventRecord.attributeNamespaceIds = array;
		}

		// Token: 0x040075B0 RID: 30128
		private const string tagName = "nullEvt";

		// Token: 0x040075B1 RID: 30129
		private const byte tagNsId = 49;

		// Token: 0x040075B2 RID: 30130
		internal const int ElementTypeIdConst = 12819;

		// Token: 0x040075B3 RID: 30131
		private static string[] attributeTagNames = new string[] { "time", "objId" };

		// Token: 0x040075B4 RID: 30132
		private static byte[] attributeNamespaceIds;
	}
}
