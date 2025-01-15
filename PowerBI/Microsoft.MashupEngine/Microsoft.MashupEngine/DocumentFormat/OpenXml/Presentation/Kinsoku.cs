using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AB8 RID: 10936
	[GeneratedCode("DomGen", "2.0")]
	internal class Kinsoku : OpenXmlLeafElement
	{
		// Token: 0x170074E7 RID: 29927
		// (get) Token: 0x0601644D RID: 91213 RVA: 0x003285BE File Offset: 0x003267BE
		public override string LocalName
		{
			get
			{
				return "kinsoku";
			}
		}

		// Token: 0x170074E8 RID: 29928
		// (get) Token: 0x0601644E RID: 91214 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074E9 RID: 29929
		// (get) Token: 0x0601644F RID: 91215 RVA: 0x003285C5 File Offset: 0x003267C5
		internal override int ElementTypeId
		{
			get
			{
				return 12351;
			}
		}

		// Token: 0x06016450 RID: 91216 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170074EA RID: 29930
		// (get) Token: 0x06016451 RID: 91217 RVA: 0x003285CC File Offset: 0x003267CC
		internal override string[] AttributeTagNames
		{
			get
			{
				return Kinsoku.attributeTagNames;
			}
		}

		// Token: 0x170074EB RID: 29931
		// (get) Token: 0x06016452 RID: 91218 RVA: 0x003285D3 File Offset: 0x003267D3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Kinsoku.attributeNamespaceIds;
			}
		}

		// Token: 0x170074EC RID: 29932
		// (get) Token: 0x06016453 RID: 91219 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016454 RID: 91220 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "lang")]
		public StringValue Language
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

		// Token: 0x170074ED RID: 29933
		// (get) Token: 0x06016455 RID: 91221 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06016456 RID: 91222 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "invalStChars")]
		public StringValue InvalidStartChars
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

		// Token: 0x170074EE RID: 29934
		// (get) Token: 0x06016457 RID: 91223 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06016458 RID: 91224 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "invalEndChars")]
		public StringValue InvalidEndChars
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601645A RID: 91226 RVA: 0x003285DC File Offset: 0x003267DC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "lang" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "invalStChars" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "invalEndChars" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601645B RID: 91227 RVA: 0x00328633 File Offset: 0x00326833
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Kinsoku>(deep);
		}

		// Token: 0x0601645C RID: 91228 RVA: 0x0032863C File Offset: 0x0032683C
		// Note: this type is marked as 'beforefieldinit'.
		static Kinsoku()
		{
			byte[] array = new byte[3];
			Kinsoku.attributeNamespaceIds = array;
		}

		// Token: 0x040096F6 RID: 38646
		private const string tagName = "kinsoku";

		// Token: 0x040096F7 RID: 38647
		private const byte tagNsId = 24;

		// Token: 0x040096F8 RID: 38648
		internal const int ElementTypeIdConst = 12351;

		// Token: 0x040096F9 RID: 38649
		private static string[] attributeTagNames = new string[] { "lang", "invalStChars", "invalEndChars" };

		// Token: 0x040096FA RID: 38650
		private static byte[] attributeNamespaceIds;
	}
}
