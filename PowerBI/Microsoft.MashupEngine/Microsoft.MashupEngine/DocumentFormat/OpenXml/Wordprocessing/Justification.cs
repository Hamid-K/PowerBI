using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E2B RID: 11819
	[GeneratedCode("DomGen", "2.0")]
	internal class Justification : OpenXmlLeafElement
	{
		// Token: 0x17008961 RID: 35169
		// (get) Token: 0x0601918A RID: 102794 RVA: 0x0031DF1C File Offset: 0x0031C11C
		public override string LocalName
		{
			get
			{
				return "jc";
			}
		}

		// Token: 0x17008962 RID: 35170
		// (get) Token: 0x0601918B RID: 102795 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008963 RID: 35171
		// (get) Token: 0x0601918C RID: 102796 RVA: 0x0034655D File Offset: 0x0034475D
		internal override int ElementTypeId
		{
			get
			{
				return 11518;
			}
		}

		// Token: 0x0601918D RID: 102797 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008964 RID: 35172
		// (get) Token: 0x0601918E RID: 102798 RVA: 0x00346564 File Offset: 0x00344764
		internal override string[] AttributeTagNames
		{
			get
			{
				return Justification.attributeTagNames;
			}
		}

		// Token: 0x17008965 RID: 35173
		// (get) Token: 0x0601918F RID: 102799 RVA: 0x0034656B File Offset: 0x0034476B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Justification.attributeNamespaceIds;
			}
		}

		// Token: 0x17008966 RID: 35174
		// (get) Token: 0x06019190 RID: 102800 RVA: 0x00346572 File Offset: 0x00344772
		// (set) Token: 0x06019191 RID: 102801 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<JustificationValues> Val
		{
			get
			{
				return (EnumValue<JustificationValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019193 RID: 102803 RVA: 0x00346581 File Offset: 0x00344781
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<JustificationValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019194 RID: 102804 RVA: 0x003465A3 File Offset: 0x003447A3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Justification>(deep);
		}

		// Token: 0x0400A6F8 RID: 42744
		private const string tagName = "jc";

		// Token: 0x0400A6F9 RID: 42745
		private const byte tagNsId = 23;

		// Token: 0x0400A6FA RID: 42746
		internal const int ElementTypeIdConst = 11518;

		// Token: 0x0400A6FB RID: 42747
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A6FC RID: 42748
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
