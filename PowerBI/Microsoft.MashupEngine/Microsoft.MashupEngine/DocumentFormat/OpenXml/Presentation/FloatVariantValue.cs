using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A2D RID: 10797
	[GeneratedCode("DomGen", "2.0")]
	internal class FloatVariantValue : OpenXmlLeafElement
	{
		// Token: 0x170070B7 RID: 28855
		// (get) Token: 0x06015AE5 RID: 88805 RVA: 0x00321F6F File Offset: 0x0032016F
		public override string LocalName
		{
			get
			{
				return "fltVal";
			}
		}

		// Token: 0x170070B8 RID: 28856
		// (get) Token: 0x06015AE6 RID: 88806 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070B9 RID: 28857
		// (get) Token: 0x06015AE7 RID: 88807 RVA: 0x00321F76 File Offset: 0x00320176
		internal override int ElementTypeId
		{
			get
			{
				return 12219;
			}
		}

		// Token: 0x06015AE8 RID: 88808 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170070BA RID: 28858
		// (get) Token: 0x06015AE9 RID: 88809 RVA: 0x00321F7D File Offset: 0x0032017D
		internal override string[] AttributeTagNames
		{
			get
			{
				return FloatVariantValue.attributeTagNames;
			}
		}

		// Token: 0x170070BB RID: 28859
		// (get) Token: 0x06015AEA RID: 88810 RVA: 0x00321F84 File Offset: 0x00320184
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FloatVariantValue.attributeNamespaceIds;
			}
		}

		// Token: 0x170070BC RID: 28860
		// (get) Token: 0x06015AEB RID: 88811 RVA: 0x00321F8B File Offset: 0x0032018B
		// (set) Token: 0x06015AEC RID: 88812 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public SingleValue Val
		{
			get
			{
				return (SingleValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015AEE RID: 88814 RVA: 0x00321F9A File Offset: 0x0032019A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new SingleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015AEF RID: 88815 RVA: 0x00321FBA File Offset: 0x003201BA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FloatVariantValue>(deep);
		}

		// Token: 0x06015AF0 RID: 88816 RVA: 0x00321FC4 File Offset: 0x003201C4
		// Note: this type is marked as 'beforefieldinit'.
		static FloatVariantValue()
		{
			byte[] array = new byte[1];
			FloatVariantValue.attributeNamespaceIds = array;
		}

		// Token: 0x0400945D RID: 37981
		private const string tagName = "fltVal";

		// Token: 0x0400945E RID: 37982
		private const byte tagNsId = 24;

		// Token: 0x0400945F RID: 37983
		internal const int ElementTypeIdConst = 12219;

		// Token: 0x04009460 RID: 37984
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009461 RID: 37985
		private static byte[] attributeNamespaceIds;
	}
}
