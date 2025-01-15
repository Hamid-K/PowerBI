using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028A3 RID: 10403
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class Point2DType : OpenXmlLeafElement
	{
		// Token: 0x17006865 RID: 26725
		// (get) Token: 0x060147C6 RID: 83910 RVA: 0x00313EE0 File Offset: 0x003120E0
		internal override string[] AttributeTagNames
		{
			get
			{
				return Point2DType.attributeTagNames;
			}
		}

		// Token: 0x17006866 RID: 26726
		// (get) Token: 0x060147C7 RID: 83911 RVA: 0x00313EE7 File Offset: 0x003120E7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Point2DType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006867 RID: 26727
		// (get) Token: 0x060147C8 RID: 83912 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x060147C9 RID: 83913 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "x")]
		public Int64Value X
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006868 RID: 26728
		// (get) Token: 0x060147CA RID: 83914 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x060147CB RID: 83915 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "y")]
		public Int64Value Y
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060147CC RID: 83916 RVA: 0x00308403 File Offset: 0x00306603
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "x" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "y" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060147CE RID: 83918 RVA: 0x00313EF0 File Offset: 0x003120F0
		// Note: this type is marked as 'beforefieldinit'.
		static Point2DType()
		{
			byte[] array = new byte[2];
			Point2DType.attributeNamespaceIds = array;
		}

		// Token: 0x04008E4A RID: 36426
		private static string[] attributeTagNames = new string[] { "x", "y" };

		// Token: 0x04008E4B RID: 36427
		private static byte[] attributeNamespaceIds;
	}
}
