using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002204 RID: 8708
	[GeneratedCode("DomGen", "2.0")]
	internal class Ink : OpenXmlLeafElement
	{
		// Token: 0x17003842 RID: 14402
		// (get) Token: 0x0600DE1A RID: 56858 RVA: 0x002BDD6E File Offset: 0x002BBF6E
		public override string LocalName
		{
			get
			{
				return "ink";
			}
		}

		// Token: 0x17003843 RID: 14403
		// (get) Token: 0x0600DE1B RID: 56859 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x17003844 RID: 14404
		// (get) Token: 0x0600DE1C RID: 56860 RVA: 0x002BDD75 File Offset: 0x002BBF75
		internal override int ElementTypeId
		{
			get
			{
				return 12402;
			}
		}

		// Token: 0x0600DE1D RID: 56861 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003845 RID: 14405
		// (get) Token: 0x0600DE1E RID: 56862 RVA: 0x002BDD7C File Offset: 0x002BBF7C
		internal override string[] AttributeTagNames
		{
			get
			{
				return Ink.attributeTagNames;
			}
		}

		// Token: 0x17003846 RID: 14406
		// (get) Token: 0x0600DE1F RID: 56863 RVA: 0x002BDD83 File Offset: 0x002BBF83
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Ink.attributeNamespaceIds;
			}
		}

		// Token: 0x17003847 RID: 14407
		// (get) Token: 0x0600DE20 RID: 56864 RVA: 0x002BDD8A File Offset: 0x002BBF8A
		// (set) Token: 0x0600DE21 RID: 56865 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "i")]
		public Base64BinaryValue InkData
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003848 RID: 14408
		// (get) Token: 0x0600DE22 RID: 56866 RVA: 0x002BDACB File Offset: 0x002BBCCB
		// (set) Token: 0x0600DE23 RID: 56867 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "annotation")]
		public TrueFalseValue AnnotationFlag
		{
			get
			{
				return (TrueFalseValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0600DE25 RID: 56869 RVA: 0x002BDD99 File Offset: 0x002BBF99
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "i" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "annotation" == name)
			{
				return new TrueFalseValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DE26 RID: 56870 RVA: 0x002BDDCF File Offset: 0x002BBFCF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Ink>(deep);
		}

		// Token: 0x0600DE27 RID: 56871 RVA: 0x002BDDD8 File Offset: 0x002BBFD8
		// Note: this type is marked as 'beforefieldinit'.
		static Ink()
		{
			byte[] array = new byte[2];
			Ink.attributeNamespaceIds = array;
		}

		// Token: 0x04006D61 RID: 28001
		private const string tagName = "ink";

		// Token: 0x04006D62 RID: 28002
		private const byte tagNsId = 27;

		// Token: 0x04006D63 RID: 28003
		internal const int ElementTypeIdConst = 12402;

		// Token: 0x04006D64 RID: 28004
		private static string[] attributeTagNames = new string[] { "i", "annotation" };

		// Token: 0x04006D65 RID: 28005
		private static byte[] attributeNamespaceIds;
	}
}
