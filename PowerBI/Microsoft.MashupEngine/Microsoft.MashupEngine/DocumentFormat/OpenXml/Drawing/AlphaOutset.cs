using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200270F RID: 9999
	[GeneratedCode("DomGen", "2.0")]
	internal class AlphaOutset : OpenXmlLeafElement
	{
		// Token: 0x17005ED1 RID: 24273
		// (get) Token: 0x06013202 RID: 78338 RVA: 0x00303EFF File Offset: 0x003020FF
		public override string LocalName
		{
			get
			{
				return "alphaOutset";
			}
		}

		// Token: 0x17005ED2 RID: 24274
		// (get) Token: 0x06013203 RID: 78339 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005ED3 RID: 24275
		// (get) Token: 0x06013204 RID: 78340 RVA: 0x00303F06 File Offset: 0x00302106
		internal override int ElementTypeId
		{
			get
			{
				return 10061;
			}
		}

		// Token: 0x06013205 RID: 78341 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005ED4 RID: 24276
		// (get) Token: 0x06013206 RID: 78342 RVA: 0x00303F0D File Offset: 0x0030210D
		internal override string[] AttributeTagNames
		{
			get
			{
				return AlphaOutset.attributeTagNames;
			}
		}

		// Token: 0x17005ED5 RID: 24277
		// (get) Token: 0x06013207 RID: 78343 RVA: 0x00303F14 File Offset: 0x00302114
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AlphaOutset.attributeNamespaceIds;
			}
		}

		// Token: 0x17005ED6 RID: 24278
		// (get) Token: 0x06013208 RID: 78344 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06013209 RID: 78345 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rad")]
		public Int64Value Radius
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

		// Token: 0x0601320B RID: 78347 RVA: 0x00303F1B File Offset: 0x0030211B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rad" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601320C RID: 78348 RVA: 0x00303F3B File Offset: 0x0030213B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlphaOutset>(deep);
		}

		// Token: 0x0601320D RID: 78349 RVA: 0x00303F44 File Offset: 0x00302144
		// Note: this type is marked as 'beforefieldinit'.
		static AlphaOutset()
		{
			byte[] array = new byte[1];
			AlphaOutset.attributeNamespaceIds = array;
		}

		// Token: 0x040084CC RID: 33996
		private const string tagName = "alphaOutset";

		// Token: 0x040084CD RID: 33997
		private const byte tagNsId = 10;

		// Token: 0x040084CE RID: 33998
		internal const int ElementTypeIdConst = 10061;

		// Token: 0x040084CF RID: 33999
		private static string[] attributeTagNames = new string[] { "rad" };

		// Token: 0x040084D0 RID: 34000
		private static byte[] attributeNamespaceIds;
	}
}
