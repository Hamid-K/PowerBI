using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002418 RID: 9240
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SetLevel : OpenXmlLeafElement
	{
		// Token: 0x17004F19 RID: 20249
		// (get) Token: 0x06010ED5 RID: 69333 RVA: 0x002E8B2B File Offset: 0x002E6D2B
		public override string LocalName
		{
			get
			{
				return "setLevel";
			}
		}

		// Token: 0x17004F1A RID: 20250
		// (get) Token: 0x06010ED6 RID: 69334 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F1B RID: 20251
		// (get) Token: 0x06010ED7 RID: 69335 RVA: 0x002E8B32 File Offset: 0x002E6D32
		internal override int ElementTypeId
		{
			get
			{
				return 12958;
			}
		}

		// Token: 0x06010ED8 RID: 69336 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004F1C RID: 20252
		// (get) Token: 0x06010ED9 RID: 69337 RVA: 0x002E8B39 File Offset: 0x002E6D39
		internal override string[] AttributeTagNames
		{
			get
			{
				return SetLevel.attributeTagNames;
			}
		}

		// Token: 0x17004F1D RID: 20253
		// (get) Token: 0x06010EDA RID: 69338 RVA: 0x002E8B40 File Offset: 0x002E6D40
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SetLevel.attributeNamespaceIds;
			}
		}

		// Token: 0x17004F1E RID: 20254
		// (get) Token: 0x06010EDB RID: 69339 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010EDC RID: 69340 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "hierarchy")]
		public Int32Value Hierarchy
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

		// Token: 0x06010EDE RID: 69342 RVA: 0x002E8B47 File Offset: 0x002E6D47
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "hierarchy" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010EDF RID: 69343 RVA: 0x002E8B67 File Offset: 0x002E6D67
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SetLevel>(deep);
		}

		// Token: 0x06010EE0 RID: 69344 RVA: 0x002E8B70 File Offset: 0x002E6D70
		// Note: this type is marked as 'beforefieldinit'.
		static SetLevel()
		{
			byte[] array = new byte[1];
			SetLevel.attributeNamespaceIds = array;
		}

		// Token: 0x040076ED RID: 30445
		private const string tagName = "setLevel";

		// Token: 0x040076EE RID: 30446
		private const byte tagNsId = 53;

		// Token: 0x040076EF RID: 30447
		internal const int ElementTypeIdConst = 12958;

		// Token: 0x040076F0 RID: 30448
		private static string[] attributeTagNames = new string[] { "hierarchy" };

		// Token: 0x040076F1 RID: 30449
		private static byte[] attributeNamespaceIds;
	}
}
