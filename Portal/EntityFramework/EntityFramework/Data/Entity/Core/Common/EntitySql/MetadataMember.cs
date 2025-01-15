using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200065E RID: 1630
	internal abstract class MetadataMember : ExpressionResolution
	{
		// Token: 0x06004E02 RID: 19970 RVA: 0x0011879D File Offset: 0x0011699D
		protected MetadataMember(MetadataMemberClass @class, string name)
			: base(ExpressionResolutionClass.MetadataMember)
		{
			this.MetadataMemberClass = @class;
			this.Name = name;
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06004E03 RID: 19971 RVA: 0x001187B4 File Offset: 0x001169B4
		internal override string ExpressionClassName
		{
			get
			{
				return MetadataMember.MetadataMemberExpressionClassName;
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06004E04 RID: 19972 RVA: 0x001187BB File Offset: 0x001169BB
		internal static string MetadataMemberExpressionClassName
		{
			get
			{
				return Strings.LocalizedMetadataMemberExpression;
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06004E05 RID: 19973
		internal abstract string MetadataMemberClassName { get; }

		// Token: 0x06004E06 RID: 19974 RVA: 0x001187C2 File Offset: 0x001169C2
		internal static IEqualityComparer<MetadataMember> CreateMetadataMemberNameEqualityComparer(StringComparer stringComparer)
		{
			return new MetadataMember.MetadataMemberNameEqualityComparer(stringComparer);
		}

		// Token: 0x04001C4A RID: 7242
		internal readonly MetadataMemberClass MetadataMemberClass;

		// Token: 0x04001C4B RID: 7243
		internal readonly string Name;

		// Token: 0x02000C72 RID: 3186
		private sealed class MetadataMemberNameEqualityComparer : IEqualityComparer<MetadataMember>
		{
			// Token: 0x06006B39 RID: 27449 RVA: 0x0016EC88 File Offset: 0x0016CE88
			internal MetadataMemberNameEqualityComparer(StringComparer stringComparer)
			{
				this._stringComparer = stringComparer;
			}

			// Token: 0x06006B3A RID: 27450 RVA: 0x0016EC97 File Offset: 0x0016CE97
			bool IEqualityComparer<MetadataMember>.Equals(MetadataMember x, MetadataMember y)
			{
				return this._stringComparer.Equals(x.Name, y.Name);
			}

			// Token: 0x06006B3B RID: 27451 RVA: 0x0016ECB0 File Offset: 0x0016CEB0
			int IEqualityComparer<MetadataMember>.GetHashCode(MetadataMember obj)
			{
				return this._stringComparer.GetHashCode(obj.Name);
			}

			// Token: 0x04003127 RID: 12583
			private readonly StringComparer _stringComparer;
		}
	}
}
