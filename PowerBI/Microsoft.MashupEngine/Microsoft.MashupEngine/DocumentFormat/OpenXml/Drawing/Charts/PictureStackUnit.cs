using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025B2 RID: 9650
	[GeneratedCode("DomGen", "2.0")]
	internal class PictureStackUnit : OpenXmlLeafElement
	{
		// Token: 0x17005736 RID: 22326
		// (get) Token: 0x06012117 RID: 74007 RVA: 0x002F5383 File Offset: 0x002F3583
		public override string LocalName
		{
			get
			{
				return "pictureStackUnit";
			}
		}

		// Token: 0x17005737 RID: 22327
		// (get) Token: 0x06012118 RID: 74008 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005738 RID: 22328
		// (get) Token: 0x06012119 RID: 74009 RVA: 0x002F538A File Offset: 0x002F358A
		internal override int ElementTypeId
		{
			get
			{
				return 10473;
			}
		}

		// Token: 0x0601211A RID: 74010 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005739 RID: 22329
		// (get) Token: 0x0601211B RID: 74011 RVA: 0x002F5391 File Offset: 0x002F3591
		internal override string[] AttributeTagNames
		{
			get
			{
				return PictureStackUnit.attributeTagNames;
			}
		}

		// Token: 0x1700573A RID: 22330
		// (get) Token: 0x0601211C RID: 74012 RVA: 0x002F5398 File Offset: 0x002F3598
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PictureStackUnit.attributeNamespaceIds;
			}
		}

		// Token: 0x1700573B RID: 22331
		// (get) Token: 0x0601211D RID: 74013 RVA: 0x002E7DC5 File Offset: 0x002E5FC5
		// (set) Token: 0x0601211E RID: 74014 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public DoubleValue Val
		{
			get
			{
				return (DoubleValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012120 RID: 74016 RVA: 0x002F2E7D File Offset: 0x002F107D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012121 RID: 74017 RVA: 0x002F539F File Offset: 0x002F359F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PictureStackUnit>(deep);
		}

		// Token: 0x06012122 RID: 74018 RVA: 0x002F53A8 File Offset: 0x002F35A8
		// Note: this type is marked as 'beforefieldinit'.
		static PictureStackUnit()
		{
			byte[] array = new byte[1];
			PictureStackUnit.attributeNamespaceIds = array;
		}

		// Token: 0x04007E07 RID: 32263
		private const string tagName = "pictureStackUnit";

		// Token: 0x04007E08 RID: 32264
		private const byte tagNsId = 11;

		// Token: 0x04007E09 RID: 32265
		internal const int ElementTypeIdConst = 10473;

		// Token: 0x04007E0A RID: 32266
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007E0B RID: 32267
		private static byte[] attributeNamespaceIds;
	}
}
