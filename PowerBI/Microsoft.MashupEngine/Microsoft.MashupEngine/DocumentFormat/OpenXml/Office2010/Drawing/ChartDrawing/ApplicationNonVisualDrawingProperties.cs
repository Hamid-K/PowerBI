using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing
{
	// Token: 0x0200233D RID: 9021
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ApplicationNonVisualDrawingProperties : OpenXmlLeafElement
	{
		// Token: 0x17004936 RID: 18742
		// (get) Token: 0x060101F1 RID: 66033 RVA: 0x002DFF99 File Offset: 0x002DE199
		public override string LocalName
		{
			get
			{
				return "nvPr";
			}
		}

		// Token: 0x17004937 RID: 18743
		// (get) Token: 0x060101F2 RID: 66034 RVA: 0x002DF9A4 File Offset: 0x002DDBA4
		internal override byte NamespaceId
		{
			get
			{
				return 47;
			}
		}

		// Token: 0x17004938 RID: 18744
		// (get) Token: 0x060101F3 RID: 66035 RVA: 0x002DFFA0 File Offset: 0x002DE1A0
		internal override int ElementTypeId
		{
			get
			{
				return 12709;
			}
		}

		// Token: 0x060101F4 RID: 66036 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004939 RID: 18745
		// (get) Token: 0x060101F5 RID: 66037 RVA: 0x002DFFA7 File Offset: 0x002DE1A7
		internal override string[] AttributeTagNames
		{
			get
			{
				return ApplicationNonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x1700493A RID: 18746
		// (get) Token: 0x060101F6 RID: 66038 RVA: 0x002DFFAE File Offset: 0x002DE1AE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ApplicationNonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700493B RID: 18747
		// (get) Token: 0x060101F7 RID: 66039 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060101F8 RID: 66040 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "macro")]
		public StringValue Macro
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

		// Token: 0x1700493C RID: 18748
		// (get) Token: 0x060101F9 RID: 66041 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060101FA RID: 66042 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fPublished")]
		public BooleanValue Published
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060101FC RID: 66044 RVA: 0x002DFFB5 File Offset: 0x002DE1B5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "macro" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fPublished" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060101FD RID: 66045 RVA: 0x002DFFEB File Offset: 0x002DE1EB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ApplicationNonVisualDrawingProperties>(deep);
		}

		// Token: 0x060101FE RID: 66046 RVA: 0x002DFFF4 File Offset: 0x002DE1F4
		// Note: this type is marked as 'beforefieldinit'.
		static ApplicationNonVisualDrawingProperties()
		{
			byte[] array = new byte[2];
			ApplicationNonVisualDrawingProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04007328 RID: 29480
		private const string tagName = "nvPr";

		// Token: 0x04007329 RID: 29481
		private const byte tagNsId = 47;

		// Token: 0x0400732A RID: 29482
		internal const int ElementTypeIdConst = 12709;

		// Token: 0x0400732B RID: 29483
		private static string[] attributeTagNames = new string[] { "macro", "fPublished" };

		// Token: 0x0400732C RID: 29484
		private static byte[] attributeNamespaceIds;
	}
}
