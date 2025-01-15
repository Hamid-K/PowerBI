using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000112 RID: 274
	internal class PropertyEntryMetadata : MemberEntryMetadata
	{
		// Token: 0x06001336 RID: 4918 RVA: 0x000325EA File Offset: 0x000307EA
		public PropertyEntryMetadata(Type declaringType, Type propertyType, string propertyName, bool isMapped, bool isComplex)
			: base(declaringType, propertyType, propertyName)
		{
			this._isMapped = isMapped;
			this._isComplex = isComplex;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00032608 File Offset: 0x00030808
		public static PropertyEntryMetadata ValidateNameAndGetMetadata(InternalContext internalContext, Type declaringType, Type requestedType, string propertyName)
		{
			Type type;
			DbHelpers.GetPropertyTypes(declaringType).TryGetValue(propertyName, out type);
			MetadataWorkspace metadataWorkspace = internalContext.ObjectContext.MetadataWorkspace;
			StructuralType item = metadataWorkspace.GetItem<StructuralType>(declaringType.FullNameWithNesting(), DataSpace.OSpace);
			bool flag = false;
			bool flag2 = false;
			EdmMember edmMember;
			item.Members.TryGetValue(propertyName, false, out edmMember);
			if (edmMember != null)
			{
				EdmProperty edmProperty = edmMember as EdmProperty;
				if (edmProperty == null)
				{
					return null;
				}
				if (type == null)
				{
					PrimitiveType primitiveType = edmProperty.TypeUsage.EdmType as PrimitiveType;
					if (primitiveType != null)
					{
						type = primitiveType.ClrEquivalentType;
					}
					else
					{
						type = ((ObjectItemCollection)metadataWorkspace.GetItemCollection(DataSpace.OSpace)).GetClrType((StructuralType)edmProperty.TypeUsage.EdmType);
					}
				}
				flag = true;
				flag2 = edmProperty.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.ComplexType;
			}
			else
			{
				IDictionary<string, Func<object, object>> propertyGetters = DbHelpers.GetPropertyGetters(declaringType);
				IDictionary<string, Action<object, object>> propertySetters = DbHelpers.GetPropertySetters(declaringType);
				if (!propertyGetters.ContainsKey(propertyName) && !propertySetters.ContainsKey(propertyName))
				{
					return null;
				}
			}
			if (!requestedType.IsAssignableFrom(type))
			{
				throw Error.DbEntityEntry_WrongGenericForProp(propertyName, declaringType.Name, requestedType.Name, type.Name);
			}
			return new PropertyEntryMetadata(declaringType, type, propertyName, flag, flag2);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00032717 File Offset: 0x00030917
		public override InternalMemberEntry CreateMemberEntry(InternalEntityEntry internalEntityEntry, InternalPropertyEntry parentPropertyEntry)
		{
			if (parentPropertyEntry != null)
			{
				return new InternalNestedPropertyEntry(parentPropertyEntry, this);
			}
			return new InternalEntityPropertyEntry(internalEntityEntry, this);
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x0003272B File Offset: 0x0003092B
		public bool IsComplex
		{
			get
			{
				return this._isComplex;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x00032733 File Offset: 0x00030933
		public override MemberEntryType MemberEntryType
		{
			get
			{
				if (!this._isComplex)
				{
					return MemberEntryType.ScalarProperty;
				}
				return MemberEntryType.ComplexProperty;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x00032740 File Offset: 0x00030940
		public bool IsMapped
		{
			get
			{
				return this._isMapped;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x00032748 File Offset: 0x00030948
		public override Type MemberType
		{
			get
			{
				return base.ElementType;
			}
		}

		// Token: 0x0400094D RID: 2381
		private readonly bool _isMapped;

		// Token: 0x0400094E RID: 2382
		private readonly bool _isComplex;
	}
}
