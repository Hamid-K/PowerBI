using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B20 RID: 11040
	[ChildElementInfo(typeof(UserInfo))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Users : OpenXmlPartRootElement
	{
		// Token: 0x17007699 RID: 30361
		// (get) Token: 0x0601681C RID: 92188 RVA: 0x0032B59F File Offset: 0x0032979F
		public override string LocalName
		{
			get
			{
				return "users";
			}
		}

		// Token: 0x1700769A RID: 30362
		// (get) Token: 0x0601681D RID: 92189 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700769B RID: 30363
		// (get) Token: 0x0601681E RID: 92190 RVA: 0x0032B5A6 File Offset: 0x003297A6
		internal override int ElementTypeId
		{
			get
			{
				return 11038;
			}
		}

		// Token: 0x0601681F RID: 92191 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700769C RID: 30364
		// (get) Token: 0x06016820 RID: 92192 RVA: 0x0032B5AD File Offset: 0x003297AD
		internal override string[] AttributeTagNames
		{
			get
			{
				return Users.attributeTagNames;
			}
		}

		// Token: 0x1700769D RID: 30365
		// (get) Token: 0x06016821 RID: 92193 RVA: 0x0032B5B4 File Offset: 0x003297B4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Users.attributeNamespaceIds;
			}
		}

		// Token: 0x1700769E RID: 30366
		// (get) Token: 0x06016822 RID: 92194 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016823 RID: 92195 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x06016824 RID: 92196 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Users(WorkbookUserDataPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06016825 RID: 92197 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(WorkbookUserDataPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x1700769F RID: 30367
		// (get) Token: 0x06016826 RID: 92198 RVA: 0x0032B5BB File Offset: 0x003297BB
		// (set) Token: 0x06016827 RID: 92199 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public WorkbookUserDataPart WorkbookUserDataPart
		{
			get
			{
				return base.OpenXmlPart as WorkbookUserDataPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016828 RID: 92200 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Users(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016829 RID: 92201 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Users(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601682A RID: 92202 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Users(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601682B RID: 92203 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Users()
		{
		}

		// Token: 0x0601682C RID: 92204 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(WorkbookUserDataPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601682D RID: 92205 RVA: 0x0032B5C8 File Offset: 0x003297C8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "userInfo" == name)
			{
				return new UserInfo();
			}
			return null;
		}

		// Token: 0x0601682E RID: 92206 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601682F RID: 92207 RVA: 0x0032B5E3 File Offset: 0x003297E3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Users>(deep);
		}

		// Token: 0x06016830 RID: 92208 RVA: 0x0032B5EC File Offset: 0x003297EC
		// Note: this type is marked as 'beforefieldinit'.
		static Users()
		{
			byte[] array = new byte[1];
			Users.attributeNamespaceIds = array;
		}

		// Token: 0x04009901 RID: 39169
		private const string tagName = "users";

		// Token: 0x04009902 RID: 39170
		private const byte tagNsId = 22;

		// Token: 0x04009903 RID: 39171
		internal const int ElementTypeIdConst = 11038;

		// Token: 0x04009904 RID: 39172
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009905 RID: 39173
		private static byte[] attributeNamespaceIds;
	}
}
