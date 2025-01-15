using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B83 RID: 11139
	[GeneratedCode("DomGen", "2.0")]
	internal class MeasureGroup : OpenXmlLeafElement
	{
		// Token: 0x17007A52 RID: 31314
		// (get) Token: 0x06017074 RID: 94324 RVA: 0x00331DE3 File Offset: 0x0032FFE3
		public override string LocalName
		{
			get
			{
				return "measureGroup";
			}
		}

		// Token: 0x17007A53 RID: 31315
		// (get) Token: 0x06017075 RID: 94325 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A54 RID: 31316
		// (get) Token: 0x06017076 RID: 94326 RVA: 0x00331DEA File Offset: 0x0032FFEA
		internal override int ElementTypeId
		{
			get
			{
				return 11117;
			}
		}

		// Token: 0x06017077 RID: 94327 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007A55 RID: 31317
		// (get) Token: 0x06017078 RID: 94328 RVA: 0x00331DF1 File Offset: 0x0032FFF1
		internal override string[] AttributeTagNames
		{
			get
			{
				return MeasureGroup.attributeTagNames;
			}
		}

		// Token: 0x17007A56 RID: 31318
		// (get) Token: 0x06017079 RID: 94329 RVA: 0x00331DF8 File Offset: 0x0032FFF8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MeasureGroup.attributeNamespaceIds;
			}
		}

		// Token: 0x17007A57 RID: 31319
		// (get) Token: 0x0601707A RID: 94330 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601707B RID: 94331 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
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

		// Token: 0x17007A58 RID: 31320
		// (get) Token: 0x0601707C RID: 94332 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601707D RID: 94333 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "caption")]
		public StringValue Caption
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601707F RID: 94335 RVA: 0x00331DFF File Offset: 0x0032FFFF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "caption" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017080 RID: 94336 RVA: 0x00331E35 File Offset: 0x00330035
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MeasureGroup>(deep);
		}

		// Token: 0x06017081 RID: 94337 RVA: 0x00331E40 File Offset: 0x00330040
		// Note: this type is marked as 'beforefieldinit'.
		static MeasureGroup()
		{
			byte[] array = new byte[2];
			MeasureGroup.attributeNamespaceIds = array;
		}

		// Token: 0x04009AD0 RID: 39632
		private const string tagName = "measureGroup";

		// Token: 0x04009AD1 RID: 39633
		private const byte tagNsId = 22;

		// Token: 0x04009AD2 RID: 39634
		internal const int ElementTypeIdConst = 11117;

		// Token: 0x04009AD3 RID: 39635
		private static string[] attributeTagNames = new string[] { "name", "caption" };

		// Token: 0x04009AD4 RID: 39636
		private static byte[] attributeNamespaceIds;
	}
}
