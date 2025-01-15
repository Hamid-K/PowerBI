using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A85 RID: 10885
	[GeneratedCode("DomGen", "2.0")]
	internal class Guide : OpenXmlLeafElement
	{
		// Token: 0x1700736C RID: 29548
		// (get) Token: 0x060160DA RID: 90330 RVA: 0x003260EF File Offset: 0x003242EF
		public override string LocalName
		{
			get
			{
				return "guide";
			}
		}

		// Token: 0x1700736D RID: 29549
		// (get) Token: 0x060160DB RID: 90331 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700736E RID: 29550
		// (get) Token: 0x060160DC RID: 90332 RVA: 0x003260F6 File Offset: 0x003242F6
		internal override int ElementTypeId
		{
			get
			{
				return 12298;
			}
		}

		// Token: 0x060160DD RID: 90333 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700736F RID: 29551
		// (get) Token: 0x060160DE RID: 90334 RVA: 0x003260FD File Offset: 0x003242FD
		internal override string[] AttributeTagNames
		{
			get
			{
				return Guide.attributeTagNames;
			}
		}

		// Token: 0x17007370 RID: 29552
		// (get) Token: 0x060160DF RID: 90335 RVA: 0x00326104 File Offset: 0x00324304
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Guide.attributeNamespaceIds;
			}
		}

		// Token: 0x17007371 RID: 29553
		// (get) Token: 0x060160E0 RID: 90336 RVA: 0x002E4355 File Offset: 0x002E2555
		// (set) Token: 0x060160E1 RID: 90337 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "orient")]
		public EnumValue<DirectionValues> Orientation
		{
			get
			{
				return (EnumValue<DirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007372 RID: 29554
		// (get) Token: 0x060160E2 RID: 90338 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060160E3 RID: 90339 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "pos")]
		public Int32Value Position
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

		// Token: 0x060160E5 RID: 90341 RVA: 0x0032610B File Offset: 0x0032430B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "orient" == name)
			{
				return new EnumValue<DirectionValues>();
			}
			if (namespaceId == 0 && "pos" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060160E6 RID: 90342 RVA: 0x00326141 File Offset: 0x00324341
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Guide>(deep);
		}

		// Token: 0x060160E7 RID: 90343 RVA: 0x0032614C File Offset: 0x0032434C
		// Note: this type is marked as 'beforefieldinit'.
		static Guide()
		{
			byte[] array = new byte[2];
			Guide.attributeNamespaceIds = array;
		}

		// Token: 0x04009600 RID: 38400
		private const string tagName = "guide";

		// Token: 0x04009601 RID: 38401
		private const byte tagNsId = 24;

		// Token: 0x04009602 RID: 38402
		internal const int ElementTypeIdConst = 12298;

		// Token: 0x04009603 RID: 38403
		private static string[] attributeTagNames = new string[] { "orient", "pos" };

		// Token: 0x04009604 RID: 38404
		private static byte[] attributeNamespaceIds;
	}
}
