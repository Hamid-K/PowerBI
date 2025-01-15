using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Charts
{
	// Token: 0x02002323 RID: 8995
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowSketchButton : OpenXmlLeafElement
	{
		// Token: 0x17004865 RID: 18533
		// (get) Token: 0x0601001A RID: 65562 RVA: 0x002DE79D File Offset: 0x002DC99D
		public override string LocalName
		{
			get
			{
				return "showSketchBtn";
			}
		}

		// Token: 0x17004866 RID: 18534
		// (get) Token: 0x0601001B RID: 65563 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		internal override byte NamespaceId
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x17004867 RID: 18535
		// (get) Token: 0x0601001C RID: 65564 RVA: 0x002DE7A4 File Offset: 0x002DC9A4
		internal override int ElementTypeId
		{
			get
			{
				return 12702;
			}
		}

		// Token: 0x0601001D RID: 65565 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004868 RID: 18536
		// (get) Token: 0x0601001E RID: 65566 RVA: 0x002DE7AB File Offset: 0x002DC9AB
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShowSketchButton.attributeTagNames;
			}
		}

		// Token: 0x17004869 RID: 18537
		// (get) Token: 0x0601001F RID: 65567 RVA: 0x002DE7B2 File Offset: 0x002DC9B2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShowSketchButton.attributeNamespaceIds;
			}
		}

		// Token: 0x1700486A RID: 18538
		// (get) Token: 0x06010020 RID: 65568 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010021 RID: 65569 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public BooleanValue Val
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06010023 RID: 65571 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010024 RID: 65572 RVA: 0x002DE7B9 File Offset: 0x002DC9B9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowSketchButton>(deep);
		}

		// Token: 0x06010025 RID: 65573 RVA: 0x002DE7C4 File Offset: 0x002DC9C4
		// Note: this type is marked as 'beforefieldinit'.
		static ShowSketchButton()
		{
			byte[] array = new byte[1];
			ShowSketchButton.attributeNamespaceIds = array;
		}

		// Token: 0x040072AB RID: 29355
		private const string tagName = "showSketchBtn";

		// Token: 0x040072AC RID: 29356
		private const byte tagNsId = 46;

		// Token: 0x040072AD RID: 29357
		internal const int ElementTypeIdConst = 12702;

		// Token: 0x040072AE RID: 29358
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040072AF RID: 29359
		private static byte[] attributeNamespaceIds;
	}
}
