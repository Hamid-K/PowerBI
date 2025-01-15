using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002833 RID: 10291
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class RightToLeft : OpenXmlLeafElement
	{
		// Token: 0x17006613 RID: 26131
		// (get) Token: 0x060142B3 RID: 82611 RVA: 0x0030FEE7 File Offset: 0x0030E0E7
		public override string LocalName
		{
			get
			{
				return "rtl";
			}
		}

		// Token: 0x17006614 RID: 26132
		// (get) Token: 0x060142B4 RID: 82612 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006615 RID: 26133
		// (get) Token: 0x060142B5 RID: 82613 RVA: 0x0030FEEE File Offset: 0x0030E0EE
		internal override int ElementTypeId
		{
			get
			{
				return 10327;
			}
		}

		// Token: 0x060142B6 RID: 82614 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17006616 RID: 26134
		// (get) Token: 0x060142B7 RID: 82615 RVA: 0x0030FEF5 File Offset: 0x0030E0F5
		internal override string[] AttributeTagNames
		{
			get
			{
				return RightToLeft.attributeTagNames;
			}
		}

		// Token: 0x17006617 RID: 26135
		// (get) Token: 0x060142B8 RID: 82616 RVA: 0x0030FEFC File Offset: 0x0030E0FC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RightToLeft.attributeNamespaceIds;
			}
		}

		// Token: 0x17006618 RID: 26136
		// (get) Token: 0x060142B9 RID: 82617 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060142BA RID: 82618 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060142BC RID: 82620 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060142BD RID: 82621 RVA: 0x0030FF03 File Offset: 0x0030E103
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RightToLeft>(deep);
		}

		// Token: 0x060142BE RID: 82622 RVA: 0x0030FF0C File Offset: 0x0030E10C
		// Note: this type is marked as 'beforefieldinit'.
		static RightToLeft()
		{
			byte[] array = new byte[1];
			RightToLeft.attributeNamespaceIds = array;
		}

		// Token: 0x04008959 RID: 35161
		private const string tagName = "rtl";

		// Token: 0x0400895A RID: 35162
		private const byte tagNsId = 10;

		// Token: 0x0400895B RID: 35163
		internal const int ElementTypeIdConst = 10327;

		// Token: 0x0400895C RID: 35164
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400895D RID: 35165
		private static byte[] attributeNamespaceIds;
	}
}
