using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002686 RID: 9862
	[GeneratedCode("DomGen", "2.0")]
	internal class AnimateOneByOne : OpenXmlLeafElement
	{
		// Token: 0x17005CC5 RID: 23749
		// (get) Token: 0x06012D95 RID: 77205 RVA: 0x0030002B File Offset: 0x002FE22B
		public override string LocalName
		{
			get
			{
				return "animOne";
			}
		}

		// Token: 0x17005CC6 RID: 23750
		// (get) Token: 0x06012D96 RID: 77206 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005CC7 RID: 23751
		// (get) Token: 0x06012D97 RID: 77207 RVA: 0x00300032 File Offset: 0x002FE232
		internal override int ElementTypeId
		{
			get
			{
				return 10677;
			}
		}

		// Token: 0x06012D98 RID: 77208 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005CC8 RID: 23752
		// (get) Token: 0x06012D99 RID: 77209 RVA: 0x00300039 File Offset: 0x002FE239
		internal override string[] AttributeTagNames
		{
			get
			{
				return AnimateOneByOne.attributeTagNames;
			}
		}

		// Token: 0x17005CC9 RID: 23753
		// (get) Token: 0x06012D9A RID: 77210 RVA: 0x00300040 File Offset: 0x002FE240
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AnimateOneByOne.attributeNamespaceIds;
			}
		}

		// Token: 0x17005CCA RID: 23754
		// (get) Token: 0x06012D9B RID: 77211 RVA: 0x00300047 File Offset: 0x002FE247
		// (set) Token: 0x06012D9C RID: 77212 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<AnimateOneByOneValues> Val
		{
			get
			{
				return (EnumValue<AnimateOneByOneValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012D9E RID: 77214 RVA: 0x00300056 File Offset: 0x002FE256
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<AnimateOneByOneValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012D9F RID: 77215 RVA: 0x00300076 File Offset: 0x002FE276
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AnimateOneByOne>(deep);
		}

		// Token: 0x06012DA0 RID: 77216 RVA: 0x00300080 File Offset: 0x002FE280
		// Note: this type is marked as 'beforefieldinit'.
		static AnimateOneByOne()
		{
			byte[] array = new byte[1];
			AnimateOneByOne.attributeNamespaceIds = array;
		}

		// Token: 0x040081E4 RID: 33252
		private const string tagName = "animOne";

		// Token: 0x040081E5 RID: 33253
		private const byte tagNsId = 14;

		// Token: 0x040081E6 RID: 33254
		internal const int ElementTypeIdConst = 10677;

		// Token: 0x040081E7 RID: 33255
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040081E8 RID: 33256
		private static byte[] attributeNamespaceIds;
	}
}
