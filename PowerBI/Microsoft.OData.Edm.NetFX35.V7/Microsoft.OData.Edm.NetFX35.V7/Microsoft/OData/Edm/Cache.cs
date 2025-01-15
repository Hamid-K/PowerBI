using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000012 RID: 18
	internal class Cache<TContainer, TProperty>
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00006B6C File Offset: 0x00004D6C
		public TProperty GetValue(TContainer container, Func<TContainer, TProperty> compute, Func<TContainer, TProperty> onCycle)
		{
			object obj = onCycle ?? this;
			object obj2 = this.value;
			if (obj2 == CacheHelper.Unknown)
			{
				object obj3 = obj;
				lock (obj3)
				{
					if (this.value == CacheHelper.Unknown)
					{
						this.value = CacheHelper.CycleSentinel;
						TProperty tproperty;
						try
						{
							tproperty = compute.Invoke(container);
						}
						catch
						{
							this.value = CacheHelper.Unknown;
							throw;
						}
						if (this.value == CacheHelper.CycleSentinel)
						{
							this.value = ((typeof(TProperty) == typeof(bool)) ? CacheHelper.BoxedBool((bool)((object)tproperty)) : tproperty);
						}
					}
					obj2 = this.value;
					goto IL_0194;
				}
			}
			if (obj2 == CacheHelper.CycleSentinel)
			{
				object obj4 = obj;
				lock (obj4)
				{
					if (this.value == CacheHelper.CycleSentinel)
					{
						this.value = CacheHelper.SecondPassCycleSentinel;
						try
						{
							compute.Invoke(container);
						}
						catch
						{
							this.value = CacheHelper.CycleSentinel;
							throw;
						}
						if (this.value == CacheHelper.SecondPassCycleSentinel)
						{
							this.value = onCycle.Invoke(container);
						}
					}
					else if (this.value == CacheHelper.Unknown)
					{
						return this.GetValue(container, compute, onCycle);
					}
					obj2 = this.value;
					goto IL_0194;
				}
			}
			if (obj2 == CacheHelper.SecondPassCycleSentinel)
			{
				object obj5 = obj;
				lock (obj5)
				{
					if (this.value == CacheHelper.SecondPassCycleSentinel)
					{
						this.value = onCycle.Invoke(container);
					}
					else if (this.value == CacheHelper.Unknown)
					{
						return this.GetValue(container, compute, onCycle);
					}
					obj2 = this.value;
				}
			}
			IL_0194:
			return (TProperty)((object)obj2);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006D58 File Offset: 0x00004F58
		public void Clear(Func<TContainer, TProperty> onCycle)
		{
			object obj = onCycle ?? this;
			lock (obj)
			{
				if (this.value != CacheHelper.CycleSentinel && this.value != CacheHelper.SecondPassCycleSentinel)
				{
					this.value = CacheHelper.Unknown;
				}
			}
		}

		// Token: 0x04000024 RID: 36
		private object value = CacheHelper.Unknown;
	}
}
