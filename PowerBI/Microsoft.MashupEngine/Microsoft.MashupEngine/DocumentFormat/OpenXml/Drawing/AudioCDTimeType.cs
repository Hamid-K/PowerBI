using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200276D RID: 10093
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class AudioCDTimeType : OpenXmlLeafElement
	{
		// Token: 0x1700613A RID: 24890
		// (get) Token: 0x06013768 RID: 79720 RVA: 0x0030757F File Offset: 0x0030577F
		internal override string[] AttributeTagNames
		{
			get
			{
				return AudioCDTimeType.attributeTagNames;
			}
		}

		// Token: 0x1700613B RID: 24891
		// (get) Token: 0x06013769 RID: 79721 RVA: 0x00307586 File Offset: 0x00305786
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AudioCDTimeType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700613C RID: 24892
		// (get) Token: 0x0601376A RID: 79722 RVA: 0x002DE388 File Offset: 0x002DC588
		// (set) Token: 0x0601376B RID: 79723 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "track")]
		public ByteValue Track
		{
			get
			{
				return (ByteValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700613D RID: 24893
		// (get) Token: 0x0601376C RID: 79724 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601376D RID: 79725 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "time")]
		public UInt32Value Time
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

		// Token: 0x0601376E RID: 79726 RVA: 0x0030758D File Offset: 0x0030578D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "track" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "time" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013770 RID: 79728 RVA: 0x003075C4 File Offset: 0x003057C4
		// Note: this type is marked as 'beforefieldinit'.
		static AudioCDTimeType()
		{
			byte[] array = new byte[2];
			AudioCDTimeType.attributeNamespaceIds = array;
		}

		// Token: 0x04008652 RID: 34386
		private static string[] attributeTagNames = new string[] { "track", "time" };

		// Token: 0x04008653 RID: 34387
		private static byte[] attributeNamespaceIds;
	}
}
