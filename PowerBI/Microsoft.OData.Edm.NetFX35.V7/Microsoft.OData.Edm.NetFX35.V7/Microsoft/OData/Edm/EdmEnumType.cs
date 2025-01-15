using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200005D RID: 93
	public class EdmEnumType : EdmType, IEdmEnumType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000ADBE File Offset: 0x00008FBE
		public EdmEnumType(string namespaceName, string name)
			: this(namespaceName, name, false)
		{
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000ADC9 File Offset: 0x00008FC9
		public EdmEnumType(string namespaceName, string name, bool isFlags)
			: this(namespaceName, name, EdmPrimitiveTypeKind.Int32, isFlags)
		{
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000ADD6 File Offset: 0x00008FD6
		public EdmEnumType(string namespaceName, string name, EdmPrimitiveTypeKind underlyingType, bool isFlags)
			: this(namespaceName, name, EdmCoreModel.Instance.GetPrimitiveType(underlyingType), isFlags)
		{
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000ADF0 File Offset: 0x00008FF0
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000092ED File Offset: 0x000074ED
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000AE4F File Offset: 0x0000904F
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000AE57 File Offset: 0x00009057
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000AE5F File Offset: 0x0000905F
		public IEdmPrimitiveType UnderlyingType
		{
			get
			{
				return this.underlyingType;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000AE67 File Offset: 0x00009067
		public virtual IEnumerable<IEdmEnumMember> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000AE6F File Offset: 0x0000906F
		public bool IsFlags
		{
			get
			{
				return this.isFlags;
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000AE77 File Offset: 0x00009077
		public void AddMember(IEdmEnumMember member)
		{
			this.members.Add(member);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000AE88 File Offset: 0x00009088
		public EdmEnumMember AddMember(string name, IEdmEnumMemberValue value)
		{
			EdmEnumMember edmEnumMember = new EdmEnumMember(this, name, value);
			this.AddMember(edmEnumMember);
			return edmEnumMember;
		}

		// Token: 0x040000BD RID: 189
		private readonly IEdmPrimitiveType underlyingType;

		// Token: 0x040000BE RID: 190
		private readonly string namespaceName;

		// Token: 0x040000BF RID: 191
		private readonly string name;

		// Token: 0x040000C0 RID: 192
		private readonly bool isFlags;

		// Token: 0x040000C1 RID: 193
		private readonly List<IEdmEnumMember> members = new List<IEdmEnumMember>();
	}
}
