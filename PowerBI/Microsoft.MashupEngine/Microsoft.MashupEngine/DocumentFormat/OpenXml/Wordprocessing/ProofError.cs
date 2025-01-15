using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EBF RID: 11967
	[GeneratedCode("DomGen", "2.0")]
	internal class ProofError : OpenXmlLeafElement
	{
		// Token: 0x17008C58 RID: 35928
		// (get) Token: 0x060197C9 RID: 104393 RVA: 0x0034C4EC File Offset: 0x0034A6EC
		public override string LocalName
		{
			get
			{
				return "proofErr";
			}
		}

		// Token: 0x17008C59 RID: 35929
		// (get) Token: 0x060197CA RID: 104394 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C5A RID: 35930
		// (get) Token: 0x060197CB RID: 104395 RVA: 0x0034C4F3 File Offset: 0x0034A6F3
		internal override int ElementTypeId
		{
			get
			{
				return 11623;
			}
		}

		// Token: 0x060197CC RID: 104396 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008C5B RID: 35931
		// (get) Token: 0x060197CD RID: 104397 RVA: 0x0034C4FA File Offset: 0x0034A6FA
		internal override string[] AttributeTagNames
		{
			get
			{
				return ProofError.attributeTagNames;
			}
		}

		// Token: 0x17008C5C RID: 35932
		// (get) Token: 0x060197CE RID: 104398 RVA: 0x0034C501 File Offset: 0x0034A701
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ProofError.attributeNamespaceIds;
			}
		}

		// Token: 0x17008C5D RID: 35933
		// (get) Token: 0x060197CF RID: 104399 RVA: 0x0034C508 File Offset: 0x0034A708
		// (set) Token: 0x060197D0 RID: 104400 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "type")]
		public EnumValue<ProofingErrorValues> Type
		{
			get
			{
				return (EnumValue<ProofingErrorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060197D2 RID: 104402 RVA: 0x0034C517 File Offset: 0x0034A717
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "type" == name)
			{
				return new EnumValue<ProofingErrorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060197D3 RID: 104403 RVA: 0x0034C539 File Offset: 0x0034A739
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ProofError>(deep);
		}

		// Token: 0x0400A90D RID: 43277
		private const string tagName = "proofErr";

		// Token: 0x0400A90E RID: 43278
		private const byte tagNsId = 23;

		// Token: 0x0400A90F RID: 43279
		internal const int ElementTypeIdConst = 11623;

		// Token: 0x0400A910 RID: 43280
		private static string[] attributeTagNames = new string[] { "type" };

		// Token: 0x0400A911 RID: 43281
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
