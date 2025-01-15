using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200008E RID: 142
	internal class Cache<TContainer, TProperty>
	{
		// Token: 0x0600039D RID: 925 RVA: 0x00009BB8 File Offset: 0x00007DB8
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
							tproperty = compute(container);
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
					goto IL_01B3;
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
							compute(container);
						}
						catch
						{
							this.value = CacheHelper.CycleSentinel;
							throw;
						}
						if (this.value == CacheHelper.SecondPassCycleSentinel)
						{
							this.value = onCycle(container);
						}
					}
					else if (this.value == CacheHelper.Unknown)
					{
						return this.GetValue(container, compute, onCycle);
					}
					obj2 = this.value;
					goto IL_01B3;
				}
			}
			if (obj2 == CacheHelper.SecondPassCycleSentinel)
			{
				object obj5 = obj;
				lock (obj5)
				{
					if (this.value == CacheHelper.SecondPassCycleSentinel)
					{
						this.value = onCycle(container);
					}
					else if (this.value == CacheHelper.Unknown)
					{
						return this.GetValue(container, compute, onCycle);
					}
					obj2 = this.value;
				}
			}
			IL_01B3:
			return (TProperty)((object)obj2);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00009DC4 File Offset: 0x00007FC4
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

		// Token: 0x0400010A RID: 266
		private object value = CacheHelper.Unknown;
	}
}
