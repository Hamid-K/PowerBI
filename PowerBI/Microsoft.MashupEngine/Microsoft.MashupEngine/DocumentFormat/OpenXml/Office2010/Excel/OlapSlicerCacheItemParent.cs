using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200243C RID: 9276
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class OlapSlicerCacheItemParent : OpenXmlLeafElement
	{
		// Token: 0x17005054 RID: 20564
		// (get) Token: 0x06011199 RID: 70041 RVA: 0x002EA9F7 File Offset: 0x002E8BF7
		public override string LocalName
		{
			get
			{
				return "p";
			}
		}

		// Token: 0x17005055 RID: 20565
		// (get) Token: 0x0601119A RID: 70042 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17005056 RID: 20566
		// (get) Token: 0x0601119B RID: 70043 RVA: 0x002EA9FE File Offset: 0x002E8BFE
		internal override int ElementTypeId
		{
			get
			{
				return 13000;
			}
		}

		// Token: 0x0601119C RID: 70044 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005057 RID: 20567
		// (get) Token: 0x0601119D RID: 70045 RVA: 0x002EAA05 File Offset: 0x002E8C05
		internal override string[] AttributeTagNames
		{
			get
			{
				return OlapSlicerCacheItemParent.attributeTagNames;
			}
		}

		// Token: 0x17005058 RID: 20568
		// (get) Token: 0x0601119E RID: 70046 RVA: 0x002EAA0C File Offset: 0x002E8C0C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OlapSlicerCacheItemParent.attributeNamespaceIds;
			}
		}

		// Token: 0x17005059 RID: 20569
		// (get) Token: 0x0601119F RID: 70047 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060111A0 RID: 70048 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "n")]
		public StringValue Name
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

		// Token: 0x060111A2 RID: 70050 RVA: 0x002EAA13 File Offset: 0x002E8C13
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "n" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060111A3 RID: 70051 RVA: 0x002EAA33 File Offset: 0x002E8C33
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OlapSlicerCacheItemParent>(deep);
		}

		// Token: 0x060111A4 RID: 70052 RVA: 0x002EAA3C File Offset: 0x002E8C3C
		// Note: this type is marked as 'beforefieldinit'.
		static OlapSlicerCacheItemParent()
		{
			byte[] array = new byte[1];
			OlapSlicerCacheItemParent.attributeNamespaceIds = array;
		}

		// Token: 0x040077A5 RID: 30629
		private const string tagName = "p";

		// Token: 0x040077A6 RID: 30630
		private const byte tagNsId = 53;

		// Token: 0x040077A7 RID: 30631
		internal const int ElementTypeIdConst = 13000;

		// Token: 0x040077A8 RID: 30632
		private static string[] attributeTagNames = new string[] { "n" };

		// Token: 0x040077A9 RID: 30633
		private static byte[] attributeNamespaceIds;
	}
}
