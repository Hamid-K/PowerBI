using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025C2 RID: 9666
	[GeneratedCode("DomGen", "2.0")]
	internal class LegendPosition : OpenXmlLeafElement
	{
		// Token: 0x1700578A RID: 22410
		// (get) Token: 0x060121C9 RID: 74185 RVA: 0x002F5A60 File Offset: 0x002F3C60
		public override string LocalName
		{
			get
			{
				return "legendPos";
			}
		}

		// Token: 0x1700578B RID: 22411
		// (get) Token: 0x060121CA RID: 74186 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700578C RID: 22412
		// (get) Token: 0x060121CB RID: 74187 RVA: 0x002F5A67 File Offset: 0x002F3C67
		internal override int ElementTypeId
		{
			get
			{
				return 10492;
			}
		}

		// Token: 0x060121CC RID: 74188 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700578D RID: 22413
		// (get) Token: 0x060121CD RID: 74189 RVA: 0x002F5A6E File Offset: 0x002F3C6E
		internal override string[] AttributeTagNames
		{
			get
			{
				return LegendPosition.attributeTagNames;
			}
		}

		// Token: 0x1700578E RID: 22414
		// (get) Token: 0x060121CE RID: 74190 RVA: 0x002F5A75 File Offset: 0x002F3C75
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LegendPosition.attributeNamespaceIds;
			}
		}

		// Token: 0x1700578F RID: 22415
		// (get) Token: 0x060121CF RID: 74191 RVA: 0x002F5A7C File Offset: 0x002F3C7C
		// (set) Token: 0x060121D0 RID: 74192 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<LegendPositionValues> Val
		{
			get
			{
				return (EnumValue<LegendPositionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060121D2 RID: 74194 RVA: 0x002F5A8B File Offset: 0x002F3C8B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<LegendPositionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060121D3 RID: 74195 RVA: 0x002F5AAB File Offset: 0x002F3CAB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LegendPosition>(deep);
		}

		// Token: 0x060121D4 RID: 74196 RVA: 0x002F5AB4 File Offset: 0x002F3CB4
		// Note: this type is marked as 'beforefieldinit'.
		static LegendPosition()
		{
			byte[] array = new byte[1];
			LegendPosition.attributeNamespaceIds = array;
		}

		// Token: 0x04007E47 RID: 32327
		private const string tagName = "legendPos";

		// Token: 0x04007E48 RID: 32328
		private const byte tagNsId = 11;

		// Token: 0x04007E49 RID: 32329
		internal const int ElementTypeIdConst = 10492;

		// Token: 0x04007E4A RID: 32330
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007E4B RID: 32331
		private static byte[] attributeNamespaceIds;
	}
}
