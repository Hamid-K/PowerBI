using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace MsolapWrapper
{
	// Token: 0x02000006 RID: 6
	internal class PropertySet : IDisposable
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00002274 File Offset: 0x00001674
		internal unsafe PropertySet(_GUID* guid)
		{
			this._guid = guid;
			this._propertyCollection = new PropertyCollection();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000031FC File Offset: 0x000025FC
		internal unsafe tagDBPROPSET* ToDbPropSet(tagDBPROPSET* A_1)
		{
			*(long*)A_1 = this._propertyCollection.ToDbProp();
			*(int*)(A_1 + 8L / (long)sizeof(tagDBPROPSET)) = this._propertyCollection.GetSize();
			cpblk(A_1 + 12L / (long)sizeof(tagDBPROPSET), this._guid, 16);
			return A_1;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000022A0 File Offset: 0x000016A0
		internal void Add(Property property)
		{
			this._propertyCollection.Add(property);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000022C0 File Offset: 0x000016C0
		private void !PropertySet()
		{
			PropertyCollection propertyCollection = this._propertyCollection;
			if (propertyCollection != null)
			{
				((IDisposable)propertyCollection).Dispose();
				this._propertyCollection = null;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000022EC File Offset: 0x000016EC
		private void ~PropertySet()
		{
			this.!PropertySet();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000028DC File Offset: 0x00001CDC
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.!PropertySet();
			}
			else
			{
				try
				{
					this.!PropertySet();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003098 File Offset: 0x00002498
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002928 File Offset: 0x00001D28
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x0400007C RID: 124
		private PropertyCollection _propertyCollection;

		// Token: 0x0400007D RID: 125
		private unsafe _GUID* _guid;
	}
}
