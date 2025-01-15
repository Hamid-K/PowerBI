using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002463 RID: 9315
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class AcceleratorKeymapType : OpenXmlLeafElement
	{
		// Token: 0x170050BD RID: 20669
		// (get) Token: 0x060112A0 RID: 70304 RVA: 0x002EB2FE File Offset: 0x002E94FE
		internal override string[] AttributeTagNames
		{
			get
			{
				return AcceleratorKeymapType.attributeTagNames;
			}
		}

		// Token: 0x170050BE RID: 20670
		// (get) Token: 0x060112A1 RID: 70305 RVA: 0x002EB305 File Offset: 0x002E9505
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AcceleratorKeymapType.attributeNamespaceIds;
			}
		}

		// Token: 0x170050BF RID: 20671
		// (get) Token: 0x060112A2 RID: 70306 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060112A3 RID: 70307 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(33, "acdName")]
		public StringValue AcceleratorName
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

		// Token: 0x060112A4 RID: 70308 RVA: 0x002EB30C File Offset: 0x002E950C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "acdName" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x04007874 RID: 30836
		private static string[] attributeTagNames = new string[] { "acdName" };

		// Token: 0x04007875 RID: 30837
		private static byte[] attributeNamespaceIds = new byte[] { 33 };
	}
}
