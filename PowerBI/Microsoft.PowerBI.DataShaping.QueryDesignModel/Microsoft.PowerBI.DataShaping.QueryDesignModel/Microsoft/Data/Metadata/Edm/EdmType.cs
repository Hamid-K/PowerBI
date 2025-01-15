using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000085 RID: 133
	public abstract class EdmType : GlobalItem
	{
		// Token: 0x060009D7 RID: 2519 RVA: 0x000178D0 File Offset: 0x00015AD0
		internal EdmType()
		{
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000178D8 File Offset: 0x00015AD8
		internal EdmType(string name, string namespaceName, DataSpace dataSpace)
		{
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			EntityUtil.GenericCheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmType.Initialize(this, name, namespaceName, dataSpace, false, null);
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00017903 File Offset: 0x00015B03
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x0001790B File Offset: 0x00015B0B
		internal string CacheIdentity
		{
			get
			{
				return this._identity;
			}
			private set
			{
				this._identity = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00017914 File Offset: 0x00015B14
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

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0001794A File Offset: 0x00015B4A
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x00017952 File Offset: 0x00015B52
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
			internal set
			{
				this._name = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0001795B File Offset: 0x00015B5B
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00017963 File Offset: 0x00015B63
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string NamespaceName
		{
			get
			{
				return this._namespace;
			}
			internal set
			{
				this._namespace = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0001796C File Offset: 0x00015B6C
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00017976 File Offset: 0x00015B76
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool Abstract
		{
			get
			{
				return base.GetFlag(MetadataItem.MetadataFlags.IsAbstract);
			}
			internal set
			{
				base.SetFlag(MetadataItem.MetadataFlags.IsAbstract, value);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00017981 File Offset: 0x00015B81
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x0001798C File Offset: 0x00015B8C
		[MetadataProperty(BuiltInTypeKind.EdmType, false)]
		public EdmType BaseType
		{
			get
			{
				return this._baseType;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				for (EdmType edmType = value; edmType != null; edmType = edmType.BaseType)
				{
				}
				this._baseType = value;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x000179B4 File Offset: 0x00015BB4
		public virtual string FullName
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x000179BC File Offset: 0x00015BBC
		internal virtual Type ClrType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000179BF File Offset: 0x00015BBF
		internal override void BuildIdentity(StringBuilder builder)
		{
			if (this.CacheIdentity != null)
			{
				builder.Append(this.CacheIdentity);
				return;
			}
			builder.Append(EdmType.CreateEdmTypeIdentity(this.NamespaceName, this.Name));
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000179F0 File Offset: 0x00015BF0
		internal static string CreateEdmTypeIdentity(string namespaceName, string name)
		{
			string text = string.Empty;
			if (string.Empty != namespaceName)
			{
				text = namespaceName + ".";
			}
			return text + name;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00017A25 File Offset: 0x00015C25
		internal static void Initialize(EdmType edmType, string name, string namespaceName, DataSpace dataSpace, bool isAbstract, EdmType baseType)
		{
			edmType._baseType = baseType;
			edmType._name = name;
			edmType._namespace = namespaceName;
			edmType.DataSpace = dataSpace;
			edmType.Abstract = isAbstract;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00017A4C File Offset: 0x00015C4C
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00017A54 File Offset: 0x00015C54
		public CollectionType GetCollectionType()
		{
			if (this._collectionType == null)
			{
				Interlocked.CompareExchange<CollectionType>(ref this._collectionType, new CollectionType(this), null);
			}
			return this._collectionType;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00017A77 File Offset: 0x00015C77
		internal virtual bool IsSubtypeOf(EdmType otherType)
		{
			return Helper.IsSubtypeOf(this, otherType);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00017A80 File Offset: 0x00015C80
		internal virtual bool IsBaseTypeOf(EdmType otherType)
		{
			return otherType != null && otherType.IsSubtypeOf(this);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00017A8E File Offset: 0x00015C8E
		internal virtual bool IsAssignableFrom(EdmType otherType)
		{
			return Helper.IsAssignableFrom(this, otherType);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00017A98 File Offset: 0x00015C98
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

		// Token: 0x060009EF RID: 2543 RVA: 0x00017AC3 File Offset: 0x00015CC3
		internal virtual IEnumerable<FacetDescription> GetAssociatedFacetDescriptions()
		{
			return MetadataItem.GetGeneralFacetDescriptions();
		}

		// Token: 0x04000813 RID: 2067
		private CollectionType _collectionType;

		// Token: 0x04000814 RID: 2068
		private string _identity;

		// Token: 0x04000815 RID: 2069
		private string _name;

		// Token: 0x04000816 RID: 2070
		private string _namespace;

		// Token: 0x04000817 RID: 2071
		private EdmType _baseType;
	}
}
