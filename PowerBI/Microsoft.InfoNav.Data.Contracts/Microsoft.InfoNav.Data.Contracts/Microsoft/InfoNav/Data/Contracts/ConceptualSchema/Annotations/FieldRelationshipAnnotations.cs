using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000134 RID: 308
	public sealed class FieldRelationshipAnnotations
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x00010911 File Offset: 0x0000EB11
		public FieldRelationshipAnnotations(IReadOnlyDictionary<IConceptualColumn, FieldRelationshipAnnotation> annotations)
		{
			this._annotations = annotations;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00010920 File Offset: 0x0000EB20
		public bool TryGetAnnotation(IConceptualColumn column, out FieldRelationshipAnnotation annotation)
		{
			return this._annotations.TryGetValue(column, out annotation);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00010930 File Offset: 0x0000EB30
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Func<KeyValuePair<IConceptualColumn, FieldRelationshipAnnotation>, string> func = (KeyValuePair<IConceptualColumn, FieldRelationshipAnnotation> kv) => kv.Key.Name + Environment.NewLine + kv.Value.ToString();
			stringBuilder.AppendMany(this._annotations.OrderBy((KeyValuePair<IConceptualColumn, FieldRelationshipAnnotation> item) => item.Key.Name), Environment.NewLine, func);
			return stringBuilder.ToString();
		}

		// Token: 0x040003B0 RID: 944
		private IReadOnlyDictionary<IConceptualColumn, FieldRelationshipAnnotation> _annotations;
	}
}
