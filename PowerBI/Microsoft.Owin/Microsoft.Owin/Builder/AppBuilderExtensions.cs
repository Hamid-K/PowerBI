using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Owin;

namespace Microsoft.Owin.Builder
{
	// Token: 0x02000045 RID: 69
	public static class AppBuilderExtensions
	{
		// Token: 0x06000267 RID: 615 RVA: 0x0000709C File Offset: 0x0000529C
		public static Func<IDictionary<string, object>, Task> Build(this IAppBuilder builder)
		{
			return builder.Build<Func<IDictionary<string, object>, Task>>();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000070A4 File Offset: 0x000052A4
		public static TApp Build<TApp>(this IAppBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			return (TApp)((object)builder.Build(typeof(TApp)));
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000070CC File Offset: 0x000052CC
		public static void AddSignatureConversion(this IAppBuilder builder, Delegate conversion)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			object obj;
			if (builder.Properties.TryGetValue("builder.AddSignatureConversion", out obj))
			{
				Action<Delegate> action = obj as Action<Delegate>;
				if (action != null)
				{
					action(conversion);
					return;
				}
			}
			throw new MissingMethodException(builder.GetType().FullName, "AddSignatureConversion");
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00007122 File Offset: 0x00005322
		public static void AddSignatureConversion<T1, T2>(this IAppBuilder builder, Func<T1, T2> conversion)
		{
			builder.AddSignatureConversion(conversion);
		}
	}
}
