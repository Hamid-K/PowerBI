using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SchemaParser
{
	// Token: 0x02000168 RID: 360
	[DataContract]
	[KnownType("GetKnownSubclassesOfTreeElement")]
	public abstract class TreeElement<TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00019313 File Offset: 0x00017513
		protected static List<Type> RegisteredTypes { get; } = new List<Type>();

		// Token: 0x06000809 RID: 2057 RVA: 0x0001931A File Offset: 0x0001751A
		public static IEnumerable<Type> GetKnownSubclassesOfTreeElement()
		{
			return TreeElement<TRegion>.RegisteredTypes;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00019321 File Offset: 0x00017521
		static TreeElement()
		{
			TreeElement<TRegion>.RegisteredTypes.Add(typeof(TRegion));
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00002130 File Offset: 0x00000330
		protected TreeElement()
		{
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00019341 File Offset: 0x00017541
		protected TreeElement(string name, string type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x0600080D RID: 2061
		public abstract bool IsInside(TRegion r);

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00019357 File Offset: 0x00017557
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x0001935F File Offset: 0x0001755F
		public TreeElement<TRegion> Parent { get; internal set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00019368 File Offset: 0x00017568
		public IEnumerable<TreeElement<TRegion>> Ancestors
		{
			get
			{
				TreeElement<TRegion> treeElement;
				for (treeElement = this.Parent; treeElement != null; treeElement = treeElement.Parent)
				{
					yield return treeElement;
				}
				treeElement = null;
				yield break;
			}
		}

		// Token: 0x0400037C RID: 892
		[DataMember]
		public string Name;

		// Token: 0x0400037D RID: 893
		[DataMember]
		public string Type;
	}
}
