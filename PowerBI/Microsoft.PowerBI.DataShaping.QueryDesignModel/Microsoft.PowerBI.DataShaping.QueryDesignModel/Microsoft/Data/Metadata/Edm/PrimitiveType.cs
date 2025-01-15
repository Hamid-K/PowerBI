using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using Microsoft.Data.Common;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000A4 RID: 164
	public sealed class PrimitiveType : SimpleType
	{
		// Token: 0x06000B2F RID: 2863 RVA: 0x0001B517 File Offset: 0x00019717
		internal PrimitiveType()
		{
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0001B51F File Offset: 0x0001971F
		internal PrimitiveType(string name, string namespaceName, DataSpace dataSpace, PrimitiveType baseType, DbProviderManifest providerManifest)
			: base(name, namespaceName, dataSpace)
		{
			EntityUtil.GenericCheckArgumentNull<PrimitiveType>(baseType, "baseType");
			EntityUtil.GenericCheckArgumentNull<DbProviderManifest>(providerManifest, "providerManifest");
			base.BaseType = baseType;
			PrimitiveType.Initialize(this, baseType.PrimitiveTypeKind, false, providerManifest);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0001B55C File Offset: 0x0001975C
		internal PrimitiveType(Type clrType, PrimitiveType baseType, DbProviderManifest providerManifest)
			: this(EntityUtil.GenericCheckArgumentNull<Type>(clrType, "clrType").Name, clrType.Namespace, DataSpace.OSpace, baseType, providerManifest)
		{
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0001B57D File Offset: 0x0001977D
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.PrimitiveType;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0001B581 File Offset: 0x00019781
		internal override Type ClrType
		{
			get
			{
				return this.ClrEquivalentType;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0001B589 File Offset: 0x00019789
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x0001B591 File Offset: 0x00019791
		[MetadataProperty(BuiltInTypeKind.PrimitiveTypeKind, false)]
		public PrimitiveTypeKind PrimitiveTypeKind
		{
			get
			{
				return this._primitiveTypeKind;
			}
			internal set
			{
				this._primitiveTypeKind = value;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0001B59A File Offset: 0x0001979A
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x0001B5A2 File Offset: 0x000197A2
		internal DbProviderManifest ProviderManifest
		{
			get
			{
				return this._providerManifest;
			}
			set
			{
				this._providerManifest = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0001B5AB File Offset: 0x000197AB
		public ReadOnlyCollection<FacetDescription> FacetDescriptions
		{
			get
			{
				return this.ProviderManifest.GetFacetDescriptions(this);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0001B5BC File Offset: 0x000197BC
		public Type ClrEquivalentType
		{
			get
			{
				switch (this.PrimitiveTypeKind)
				{
				case PrimitiveTypeKind.Binary:
					return typeof(byte[]);
				case PrimitiveTypeKind.Boolean:
					return typeof(bool);
				case PrimitiveTypeKind.Byte:
					return typeof(byte);
				case PrimitiveTypeKind.DateTime:
					return typeof(DateTime);
				case PrimitiveTypeKind.Decimal:
					return typeof(decimal);
				case PrimitiveTypeKind.Double:
					return typeof(double);
				case PrimitiveTypeKind.Guid:
					return typeof(Guid);
				case PrimitiveTypeKind.Single:
					return typeof(float);
				case PrimitiveTypeKind.SByte:
					return typeof(sbyte);
				case PrimitiveTypeKind.Int16:
					return typeof(short);
				case PrimitiveTypeKind.Int32:
					return typeof(int);
				case PrimitiveTypeKind.Int64:
					return typeof(long);
				case PrimitiveTypeKind.String:
					return typeof(string);
				case PrimitiveTypeKind.Time:
					return typeof(TimeSpan);
				case PrimitiveTypeKind.DateTimeOffset:
					return typeof(DateTimeOffset);
				default:
					return null;
				}
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0001B6BD File Offset: 0x000198BD
		internal override IEnumerable<FacetDescription> GetAssociatedFacetDescriptions()
		{
			return base.GetAssociatedFacetDescriptions().Concat(this.FacetDescriptions);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0001B6D0 File Offset: 0x000198D0
		internal static void Initialize(PrimitiveType primitiveType, PrimitiveTypeKind primitiveTypeKind, bool isDefaultType, DbProviderManifest providerManifest)
		{
			primitiveType._primitiveTypeKind = primitiveTypeKind;
			primitiveType._providerManifest = providerManifest;
		}

		// Token: 0x04000886 RID: 2182
		private PrimitiveTypeKind _primitiveTypeKind;

		// Token: 0x04000887 RID: 2183
		private DbProviderManifest _providerManifest;
	}
}
