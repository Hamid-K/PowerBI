using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes
{
	// Token: 0x02000147 RID: 327
	public sealed class ConceptualTypeColumn
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x0001171C File Offset: 0x0000F91C
		public ConceptualTypeColumn(ConceptualPrimitiveResultType scalar, string name)
			: this(scalar, name, name)
		{
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00011727 File Offset: 0x0000F927
		public ConceptualTypeColumn(ConceptualPrimitiveResultType scalar, string name, string edmName)
		{
			this.PrimitiveType = scalar;
			this.EdmName = edmName ?? name;
			this.Name = name;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00011749 File Offset: 0x0000F949
		public ConceptualPrimitiveResultType PrimitiveType { get; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00011751 File Offset: 0x0000F951
		public string Name { get; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00011759 File Offset: 0x0000F959
		public string EdmName { get; }

		// Token: 0x06000864 RID: 2148 RVA: 0x00011764 File Offset: 0x0000F964
		public override bool Equals(object other)
		{
			ConceptualTypeColumn conceptualTypeColumn = other as ConceptualTypeColumn;
			return conceptualTypeColumn != null && (this.PrimitiveType.Equals(conceptualTypeColumn.PrimitiveType) && ConceptualNameComparer.Instance.Equals(this.EdmName, conceptualTypeColumn.EdmName)) && ConceptualNameComparer.Instance.Equals(this.Name, conceptualTypeColumn.Name);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000117C0 File Offset: 0x0000F9C0
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.PrimitiveType.GetHashCode(), ConceptualNameComparer.Instance.GetHashCode(this.Name), ConceptualNameComparer.Instance.GetHashCode(this.EdmName));
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000117F4 File Offset: 0x0000F9F4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Column(",
				this.Name,
				",",
				this.EdmName,
				",",
				this.PrimitiveType.ToString(),
				")"
			});
		}
	}
}
