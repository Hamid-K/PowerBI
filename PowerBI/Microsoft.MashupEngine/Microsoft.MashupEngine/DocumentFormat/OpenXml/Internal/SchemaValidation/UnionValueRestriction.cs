using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200312B RID: 12587
	[Serializable]
	internal class UnionValueRestriction : SimpleTypeRestriction
	{
		// Token: 0x1700994C RID: 39244
		// (get) Token: 0x0601B4DB RID: 111835 RVA: 0x00375FDF File Offset: 0x003741DF
		// (set) Token: 0x0601B4DC RID: 111836 RVA: 0x00375FE7 File Offset: 0x003741E7
		public SimpleTypeRestriction[] UnionTypes
		{
			get
			{
				return this.unionField;
			}
			set
			{
				this.unionField = value;
			}
		}

		// Token: 0x1700994D RID: 39245
		// (get) Token: 0x0601B4DD RID: 111837 RVA: 0x00375FF0 File Offset: 0x003741F0
		// (set) Token: 0x0601B4DE RID: 111838 RVA: 0x00375FF8 File Offset: 0x003741F8
		internal int UnionId { get; set; }

		// Token: 0x1700994E RID: 39246
		// (get) Token: 0x0601B4E0 RID: 111840 RVA: 0x002DF9A4 File Offset: 0x002DDBA4
		// (set) Token: 0x0601B4E1 RID: 111841 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.Union;
			}
			set
			{
			}
		}

		// Token: 0x1700994F RID: 39247
		// (get) Token: 0x0601B4E2 RID: 111842 RVA: 0x0007E4B5 File Offset: 0x0007C6B5
		public override string ClrTypeName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x0601B4E3 RID: 111843 RVA: 0x00376004 File Offset: 0x00374204
		public override bool ValidateValueType(OpenXmlSimpleType attributeValue)
		{
			StringValue stringValue = attributeValue.InnerText;
			OpenXmlSimpleType[] array = UnionHelper.CreatePossibleMembers(this, base.FileFormat);
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				OpenXmlSimpleType openXmlSimpleType = array[i];
				openXmlSimpleType.InnerText = stringValue;
				SimpleTypeRestriction simpleTypeRestriction = this.UnionTypes[i];
				if (simpleTypeRestriction.ValidateValueType(openXmlSimpleType) && simpleTypeRestriction.Validate(openXmlSimpleType) == RestrictionField.None)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400B525 RID: 46373
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SimpleTypeRestriction[] unionField;
	}
}
