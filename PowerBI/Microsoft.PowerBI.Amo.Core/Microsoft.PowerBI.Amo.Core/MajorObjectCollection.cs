using System;
using System.Collections;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200009E RID: 158
	[Guid("28AAE99B-1831-4017-A5CA-8F122C957C29")]
	public abstract class MajorObjectCollection : NamedComponentCollection
	{
		// Token: 0x06000795 RID: 1941 RVA: 0x00025C1A File Offset: 0x00023E1A
		protected MajorObjectCollection(IModelComponent parent)
			: base(parent)
		{
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00025C23 File Offset: 0x00023E23
		protected internal override int Add(ModelComponent item)
		{
			Server.SendCreate(base.Parent, item as IMajorObject);
			return base.Add(item);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00025C3D File Offset: 0x00023E3D
		private int Add(MajorObject item, bool silent)
		{
			if (silent)
			{
				return base.Add(item);
			}
			return this.Add(item);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00025C51 File Offset: 0x00023E51
		protected internal override void Insert(int index, ModelComponent item)
		{
			if (index < 0 || index > base.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.Collections_IndexOutOfRangeException);
			}
			Server.SendCreate(item.Parent, item as IMajorObject);
			base.Insert(index, item);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00025C90 File Offset: 0x00023E90
		internal void CopyTo(MajorObjectCollection destination, bool forceBodyLoading)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destination == this)
			{
				return;
			}
			base.EnsureLoaded();
			Hashtable hashtable = new Hashtable(Math.Min(base.Count, destination.Count));
			foreach (object obj in this)
			{
				MajorObject majorObject = (MajorObject)obj;
				if (destination.Contains(majorObject.ID))
				{
					hashtable.Add(majorObject, destination.GetItem(majorObject.ID, false, null));
				}
			}
			destination.Clear(false);
			foreach (object obj2 in this)
			{
				MajorObject majorObject2 = (MajorObject)obj2;
				MajorObject majorObject3 = (MajorObject)hashtable[majorObject2];
				if (majorObject3 == null)
				{
					majorObject3 = majorObject2.Clone(forceBodyLoading);
				}
				else
				{
					majorObject2.CopyTo(majorObject3, forceBodyLoading);
				}
				destination.Add(majorObject3, true);
			}
			hashtable.Clear();
		}
	}
}
