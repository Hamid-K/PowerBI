using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Text;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B4 RID: 1204
	public abstract class EdmType : GlobalItem, INamedDataModelItem
	{
		// Token: 0x06003B42 RID: 15170 RVA: 0x000C3CFD File Offset: 0x000C1EFD
		internal static IEnumerable<T> SafeTraverseHierarchy<T>(T startFrom) where T : EdmType
		{
			HashSet<T> visitedTypes = new HashSet<T>();
			T thisType = startFrom;
			while (thisType != null && !visitedTypes.Contains(thisType))
			{
				visitedTypes.Add(thisType);
				yield return thisType;
				thisType = thisType.BaseType as T;
			}
			yield break;
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x000C3D0D File Offset: 0x000C1F0D
		internal EdmType()
		{
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x000C3D15 File Offset: 0x000C1F15
		internal EdmType(string name, string namespaceName, DataSpace dataSpace)
		{
			Check.NotNull<string>(name, "name");
			Check.NotNull<string>(namespaceName, "namespaceName");
			EdmType.Initialize(this, name, namespaceName, dataSpace, false, null);
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06003B45 RID: 15173 RVA: 0x000C3D40 File Offset: 0x000C1F40
		// (set) Token: 0x06003B46 RID: 15174 RVA: 0x000C3D48 File Offset: 0x000C1F48
		internal string CacheIdentity { get; private set; }

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06003B47 RID: 15175 RVA: 0x000C3D51 File Offset: 0x000C1F51
		string INamedDataModelItem.Identity
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000C3D5C File Offset: 0x000C1F5C
		internal override string Identity
		{
			get
			{
				if (this.CacheIdentity == null)
				{
					StringBuilder stringBuilder = new StringBuilder(50);
					this.BuildIdentity(stringBuilder);
					this.CacheIdentity = stringBuilder.ToString();
				}
				return this.CacheIdentity;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x000C3D92 File Offset: 0x000C1F92
		// (set) Token: 0x06003B4A RID: 15178 RVA: 0x000C3D9A File Offset: 0x000C1F9A
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this._name = value;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x000C3DA9 File Offset: 0x000C1FA9
		// (set) Token: 0x06003B4C RID: 15180 RVA: 0x000C3DB1 File Offset: 0x000C1FB1
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public virtual string NamespaceName
		{
			get
			{
				return this._namespace;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this._namespace = value;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06003B4D RID: 15181 RVA: 0x000C3DC0 File Offset: 0x000C1FC0
		// (set) Token: 0x06003B4E RID: 15182 RVA: 0x000C3DCA File Offset: 0x000C1FCA
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool Abstract
		{
			get
			{
				return base.GetFlag(MetadataItem.MetadataFlags.IsAbstract);
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				base.SetFlag(MetadataItem.MetadataFlags.IsAbstract, value);
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06003B4F RID: 15183 RVA: 0x000C3DDB File Offset: 0x000C1FDB
		// (set) Token: 0x06003B50 RID: 15184 RVA: 0x000C3DE3 File Offset: 0x000C1FE3
		[MetadataProperty(BuiltInTypeKind.EdmType, false)]
		public virtual EdmType BaseType
		{
			get
			{
				return this._baseType;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this.CheckBaseType(value);
				this._baseType = value;
			}
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000C3DFC File Offset: 0x000C1FFC
		private void CheckBaseType(EdmType baseType)
		{
			for (EdmType edmType = baseType; edmType != null; edmType = edmType.BaseType)
			{
				if (edmType == this)
				{
					throw new ArgumentException(Strings.CannotSetBaseTypeCyclicInheritance(baseType.Name, this.Name));
				}
			}
			if (baseType != null && Helper.IsEntityTypeBase(this) && ((EntityTypeBase)baseType).KeyMembers.Count != 0 && ((EntityTypeBase)this).KeyMembers.Count != 0)
			{
				throw new ArgumentException(Strings.CannotDefineKeysOnBothBaseAndDerivedTypes);
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06003B52 RID: 15186 RVA: 0x000C3E6C File Offset: 0x000C206C
		public virtual string FullName
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06003B53 RID: 15187 RVA: 0x000C3E74 File Offset: 0x000C2074
		internal virtual Type ClrType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x000C3E77 File Offset: 0x000C2077
		internal override void BuildIdentity(StringBuilder builder)
		{
			if (this.CacheIdentity != null)
			{
				builder.Append(this.CacheIdentity);
				return;
			}
			builder.Append(EdmType.CreateEdmTypeIdentity(this.NamespaceName, this.Name));
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x000C3EA8 File Offset: 0x000C20A8
		internal static string CreateEdmTypeIdentity(string namespaceName, string name)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(namespaceName))
			{
				text = namespaceName + ".";
			}
			return text + name;
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x000C3ED8 File Offset: 0x000C20D8
		internal static void Initialize(EdmType type, string name, string namespaceName, DataSpace dataSpace, bool isAbstract, EdmType baseType)
		{
			type._baseType = baseType;
			type._name = name;
			type._namespace = namespaceName;
			type.DataSpace = dataSpace;
			type.Abstract = isAbstract;
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x000C3EFF File Offset: 0x000C20FF
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x000C3F07 File Offset: 0x000C2107
		public CollectionType GetCollectionType()
		{
			if (this._collectionType == null)
			{
				Interlocked.CompareExchange<CollectionType>(ref this._collectionType, new CollectionType(this), null);
			}
			return this._collectionType;
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x000C3F2A File Offset: 0x000C212A
		internal virtual bool IsSubtypeOf(EdmType otherType)
		{
			return Helper.IsSubtypeOf(this, otherType);
		}

		// Token: 0x06003B5A RID: 15194 RVA: 0x000C3F33 File Offset: 0x000C2133
		internal virtual bool IsBaseTypeOf(EdmType otherType)
		{
			return otherType != null && otherType.IsSubtypeOf(this);
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x000C3F41 File Offset: 0x000C2141
		internal virtual bool IsAssignableFrom(EdmType otherType)
		{
			return Helper.IsAssignableFrom(this, otherType);
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x000C3F4C File Offset: 0x000C214C
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				EdmType baseType = this.BaseType;
				if (baseType != null)
				{
					baseType.SetReadOnly();
				}
			}
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x000C3F77 File Offset: 0x000C2177
		internal virtual IEnumerable<FacetDescription> GetAssociatedFacetDescriptions()
		{
			return MetadataItem.GetGeneralFacetDescriptions();
		}

		// Token: 0x04001479 RID: 5241
		private CollectionType _collectionType;

		// Token: 0x0400147A RID: 5242
		private string _name;

		// Token: 0x0400147B RID: 5243
		private string _namespace;

		// Token: 0x0400147C RID: 5244
		private EdmType _baseType;
	}
}
