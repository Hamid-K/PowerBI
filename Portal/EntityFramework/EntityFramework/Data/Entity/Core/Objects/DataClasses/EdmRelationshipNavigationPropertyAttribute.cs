using System;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000470 RID: 1136
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class EdmRelationshipNavigationPropertyAttribute : EdmPropertyAttribute
	{
		// Token: 0x06003787 RID: 14215 RVA: 0x000B5F0E File Offset: 0x000B410E
		public EdmRelationshipNavigationPropertyAttribute(string relationshipNamespaceName, string relationshipName, string targetRoleName)
		{
			this._relationshipNamespaceName = relationshipNamespaceName;
			this._relationshipName = relationshipName;
			this._targetRoleName = targetRoleName;
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06003788 RID: 14216 RVA: 0x000B5F2B File Offset: 0x000B412B
		public string RelationshipNamespaceName
		{
			get
			{
				return this._relationshipNamespaceName;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06003789 RID: 14217 RVA: 0x000B5F33 File Offset: 0x000B4133
		public string RelationshipName
		{
			get
			{
				return this._relationshipName;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600378A RID: 14218 RVA: 0x000B5F3B File Offset: 0x000B413B
		public string TargetRoleName
		{
			get
			{
				return this._targetRoleName;
			}
		}

		// Token: 0x040012C8 RID: 4808
		private readonly string _relationshipNamespaceName;

		// Token: 0x040012C9 RID: 4809
		private readonly string _relationshipName;

		// Token: 0x040012CA RID: 4810
		private readonly string _targetRoleName;
	}
}
