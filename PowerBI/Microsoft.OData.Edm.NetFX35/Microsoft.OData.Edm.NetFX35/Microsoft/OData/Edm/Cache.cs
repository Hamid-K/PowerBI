using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020001E8 RID: 488
	internal class Cache<TContainer, TProperty>
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x00020B18 File Offset: 0x0001ED18
		public TProperty GetValue(TContainer container, Func<TContainer, TProperty> compute, Func<TContainer, TProperty> onCycle)
		{
			object obj = onCycle ?? this;
			object obj2 = this.value;
			if (obj2 == CacheHelper.Unknown)
			{
				lock (obj)
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
					goto IL_0192;
				}
			}
			if (obj2 == CacheHelper.CycleSentinel)
			{
				lock (obj)
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
					goto IL_0192;
				}
			}
			if (obj2 == CacheHelper.SecondPassCycleSentinel)
			{
				lock (obj)
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
			IL_0192:
			return (TProperty)((object)obj2);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00020D00 File Offset: 0x0001EF00
		public void Clear(Func<TContainer, TProperty> onCycle)
		{
			lock (onCycle ?? this)
			{
				if (this.value != CacheHelper.CycleSentinel && this.value != CacheHelper.SecondPassCycleSentinel)
				{
					this.value = CacheHelper.Unknown;
				}
			}
		}

		// Token: 0x04000534 RID: 1332
		private object value = CacheHelper.Unknown;
	}
}
