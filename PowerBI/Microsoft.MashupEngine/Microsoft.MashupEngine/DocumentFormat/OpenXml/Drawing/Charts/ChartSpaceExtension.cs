using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing.Charts;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025FB RID: 9723
	[ChildElementInfo(typeof(PivotOptions), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SketchOptions), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ChartSpaceExtension : OpenXmlCompositeElement
	{
		// Token: 0x17005970 RID: 22896
		// (get) Token: 0x060125F1 RID: 75249 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x17005971 RID: 22897
		// (get) Token: 0x060125F2 RID: 75250 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005972 RID: 22898
		// (get) Token: 0x060125F3 RID: 75251 RVA: 0x002FA46C File Offset: 0x002F866C
		internal override int ElementTypeId
		{
			get
			{
				return 10568;
			}
		}

		// Token: 0x060125F4 RID: 75252 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005973 RID: 22899
		// (get) Token: 0x060125F5 RID: 75253 RVA: 0x002FA473 File Offset: 0x002F8673
		internal override string[] AttributeTagNames
		{
			get
			{
				return ChartSpaceExtension.attributeTagNames;
			}
		}

		// Token: 0x17005974 RID: 22900
		// (get) Token: 0x060125F6 RID: 75254 RVA: 0x002FA47A File Offset: 0x002F867A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ChartSpaceExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x17005975 RID: 22901
		// (get) Token: 0x060125F7 RID: 75255 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060125F8 RID: 75256 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uri")]
		public StringValue Uri
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

		// Token: 0x060125F9 RID: 75257 RVA: 0x00293ECF File Offset: 0x002920CF
		public ChartSpaceExtension()
		{
		}

		// Token: 0x060125FA RID: 75258 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ChartSpaceExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060125FB RID: 75259 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ChartSpaceExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060125FC RID: 75260 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ChartSpaceExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060125FD RID: 75261 RVA: 0x002FA481 File Offset: 0x002F8681
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (46 == namespaceId && "pivotOptions" == name)
			{
				return new PivotOptions();
			}
			if (46 == namespaceId && "sketchOptions" == name)
			{
				return new SketchOptions();
			}
			return null;
		}

		// Token: 0x060125FE RID: 75262 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060125FF RID: 75263 RVA: 0x002FA4B4 File Offset: 0x002F86B4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartSpaceExtension>(deep);
		}

		// Token: 0x06012600 RID: 75264 RVA: 0x002FA4C0 File Offset: 0x002F86C0
		// Note: this type is marked as 'beforefieldinit'.
		static ChartSpaceExtension()
		{
			byte[] array = new byte[1];
			ChartSpaceExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04007F4B RID: 32587
		private const string tagName = "ext";

		// Token: 0x04007F4C RID: 32588
		private const byte tagNsId = 11;

		// Token: 0x04007F4D RID: 32589
		internal const int ElementTypeIdConst = 10568;

		// Token: 0x04007F4E RID: 32590
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04007F4F RID: 32591
		private static byte[] attributeNamespaceIds;
	}
}
