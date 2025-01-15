using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004BD RID: 1213
	public sealed class EnumMember : MetadataItem
	{
		// Token: 0x06003BF9 RID: 15353 RVA: 0x000C6DEF File Offset: 0x000C4FEF
		internal EnumMember(string name, object value)
			: base(MetadataItem.MetadataFlags.Readonly)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._value = value;
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x000C6E12 File Offset: 0x000C5012
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EnumMember;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06003BFB RID: 15355 RVA: 0x000C6E16 File Offset: 0x000C5016
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06003BFC RID: 15356 RVA: 0x000C6E1E File Offset: 0x000C501E
		[MetadataProperty(BuiltInTypeKind.PrimitiveType, false)]
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x000C6E26 File Offset: 0x000C5026
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x000C6E2E File Offset: 0x000C502E
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x000C6E36 File Offset: 0x000C5036
		[CLSCompliant(false)]
		public static EnumMember Create(string name, sbyte value, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			return EnumMember.CreateInternal(name, value, metadataProperties);
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x000C6E51 File Offset: 0x000C5051
		public static EnumMember Create(string name, byte value, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			return EnumMember.CreateInternal(name, value, metadataProperties);
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x000C6E6C File Offset: 0x000C506C
		public static EnumMember Create(string name, short value, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			return EnumMember.CreateInternal(name, value, metadataProperties);
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x000C6E87 File Offset: 0x000C5087
		public static EnumMember Create(string name, int value, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			return EnumMember.CreateInternal(name, value, metadataProperties);
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x000C6EA2 File Offset: 0x000C50A2
		public static EnumMember Create(string name, long value, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			return EnumMember.CreateInternal(name, value, metadataProperties);
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x000C6EC0 File Offset: 0x000C50C0
		private static EnumMember CreateInternal(string name, object value, IEnumerable<MetadataProperty> metadataProperties)
		{
			EnumMember enumMember = new EnumMember(name, value);
			if (metadataProperties != null)
			{
				enumMember.AddMetadataProperties(metadataProperties);
			}
			enumMember.SetReadOnly();
			return enumMember;
		}

		// Token: 0x040014A7 RID: 5287
		private readonly string _name;

		// Token: 0x040014A8 RID: 5288
		private readonly object _value;
	}
}
