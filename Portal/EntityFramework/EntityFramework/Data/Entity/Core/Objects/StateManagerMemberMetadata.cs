using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000431 RID: 1073
	internal class StateManagerMemberMetadata
	{
		// Token: 0x06003427 RID: 13351 RVA: 0x000A864B File Offset: 0x000A684B
		internal StateManagerMemberMetadata()
		{
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000A8654 File Offset: 0x000A6854
		internal StateManagerMemberMetadata(ObjectPropertyMapping memberMap, EdmProperty memberMetadata, bool isPartOfKey)
		{
			this._clrProperty = memberMap.ClrProperty;
			this._edmProperty = memberMetadata;
			this._isPartOfKey = isPartOfKey;
			this._isComplexType = Helper.IsEntityType(this._edmProperty.TypeUsage.EdmType) || Helper.IsComplexType(this._edmProperty.TypeUsage.EdmType);
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06003429 RID: 13353 RVA: 0x000A86B6 File Offset: 0x000A68B6
		internal string CLayerName
		{
			get
			{
				return this._edmProperty.Name;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x000A86C3 File Offset: 0x000A68C3
		internal Type ClrType
		{
			get
			{
				return this._clrProperty.TypeUsage.EdmType.ClrType;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x000A86DA File Offset: 0x000A68DA
		internal virtual bool IsComplex
		{
			get
			{
				return this._isComplexType;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600342C RID: 13356 RVA: 0x000A86E2 File Offset: 0x000A68E2
		internal virtual EdmProperty CdmMetadata
		{
			get
			{
				return this._edmProperty;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x000A86EA File Offset: 0x000A68EA
		internal EdmProperty ClrMetadata
		{
			get
			{
				return this._clrProperty;
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x0600342E RID: 13358 RVA: 0x000A86F2 File Offset: 0x000A68F2
		internal bool IsPartOfKey
		{
			get
			{
				return this._isPartOfKey;
			}
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x000A86FA File Offset: 0x000A68FA
		public virtual object GetValue(object userObject)
		{
			return DelegateFactory.GetValue(this._clrProperty, userObject);
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x000A8708 File Offset: 0x000A6908
		public void SetValue(object userObject, object value)
		{
			if (DBNull.Value == value)
			{
				value = null;
			}
			if (this.IsComplex && value == null)
			{
				throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(this.CLayerName));
			}
			DelegateFactory.SetValue(this._clrProperty, userObject, value);
		}

		// Token: 0x040010D5 RID: 4309
		private readonly EdmProperty _clrProperty;

		// Token: 0x040010D6 RID: 4310
		private readonly EdmProperty _edmProperty;

		// Token: 0x040010D7 RID: 4311
		private readonly bool _isPartOfKey;

		// Token: 0x040010D8 RID: 4312
		private readonly bool _isComplexType;
	}
}
