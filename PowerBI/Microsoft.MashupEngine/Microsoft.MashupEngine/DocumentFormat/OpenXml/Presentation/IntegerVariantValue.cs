using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A2C RID: 10796
	[GeneratedCode("DomGen", "2.0")]
	internal class IntegerVariantValue : OpenXmlLeafElement
	{
		// Token: 0x170070B1 RID: 28849
		// (get) Token: 0x06015AD9 RID: 88793 RVA: 0x00321F1B File Offset: 0x0032011B
		public override string LocalName
		{
			get
			{
				return "intVal";
			}
		}

		// Token: 0x170070B2 RID: 28850
		// (get) Token: 0x06015ADA RID: 88794 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070B3 RID: 28851
		// (get) Token: 0x06015ADB RID: 88795 RVA: 0x00321F22 File Offset: 0x00320122
		internal override int ElementTypeId
		{
			get
			{
				return 12218;
			}
		}

		// Token: 0x06015ADC RID: 88796 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170070B4 RID: 28852
		// (get) Token: 0x06015ADD RID: 88797 RVA: 0x00321F29 File Offset: 0x00320129
		internal override string[] AttributeTagNames
		{
			get
			{
				return IntegerVariantValue.attributeTagNames;
			}
		}

		// Token: 0x170070B5 RID: 28853
		// (get) Token: 0x06015ADE RID: 88798 RVA: 0x00321F30 File Offset: 0x00320130
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return IntegerVariantValue.attributeNamespaceIds;
			}
		}

		// Token: 0x170070B6 RID: 28854
		// (get) Token: 0x06015ADF RID: 88799 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06015AE0 RID: 88800 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06015AE2 RID: 88802 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015AE3 RID: 88803 RVA: 0x00321F37 File Offset: 0x00320137
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IntegerVariantValue>(deep);
		}

		// Token: 0x06015AE4 RID: 88804 RVA: 0x00321F40 File Offset: 0x00320140
		// Note: this type is marked as 'beforefieldinit'.
		static IntegerVariantValue()
		{
			byte[] array = new byte[1];
			IntegerVariantValue.attributeNamespaceIds = array;
		}

		// Token: 0x04009458 RID: 37976
		private const string tagName = "intVal";

		// Token: 0x04009459 RID: 37977
		private const byte tagNsId = 24;

		// Token: 0x0400945A RID: 37978
		internal const int ElementTypeIdConst = 12218;

		// Token: 0x0400945B RID: 37979
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400945C RID: 37980
		private static byte[] attributeNamespaceIds;
	}
}
