using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027B6 RID: 10166
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class Vector3DType : OpenXmlLeafElement
	{
		// Token: 0x1700633B RID: 25403
		// (get) Token: 0x06013BE0 RID: 80864 RVA: 0x0030B4AB File Offset: 0x003096AB
		internal override string[] AttributeTagNames
		{
			get
			{
				return Vector3DType.attributeTagNames;
			}
		}

		// Token: 0x1700633C RID: 25404
		// (get) Token: 0x06013BE1 RID: 80865 RVA: 0x0030B4B2 File Offset: 0x003096B2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Vector3DType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700633D RID: 25405
		// (get) Token: 0x06013BE2 RID: 80866 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06013BE3 RID: 80867 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "dx")]
		public Int64Value Dx
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

		// Token: 0x1700633E RID: 25406
		// (get) Token: 0x06013BE4 RID: 80868 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x06013BE5 RID: 80869 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dy")]
		public Int64Value Dy
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

		// Token: 0x1700633F RID: 25407
		// (get) Token: 0x06013BE6 RID: 80870 RVA: 0x002E0CD2 File Offset: 0x002DEED2
		// (set) Token: 0x06013BE7 RID: 80871 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "dz")]
		public Int64Value Dz
		{
			get
			{
				return (Int64Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06013BE8 RID: 80872 RVA: 0x0030B4BC File Offset: 0x003096BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dx" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "dy" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "dz" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013BEA RID: 80874 RVA: 0x0030B514 File Offset: 0x00309714
		// Note: this type is marked as 'beforefieldinit'.
		static Vector3DType()
		{
			byte[] array = new byte[3];
			Vector3DType.attributeNamespaceIds = array;
		}

		// Token: 0x0400878C RID: 34700
		private static string[] attributeTagNames = new string[] { "dx", "dy", "dz" };

		// Token: 0x0400878D RID: 34701
		private static byte[] attributeNamespaceIds;
	}
}
