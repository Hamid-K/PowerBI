using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200015D RID: 349
	internal sealed class ODataVersionCache<T>
	{
		// Token: 0x0600094B RID: 2379 RVA: 0x0001CFDC File Offset: 0x0001B1DC
		internal ODataVersionCache(Func<ODataVersion, T> factory)
		{
			this.v1 = new SimpleLazy<T>(() => factory.Invoke(ODataVersion.V1), true);
			this.v2 = new SimpleLazy<T>(() => factory.Invoke(ODataVersion.V2), true);
			this.v3 = new SimpleLazy<T>(() => factory.Invoke(ODataVersion.V3), true);
		}

		// Token: 0x1700024E RID: 590
		internal T this[ODataVersion version]
		{
			get
			{
				switch (version)
				{
				case ODataVersion.V1:
					return this.v1.Value;
				case ODataVersion.V2:
					return this.v2.Value;
				case ODataVersion.V3:
					return this.v3.Value;
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataVersionCache_UnknownVersion));
				}
			}
		}

		// Token: 0x0400037E RID: 894
		private readonly SimpleLazy<T> v1;

		// Token: 0x0400037F RID: 895
		private readonly SimpleLazy<T> v2;

		// Token: 0x04000380 RID: 896
		private readonly SimpleLazy<T> v3;
	}
}
