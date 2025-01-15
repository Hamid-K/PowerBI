using System;
using System.Globalization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000391 RID: 913
	public class ReflectionSinkFactory : ISinkFactory
	{
		// Token: 0x06001C2C RID: 7212 RVA: 0x0006B630 File Offset: 0x00069830
		public ISink Create([NotNull] SinkIdentifier sid)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SinkIdentifier>(sid, "sid");
			ISink sink;
			try
			{
				sink = DynamicLoader.Instantiate(sid.Assembly, sid.SinkType, new Predicate<Type>(DynamicLoader.ImplementsInterface<ISink>), LoadOptions.Explicit, new object[0]) as ISink;
			}
			catch (DynamicLoaderException ex)
			{
				throw new SinkNotFoundException(string.Format(CultureInfo.CurrentCulture, "Sink {0} cannot be created", new object[] { sid }), ex);
			}
			return sink;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x0006B6A8 File Offset: 0x000698A8
		public void Destroy([NotNull] ISink sink)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ISink>(sink, "sink");
			IDisposable disposable = sink as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}
}
