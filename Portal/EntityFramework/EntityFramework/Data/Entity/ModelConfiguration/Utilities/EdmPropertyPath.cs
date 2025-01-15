using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;

namespace System.Data.Entity.ModelConfiguration.Utilities
{
	// Token: 0x020001BD RID: 445
	internal class EdmPropertyPath : IEnumerable<EdmProperty>, IEnumerable
	{
		// Token: 0x060017B2 RID: 6066 RVA: 0x00040550 File Offset: 0x0003E750
		public EdmPropertyPath(IEnumerable<EdmProperty> components)
		{
			this._components.AddRange(components);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0004056F File Offset: 0x0003E76F
		public EdmPropertyPath(EdmProperty component)
		{
			this._components.Add(component);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0004058E File Offset: 0x0003E78E
		private EdmPropertyPath()
		{
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x000405A1 File Offset: 0x0003E7A1
		public static EdmPropertyPath Empty
		{
			get
			{
				return EdmPropertyPath._empty;
			}
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x000405A8 File Offset: 0x0003E7A8
		public override string ToString()
		{
			StringBuilder propertyPathName = new StringBuilder();
			this._components.Each(delegate(EdmProperty pi)
			{
				propertyPathName.Append(pi.Name);
				propertyPathName.Append('.');
			});
			return propertyPathName.ToString(0, propertyPathName.Length - 1);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x000405F6 File Offset: 0x0003E7F6
		public bool Equals(EdmPropertyPath other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			return this._components.SequenceEqual(other._components, (EdmProperty p1, EdmProperty p2) => p1 == p2);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00040633 File Offset: 0x0003E833
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != typeof(EdmPropertyPath)) && this.Equals((EdmPropertyPath)obj)));
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00040665 File Offset: 0x0003E865
		public override int GetHashCode()
		{
			return this._components.Aggregate(0, (int t, EdmProperty n) => t + n.GetHashCode());
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00040692 File Offset: 0x0003E892
		public static bool operator ==(EdmPropertyPath left, EdmPropertyPath right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0004069B File Offset: 0x0003E89B
		public static bool operator !=(EdmPropertyPath left, EdmPropertyPath right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x000406A7 File Offset: 0x0003E8A7
		IEnumerator<EdmProperty> IEnumerable<EdmProperty>.GetEnumerator()
		{
			return this._components.GetEnumerator();
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x000406B9 File Offset: 0x0003E8B9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._components.GetEnumerator();
		}

		// Token: 0x04000A42 RID: 2626
		private static readonly EdmPropertyPath _empty = new EdmPropertyPath();

		// Token: 0x04000A43 RID: 2627
		private readonly List<EdmProperty> _components = new List<EdmProperty>();
	}
}
