using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200065C RID: 1628
	internal sealed class MetadataEnumMember : MetadataMember
	{
		// Token: 0x06004DFC RID: 19964 RVA: 0x00118758 File Offset: 0x00116958
		internal MetadataEnumMember(string name, TypeUsage enumType, EnumMember enumMember)
			: base(MetadataMemberClass.EnumMember, name)
		{
			this.EnumType = enumType;
			this.EnumMember = enumMember;
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06004DFD RID: 19965 RVA: 0x00118770 File Offset: 0x00116970
		internal override string MetadataMemberClassName
		{
			get
			{
				return MetadataEnumMember.EnumMemberClassName;
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06004DFE RID: 19966 RVA: 0x00118777 File Offset: 0x00116977
		internal static string EnumMemberClassName
		{
			get
			{
				return Strings.LocalizedEnumMember;
			}
		}

		// Token: 0x04001C47 RID: 7239
		internal readonly TypeUsage EnumType;

		// Token: 0x04001C48 RID: 7240
		internal readonly EnumMember EnumMember;
	}
}
