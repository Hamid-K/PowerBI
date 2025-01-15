using System;
using Microsoft.InfoNav;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000083 RID: 131
	public abstract class ConceptualBinding : IEquatable<ConceptualBinding>
	{
		// Token: 0x06000253 RID: 595 RVA: 0x00005848 File Offset: 0x00003A48
		public ConceptualBinding(string entity, string schema = null)
		{
			this.Entity = entity;
			this.Schema = schema ?? string.Empty;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00005867 File Offset: 0x00003A67
		public string Schema { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000586F File Offset: 0x00003A6F
		public string Entity { get; }

		// Token: 0x06000256 RID: 598
		public abstract bool Equals(ConceptualBinding other);

		// Token: 0x06000257 RID: 599
		protected abstract int GetHashCodeCore();

		// Token: 0x06000258 RID: 600 RVA: 0x00005877 File Offset: 0x00003A77
		public sealed override bool Equals(object obj)
		{
			return this.Equals(obj as ConceptualBinding);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00005888 File Offset: 0x00003A88
		public sealed override int GetHashCode()
		{
			int num = Hashing.CombineHash(ConceptualNameComparer.Instance.GetHashCode(this.Schema), ConceptualNameComparer.Instance.GetHashCode(this.Entity));
			int hashCodeCore = this.GetHashCodeCore();
			if (hashCodeCore != 0)
			{
				num = Hashing.CombineHash(num, hashCodeCore);
			}
			return num;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000058CE File Offset: 0x00003ACE
		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
