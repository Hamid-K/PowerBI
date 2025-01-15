using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002597 RID: 9623
	[GeneratedCode("DomGen", "2.0")]
	internal class TrendlineType : OpenXmlLeafElement
	{
		// Token: 0x170056AC RID: 22188
		// (get) Token: 0x06011FDD RID: 73693 RVA: 0x002F48F4 File Offset: 0x002F2AF4
		public override string LocalName
		{
			get
			{
				return "trendlineType";
			}
		}

		// Token: 0x170056AD RID: 22189
		// (get) Token: 0x06011FDE RID: 73694 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170056AE RID: 22190
		// (get) Token: 0x06011FDF RID: 73695 RVA: 0x002F48FB File Offset: 0x002F2AFB
		internal override int ElementTypeId
		{
			get
			{
				return 10437;
			}
		}

		// Token: 0x06011FE0 RID: 73696 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170056AF RID: 22191
		// (get) Token: 0x06011FE1 RID: 73697 RVA: 0x002F4902 File Offset: 0x002F2B02
		internal override string[] AttributeTagNames
		{
			get
			{
				return TrendlineType.attributeTagNames;
			}
		}

		// Token: 0x170056B0 RID: 22192
		// (get) Token: 0x06011FE2 RID: 73698 RVA: 0x002F4909 File Offset: 0x002F2B09
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TrendlineType.attributeNamespaceIds;
			}
		}

		// Token: 0x170056B1 RID: 22193
		// (get) Token: 0x06011FE3 RID: 73699 RVA: 0x002F4910 File Offset: 0x002F2B10
		// (set) Token: 0x06011FE4 RID: 73700 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<TrendlineValues> Val
		{
			get
			{
				return (EnumValue<TrendlineValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011FE6 RID: 73702 RVA: 0x002F491F File Offset: 0x002F2B1F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<TrendlineValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011FE7 RID: 73703 RVA: 0x002F493F File Offset: 0x002F2B3F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TrendlineType>(deep);
		}

		// Token: 0x06011FE8 RID: 73704 RVA: 0x002F4948 File Offset: 0x002F2B48
		// Note: this type is marked as 'beforefieldinit'.
		static TrendlineType()
		{
			byte[] array = new byte[1];
			TrendlineType.attributeNamespaceIds = array;
		}

		// Token: 0x04007D9D RID: 32157
		private const string tagName = "trendlineType";

		// Token: 0x04007D9E RID: 32158
		private const byte tagNsId = 11;

		// Token: 0x04007D9F RID: 32159
		internal const int ElementTypeIdConst = 10437;

		// Token: 0x04007DA0 RID: 32160
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007DA1 RID: 32161
		private static byte[] attributeNamespaceIds;
	}
}
