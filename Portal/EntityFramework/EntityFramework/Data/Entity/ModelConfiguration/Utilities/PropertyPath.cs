using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Data.Entity.ModelConfiguration.Utilities
{
	// Token: 0x020001BF RID: 447
	internal class PropertyPath : IEnumerable<PropertyInfo>, IEnumerable
	{
		// Token: 0x060017C6 RID: 6086 RVA: 0x0004085E File Offset: 0x0003EA5E
		public PropertyPath(IEnumerable<PropertyInfo> components)
		{
			this._components.AddRange(components);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0004087D File Offset: 0x0003EA7D
		public PropertyPath(PropertyInfo component)
		{
			this._components.Add(component);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0004089C File Offset: 0x0003EA9C
		private PropertyPath()
		{
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x000408AF File Offset: 0x0003EAAF
		public int Count
		{
			get
			{
				return this._components.Count;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x000408BC File Offset: 0x0003EABC
		public static PropertyPath Empty
		{
			get
			{
				return PropertyPath._empty;
			}
		}

		// Token: 0x170005CB RID: 1483
		public PropertyInfo this[int index]
		{
			get
			{
				return this._components[index];
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x000408D4 File Offset: 0x0003EAD4
		public override string ToString()
		{
			StringBuilder propertyPathName = new StringBuilder();
			this._components.Each(delegate(PropertyInfo pi)
			{
				propertyPathName.Append(pi.Name);
				propertyPathName.Append('.');
			});
			return propertyPathName.ToString(0, propertyPathName.Length - 1);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00040922 File Offset: 0x0003EB22
		public bool Equals(PropertyPath other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			return this._components.SequenceEqual(other._components, (PropertyInfo p1, PropertyInfo p2) => p1.IsSameAs(p2));
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0004095F File Offset: 0x0003EB5F
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != typeof(PropertyPath)) && this.Equals((PropertyPath)obj)));
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00040991 File Offset: 0x0003EB91
		public override int GetHashCode()
		{
			return this._components.Aggregate(0, (int t, PropertyInfo n) => t ^ (n.DeclaringType.GetHashCode() * n.Name.GetHashCode() * 397));
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x000409BE File Offset: 0x0003EBBE
		public static bool operator ==(PropertyPath left, PropertyPath right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x000409C7 File Offset: 0x0003EBC7
		public static bool operator !=(PropertyPath left, PropertyPath right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x000409D3 File Offset: 0x0003EBD3
		IEnumerator<PropertyInfo> IEnumerable<PropertyInfo>.GetEnumerator()
		{
			return this._components.GetEnumerator();
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x000409E5 File Offset: 0x0003EBE5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._components.GetEnumerator();
		}

		// Token: 0x04000A45 RID: 2629
		private static readonly PropertyPath _empty = new PropertyPath();

		// Token: 0x04000A46 RID: 2630
		private readonly List<PropertyInfo> _components = new List<PropertyInfo>();
	}
}
