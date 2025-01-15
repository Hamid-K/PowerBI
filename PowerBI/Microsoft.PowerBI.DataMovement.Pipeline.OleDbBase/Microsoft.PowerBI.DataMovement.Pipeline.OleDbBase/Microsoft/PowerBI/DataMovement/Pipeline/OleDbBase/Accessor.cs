using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000007 RID: 7
	public class Accessor : IAccessor
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020B3 File Offset: 0x000002B3
		public Accessor()
		{
			this.binders = new Dictionary<int, RefCount<Binder>>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020C6 File Offset: 0x000002C6
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal Binder GetBinder(HACCESSOR accessor)
		{
			return this.GetRefCountBinder(accessor).Value;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020D4 File Offset: 0x000002D4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private RefCount<Binder> GetRefCountBinder(HACCESSOR accessor)
		{
			RefCount<Binder> refCount;
			if (!this.binders.TryGetValue((int)accessor.Value, out refCount))
			{
				throw new COMException("Invalid accessor", -2147217920);
			}
			return refCount;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002108 File Offset: 0x00000308
		unsafe void IAccessor.AddRefAccessor(HACCESSOR accessor, uint* refCount)
		{
			if (refCount != null)
			{
				*refCount = 0U;
			}
			int num = this.GetRefCountBinder(accessor).AddRef();
			if (refCount != null)
			{
				*refCount = (uint)num;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002134 File Offset: 0x00000334
		unsafe void IAccessor.CreateAccessor(DBACCESSORFLAGS accessorFlags, DBCOUNTITEM countBindings, DBBINDING* nativeBindings, DBLENGTH rowSize, out HACCESSOR nativeAccessor, DBBINDSTATUS* nativeStatus)
		{
			nativeAccessor = default(HACCESSOR);
			if (((accessorFlags & DBACCESSORFLAGS.ROWDATA) != DBACCESSORFLAGS.ROWDATA && (accessorFlags & DBACCESSORFLAGS.PARAMETERDATA) != DBACCESSORFLAGS.PARAMETERDATA) || accessorFlags >= DBACCESSORFLAGS.INHERITED)
			{
				throw new COMException("Invalid accessor flags", -2147217850);
			}
			if (countBindings.Value > 2147483647UL || rowSize.Value > 2147483647UL)
			{
				throw new COMException("Invalid accessor flags", -2147217850);
			}
			int num = (int)countBindings.Value;
			if ((accessorFlags & DBACCESSORFLAGS.PASSBYREF) == DBACCESSORFLAGS.PASSBYREF)
			{
				throw new COMException("By ref accessors not supported", -2147217848);
			}
			DBACCESSORFLAGS dbaccessorflags = DBACCESSORFLAGS.OPTIMIZED;
			accessorFlags &= ~dbaccessorflags;
			bool flag = false;
			List<Binding> list = new List<Binding>();
			for (int i = 0; i < num; i++)
			{
				DBBINDING dbbinding = nativeBindings[i];
				if (this.ValidBinding(ref dbbinding))
				{
					list.Add(new Binding(ref dbbinding));
					if (nativeStatus != null)
					{
						nativeStatus[i] = DBBINDSTATUS.OK;
					}
				}
				else
				{
					flag = true;
					if (nativeStatus != null)
					{
						nativeStatus[i] = DBBINDSTATUS.BADBINDINFO;
					}
				}
			}
			if (flag)
			{
				throw new COMException("Errors occurred", -2147217887);
			}
			int num2 = this.CreateAccessor();
			this.binders.Add(num2, new RefCount<Binder>(new Binder(accessorFlags, rowSize, list.ToArray())));
			nativeAccessor = new HACCESSOR
			{
				Value = (long)num2
			};
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000227C File Offset: 0x0000047C
		unsafe void IAccessor.GetBindings(HACCESSOR accessor, out DBACCESSORFLAGS accessorFlags, out DBCOUNTITEM countBindings, out DBBINDING* nativeBindings)
		{
			countBindings = default(DBCOUNTITEM);
			nativeBindings = (IntPtr)((UIntPtr)0);
			Binder binder = this.GetBinder(accessor);
			using (ComHeap comHeap = new ComHeap())
			{
				Binding[] bindings = binder.Bindings;
				DBBINDING* ptr = (DBBINDING*)comHeap.AllocArray(bindings.Length, sizeof(DBBINDING));
				for (int i = 0; i < bindings.Length; i++)
				{
					Binding binding = bindings[i];
					DBBINDING* ptr2 = ptr + i;
					ptr2->Ordinal = binding.Ordinal;
					ptr2->Value = binding.ValueOffset;
					ptr2->Length = binding.LengthOffset;
					ptr2->Status = binding.StatusOffset;
					ptr2->TypeInfo = null;
					ptr2->Object = null;
					ptr2->BindExt = null;
					ptr2->Part = binding.Part;
					ptr2->MemOwner = binding.MemoryOwner;
					ptr2->ParamIO = binding.ParamIO;
					ptr2->MaxLen = binding.DestMaxLength;
					ptr2->Flags = binding.Flags;
					ptr2->Type = binding.DestType;
					ptr2->Precision = binding.Precision;
					ptr2->Scale = binding.Scale;
				}
				comHeap.Commit();
				countBindings = new DBCOUNTITEM
				{
					Value = (ulong)bindings.Length
				};
				accessorFlags = binder.AccessorFlags;
				nativeBindings = ptr;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023F0 File Offset: 0x000005F0
		unsafe void IAccessor.ReleaseAccessor(HACCESSOR accessor, uint* refCount)
		{
			if (refCount != null)
			{
				*refCount = 0U;
			}
			int num = this.GetRefCountBinder(accessor).Release();
			if (num == 0)
			{
				this.binders.Remove((int)accessor.Value);
			}
			if (refCount != null)
			{
				*refCount = (uint)num;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002430 File Offset: 0x00000630
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

		// Token: 0x0600000F RID: 15 RVA: 0x00002460 File Offset: 0x00000660
		private unsafe bool ValidBinding(ref DBBINDING nativeBinding)
		{
			bool part = nativeBinding.Part != DBPART.INVALID;
			DBTYPE type = nativeBinding.Type;
			if (!part)
			{
				return false;
			}
			if (Accessor.HasFlag(type, (DBTYPE)24576))
			{
				return false;
			}
			if (Accessor.HasFlag(type, (DBTYPE)20480))
			{
				return false;
			}
			if (Accessor.HasFlag(type, (DBTYPE)12288))
			{
				return false;
			}
			if (type == DBTYPE.NULL || type == DBTYPE.EMPTY)
			{
				return false;
			}
			if (Accessor.HasFlag(type, DBTYPE.RESERVED))
			{
				return false;
			}
			if (type == DBTYPE.BYREF || type == (DBTYPE)16385 || type == DBTYPE.BYREF)
			{
				return false;
			}
			if (type != DBTYPE.STR && type != DBTYPE.WSTR && (nativeBinding.Flags & 1U) == 1U)
			{
				return false;
			}
			if (nativeBinding.Flags != 0U && nativeBinding.Flags != 1U)
			{
				return false;
			}
			if (nativeBinding.MemOwner == DBMEMOWNER.PROVIDEROWNED)
			{
				if (!Accessor.HasFlag(type, DBTYPE.BYREF) && !Accessor.HasFlag(type, DBTYPE.VECTOR) && !Accessor.HasFlag(type, DBTYPE.ARRAY) && !Accessor.HasFlag((DBTYPE)49151 & type, DBTYPE.BSTR))
				{
					return false;
				}
			}
			else if (nativeBinding.MemOwner != DBMEMOWNER.CLIENTOWNED && !Accessor.HasFlag(type, DBTYPE.BYREF))
			{
				return false;
			}
			return (nativeBinding.Object == null || !(((DBOBJECT*)nativeBinding.Object)->IId != IID.ISequentialStream)) && nativeBinding.TypeInfo == null && nativeBinding.BindExt == null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025A2 File Offset: 0x000007A2
		private static bool HasFlag(DBTYPE dbToCheck, DBTYPE dbCombo)
		{
			return (dbToCheck & dbCombo) == dbCombo;
		}

		// Token: 0x04000011 RID: 17
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly Dictionary<int, RefCount<Binder>> binders;

		// Token: 0x04000012 RID: 18
		private int nextAccessorId;
	}
}
