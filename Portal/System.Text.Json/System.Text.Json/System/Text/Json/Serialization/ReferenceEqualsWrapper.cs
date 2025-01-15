using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000085 RID: 133
	internal readonly struct ReferenceEqualsWrapper : IEquatable<ReferenceEqualsWrapper>
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x00024CFC File Offset: 0x00022EFC
		public ReferenceEqualsWrapper(object obj)
		{
			this._object = obj;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00024D08 File Offset: 0x00022F08
		public override bool Equals([NotNullWhen(true)] object obj)
		{
			if (obj is ReferenceEqualsWrapper)
			{
				ReferenceEqualsWrapper referenceEqualsWrapper = (ReferenceEqualsWrapper)obj;
				return this.Equals(referenceEqualsWrapper);
			}
			return false;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00024D2D File Offset: 0x00022F2D
		public bool Equals(ReferenceEqualsWrapper obj)
		{
			return this._object == obj._object;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00024D3D File Offset: 0x00022F3D
		public override int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this._object);
		}

		// Token: 0x040002E9 RID: 745
		private readonly object _object;
	}
}
