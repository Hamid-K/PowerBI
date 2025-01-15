using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002376 RID: 9078
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SharpenSoften : OpenXmlLeafElement
	{
		// Token: 0x17004AEC RID: 19180
		// (get) Token: 0x0601058D RID: 66957 RVA: 0x002E255B File Offset: 0x002E075B
		public override string LocalName
		{
			get
			{
				return "sharpenSoften";
			}
		}

		// Token: 0x17004AED RID: 19181
		// (get) Token: 0x0601058E RID: 66958 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004AEE RID: 19182
		// (get) Token: 0x0601058F RID: 66959 RVA: 0x002E2562 File Offset: 0x002E0762
		internal override int ElementTypeId
		{
			get
			{
				return 12761;
			}
		}

		// Token: 0x06010590 RID: 66960 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004AEF RID: 19183
		// (get) Token: 0x06010591 RID: 66961 RVA: 0x002E2569 File Offset: 0x002E0769
		internal override string[] AttributeTagNames
		{
			get
			{
				return SharpenSoften.attributeTagNames;
			}
		}

		// Token: 0x17004AF0 RID: 19184
		// (get) Token: 0x06010592 RID: 66962 RVA: 0x002E2570 File Offset: 0x002E0770
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SharpenSoften.attributeNamespaceIds;
			}
		}

		// Token: 0x17004AF1 RID: 19185
		// (get) Token: 0x06010593 RID: 66963 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010594 RID: 66964 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "amount")]
		public Int32Value Amount
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06010596 RID: 66966 RVA: 0x002E2577 File Offset: 0x002E0777
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "amount" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010597 RID: 66967 RVA: 0x002E2597 File Offset: 0x002E0797
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SharpenSoften>(deep);
		}

		// Token: 0x06010598 RID: 66968 RVA: 0x002E25A0 File Offset: 0x002E07A0
		// Note: this type is marked as 'beforefieldinit'.
		static SharpenSoften()
		{
			byte[] array = new byte[1];
			SharpenSoften.attributeNamespaceIds = array;
		}

		// Token: 0x04007441 RID: 29761
		private const string tagName = "sharpenSoften";

		// Token: 0x04007442 RID: 29762
		private const byte tagNsId = 48;

		// Token: 0x04007443 RID: 29763
		internal const int ElementTypeIdConst = 12761;

		// Token: 0x04007444 RID: 29764
		private static string[] attributeTagNames = new string[] { "amount" };

		// Token: 0x04007445 RID: 29765
		private static byte[] attributeNamespaceIds;
	}
}
