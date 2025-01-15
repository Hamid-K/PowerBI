using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000171 RID: 369
	public struct EdmPropertyRef : IEquatable<EdmPropertyRef>, ICheckable
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x0001365C File Offset: 0x0001185C
		public EdmPropertyRef(string entitySetReferenceName, string propertyReferenceName)
		{
			Contract.CheckValue<string>(entitySetReferenceName, "entitySetReferenceName");
			Contract.CheckValue<string>(propertyReferenceName, "propertyReferenceName");
			this._entitySetReferenceName = entitySetReferenceName;
			this._propertyReferenceName = propertyReferenceName;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00013682 File Offset: 0x00011882
		public EdmPropertyRef(string entityContainerName, string entitySetReferenceName, string propertyReferenceName)
		{
			Contract.CheckNonEmpty(entityContainerName, "entityContainerName");
			Contract.CheckNonEmpty(entitySetReferenceName, "entitySetReferenceName");
			Contract.CheckNonEmpty(propertyReferenceName, "propertyReferenceName");
			this._entitySetReferenceName = entitySetReferenceName;
			this._propertyReferenceName = propertyReferenceName;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x000136B3 File Offset: 0x000118B3
		public string EntityContainerName
		{
			get
			{
				return "Sandbox";
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x000136BA File Offset: 0x000118BA
		public string EntitySetReferenceName
		{
			get
			{
				return this._entitySetReferenceName;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x000136C2 File Offset: 0x000118C2
		public string PropertyReferenceName
		{
			get
			{
				return this._propertyReferenceName;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x000136CA File Offset: 0x000118CA
		public bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this._entitySetReferenceName) && !string.IsNullOrEmpty(this._propertyReferenceName);
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x000136E9 File Offset: 0x000118E9
		public string GetQualifiedName()
		{
			return StringUtil.FormatInvariant("{0}.{1}", StringUtil.EscapeIdentifier(this._entitySetReferenceName), StringUtil.EscapeIdentifier(this._propertyReferenceName));
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001370B File Offset: 0x0001190B
		public override bool Equals(object obj)
		{
			return obj is EdmPropertyRef && this.Equals((EdmPropertyRef)obj);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00013723 File Offset: 0x00011923
		public bool Equals(EdmPropertyRef other)
		{
			return EdmNameComparer.Instance.Equals(this._propertyReferenceName, other._propertyReferenceName) && EdmNameComparer.Instance.Equals(this._entitySetReferenceName, other._entitySetReferenceName);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00013755 File Offset: 0x00011955
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<string>(this._entitySetReferenceName, EdmNameComparer.Instance), Hashing.GetHashCode<string>(this._propertyReferenceName, EdmNameComparer.Instance));
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001377C File Offset: 0x0001197C
		public override string ToString()
		{
			return this.GetQualifiedName();
		}

		// Token: 0x04000565 RID: 1381
		public static readonly EdmPropertyRef Empty = new EdmPropertyRef(string.Empty, string.Empty);

		// Token: 0x04000566 RID: 1382
		private readonly string _entitySetReferenceName;

		// Token: 0x04000567 RID: 1383
		private readonly string _propertyReferenceName;
	}
}
