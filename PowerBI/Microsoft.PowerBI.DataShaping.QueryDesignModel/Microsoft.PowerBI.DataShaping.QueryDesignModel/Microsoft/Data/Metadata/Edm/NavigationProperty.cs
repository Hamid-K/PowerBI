using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000A0 RID: 160
	public sealed class NavigationProperty : EdmMember
	{
		// Token: 0x06000B16 RID: 2838 RVA: 0x0001B310 File Offset: 0x00019510
		internal NavigationProperty(string name, TypeUsage typeUsage)
			: base(name, typeUsage)
		{
			EntityUtil.CheckStringArgument(name, "name");
			EntityUtil.GenericCheckArgumentNull<TypeUsage>(typeUsage, "typeUsage");
			this._accessor = new NavigationPropertyAccessor(name);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001B340 File Offset: 0x00019540
		internal NavigationProperty(string name, TypeUsage typeUsage, PropertyInfo propertyInfo)
			: this(name, typeUsage)
		{
			if (null != propertyInfo)
			{
				MethodInfo getMethod = propertyInfo.GetGetMethod();
				this.PropertyGetterHandle = ((null != getMethod) ? getMethod.MethodHandle : default(RuntimeMethodHandle));
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0001B385 File Offset: 0x00019585
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.NavigationProperty;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0001B389 File Offset: 0x00019589
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x0001B391 File Offset: 0x00019591
		[MetadataProperty(BuiltInTypeKind.RelationshipType, false)]
		public RelationshipType RelationshipType
		{
			get
			{
				return this._relationshipType;
			}
			internal set
			{
				this._relationshipType = value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0001B39A File Offset: 0x0001959A
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x0001B3A2 File Offset: 0x000195A2
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember ToEndMember
		{
			get
			{
				return this._toEndMember;
			}
			internal set
			{
				this._toEndMember = value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0001B3AB File Offset: 0x000195AB
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x0001B3B3 File Offset: 0x000195B3
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember FromEndMember
		{
			get
			{
				return this._fromEndMember;
			}
			internal set
			{
				this._fromEndMember = value;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0001B3BC File Offset: 0x000195BC
		internal NavigationPropertyAccessor Accessor
		{
			get
			{
				return this._accessor;
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001B3C4 File Offset: 0x000195C4
		public IEnumerable<EdmProperty> GetDependentProperties()
		{
			AssociationType associationType = (AssociationType)this.RelationshipType;
			if (associationType.ReferentialConstraints.Count > 0)
			{
				ReferentialConstraint referentialConstraint = associationType.ReferentialConstraints[0];
				if (referentialConstraint.ToRole.EdmEquals(this.FromEndMember))
				{
					ReadOnlyMetadataCollection<EdmMember> keyMembers = referentialConstraint.FromRole.GetEntityType().KeyMembers;
					List<EdmProperty> list = new List<EdmProperty>(keyMembers.Count);
					for (int i = 0; i < keyMembers.Count; i++)
					{
						list.Add(referentialConstraint.ToProperties[referentialConstraint.FromProperties.IndexOf((EdmProperty)keyMembers[i])]);
					}
					return list.AsReadOnly();
				}
			}
			return Enumerable.Empty<EdmProperty>();
		}

		// Token: 0x04000870 RID: 2160
		internal const string RelationshipTypeNamePropertyName = "RelationshipType";

		// Token: 0x04000871 RID: 2161
		internal const string ToEndMemberNamePropertyName = "ToEndMember";

		// Token: 0x04000872 RID: 2162
		private RelationshipType _relationshipType;

		// Token: 0x04000873 RID: 2163
		private RelationshipEndMember _toEndMember;

		// Token: 0x04000874 RID: 2164
		private RelationshipEndMember _fromEndMember;

		// Token: 0x04000875 RID: 2165
		internal readonly RuntimeMethodHandle PropertyGetterHandle;

		// Token: 0x04000876 RID: 2166
		private readonly NavigationPropertyAccessor _accessor;
	}
}
