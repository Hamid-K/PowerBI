using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027BD RID: 10173
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class RelativeRectangleType : OpenXmlLeafElement
	{
		// Token: 0x17006354 RID: 25428
		// (get) Token: 0x06013C14 RID: 80916 RVA: 0x0030B677 File Offset: 0x00309877
		internal override string[] AttributeTagNames
		{
			get
			{
				return RelativeRectangleType.attributeTagNames;
			}
		}

		// Token: 0x17006355 RID: 25429
		// (get) Token: 0x06013C15 RID: 80917 RVA: 0x0030B67E File Offset: 0x0030987E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RelativeRectangleType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006356 RID: 25430
		// (get) Token: 0x06013C16 RID: 80918 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013C17 RID: 80919 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "l")]
		public Int32Value Left
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

		// Token: 0x17006357 RID: 25431
		// (get) Token: 0x06013C18 RID: 80920 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06013C19 RID: 80921 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "t")]
		public Int32Value Top
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

		// Token: 0x17006358 RID: 25432
		// (get) Token: 0x06013C1A RID: 80922 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06013C1B RID: 80923 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "r")]
		public Int32Value Right
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17006359 RID: 25433
		// (get) Token: 0x06013C1C RID: 80924 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06013C1D RID: 80925 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "b")]
		public Int32Value Bottom
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06013C1E RID: 80926 RVA: 0x0030B688 File Offset: 0x00309888
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "l" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "t" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "r" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "b" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013C20 RID: 80928 RVA: 0x0030B6F8 File Offset: 0x003098F8
		// Note: this type is marked as 'beforefieldinit'.
		static RelativeRectangleType()
		{
			byte[] array = new byte[4];
			RelativeRectangleType.attributeNamespaceIds = array;
		}

		// Token: 0x0400879F RID: 34719
		private static string[] attributeTagNames = new string[] { "l", "t", "r", "b" };

		// Token: 0x040087A0 RID: 34720
		private static byte[] attributeNamespaceIds;
	}
}
