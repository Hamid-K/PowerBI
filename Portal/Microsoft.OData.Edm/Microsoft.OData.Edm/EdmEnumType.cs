using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200007B RID: 123
	public class EdmEnumType : EdmType, IEdmEnumType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmFullNamedElement
	{
		// Token: 0x06000264 RID: 612 RVA: 0x00005F9A File Offset: 0x0000419A
		public EdmEnumType(string namespaceName, string name)
			: this(namespaceName, name, false)
		{
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00005FA5 File Offset: 0x000041A5
		public EdmEnumType(string namespaceName, string name, bool isFlags)
			: this(namespaceName, name, EdmPrimitiveTypeKind.Int32, isFlags)
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00005FB2 File Offset: 0x000041B2
		public EdmEnumType(string namespaceName, string name, EdmPrimitiveTypeKind underlyingType, bool isFlags)
			: this(namespaceName, name, EdmCoreModel.Instance.GetPrimitiveType(underlyingType), isFlags)
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00005FCC File Offset: 0x000041CC
		public EdmEnumType(string namespaceName, string name, IEdmPrimitiveType underlyingType, bool isFlags)
		{
			EdmUtil.CheckArgumentNull<IEdmPrimitiveType>(underlyingType, "underlyingType");
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.underlyingType = underlyingType;
			this.name = name;
			this.namespaceName = namespaceName;
			this.isFlags = isFlags;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(this.namespaceName, this.name);
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00003A59 File Offset: 0x00001C59
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00006042 File Offset: 0x00004242
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000604A File Offset: 0x0000424A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00006052 File Offset: 0x00004252
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000605A File Offset: 0x0000425A
		public IEdmPrimitiveType UnderlyingType
		{
			get
			{
				return this.underlyingType;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00006062 File Offset: 0x00004262
		public virtual IEnumerable<IEdmEnumMember> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000606A File Offset: 0x0000426A
		public bool IsFlags
		{
			get
			{
				return this.isFlags;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00006072 File Offset: 0x00004272
		public void AddMember(IEdmEnumMember member)
		{
			this.members.Add(member);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00006080 File Offset: 0x00004280
		public EdmEnumMember AddMember(string name, IEdmEnumMemberValue value)
		{
			EdmEnumMember edmEnumMember = new EdmEnumMember(this, name, value);
			this.AddMember(edmEnumMember);
			return edmEnumMember;
		}

		// Token: 0x040000D4 RID: 212
		private readonly IEdmPrimitiveType underlyingType;

		// Token: 0x040000D5 RID: 213
		private readonly string namespaceName;

		// Token: 0x040000D6 RID: 214
		private readonly string name;

		// Token: 0x040000D7 RID: 215
		private readonly string fullName;

		// Token: 0x040000D8 RID: 216
		private readonly bool isFlags;

		// Token: 0x040000D9 RID: 217
		private readonly List<IEdmEnumMember> members = new List<IEdmEnumMember>();
	}
}
