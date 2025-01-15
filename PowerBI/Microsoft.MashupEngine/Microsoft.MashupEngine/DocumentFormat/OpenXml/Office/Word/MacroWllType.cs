using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002460 RID: 9312
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class MacroWllType : OpenXmlLeafElement
	{
		// Token: 0x170050B4 RID: 20660
		// (get) Token: 0x0601128D RID: 70285 RVA: 0x002EB264 File Offset: 0x002E9464
		internal override string[] AttributeTagNames
		{
			get
			{
				return MacroWllType.attributeTagNames;
			}
		}

		// Token: 0x170050B5 RID: 20661
		// (get) Token: 0x0601128E RID: 70286 RVA: 0x002EB26B File Offset: 0x002E946B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MacroWllType.attributeNamespaceIds;
			}
		}

		// Token: 0x170050B6 RID: 20662
		// (get) Token: 0x0601128F RID: 70287 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06011290 RID: 70288 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(33, "macroName")]
		public StringValue MacroName
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

		// Token: 0x06011291 RID: 70289 RVA: 0x002EB272 File Offset: 0x002E9472
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "macroName" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400786C RID: 30828
		private static string[] attributeTagNames = new string[] { "macroName" };

		// Token: 0x0400786D RID: 30829
		private static byte[] attributeNamespaceIds = new byte[] { 33 };
	}
}
