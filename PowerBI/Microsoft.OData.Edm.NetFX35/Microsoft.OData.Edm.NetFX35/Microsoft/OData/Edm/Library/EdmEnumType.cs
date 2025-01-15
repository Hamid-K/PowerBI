using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001B7 RID: 439
	public class EdmEnumType : EdmType, IEdmEnumType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x00019265 File Offset: 0x00017465
		public EdmEnumType(string namespaceName, string name)
			: this(namespaceName, name, false)
		{
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00019270 File Offset: 0x00017470
		public EdmEnumType(string namespaceName, string name, bool isFlags)
			: this(namespaceName, name, EdmPrimitiveTypeKind.Int32, isFlags)
		{
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001927D File Offset: 0x0001747D
		public EdmEnumType(string namespaceName, string name, EdmPrimitiveTypeKind underlyingType, bool isFlags)
			: this(namespaceName, name, EdmCoreModel.Instance.GetPrimitiveType(underlyingType), isFlags)
		{
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00019294 File Offset: 0x00017494
		public EdmEnumType(string namespaceName, string name, IEdmPrimitiveType underlyingType, bool isFlags)
		{
			EdmUtil.CheckArgumentNull<IEdmPrimitiveType>(underlyingType, "underlyingType");
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.underlyingType = underlyingType;
			this.name = name;
			this.namespaceName = namespaceName;
			this.isFlags = isFlags;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x000192F3 File Offset: 0x000174F3
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x000192F6 File Offset: 0x000174F6
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x000192F9 File Offset: 0x000174F9
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00019301 File Offset: 0x00017501
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00019309 File Offset: 0x00017509
		public IEdmPrimitiveType UnderlyingType
		{
			get
			{
				return this.underlyingType;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00019311 File Offset: 0x00017511
		public virtual IEnumerable<IEdmEnumMember> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00019319 File Offset: 0x00017519
		public bool IsFlags
		{
			get
			{
				return this.isFlags;
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00019321 File Offset: 0x00017521
		public void AddMember(IEdmEnumMember member)
		{
			this.members.Add(member);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00019330 File Offset: 0x00017530
		public EdmEnumMember AddMember(string name, IEdmPrimitiveValue value)
		{
			EdmEnumMember edmEnumMember = new EdmEnumMember(this, name, value);
			this.AddMember(edmEnumMember);
			return edmEnumMember;
		}

		// Token: 0x04000490 RID: 1168
		private readonly IEdmPrimitiveType underlyingType;

		// Token: 0x04000491 RID: 1169
		private readonly string namespaceName;

		// Token: 0x04000492 RID: 1170
		private readonly string name;

		// Token: 0x04000493 RID: 1171
		private readonly bool isFlags;

		// Token: 0x04000494 RID: 1172
		private readonly List<IEdmEnumMember> members = new List<IEdmEnumMember>();
	}
}
