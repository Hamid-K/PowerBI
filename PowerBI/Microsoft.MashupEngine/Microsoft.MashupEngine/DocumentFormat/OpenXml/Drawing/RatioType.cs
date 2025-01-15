using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002788 RID: 10120
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class RatioType : OpenXmlLeafElement
	{
		// Token: 0x170061CE RID: 25038
		// (get) Token: 0x060138D2 RID: 80082 RVA: 0x00308343 File Offset: 0x00306543
		internal override string[] AttributeTagNames
		{
			get
			{
				return RatioType.attributeTagNames;
			}
		}

		// Token: 0x170061CF RID: 25039
		// (get) Token: 0x060138D3 RID: 80083 RVA: 0x0030834A File Offset: 0x0030654A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RatioType.attributeNamespaceIds;
			}
		}

		// Token: 0x170061D0 RID: 25040
		// (get) Token: 0x060138D4 RID: 80084 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060138D5 RID: 80085 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "n")]
		public Int32Value Numerator
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

		// Token: 0x170061D1 RID: 25041
		// (get) Token: 0x060138D6 RID: 80086 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060138D7 RID: 80087 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "d")]
		public Int32Value Denominator
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

		// Token: 0x060138D8 RID: 80088 RVA: 0x00308351 File Offset: 0x00306551
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "n" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "d" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060138DA RID: 80090 RVA: 0x00308388 File Offset: 0x00306588
		// Note: this type is marked as 'beforefieldinit'.
		static RatioType()
		{
			byte[] array = new byte[2];
			RatioType.attributeNamespaceIds = array;
		}

		// Token: 0x040086B5 RID: 34485
		private static string[] attributeTagNames = new string[] { "n", "d" };

		// Token: 0x040086B6 RID: 34486
		private static byte[] attributeNamespaceIds;
	}
}
