using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002793 RID: 10131
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class ConnectionType : OpenXmlLeafElement
	{
		// Token: 0x17006213 RID: 25107
		// (get) Token: 0x06013961 RID: 80225 RVA: 0x003088C9 File Offset: 0x00306AC9
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConnectionType.attributeTagNames;
			}
		}

		// Token: 0x17006214 RID: 25108
		// (get) Token: 0x06013962 RID: 80226 RVA: 0x003088D0 File Offset: 0x00306AD0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConnectionType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006215 RID: 25109
		// (get) Token: 0x06013963 RID: 80227 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06013964 RID: 80228 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006216 RID: 25110
		// (get) Token: 0x06013965 RID: 80229 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06013966 RID: 80230 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idx")]
		public UInt32Value Index
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06013967 RID: 80231 RVA: 0x003088D7 File Offset: 0x00306AD7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "idx" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013969 RID: 80233 RVA: 0x00308910 File Offset: 0x00306B10
		// Note: this type is marked as 'beforefieldinit'.
		static ConnectionType()
		{
			byte[] array = new byte[2];
			ConnectionType.attributeNamespaceIds = array;
		}

		// Token: 0x040086DB RID: 34523
		private static string[] attributeTagNames = new string[] { "id", "idx" };

		// Token: 0x040086DC RID: 34524
		private static byte[] attributeNamespaceIds;
	}
}
