using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004BE RID: 1214
	public class EnumType : SimpleType
	{
		// Token: 0x06003C05 RID: 15365 RVA: 0x000C6EE6 File Offset: 0x000C50E6
		internal EnumType()
		{
			this._underlyingType = PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int32);
			this._isFlags = false;
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x000C6F12 File Offset: 0x000C5112
		internal EnumType(string name, string namespaceName, PrimitiveType underlyingType, bool isFlags, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
			this._isFlags = isFlags;
			this._underlyingType = underlyingType;
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x000C6F40 File Offset: 0x000C5140
		internal EnumType(Type clrType)
			: base(clrType.Name, clrType.NestingNamespace() ?? string.Empty, DataSpace.OSpace)
		{
			ClrProviderManifest.Instance.TryGetPrimitiveType(clrType.GetEnumUnderlyingType(), out this._underlyingType);
			this._isFlags = clrType.GetCustomAttributes(false).Any<FlagsAttribute>();
			foreach (string text in Enum.GetNames(clrType))
			{
				this.AddMember(new EnumMember(text, Convert.ChangeType(Enum.Parse(clrType, text), clrType.GetEnumUnderlyingType(), CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06003C08 RID: 15368 RVA: 0x000C6FDE File Offset: 0x000C51DE
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EnumType;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06003C09 RID: 15369 RVA: 0x000C6FE2 File Offset: 0x000C51E2
		[MetadataProperty(BuiltInTypeKind.EnumMember, true)]
		public ReadOnlyMetadataCollection<EnumMember> Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06003C0A RID: 15370 RVA: 0x000C6FEA File Offset: 0x000C51EA
		// (set) Token: 0x06003C0B RID: 15371 RVA: 0x000C6FF2 File Offset: 0x000C51F2
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool IsFlags
		{
			get
			{
				return this._isFlags;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this._isFlags = value;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06003C0C RID: 15372 RVA: 0x000C7001 File Offset: 0x000C5201
		// (set) Token: 0x06003C0D RID: 15373 RVA: 0x000C7009 File Offset: 0x000C5209
		[MetadataProperty(BuiltInTypeKind.PrimitiveType, false)]
		public PrimitiveType UnderlyingType
		{
			get
			{
				return this._underlyingType;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this._underlyingType = value;
			}
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x000C7018 File Offset: 0x000C5218
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.Members.Source.SetReadOnly();
			}
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x000C7039 File Offset: 0x000C5239
		internal void AddMember(EnumMember enumMember)
		{
			this.Members.Source.Add(enumMember);
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x000C704C File Offset: 0x000C524C
		public static EnumType Create(string name, string namespaceName, PrimitiveType underlyingType, bool isFlags, IEnumerable<EnumMember> members, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			Check.NotNull<PrimitiveType>(underlyingType, "underlyingType");
			if (!Helper.IsSupportedEnumUnderlyingType(underlyingType.PrimitiveTypeKind))
			{
				throw new ArgumentException(Strings.InvalidEnumUnderlyingType, "underlyingType");
			}
			EnumType enumType = new EnumType(name, namespaceName, underlyingType, isFlags, DataSpace.CSpace);
			if (members != null)
			{
				foreach (EnumMember enumMember in members)
				{
					if (!Helper.IsEnumMemberValueInRange(underlyingType.PrimitiveTypeKind, Convert.ToInt64(enumMember.Value, CultureInfo.InvariantCulture)))
					{
						throw new ArgumentException(Strings.EnumMemberValueOutOfItsUnderylingTypeRange(enumMember.Value, enumMember.Name, underlyingType.Name), "members");
					}
					enumType.AddMember(enumMember);
				}
			}
			if (metadataProperties != null)
			{
				enumType.AddMetadataProperties(metadataProperties);
			}
			enumType.SetReadOnly();
			return enumType;
		}

		// Token: 0x040014A9 RID: 5289
		private readonly ReadOnlyMetadataCollection<EnumMember> _members = new ReadOnlyMetadataCollection<EnumMember>(new MetadataCollection<EnumMember>());

		// Token: 0x040014AA RID: 5290
		private PrimitiveType _underlyingType;

		// Token: 0x040014AB RID: 5291
		private bool _isFlags;
	}
}
