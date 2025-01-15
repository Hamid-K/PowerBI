using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm
{
	// Token: 0x020000A5 RID: 165
	internal sealed class ExtensionEdmItemCollection<T> : IReadOnlyExtensionEdmItemCollection<T>
	{
		// Token: 0x0600078C RID: 1932 RVA: 0x0001D515 File Offset: 0x0001B715
		internal ExtensionEdmItemCollection()
		{
			this.m_items = new Dictionary<ExtensionEdmItemCollection<T>.Key, T>();
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x0001D528 File Offset: 0x0001B728
		public int Count
		{
			get
			{
				return this.m_items.Count;
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001D535 File Offset: 0x0001B735
		public bool TryGetItem(string entitySetReferenceName, string itemReferenceName, out T item)
		{
			return this.m_items.TryGetValue(new ExtensionEdmItemCollection<T>.Key(entitySetReferenceName, itemReferenceName), out item);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001D54A File Offset: 0x0001B74A
		public void AddItem(string entitySetReferenceName, string itemReferenceName, T item)
		{
			this.m_items.Add(new ExtensionEdmItemCollection<T>.Key(entitySetReferenceName, itemReferenceName), item);
		}

		// Token: 0x040003C6 RID: 966
		private readonly Dictionary<ExtensionEdmItemCollection<T>.Key, T> m_items;

		// Token: 0x020002A7 RID: 679
		private struct Key : IEquatable<ExtensionEdmItemCollection<T>.Key>
		{
			// Token: 0x060015D7 RID: 5591 RVA: 0x000509BE File Offset: 0x0004EBBE
			public Key(string entitySetReferenceName, string itemReferenceName)
			{
				this.m_entitySetReferenceName = entitySetReferenceName;
				this.m_itemReferenceName = itemReferenceName;
			}

			// Token: 0x060015D8 RID: 5592 RVA: 0x000509CE File Offset: 0x0004EBCE
			public override bool Equals(object obj)
			{
				return obj is ExtensionEdmItemCollection<T>.Key && this.Equals((ExtensionEdmItemCollection<T>.Key)obj);
			}

			// Token: 0x060015D9 RID: 5593 RVA: 0x000509E6 File Offset: 0x0004EBE6
			public bool Equals(ExtensionEdmItemCollection<T>.Key other)
			{
				return ExtensionEdmItemCollection<T>.Key.NameComparer.Equals(this.m_entitySetReferenceName, other.m_entitySetReferenceName) && ExtensionEdmItemCollection<T>.Key.NameComparer.Equals(this.m_itemReferenceName, other.m_itemReferenceName);
			}

			// Token: 0x060015DA RID: 5594 RVA: 0x00050A18 File Offset: 0x0004EC18
			public override int GetHashCode()
			{
				return Hashing.CombineHash(ExtensionEdmItemCollection<T>.Key.NameComparer.GetHashCode(this.m_entitySetReferenceName), ExtensionEdmItemCollection<T>.Key.NameComparer.GetHashCode(this.m_itemReferenceName));
			}

			// Token: 0x04000A38 RID: 2616
			private static readonly StringComparer NameComparer = EdmItem.ReferenceNameComparer;

			// Token: 0x04000A39 RID: 2617
			private readonly string m_entitySetReferenceName;

			// Token: 0x04000A3A RID: 2618
			private readonly string m_itemReferenceName;
		}
	}
}
