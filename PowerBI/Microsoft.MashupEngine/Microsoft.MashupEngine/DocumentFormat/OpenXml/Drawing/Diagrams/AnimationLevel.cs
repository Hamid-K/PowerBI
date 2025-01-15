using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002687 RID: 9863
	[GeneratedCode("DomGen", "2.0")]
	internal class AnimationLevel : OpenXmlLeafElement
	{
		// Token: 0x17005CCB RID: 23755
		// (get) Token: 0x06012DA1 RID: 77217 RVA: 0x003000AF File Offset: 0x002FE2AF
		public override string LocalName
		{
			get
			{
				return "animLvl";
			}
		}

		// Token: 0x17005CCC RID: 23756
		// (get) Token: 0x06012DA2 RID: 77218 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005CCD RID: 23757
		// (get) Token: 0x06012DA3 RID: 77219 RVA: 0x003000B6 File Offset: 0x002FE2B6
		internal override int ElementTypeId
		{
			get
			{
				return 10678;
			}
		}

		// Token: 0x06012DA4 RID: 77220 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005CCE RID: 23758
		// (get) Token: 0x06012DA5 RID: 77221 RVA: 0x003000BD File Offset: 0x002FE2BD
		internal override string[] AttributeTagNames
		{
			get
			{
				return AnimationLevel.attributeTagNames;
			}
		}

		// Token: 0x17005CCF RID: 23759
		// (get) Token: 0x06012DA6 RID: 77222 RVA: 0x003000C4 File Offset: 0x002FE2C4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AnimationLevel.attributeNamespaceIds;
			}
		}

		// Token: 0x17005CD0 RID: 23760
		// (get) Token: 0x06012DA7 RID: 77223 RVA: 0x003000CB File Offset: 0x002FE2CB
		// (set) Token: 0x06012DA8 RID: 77224 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<AnimationLevelStringValues> Val
		{
			get
			{
				return (EnumValue<AnimationLevelStringValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012DAA RID: 77226 RVA: 0x003000DA File Offset: 0x002FE2DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<AnimationLevelStringValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012DAB RID: 77227 RVA: 0x003000FA File Offset: 0x002FE2FA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AnimationLevel>(deep);
		}

		// Token: 0x06012DAC RID: 77228 RVA: 0x00300104 File Offset: 0x002FE304
		// Note: this type is marked as 'beforefieldinit'.
		static AnimationLevel()
		{
			byte[] array = new byte[1];
			AnimationLevel.attributeNamespaceIds = array;
		}

		// Token: 0x040081E9 RID: 33257
		private const string tagName = "animLvl";

		// Token: 0x040081EA RID: 33258
		private const byte tagNsId = 14;

		// Token: 0x040081EB RID: 33259
		internal const int ElementTypeIdConst = 10678;

		// Token: 0x040081EC RID: 33260
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040081ED RID: 33261
		private static byte[] attributeNamespaceIds;
	}
}
