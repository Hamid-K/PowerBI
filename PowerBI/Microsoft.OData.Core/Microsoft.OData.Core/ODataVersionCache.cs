using System;

namespace Microsoft.OData
{
	// Token: 0x020000C1 RID: 193
	internal sealed class ODataVersionCache<T>
	{
		// Token: 0x0600086E RID: 2158 RVA: 0x00013EBC File Offset: 0x000120BC
		internal ODataVersionCache(Func<ODataVersion, T> factory)
		{
			this.v4 = new SimpleLazy<T>(() => factory(ODataVersion.V4), true);
			this.v401 = new SimpleLazy<T>(() => factory(ODataVersion.V401), true);
		}

		// Token: 0x170001C0 RID: 448
		internal T this[ODataVersion version]
		{
			get
			{
				if (version == ODataVersion.V4)
				{
					return this.v4.Value;
				}
				if (version != ODataVersion.V401)
				{
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataVersionCache_UnknownVersion));
				}
				return this.v401.Value;
			}
		}

		// Token: 0x04000332 RID: 818
		private readonly SimpleLazy<T> v4;

		// Token: 0x04000333 RID: 819
		private readonly SimpleLazy<T> v401;
	}
}
