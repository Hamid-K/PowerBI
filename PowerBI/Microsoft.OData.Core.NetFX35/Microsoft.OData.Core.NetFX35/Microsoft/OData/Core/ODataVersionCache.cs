using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020001A8 RID: 424
	internal sealed class ODataVersionCache<T>
	{
		// Token: 0x06000FCF RID: 4047 RVA: 0x00036474 File Offset: 0x00034674
		internal ODataVersionCache(Func<ODataVersion, T> factory)
		{
			this.v3 = new SimpleLazy<T>(() => factory.Invoke(ODataVersion.V4), true);
		}

		// Token: 0x1700037B RID: 891
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

		// Token: 0x040006F3 RID: 1779
		private readonly SimpleLazy<T> v3;
	}
}
