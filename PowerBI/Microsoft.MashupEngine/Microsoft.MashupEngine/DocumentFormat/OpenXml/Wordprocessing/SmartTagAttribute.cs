using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F5C RID: 12124
	[GeneratedCode("DomGen", "2.0")]
	internal class SmartTagAttribute : OpenXmlLeafElement
	{
		// Token: 0x1700904D RID: 36941
		// (get) Token: 0x0601A0A9 RID: 106665 RVA: 0x0035CB14 File Offset: 0x0035AD14
		public override string LocalName
		{
			get
			{
				return "attr";
			}
		}

		// Token: 0x1700904E RID: 36942
		// (get) Token: 0x0601A0AA RID: 106666 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700904F RID: 36943
		// (get) Token: 0x0601A0AB RID: 106667 RVA: 0x0035CBE0 File Offset: 0x0035ADE0
		internal override int ElementTypeId
		{
			get
			{
				return 11780;
			}
		}

		// Token: 0x0601A0AC RID: 106668 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009050 RID: 36944
		// (get) Token: 0x0601A0AD RID: 106669 RVA: 0x0035CBE7 File Offset: 0x0035ADE7
		internal override string[] AttributeTagNames
		{
			get
			{
				return SmartTagAttribute.attributeTagNames;
			}
		}

		// Token: 0x17009051 RID: 36945
		// (get) Token: 0x0601A0AE RID: 106670 RVA: 0x0035CBEE File Offset: 0x0035ADEE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SmartTagAttribute.attributeNamespaceIds;
			}
		}

		// Token: 0x17009052 RID: 36946
		// (get) Token: 0x0601A0AF RID: 106671 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A0B0 RID: 106672 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "uri")]
		public StringValue Uri
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

		// Token: 0x17009053 RID: 36947
		// (get) Token: 0x0601A0B1 RID: 106673 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601A0B2 RID: 106674 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "name")]
		public StringValue Name
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

		// Token: 0x17009054 RID: 36948
		// (get) Token: 0x0601A0B3 RID: 106675 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601A0B4 RID: 106676 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "val")]
		public StringValue Val
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

		// Token: 0x0601A0B6 RID: 106678 RVA: 0x0035CBF8 File Offset: 0x0035ADF8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "uri" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "name" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A0B7 RID: 106679 RVA: 0x0035CC55 File Offset: 0x0035AE55
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SmartTagAttribute>(deep);
		}

		// Token: 0x0400AB89 RID: 43913
		private const string tagName = "attr";

		// Token: 0x0400AB8A RID: 43914
		private const byte tagNsId = 23;

		// Token: 0x0400AB8B RID: 43915
		internal const int ElementTypeIdConst = 11780;

		// Token: 0x0400AB8C RID: 43916
		private static string[] attributeTagNames = new string[] { "uri", "name", "val" };

		// Token: 0x0400AB8D RID: 43917
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
