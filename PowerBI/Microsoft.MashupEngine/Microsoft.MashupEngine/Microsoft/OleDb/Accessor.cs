using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001E54 RID: 7764
	public class Accessor : IAccessor
	{
		// Token: 0x0600BEA8 RID: 48808 RVA: 0x00268F23 File Offset: 0x00267123
		public Accessor()
		{
			this.binders = new Dictionary<int, RefCount<Binder>>();
			this.nextAccessorId = 1;
		}

		// Token: 0x0600BEA9 RID: 48809 RVA: 0x00268F3D File Offset: 0x0026713D
		internal Binder GetBinder(HACCESSOR hAccessor)
		{
			return this.GetRefCountBinder(hAccessor).Value;
		}

		// Token: 0x0600BEAA RID: 48810 RVA: 0x00268F4C File Offset: 0x0026714C
		private RefCount<Binder> GetRefCountBinder(HACCESSOR hAccessor)
		{
			RefCount<Binder> refCount;
			if (!this.binders.TryGetValue((int)hAccessor.value, out refCount))
			{
				throw new COMException("Invalid accessor", -2147217920);
			}
			return refCount;
		}

		// Token: 0x0600BEAB RID: 48811 RVA: 0x00268F80 File Offset: 0x00267180
		unsafe void IAccessor.AddRefAccessor(HACCESSOR hAccessor, uint* pcRefCount)
		{
			if (pcRefCount != null)
			{
				*pcRefCount = 0U;
			}
			int num = this.GetRefCountBinder(hAccessor).AddRef();
			if (pcRefCount != null)
			{
				*pcRefCount = (uint)num;
			}
		}

		// Token: 0x0600BEAC RID: 48812 RVA: 0x00268FAC File Offset: 0x002671AC
		unsafe void IAccessor.CreateAccessor(DBACCESSORFLAGS dwAccessorFlags, DBCOUNTITEM countBindings, DBBINDING* nativeBindings, DBLENGTH cbRowSize, out HACCESSOR nativeAccessor, DBBINDSTATUS* nativeStatus)
		{
			nativeAccessor = default(HACCESSOR);
			if ((dwAccessorFlags & DBACCESSORFLAGS.ROWDATA) != DBACCESSORFLAGS.ROWDATA || dwAccessorFlags >= DBACCESSORFLAGS.INHERITED)
			{
				throw new COMException("Invalid accessor flags", -2147217850);
			}
			if (countBindings.value > 2147483647UL || cbRowSize.value > 2147483647UL)
			{
				throw new COMException("Invalid accessor flags", -2147217850);
			}
			int num = (int)countBindings.value;
			if ((dwAccessorFlags & DBACCESSORFLAGS.PASSBYREF) == DBACCESSORFLAGS.PASSBYREF)
			{
				throw new COMException("By ref accessors not supported", -2147217848);
			}
			if ((dwAccessorFlags & DBACCESSORFLAGS.PARAMETERDATA) == DBACCESSORFLAGS.PARAMETERDATA)
			{
				throw new COMException("Invalid accessor flags", -2147217850);
			}
			DBACCESSORFLAGS dbaccessorflags = DBACCESSORFLAGS.OPTIMIZED;
			dwAccessorFlags &= ~dbaccessorflags;
			bool flag = false;
			List<Binding> list = new List<Binding>();
			for (int i = 0; i < num; i++)
			{
				DBBINDING dbbinding = nativeBindings[i];
				if (this.ValidBinding(ref dbbinding))
				{
					list.Add(new Binding(ref dbbinding));
					nativeStatus[i] = DBBINDSTATUS.OK;
				}
				else
				{
					flag = true;
					nativeStatus[i] = DBBINDSTATUS.BADBINDINFO;
				}
			}
			if (flag)
			{
				throw new COMException("Errors occurred", -2147217887);
			}
			int num2 = this.CreateAccessor();
			this.binders.Add(num2, new RefCount<Binder>(new Binder(dwAccessorFlags, list.ToArray())));
			nativeAccessor = new HACCESSOR
			{
				value = (long)num2
			};
		}

		// Token: 0x0600BEAD RID: 48813 RVA: 0x002690F4 File Offset: 0x002672F4
		unsafe void IAccessor.GetBindings(HACCESSOR hAccessor, out DBACCESSORFLAGS dwAccessorFlags, out DBCOUNTITEM countBindings, out DBBINDING* _nativeBindings)
		{
			countBindings = default(DBCOUNTITEM);
			_nativeBindings = (IntPtr)((UIntPtr)0);
			Binder binder = this.GetBinder(hAccessor);
			using (ComHeap comHeap = new ComHeap())
			{
				Binding[] bindings = binder.Bindings;
				DBBINDING* ptr = (DBBINDING*)comHeap.AllocArray(bindings.Length, sizeof(DBBINDING));
				for (int i = 0; i < bindings.Length; i++)
				{
					Binding binding = bindings[i];
					DBBINDING* ptr2 = ptr + i;
					ptr2->iOrdinal = binding.Ordinal;
					ptr2->obValue = binding.ValueOffset;
					ptr2->obLength = binding.LengthOffset;
					ptr2->obStatus = binding.StatusOffset;
					ptr2->pTypeInfo = null;
					ptr2->pObject = null;
					ptr2->pBindExt = null;
					ptr2->dwPart = binding.Part;
					ptr2->dwMemOwner = binding.MemoryOwner;
					ptr2->eParamIO = binding.ParamIO;
					ptr2->cbMaxLen = binding.DestMaxLength;
					ptr2->dwFlags = binding.Flags;
					ptr2->wType = binding.DestType;
					ptr2->bPrecision = binding.Precision;
					ptr2->bScale = binding.Scale;
				}
				comHeap.Commit();
				countBindings = new DBCOUNTITEM
				{
					value = (ulong)bindings.Length
				};
				dwAccessorFlags = binder.AccessorFlags;
				_nativeBindings = ptr;
			}
		}

		// Token: 0x0600BEAE RID: 48814 RVA: 0x00269268 File Offset: 0x00267468
		unsafe void IAccessor.ReleaseAccessor(HACCESSOR hAccessor, uint* pcRefCount)
		{
			if (pcRefCount != null)
			{
				*pcRefCount = 0U;
			}
			int num = this.GetRefCountBinder(hAccessor).Release();
			if (num == 0)
			{
				this.binders.Remove((int)hAccessor.value);
			}
			if (pcRefCount != null)
			{
				*pcRefCount = (uint)num;
			}
		}

		// Token: 0x0600BEAF RID: 48815 RVA: 0x002692A8 File Offset: 0x002674A8
		private int CreateAccessor()
		{
			int num2;
			do
			{
				int num = this.nextAccessorId;
				this.nextAccessorId = num + 1;
				num2 = num;
			}
			while (this.binders.ContainsKey(num2));
			return num2;
		}

		// Token: 0x0600BEB0 RID: 48816 RVA: 0x002692D8 File Offset: 0x002674D8
		private unsafe bool ValidBinding(ref DBBINDING nativeBinding)
		{
			bool dwPart = nativeBinding.dwPart != DBPART.INVALID;
			DBTYPE wType = nativeBinding.wType;
			if (!dwPart)
			{
				return false;
			}
			if (Accessor.HasFlag(wType, (DBTYPE)24576))
			{
				return false;
			}
			if (Accessor.HasFlag(wType, (DBTYPE)20480))
			{
				return false;
			}
			if (Accessor.HasFlag(wType, (DBTYPE)12288))
			{
				return false;
			}
			if (wType == DBTYPE.NULL || wType == DBTYPE.EMPTY)
			{
				return false;
			}
			if (Accessor.HasFlag(wType, DBTYPE.RESERVED))
			{
				return false;
			}
			if (wType == DBTYPE.BYREF || wType == (DBTYPE)16385 || wType == DBTYPE.BYREF)
			{
				return false;
			}
			if (wType != DBTYPE.STR && wType != DBTYPE.WSTR && (nativeBinding.dwFlags & 1U) == 1U)
			{
				return false;
			}
			if (nativeBinding.dwFlags != 0U && nativeBinding.dwFlags != 1U)
			{
				return false;
			}
			if (nativeBinding.dwMemOwner == DBMEMOWNER.PROVIDEROWNED)
			{
				if (!Accessor.HasFlag(wType, DBTYPE.BYREF) && !Accessor.HasFlag(wType, DBTYPE.VECTOR) && !Accessor.HasFlag(wType, DBTYPE.ARRAY) && !Accessor.HasFlag((DBTYPE)49151 & wType, DBTYPE.BSTR))
				{
					return false;
				}
			}
			else if (nativeBinding.dwMemOwner != DBMEMOWNER.CLIENTOWNED && !Accessor.HasFlag(wType, DBTYPE.BYREF))
			{
				return false;
			}
			return (nativeBinding.pObject == null || !(((DBOBJECT*)nativeBinding.pObject)->iid != IID.ISequentialStream)) && nativeBinding.pTypeInfo == null && nativeBinding.pBindExt == null;
		}

		// Token: 0x0600BEB1 RID: 48817 RVA: 0x0026941A File Offset: 0x0026761A
		private static bool HasFlag(DBTYPE dbToCheck, DBTYPE dbCombo)
		{
			return (dbToCheck & dbCombo) == dbCombo;
		}

		// Token: 0x04006119 RID: 24857
		private readonly Dictionary<int, RefCount<Binder>> binders;

		// Token: 0x0400611A RID: 24858
		private int nextAccessorId;
	}
}
