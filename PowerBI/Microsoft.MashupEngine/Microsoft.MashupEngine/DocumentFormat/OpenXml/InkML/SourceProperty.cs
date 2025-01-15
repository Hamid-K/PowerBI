using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003092 RID: 12434
	[GeneratedCode("DomGen", "2.0")]
	internal class SourceProperty : OpenXmlLeafElement
	{
		// Token: 0x170097AC RID: 38828
		// (get) Token: 0x0601B089 RID: 110729 RVA: 0x0036AEEB File Offset: 0x003690EB
		public override string LocalName
		{
			get
			{
				return "srcProperty";
			}
		}

		// Token: 0x170097AD RID: 38829
		// (get) Token: 0x0601B08A RID: 110730 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x170097AE RID: 38830
		// (get) Token: 0x0601B08B RID: 110731 RVA: 0x0036AEF2 File Offset: 0x003690F2
		internal override int ElementTypeId
		{
			get
			{
				return 12655;
			}
		}

		// Token: 0x0601B08C RID: 110732 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170097AF RID: 38831
		// (get) Token: 0x0601B08D RID: 110733 RVA: 0x0036AEF9 File Offset: 0x003690F9
		internal override string[] AttributeTagNames
		{
			get
			{
				return SourceProperty.attributeTagNames;
			}
		}

		// Token: 0x170097B0 RID: 38832
		// (get) Token: 0x0601B08E RID: 110734 RVA: 0x0036AF00 File Offset: 0x00369100
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SourceProperty.attributeNamespaceIds;
			}
		}

		// Token: 0x170097B1 RID: 38833
		// (get) Token: 0x0601B08F RID: 110735 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B090 RID: 110736 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170097B2 RID: 38834
		// (get) Token: 0x0601B091 RID: 110737 RVA: 0x0036A078 File Offset: 0x00368278
		// (set) Token: 0x0601B092 RID: 110738 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "value")]
		public DecimalValue Value
		{
			get
			{
				return (DecimalValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170097B3 RID: 38835
		// (get) Token: 0x0601B093 RID: 110739 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601B094 RID: 110740 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "units")]
		public StringValue Units
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601B096 RID: 110742 RVA: 0x0036AF08 File Offset: 0x00369108
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "value" == name)
			{
				return new DecimalValue();
			}
			if (namespaceId == 0 && "units" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B097 RID: 110743 RVA: 0x0036AF5F File Offset: 0x0036915F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SourceProperty>(deep);
		}

		// Token: 0x0601B098 RID: 110744 RVA: 0x0036AF68 File Offset: 0x00369168
		// Note: this type is marked as 'beforefieldinit'.
		static SourceProperty()
		{
			byte[] array = new byte[3];
			SourceProperty.attributeNamespaceIds = array;
		}

		// Token: 0x0400B2A7 RID: 45735
		private const string tagName = "srcProperty";

		// Token: 0x0400B2A8 RID: 45736
		private const byte tagNsId = 43;

		// Token: 0x0400B2A9 RID: 45737
		internal const int ElementTypeIdConst = 12655;

		// Token: 0x0400B2AA RID: 45738
		private static string[] attributeTagNames = new string[] { "name", "value", "units" };

		// Token: 0x0400B2AB RID: 45739
		private static byte[] attributeNamespaceIds;
	}
}
