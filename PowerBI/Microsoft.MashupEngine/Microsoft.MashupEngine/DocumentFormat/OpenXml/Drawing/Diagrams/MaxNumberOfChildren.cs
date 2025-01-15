using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002681 RID: 9857
	[GeneratedCode("DomGen", "2.0")]
	internal class MaxNumberOfChildren : OpenXmlLeafElement
	{
		// Token: 0x17005CA7 RID: 23719
		// (get) Token: 0x06012D59 RID: 77145 RVA: 0x002FFE27 File Offset: 0x002FE027
		public override string LocalName
		{
			get
			{
				return "chMax";
			}
		}

		// Token: 0x17005CA8 RID: 23720
		// (get) Token: 0x06012D5A RID: 77146 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005CA9 RID: 23721
		// (get) Token: 0x06012D5B RID: 77147 RVA: 0x002FFE2E File Offset: 0x002FE02E
		internal override int ElementTypeId
		{
			get
			{
				return 10672;
			}
		}

		// Token: 0x06012D5C RID: 77148 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005CAA RID: 23722
		// (get) Token: 0x06012D5D RID: 77149 RVA: 0x002FFE35 File Offset: 0x002FE035
		internal override string[] AttributeTagNames
		{
			get
			{
				return MaxNumberOfChildren.attributeTagNames;
			}
		}

		// Token: 0x17005CAB RID: 23723
		// (get) Token: 0x06012D5E RID: 77150 RVA: 0x002FFE3C File Offset: 0x002FE03C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MaxNumberOfChildren.attributeNamespaceIds;
			}
		}

		// Token: 0x17005CAC RID: 23724
		// (get) Token: 0x06012D5F RID: 77151 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06012D60 RID: 77152 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public Int32Value Val
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

		// Token: 0x06012D62 RID: 77154 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012D63 RID: 77155 RVA: 0x002FFE43 File Offset: 0x002FE043
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MaxNumberOfChildren>(deep);
		}

		// Token: 0x06012D64 RID: 77156 RVA: 0x002FFE4C File Offset: 0x002FE04C
		// Note: this type is marked as 'beforefieldinit'.
		static MaxNumberOfChildren()
		{
			byte[] array = new byte[1];
			MaxNumberOfChildren.attributeNamespaceIds = array;
		}

		// Token: 0x040081CB RID: 33227
		private const string tagName = "chMax";

		// Token: 0x040081CC RID: 33228
		private const byte tagNsId = 14;

		// Token: 0x040081CD RID: 33229
		internal const int ElementTypeIdConst = 10672;

		// Token: 0x040081CE RID: 33230
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040081CF RID: 33231
		private static byte[] attributeNamespaceIds;
	}
}
