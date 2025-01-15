using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002443 RID: 9283
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OlapSlicerCacheItemParent), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class OlapSlicerCacheSelection : OpenXmlCompositeElement
	{
		// Token: 0x17005085 RID: 20613
		// (get) Token: 0x06011210 RID: 70160 RVA: 0x002EAE4F File Offset: 0x002E904F
		public override string LocalName
		{
			get
			{
				return "selection";
			}
		}

		// Token: 0x17005086 RID: 20614
		// (get) Token: 0x06011211 RID: 70161 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17005087 RID: 20615
		// (get) Token: 0x06011212 RID: 70162 RVA: 0x002EAE56 File Offset: 0x002E9056
		internal override int ElementTypeId
		{
			get
			{
				return 13007;
			}
		}

		// Token: 0x06011213 RID: 70163 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005088 RID: 20616
		// (get) Token: 0x06011214 RID: 70164 RVA: 0x002EAE5D File Offset: 0x002E905D
		internal override string[] AttributeTagNames
		{
			get
			{
				return OlapSlicerCacheSelection.attributeTagNames;
			}
		}

		// Token: 0x17005089 RID: 20617
		// (get) Token: 0x06011215 RID: 70165 RVA: 0x002EAE64 File Offset: 0x002E9064
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OlapSlicerCacheSelection.attributeNamespaceIds;
			}
		}

		// Token: 0x1700508A RID: 20618
		// (get) Token: 0x06011216 RID: 70166 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06011217 RID: 70167 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06011218 RID: 70168 RVA: 0x00293ECF File Offset: 0x002920CF
		public OlapSlicerCacheSelection()
		{
		}

		// Token: 0x06011219 RID: 70169 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OlapSlicerCacheSelection(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601121A RID: 70170 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OlapSlicerCacheSelection(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601121B RID: 70171 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OlapSlicerCacheSelection(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601121C RID: 70172 RVA: 0x002EAA87 File Offset: 0x002E8C87
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "p" == name)
			{
				return new OlapSlicerCacheItemParent();
			}
			return null;
		}

		// Token: 0x0601121D RID: 70173 RVA: 0x002EAA13 File Offset: 0x002E8C13
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "n" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601121E RID: 70174 RVA: 0x002EAE6B File Offset: 0x002E906B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OlapSlicerCacheSelection>(deep);
		}

		// Token: 0x0601121F RID: 70175 RVA: 0x002EAE74 File Offset: 0x002E9074
		// Note: this type is marked as 'beforefieldinit'.
		static OlapSlicerCacheSelection()
		{
			byte[] array = new byte[1];
			OlapSlicerCacheSelection.attributeNamespaceIds = array;
		}

		// Token: 0x040077C8 RID: 30664
		private const string tagName = "selection";

		// Token: 0x040077C9 RID: 30665
		private const byte tagNsId = 53;

		// Token: 0x040077CA RID: 30666
		internal const int ElementTypeIdConst = 13007;

		// Token: 0x040077CB RID: 30667
		private static string[] attributeTagNames = new string[] { "n" };

		// Token: 0x040077CC RID: 30668
		private static byte[] attributeNamespaceIds;
	}
}
