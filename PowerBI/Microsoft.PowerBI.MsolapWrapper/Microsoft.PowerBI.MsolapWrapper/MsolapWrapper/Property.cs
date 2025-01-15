using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using ATL;

namespace MsolapWrapper
{
	// Token: 0x02000004 RID: 4
	internal class Property : IDisposable
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00002068 File Offset: 0x00001468
		internal unsafe Property(uint dwOptions, tagDBID colid, uint dwPropId, short boolVal)
		{
			tagDBPROP* ptr = <Module>.@new(72UL);
			tagDBPROP* ptr2;
			if (ptr != null)
			{
				initblk(ptr, 0, 72L);
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
			this._prop = ptr2;
			<Module>.VariantInit((tagVARIANT*)(ptr2 + 48L / (long)sizeof(tagDBPROP)));
			*(int*)(this._prop + 4L / (long)sizeof(tagDBPROP)) = dwOptions;
			cpblk(this._prop + 16L / (long)sizeof(tagDBPROP), ref colid, 32);
			*(int*)this._prop = dwPropId;
			*(short*)(this._prop + 48L / (long)sizeof(tagDBPROP)) = 11;
			*(short*)(this._prop + 56L / (long)sizeof(tagDBPROP)) = boolVal;
			GC.KeepAlive(this);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002B3C File Offset: 0x00001F3C
		internal unsafe Property(uint dwOptions, tagDBID colid, uint dwPropId, CComBSTR* bstrVal)
		{
			try
			{
				base..ctor();
				tagDBPROP* ptr = <Module>.@new(72UL);
				tagDBPROP* ptr2;
				if (ptr != null)
				{
					initblk(ptr, 0, 72L);
					ptr2 = ptr;
				}
				else
				{
					ptr2 = null;
				}
				this._prop = ptr2;
				<Module>.VariantInit((tagVARIANT*)(ptr2 + 48L / (long)sizeof(tagDBPROP)));
				*(int*)(this._prop + 4L / (long)sizeof(tagDBPROP)) = dwOptions;
				cpblk(this._prop + 16L / (long)sizeof(tagDBPROP), ref colid, 32);
				*(int*)this._prop = dwPropId;
				*(short*)(this._prop + 48L / (long)sizeof(tagDBPROP)) = 8;
				*(long*)(this._prop + 56L / (long)sizeof(tagDBPROP)) = <Module>.ATL.CComBSTR.Copy(bstrVal);
				GC.KeepAlive(this);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)bstrVal);
				throw;
			}
			<Module>.SysFreeString(*(long*)bstrVal);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00001FE0 File Offset: 0x000013E0
		internal unsafe Property(uint dwOptions, tagDBID colid, uint dwPropId, int intVal)
		{
			tagDBPROP* ptr = <Module>.@new(72UL);
			tagDBPROP* ptr2;
			if (ptr != null)
			{
				initblk(ptr, 0, 72L);
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
			this._prop = ptr2;
			<Module>.VariantInit((tagVARIANT*)(ptr2 + 48L / (long)sizeof(tagDBPROP)));
			*(int*)(this._prop + 4L / (long)sizeof(tagDBPROP)) = dwOptions;
			cpblk(this._prop + 16L / (long)sizeof(tagDBPROP), ref colid, 32);
			*(int*)this._prop = dwPropId;
			*(short*)(this._prop + 48L / (long)sizeof(tagDBPROP)) = 3;
			*(int*)(this._prop + 56L / (long)sizeof(tagDBPROP)) = intVal;
			GC.KeepAlive(this);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000020F0 File Offset: 0x000014F0
		private unsafe void !Property()
		{
			tagDBPROP* prop = this._prop;
			if (prop != null)
			{
				if (*(ushort*)(prop + 48L / (long)sizeof(tagDBPROP)) == 8)
				{
					<Module>.SysFreeString(*(long*)(prop + 56L / (long)sizeof(tagDBPROP)));
				}
				<Module>.delete((void*)this._prop, 72UL);
				this._prop = null;
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00002140 File Offset: 0x00001540
		private void ~Property()
		{
			this.!Property();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000017F8 File Offset: 0x00000BF8
		internal unsafe tagDBPROP* Prop
		{
			get
			{
				return this._prop;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000280C File Offset: 0x00001C0C
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.!Property();
			}
			else
			{
				try
				{
					this.!Property();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003058 File Offset: 0x00002458
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00002858 File Offset: 0x00001C58
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x04000079 RID: 121
		private unsafe tagDBPROP* _prop;
	}
}
