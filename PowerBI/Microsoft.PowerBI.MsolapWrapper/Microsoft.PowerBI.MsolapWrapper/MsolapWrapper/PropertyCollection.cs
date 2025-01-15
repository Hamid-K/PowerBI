using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace MsolapWrapper
{
	// Token: 0x02000005 RID: 5
	internal class PropertyCollection : IDisposable
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x0000215C File Offset: 0x0000155C
		internal PropertyCollection()
		{
			this._properties = new List<Property>();
			this._dbprops = null;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002188 File Offset: 0x00001588
		internal void Add(Property property)
		{
			this._properties.Add(property);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003138 File Offset: 0x00002538
		internal unsafe tagDBPROP* ToDbProp()
		{
			if (this._dbprops == null)
			{
				int count = this._properties.Count;
				if (count == null)
				{
					return null;
				}
				ulong num = (ulong)count;
				void* ptr = <Module>.new[]((num > 256204778801521550UL) ? ulong.MaxValue : (num * 72UL));
				this._dbprops = (tagDBPROP*)ptr;
				if (ptr == null)
				{
					WrapperContract.Fail("Failed to allocated array for DBPROPS");
				}
				int num2 = 0;
				long num3 = count;
				if (0L < num3)
				{
					long num4 = 0L;
					ulong num5 = (ulong)num3;
					do
					{
						tagDBPROP* prop = this._properties[num2].Prop;
						cpblk(this._dbprops + num4 / (long)sizeof(tagDBPROP), prop, 72);
						num2++;
						num4 += 72L;
						num5 -= 1UL;
					}
					while (num5 > 0UL);
				}
			}
			GC.KeepAlive(this);
			return this._dbprops;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000021A8 File Offset: 0x000015A8
		internal int GetSize()
		{
			return this._properties.Count;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000021C8 File Offset: 0x000015C8
		private unsafe void !PropertyCollection()
		{
			tagDBPROP* dbprops = this._dbprops;
			if (dbprops != null)
			{
				<Module>.delete[]((void*)dbprops);
				this._dbprops = null;
			}
			List<Property> properties = this._properties;
			if (properties != null)
			{
				int num = 0;
				if (0 < properties.Count)
				{
					do
					{
						Property property = this._properties[num];
						if (property != null)
						{
							((IDisposable)property).Dispose();
						}
						num++;
					}
					while (num < this._properties.Count);
				}
				IDisposable disposable = this._properties as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				this._properties = null;
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00002258 File Offset: 0x00001658
		private void ~PropertyCollection()
		{
			this.!PropertyCollection();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002874 File Offset: 0x00001C74
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.!PropertyCollection();
			}
			else
			{
				try
				{
					this.!PropertyCollection();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003078 File Offset: 0x00002478
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000028C0 File Offset: 0x00001CC0
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x0400007A RID: 122
		private List<Property> _properties;

		// Token: 0x0400007B RID: 123
		private unsafe tagDBPROP* _dbprops;
	}
}
