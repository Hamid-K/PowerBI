using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200279E RID: 10142
	[GeneratedCode("DomGen", "2.0")]
	internal class Chart : OpenXmlLeafElement
	{
		// Token: 0x17006261 RID: 25185
		// (get) Token: 0x06013A0F RID: 80399 RVA: 0x002AC9FE File Offset: 0x002AABFE
		public override string LocalName
		{
			get
			{
				return "chart";
			}
		}

		// Token: 0x17006262 RID: 25186
		// (get) Token: 0x06013A10 RID: 80400 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006263 RID: 25187
		// (get) Token: 0x06013A11 RID: 80401 RVA: 0x0030A10B File Offset: 0x0030830B
		internal override int ElementTypeId
		{
			get
			{
				return 10175;
			}
		}

		// Token: 0x06013A12 RID: 80402 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006264 RID: 25188
		// (get) Token: 0x06013A13 RID: 80403 RVA: 0x0030A112 File Offset: 0x00308312
		internal override string[] AttributeTagNames
		{
			get
			{
				return Chart.attributeTagNames;
			}
		}

		// Token: 0x17006265 RID: 25189
		// (get) Token: 0x06013A14 RID: 80404 RVA: 0x0030A119 File Offset: 0x00308319
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Chart.attributeNamespaceIds;
			}
		}

		// Token: 0x17006266 RID: 25190
		// (get) Token: 0x06013A15 RID: 80405 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013A16 RID: 80406 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "seriesIdx")]
		public Int32Value SeriesIndex
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

		// Token: 0x17006267 RID: 25191
		// (get) Token: 0x06013A17 RID: 80407 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06013A18 RID: 80408 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "categoryIdx")]
		public Int32Value CategoryIndex
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006268 RID: 25192
		// (get) Token: 0x06013A19 RID: 80409 RVA: 0x0030A120 File Offset: 0x00308320
		// (set) Token: 0x06013A1A RID: 80410 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "bldStep")]
		public EnumValue<ChartBuildStepValues> BuildStep
		{
			get
			{
				return (EnumValue<ChartBuildStepValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06013A1C RID: 80412 RVA: 0x0030A130 File Offset: 0x00308330
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "seriesIdx" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "categoryIdx" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "bldStep" == name)
			{
				return new EnumValue<ChartBuildStepValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013A1D RID: 80413 RVA: 0x0030A187 File Offset: 0x00308387
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Chart>(deep);
		}

		// Token: 0x06013A1E RID: 80414 RVA: 0x0030A190 File Offset: 0x00308390
		// Note: this type is marked as 'beforefieldinit'.
		static Chart()
		{
			byte[] array = new byte[3];
			Chart.attributeNamespaceIds = array;
		}

		// Token: 0x04008708 RID: 34568
		private const string tagName = "chart";

		// Token: 0x04008709 RID: 34569
		private const byte tagNsId = 10;

		// Token: 0x0400870A RID: 34570
		internal const int ElementTypeIdConst = 10175;

		// Token: 0x0400870B RID: 34571
		private static string[] attributeTagNames = new string[] { "seriesIdx", "categoryIdx", "bldStep" };

		// Token: 0x0400870C RID: 34572
		private static byte[] attributeNamespaceIds;
	}
}
