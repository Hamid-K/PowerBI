using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000126 RID: 294
	public class ColumnGroupingAnnotations
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x0000FEF9 File Offset: 0x0000E0F9
		public ColumnGroupingAnnotations(IReadOnlyDictionary<IConceptualColumn, ColumnGroupingAnnotation> annotations)
		{
			this._annotations = annotations;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0000FF08 File Offset: 0x0000E108
		public bool TryGetAnnotation(IConceptualColumn column, out ColumnGroupingAnnotation annotation)
		{
			return this._annotations.TryGetValue(column, out annotation);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0000FF17 File Offset: 0x0000E117
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0000FF20 File Offset: 0x0000E120
		public string ToString(string[] columnNamesToIgnore)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IEnumerable<KeyValuePair<IConceptualColumn, ColumnGroupingAnnotation>> enumerable;
			if (columnNamesToIgnore != null)
			{
				HashSet<string> columnNamesToIgnoreSet = columnNamesToIgnore.ToHashSet(null);
				enumerable = from item in this._annotations
					where !columnNamesToIgnoreSet.Contains(this.GetName(item.Key))
					orderby this.GetName(item.Key)
					select item;
			}
			else
			{
				enumerable = this._annotations.OrderBy((KeyValuePair<IConceptualColumn, ColumnGroupingAnnotation> item) => this.GetName(item.Key));
			}
			Func<KeyValuePair<IConceptualColumn, ColumnGroupingAnnotation>, string> func = (KeyValuePair<IConceptualColumn, ColumnGroupingAnnotation> kv) => this.GetName(kv.Key) + Environment.NewLine + kv.Value.ToString();
			stringBuilder.AppendMany(enumerable, Environment.NewLine, func);
			return stringBuilder.ToString();
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0000FFB2 File Offset: 0x0000E1B2
		private string GetName(IConceptualColumn column)
		{
			return column.Entity.Name + "." + column.Name;
		}

		// Token: 0x04000380 RID: 896
		private IReadOnlyDictionary<IConceptualColumn, ColumnGroupingAnnotation> _annotations;
	}
}
