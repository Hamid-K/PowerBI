using System;
using System.Collections;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000D0 RID: 208
	public sealed class ValidationResultCollection : ICollection, IEnumerable
	{
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0002A3E0 File Offset: 0x000285E0
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0002A3E3 File Offset: 0x000285E3
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0002A3E6 File Offset: 0x000285E6
		void ICollection.CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002A3F5 File Offset: 0x000285F5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x17000245 RID: 581
		public ValidationResult this[int index]
		{
			get
			{
				return (ValidationResult)this.items[index];
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0002A415 File Offset: 0x00028615
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002A422 File Offset: 0x00028622
		internal void Add(ModelComponent source, ValidationRule rule, params string[] parameters)
		{
			this.items.Add(new ValidationResult(source, rule, parameters));
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002A438 File Offset: 0x00028638
		internal void AddError(ModelComponent source, string description)
		{
			this.items.Add(new ValidationResult(source, description, ValidationRule.ErrorHigh));
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002A452 File Offset: 0x00028652
		internal void AddErrorMedium(ModelComponent source, string description)
		{
			this.items.Add(new ValidationResult(source, description, ValidationRule.ErrorMedium));
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002A46C File Offset: 0x0002866C
		internal void AddIfAllowed(ModelComponent source, ValidationRule rule, DismissedValidationRuleCollection dismissedRules, DismissedValidationResultCollection dismissedResults, params string[] parameters)
		{
			if ((dismissedRules == null || !dismissedRules.Contains(rule)) && (dismissedResults == null || !dismissedResults.Contains(source, rule, parameters)))
			{
				this.Add(source, rule, parameters);
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0002A495 File Offset: 0x00028695
		public void Clear()
		{
			this.items.Clear();
		}

		// Token: 0x04000715 RID: 1813
		private ArrayList items = new ArrayList();
	}
}
