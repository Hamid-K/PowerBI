using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace MsolapWrapper
{
	// Token: 0x02000007 RID: 7
	internal abstract class PropertySetCollection : IDisposable
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00003278 File Offset: 0x00002678
		internal unsafe tagDBPROPSET* ToDbPropSet()
		{
			if (this._dbpropsets == null)
			{
				int count = this._sets.Count;
				if (count == null)
				{
					return null;
				}
				ulong num = (ulong)count;
				void* ptr = <Module>.new[]((num > 576460752303423487UL) ? ulong.MaxValue : (num * 32UL));
				this._dbpropsets = (tagDBPROPSET*)ptr;
				if (ptr == null)
				{
					WrapperContract.Fail("Failed to allocated array for DBPROPSET");
				}
				Dictionary<int, PropertySet>.KeyCollection.Enumerator enumerator = this._sets.Keys.GetEnumerator();
				if (enumerator.MoveNext())
				{
					long num2 = 0L;
					do
					{
						int num3 = enumerator.Current;
						tagDBPROPSET tagDBPROPSET;
						tagDBPROPSET* ptr2 = this._sets[num3].ToDbPropSet(&tagDBPROPSET);
						cpblk(this._dbpropsets + num2 / (long)sizeof(tagDBPROPSET), ptr2, 32);
						num2 += 32L;
					}
					while (enumerator.MoveNext());
				}
			}
			GC.KeepAlive(this);
			return this._dbpropsets;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00001814 File Offset: 0x00000C14
		internal int Size
		{
			get
			{
				return this._sets.Count;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00002340 File Offset: 0x00001740
		private unsafe void !PropertySetCollection()
		{
			tagDBPROPSET* dbpropsets = this._dbpropsets;
			if (dbpropsets != null)
			{
				<Module>.delete[]((void*)dbpropsets);
				this._dbpropsets = null;
			}
			Dictionary<int, PropertySet> sets = this._sets;
			if (sets != null)
			{
				Dictionary<int, PropertySet>.ValueCollection.Enumerator enumerator = sets.Values.GetEnumerator();
				if (enumerator.MoveNext())
				{
					do
					{
						PropertySet propertySet = enumerator.Current;
						if (propertySet != null)
						{
							((IDisposable)propertySet).Dispose();
						}
					}
					while (enumerator.MoveNext());
				}
				IDisposable disposable = this._sets as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				this._sets = null;
			}
			HashSet<int> propertiesAlreadySet = this._propertiesAlreadySet;
			if (propertiesAlreadySet != null)
			{
				IDisposable disposable2 = propertiesAlreadySet as IDisposable;
				if (disposable2 != null)
				{
					disposable2.Dispose();
				}
				this._propertiesAlreadySet = null;
			}
			GC.KeepAlive(this);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000023F0 File Offset: 0x000017F0
		private void ~PropertySetCollection()
		{
			this.!PropertySetCollection();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002308 File Offset: 0x00001708
		protected PropertySetCollection()
		{
			this._sets = new Dictionary<int, PropertySet>();
			this._propertiesAlreadySet = new HashSet<int>();
			this._dbpropsets = null;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003240 File Offset: 0x00002640
		protected PropertySet GetPropertySet(int set)
		{
			if (this._sets.ContainsKey(set))
			{
				return this._sets[set];
			}
			return this.AddSet(set);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00002C00 File Offset: 0x00002000
		private unsafe PropertySet AddSet(int set)
		{
			PropertySet propertySet = null;
			if (set != 0)
			{
				if (set != 1)
				{
					WrapperContract.Fail("Unexpected CommandPropertySet");
				}
				else
				{
					propertySet = new PropertySet((_GUID*)(&<Module>.DBPROPSET_MDCOMMAND));
				}
			}
			else
			{
				propertySet = new PropertySet((_GUID*)(&<Module>.DBPROPSET_ROWSET));
			}
			this._sets.Add(set, propertySet);
			return propertySet;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00002944 File Offset: 0x00001D44
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.!PropertySetCollection();
			}
			else
			{
				try
				{
					this.!PropertySetCollection();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000030B8 File Offset: 0x000024B8
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00002990 File Offset: 0x00001D90
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x0400007E RID: 126
		protected Dictionary<int, PropertySet> _sets;

		// Token: 0x0400007F RID: 127
		protected HashSet<int> _propertiesAlreadySet;

		// Token: 0x04000080 RID: 128
		protected unsafe tagDBPROPSET* _dbpropsets;

		// Token: 0x04000081 RID: 129
		protected static int RowsetPropSet = 0;

		// Token: 0x04000082 RID: 130
		protected static int CommandPropSet = 1;
	}
}
