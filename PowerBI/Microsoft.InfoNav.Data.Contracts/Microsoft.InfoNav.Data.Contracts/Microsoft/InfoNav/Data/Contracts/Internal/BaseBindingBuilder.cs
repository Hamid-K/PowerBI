using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001B8 RID: 440
	public abstract class BaseBindingBuilder<TObject, TParent> : BaseBuilder<TObject, TParent>
	{
		// Token: 0x06000BA6 RID: 2982 RVA: 0x00016E3E File Offset: 0x0001503E
		protected BaseBindingBuilder(TParent parent)
			: base(parent)
		{
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00016E47 File Offset: 0x00015047
		protected static void AddToLazyList<T>(ref IList<T> list, T item)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.Add(item);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00016E5C File Offset: 0x0001505C
		protected static void AddToLazyList<T>(ref List<T> list, T item)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.Add(item);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00016E74 File Offset: 0x00015074
		protected static TObj SafeBuild<TObj, TPar>(BaseBindingBuilder<TObj, TPar> objectBuilder)
		{
			if (objectBuilder == null)
			{
				return default(TObj);
			}
			return objectBuilder.Build();
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00016E94 File Offset: 0x00015094
		protected static IList<TObj> SafeBuild<TObj, TBuilder, TPar>(IList<TBuilder> objectBuilders) where TBuilder : BaseBuilder<TObj, TPar>
		{
			if (objectBuilders == null)
			{
				return null;
			}
			return objectBuilders.Select((TBuilder obj) => obj.Build()).ToList<TObj>();
		}
	}
}
