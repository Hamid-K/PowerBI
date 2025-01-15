using System;

namespace Microsoft.OData
{
	// Token: 0x020000A3 RID: 163
	internal sealed class ODataVersionCache<T>
	{
		// Token: 0x06000617 RID: 1559 RVA: 0x00010640 File Offset: 0x0000E840
		internal ODataVersionCache(Func<ODataVersion, T> factory)
		{
			this.v3 = new SimpleLazy<T>(() => factory.Invoke(ODataVersion.V4), true);
		}

		// Token: 0x1700017C RID: 380
		internal T this[ODataVersion version]
		{
			get
			{
				if (version == ODataVersion.V4)
				{
					return this.v3.Value;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataVersionCache_UnknownVersion));
			}
		}

		// Token: 0x040002D3 RID: 723
		private readonly SimpleLazy<T> v3;
	}
}
