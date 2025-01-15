using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002344 RID: 9028
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class CompatExtension : OpenXmlLeafElement
	{
		// Token: 0x17004970 RID: 18800
		// (get) Token: 0x0601026B RID: 66155 RVA: 0x002E0433 File Offset: 0x002DE633
		public override string LocalName
		{
			get
			{
				return "compatExt";
			}
		}

		// Token: 0x17004971 RID: 18801
		// (get) Token: 0x0601026C RID: 66156 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004972 RID: 18802
		// (get) Token: 0x0601026D RID: 66157 RVA: 0x002E043A File Offset: 0x002DE63A
		internal override int ElementTypeId
		{
			get
			{
				return 12713;
			}
		}

		// Token: 0x0601026E RID: 66158 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004973 RID: 18803
		// (get) Token: 0x0601026F RID: 66159 RVA: 0x002E0441 File Offset: 0x002DE641
		internal override string[] AttributeTagNames
		{
			get
			{
				return CompatExtension.attributeTagNames;
			}
		}

		// Token: 0x17004974 RID: 18804
		// (get) Token: 0x06010270 RID: 66160 RVA: 0x002E0448 File Offset: 0x002DE648
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CompatExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x17004975 RID: 18805
		// (get) Token: 0x06010271 RID: 66161 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06010272 RID: 66162 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "spid")]
		public StringValue ShapeId
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06010274 RID: 66164 RVA: 0x002E015B File Offset: 0x002DE35B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "spid" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010275 RID: 66165 RVA: 0x002E044F File Offset: 0x002DE64F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CompatExtension>(deep);
		}

		// Token: 0x06010276 RID: 66166 RVA: 0x002E0458 File Offset: 0x002DE658
		// Note: this type is marked as 'beforefieldinit'.
		static CompatExtension()
		{
			byte[] array = new byte[1];
			CompatExtension.attributeNamespaceIds = array;
		}

		// Token: 0x0400734D RID: 29517
		private const string tagName = "compatExt";

		// Token: 0x0400734E RID: 29518
		private const byte tagNsId = 48;

		// Token: 0x0400734F RID: 29519
		internal const int ElementTypeIdConst = 12713;

		// Token: 0x04007350 RID: 29520
		private static string[] attributeTagNames = new string[] { "spid" };

		// Token: 0x04007351 RID: 29521
		private static byte[] attributeNamespaceIds;
	}
}
