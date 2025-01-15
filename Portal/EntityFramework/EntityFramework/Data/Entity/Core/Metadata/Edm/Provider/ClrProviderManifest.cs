using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm.Provider
{
	// Token: 0x02000518 RID: 1304
	internal class ClrProviderManifest : DbProviderManifest
	{
		// Token: 0x06004035 RID: 16437 RVA: 0x000D6D6F File Offset: 0x000D4F6F
		private ClrProviderManifest()
		{
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06004036 RID: 16438 RVA: 0x000D6D77 File Offset: 0x000D4F77
		internal static ClrProviderManifest Instance
		{
			get
			{
				return ClrProviderManifest._instance;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x000D6D7E File Offset: 0x000D4F7E
		public override string NamespaceName
		{
			get
			{
				return "System";
			}
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x000D6D88 File Offset: 0x000D4F88
		internal bool TryGetPrimitiveType(Type clrType, out PrimitiveType primitiveType)
		{
			primitiveType = null;
			PrimitiveTypeKind primitiveTypeKind;
			if (ClrProviderManifest.TryGetPrimitiveTypeKind(clrType, out primitiveTypeKind))
			{
				this.InitializePrimitiveTypes();
				primitiveType = this._primitiveTypesArray[(int)primitiveTypeKind];
				return true;
			}
			return false;
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x000D6DBC File Offset: 0x000D4FBC
		internal static bool TryGetPrimitiveTypeKind(Type clrType, out PrimitiveTypeKind resolvedPrimitiveTypeKind)
		{
			PrimitiveTypeKind? primitiveTypeKind = null;
			if (!clrType.IsEnum())
			{
				switch (Type.GetTypeCode(clrType))
				{
				case TypeCode.Object:
					if (typeof(byte[]) == clrType)
					{
						primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Binary);
					}
					else if (typeof(DateTimeOffset) == clrType)
					{
						primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.DateTimeOffset);
					}
					else if (typeof(DbGeography).IsAssignableFrom(clrType))
					{
						primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Geography);
					}
					else if (typeof(DbGeometry).IsAssignableFrom(clrType))
					{
						primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Geometry);
					}
					else if (typeof(Guid) == clrType)
					{
						primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Guid);
					}
					else if (typeof(HierarchyId) == clrType)
					{
						primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.HierarchyId);
					}
					else if (typeof(TimeSpan) == clrType)
					{
						primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Time);
					}
					break;
				case TypeCode.Boolean:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Boolean);
					break;
				case TypeCode.SByte:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.SByte);
					break;
				case TypeCode.Byte:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Byte);
					break;
				case TypeCode.Int16:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Int16);
					break;
				case TypeCode.Int32:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Int32);
					break;
				case TypeCode.Int64:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Int64);
					break;
				case TypeCode.Single:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Single);
					break;
				case TypeCode.Double:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Double);
					break;
				case TypeCode.Decimal:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Decimal);
					break;
				case TypeCode.DateTime:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.DateTime);
					break;
				case TypeCode.String:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.String);
					break;
				}
			}
			if (primitiveTypeKind != null)
			{
				resolvedPrimitiveTypeKind = primitiveTypeKind.Value;
				return true;
			}
			resolvedPrimitiveTypeKind = PrimitiveTypeKind.Binary;
			return false;
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x000D6FB0 File Offset: 0x000D51B0
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			return Helper.EmptyEdmFunctionReadOnlyCollection;
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x000D6FB8 File Offset: 0x000D51B8
		public override ReadOnlyCollection<FacetDescription> GetFacetDescriptions(EdmType type)
		{
			if (Helper.IsPrimitiveType(type) && type.DataSpace == DataSpace.OSpace)
			{
				PrimitiveType primitiveType = (PrimitiveType)type.BaseType;
				return primitiveType.ProviderManifest.GetFacetDescriptions(primitiveType);
			}
			return Helper.EmptyFacetDescriptionEnumerable;
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x000D6FF4 File Offset: 0x000D51F4
		private void InitializePrimitiveTypes()
		{
			if (this._primitiveTypes != null)
			{
				return;
			}
			PrimitiveType[] array = new PrimitiveType[32];
			array[0] = this.CreatePrimitiveType(typeof(byte[]), PrimitiveTypeKind.Binary);
			array[1] = this.CreatePrimitiveType(typeof(bool), PrimitiveTypeKind.Boolean);
			array[2] = this.CreatePrimitiveType(typeof(byte), PrimitiveTypeKind.Byte);
			array[3] = this.CreatePrimitiveType(typeof(DateTime), PrimitiveTypeKind.DateTime);
			array[13] = this.CreatePrimitiveType(typeof(TimeSpan), PrimitiveTypeKind.Time);
			array[14] = this.CreatePrimitiveType(typeof(DateTimeOffset), PrimitiveTypeKind.DateTimeOffset);
			array[4] = this.CreatePrimitiveType(typeof(decimal), PrimitiveTypeKind.Decimal);
			array[5] = this.CreatePrimitiveType(typeof(double), PrimitiveTypeKind.Double);
			array[16] = this.CreatePrimitiveType(typeof(DbGeography), PrimitiveTypeKind.Geography);
			array[15] = this.CreatePrimitiveType(typeof(DbGeometry), PrimitiveTypeKind.Geometry);
			array[6] = this.CreatePrimitiveType(typeof(Guid), PrimitiveTypeKind.Guid);
			array[31] = this.CreatePrimitiveType(typeof(HierarchyId), PrimitiveTypeKind.HierarchyId);
			array[9] = this.CreatePrimitiveType(typeof(short), PrimitiveTypeKind.Int16);
			array[10] = this.CreatePrimitiveType(typeof(int), PrimitiveTypeKind.Int32);
			array[11] = this.CreatePrimitiveType(typeof(long), PrimitiveTypeKind.Int64);
			array[8] = this.CreatePrimitiveType(typeof(sbyte), PrimitiveTypeKind.SByte);
			array[7] = this.CreatePrimitiveType(typeof(float), PrimitiveTypeKind.Single);
			array[12] = this.CreatePrimitiveType(typeof(string), PrimitiveTypeKind.String);
			ReadOnlyCollection<PrimitiveType> readOnlyCollection = new ReadOnlyCollection<PrimitiveType>(array);
			ReadOnlyCollection<PrimitiveType> readOnlyCollection2 = new ReadOnlyCollection<PrimitiveType>(array.Where((PrimitiveType t) => t != null).ToList<PrimitiveType>());
			Interlocked.CompareExchange<ReadOnlyCollection<PrimitiveType>>(ref this._primitiveTypesArray, readOnlyCollection, null);
			Interlocked.CompareExchange<ReadOnlyCollection<PrimitiveType>>(ref this._primitiveTypes, readOnlyCollection2, null);
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x000D71E0 File Offset: 0x000D53E0
		private PrimitiveType CreatePrimitiveType(Type clrType, PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveType primitiveType = MetadataItem.EdmProviderManifest.GetPrimitiveType(primitiveTypeKind);
			PrimitiveType primitiveType2 = new PrimitiveType(clrType, primitiveType, this);
			primitiveType2.SetReadOnly();
			return primitiveType2;
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x000D7207 File Offset: 0x000D5407
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			this.InitializePrimitiveTypes();
			return this._primitiveTypes;
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x000D7215 File Offset: 0x000D5415
		public override TypeUsage GetEdmType(TypeUsage storeType)
		{
			Check.NotNull<TypeUsage>(storeType, "storeType");
			throw new NotImplementedException();
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x000D7228 File Offset: 0x000D5428
		public override TypeUsage GetStoreType(TypeUsage edmType)
		{
			Check.NotNull<TypeUsage>(edmType, "edmType");
			throw new NotImplementedException();
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x000D723B File Offset: 0x000D543B
		protected override XmlReader GetDbInformation(string informationType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400165E RID: 5726
		private const int s_PrimitiveTypeCount = 32;

		// Token: 0x0400165F RID: 5727
		private ReadOnlyCollection<PrimitiveType> _primitiveTypesArray;

		// Token: 0x04001660 RID: 5728
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x04001661 RID: 5729
		private static readonly ClrProviderManifest _instance = new ClrProviderManifest();
	}
}
