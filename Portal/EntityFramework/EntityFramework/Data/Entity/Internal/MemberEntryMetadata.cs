using System;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200010E RID: 270
	internal abstract class MemberEntryMetadata
	{
		// Token: 0x06001329 RID: 4905 RVA: 0x0003253B File Offset: 0x0003073B
		protected MemberEntryMetadata(Type declaringType, Type elementType, string memberName)
		{
			this._declaringType = declaringType;
			this._elementType = elementType;
			this._memberName = memberName;
		}

		// Token: 0x0600132A RID: 4906
		public abstract InternalMemberEntry CreateMemberEntry(InternalEntityEntry internalEntityEntry, InternalPropertyEntry parentPropertyEntry);

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600132B RID: 4907
		public abstract MemberEntryType MemberEntryType { get; }

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x00032558 File Offset: 0x00030758
		public string MemberName
		{
			get
			{
				return this._memberName;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x00032560 File Offset: 0x00030760
		public Type DeclaringType
		{
			get
			{
				return this._declaringType;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00032568 File Offset: 0x00030768
		public Type ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600132F RID: 4911
		public abstract Type MemberType { get; }

		// Token: 0x04000943 RID: 2371
		private readonly Type _declaringType;

		// Token: 0x04000944 RID: 2372
		private readonly Type _elementType;

		// Token: 0x04000945 RID: 2373
		private readonly string _memberName;
	}
}
