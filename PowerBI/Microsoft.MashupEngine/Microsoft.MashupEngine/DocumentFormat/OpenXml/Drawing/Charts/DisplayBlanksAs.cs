using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025CC RID: 9676
	[GeneratedCode("DomGen", "2.0")]
	internal class DisplayBlanksAs : OpenXmlLeafElement
	{
		// Token: 0x170057C5 RID: 22469
		// (get) Token: 0x06012257 RID: 74327 RVA: 0x002F63F4 File Offset: 0x002F45F4
		public override string LocalName
		{
			get
			{
				return "dispBlanksAs";
			}
		}

		// Token: 0x170057C6 RID: 22470
		// (get) Token: 0x06012258 RID: 74328 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170057C7 RID: 22471
		// (get) Token: 0x06012259 RID: 74329 RVA: 0x002F63FB File Offset: 0x002F45FB
		internal override int ElementTypeId
		{
			get
			{
				return 10503;
			}
		}

		// Token: 0x0601225A RID: 74330 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170057C8 RID: 22472
		// (get) Token: 0x0601225B RID: 74331 RVA: 0x002F6402 File Offset: 0x002F4602
		internal override string[] AttributeTagNames
		{
			get
			{
				return DisplayBlanksAs.attributeTagNames;
			}
		}

		// Token: 0x170057C9 RID: 22473
		// (get) Token: 0x0601225C RID: 74332 RVA: 0x002F6409 File Offset: 0x002F4609
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DisplayBlanksAs.attributeNamespaceIds;
			}
		}

		// Token: 0x170057CA RID: 22474
		// (get) Token: 0x0601225D RID: 74333 RVA: 0x002F6410 File Offset: 0x002F4610
		// (set) Token: 0x0601225E RID: 74334 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<DisplayBlanksAsValues> Val
		{
			get
			{
				return (EnumValue<DisplayBlanksAsValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012260 RID: 74336 RVA: 0x002F641F File Offset: 0x002F461F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<DisplayBlanksAsValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012261 RID: 74337 RVA: 0x002F643F File Offset: 0x002F463F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DisplayBlanksAs>(deep);
		}

		// Token: 0x06012262 RID: 74338 RVA: 0x002F6448 File Offset: 0x002F4648
		// Note: this type is marked as 'beforefieldinit'.
		static DisplayBlanksAs()
		{
			byte[] array = new byte[1];
			DisplayBlanksAs.attributeNamespaceIds = array;
		}

		// Token: 0x04007E6E RID: 32366
		private const string tagName = "dispBlanksAs";

		// Token: 0x04007E6F RID: 32367
		private const byte tagNsId = 11;

		// Token: 0x04007E70 RID: 32368
		internal const int ElementTypeIdConst = 10503;

		// Token: 0x04007E71 RID: 32369
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007E72 RID: 32370
		private static byte[] attributeNamespaceIds;
	}
}
