using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000660 RID: 1632
	internal sealed class MetadataNamespace : MetadataMember
	{
		// Token: 0x06004E07 RID: 19975 RVA: 0x001187CA File Offset: 0x001169CA
		internal MetadataNamespace(string name)
			: base(MetadataMemberClass.Namespace, name)
		{
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06004E08 RID: 19976 RVA: 0x001187D4 File Offset: 0x001169D4
		internal override string MetadataMemberClassName
		{
			get
			{
				return MetadataNamespace.NamespaceClassName;
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06004E09 RID: 19977 RVA: 0x001187DB File Offset: 0x001169DB
		internal static string NamespaceClassName
		{
			get
			{
				return Strings.LocalizedNamespace;
			}
		}
	}
}
