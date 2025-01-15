using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025B3 RID: 9651
	[GeneratedCode("DomGen", "2.0")]
	internal class BuiltInUnit : OpenXmlLeafElement
	{
		// Token: 0x1700573C RID: 22332
		// (get) Token: 0x06012123 RID: 74019 RVA: 0x002F53D7 File Offset: 0x002F35D7
		public override string LocalName
		{
			get
			{
				return "builtInUnit";
			}
		}

		// Token: 0x1700573D RID: 22333
		// (get) Token: 0x06012124 RID: 74020 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700573E RID: 22334
		// (get) Token: 0x06012125 RID: 74021 RVA: 0x002F53DE File Offset: 0x002F35DE
		internal override int ElementTypeId
		{
			get
			{
				return 10475;
			}
		}

		// Token: 0x06012126 RID: 74022 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700573F RID: 22335
		// (get) Token: 0x06012127 RID: 74023 RVA: 0x002F53E5 File Offset: 0x002F35E5
		internal override string[] AttributeTagNames
		{
			get
			{
				return BuiltInUnit.attributeTagNames;
			}
		}

		// Token: 0x17005740 RID: 22336
		// (get) Token: 0x06012128 RID: 74024 RVA: 0x002F53EC File Offset: 0x002F35EC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BuiltInUnit.attributeNamespaceIds;
			}
		}

		// Token: 0x17005741 RID: 22337
		// (get) Token: 0x06012129 RID: 74025 RVA: 0x002F53F3 File Offset: 0x002F35F3
		// (set) Token: 0x0601212A RID: 74026 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<BuiltInUnitValues> Val
		{
			get
			{
				return (EnumValue<BuiltInUnitValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601212C RID: 74028 RVA: 0x002F5402 File Offset: 0x002F3602
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<BuiltInUnitValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601212D RID: 74029 RVA: 0x002F5422 File Offset: 0x002F3622
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BuiltInUnit>(deep);
		}

		// Token: 0x0601212E RID: 74030 RVA: 0x002F542C File Offset: 0x002F362C
		// Note: this type is marked as 'beforefieldinit'.
		static BuiltInUnit()
		{
			byte[] array = new byte[1];
			BuiltInUnit.attributeNamespaceIds = array;
		}

		// Token: 0x04007E0C RID: 32268
		private const string tagName = "builtInUnit";

		// Token: 0x04007E0D RID: 32269
		private const byte tagNsId = 11;

		// Token: 0x04007E0E RID: 32270
		internal const int ElementTypeIdConst = 10475;

		// Token: 0x04007E0F RID: 32271
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007E10 RID: 32272
		private static byte[] attributeNamespaceIds;
	}
}
