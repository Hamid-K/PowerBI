using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002508 RID: 9480
	[GeneratedCode("DomGen", "2.0")]
	internal class DataLabelPosition : OpenXmlLeafElement
	{
		// Token: 0x1700542E RID: 21550
		// (get) Token: 0x06011A56 RID: 72278 RVA: 0x002F1141 File Offset: 0x002EF341
		public override string LocalName
		{
			get
			{
				return "dLblPos";
			}
		}

		// Token: 0x1700542F RID: 21551
		// (get) Token: 0x06011A57 RID: 72279 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005430 RID: 21552
		// (get) Token: 0x06011A58 RID: 72280 RVA: 0x002F1148 File Offset: 0x002EF348
		internal override int ElementTypeId
		{
			get
			{
				return 10345;
			}
		}

		// Token: 0x06011A59 RID: 72281 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005431 RID: 21553
		// (get) Token: 0x06011A5A RID: 72282 RVA: 0x002F114F File Offset: 0x002EF34F
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataLabelPosition.attributeTagNames;
			}
		}

		// Token: 0x17005432 RID: 21554
		// (get) Token: 0x06011A5B RID: 72283 RVA: 0x002F1156 File Offset: 0x002EF356
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataLabelPosition.attributeNamespaceIds;
			}
		}

		// Token: 0x17005433 RID: 21555
		// (get) Token: 0x06011A5C RID: 72284 RVA: 0x002F115D File Offset: 0x002EF35D
		// (set) Token: 0x06011A5D RID: 72285 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<DataLabelPositionValues> Val
		{
			get
			{
				return (EnumValue<DataLabelPositionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011A5F RID: 72287 RVA: 0x002F116C File Offset: 0x002EF36C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<DataLabelPositionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011A60 RID: 72288 RVA: 0x002F118C File Offset: 0x002EF38C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataLabelPosition>(deep);
		}

		// Token: 0x06011A61 RID: 72289 RVA: 0x002F1198 File Offset: 0x002EF398
		// Note: this type is marked as 'beforefieldinit'.
		static DataLabelPosition()
		{
			byte[] array = new byte[1];
			DataLabelPosition.attributeNamespaceIds = array;
		}

		// Token: 0x04007BAB RID: 31659
		private const string tagName = "dLblPos";

		// Token: 0x04007BAC RID: 31660
		private const byte tagNsId = 11;

		// Token: 0x04007BAD RID: 31661
		internal const int ElementTypeIdConst = 10345;

		// Token: 0x04007BAE RID: 31662
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007BAF RID: 31663
		private static byte[] attributeNamespaceIds;
	}
}
