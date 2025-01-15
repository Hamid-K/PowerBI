using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002429 RID: 9257
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotEditValue : OpenXmlLeafTextElement
	{
		// Token: 0x17004FAA RID: 20394
		// (get) Token: 0x06011024 RID: 69668 RVA: 0x002E9A3E File Offset: 0x002E7C3E
		public override string LocalName
		{
			get
			{
				return "editValue";
			}
		}

		// Token: 0x17004FAB RID: 20395
		// (get) Token: 0x06011025 RID: 69669 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004FAC RID: 20396
		// (get) Token: 0x06011026 RID: 69670 RVA: 0x002E9A45 File Offset: 0x002E7C45
		internal override int ElementTypeId
		{
			get
			{
				return 12981;
			}
		}

		// Token: 0x06011027 RID: 69671 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004FAD RID: 20397
		// (get) Token: 0x06011028 RID: 69672 RVA: 0x002E9A4C File Offset: 0x002E7C4C
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotEditValue.attributeTagNames;
			}
		}

		// Token: 0x17004FAE RID: 20398
		// (get) Token: 0x06011029 RID: 69673 RVA: 0x002E9A53 File Offset: 0x002E7C53
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotEditValue.attributeNamespaceIds;
			}
		}

		// Token: 0x17004FAF RID: 20399
		// (get) Token: 0x0601102A RID: 69674 RVA: 0x002E9A5A File Offset: 0x002E7C5A
		// (set) Token: 0x0601102B RID: 69675 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "valueType")]
		public EnumValue<PivotEditValueTypeValues> ValueType
		{
			get
			{
				return (EnumValue<PivotEditValueTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601102C RID: 69676 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PivotEditValue()
		{
		}

		// Token: 0x0601102D RID: 69677 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PivotEditValue(string text)
			: base(text)
		{
		}

		// Token: 0x0601102E RID: 69678 RVA: 0x002E9A6C File Offset: 0x002E7C6C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601102F RID: 69679 RVA: 0x002E9A87 File Offset: 0x002E7C87
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "valueType" == name)
			{
				return new EnumValue<PivotEditValueTypeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011030 RID: 69680 RVA: 0x002E9AA7 File Offset: 0x002E7CA7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotEditValue>(deep);
		}

		// Token: 0x06011031 RID: 69681 RVA: 0x002E9AB0 File Offset: 0x002E7CB0
		// Note: this type is marked as 'beforefieldinit'.
		static PivotEditValue()
		{
			byte[] array = new byte[1];
			PivotEditValue.attributeNamespaceIds = array;
		}

		// Token: 0x04007740 RID: 30528
		private const string tagName = "editValue";

		// Token: 0x04007741 RID: 30529
		private const byte tagNsId = 53;

		// Token: 0x04007742 RID: 30530
		internal const int ElementTypeIdConst = 12981;

		// Token: 0x04007743 RID: 30531
		private static string[] attributeTagNames = new string[] { "valueType" };

		// Token: 0x04007744 RID: 30532
		private static byte[] attributeNamespaceIds;
	}
}
