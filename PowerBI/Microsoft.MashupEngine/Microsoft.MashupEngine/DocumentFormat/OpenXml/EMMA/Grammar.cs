using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x0200307B RID: 12411
	[GeneratedCode("DomGen", "2.0")]
	internal class Grammar : OpenXmlLeafElement
	{
		// Token: 0x1700970E RID: 38670
		// (get) Token: 0x0601AF1A RID: 110362 RVA: 0x00369CFF File Offset: 0x00367EFF
		public override string LocalName
		{
			get
			{
				return "grammar";
			}
		}

		// Token: 0x1700970F RID: 38671
		// (get) Token: 0x0601AF1B RID: 110363 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x17009710 RID: 38672
		// (get) Token: 0x0601AF1C RID: 110364 RVA: 0x00369D06 File Offset: 0x00367F06
		internal override int ElementTypeId
		{
			get
			{
				return 12680;
			}
		}

		// Token: 0x0601AF1D RID: 110365 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009711 RID: 38673
		// (get) Token: 0x0601AF1E RID: 110366 RVA: 0x00369D0D File Offset: 0x00367F0D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Grammar.attributeTagNames;
			}
		}

		// Token: 0x17009712 RID: 38674
		// (get) Token: 0x0601AF1F RID: 110367 RVA: 0x00369D14 File Offset: 0x00367F14
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Grammar.attributeNamespaceIds;
			}
		}

		// Token: 0x17009713 RID: 38675
		// (get) Token: 0x0601AF20 RID: 110368 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AF21 RID: 110369 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17009714 RID: 38676
		// (get) Token: 0x0601AF22 RID: 110370 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601AF23 RID: 110371 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x0601AF25 RID: 110373 RVA: 0x00369D1B File Offset: 0x00367F1B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AF26 RID: 110374 RVA: 0x00369D51 File Offset: 0x00367F51
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Grammar>(deep);
		}

		// Token: 0x0601AF27 RID: 110375 RVA: 0x00369D5C File Offset: 0x00367F5C
		// Note: this type is marked as 'beforefieldinit'.
		static Grammar()
		{
			byte[] array = new byte[2];
			Grammar.attributeNamespaceIds = array;
		}

		// Token: 0x0400B239 RID: 45625
		private const string tagName = "grammar";

		// Token: 0x0400B23A RID: 45626
		private const byte tagNsId = 44;

		// Token: 0x0400B23B RID: 45627
		internal const int ElementTypeIdConst = 12680;

		// Token: 0x0400B23C RID: 45628
		private static string[] attributeTagNames = new string[] { "id", "ref" };

		// Token: 0x0400B23D RID: 45629
		private static byte[] attributeNamespaceIds;
	}
}
