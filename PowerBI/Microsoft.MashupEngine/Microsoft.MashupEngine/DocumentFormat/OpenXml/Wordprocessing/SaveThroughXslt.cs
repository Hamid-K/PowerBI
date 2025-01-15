using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FEF RID: 12271
	[GeneratedCode("DomGen", "2.0")]
	internal class SaveThroughXslt : OpenXmlLeafElement
	{
		// Token: 0x17009538 RID: 38200
		// (get) Token: 0x0601AB0F RID: 109327 RVA: 0x00365F25 File Offset: 0x00364125
		public override string LocalName
		{
			get
			{
				return "saveThroughXslt";
			}
		}

		// Token: 0x17009539 RID: 38201
		// (get) Token: 0x0601AB10 RID: 109328 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700953A RID: 38202
		// (get) Token: 0x0601AB11 RID: 109329 RVA: 0x00365F2C File Offset: 0x0036412C
		internal override int ElementTypeId
		{
			get
			{
				return 12031;
			}
		}

		// Token: 0x0601AB12 RID: 109330 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700953B RID: 38203
		// (get) Token: 0x0601AB13 RID: 109331 RVA: 0x00365F33 File Offset: 0x00364133
		internal override string[] AttributeTagNames
		{
			get
			{
				return SaveThroughXslt.attributeTagNames;
			}
		}

		// Token: 0x1700953C RID: 38204
		// (get) Token: 0x0601AB14 RID: 109332 RVA: 0x00365F3A File Offset: 0x0036413A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SaveThroughXslt.attributeNamespaceIds;
			}
		}

		// Token: 0x1700953D RID: 38205
		// (get) Token: 0x0601AB15 RID: 109333 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AB16 RID: 109334 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700953E RID: 38206
		// (get) Token: 0x0601AB17 RID: 109335 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601AB18 RID: 109336 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "solutionID")]
		public StringValue SolutionId
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601AB1A RID: 109338 RVA: 0x00365F41 File Offset: 0x00364141
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "solutionID" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AB1B RID: 109339 RVA: 0x00365F7B File Offset: 0x0036417B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SaveThroughXslt>(deep);
		}

		// Token: 0x0400AE0C RID: 44556
		private const string tagName = "saveThroughXslt";

		// Token: 0x0400AE0D RID: 44557
		private const byte tagNsId = 23;

		// Token: 0x0400AE0E RID: 44558
		internal const int ElementTypeIdConst = 12031;

		// Token: 0x0400AE0F RID: 44559
		private static string[] attributeTagNames = new string[] { "id", "solutionID" };

		// Token: 0x0400AE10 RID: 44560
		private static byte[] attributeNamespaceIds = new byte[] { 19, 23 };
	}
}
