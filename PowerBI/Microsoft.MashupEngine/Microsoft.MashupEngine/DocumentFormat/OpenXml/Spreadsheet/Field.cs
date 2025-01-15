using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B77 RID: 11127
	[GeneratedCode("DomGen", "2.0")]
	internal class Field : OpenXmlLeafElement
	{
		// Token: 0x170079E7 RID: 31207
		// (get) Token: 0x06016F95 RID: 94101 RVA: 0x00331497 File Offset: 0x0032F697
		public override string LocalName
		{
			get
			{
				return "field";
			}
		}

		// Token: 0x170079E8 RID: 31208
		// (get) Token: 0x06016F96 RID: 94102 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170079E9 RID: 31209
		// (get) Token: 0x06016F97 RID: 94103 RVA: 0x0033149E File Offset: 0x0032F69E
		internal override int ElementTypeId
		{
			get
			{
				return 11107;
			}
		}

		// Token: 0x06016F98 RID: 94104 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170079EA RID: 31210
		// (get) Token: 0x06016F99 RID: 94105 RVA: 0x003314A5 File Offset: 0x0032F6A5
		internal override string[] AttributeTagNames
		{
			get
			{
				return Field.attributeTagNames;
			}
		}

		// Token: 0x170079EB RID: 31211
		// (get) Token: 0x06016F9A RID: 94106 RVA: 0x003314AC File Offset: 0x0032F6AC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Field.attributeNamespaceIds;
			}
		}

		// Token: 0x170079EC RID: 31212
		// (get) Token: 0x06016F9B RID: 94107 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06016F9C RID: 94108 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "x")]
		public Int32Value Index
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

		// Token: 0x06016F9E RID: 94110 RVA: 0x0032FA8A File Offset: 0x0032DC8A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "x" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016F9F RID: 94111 RVA: 0x003314B3 File Offset: 0x0032F6B3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Field>(deep);
		}

		// Token: 0x06016FA0 RID: 94112 RVA: 0x003314BC File Offset: 0x0032F6BC
		// Note: this type is marked as 'beforefieldinit'.
		static Field()
		{
			byte[] array = new byte[1];
			Field.attributeNamespaceIds = array;
		}

		// Token: 0x04009A93 RID: 39571
		private const string tagName = "field";

		// Token: 0x04009A94 RID: 39572
		private const byte tagNsId = 22;

		// Token: 0x04009A95 RID: 39573
		internal const int ElementTypeIdConst = 11107;

		// Token: 0x04009A96 RID: 39574
		private static string[] attributeTagNames = new string[] { "x" };

		// Token: 0x04009A97 RID: 39575
		private static byte[] attributeNamespaceIds;
	}
}
