using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200003C RID: 60
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class OwnedCollection<T, O> : CheckedCollection<T> where T : IOwned<O>
	{
		// Token: 0x06000253 RID: 595 RVA: 0x000095E1 File Offset: 0x000077E1
		protected OwnedCollection(O owner)
		{
			if (owner == null)
			{
				throw new InternalModelingException("owner is null");
			}
			this.m_owner = owner;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009603 File Offset: 0x00007803
		protected override void InsertItem(int index, T item)
		{
			base.CheckWriteable();
			this.ValidateItem(item);
			item.SetOwner(this.m_owner);
			base.InsertItem(index, item);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000962D File Offset: 0x0000782D
		protected override void SetItem(int index, T item)
		{
			base.CheckWriteable();
			this.ValidateItem(item);
			item.SetOwner(this.m_owner);
			base.SetItem(index, item);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009658 File Offset: 0x00007858
		protected override void RemoveItem(int index)
		{
			base.CheckWriteable();
			T t = base[index];
			t.SetOwner(default(O));
			base.RemoveItem(index);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009690 File Offset: 0x00007890
		protected override void ClearItems()
		{
			base.CheckWriteable();
			foreach (T t in this)
			{
				ref T ptr = ref t;
				if (default(T) == null)
				{
					T t2 = t;
					ptr = ref t2;
				}
				ptr.SetOwner(default(O));
			}
			base.ClearItems();
		}

		// Token: 0x04000142 RID: 322
		private readonly O m_owner;
	}
}
