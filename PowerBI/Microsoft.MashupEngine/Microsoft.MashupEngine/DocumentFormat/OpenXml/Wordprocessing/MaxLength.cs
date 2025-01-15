using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F42 RID: 12098
	[GeneratedCode("DomGen", "2.0")]
	internal class MaxLength : OpenXmlLeafElement
	{
		// Token: 0x17008FBD RID: 36797
		// (get) Token: 0x06019F56 RID: 106326 RVA: 0x0035A428 File Offset: 0x00358628
		public override string LocalName
		{
			get
			{
				return "maxLength";
			}
		}

		// Token: 0x17008FBE RID: 36798
		// (get) Token: 0x06019F57 RID: 106327 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FBF RID: 36799
		// (get) Token: 0x06019F58 RID: 106328 RVA: 0x0035A42F File Offset: 0x0035862F
		internal override int ElementTypeId
		{
			get
			{
				return 11745;
			}
		}

		// Token: 0x06019F59 RID: 106329 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008FC0 RID: 36800
		// (get) Token: 0x06019F5A RID: 106330 RVA: 0x0035A436 File Offset: 0x00358636
		internal override string[] AttributeTagNames
		{
			get
			{
				return MaxLength.attributeTagNames;
			}
		}

		// Token: 0x17008FC1 RID: 36801
		// (get) Token: 0x06019F5B RID: 106331 RVA: 0x0035A43D File Offset: 0x0035863D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MaxLength.attributeNamespaceIds;
			}
		}

		// Token: 0x17008FC2 RID: 36802
		// (get) Token: 0x06019F5C RID: 106332 RVA: 0x0034726F File Offset: 0x0034546F
		// (set) Token: 0x06019F5D RID: 106333 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public Int16Value Val
		{
			get
			{
				return (Int16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019F5F RID: 106335 RVA: 0x0035A444 File Offset: 0x00358644
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Int16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019F60 RID: 106336 RVA: 0x0035A466 File Offset: 0x00358666
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MaxLength>(deep);
		}

		// Token: 0x0400AB1C RID: 43804
		private const string tagName = "maxLength";

		// Token: 0x0400AB1D RID: 43805
		private const byte tagNsId = 23;

		// Token: 0x0400AB1E RID: 43806
		internal const int ElementTypeIdConst = 11745;

		// Token: 0x0400AB1F RID: 43807
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AB20 RID: 43808
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
